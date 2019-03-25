using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Reflection;

namespace shamsa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool ispin = true;
        bool isbaaz = false;
        bool isjam = false;
        public bool istimer = false;
        public double i = 0;
        public double qabltop;

        int day, year;
        string dayname = "", monthname = "";
        PersianCalendar pc = new PersianCalendar();
        DateTime pdt = DateTime.Today;
        GregorianCalendar mpc = new GregorianCalendar();
        DateTime mdt = DateTime.Today;
        System.Globalization.DateTimeFormatInfo mmname = new System.Globalization.DateTimeFormatInfo();

        win_timer wtForm = new win_timer();
        win_about aForm = new win_about();

        public MainWindow()
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                MessageBox.Show("برنامه هم اکنون در حال اجراست");
                Application.Current.Shutdown();
            }
            InitializeComponent();

            if (Properties.Settings.Default.isstartsmall == true)
            {
                btn_jam.Margin = new Thickness(-30, 0, 0, -30);
                btn_pin.Margin = new Thickness(-30, 0, 0, -30);
                btn_time.Margin = new Thickness(6, 0, 0, 7);
                txt_month.Margin = new Thickness(7, 0, 0, 7);
                Height = 36;
                wtForm.Top = Properties.Settings.Default.ftop + Height;
                isjam = true;
            }
            Width = 32;

            frm_main.Left = SystemParameters.PrimaryScreenWidth - grd_biroon.Width;

            try
            {
                frm_main.Top = Properties.Settings.Default.ftop;
            }
            catch (FileNotFoundException)
            {
                frm_main.Top = 50;
                try
                {
                    Properties.Settings.Default.ftop = frm_main.Top;
                    Properties.Settings.Default.Save();
                }
                catch (Exception)
                {

                }
            }

            if (Properties.Settings.Default.isfirst == true)
            {
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    Assembly curAssembly = Assembly.GetExecutingAssembly();
                    key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                }
                catch
                {

                }
                try
                {
                    Properties.Settings.Default.isfirst = false;
                    Properties.Settings.Default.sup = true;
                    Properties.Settings.Default.Save();
                }
                catch (Exception)
                {

                }
            }

        }

        private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public static string ToPersianNumber(int intNum)
        {
            string chash = intNum.ToString();
            for (int i = 0; i < 10; i++)
            {
                chash = chash.Replace(en[i], pn[i]);
            }

            return chash;
        }

        public void updatetime()
        {
            pdt = DateTime.Today;
            mdt = DateTime.Today;
            day = pc.GetDayOfMonth(pdt);
            switch (pc.GetDayOfWeek(pdt).ToString())
            {
                case "Saturday":
                    dayname = "شنبه";
                    break;
                case "Sunday":
                    dayname = "یکشنبه";
                    break;
                case "Monday":
                    dayname = "دوشنبه";
                    break;
                case "Tuesday":
                    dayname = "سه شنبه";
                    break;
                case "Wednesday":
                    dayname = "چهارشنبه";
                    break;
                case "Thursday":
                    dayname = "پنجشنبه";
                    break;
                case "Friday":
                    dayname = "جمعه";
                    break;
            }
            switch (pc.GetMonth(pdt).ToString())
            {
                case "1":
                    monthname = "فروردین";
                    break;
                case "2":
                    monthname = "اردیبهشت";
                    break;
                case "3":
                    monthname = "خرداد";
                    break;
                case "4":
                    monthname = "تیر";
                    break;
                case "5":
                    monthname = "مرداد";
                    break;
                case "6":
                    monthname = "شهریور";
                    break;
                case "7":
                    monthname = "مهر";
                    break;
                case "8":
                    monthname = "آبان";
                    break;
                case "9":
                    monthname = "آذر";
                    break;
                case "10":
                    monthname = "دی";
                    break;
                case "11":
                    monthname = "بهمن";
                    break;
                case "12":
                    monthname = "اسفند";
                    break;
            }
            year = pc.GetYear(pdt);

            lbl_date.Text = dayname + "، " + ToPersianNumber(day) + " " + monthname + " " + ToPersianNumber(year);
            lbl_mdate.Text = mpc.GetDayOfWeek(mdt).ToString().Substring(0, 3) + ". " + mpc.GetDayOfMonth(mdt) + " / " + mmname.GetMonthName(mdt.Month).Substring(0, 3) + "(" + mdt.Month + ") / " + mdt.Year;
            txt_month.Text = ToPersianNumber(day);
        }

        private void frm_main_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_exit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Properties.Settings.Default.matn = txt_matn.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                MessageBox.Show("امکان ذخیره یادداشت ها وجود ندارد", "خطا");
            }
            Application.Current.Shutdown();
        }

        private void btn_pin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ispin == true)
            {
                frm_main.Topmost = false;
                btn_pin.Source = new BitmapImage(new Uri("pics/unpin.png", UriKind.Relative));
            }
            else
            {
                frm_main.Topmost = true;
                btn_pin.Source = new BitmapImage(new Uri("pics/pin.png", UriKind.Relative));
            }
            ispin = !ispin;
        }

        private void lbl_date_Loaded(object sender, RoutedEventArgs e)
        {
            updatetime();
            btn_time.ToolTip = lbl_date.Text;
        }


        private void txt_matn_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txt_matn.Text = Properties.Settings.Default.matn;
            }
            catch (FileNotFoundException)
            {

            }
        }

        private void btn_timer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!istimer) {
                wtForm.Show();
                wtForm.Left = SystemParameters.PrimaryScreenWidth - Width;
                try
                {
                    wtForm.Top = Properties.Settings.Default.ftop + Height;
                }
                catch (FileNotFoundException)
                {
                    wtForm.Top = 400;
                }
            }
            else
            {
                wtForm.Hide();
            }
            istimer = !istimer;
        }
        private void grd_biroon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            qabltop = Top;
        }

        private void grd_biroon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isjam == false)
            {
                if (isbaaz == false)
                {
                    if (Top == qabltop)
                    {
                        isbaaz = !isbaaz;
                        btn_exit.Margin = new Thickness(12, -10, 0, 0);
                        btn_pin.Margin = new Thickness(10, 0, 0, 11);
                        btn_jam.Margin = new Thickness(-20, 0, 0, 11);
                        for (i = 35; i <= 278; i++)
                        {
                            frm_main.Left = SystemParameters.PrimaryScreenWidth - i;
                        }
                        Width = 278;
                    }
                    else
                    {
                        frm_main.Left = SystemParameters.PrimaryScreenWidth - 35;
                    }
                }
                else
                {
                    if (Top == qabltop)
                    {
                        wtForm.Hide();
                        istimer = false;
                        btn_exit.Margin = new Thickness(-20, -10, 0, 0);
                        btn_pin.Margin = new Thickness(-20, 0, 0, 11);
                        btn_jam.Margin = new Thickness(11, 0, 0, 11);

                        isbaaz = !isbaaz;
                        for (i = 278; i >= 35; i--)
                        {
                            frm_main.Left = SystemParameters.PrimaryScreenWidth - i;
                        }
                        Width = 35;
                    }
                    else
                    {
                        frm_main.Left = SystemParameters.PrimaryScreenWidth - Width;
                    }
                }
                try
                {
                    Properties.Settings.Default.ftop = frm_main.Top;
                    Properties.Settings.Default.Save();
                }
                catch (Exception)
                {

                }
                try
                {
                    wtForm.Top = Properties.Settings.Default.ftop + Height;
                }
                catch (FileNotFoundException)
                {
                }
            }

            updatetime();
            btn_time.ToolTip = lbl_date.Text;
        }

        private void frm_main_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.matn = txt_matn.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                MessageBox.Show("امکان ذخیره یادداشت ها وجود ندارد", "خطا");
            }

            updatetime();
            btn_time.ToolTip = lbl_date.Text;
        }

        private void btn_about_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            aForm.Show();
        }

        private void btn_jam_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btn_jam.Margin = new Thickness(-30, 0, 0, -30);
            btn_pin.Margin = new Thickness(-30, 0, 0, -30);
            btn_time.Margin = new Thickness(6, 0, 0, 7);
            txt_month.Margin = new Thickness(7, 0, 0, 7);
            Height = 36;
            wtForm.Top = Properties.Settings.Default.ftop + Height;
            isjam = true;
        }

        private void btn_time_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btn_jam.Margin = new Thickness(11, 0, 0, 11);
            btn_pin.Margin = new Thickness(-30, 0, 0, -30);
            btn_time.Margin = new Thickness(-30, 0, 0, -30);
            txt_month.Margin = new Thickness(-30, 0, 0, -30);
            Height = 350;
            isjam = false;
            updatetime();
            btn_time.ToolTip = lbl_date.Text;
        }

        private void txt_matn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            updatetime();
            btn_time.ToolTip = lbl_date.Text;
        }

        private void grd_biroon_MouseMove(object sender, MouseEventArgs e)
        {
            if (isjam == false)
            {
                try
                {
                    this.DragMove();
                }
                catch (Exception)
                {

                }
            }
        }

        private void frm_main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
