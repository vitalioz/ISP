using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Transactions
{
    public partial class frmSecuritiesCheck : Form
    {
        int i = 0, iClient_ID = 0, iShare_ID = 0, iOddEvenBlock = 0, iStyle = 0;
        int iRow, iRightsLevel;
        string sExtra;
        string[] sStatus = { "Δεν ελέγχθηκε", "ΟΚ", "Πρόβλημα" };
        bool bFilter, bCheckList;
        Hashtable imgMap = new Hashtable();
        CellStyle csNotChecked, csProblem;
        CellRange rng;
        public frmSecuritiesCheck()
        {
            InitializeComponent();

            iClient_ID = 0;
            iShare_ID = 0;
            bCheckList = false;
            panCritiries.Visible = true;
        }

        private void frmSecuritiesCheck_Load(object sender, EventArgs e)
        {
            dFrom.Value = DateTime.Now;
            dTo.Value = DateTime.Now;

            dExecFrom.Value = DateTime.Now.AddDays(-30);
            dExecTo.Value = DateTime.Now;

            for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);

            cmbStatus.SelectedIndex = 1;

            //-------------- Define Products List ------------------
            cmbProducts.DataSource = Global.dtProductTypes.Copy();
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";
            cmbProducts.SelectedValue = 0;

            //-------------- Define Products List ------------------
            cmbStockCompanies.DataSource = Global.dtServiceProviders.Copy();
            cmbStockCompanies.DisplayMember = "Title";
            cmbStockCompanies.ValueMember = "ID";
            cmbStockCompanies.SelectedValue = 0;

            //-------------- Define CheckPRoblems List -------------
            clsSystem System = new clsSystem();
            System.GetList_CommandsCheckProblems();
            cmbProblemTypes.DataSource = System.List.Copy();
            cmbProblemTypes.DisplayMember = "Title";
            cmbProblemTypes.ValueMember = "ID";
            cmbProblemTypes.SelectedIndex = 1;

            ucCS.StartInit(700, 400, 200, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 And Contract_ID > 0";
            ucCS.ListType = 2;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextOfLabelChanged);
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 1 ";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = "ΑΑ";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Σύμβαση";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Πάροχος";

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = "Κωδικός";

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = "Portfolio";

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = "Α/Π";

            rng = fgList.GetCellRange(0, 7, 0, 10);
            rng.Data = "Προϊον";

            fgList[1, 7] = "Τύπος";
            fgList[1, 8] = "Τίτλος";
            fgList[1, 9] = "Κωδικός";
            fgList[1, 10] = "ISIN";

            rng = fgList.GetCellRange(0, 11, 0, 12);
            rng.Data = "Εντολή";

            fgList[1, 11] = "Ποσότητα";
            fgList[1, 12] = "Τιμή";

            rng = fgList.GetCellRange(0, 13, 0, 14);
            rng.Data = "Εκτελεσμένη Εντολή";

            fgList[1, 13] = "Ποσότητα";
            fgList[1, 14] = "Τιμή";

            fgList.Cols[15].AllowMerging = true;
            rng = fgList.GetCellRange(0, 15, 1, 15);
            rng.Data = "Νόμισμα";

            fgList.Cols[16].AllowMerging = true;
            rng = fgList.GetCellRange(0, 16, 1, 16);
            rng.Data = "Υπολογισμός προμηθειών";

            fgList.Cols[17].AllowMerging = true;
            rng = fgList.GetCellRange(0, 17, 1, 17);
            rng.Data = "Έλεγχος πινακιδίων";

            fgList.Cols[18].AllowMerging = true;
            rng = fgList.GetCellRange(0, 18, 1, 18);
            rng.Data = "Τύπος προβλήματος";

            fgList.Cols[19].AllowMerging = true;
            rng = fgList.GetCellRange(0, 19, 1, 19);
            rng.Data = "Σχόλιο";

            fgList.Cols[20].AllowMerging = true;
            rng = fgList.GetCellRange(0, 20, 1, 20);
            rng.Data = "Αρχείο";

            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);
            rng.Data = "Reversal Request Mailed";

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = "Ωρα Λήψης";

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = "Ώρα Διαβίβασης";

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = "Ημερομηνία Εκτέλεσης";

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = "Τρόπος Λήψης Εντολης";

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = "Τρόπος Ενημέρωσης";

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "Παρατήρηση";

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = "Διαβιβαστής";

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = "Advisor";

            rng = fgList.GetCellRange(0, 30, 0, 33);
            rng.Data = "Προμήθειες";

            fgList[1, 30] = "%";
            fgList[1, 31] = "Ποσό";
            fgList[1, 32] = "Τελικό %";
            fgList[1, 33] = "Τελικό ποσό";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = "Τζίρο";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Υπηρεσίες";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "Χρηματιστήριο";

            Column clm20 = fgList.Cols["image_map"];
            clm20.ImageMap = imgMap;
            clm20.ImageAndText = false;
            clm20.ImageAlign = ImageAlignEnum.CenterCenter;

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            csNotChecked = fgList.Styles.Add("NotChecked");
            csNotChecked.BackColor = Color.Yellow;

            csProblem = fgList.Styles.Add("CheckPinakidio");
            csProblem.BackColor = Color.LightCoral;


            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 42;

            btnSearch.Left = this.Width - 140;
            fgList.Width = this.Width - 40;
            fgList.Height = this.Height - 220;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 2;

            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            if (chkDateIns.Checked) {
                klsOrder.DateFrom = dFrom.Value;
                klsOrder.DateTo = dTo.Value;
            }
            else
               {
                klsOrder.DateFrom = Convert.ToDateTime("2000/01/01");
                klsOrder.DateTo = Convert.ToDateTime("2070/12/31");
            }
            if (chkDateExec.Checked)
            {
                klsOrder.ExecDateFrom = dExecFrom.Value;
                klsOrder.ExecDateTo = dExecTo.Value;
            }
            else
            {
                klsOrder.ExecDateFrom = Convert.ToDateTime("2000/01/01");
                klsOrder.ExecDateTo = Convert.ToDateTime("2070/12/31");
            }

            klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbStockCompanies.SelectedValue);
            klsOrder.Status = Convert.ToInt32(cmbStatus.SelectedIndex) - 1;
            klsOrder.Client_ID = iClient_ID;
            klsOrder.Product_ID = Convert.ToInt32(cmbProducts.SelectedValue);
            klsOrder.Share_ID = iShare_ID;
            klsOrder.GetPinakidia();

            i = 0;
            foreach (DataRow dtRow in klsOrder.List.Rows)
            {
                bFilter = true;
                if (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("01/01/1900")) bFilter = false;

                if (bFilter)
                {
                    i = i + 1;

                    if (Convert.ToInt32(dtRow["Type"]) == 3 && Convert.ToInt32(dtRow["Parent_ID"]) == 0) {           // if it's scenario first command
                        if (iOddEvenBlock == 1) iOddEvenBlock = 2;                                                   // define odd/even block
                        else iOddEvenBlock = 1;
                        iStyle = iOddEvenBlock;
                    }
                    else if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iStyle = 0;                                   // it's simple command

                    fgList.AddItem(false + "\t" + i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                       (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                       dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + string.Format("{0:#,0.00}", dtRow["Quantity"]) + "\t" + 
                                       Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                       (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                       (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                       dtRow["Currency"] + "\t" + dtRow["FeesNotes"] + "\t" + sStatus[Convert.ToInt32(dtRow["Pinakidio"])] + "\t" + dtRow["CheckProblem_Title"] + "\t" +
                                       dtRow["Check_Notes"] + "\t" + ((dtRow["Check_FileName"] + "") == ""? "0": "1") + "\t" + dtRow["ReversalRequestDate"] + "\t" + 
                                       ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("dd/MM/yyyy") : "") + "\t" +
                                       ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("dd/MM/yyyy") : "") + "\t" +
                                       ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("dd/MM/yyyy") : "") + "\t" + dtRow["RecieveTitle"] + "\t" + dtRow["InformationTitle"] + "\t" +
                                       dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + dtRow["Advisor_Fullname"] + "\t" + dtRow["FeesPercent"] + "\t" +
                                       dtRow["FeesAmount"] + "\t" + dtRow["FinishFeesPercent"] + "\t" + dtRow["FinishFeesAmount"] + "\t" + dtRow["RealAmount"] + "\t" +
                                       dtRow["ServiceTitle"] + "\t" + dtRow["StockExchange_Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                       dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + ((Convert.ToInt32(dtRow["Parent_ID"]) == 0) ? dtRow["ID"] : dtRow["Parent_ID"]) + "\t" +
                                       iStyle + "\t" + dtRow["Share_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Pinakidio"] + "\t" +
                                       dtRow["Check_FileName"] + "\t" + dtRow["Commands_Check_ID"] + "\t" + dtRow["CommandType_ID"] + "\t" + dtRow["Product_ID"]);
                }
            }
            fgList.Redraw = true;
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }

        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                iRow = fgList.Row;
                if (Convert.ToInt32(fgList[iRow, 48]) == 1)
                {
                    frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
                    locOrderSecurity.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                    //locOrderSecurity.BusinessType = iBusinessType_Param;
                    locOrderSecurity.RightsLevel = iRightsLevel;
                    locOrderSecurity.Editable = 1;
                    locOrderSecurity.Mode = 2;                                                        // 2 - SecurutiesCheck
                    locOrderSecurity.ShowDialog();
                    if (locOrderSecurity.LastAktion == 1) {                                           // Aktion=1        was saved (added)
                        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                        klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        klsOrder.CommandType_ID = Convert.ToInt32(fgList[iRow, "CommandType_ID"]);
                        klsOrder.GetRecord();

                        fgList[iRow, 2] = klsOrder.ContractTitle;
                        fgList[iRow, 3] = klsOrder.ServiceProvider_Title;
                        fgList[iRow, 4] = klsOrder.Code;
                        fgList[iRow, 5] = klsOrder.ProfitCenter;
                        fgList[iRow, 6] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                        fgList[iRow, 7] = klsOrder.Product_Title;
                        fgList[iRow, 8] = klsOrder.Security_Title;
                        fgList[iRow, 9] = klsOrder.Security_Code;
                        fgList[iRow, 11] = klsOrder.Quantity.ToString("0.00");
                        fgList[iRow, 12] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                        fgList[iRow, 13] = (locOrderSecurity.txtRealQuantity.Text != "0" ? locOrderSecurity.txtRealQuantity.Text : "");
                        fgList[iRow, 14] = (locOrderSecurity.txtRealPrice.Text != "0" ? locOrderSecurity.txtRealPrice.Text : "");
                        fgList[iRow, 15] = klsOrder.Curr;
                        fgList[iRow, 16] = klsOrder.FeesNotes;
                        fgList[iRow, 17] = sStatus[klsOrder.Pinakidio];
                        fgList[iRow, 20] = klsOrder.LastCheckFile;
                        fgList[iRow, 22] = (klsOrder.RecieveDate.ToString("dd/MM/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(klsOrder.RecieveDate).ToString("dd/MM/yy HH:mm:ss"));
                        if (klsOrder.SentDate == Convert.ToDateTime("1900/01/01")) fgList[iRow, 23] = "";
                        else fgList[iRow, 23] = Convert.ToDateTime(klsOrder.SentDate).ToString("dd/MM/yy HH:mm:ss");
                        fgList[iRow, 24] = (klsOrder.RealPrice == 0 ? "" : Convert.ToDateTime(klsOrder.ExecuteDate).ToString("dd/MM/yy"));
                        fgList[iRow, 25] = klsOrder.RecieveTitle;
                        fgList[iRow, 26] = klsOrder.InformationTitle;
                        fgList[iRow, 27] = klsOrder.Notes;
                        fgList[iRow, 29] = klsOrder.AdvisorName;
                        fgList[iRow, 30] = klsOrder.FeesPercent;
                        fgList[iRow, 31] = klsOrder.FeesAmount;
                        fgList[iRow, 32] = klsOrder.FinishFeesPercent;
                        fgList[iRow, 33] = klsOrder.FinishFeesAmount;
                        fgList[iRow, 36] = klsOrder.StockExchange_Title;
                        fgList[iRow, 38] = klsOrder.Client_ID;
                        fgList[iRow, 39] = klsOrder.ServiceProvider_ID;
                        fgList[iRow, 40] = klsOrder.Status;
                        fgList[iRow, 41] = klsOrder.Security_Share_ID;
                        fgList[iRow, 44] = klsOrder.Contract_ID;
                        fgList[iRow, 45] = klsOrder.Pinakidio;

                        clsOrders_Check Orders_Check = new clsOrders_Check();
                        Orders_Check.Command_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        Orders_Check.GetList();
                        foreach (DataRow dtRow in Orders_Check.List.Rows)
                        {
                            fgList[iRow, 18] = dtRow["ProblemType_Title"];
                            fgList[iRow, 19] = dtRow["Notes"];
                            fgList[iRow, 21] = dtRow["ReversalRequestDate"];
                            break;
                        }
                        fgList.Redraw = true;
                    }
                }
                else {
                    frmOrderExecution locOrderExecution = new frmOrderExecution();
                    locOrderExecution.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                    locOrderExecution.CommandType_ID = 2;                                       // 2 - Execution Order
                    locOrderExecution.RightsLevel = iRightsLevel;
                    locOrderExecution.Editable = 1;
                    locOrderExecution.ShowDialog();
                    if (locOrderExecution.LastAktion == 1)
                    {                                     // Aktion=1        was saved (added)
                        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                        klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        klsOrder.CommandType_ID = Convert.ToInt32(fgList[iRow, "CommandType_ID"]);
                        klsOrder.GetRecord();

                        fgList[iRow, 2] = klsOrder.ClientName;
                        fgList[iRow, 3] = klsOrder.ServiceProvider_Title;
                        fgList[iRow, 4] = klsOrder.Code;
                        fgList[iRow, 5] = klsOrder.ProfitCenter;
                        fgList[iRow, 6] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                        fgList[iRow, 7] = klsOrder.Product_Title;
                        fgList[iRow, 8] = klsOrder.Security_Title;
                        fgList[iRow, 9] = klsOrder.Security_Code;
                        fgList[iRow, 11] = klsOrder.Quantity.ToString("0.00");
                        fgList[iRow, 12] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                        //fgList[iRow, 13] = (locOrderExecution.txtRealPrice.Text != "0" ? locOrderExecution.txtRealPrice.Text : "");
                        //fgList[iRow, 14] = (locOrderExecution.txtRealQuantity.Text != "0" ? locOrderExecution.txtRealQuantity.Text : "");
                        fgList[iRow, 15] = klsOrder.Curr;
                        fgList[iRow, 16] = klsOrder.FeesNotes;
                        fgList[iRow, 17] = sStatus[klsOrder.Pinakidio];
                        fgList[iRow, 20] = klsOrder.LastCheckFile;
                        fgList[iRow, 22] = (klsOrder.RecieveDate.ToString("dd/MM/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(klsOrder.RecieveDate).ToString("dd/MM/yy HH:mm:ss"));
                        if (klsOrder.SentDate == Convert.ToDateTime("1900/01/01")) fgList[iRow, 23] = "";
                        else fgList[iRow, 23] = Convert.ToDateTime(klsOrder.SentDate).ToString("dd/MM/yy HH:mm:ss");
                        fgList[iRow, 24] = (klsOrder.RealPrice == 0 ? "" : Convert.ToDateTime(klsOrder.ExecuteDate).ToString("dd/MM/yy"));
                        fgList[iRow, 25] = klsOrder.RecieveTitle;
                        fgList[iRow, 26] = klsOrder.InformationTitle;
                        fgList[iRow, 27] = klsOrder.Notes;
                        fgList[iRow, 29] = klsOrder.AdvisorName;
                        fgList[iRow, 30] = klsOrder.FeesPercent;
                        fgList[iRow, 31] = klsOrder.FeesAmount;
                        fgList[iRow, 32] = klsOrder.FinishFeesPercent;
                        fgList[iRow, 33] = klsOrder.FinishFeesAmount;
                        fgList[iRow, 36] = klsOrder.StockExchange_Title;
                        fgList[iRow, 38] = klsOrder.Client_ID;
                        fgList[iRow, 39] = klsOrder.ServiceProvider_ID;
                        fgList[iRow, 40] = klsOrder.Status;
                        fgList[iRow, 41] = klsOrder.Security_Share_ID;
                        fgList[iRow, 44] = klsOrder.Contract_ID;
                        fgList[iRow, 45] = klsOrder.Pinakidio;

                        clsOrders_Check Orders_Check = new clsOrders_Check();
                        Orders_Check.Command_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        Orders_Check.GetList();
                        foreach (DataRow dtRow in Orders_Check.List.Rows)
                        {
                            fgList[iRow, 18] = dtRow["ProblemType_Title"];
                            fgList[iRow, 19] = dtRow["Notes"];
                            fgList[iRow, 21] = dtRow["ReversalRequestDate"];
                            break;
                        }
                        fgList.Redraw = true;

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
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1)
            {
                if (e.Col == 45)  {                                                                              // 45 - Pinakidio
                    rng = fgList.GetCellRange(e.Row, 17, e.Row, 17);
                    switch (Convert.ToInt32(fgList[e.Row, 45])) {
                        case 0:
                            rng.Style = csNotChecked;
                            break;
                        case 2:
                            rng.Style = csProblem;
                            break;
                    }
                }
            }
        }
        private void mnuCustomerData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }

        private void mnuShowProduct_Click(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ShareCode_ID"]);
            locProductData.Product_ID = Convert.ToInt32(fgList[fgList.Row, "Product_ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
        }

        private void dFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
                if (dFrom.Value > dTo.Value) dTo.Value = dFrom.Value;
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {
            panEMail.Visible = true;
        }
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            string sTemp = "", sTemp2 = "", sAttachFiles = "";
            clsOrders_Check Orders_Check = new clsOrders_Check();

            for (i = 2; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {
                    sTemp = sTemp + "<tr><td>" + fgList[i, "LastCheckFile"] + " </td><td> - </td><td>" + fgList[i, "Notes"] + "</td></tr>";

                    if (fgList[i, "LastCheckFile"] + "" != "")
                    {
                        sTemp2 = fgList[i, 2] + "";
                        sAttachFiles = sAttachFiles + "C:/DMS/Customers/" + sTemp2.Replace(".", "_") + "/Informing/" + fgList[i, "LastCheckFile"] + "~";
                    }

                    fgList[i, "ReversalRequestDate"] = DateTime.Now.ToString("dd/MM/yyyy");

                    if (fgList[i, "Commands_Check_ID"]+"" != "") {
                        Orders_Check = new clsOrders_Check();
                        Orders_Check.Record_ID = Convert.ToInt32(fgList[i, "Commands_Check_ID"]);
                        Orders_Check.GetRecord();
                        Orders_Check.ReversalRequestDate = fgList[i, "ReversalRequestDate"] + "";
                        Orders_Check.EditRecord();
                    }
                }
            }

            sTemp = sTemp.Replace("\n", "<br/>") + "</table><br/><br/><br/>";
            sTemp = sTemp + Global.UserName + "<br/><br/>" +
                            "<strong>HellasFin</strong><br/>" +
                            "<strong>Global Wealth Management</strong><br/><br/>" +
                            "90, 26th Oktovriou Str. Office 507<br/>" +
                            "P.C.546 27, Thessaloniki, Greece<br/>" +
                            "T. +30 2310 517800<br/>" +
                            "F. +30 2310 515053<br/>" +
                            "E. " + Global.UserEMail + "<br/>" +
                            "W.www.hellasfin.gr</p>";

            Global.AddInformingRecord(0, 0, 5, 1, 0, 0, txtEMail.Text, "", txtThema.Text, sTemp, "", sAttachFiles, "", 0, 0, "");                      // 5 - e-mail
            panEMail.Visible = false;
        }
        private void btnCancelMail_Click(object sender, EventArgs e)
        {
            panEMail.Visible = false;
        }
        private void dTo_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
                if (dFrom.Value > dTo.Value) dFrom.Value = dTo.Value;
        }
        private void dExecFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
                if (dExecFrom.Value > dExecTo.Value) dExecTo.Value = dExecFrom.Value;
        }
        private void dExecTo_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
                if (dExecFrom.Value > dExecTo.Value) dExecFrom.Value = dExecTo.Value;
        }
        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            dFrom.Value = DateTime.Now;
            dTo.Value = DateTime.Now;

            dExecFrom.Value = DateTime.Now.AddDays(-30);
            dExecTo.Value = DateTime.Now;

            iClient_ID = 0;
            iShare_ID = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            lnkPelatis.Text = "";
            ucCS.ShowClientsList = true;
            cmbStockCompanies.SelectedValue = 0;
            cmbStatus.SelectedIndex = 1;
            cmbProducts.SelectedValue = 0;
            ucPS.txtShareTitle.Text = "";
            lnkISIN.Text = "";
            lnkShareTitle.Text = "";

            fgList.Rows.Count = 2;
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            lnkPelatis.Text = stContract.ContractTitle;
        }
        protected void ucPS_TextOfLabelChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            lnkISIN.Text = stProduct.ISIN;
            lnkShareTitle.Text = stProduct.Title;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
