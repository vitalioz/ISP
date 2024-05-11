using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using CrystalDecisions.CrystalReports.Engine;
using Core;

namespace Transactions
{
    public partial class frmClientInforming : Form
    {
        int i, iBusiness, iProvider_ID, iOldID, iInformMethod;
        DateTime dAktionDate;
        string sTemp, sCode, sInformMethod, sSMS_Disabled, sUsername, sPassword, sFrom;
        CellRange rng;
        public frmClientInforming()
        {
            InitializeComponent();
        }
        private void frmClientInforming_Load(object sender, EventArgs e)
        {
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("full_name");

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = Global.GetLabel("portfolio");

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("transaction");

            rng = fgList.GetCellRange(0, 5, 0, 8);
            rng.Data = Global.GetLabel("product");

            fgList[1, 5] = Global.GetLabel("type");
            fgList[1, 6] = Global.GetLabel("code");
            fgList[1, 7] = Global.GetLabel("isin");
            fgList[1, 8] = Global.GetLabel("title");

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("quantity");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("price");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("currency");

            fgList.Cols[12].AllowMerging = true;
            rng = fgList.GetCellRange(0, 12, 1, 12);
            rng.Data = Global.GetLabel("execution_date");

            fgList.Cols[13].AllowMerging = true;
            rng = fgList.GetCellRange(0, 13, 1, 13);
            rng.Data = Global.GetLabel("informing_ways");

            fgList.Cols[14].AllowMerging = true;
            rng = fgList.GetCellRange(0, 14, 1, 14);
            rng.Data = Global.GetLabel("update_date");

            fgList.Cols[15].AllowMerging = true;
            rng = fgList.GetCellRange(0, 15, 1, 15);
            rng.Data = Global.GetLabel("service");

            fgList.Cols[16].AllowMerging = true;
            rng = fgList.GetCellRange(0, 16, 1, 16);
            rng.Data = Global.GetLabel("advisor");

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            //-------------- Define Information Methods List ------------------
            cmbInformMethods.DataSource = Global.dtInformMethods.Copy();
            cmbInformMethods.DisplayMember = "Title";
            cmbInformMethods.ValueMember = "ID";
            cmbInformMethods.SelectedValue = 4;                                        // 4 - by default SMS

            clsOptions Options = new clsOptions();
            Options.GetRecord();
            sUsername = Options.SMS_Username;
            sPassword = Options.SMS_Password;
            sFrom = Options.SMS_From;

            DefineList();
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= (fgList.Rows.Count - 1); i++) fgList[i, 0] = chkList.Checked;
        }
        private void DefineList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 2;

            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            klsOrder.CommandType_ID = 1;
            klsOrder.DateFrom = dAktionDate;
            klsOrder.DateTo = dAktionDate;
            klsOrder.ServiceProvider_ID = iProvider_ID;
            klsOrder.Sent = 0;
            klsOrder.Actions = 0;
            klsOrder.SendCheck = 0;
            klsOrder.User_ID = 0;
            klsOrder.User1_ID = 0;
            klsOrder.User4_ID = 0;
            klsOrder.Division_ID = 0;
            klsOrder.Code = sCode;
            klsOrder.Product_ID = 0;
            klsOrder.Share_ID = 0;
            klsOrder.Currency = "";
            klsOrder.ShowCancelled = 0;
            klsOrder.GetList();
            foreach (DataRow dtRow in klsOrder.List.Rows)
            {
                if (iOldID != Convert.ToInt32(dtRow["ID"])) {
                    iOldID = Convert.ToInt32(dtRow["ID"]);

                    fgList.AddItem(false + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                   (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "\t" +
                                   dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + dtRow["Share_Title"] + "\t" +
                                   (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                   (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" + dtRow["Currency"] + "\t" +
                                   ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                   "" + "\t" + "" + "\t" + dtRow["ServiceTitle"] + "\t" + dtRow["Advisor_Fullname"] + "\t" + dtRow["ID"] + "\t" + 
                                   dtRow["Address"] + " " + dtRow["City"] + " " + dtRow["Zip"] + " " + dtRow["Country_Title"] + "\t" + dtRow["EMail"] + "\t" + dtRow["Mobile"] + "\t" + 
                                   dtRow["Client_ID"] + "\t" + dtRow["Service_ID"] + "\t" +  dtRow["SendSMS"]);
                }
            }

            fgList.Redraw = true;
        }
        public int Business { get { return iBusiness; } set { iBusiness = value; } }                                    //1 - Securuties, 2 - FX, 3 - LL

        private void btnInform_Click(object sender, EventArgs e)
        {
            switch (cmbInformMethods.Text) {
                case "SMS":                      // 4 - SMS
                    SendSMS_Securities();
                    break;
                case "E-mail":                   // 5 - e-mail
                    SendEMail_Securities();
                    break;
                case "Ταχ/κη αποστολή":          // 8 - Ταχ/κη αποστολή
                    SendPost_Securities();
                    break;
                default:
                    for (i = 2; i <= (fgList.Rows.Count - 1); i++) {
                        if (Convert.ToBoolean(fgList[i, "Check"])) {
                            Global.AddInformingRecord(iBusiness, Convert.ToInt32(fgList[i, "ID"]), Convert.ToInt32(cmbInformMethods.SelectedValue), 5, Convert.ToInt32(fgList[i, "Client_ID"]), 0, "", "",
                                                      Global.GetLabel("update_execution_command"), "", "", "", DateTime.Now.ToString("dd/MM/yyyy"), 1, 1, "");
                            fgList[i, "InformMethod"] = cmbInformMethods.Text;
                            fgList[i, "InformDate"] = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                    }
                    break;
            }

            for (i = 2; i <= (fgList.Rows.Count - 1); i++) fgList[i, 0] = false;
        }
        private void SendSMS_Securities()
        {
            sTemp = "";
            for (i = 2; i <= (fgList.Rows.Count - 1); i++) {
                if (Convert.ToBoolean(fgList[i, 0]) && Convert.ToInt32(fgList[i, "SMS_Enable"]) == 1) {

                    if ((fgList[i, "Mobile"] + "") != "") {
                        sTemp = "ΠΡΑΞΗ: " + ((fgList[i, "ExecDate"] + "") != "" ? ((fgList[i, "Aktion"] + "") == "SELL" ? "ΠΩΛΗΣΗ" : "ΑΓΟΡΑ") : "ΔΕΝ ΕΚΤΕΛΕΣΤΙΚΕ") + "\n";
                        sTemp = sTemp + "ΤΥΠΟΣ: " + fgList[i, "Product_Type"] + "\n";
                        sTemp = sTemp + "ΤΊΤΛΟΣ: " + fgList[i, "Share_Title"] + "\n";
                        sTemp = sTemp + "ΙSIN: " + fgList[i, "Share_ISIN"] + "\n";
                        sTemp = sTemp + "ΤΙΜΗ: " + fgList[i, "Price"] + " " + fgList[i, "Currency"]  + "\n";
                        sTemp = sTemp + "ΠΟΣΟΤΗΤΑ: " + fgList[i, "Quantity"];

                        frmSMS locSMS = new frmSMS();
                        locSMS.txtMobile.Text = fgList[i, "Mobile"] + "";
                        locSMS.txtMessage.Text = sTemp;
                        locSMS.SMS_Username = sUsername;
                        locSMS.SMS_Password = sPassword;
                        locSMS.SMS_From = sFrom;
                        locSMS.ShowDialog();

                        if (locSMS.Aktion == 1) {
                            iInformMethod = Convert.ToInt32(cmbInformMethods.SelectedValue);
                            sInformMethod = cmbInformMethods.Text;
                            Global.AddInformingRecord(1, Convert.ToInt32(fgList[i, "ID"]), 4, 5, Convert.ToInt32(fgList[i, "Client_ID"]), 0, locSMS.txtMobile.Text, "", 
                                                      Global.GetLabel("update_execution_command"), locSMS.txtMessage.Text, "", "", DateTime.Now.ToString("dd/MM/yyyy"), 1, 1, "");      // 4 - SMS
                        }
                        else {
                            iInformMethod = -1;
                            sInformMethod = "-";
                        }
                    }
                    else {
                        if (Convert.ToInt32(fgList[i, "SendSMS"]) == 0) {
                            iInformMethod = -1;
                            sInformMethod = "-";
                            sSMS_Disabled = sSMS_Disabled + fgList[i, 1] + "\n";
                        }
                    }

                    fgList[i, "InformMethod"] = sInformMethod;
                    fgList[i, "InformDate"] = DateTime.Now.ToString("dd/MM/yyyy");

                    clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
                    klsOrderSecurity.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    klsOrderSecurity.GetRecord();
                    klsOrderSecurity.InformationMethod_ID = iInformMethod;
                    klsOrderSecurity.EditRecord();
                }
            }
        }
        private void SendEMail_Securities()
        {
            sTemp = "";
            for (i = 2; i <= (fgList.Rows.Count - 1); i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]) && (fgList[i, "Email"]+"") != "")  {

                    sTemp = "ΠΕΛΑΤΗΣ: " + fgList[i, "ClientName"] + "<br />";
                    sTemp = sTemp + "ΗΜΕΡ.ΕΚΤΕΛΕΣΗΣ: " + fgList[i, "ExecDate"] + "<br />";
                    sTemp = sTemp + "ΠΡΑΞΗ: " + ((fgList[i, "ExecDate"] + "") != "" ? ((fgList[i, "Aktion"] + "") == "SELL" ? "ΠΩΛΗΣΗ" : "ΑΓΟΡΑ") : "ΔΕΝ ΕΚΤΕΛΕΣΤΙΚΕ") + "<br />";
                    sTemp = sTemp + "ΤΥΠΟΣ: " + fgList[i, "Product_Type"] + "<br />";
                    sTemp = sTemp + "ΤΊΤΛΟΣ: " + fgList[i, "Share_Title"] + "<br />";
                    sTemp = sTemp + "ΙSIN: " + fgList[i, "Share_ISIN"] + "<br />";
                    sTemp = sTemp + "ΤΙΜΗ: " + fgList[i, "Price"] + " " + fgList[i, "Currency"] + "<br />";
                    sTemp = sTemp + "ΠΟΣΟΤΗΤΑ: " + fgList[i, "Quantity"];

                    clsServerJobs ServerJobs = new clsServerJobs();
                    ServerJobs.JobType_ID = 41;
                    ServerJobs.Source_ID = 0;
                    ServerJobs.Parameters = "{'email': '" + fgList[i, "Email"] + "', 'subject': '" + Global.GetLabel("update_execution_command") + "', 'body': '" + sTemp + "'}";
                    ServerJobs.DateStart = DateTime.Now;
                    ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                    ServerJobs.PubKey = "";
                    ServerJobs.PrvKey = "";
                    ServerJobs.Attempt = 0;
                    ServerJobs.Status = 0;
                    ServerJobs.InsertRecord();

                    iInformMethod = Convert.ToInt32(cmbInformMethods.SelectedValue);
                    sInformMethod = cmbInformMethods.Text;
                    Global.AddInformingRecord(1, Convert.ToInt32(fgList[i, "ID"]), 5, 5, Convert.ToInt32(fgList[i, "Client_ID"]), 0, fgList[i, "Email"]+"", "",
                                                    Global.GetLabel("update_execution_command"), sTemp, "", "", DateTime.Now.ToString("dd/MM/yyyy"), 1, 1, "");       // 5 - email

                    fgList[i, "InformMethod"] = sInformMethod;
                    fgList[i, "InformDate"] = DateTime.Now.ToString("dd/MM/yyyy");

                    clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
                    klsOrderSecurity.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    klsOrderSecurity.GetRecord();
                    klsOrderSecurity.InformationMethod_ID = iInformMethod;
                    klsOrderSecurity.EditRecord();
                }
                else {
                    if ((fgList[i, "Email"] + "") != "") {
                        iInformMethod = -1;
                        sInformMethod = "-";
                    }
                }
            }
        }
        private void SendPost_Securities()
        {
            ReportDocument rptCommandsInform = new ReportDocument();
            rptCommandsInform.Load(Application.StartupPath + @"\Reports\repCommandsInform.rpt");

            for (i = 2; i <= fgList.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgList[i, 0])) {

                    sTemp = "Πελάτης: " + fgList[i, "ClientName"] + "\n";
                    sTemp = sTemp + "Ημερομηνια Εκτελεσης: " + fgList[i, "ExecDate"] + "\n";
                    sTemp = sTemp + "Κωδικός: " + fgList[i, "Code"] + "\n";
                    sTemp = sTemp + ((fgList[i, "ExecDate"] +"") != "" ? "Πράξη: " +((fgList[i, "Aktion"] +"") == "SELL"? "ΠΩΛΗΣΗ" : "ΑΓΟΡΑ"): "ΔΕΝ ΕΚΤΕΛΕΣΤΙΚΕ") + "\n";
                    sTemp = sTemp + "Τύπος: " + fgList[i, "Product_Type"] + "\n";
                    sTemp = sTemp + "Τίτλος: " + fgList[i, "Share_Title"] + "\n";
                    sTemp = sTemp + "ΙSIN: " + fgList[i, "Share_ISIN"] + "\n";
                    sTemp = sTemp + "Τιμή: " + fgList[i, "Price"] + " " + fgList[i, "Currency"] + "\n";
                    sTemp = sTemp + "Ποσότητα: " + fgList[i, "Quantity"];

                    rptCommandsInform.SetParameterValue(0, "Πελάτης: " + fgList[i, "ClientName"]);                                  // ClientName
                    rptCommandsInform.SetParameterValue(1, "Ημερομηνια Εκτελεσης: " + fgList[i, "ExecDate"]);                       // Execute Date
                    rptCommandsInform.SetParameterValue(2, "Κωδικός: " + fgList[i, "Code"]);                                        // Code
                    rptCommandsInform.SetParameterValue(3, ((fgList[i, 8]+"") != "" ? "Πράξη: " + ((fgList[i, 3]+"") == "SELL" ? "ΠΩΛΗΣΗ" : "ΑΓΟΡΑ") : "ΔΕΝ ΕΚΤΕΛΕΣΤΙΚΕ"));
                    rptCommandsInform.SetParameterValue(4, "Τύπος: " + fgList[i, "Product_Type"]);
                    rptCommandsInform.SetParameterValue(5, "Τίτλος: " + fgList[i, "Share_Title"]);
                    rptCommandsInform.SetParameterValue(6, "ISIN: " + fgList[i, "Share_ISIN"]);
                    rptCommandsInform.SetParameterValue(7, "Τιμή: " + fgList[i, "Price"] + " " + fgList[i, "Currency"]);
                    rptCommandsInform.SetParameterValue(8, "Ποσότητα: " + fgList[i, "Quantity"]);

                    rptCommandsInform.PrintToPrinter(1, true, 1, 999);
                    // crwReport.Visible = true;
                    // crwReport.ReportSource = rptCommandsInform;

                    fgList[i, 12] = cmbInformMethods.Text;
                    fgList[i, 13] = DateTime.Now.ToString("dd/MM/yyyy");

                    clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
                    klsOrderSecurity.CommandType_ID = 1;
                    klsOrderSecurity.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    klsOrderSecurity.GetRecord();
                    //'klsOrderSecurity.OfficialInformingDate = "";
                    klsOrderSecurity.InformationMethod_ID = Convert.ToInt32(cmbInformMethods.SelectedValue);
                    klsOrderSecurity.EditRecord();

                    Global.AddInformingRecord(1, Convert.ToInt32(fgList[i, "ID"]), 8, 5, Convert.ToInt32(fgList[i, "Client_ID"]), 0, fgList[i, "Email"]+"", "", 
                               Global.GetLabel("update_execution_command"), sTemp, "", "", DateTime.Now.ToString("dd/MM/yyyy"), 1, 1, "");        // 8 - Taxidromeio
                }
            }
        }
        public DateTime AktionDate { get { return dAktionDate; } set { dAktionDate = value; } }
        public int Provider_ID { get { return iProvider_ID; } set { iProvider_ID = value; } }
        //public int Advisor_ID { get { return iAdvisor_ID; } set { iAdvisor_ID = value; } }
        //public int Aktion { get { return iAktion; } set { iAktion = value; } }
        public string Code { get { return sCode; } set { sCode = value; } }
    }
}
