namespace ISPDBO.Models
{
    public class Global
    {
        public static void StartInit(string cs, string df, string tf, string ftp_host, string ftp_username, string ftp_password)
        {
            ConnectionString = cs;
            DMSFolder = df;
            TransferFolder = tf;
            FTP_Host = ftp_host;
            FTP_Username = ftp_username;
            FTP_Password = ftp_password;
            WebUserDevice_ID = 0;
            WebUser_ID = 0;            
            Client_ID = 0;
        }
        public static string ConnectionString { get; set; }
        public static string DMSFolder { get; set; }
        public static string TransferFolder { get; set; }
        public static string FTP_Host { get; set; }
        public static string FTP_Username { get; set; }
        public static string FTP_Password { get; set; }
        public static int WebUserDevice_ID { get; set; }
        public static int WebUser_ID { get; set; }
        public static int Client_ID { get; set; }
    }
}
