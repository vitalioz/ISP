using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Accounting
{
    public partial class frmAcc_InvoicesRTO : Form
    {
        int i, j, iID, iNum, iRec_ID, iInvoiceType = 0, iRightsLevel, iCopies, iMode,
            iInvoiceFisiko = 0, iInvoiceNomiko = 0, iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko;
        string sSeira, sInvoicePrinter, sPDF_FullPath, sSubPath = "", sInvTitleGr = "", sInvTitleEn = "", 
               sInvTitleFisikoGr = "", sInvTitleFisikoEn = "", sInvoiceCodeFisiko = "", sInvTitleNomikoGr = "", sInvTitleNomikoEn = "",
               sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "", sSeiraPistotikoFisiko = "", sSeiraPistotikoNomiko = "",
               sSeiraAkyrotiko = "", sInvoiceTemplate = "", sInvoiceAnalysisTemplate = "",
               sInvoiceCodePistotikoFisiko = "", sInvTitlePistotikoFisikoGr = "", sInvTitlePistotikoFisikoEn = "", sInvoiceTypePistotikoFisiko = "",
               sInvoiceCodePistotikoNomiko = "", sInvTitlePistotikoNomikoGr = "", sInvTitlePistotikoNomikoEn = "", sInvoiceTypePistotikoNomiko = "",
               sInvoiceCodeAkyrotiko = "", sInvTitleAkyrotikoGr = "", sInvTitleAkyrotikoEn = "", sInvoiceTypeAkyrotiko = "",
               sSeiraFisiko = "", sSeiraNomiko = "", sExtra;        
        DateTime dTemp, dIssueDate;
        bool bCheckList = false, bSettingPrinter = false;
        C1.Win.C1FlexGrid.CellRange rng;
        Hashtable imgMap = new Hashtable();
        clsSettlements klsSettlement = new clsSettlements();
        DataRow[] foundRows;
        Point position;
        bool pMove;

        public frmAcc_InvoicesRTO()
        {
            InitializeComponent();
        }
        private void frmAcc_InvoicesRTO_Load(object sender, EventArgs e)
        {
            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            for (i = 0; i < imgFile.Images.Count; i++) imgMap.Add(i, imgFile.Images[i]); 

            cmbServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedItem = 1;

            cmbServices.DataSource = Global.dtServices.Copy();
            cmbServices.DisplayMember = "Title";
            cmbServices.ValueMember = "ID";
            cmbServices.SelectedItem = 1;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            fgList.ShowCellLabels = true;
            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";
            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = " ";
            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = Global.GetLabel("n");
            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = Global.GetLabel("contract");
            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("code");
            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("subaccount");
            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("provider");
            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("service");
            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("profile");

            rng = fgList.GetCellRange(0, 9, 0, 14);
            rng.Data = "Έξοδα Λήψης & Διαβίβασης";
            fgList[1, 9] = "% σύμβασης";
            fgList[1, 10] = "% έκπτωση";
            fgList[1, 11] = "τελικό %";
            fgList[1, 12] = "ποσό μετά την έκπτωση";
            fgList[1, 13] = "Ισοτιμία μετατροπής (EUR)";
            fgList[1, 14] = "ποσό σε EUR";
            rng = fgList.GetCellRange(0, 15, 0, 18);
            rng.Data = "Minimum Εξοδο λήψης & Διαβίβασης";
            fgList[1, 15] = "σύμβασης";
            fgList[1, 16] = "% έκπτωσης";
            fgList[1, 17] = "ποσό έκπτωσης";
            fgList[1, 18] = "ποσό μετά την έκπτωσης";
            fgList.Cols[19].AllowMerging = true;
            rng = fgList.GetCellRange(0, 19, 1, 19);
            rng.Data = "Εξοδο λήψης & Διαβίβασης σε EUR";
            fgList.Cols[20].AllowMerging = true;
            rng = fgList.GetCellRange(0, 20, 1, 20);
            rng.Data = "ΦΠΑ";
            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);
            rng.Data = "Πληρωτέο Ποσό";
            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = "Αρ.Παραστατικου";
            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = Global.GetLabel("notes");

            rng = fgList.GetCellRange(0, 24, 0, 26);
            rng.Data = Global.GetLabel("product");
            fgList[1, 24] = Global.GetLabel("title");
            fgList[1, 25] = Global.GetLabel("isin");
            fgList[1, 26] = Global.GetLabel("category");

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = Global.GetLabel("action");
            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = "Ημερ.Συναλλαγής";
            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = "Ημερ.Εκκαθάρησης";
            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = Global.GetLabel("currency");
            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = Global.GetLabel("price");
            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = Global.GetLabel("quantity");
            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = Global.GetLabel("amount");
            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = Global.GetLabel("afm");
            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = Global.GetLabel("doy");

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "1ος Δικαιούχος";
            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = Global.GetLabel("address");
            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = Global.GetLabel("city");
            fgList.Cols[39].AllowMerging = true;
            rng = fgList.GetCellRange(0, 39, 1, 39);
            rng.Data = Global.GetLabel("zip");
            fgList.Cols[40].AllowMerging = true;
            rng = fgList.GetCellRange(0, 40, 1, 40);
            rng.Data = Global.GetLabel("country");  

            fgList.Cols[41].AllowMerging = true;
            rng = fgList.GetCellRange(0, 41, 1, 41);
            rng.Data = "Cash Account EUR";
            fgList.Cols[42].AllowMerging = true;
            rng = fgList.GetCellRange(0, 42, 1, 42);
            rng.Data = "Advisor";
            fgList.Cols[43].AllowMerging = true;
            rng = fgList.GetCellRange(0, 43, 1, 43);
            rng.Data = "RM";
            fgList.Cols[44].AllowMerging = true;
            rng = fgList.GetCellRange(0, 44, 1, 44);
            rng.Data = "ID εντολής";

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;

            ucExec.DateFrom = Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            ucExec.DateTo = DateTime.Now;
            ucSettlement.DateFrom = DateTime.Now.AddDays(-1);
            ucSettlement.DateTo = DateTime.Now;

            DefineOptions();

            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 168;
            fgList.Width = this.Width - 30;
            panTools.Width = this.Width - 30;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
            ShowList();
            panTools.Visible = true;
            chkPrint.Visible = true;
            fgList.Visible = true;
        }
        private void cmbServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            clsInvoicesRTO_Details klsInvoicesRTO_Details = new clsInvoicesRTO_Details();
            if (iRec_ID != 0)
            {
                klsInvoicesRTO_Details.Record_ID = iRec_ID;
                klsInvoicesRTO_Details.GetRecord();
            }
            klsInvoicesRTO_Details.Command_ID = Convert.ToInt32(fgList[i, "Record_ID"]);
            klsInvoicesRTO_Details.InvoiceType = ( (iMode < 3) ? Convert.ToInt32(fgList[i, "InvoiceType"]) : iMode);
            klsInvoicesRTO_Details.InvoiceTitles_ID = iID;
            klsInvoicesRTO_Details.RealQuantity = Convert.ToSingle(fgList[i, "Quantity"]);
            klsInvoicesRTO_Details.Curr = fgList[i, "Currency"] + "";
            klsInvoicesRTO_Details.RealPrice = Convert.ToSingle(fgList[i, "Price"]);
            klsInvoicesRTO_Details.RealAmount = Convert.ToSingle(fgList[i, "Axia"]);

            klsInvoicesRTO_Details.FeesPercent = Convert.ToSingle(lblFeesPercent.Text);
            klsInvoicesRTO_Details.FeesDiscountPercent = Convert.ToSingle(lblFeesDiscountPercent.Text);
            klsInvoicesRTO_Details.FinishFeesPercent = Convert.ToSingle(lblFinishFeesPercent.Text);
            klsInvoicesRTO_Details.FinishFeesAmount = Convert.ToSingle(lblFinishFeesAmount.Text);
            klsInvoicesRTO_Details.FeesRate = Convert.ToSingle(lblFeesRate.Text);
            klsInvoicesRTO_Details.FeesAmountEUR = Convert.ToSingle(lblFeesAmountEUR.Text);
            klsInvoicesRTO_Details.MinFeesAmount = Convert.ToSingle(lblMinFeesAmount.Text);
            klsInvoicesRTO_Details.MinFeesDiscountPercent = Convert.ToSingle(lblMinFeesDiscountPercent.Text);
            klsInvoicesRTO_Details.MinFeesDiscountAmount = Convert.ToSingle(lblMinFeesDiscountAmount.Text);
            klsInvoicesRTO_Details.FinishMinFeesAmount = Convert.ToSingle(lblFinishMinFeesAmount.Text);
            klsInvoicesRTO_Details.FeesProVAT = Convert.ToSingle(txtFeesProVAT.Text);
            klsInvoicesRTO_Details.FeesVAT = Convert.ToSingle(txtFeesVAT.Text);
            klsInvoicesRTO_Details.CompanyFees = Convert.ToSingle(txtCompanyFees.Text);
            if (iRec_ID == 0) klsInvoicesRTO_Details.InsertRecord();
            else              klsInvoicesRTO_Details.EditRecord();

            panEdit.Visible = false;

            DefineList();
            ShowList();
        }

        private void picClose_History_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }

        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                if (fgList.Col == 1) ShowInvoice();
                else if (Convert.ToInt32(fgList[fgList.Row, "InvoiceType"]) > 2) EditRecord();               
                     else ShowOrder();
            }
        }
        private void EditRecord()
        {
            iRec_ID = 0;
            i = fgList.Row;
            iMode = 4;
            EditRec();
        }
        private void fgList_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                if (fgList.Col == 0)
                {
                    if (Convert.ToBoolean(fgList[fgList.Row, 0]))
                    {
                        if (fgList[fgList.Row, "FileName"].ToString() != "")
                        {
                            fgList[fgList.Row, 0] = false;
                            MessageBox.Show("Invoice was issued", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }

        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void DefineList()
        {
            if (chkDateExec.Checked)
            {
                klsSettlement.DateExecFrom = ucExec.DateFrom;
                klsSettlement.DateExecTo = ucExec.DateTo;
            }
            else
            {
                klsSettlement.DateExecFrom = Convert.ToDateTime("1900/01/02");
                klsSettlement.DateExecTo = Convert.ToDateTime("2070/12/30");
            }
            if (chkDateSettlement.Checked)
            {
                klsSettlement.DateFrom = ucSettlement.DateFrom;
                klsSettlement.DateTo = ucSettlement.DateTo;
            }
            else
            {
                klsSettlement.DateFrom = Convert.ToDateTime("1900/01/02");
                klsSettlement.DateTo = Convert.ToDateTime("2070/12/30");
            }

            klsSettlement.StockCompany_ID = 0;
            klsSettlement.User1_ID = 0;
            klsSettlement.User4_ID = 0;
            klsSettlement.Division_ID = 0;
            klsSettlement.ClientCode = "";
            klsSettlement.GetList();
        }
        private void ShowList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            int i = 0;
            foreach (DataRow dtRow in klsSettlement.List.Rows)
            {
                if (((Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0) || (Convert.ToInt32(dtRow["StockCompany_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue))) &&
                    ((Convert.ToInt32(cmbServices.SelectedValue) == 0) || (Convert.ToInt32(dtRow["Service_ID"]) == Convert.ToInt32(cmbServices.SelectedValue))) &&
                    ((txtCode.Text.Trim() == "" || dtRow["Code"].ToString().Contains(txtCode.Text)))
                  )
                {
                    i = i + 1;
                    fgList.AddItem(false + "\t" + dtRow["ImageType"] + "\t" + i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + 
                                dtRow["StockCompanyTitle"] + "\t" +  dtRow["ServiceTitle"] + "\t" + dtRow["ProfileTitle"] + "\t" + 
                                dtRow["RTO_FeesPercent"] + "\t" + dtRow["RTO_FeesDiscountPercent"] + "\t" + dtRow["RTO_FinishFeesPercent"] + "\t" + dtRow["RTO_FinishFeesAmount"] + "\t" +
                                dtRow["CurrRate"] + "\t" + dtRow["RTO_FeesAmountEUR"] + "\t" + dtRow["RTO_MinFeesAmount"] + "\t" +
                                dtRow["RTO_MinFeesDiscountPercent"] + "\t" + dtRow["RTO_MinFeesDiscountAmount"] + "\t" + dtRow["RTO_FinishMinFeesAmount"] + "\t" +
                                dtRow["RTO_FeesProVAT"] + "\t" + dtRow["RTO_FeesVAT"] + "\t" + dtRow["RTO_CompanyFees"] + "\t" + dtRow["Invoice_Num"] + "\t" + dtRow["Notes"] + "\t" +                                
                                dtRow["ShareTitle"] + "\t" +  dtRow["ISIN"] + "\t" +  dtRow["ProductTitle"] + "\t" + dtRow["Aktion"] + "\t" +
                                dtRow["ExecuteDate"] + "\t" + dtRow["SettlementDate"] + "\t" + dtRow["Curr"] + "\t" + dtRow["RealPrice"] + "\t" + 
                                dtRow["RealQuantity"] + "\t" + dtRow["RealAmount"] + "\t" + dtRow["AFM"] + "\t" + dtRow["DOY"] + "\t" + 
                                dtRow["ClientName"] + "\t" + dtRow["Address"] + "\t" + dtRow["City"] + "\t" + dtRow["Zip"] + "\t" + dtRow["CountryTitleGr"] + "\t" +                                 
                                dtRow["CashAccount"] + "\t" + dtRow["AdvisorName"] + "\t" + dtRow["RMName"] + "\t" + dtRow["ID"] + "\t" + dtRow["BusinessType_ID"] + "\t" +
                                dtRow["ClientTipos"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Service_ID"] + "\t" +
                                dtRow["FileName"] + "\t" + dtRow["MIFID_2"] + "\t" + dtRow["CountryTitleEn"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + 
                                dtRow["Contracts_Packages_ID"] + "\t" + dtRow["ContractTipos"] + "\t" + dtRow["InvoiceType"] + "\t" + dtRow["RTO_ID"] + "\t" + 
                                dtRow["Invoice_Titles_ID"] + "\t" + "" + "\t" + dtRow["CountryTitleEn"]);
                }
            }
            fgList.Redraw = true;
        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, 1].ToString();
            locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
            locContract.ShowDialog();
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }
        private void mnuShowInvoice_Click(object sender, EventArgs e)
        {
            ShowInvoice();
        }
        private void ShowInvoice()
        {
            if (fgList[fgList.Row, "FileName"].ToString().Length > 0)
            {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "ContractTitle"] + "\\Invoices", fgList[fgList.Row, "FileName"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowOrder();
        }
        private void tsbFeesCalculation_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            klsOrder.DateFrom = ucExec.DateFrom.Date;
            klsOrder.DateTo = ucExec.DateTo.Date;
            klsOrder.CalcRTOFees();
            this.Cursor = Cursors.Default;

            MessageBox.Show("Calculation Finished", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void tsbHistory_Click(object sender, EventArgs e)
        {
            //
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            frmOptions locOptions = new frmOptions();
            locOptions.StartPosition = FormStartPosition.CenterScreen;
            locOptions.RightsLevel = 2;
            locOptions.VisualFlags = "00100000";
            locOptions.Show();

            DefineOptions();
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {

        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Τιμολόγηση ΛΔ εντολών";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 2; this.j <= 44; this.j++)
                {
                    if (j == 21)
                    {
                        EXL.Cells[i + 3, j].Value = fgList[i, j];
                    }
                    else
                    {
                        EXL.Cells[i + 3, j].Value = fgList[i, j];
                    }
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count - 2; i++) fgList[i, 0] = chkPrint.Checked;
        }

        private void mnuPistotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0 && fgList.Row < (fgList.Rows.Count - 1)) {
                iRec_ID = 0;
                i = fgList.Row;
                iMode = 4;
                EditRec();
            }
        }

        private void mnuAkyrotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0 && fgList.Row < fgList.Rows.Count - 1) 
            { 
               iRec_ID = 0;
               i = fgList.Row;
               iMode = 5;
               EditRec();
            }
        }
        private void mnuPrintInvoice_Click(object sender, EventArgs e)
        {
            fgList[fgList.Row, 0] = true;
            PrintingInvoices();
        }
        private void EditRec()
        {
            if (iMode == 1) j = 1;
            else            j = -1;

            i = fgList.Row;
            lblContractTitle.Text = fgList[i, "ContractTitle"].ToString();
            lblCode.Text = fgList[i, "Code"].ToString();
            lblPortfolio.Text = fgList[i, "Portfolio"].ToString();
            lblProvider.Text = fgList[i, "Provider"].ToString();
            lblService.Text = fgList[i, "Service"].ToString();
            lblProfile.Text = fgList[i, "Profile"].ToString();
            lblMasterName.Text = fgList[i, "FirstOwner"].ToString();
            lblQuantity.Text = fgList[i, "Quantity"].ToString();
            lblCurrency.Text = fgList[i, "Currency"].ToString();
            lblPrice.Text = fgList[i, "Price"].ToString();
            lblAmount.Text = fgList[i, "Axia"].ToString();
            lblFeesPercent.Text = fgList[i, "FeesPercent"].ToString();
            lblFeesDiscountPercent.Text = fgList[i, "FeesDiscountPercent"].ToString();
            lblFinishFeesPercent.Text = fgList[i, "AfterDiscount"].ToString();
            lblFinishFeesAmount.Text = (Convert.ToDecimal(fgList[i, "SeNomismaPraxis"])).ToString();
            lblFeesRate.Text = fgList[i, "RateEUR"].ToString();
            lblFeesAmountEUR.Text = (Convert.ToDecimal(fgList[i, "SeEUR"])).ToString();
            lblMinFeesAmount.Text = fgList[i, "MinFeesAmount"].ToString();
            lblMinFeesDiscountPercent.Text = fgList[i, "MinFeesDiscountPercent"].ToString();
            lblMinFeesDiscountAmount.Text = fgList[i, "MinFeesDiscountAmount"].ToString();
            lblFinishMinFeesAmount.Text = fgList[i, "MinSeEUR"].ToString();
            txtFeesProVAT.Text = (j * Math.Abs(Convert.ToDecimal(fgList[i, "Amount"]))).ToString();
            txtFeesVAT.Text = fgList[i, "VAT"].ToString();
            txtCompanyFees.Text = (j * Math.Abs(Convert.ToDecimal(fgList[i, "PayAmount"]))).ToString();
            panEdit.Visible = true;
        }
        private void DefineOptions()
        {            
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            //sFeesFilePath = Options.FeesFilePath;
            sInvoicePrinter = Options.InvoicePrinter;
            iCopies = Options.InvoiceCopies;
            iInvoiceFisiko = Options.InvoiceFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitleFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitleFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeFisiko = foundRows[0]["Type"].ToString();
            }

            iInvoiceNomiko = Options.InvoiceNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitleNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitleNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeNomiko = foundRows[0]["Type"].ToString();
            }

            iInvoicePistotikoFisiko = Options.InvoicePistotikoFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoFisiko = foundRows[0]["Type"].ToString();
            }

            iInvoicePistotikoNomiko = Options.InvoicePistotikoNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoNomiko = foundRows[0]["Type"].ToString();
            }

            iInvoiceAkyrotiko = Options.InvoiceAkyrotiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceAkyrotiko);
            if (foundRows.Length > 0)
            {
                sInvTitleAkyrotikoGr = foundRows[0]["Title"].ToString();
                sInvTitleAkyrotikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeAkyrotiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeAkyrotiko = foundRows[0]["Type"].ToString();
            }

            sInvoiceTemplate = Options.InvoiceTemplate;
            sInvoiceAnalysisTemplate = Options.InvoiceAnalysisTemplate;
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            bool bPrintInvoice = true;
            if (!bSettingPrinter)
            {
                bSettingPrinter = true;

                sInvoicePrinter = Global.InvoicePrinter;
                frmPrintInvoiceOptions PrintInvoiceOptions = new frmPrintInvoiceOptions();
                PrintInvoiceOptions.Mode = 2;                                               // 1 - full options, 2 - hide IssueDate
                PrintInvoiceOptions.InvoicePrinter = Global.InvoicePrinter;
                PrintInvoiceOptions.NumCopies = iCopies;
                PrintInvoiceOptions.DateIssue = Convert.ToDateTime(fgList[fgList.Row, "ExecuteDate"]);
                PrintInvoiceOptions.ShowDialog();
                if (PrintInvoiceOptions.LastAktion == 1)
                {
                    sInvoicePrinter = PrintInvoiceOptions.InvoicePrinter;
                    iCopies = PrintInvoiceOptions.NumCopies;
                    dIssueDate = PrintInvoiceOptions.DateIssue;
                    bPrintInvoice = true;
                    //PrinterClass.SetDefaultPrinter(sInvoicePrinter);
                }
                else bPrintInvoice = false;
            }

            if (bPrintInvoice) PrintingInvoices();
        }
        private void PrintingInvoices()
        {
            int iLine;
            string sTemp, sInvRow1_Gr, sInvRow1_En, sMainTitleGr, sMainTitleEn, sEnergeiaGr, sEnergeiaEn,
                   sCol1_Gr, sCol1_En, sCol3_Gr, sCol3_En, sRow1_Gr, sRow1_En, sRow2_Gr, sRow2_En, sRow4_Gr, sRow4_En, sRow5_Gr, sRow5_En, sRow6_Gr, sRow6_En,
                   sCountry, sProfileGr, sProfileEn, sProfile, sInvoiceCode, sMergeFile, sNum, sInvType, sEafdss, sFileName;
            string[] sNewFiles;
            bool bPDFsMerged = false;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            bCheckList = false;
            iInvoiceType = 0;
            iNum = 0;
            iLine = 0;
            sNum = "";
            sSeira = "";
            sNewFiles = new string[2] { "", "" };
            sInvoiceCode = "";
            sProfileGr = "";
            sProfileEn = "";
            sProfile = "";
            sCol1_Gr = "";
            sCol1_En = "";
            sCol3_Gr = "";
            sCol3_En = "";
            sCountry = "";
            sInvType = "";
            sEafdss = "";

            try
            {
                WordApp.Visible = false;

                for (iLine = 2; iLine <= (fgList.Rows.Count - 1); iLine++)
                {
                    if (Convert.ToBoolean(fgList[iLine, 0]))
                    {
                        if (DateTime.TryParse(fgList[iLine, "ExecuteDate"].ToString(), out DateTime Temp) == true)
                        {
                            switch (Convert.ToInt16(fgList[iLine, "InvoiceType"]))
                            {
                                case 1:
                                    iInvoiceType = iInvoiceFisiko;
                                    sInvoiceCode = sInvoiceCodeFisiko;
                                    sInvTitleGr = sInvTitleFisikoGr;
                                    sInvTitleEn = sInvTitleFisikoEn;
                                    sInvType = sInvoiceTypeFisiko;
                                    sSeira = sSeiraFisiko;
                                    break;
                                case 2:
                                    iInvoiceType = iInvoiceNomiko;
                                    sInvoiceCode = sInvoiceCodeNomiko;
                                    sInvTitleGr = sInvTitleNomikoGr;
                                    sInvTitleEn = sInvTitleNomikoEn;
                                    sInvType = sInvoiceTypeNomiko;
                                    sSeira = sSeiraNomiko;
                                    break;
                                case 4:                                                        //  4 - ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ
                                    if (Convert.ToInt16(fgList[iLine, "ClientTipos"]) == 1)
                                    {
                                        iInvoiceType = iInvoicePistotikoFisiko;
                                        sInvoiceCode = sInvoiceCodePistotikoFisiko;
                                        sSeira = sSeiraPistotikoFisiko;
                                        sInvTitleGr = sInvTitlePistotikoFisikoGr;
                                        sInvTitleEn = sInvTitlePistotikoFisikoEn;
                                        sInvType = sInvoiceTypePistotikoFisiko;
                                    }
                                    else
                                    {
                                        iInvoiceType = iInvoicePistotikoNomiko;
                                        sInvoiceCode = sInvoiceCodePistotikoNomiko;
                                        sSeira = sSeiraPistotikoNomiko;
                                        sInvTitleGr = sInvTitlePistotikoNomikoGr;
                                        sInvTitleEn = sInvTitlePistotikoNomikoEn;
                                        sInvType = sInvoiceTypePistotikoNomiko;
                                    }
                                    break;
                                case 5:                                                       // 5 - ΑΚΥΡΩΤΙΚΟ ΣΗΜΕΙΩΜΑ
                                    iInvoiceType = iInvoiceAkyrotiko;
                                    sInvoiceCode = sInvoiceCodeAkyrotiko;
                                    sSeira = fgList[iLine, "Invoice_External"].ToString();
                                    sInvTitleGr = sInvTitleAkyrotikoGr;
                                    sInvTitleEn = sInvTitleAkyrotikoEn;
                                    sInvType = sInvoiceTypeAkyrotiko;
                                    break;
                            }

                            clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
                            InvoiceTitles.Tipos = iInvoiceType;
                            InvoiceTitles.Seira = sSeira;
                            iNum = Convert.ToInt32(InvoiceTitles.GetInvoice_LastNumber()) + 1;

                            if (Convert.ToInt32(fgList[iLine, "ContractTipos"]) == 1) sSubPath = fgList[iLine, "ContractTitle"] +"";
                            else sSubPath = fgList[iLine, "FirstOwner"] + "";


                            if (Convert.ToInt32(fgList[iLine, "Service_ID"]) == 3)                                       // 3 - Discretionary                                        
                            {
                                sInvRow1_Gr = "Αμοιβή Διαβίβασης Εντολής";
                                sInvRow1_En = "Transmission Order Fee";
                                sMainTitleGr = "ΑΝΑΛΥΣΗ ΑΜΟΙΒΗΣ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ";
                                sMainTitleEn = "TRANSMISSION ORDER FEE ANALYSIS";
                                sRow1_Gr = "Αμοιβή Διαβίβασης Εντολής (%)";
                                sRow1_En = "Transmission Order Fee (%)";
                                sRow2_Gr = "Αμοιβή Διαβίβασης Εντολής";
                                sRow2_En = "Transmission Order Fee";
                                sRow4_Gr = "Αμοιβή Διαβίβασης Εντολής (EUR)";
                                sRow4_En = "Transmission Order Fee (EUR)";
                                sRow5_Gr = "Ελάχιστη Αμοιβή Διαβίβασης Εντολής (EUR)";
                                sRow5_En = "Minimum Transmission Order Fee (EUR)";
                                sRow6_Gr = "ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ (EUR)";
                                sRow6_En = "TRANSMISSION ORDER FEE (EUR)";
                            }
                            else
                            {
                                sInvRow1_Gr = "Αμοιβή Λήψης & Διαβίβασης Εντολής";
                                sInvRow1_En = "Reception & Transmission Order Fee";
                                sMainTitleGr = "ΑΝΑΛΥΣΗ ΑΜΟΙΒΗΣ ΛΗΨΗΣ & ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ";
                                sMainTitleEn = "RECEPTION & TRANSMISSION ORDER FEE ANALYSIS";
                                sRow1_Gr = "Αμοιβή Λήψης & Διαβίβασης Εντολής (%)";
                                sRow1_En = "Reception & Transmission Order Fee (%)";
                                sRow2_Gr = "Αμοιβή Λήψης & Διαβίβασης Εντολής";
                                sRow2_En = "Reception & Transmission Order Fee";
                                sRow4_Gr = "Αμοιβή Λήψης & Διαβίβασης Εντολής (EUR)";
                                sRow4_En = "Reception & Transmission Order Fee (EUR)";
                                sRow5_Gr = "Ελάχιστη Αμοιβή Λήψης & Διαβίβασης Εντολής (EUR)";
                                sRow5_En = "Minimum Reception & Transmission Order Fee (EUR)";
                                sRow6_Gr = "ΑΜΟΙΒΗ ΛΗΨΗΣ & ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ (EUR)";
                                sRow6_En = "RECEPTION & TRANSMISSION ORDER FEE (EUR)";
                            }

                            if (fgList[iLine, "Aktion"].ToString() == "BUY")
                            {
                                sEnergeiaGr = "ΑΓΟΡΑ";
                                sEnergeiaEn = "BUY";
                            }
                            else
                            {
                                sEnergeiaGr = "ΠΩΛΗΣΗ";
                                sEnergeiaEn = "SELL";
                            }

                            switch (Convert.ToInt32(fgList[iLine, "Product_ID"]))             // 46 - Product_ID
                            {
                                case 1:                                      // 1 - Metoxes
                                    sCol1_Gr = "Αρ. Μετοχών";
                                    sCol1_En = "Quantity";
                                    sCol3_Gr = "Τιμή";
                                    sCol3_En = "Price";
                                    break;
                                case 2:                                      // 2 - Omologa
                                    sCol1_Gr = "Ονομαστική Αξία";
                                    sCol1_En = "Nominal Value";
                                    sCol3_Gr = "Τιμή %";
                                    sCol3_En = "Price %";
                                    break;
                                case 4:                                      // 4 - DAK
                                    sCol1_Gr = "Αρ. Μεριδίων";
                                    sCol1_En = "Quantity";
                                    sCol3_Gr = "Τιμή";
                                    sCol3_En = "Price";
                                    break;
                                case 6:                                      // 6 - AK
                                    sCol1_Gr = "Αρ. Μεριδίων";
                                    sCol1_En = "Quantity";
                                    sCol3_Gr = "Τιμή";
                                    sCol3_En = "Price";
                                    break;
                            }

                            // --- Country : Greece or Not ------------
                            if (fgList[iLine, "Country"].ToString() == "" || fgList[iLine, "Country"].ToString() == "Ελλάδα" || fgList[iLine, "Country"].ToString() == "Greece")
                                sCountry = fgList[iLine, "Country"] + "";
                            else sCountry = fgList[iLine, "CountryEnglish"] + "";


                            if (Convert.ToInt16(fgList[iLine, "MIFID_2"]) == 1)
                            {
                                sProfileGr = "Επενδυτικό Προφίλ";
                                sProfileEn = "Investment Profile";
                                sProfile = fgList[iLine, "Profile"] + "";
                            }

                            sNum = iNum.ToString();
                            sTemp = sNum;
                            i = sTemp.Trim().Length;
                            switch (i)
                            {
                                case 1: { sNum = "00000000" + iNum; break; }
                                case 2: { sNum = "0000000" + iNum; break; }
                                case 3: { sNum = "000000" + iNum; break; }
                                case 4: { sNum = "00000" + iNum; break; }
                                case 5: { sNum = "0000" + iNum; break; }
                                case 6: { sNum = "000" + iNum; break; }
                                case 7: { sNum = "00" + iNum; break; }
                                case 8: { sNum = "0" + iNum; break; }
                            }                            

                            // --- check Temp folder  -------------
                            sPDF_FullPath = Application.StartupPath + "\\Temp";
                            if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                            sTemp = sPDF_FullPath + "\\RTOF_" + sNum + ".docx";
                            if (File.Exists(sTemp)) File.Delete(sTemp);

                            File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceTemplate, sTemp);
                            curDoc = WordApp.Documents.Open(sTemp);

                            sEafdss = "<%SL ;;" + fgList[iLine, "AFM"] + ";;;;;;" + sInvType + ";;" + sNum + ";0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "Amount"])) + 
                                      ";0;0;0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "VAT"])) + ";0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "PayAmount"])) + ";" + "EUR" + ";>";

                            curDoc.Content.Find.Execute(FindText: "{title_gr}", ReplaceWith: sInvTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{title_en}", ReplaceWith: sInvTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "Service"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{profile_gr}", ReplaceWith: sProfileGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{profile_en}", ReplaceWith: sProfileEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{invest_profile}", ReplaceWith: sProfile, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{surname}", ReplaceWith: fgList[iLine, "FirstOwner"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{firstname}", ReplaceWith: "", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: iNum, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            dTemp = Convert.ToDateTime(fgList[iLine, "ExecuteDate"]);
                            curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dTemp.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{row1_gr}", ReplaceWith: sInvRow1_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{row1_en}", ReplaceWith: sInvRow1_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "PayAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                            curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                            sNewFiles[0] = sPDF_FullPath + "\\RTOF_" + sNum + ".pdf";
                            curDoc.SaveAs2(sNewFiles[0], Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                            WordApp.ScreenUpdating = false;
                            WordApp.Documents.Close();
                            SendKeys.SendWait("{Enter}");

                            //Global.PrintPDF(sNewFiles[0]);
                            //sNewFiles[0] = sPDF_FullPath + "\\RTOF_" + sNum + "_sig.pdf";
                            //--- sNewFiles[0] file preparation finish is below ---------------------------------


                            fgList[iLine, "InvoiceNum"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();

                            // ---------------------------------------------------------------------------------------------------------------------
                            if (Convert.ToInt16(fgList[iLine, "InvoiceType"]) <= 2)                 // second page is neccecary only for APY (InvoiceType=1)  and TPY (InvoiceType=2)
                            { 
                                sTemp = sPDF_FullPath + "\\RTOA_" + sNum + ".docx";
                                if (File.Exists(sTemp)) File.Delete(sTemp);

                                File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceAnalysisTemplate, sTemp);
                                curDoc = WordApp.Documents.Open(sTemp);

                                curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{custodian}", ReplaceWith: fgList[iLine, "Provider"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "Service"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{invest_profile}", ReplaceWith: fgList[iLine, "Profile"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{surname}", ReplaceWith: fgList[iLine, "FirstOwner"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{firstname}", ReplaceWith: "", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: fgList[iLine, "Country"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{main_title_gr}", ReplaceWith: sMainTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{main_title_en}", ReplaceWith: sMainTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                dTemp = Convert.ToDateTime(fgList[iLine, "ExecuteDate"]);
                                curDoc.Content.Find.Execute(FindText: "{execdate}", ReplaceWith: dTemp.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{APY}", ReplaceWith: fgList[iLine, "InvoiceNum"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{ISIN}", ReplaceWith: fgList[iLine, "ISIN"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{ISINtitle}", ReplaceWith: fgList[iLine, "Description"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{producttype}", ReplaceWith: fgList[iLine, "Product_Category"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{energeiagr}", ReplaceWith: sEnergeiaGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{energeia}", ReplaceWith: sEnergeiaEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{col1_gr}", ReplaceWith: sCol1_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{col1_en}", ReplaceWith: sCol1_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{col3_gr}", ReplaceWith: sCol3_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{col3_en}", ReplaceWith: sCol3_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{quantity}", ReplaceWith: Convert.ToSingle(fgList[iLine, "Quantity"]).ToString("0,0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{currency}", ReplaceWith: fgList[iLine, "Currency"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{price}", ReplaceWith: Convert.ToSingle(fgList[iLine, "Price"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{poso}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "Axia"])).ToString("0,0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row1_gr}", ReplaceWith: sRow1_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row1_en}", ReplaceWith: sRow1_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row2_gr}", ReplaceWith: sRow2_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row2_en}", ReplaceWith: sRow2_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row4_gr}", ReplaceWith: sRow4_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row4_en}", ReplaceWith: sRow4_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row5_gr}", ReplaceWith: sRow5_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row5_en}", ReplaceWith: sRow5_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row6_gr}", ReplaceWith: sRow6_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{row6_en}", ReplaceWith: sRow6_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{metatinekptosi}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "AfterDiscount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{senomismapraksis}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "SeNomismaPraxis"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{isotimiameeur}", ReplaceWith: Convert.ToSingle(fgList[iLine, "RateEUR"]).ToString("0.0000"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{seeur}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "SeEUR"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{minseeur}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "MinSeEUR"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                                curDoc.Content.Find.Execute(FindText: "{pliroteoposo}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "PayAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                                sNewFiles[1] = sPDF_FullPath + "\\RTOA_" + sNum + "_syn.pdf";
                                curDoc.SaveAs2(sNewFiles[1], Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                                //System.Threading.Thread.Sleep(3000);
                                WordApp.ScreenUpdating = false;
                                WordApp.Documents.Close();

                                //--- finish sNewFiles[0] file preparation ---------------------------------
                                //if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sNewFiles[0]);
                                //System.Threading.Thread.Sleep(1000);
                                if (!File.Exists(sNewFiles[0])) sNewFiles[0] = sPDF_FullPath + "\\RTOF_" + sNum + ".pdf";
                                //---------------------------------------------------------------------------

                                sMergeFile = sPDF_FullPath + "\\InvoiceRTO_" + (sSeira + " " + iNum).Trim() + ".pdf";
                                bPDFsMerged = Global.MergePdfFiles(sNewFiles, sMergeFile, Global.UserName, Global.AppTitle, "Invoice", "Invoice", "Invoice");
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(10000);
                                sNewFiles[1] = "";                                                                     // it's second page's file - second page is neccecary only for APY (InvoiceType=1)  and TPY (InvoiceType=2)
                                sMergeFile = sPDF_FullPath + "\\InvoiceRTO_" + (sSeira + " " + iNum).Trim() + ".pdf";  // so smergeFile - isn't MERGED file - it's a only first file's page

                                //--- finish sNewFiles[0] file preparation ---------------------------------
                                //for (i = 0; i <= 50; i++)
                                //    if (!File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.Threading.Thread.Sleep(3000);
                                //    else break;

                                //System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sMergeFile);
                                //---------------------------------------------------------------------------
                            }

                            sFileName = Global.DMS_UploadFile(sMergeFile, "Customers/" + sSubPath + "/Invoices", Path.GetFileName(sMergeFile));

                            //System.Threading.Thread.Sleep(3000);
                            iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sFileName), Convert.ToInt32(fgList[iLine, "Record_ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                            // ---- add/edit record into InvoicesRTO_Details ------------
                            clsInvoicesRTO_Details InvoicesRTO_Details = new clsInvoicesRTO_Details();
                            if (Convert.ToInt32(fgList[iLine, "RTO_ID"]) == 0)
                            {                                
                                InvoicesRTO_Details.Command_ID = Convert.ToInt32(fgList[iLine, "Record_ID"]);
                                InvoicesRTO_Details.InvoiceType = Convert.ToInt32(fgList[iLine, "InvoiceType"]);
                                InvoicesRTO_Details.InvoiceTitles_ID = iID;
                                InvoicesRTO_Details.RealQuantity = Convert.ToSingle(fgList[iLine, "Quantity"]);
                                InvoicesRTO_Details.Curr = "EUR";
                                InvoicesRTO_Details.RealPrice = Convert.ToSingle(fgList[iLine, "Price"]);
                                InvoicesRTO_Details.RealAmount = Convert.ToSingle(fgList[iLine, "Axia"]);
                                InvoicesRTO_Details.FeesPercent = Convert.ToSingle(fgList[iLine, "FeesPercent"]);
                                InvoicesRTO_Details.FeesDiscountPercent = Convert.ToSingle(fgList[iLine, "FeesDiscountPercent"]);
                                InvoicesRTO_Details.FinishFeesPercent = Convert.ToSingle(fgList[iLine, "AfterDiscount"]);
                                InvoicesRTO_Details.FinishFeesAmount = Convert.ToSingle(fgList[iLine, "SeNomismaPraxis"]);
                                InvoicesRTO_Details.FeesRate = Convert.ToSingle(fgList[iLine, "RateEUR"]);
                                InvoicesRTO_Details.FeesAmountEUR = Convert.ToSingle(fgList[iLine, "SeEUR"]);
                                InvoicesRTO_Details.MinFeesAmount = Convert.ToSingle(fgList[iLine, "MinFeesAmount"]);
                                InvoicesRTO_Details.MinFeesDiscountPercent = Convert.ToSingle(fgList[iLine, "MinFeesDiscountPercent"]);
                                InvoicesRTO_Details.MinFeesDiscountAmount = Convert.ToSingle(fgList[iLine, "MinFeesDiscountAmount"]);
                                InvoicesRTO_Details.FinishMinFeesAmount = Convert.ToSingle(fgList[iLine, "MinSeEUR"]);
                                InvoicesRTO_Details.FeesProVAT = Convert.ToSingle(fgList[iLine, "Amount"]);
                                InvoicesRTO_Details.FeesVAT = Convert.ToSingle(fgList[iLine, "VAT"]);
                                InvoicesRTO_Details.CompanyFees = Convert.ToSingle(fgList[iLine, "PayAmount"]);
                                InvoicesRTO_Details.InsertRecord();
                            }
                            else
                            {
                                InvoicesRTO_Details.Record_ID = Convert.ToInt32(fgList[iLine, "RTO_ID"]);
                                InvoicesRTO_Details.GetRecord();
                                InvoicesRTO_Details.InvoiceTitles_ID = iID;
                                InvoicesRTO_Details.EditRecord();
                            }

                            // ---- edit Command.RTO_InvoiceTitle_ID ---------
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.CommandType_ID = 1;
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iLine, "Record_ID"]);
                            klsOrder.GetRecord();
                            klsOrder.RTO_InvoiceTitle_ID = iID;
                            klsOrder.EditRecord();
                            

                            fgList[iLine, 0] = false;
                            fgList[iLine, 1] = 1;
                            fgList[iLine, "FileName"] = Path.GetFileName(sFileName);
                            fgList.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            {
                WordApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (bPDFsMerged)
                {
                    sTemp = sPDF_FullPath + "\\RTOF_" + sNum + ".docx";
                    if (File.Exists(sTemp)) File.Delete(sTemp);

                    sTemp = sPDF_FullPath + "\\RTOF_" + sNum + ".pdf";
                    if (File.Exists(sTemp)) File.Delete(sTemp);

                    sTemp = sPDF_FullPath + "\\RTOF_" + sNum + "_sig.pdf";
                    if (File.Exists(sTemp)) File.Delete(sTemp);

                    sTemp = sPDF_FullPath + "\\RTOA_" + sNum + ".docx";
                    if (File.Exists(sTemp)) File.Delete(sTemp);

                    sTemp = sPDF_FullPath + "\\RTOA_" + sNum + "_syn.pdf";
                    if (File.Exists(sTemp)) File.Delete(sTemp);

                    if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) 
                        File.Delete(sPDF_FullPath + "\\Signature_Processor__sig.pdf");
                }
            }

            bCheckList = true;
        }
        private int SaveRecord(int iRow, int iInvType, string sSeira, int iArithmos, string sInvoiceFile, int iSource_ID, int iContract_ID)
        {
            int iRecord_ID = 0;

            clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
            InvoiceTitles.DateIssued = Convert.ToDateTime(fgList[iRow, "ExecuteDate"]);
            InvoiceTitles.Tipos = iInvType;
            InvoiceTitles.Seira = sSeira;
            InvoiceTitles.Arithmos = iArithmos;
            InvoiceTitles.Selida = "";
            InvoiceTitles.Client_ID = Convert.ToInt32(fgList[iRow, "Client_ID"]);
            InvoiceTitles.TroposApostolis = 0;
            InvoiceTitles.TroposPliromis = 1;
            InvoiceTitles.Posotita = 0;
            InvoiceTitles.AxiaMikti = Convert.ToSingle(fgList[iRow, "Amount"]);
            InvoiceTitles.Ekptosi = 0;
            InvoiceTitles.AxiaKathari = Convert.ToSingle(fgList[iRow, "Amount"]);
            InvoiceTitles.AxiaFPA = Convert.ToSingle(fgList[iRow, "VAT"]);
            InvoiceTitles.AxiaTeliki = Convert.ToSingle(fgList[iRow, "PayAmount"]);
            InvoiceTitles.FileName = sInvoiceFile;
            InvoiceTitles.SourceType = 1;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
            InvoiceTitles.Source_ID = iSource_ID;                                           // Commands.ID
            InvoiceTitles.Contract_ID = iContract_ID;
            InvoiceTitles.OfficialInformingDate = "";
            InvoiceTitles.Author_ID = Global.User_ID;
            InvoiceTitles.DateIns = DateTime.Now;
            iRecord_ID = InvoiceTitles.InsertRecord();

            clsInvoiceRecs InvoiceRecs = new clsInvoiceRecs();
            InvoiceRecs.IT_ID = iRecord_ID;
            InvoiceRecs.Good_Type = 2;                                             // 1-Good, 2- Service
            InvoiceRecs.Good_Code = "";
            InvoiceRecs.Good_Title = "";
            InvoiceRecs.Good_MM = "";
            InvoiceRecs.Price = Convert.ToSingle(fgList[iRow, "Price"]);
            InvoiceRecs.Posotita = Convert.ToSingle(fgList[iRow, "Quantity"]);
            InvoiceRecs.AxiaMikti = Convert.ToSingle(fgList[iRow, "Axia"]);
            InvoiceRecs.EkptosiPercent = 0;
            InvoiceRecs.EkptosiAxia = 0;
            InvoiceRecs.AxiaKathari = Convert.ToSingle(fgList[iRow, "Amount"]);
            InvoiceRecs.FPAPercent = 0;
            InvoiceRecs.FPAAxia = Convert.ToSingle(fgList[iRow, "VAT"]);
            InvoiceRecs.AxiaTeliki = Convert.ToSingle(fgList[iRow, "PayAmount"]);
            InvoiceRecs.InsertRecord();

            return iRecord_ID;
        }
        private void ShowOrder()
        {
            int iRow = 0;
            if (fgList.Row > 0) {
               iRow = fgList.Row;
               if (Convert.ToInt32(fgList[iRow, "BusinessType_ID"]) == 1)
               {
                    frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
                    locOrderSecurity.Rec_ID = Convert.ToInt32(fgList[iRow, "Record_ID"]);                        // Rec_ID <> 0     EDIT mode
                    locOrderSecurity.RightsLevel = 2;
                    locOrderSecurity.Editable = 0;
                    locOrderSecurity.ShowDialog();   
                    if (locOrderSecurity.LastAktion == 1) {
                        DefineList();
                        ShowList();
                    }
                }
                else
                {
                    frmOrderExecution locOrderExecution = new frmOrderExecution();
                    locOrderExecution.Rec_ID = Convert.ToInt32(fgList[iRow, "Record_ID"]);                        // Rec_ID <> 0     EDIT mode
                    locOrderExecution.CommandType_ID = 2;                                                         // 2 - Execution Order, 3 - BulkOrder
                    locOrderExecution.RightsLevel = 2;
                    locOrderExecution.Editable = 0;
                    locOrderExecution.Show();
                }
            }
        }
        private void panEdit_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panEdit.Location = new Point(this.panEdit.Location.X + e.X - this.position.X, this.panEdit.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panEdit_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iLine = 0;
            for (iLine = 2; iLine <= (fgList.Rows.Count - 1); iLine++)
            {
                if ((fgList[iLine, "InvoiceNum"] + "").Trim() != "" && Convert.ToInt32(fgList[iLine, "RTO_ID"]) == 0)
                {
                    clsInvoicesRTO_Details InvoicesRTO_Details = new clsInvoicesRTO_Details();
                    InvoicesRTO_Details.Command_ID = Convert.ToInt32(fgList[iLine, "Record_ID"]);
                    InvoicesRTO_Details.InvoiceType = Convert.ToInt32(fgList[iLine, "InvoiceType"]);
                    InvoicesRTO_Details.InvoiceTitles_ID = Convert.ToInt32(fgList[iLine, "Invoice_Titles_ID"]);
                    InvoicesRTO_Details.FeesPercent = Convert.ToSingle(fgList[iLine, "FeesPercent"]);
                    InvoicesRTO_Details.FeesDiscountPercent = Convert.ToSingle(fgList[iLine, "FeesDiscountPercent"]);
                    InvoicesRTO_Details.FinishFeesPercent = Convert.ToSingle(fgList[iLine, "AfterDiscount"]);
                    InvoicesRTO_Details.FinishFeesAmount = Convert.ToSingle(fgList[iLine, "SeNomismaPraxis"]);
                    InvoicesRTO_Details.FeesRate = Convert.ToSingle(fgList[iLine, "RateEUR"]);
                    InvoicesRTO_Details.FeesAmountEUR = Convert.ToSingle(fgList[iLine, "SeEUR"]);
                    InvoicesRTO_Details.MinFeesAmount = Convert.ToSingle(fgList[iLine, "MinFeesAmount"]);
                    InvoicesRTO_Details.MinFeesDiscountPercent = Convert.ToSingle(fgList[iLine, "MinFeesDiscountPercent"]);
                    InvoicesRTO_Details.MinFeesDiscountAmount = Convert.ToSingle(fgList[iLine, "MinFeesDiscountAmount"]);
                    InvoicesRTO_Details.FinishMinFeesAmount = Convert.ToSingle(fgList[iLine, "MinSeEUR"]);
                    InvoicesRTO_Details.FeesProVAT = Convert.ToSingle(fgList[iLine, "Amount"]);
                    InvoicesRTO_Details.FeesVAT = Convert.ToSingle(fgList[iLine, "VAT"]);
                    InvoicesRTO_Details.CompanyFees = Convert.ToSingle(fgList[iLine, "PayAmount"]);
                    InvoicesRTO_Details.InsertRecord();
                }
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
