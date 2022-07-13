using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;

namespace WindowToggler2000
{
    //Mish mash of these:
    //https://www.thomasclaudiushuber.com/2015/08/22/creating-a-background-application-with-wpf/
    //https://www.youtube.com/watch?v=8NuoBe1HS0U
    //nuget needed for windows.forms in .net 6: https://github.com/hardcodet/wpf-notifyicon/blob/develop/README.md
    public partial class App : Application
    {
        private readonly Forms.NotifyIcon _notifyIcon;
        private bool _isExit;
        public App() {_notifyIcon = new Forms.NotifyIcon();}


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Show();
            MainWindow.Hide();

            MainWindow.Closing += MainWindowClosing;


            _notifyIcon.Icon = new System.Drawing.Icon("Resources/Icon.ico");
            _notifyIcon.Text = "Sanck is running";
            _notifyIcon.DoubleClick += (s, args) => NotifyIcon_Click();


            _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }
        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
        private void NotifyIcon_Click()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else MainWindow.Show();

        }

        private void MainWindowClosing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide();
            }
        }
    }
}
