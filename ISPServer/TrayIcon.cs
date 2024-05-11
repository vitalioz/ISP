using System;
using System.Windows.Forms;
using System.Drawing;

namespace ISPServer
{
    public class TrayIcon : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        //frmMain configWindow = new frmMain();

        public TrayIcon()
        {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = new Icon(@"C:\Apps\ISP\ISPServer\bin\Debug\email.ico"); // ISPServer.Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(ShowForm);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
        }

        void ShowForm(object sender, EventArgs e)
        {
            frmMain Main = new frmMain();
            Main.WindowState = FormWindowState.Maximized;
            Main.Show();
        }

        void ShowConfig(object sender, EventArgs e)
        {
            /*
            // If we are already showing the window meerly focus it.
            if (configWindow.Visible)
                configWindow.Focus();
            else
                configWindow.ShowDialog();
            */
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;

            Application.Exit();
        }
    }
}
