using System;
using System.Windows.Forms;
using System.Net;

namespace Core
{
    public partial class frmSMS : Form
    {
        int i, iAktion;
        string sSMS_Username, sSMS_Password, sSMS_From;
        public frmSMS()
        {
            InitializeComponent();
        }

        private void frmSMS_Load(object sender, EventArgs e)
        {
            iAktion = 0;

            txtMobile.Text = txtMobile.Text.Replace("-", "");
            lblChars.Text = (160 - txtMessage.Text.Length).ToString();

            DefineButtonStatus();
        }
        public int Aktion { get { return iAktion; } set { iAktion = value; } }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            DefineButtonStatus();
        }
        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            i = txtMessage.Text.Length;
            lblChars.Text = (160 - i).ToString();
            DefineButtonStatus();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            string sTemp = txtMessage.Text;
            string URL = "http://services.yuboto.com/sms/api/smsc.asp?user=" + sSMS_Username + "&pass=" + sSMS_Password +
                         "&action=send&from=" + sSMS_From + "&to=" + txtMobile.Text + "&text=" + sTemp.Replace("&", "-");

            request = (HttpWebRequest)WebRequest.Create(URL);
            response = (HttpWebResponse)request.GetResponse();

            iAktion = 1;
            this.Close();
        }
        private void DefineButtonStatus()
        {
            if (txtMobile.Text.Trim().Length == 0 || txtMessage.Text.Trim().Length == 0) btnSend.Enabled = false;
            else btnSend.Enabled = true;        
        }
        public string SMS_Username { get { return sSMS_Username; } set { sSMS_Username = value; } }
        public string SMS_Password { get { return sSMS_Password; } set { sSMS_Password = value; } }
        public string SMS_From { get { return sSMS_From; } set { sSMS_From = value; } }
    }
}
