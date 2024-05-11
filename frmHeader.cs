using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;

namespace ISP
{
    public partial class frmHeader : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlCommand cmd;
        IDataReader drList;

        int iTry = 1;
        int iUser_ID = 0;
        string sDBSuffix = "";
        public frmHeader()
        {
            InitializeComponent();
        }

        private void frmHeader_Load(object sender, EventArgs e)
        {
            lblToday.Text = DateTime.Now.ToString();
        }
        private void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) CheckPassword();
        }
        private void btnEntry_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }
        private void CheckPassword()
        {
            //--- if in password exists word "test" it means that user want's to run test version----------
            if (txtPassword.Text.Length > 0)
            {
                if (txtPassword.Text.IndexOf("test") > 0)
                {
                    sDBSuffix = "Test";
                    conn.ConnectionString = conn.ConnectionString + sDBSuffix;                    
                    txtPassword.Text = txtPassword.Text.Replace("test", "");
                }
            }
            else txtPassword.Text = "~!@#$%^&*()_+";
 
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetUserID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@password", txtPassword.Text));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    iUser_ID = Convert.ToInt32(drList["ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }


            if (iUser_ID > 0) {
                this.Hide();

                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo("Core.dll");
                //MessageBox.Show("File: " + myFileVersionInfo.FileDescription + '\n' +
                //      "Version number: " + myFileVersionInfo.FileVersion);


                Assembly xAssembly = Assembly.LoadFrom("Core.dll");
                Type type = xAssembly.GetType("Core.clsStart");
                object instance = Activator.CreateInstance(type, iUser_ID + ";" + sDBSuffix);
            }
            else
            {
                switch (iTry)
                {
                    case 1:
                        txtPassword.BackColor = Color.Yellow;
                        break;
                    case 2:
                        txtPassword.BackColor = Color.LightCoral;
                        break;
                    case 3:
                        this.Close();
                        Application.Exit();
                        break;
                }
                iTry = iTry + 1;
                txtPassword.Text = "********";
                txtPassword.SelectionStart = 0;
                txtPassword.SelectionLength = txtPassword.Text.Length;
                txtPassword.Focus();
            }
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
