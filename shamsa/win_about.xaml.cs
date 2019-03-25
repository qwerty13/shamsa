using System;
using System.Collections.Generic;
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
using System.IO;
using System.Reflection;

namespace shamsa
{
    /// <summary>
    /// Interaction logic for win_about.xaml
    /// </summary>
    public partial class win_about : Window
    {
        bool isstartup = true;
        bool isstartsmall = false;
        //bool isf = true;

        public win_about()
        {
            InitializeComponent();
            Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
            Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
        }

        private void btn_exit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }

        private void chk_isstartup_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                isstartup = Properties.Settings.Default.sup;
            }
            catch (FileNotFoundException)
            {
                try
                {
                    Properties.Settings.Default.sup = isstartup;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.sup = isstartup;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            chk_isstartup.IsChecked = isstartup;
            //isf = false;
        }

        private void chk_isstartup_Click(object sender, RoutedEventArgs e)
        {
            if (chk_isstartup.IsChecked == true)
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
                isstartup = true;
                try
                {
                    Properties.Settings.Default.sup = isstartup;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.sup = isstartup;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    Assembly curAssembly = Assembly.GetExecutingAssembly();
                    key.DeleteValue(curAssembly.GetName().Name);
                }
                catch
                {
                }
                isstartup = false;
                try
                {
                    Properties.Settings.Default.sup = isstartup;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.sup = isstartup;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void chk_isstartsmall_Click(object sender, RoutedEventArgs e)
        {
            if (chk_isstartsmall.IsChecked == true)
            {
                isstartsmall = true;
                try
                {
                    Properties.Settings.Default.isstartsmall = isstartsmall;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.isstartsmall = isstartsmall;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                isstartsmall = false;
                try
                {
                    Properties.Settings.Default.isstartsmall = isstartsmall;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.isstartsmall = isstartsmall;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void chk_isstartsmall_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                isstartsmall = Properties.Settings.Default.isstartsmall;
            }
            catch (FileNotFoundException)
            {
                try
                {
                    Properties.Settings.Default.isstartsmall = isstartup;
                    Properties.Settings.Default.Save();
                }
                catch (FileNotFoundException)
                {
                    try
                    {
                        Properties.Settings.Default.isstartsmall = isstartsmall;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            chk_isstartsmall.IsChecked = isstartsmall;
        }
    }
}
