using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucOfficialInforming_Commands : UserControl
    {
        DataColumn dtCol;
        DataRow dtRow;
        DataTable dtInform;
        int i, iClient_ID;
        string sTemp, sClientName, sRecipientName, sConnectionMethod, sConnectionData, sDate, sStatement_FileName, sOldCode, sBody;
        bool bCheckList;
        Global.ContractData stContractData;
        public ucOfficialInforming_Commands()
        {
            InitializeComponent();
            EmptyContractData();
        }

        private void ucOfficialInforming_Commands_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            panEditCommandData.Visible = false;
            panEditCommandData.Top = (this.Height - panEditCommandData.Height) / 2;
            panEditCommandData.Left = (this.Width - panEditCommandData.Width) / 2;

            ucCS.StartInit(700, 400, 570, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 And Contract_ID > 0";
            ucCS.ListType = 1;
            bCheckList = true;

            cmbProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedItem = 1;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.ShowCellLabels = true;
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            dFrom.Value = DateTime.Now.AddDays(-7);
            dTo.Value = DateTime.Now;
            sStatement_FileName = "";
        }
        protected override void OnResize(EventArgs e)
        {
            btnSearch.Left = this.Width - 110;
            fgList.Width = this.Width - 20;
            fgList.Height = this.Height - 116;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }

        private void picEmptyClient_Click(object sender, EventArgs e)
        {
            iClient_ID = 0;
            EmptyContractData();
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblProfitCenter.Text = "";
            lblCode.Text = "";
            lnkPelatis.Text = "";
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditCommandsInformingData();
        }
        private void tsbEditProposal_Click(object sender, EventArgs e)
        {
            EditCommandsInformingData();
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {
            int iRec_ID = 0;
            clsOrdersSecurity klsOrders = new clsOrdersSecurity();
            sOldCode = "~~~";

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {

                    sTemp = fgList[i, "ContractTitle"] + "";
                    sTemp = sTemp.Replace(".", "_");

                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 1)                                     // 1 - e-mail
                    {
                        sBody = DailyEmailBody(fgList[i, "ProviderTitle"] + "");

                        if (sOldCode != (fgList[i, "Code"] + ""))                     // if it's a new code write into Informings table record that will be send
                        {
                            sOldCode = fgList[i, "Code"] + "";
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 2, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "backoffice@hellasfin.gr", fgList[i, "Thema"] + "", sBody, fgList[i, "StatementFile"] + "", "", "", 0, 0, "");              // 5 - e-mail 

                            clsServerJobs ServerJob = new clsServerJobs();
                            ServerJob.JobType_ID = 43;                                           // 43  - send e-mail from Informings table
                            ServerJob.Source_ID = 0;
                            ServerJob.Parameters = "{'informing_id': '" + iRec_ID + "'}";
                            ServerJob.DateStart = DateTime.Now;
                            ServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                            ServerJob.PubKey = "";
                            ServerJob.PrvKey = "";
                            ServerJob.Attempt = 0;
                            ServerJob.Status = 0;
                            ServerJob.InsertRecord();
                        }
                        else                                             // if it's an old code write into Informings table record that will not be send - last 3 parameters say that this record was sent
                        {
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 2, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "", fgList[i, "Thema"] + "", sBody, fgList[i, "StatementFile"] + "", "", DateTime.Now.ToString(), 1, 1, "");
                        }

                        klsOrders.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsOrders.GetRecord();
                        klsOrders.OfficialInformingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        klsOrders.EditRecord();
                    }
                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 2)                  // 2 - post
                    {
                        dtInform = new DataTable("OfficialInforming");
                        dtCol = dtInform.Columns.Add("f1", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f2", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f3", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f4", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f5", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f6", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));

                        dtRow = dtInform.NewRow();
                        sTemp = fgList[i, "ClientData"] + "";
                        dtRow["f1"] = sTemp.Replace("\t", "\n");
                        dtRow["f2"] = "";
                        dtRow["f3"] = "";
                        dtRow["f4"] = "ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy");
                        dtRow["f5"] = "ΘΕΜΑ: " + fgList[i, "Thema"];
                        dtRow["f6"] = "Στο πλαίσιο ενημέρωσής σας, σας επισυνάπτουμε βεβαίωση εκτέλεσης συναλλαγής της εκτελούσας επιχείρησης " + fgList[i, "ProviderTitle"] + "\n" + "\n" +
                                      "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση.";
                        dtInform.Rows.Add(dtRow);

                        frmReports locReports = new frmReports();
                        locReports.ReportID = 19;
                        locReports.Params = sTemp;
                        locReports.ShowResult = dtInform;
                        locReports.Text = "Επίσημη Ενημέρωση Πελατών";
                        locReports.Show();

                        sTemp = fgList[i, "ContractTitle"] + "";
                        // DMS_PrintFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgCommands(i, "StatementFile"))


                        Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 8, 2, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "", "",
                                                  fgList[i, "Thema"] + "", "", fgList[i, "StatementFile"] + "", "", DateTime.Now.ToString(), 1, 1, "");                        // 8 - post 

                        klsOrders.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsOrders.GetRecord();
                        klsOrders.OfficialInformingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        klsOrders.EditRecord();

                        fgList[i, "InformDate"] = DateTime.Now.ToString("dd/MM/yy");
                    }
                    fgList[i, 0] = false;
                }
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {

            dtInform = new DataTable("OfficialInformingList");
            dtCol = dtInform.Columns.Add("f1", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f2", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f3", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f4", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f5", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f6", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f8", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f9", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f10", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f11", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f12", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f13", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f14", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f15", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f16", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f17", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f18", System.Type.GetType("System.String"));

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                dtRow = dtInform.NewRow();
                dtRow["f1"] = fgList[i, "AA"];
                dtRow["f2"] = fgList[i, "ContractTitle"];
                dtRow["f3"] = fgList[i, "ProviderTitle"];
                dtRow["f4"] = fgList[i, "Code"];
                dtRow["f5"] = fgList[i, "Portfolio"];
                dtRow["f6"] = fgList[i, "Aktion"];
                dtRow["f7"] = fgList[i, "Product_Type"];
                dtRow["f8"] = fgList[i, "Product_Title"];
                dtRow["f9"] = fgList[i, "Share_Code"];
                dtRow["f10"] = fgList[i, "ISIN"];
                dtRow["f11"] = fgList[i, "Price"];
                dtRow["f12"] = fgList[i, "Quantity"];
                dtRow["f13"] = fgList[i, "Amount"];
                dtRow["f14"] = fgList[i, "Currency"];
                dtRow["f15"] = fgList[i, "ExecuteDate"];
                dtRow["f16"] = fgList[i, "InformDate"];
                dtRow["f17"] = fgList[i, "InformMethod"];
                dtRow["f18"] = fgList[i, "Thema"];
                dtInform.Rows.Add(dtRow);
            }

            frmReports locReports = new frmReports();
            locReports.Params = Convert.ToDateTime(dFrom.Value).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(dTo.Value).ToString("dd/MM/yyyy") + "~" +
                            cmbProviders.Text + "~" + ucCS.txtContractTitle.Text + "~" + Global.UserName + "~" + Global.CompanyName + "~";

            locReports.ReportID = 20;
            locReports.ShowResult = dtInform;
            locReports.Text = "Επίσημη Ενημέρωση Πελατών";
            locReports.Show();
        }
        private void PrintWord()
        {
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();
            object oMissing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Table oTable1;
            Microsoft.Office.Interop.Word.Paragraph oHeader1Paragraf, oTable1Paragraf;

            string sOfficialInformingCommandsTemplate = "OfficialInformingCommandsTemplate.docx";
            string sTempFile = Application.StartupPath + "\\Temp\\OIC_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";
            string sTargetFile = Application.StartupPath + "\\Temp\\OIC_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

            File.Copy(Application.StartupPath + "\\Templates\\" + sOfficialInformingCommandsTemplate, sTempFile);
            curDoc = WordApp.Documents.Open(sTempFile);

            oTable1Paragraf = curDoc.Content.Paragraphs.Add(ref oMissing);
            oTable1Paragraf.Range.Text = "Επίσημη Ενημέρωση Πελατών";
            oTable1Paragraf.Range.Font.Bold = 1;
            oTable1Paragraf.Format.SpaceAfter = 20;    //24 pt spacing after paragraph.
            oTable1Paragraf.Range.InsertParagraphAfter();

            oHeader1Paragraf = curDoc.Content.Paragraphs.Add(ref oMissing);
            oHeader1Paragraf.Range.InsertParagraphAfter();
            oHeader1Paragraf.Range.Text = "Ημερομηνία";
            oHeader1Paragraf.Range.Font.Bold = 0;
            oHeader1Paragraf.Format.SpaceAfter = 2;

            oTable1 = curDoc.Content.Tables.Add(oTable1Paragraf.Range, (fgList.Rows.Count - 1), 18);
            oTable1.Range.ParagraphFormat.SpaceBefore = 0;
            oTable1.Range.ParagraphFormat.SpaceAfter = 0;
            oTable1.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            oTable1.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            oTable1.Range.Font.Size = 7;
            oTable1.Range.Font.Bold = 0;

            oTable1.LeftPadding = 0;
            oTable1.RightPadding = 0;
            oTable1.Spacing = 0;

            oTable1.Columns[1].SetWidth(WordApp.Application.CentimetersToPoints(0.7f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[2].SetWidth(WordApp.Application.CentimetersToPoints(3.4f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[3].SetWidth(WordApp.Application.CentimetersToPoints(2.4f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[4].SetWidth(WordApp.Application.CentimetersToPoints(2f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[5].SetWidth(WordApp.Application.CentimetersToPoints(2f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[6].SetWidth(WordApp.Application.CentimetersToPoints(0.6f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[7].SetWidth(WordApp.Application.CentimetersToPoints(1.4f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[8].SetWidth(WordApp.Application.CentimetersToPoints(2f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[9].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[10].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[11].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[12].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[13].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[14].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[15].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[16].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[17].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Columns[18].SetWidth(WordApp.Application.CentimetersToPoints(1f), Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
            oTable1.Cell(1, 1).Range.Text = "Α/Α";
            oTable1.Cell(1, 2).Range.Text = "Ονοματεπώνυμο";
            oTable1.Cell(1, 3).Range.Text = "Πάροχος";
            oTable1.Cell(1, 4).Range.Text = "Κώδικος";
            oTable1.Cell(1, 5).Range.Text = "Portfolio";
            oTable1.Cell(1, 6).Range.Text = "Πράξη";
            oTable1.Cell(1, 7).Range.Text = "Τύπος";
            oTable1.Cell(1, 8).Range.Text = "Τίτλος";
            oTable1.Cell(1, 9).Range.Text = "Reuters";
            oTable1.Cell(1, 10).Range.Text = "ISIN";
            oTable1.Cell(1, 11).Range.Text = "Τιμή";
            oTable1.Cell(1, 12).Range.Text = "Ποσοτ";
            oTable1.Cell(1, 13).Range.Text = "Αξια";
            oTable1.Cell(1, 14).Range.Text = "Νομισμα";
            oTable1.Cell(1, 15).Range.Text = "Ημερ.Εκτέλεσης";
            oTable1.Cell(1, 16).Range.Text = "Ημερ.Ενυμερ";
            oTable1.Cell(1, 17).Range.Text = "Τρόπος Ενημερ";
            oTable1.Cell(1, 18).Range.Text = "Θεμα";
            oTable1.Rows[1].Range.Font.Name = "Arial";
            oTable1.Rows[1].Range.Font.Size = 7;
            oTable1.Rows[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                oTable1.Cell(i + 1, 1).Range.Text = i + "";
                oTable1.Cell(i + 1, 2).Range.Text = fgList[i, "ClientName"] + "";
                oTable1.Cell(i + 1, 3).Range.Text = fgList[i, "ProviderTitle"] + "";
                oTable1.Cell(i + 1, 4).Range.Text = fgList[i, "Code"] + "";
                oTable1.Cell(i + 1, 5).Range.Text = fgList[i, "Portfolio"] + "";
                oTable1.Cell(i + 1, 6).Range.Text = fgList[i, "Aktion"] + "";
                oTable1.Cell(i + 1, 7).Range.Text = fgList[i, "Product_Type"] + "";
                oTable1.Cell(i + 1, 8).Range.Text = fgList[i, "Product_Title"] + "";
                oTable1.Cell(i + 1, 9).Range.Text = fgList[i, "Share_Code"] + "";
                oTable1.Cell(i + 1, 10).Range.Text = fgList[i, "ISIN"] + "";
                oTable1.Cell(i + 1, 11).Range.Text = fgList[i, "Price"] + "";
                oTable1.Cell(i + 1, 12).Range.Text = fgList[i, "Quantity"] + "";
                oTable1.Cell(i + 1, 13).Range.Text = fgList[i, "Amount"] + "";
                oTable1.Cell(i + 1, 14).Range.Text = fgList[i, "Currency"] + "";
                oTable1.Cell(i + 1, 15).Range.Text = fgList[i, "ExecuteDate"] + "";
                oTable1.Cell(i + 1, 16).Range.Text = fgList[i, "InformDate"] + "";
                oTable1.Cell(i + 1, 17).Range.Text = fgList[i, "InformMethod"] + "";
                oTable1.Cell(i + 1, 18).Range.Text = fgList[i, "Thema"] + "";
                /*
                oTable1.Cell(i, 3).Range.Text = dtRow("Good_Title");
                oTable1.Cell(i, 4).Range.Text = FormatNumber(dtRow("Price"), 2, TriState.True);
                oTable1.Cell(i, 5).Range.Text = FormatNumber(dtRow("Amount"), 2, TriState.True);
                oTable1.Cell(i, 6).Range.Text = "";
                oTable1.Rows[i].Range.Font.Name = "Times New Roman";
                oTable1.Rows[i].Range.Font.Size = 12;
                oTable1.Rows[i].Range.Font.Bold = False;
                oTable1.Rows[i].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                oTable1.Cell(i, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                */
            }

            curDoc.SaveAs2(sTargetFile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
            WordApp.Documents.Close();

            WordApp.Quit();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }
        private void DefineCommandsList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 1;
            i = 0;

            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            klsOrder.CommandType_ID = 1;
            klsOrder.DateFrom = dFrom.Value;
            klsOrder.DateTo = dTo.Value;
            klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            klsOrder.Sent = 0;
            klsOrder.Actions = 1;
            klsOrder.SendCheck = 0;
            klsOrder.User_ID = 0;
            klsOrder.User1_ID = 0;
            klsOrder.User4_ID = 0;
            klsOrder.Division_ID = 0;
            klsOrder.Code = lblCode.Text;
            klsOrder.Product_ID = 0;
            klsOrder.Share_ID = 0;
            klsOrder.Currency = "";
            klsOrder.ShowCancelled = 0;
            klsOrder.GetList();
            foreach (DataRow dtRow in klsOrder.List.Rows)
            {
                if (Convert.ToSingle(dtRow["RealPrice"]) != 0)
                {
                    i = i + 1;

                    sDate = "";
                    if ((dtRow["OfficialInformingDate"] + "") != "") sDate = Convert.ToDateTime(dtRow["OfficialInformingDate"]).ToString("dd/MM/yy");

                    sClientName = dtRow["ClientFullName"] + "";
                    sRecipientName = "";
                    if (Convert.ToInt32(dtRow["ContractTipos"]) == 0) sRecipientName = dtRow["ClientFullName"] + "";             // Fisiko Prosopo
                    else sRecipientName = dtRow["Recipient"] + "";

                    sConnectionMethod = "";
                    sConnectionData = "";
                    switch (Convert.ToInt32(dtRow["ConnectionMethod"]))
                    {
                        case 1:
                            sConnectionMethod = "e-mail";
                            sConnectionData = dtRow["EMail_Today"] + "";
                            break;
                        case 2:
                            sConnectionMethod = "Ταχ/κη αποστολή";
                            sConnectionData = sRecipientName + "\n" + dtRow["Address"] + "\n" + dtRow["City"] + " " + dtRow["ZIP"];
                            if (dtRow["Country_Title"] + "" != "Greece") sConnectionData = sConnectionData + " " + dtRow["Country_Title"];
                            break;
                    }

                    fgList.AddItem(false + "\t" + i + "\t" + sClientName + "\t" + dtRow["ContractTitle"] + "\t" +
                                   dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                   ((Convert.ToInt32(dtRow["Aktion"]) == 1) ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                   dtRow["Share_Title"] + "\t" + dtRow["Share_Code2"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                   (Convert.ToDouble(dtRow["RealPrice"]) == 0 ? "" : Convert.ToDouble(dtRow["RealPrice"]).ToString("0.00##")) + "\t" +
                                   (Convert.ToDouble(dtRow["RealQuantity"]) == 0 ? "" : Convert.ToDouble(dtRow["RealQuantity"]).ToString("0.00")) + "\t" +
                                   (Convert.ToDouble(dtRow["RealAmount"]) == 0 ? "" : Convert.ToDouble(dtRow["RealAmount"]).ToString("0.00")) + "\t" +
                                   dtRow["Currency"] + "\t" + dtRow["ExecuteDate"] + "\t" + sDate + "\t" +
                                   sConnectionMethod + "\t" + sConnectionData + "\t" + "Βεβαίωση εκτέλεσης συναλλαγής" + "\t" +
                                   dtRow["LastCheckFile"] + "\t" + "" + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                   dtRow["ConnectionMethod"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"]);
                }
            }
            fgList.Redraw = true;
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            lnkPelatis.Text = stContractData.ClientName;
            lblCode.Text = stContractData.Code;
            lblProfitCenter.Text = stContractData.Portfolio;
            iClient_ID = stContractData.Client_ID;
        }
        private void EmptyContractData()
        {
            stContractData.ContractTitle = "";
            stContractData.Code = "";
            stContractData.Portfolio = "";
            stContractData.ClientName = "";
            stContractData.Service_Title = "";
            stContractData.Profile_Title = "";
            stContractData.Policy_Title = "";
            stContractData.Provider_Title = "";
            stContractData.Package_Title = "";
            stContractData.Currency = "";
            stContractData.EMail = "";
            stContractData.Mobile = "";
            stContractData.NumberAccount = "";
            stContractData.Contract_ID = 0;
            stContractData.Client_ID = 0;
            stContractData.Provider_ID = 0;
            stContractData.Policy_ID = 0;
            stContractData.Profile_ID = 0;
            stContractData.Service_ID = 0;
            stContractData.Status = 0;
            stContractData.ClientType = 0;
            stContractData.VAT_Percent = 0;
            stContractData.CFP_ID = 0;
            stContractData.Contracts_Details_ID = 0;
            stContractData.Contracts_Packages_ID = 0;
            stContractData.MIFID_Risk_Index = 0;
            stContractData.MIFIDCategory_ID = 0;
            stContractData.MIFID_2 = 0;
        }

        private void chkCommands_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkCommands.Checked;
        }

        private void EditCommandsInformingData()
        {
            lblInformingDate.Text = fgList[fgList.Row, "InformDate"] + "";
            lblInformingMethod.Text = fgList[fgList.Row, "InformMethod"] + "";
            lblInformingClientData.Text = fgList[fgList.Row, "ClientData"] + "";
            txtThema.Text = fgList[fgList.Row, "Thema"] + "";
            txtStatement_FileName.Text = fgList[fgList.Row, "StatementFile"] + "";
            txtInforming_Notes.Text = fgList[fgList.Row, "Notes"] + "";
            panEditCommandData.Visible = true;
            btnCancel.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            fgList[fgList.Row, "ClientData"] = lblInformingClientData.Text;
            fgList[fgList.Row, "Thema"] = txtThema.Text;
            fgList[fgList.Row, "StatementFile"] = txtStatement_FileName.Text;
            fgList[fgList.Row, "Notes"] = txtInforming_Notes.Text;
            panEditCommandData.Visible = false;

            if (sStatement_FileName.Length > 0)
            {
                sTemp = fgList[fgList.Row, "ContractTitle"] + "";
                txtStatement_FileName.Text = Global.DMS_UploadFile(sStatement_FileName, @"Customers/" + sTemp.Replace(".", "_") + "/Informing", txtStatement_FileName.Text);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panEditCommandData.Visible = false;
        }
        private string DailyEmailBody(string sProvider)
        {
            string sBody;
            sBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
                    "<style>img.logo {height: 60%;width: 40%;}</style></head><body style='width: 800px;'><font face='verdana'><br/><br/><table><tr><td width=800>" +
                    "<div style='height: 150px;'><img class='logo' src='http://www.hellasfin.gr/signs/images/Logo_500px.jpg'  width='50%' alt='' /></div><br/><br/><br/><br/>" +
                    "<div align='right'>ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy") + "</div><br/><br/><br/><br/><br/><br/>" +
                    "<center> ΘΕΜΑ: Βεβαίωση εκτέλεσης συναλλαγής </center>" + "<br/><br/><br/><br/>" +
                    "Στο πλαίσιο ενημέρωσής σας, σας επισυνάπτουμε βεβαίωση εκτέλεσης συναλλαγής της εκτελούσας επιχείρησης " + sProvider +
                    ". <br/><br/>Στη διάθεσή σας για οποιαδήποτε διευκρίνιση." + "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
                    "<div align='left'>HELLASFIN Α.Ε.Π.Ε.Υ.<br/><br/>Διεύθυνση Λειτουργικής Υποστήριξης και Εξυπηρέτησης Πελατών</div>" + "<br/><br/>" +
                    "Παρακαλούμε για οποιαδήποτε διευκρίνηση επικοινωνήστε με τον Επενδυτικό σας Σύμβουλο ή τον Υπεύθυνο Σχέσης (RM) στα τηλ. Θεσσαλονίκη: +30 2310 517800, " +
                    "Αθήνα: +30 210 3387710, Κρήτη: +30 2810 343366<br/><br/>" +
                    "*Tυχόν αντιρρήσεις σας σε οποιοδήποτε στοιχείο της παρούσας ενημέρωσης καλείστε να τις υποβάλλετε στην Εταιρία μας εγγράφως εντός δεκαπέντε (15) " +
                    "ημερολογιακών ημερών, αλλιώς θεωρούμε ότι συμφωνείτε απολύτως. </td></tr></table><br/><br/></font></body></html>";
            return sBody;
        }
        private void picStatement_FilePath_Click(object sender, EventArgs e)
        {
            sStatement_FileName = Global.FileChoice(Global.DefaultFolder);
            if (sStatement_FileName.Length > 0) txtStatement_FileName.Text = Path.GetFileName(sStatement_FileName);
        }

        private void picStatement_Show_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, "StatementFile"] + "";
            if (sTemp.Length > 0 && txtStatement_FileName.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", txtStatement_FileName.Text.Trim());

        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;   // !!!!!!!!!!
            locContract.ClientFullName = fgList[fgList.Row, 2] + "";
            locContract.RightsLevel = 1;
            locContract.ShowDialog();
        }

        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }

        private void mnuCommandData_Click(object sender, EventArgs e)
        {
            frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
            locOrderSecurity.Rec_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locOrderSecurity.Editable = 0;
            locOrderSecurity.ShowDialog();
        }

        private void mnuViewStatement_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, "ContractTitle"] + "";
            Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgList[fgList.Row, "StatementFile"].ToString());
        }
    }
}
