using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Core;

namespace ISPServer
{
    public class SystemTray : ApplicationContext
    {
        /// <summary>
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ContextMenu m_contextMenu;
        private System.Windows.Forms.NotifyIcon m_trayIcon;

        private Icon m_Icon1;
        private Icon m_Icon2;
        private bool m_bTrayFlag;
        private bool m_bIconFlag;
        private int iMaxID = 0;
        private DateTime dNow, dLastTime, dToday;
        private bool b1min, bCheckDMSFolders;

        public SystemTray()
        {
            //
            // Required for Windows Form Designer support
            //
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SystemTray));
            this.components = new System.ComponentModel.Container();
            this.m_trayIcon = new System.Windows.Forms.NotifyIcon();
            this.m_contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            dToday = DateTime.Now;

            m_trayIcon.Text = "ISP Server";
            m_trayIcon.Visible = true;
            m_trayIcon.Icon = new Icon(@"images\database.ico");
            m_trayIcon.ContextMenu = this.m_contextMenu;
            m_trayIcon.DoubleClick += new System.EventHandler(this.OnDBClkTrayIcon);
            //@m_contextMenu.SetLocation (new System.Drawing.Point (102, 7));
            m_contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4] { this.menuItem1, this.menuItem4, this.menuItem2, this.menuItem3 });
            menuItem4.Text = "Stop";
            menuItem4.Index = 1;
            menuItem4.Click += new System.EventHandler(this.OnClickStop);
            menuItem2.Text = "View";
            menuItem2.Index = 2;
            menuItem2.Click += new System.EventHandler(this.OnClickAbout);
            menuItem1.Text = "&Start";
            menuItem1.Index = 0;
            menuItem1.Click += new System.EventHandler(this.OnClickStart);
            //@m_timer.SetLocation (new System.Drawing.Point (7, 34));
            m_timer.Interval = 300;
            m_timer.Tick += new System.EventHandler(this.AnimateIcon);
            menuItem3.Text = "Exit";
            menuItem3.Index = 3;
            menuItem3.Click += new System.EventHandler(this.OnClickExit);


            m_bTrayFlag = false;
            m_bIconFlag = true;

            menuItem4.Enabled = false;

            m_timer.Start();
            menuItem4.Enabled = true;
            menuItem1.Enabled = true;

            try
            {
                m_Icon1 = new Icon(@"images\database.ico");
                m_Icon2 = new Icon(@"images\Database-Create.ico");

            }
            catch (Exception e)
            {
                MessageBox.Show("Error " + e.Message, "Tray - Error", MessageBoxButtons.AbortRetryIgnore);
                menuItem4.Enabled = false;
                menuItem1.Enabled = false;
            }
            Tick();

        //    TimerCallback callback = new TimerCallback(Tick);

            //Console.WriteLine("Creating timer: {0}\n", DateTime.Now.ToString("h:mm:ss"));

            // create a 5 seconds timer tick
        //    System.Threading.Timer stateTimer = new System.Threading.Timer(callback, null, 0, 5000);
        }

        public void Tick()
        {

            if (m_Icon1 != null && m_Icon2 != null)
            {
                if (m_bIconFlag == true)
                {
                    m_trayIcon.Icon = m_Icon2;
                    m_bIconFlag = false;
                }
                else
                {
                    m_trayIcon.Icon = m_Icon1;
                    m_bIconFlag = true;
                }
            }
        }

        /// <summary>
        ///    Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
            m_trayIcon.Dispose();
        }

        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        protected void OnDBClkTrayIcon(object sender, System.EventArgs e)
        {
            if (m_bTrayFlag == true)
            {
                //this.Activate();
                //this.Show();
                //this.Refresh();
                m_bTrayFlag = false;
            }
        }

        protected void OnClickExit(object sender, System.EventArgs e)
        {
            m_trayIcon.Dispose();
            Application.Exit();
        }

        protected void OnClickAbout(object sender, System.EventArgs e)
        {
            frmMain M = new frmMain();
            M.ShowDialog();
        }

        protected void OnClickStop(object sender, System.EventArgs e)
        {
            m_timer.Stop();
            menuItem4.Enabled = false;
            menuItem1.Enabled = true;
        }

        protected void OnClickStart(object sender, System.EventArgs e)
        {
            m_timer.Start();
            menuItem4.Enabled = true;
            menuItem1.Enabled = false;
        }

        protected void OnSystemTrayResize(object sender, System.EventArgs e)
        {
            if (m_bTrayFlag == false)
            {
                //this.Hide();
                m_bTrayFlag = true;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*	Timer handling function */
        public void AnimateIcon(object sender, System.EventArgs e)
        {

            b1min = false;
            bCheckDMSFolders = false;

            if (m_Icon1 != null && m_Icon2 != null)
            {
                if (m_bIconFlag == true)
                {
                    m_trayIcon.Icon = m_Icon2;
                    m_bIconFlag = false;
                }
                else
                {
                    m_trayIcon.Icon = m_Icon1;
                    m_bIconFlag = true;
                }
            }

            dNow = DateTime.Now;
            if (System.Math.Abs((dNow - dLastTime).TotalSeconds) > 60)
            {
                dLastTime = dNow;
                b1min = true;
            }

            //dToday = dToday.AddDays(-1);
            if (dToday.Date < DateTime.Now.Date)
            {
                bCheckDMSFolders = true;
                dToday = DateTime.Now;
            }

            StartUp S = new StartUp();
            S.MaxID = iMaxID;
            S.MainPoint(b1min, bCheckDMSFolders);
            iMaxID = S.MaxID;
        }       
    }
}
