using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace shamsa
{
    public partial class win_timer : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public win_timer()
        {
            InitializeComponent();
            grd_alarmshow.Margin = new Thickness(0, 0, 278, 0);
        }

        public void chk_timer_Checked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, Convert.ToInt32(sldr_time.Value), 0);
            dispatcherTimer.Start();
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Show();
            grd_alarmshow.Margin = new Thickness(0,0,0,0);
            chk_timer.IsChecked = false;
            dispatcherTimer.Stop();
        }

        private void sldr_time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (chk_timer != null) {
                chk_timer.IsChecked = false;
                dispatcherTimer.Stop();
            }
        }

        private void grd_alarmshow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grd_alarmshow.Margin = new Thickness(0, 0, 278, 0);
            this.Hide();
        }

        private void txt_alarmtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (lbl_showalarmtxt != null)
                    lbl_showalarmtxt.Text = txt_alarmtxt.Text;
            }
            catch (Exception)
            {

            }
        }

        private void chk_timer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chk_timer != null)
            {
                chk_timer.IsChecked = false;
                dispatcherTimer.Stop();
            }
        }
    }
}
