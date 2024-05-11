using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmOrderDPM : Form
    {
        int i, iRec_ID, iDPM_ID, iCommandType_ID, iBusinessType, iLastAktion, iBulcCommand_ID, iBulcCommand2_ID, iStatus, iManager_ID, iProductStockExchange_ID, iStockExchange_ID, 
            iProduct_ID, iProductCategory_ID, iShare_ID, iChild_ID, iRightsLevel, iEditable, iFeesCalcMode;
        string sTemp, sTemp1, sBulkCommand, sMessage, sNewFileName;
        decimal sgTemp, sgTemp1, sgTemp2, decRealPrice, decRealQuantity, decRealAmount;
        float fltAllocationPercent;
        bool bContinue;
        DateTime dTemp, dRecieved;
        DataView dtView;
        SortedList lstRecieved = new SortedList();
        CellRange rng;
        clsOrdersSecurity klsOrder = new clsOrdersSecurity();

        #region --- Start -----------------------------------------------------------------------
        public frmOrderDPM()
        {
            InitializeComponent();

            this.Width = 936;
            this.Height = 812;

            panEMail.Left = 200;
            panEMail.Top = 360;

            panAddClients.Left = 24;
            panAddClients.Top = 310;

            panEdit.Left = 260;
            panEdit.Top = 534;
        }

        private void frmOrderDPM_Load(object sender, EventArgs e)
        {
            this.Text = "Εντολή (" + iRec_ID + ")";

            iLastAktion = 0;
            iBulcCommand_ID = 0;
            iBulcCommand2_ID = 0;
            dRecieved = Convert.ToDateTime("1900/01/01");          

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 1 ";

            //-------------- Define Senders List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Sender = 1 AND Aktive = 1";
            cmbSenders.DataSource = dtView;
            cmbSenders.DisplayMember = "Title";
            cmbSenders.ValueMember = "ID";
            cmbSenders.SelectedValue = 0;

            //----- initialize StockExchanges List -------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";                    //Code = Title / MIC
            cmbStockExchanges.ValueMember = "ID";
            cmbStockExchanges.SelectedValue = 0;

            dSend.Value = Convert.ToDateTime("1900/01/01");
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;
            dSend.Enabled = false;
            txtSendHour.Enabled = false;
            txtSendMinute.Enabled = false;
            txtSendSecond.Enabled = false;

            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            //----- initialize SettlementProviders List -------
            cmbServiceProvider.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProvider.DisplayMember = "Title";
            cmbServiceProvider.ValueMember = "ID";
            cmbServiceProvider.SelectedValue = 0;

            //------- fgRecieved ----------------------------
            fgRecieved.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRecieved.Styles.ParseString(Global.GridStyle);
            fgRecieved.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellChanged);
            fgRecieved.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellButtonClick);

            Column col2 = fgRecieved.Cols[2];
            col2.Name = "Image";
            col2.DataType = typeof(String);
            col2.ComboList = "...";

            //-------------- Define Recieve Methods List ------------------
            lstRecieved.Clear();
            foreach (DataRow dtRow in Global.dtRecieveMethods.Rows)
                lstRecieved.Add(dtRow["ID"], dtRow["Title"]);
            fgRecieved.Cols[1].DataMap = lstRecieved;

            //------- fgSingleOrders ----------------------------
            fgSingleOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSingleOrders.Styles.ParseString(Global.GridStyle);
            fgSingleOrders.DrawMode = DrawModeEnum.OwnerDraw;
            fgSingleOrders.ShowCellLabels = true;
            fgSingleOrders.DoubleClick += new System.EventHandler(fgSingleOrders_DoubleClick);

            fgSingleOrders.Styles.Normal.WordWrap = true;
            fgSingleOrders.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSingleOrders.Rows[0].AllowMerging = true;
            fgSingleOrders.Cols[0].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 0, 1, 0);
            rng.Data = "ΑΑ";

            fgSingleOrders.Cols[1].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 1, 1, 1);
            rng.Data = "Εντολέας";

            fgSingleOrders.Cols[2].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 2, 1, 2);
            rng.Data = "Σύμβαση";

            fgSingleOrders.Cols[3].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 3, 1, 3);
            rng.Data = "Κωδικός";

            fgSingleOrders.Cols[4].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 4, 1, 4);
            rng.Data = "Portfolio";

            fgSingleOrders.Cols[5].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 5, 1, 5);
            rng.Data = "Ημερομηνία Εκτέλεσης";

            rng = fgSingleOrders.GetCellRange(0, 6, 0, 8);
            rng.Data = Global.GetLabel("order");

            fgSingleOrders[1, 6] = Global.GetLabel("price");
            fgSingleOrders[1, 7] = Global.GetLabel("quantity");
            fgSingleOrders[1, 8] = Global.GetLabel("amount");

            rng = fgSingleOrders.GetCellRange(0, 9, 0, 11);
            rng.Data = Global.GetLabel("executed_command");

            fgSingleOrders[1, 9] = Global.GetLabel("price");
            fgSingleOrders[1, 10] = Global.GetLabel("quantity");
            fgSingleOrders[1, 11] = Global.GetLabel("amount");

            //---- Start Initialisation - Show Command --------------

            if (iRec_ID != 0) {
                this.Width = 936;
                this.Height = 812;

                klsOrder.Record_ID = iRec_ID;
                klsOrder.GetRecord();

                switch (Convert.ToInt32(klsOrder.Aktion)) {
                    case 1:
                        pan1.BackColor = Color.MediumAquamarine;
                        pan2.BackColor = Color.MediumAquamarine;
                        pan3.BackColor = Color.MediumAquamarine;
                        pan4.BackColor = Color.MediumAquamarine;
                        pan5.BackColor = Color.MediumAquamarine;
                        pan6.BackColor = Color.MediumAquamarine;
                        break;
                    case 2:
                        pan1.BackColor = Color.LightCoral;
                        pan2.BackColor = Color.LightCoral;
                        pan3.BackColor = Color.LightCoral;
                        pan4.BackColor = Color.LightCoral;
                        pan5.BackColor = Color.LightCoral;
                        pan6.BackColor = Color.LightCoral;
                        break;
                    case 3:
                        pan1.BackColor = Color.Silver;
                        pan2.BackColor = Color.Silver;
                        pan3.BackColor = Color.Silver;
                        pan4.BackColor = Color.Silver;
                        pan5.BackColor = Color.Silver;
                        pan6.BackColor = Color.Silver;
                        break;
                }

                sBulkCommand = klsOrder.BulkCommand.Replace("<", "").Replace(">", "");
                if (sBulkCommand.Length > 0) {
                    string[] tokens = sBulkCommand.Split('/');
                    if (tokens.Length > 0) {
                        iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                        if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                    }
                }

                iDPM_ID = Convert.ToInt32(klsOrder.II_ID);
                iBusinessType = Convert.ToInt32(klsOrder.BusinessType_ID);
                iManager_ID = Convert.ToInt32(klsOrder.Company_ID);                                     // it's DPM-command, so in Company_ID was saved Manager_ID
                cmbServiceProvider.SelectedValue = Convert.ToInt32(klsOrder.ServiceProvider_ID);
                iStockExchange_ID = klsOrder.StockExchange_ID;
                lblPelatis.Text = klsOrder.CompanyTitle;
                fltAllocationPercent = klsOrder.AllocationPercent;
                txtAction.Text = klsOrder.Aktion == 1? "BUY": "SELL";
                dAktionDate.Value = klsOrder.AktionDate;
                iProduct_ID = klsOrder.Product_ID;
                lblProduct.Text = klsOrder.Product_Title;
                iProductCategory_ID = klsOrder.ProductCategory_ID;
                lblProductCategory.Text = klsOrder.ProductCategory_Title;
                lblProductStockExchange_Title.Text = klsOrder.ProductStockExchange_Title;
                iShare_ID = klsOrder.Share_ID;
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = klsOrder.Security_Code;
                ucPS.ShowProductsList = true;
                lnkISIN.Text = klsOrder.Security_ISIN;
                lblShareTitle.Text = klsOrder.Security_Title;
                cmbConstant.SelectedIndex = klsOrder.Constant;
                dConstant.Text = klsOrder.ConstantDate;
                txtPrice.Text = klsOrder.Price.ToString("0.00##");
                txtQuantity.Text = klsOrder.Quantity.ToString("0.00######");
                txtAmount.Text = klsOrder.Amount.ToString("0.00##");
                lblCurr.Text = klsOrder.Curr;

                if (Convert.ToDateTime(klsOrder.SentDate) != Convert.ToDateTime("1900/01/01")) {              
                    dTemp = Convert.ToDateTime(klsOrder.SentDate);          
                    dSend.Value = dTemp;
                    dSend.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                    txtSendHour.Text = dTemp.Hour.ToString();
                    txtSendMinute.Text = dTemp.Minute.ToString();
                    txtSendSecond.Text = dTemp.Second.ToString();
                    cbChecked.Checked = (klsOrder.SendCheck == 0 ? false : true);

                    dSend.Enabled = true;
                    txtSendHour.Enabled = true;
                    txtSendMinute.Enabled = true;
                    txtSendSecond.Enabled = true;
                }

                if (Convert.ToDateTime(klsOrder.ExecuteDate) != Convert.ToDateTime("1900/01/01"))                           // ExecuteDate <> "1900/01/01" - order was executed
                    btnSend.Enabled = false;
                else 
                    btnSend.Enabled = true;

                if (klsOrder.ExecuteDate != Convert.ToDateTime("01/01/1900")) {
                  
                    this.Height = 814;

                    dTemp = klsOrder.ExecuteDate;
                    dExecute.Text = dTemp.ToString("dd/MM/yyyy");   
                    dExecute.CustomFormat = "dd/MM/yyyy";
                    txtExecuteHour.Text = dTemp.Hour.ToString();
                    txtExecuteMinute.Text = dTemp.Minute.ToString();
                    txtExecuteSecond.Text = dTemp.Second.ToString();
                    cmbStockExchanges.SelectedValue = iStockExchange_ID;
                    pan1.Enabled = false;

                    dExecute.Enabled = true;
                    txtExecuteHour.Enabled = true;
                    txtExecuteMinute.Enabled = true;
                    txtExecuteSecond.Enabled = true;

                    txtRealPrice.Enabled = true;
                    txtRealQuantity.Enabled = true;
                    txtAccruedInterest.Enabled = true;
                    txtRealAmount.Enabled = true;

                    btnExecuted.Enabled = false;
                    panExecute.Enabled = true;

                    tsbEMail.Enabled = true;
                }
                else {
                    dExecute.Value = Convert.ToDateTime("1900/01/01");
                    dExecute.CustomFormat = "          ";
                    dExecute.Format = DateTimePickerFormat.Custom;
                    txtExecuteHour.Text = "";
                    txtExecuteMinute.Text = "";
                    txtExecuteSecond.Text = "";

                    panExecute.Enabled = false;
                    tsbEMail.Enabled = false;
                }

                dRecieved = klsOrder.RecieveDate;

                decRealPrice = klsOrder.RealPrice;
                decRealQuantity = klsOrder.RealQuantity;
                decRealAmount = klsOrder.RealAmount;

                if (Convert.ToDateTime(klsOrder.ExecuteDate) == Convert.ToDateTime("1900/01/01")) btnExecuted.Enabled = true;
                else  btnExecuted.Enabled = false;

                txtRealQuantity.Text = string.Format("{0:#,0.#######}", klsOrder.RealQuantity);   
                txtAccruedInterest.Text = string.Format("{0:#,0.#######}", klsOrder.AccruedInterest);  
                txtRealPrice.Text = string.Format("{0:#,0.#######}", klsOrder.RealPrice);  
                txtRealAmount.Text = string.Format("{0:#,0.#######}", klsOrder.RealAmount);
                txtAccruedInterest.Text = string.Format("{0:#,0.#######}", klsOrder.AccruedInterest);
                lblInvestAmount.Text = string.Format("{0:#,0.####}", klsOrder.RealAmount + klsOrder.AccruedInterest);
                lblCurrRate.Text = string.Format("{0:#,0.####}", klsOrder.CurrRate);

                txtNotes.Text = klsOrder.Notes;
                lstType.SelectedIndex = klsOrder.PriceType;
                if (lstType.SelectedIndex == 1) txtPrice.Text = "M";              // 1 - Market
                    
                iFeesCalcMode = klsOrder.FeesCalcMode;
                cmbSenders.SelectedValue = klsOrder.User_ID;

                lnkDivision.Visible = false;
                switch (klsOrder.Product_ID) {
                    case 1:
                        lblQuantity.Text = "Τεμάχια";
                        break;
                    case 2:                  // Bond (Omologa)
                        lblQuantity.Text = "Ονομ.Αξία";
                        break;
                    case 4:                 // ETF (DAK)
                        lblQuantity.Text = "Τεμάχια";
                        break;
                    case 6:                 // FUND (AK)
                        lblQuantity.Text = "Μερίδια";
                        lnkDivision.Visible = true;
                        break;                
                }

                if (klsOrder.Status >= 0) {
                    tslCancel.Text = "Ακύρωση εντολής";
                    sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί η εντολή.\n\n Είστε σίγουρος για την ακύρωση της;";
                    iStatus = -1;
                }
                else {
                    tslCancel.Text = "Επαναφορά εντολής";
                    tslCancel.Enabled = true;
                    tsbSave.Enabled = false;
                    btnSend.Enabled = false;
                    btnExecuted.Enabled = false;
                    sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να επαναφερθεί η εντολή.\n\n Είστε σίγουρος για την επαναφορά της;";
                    iStatus = 0;
                }

                if (klsOrder.Product_ID == 0 || klsOrder.ProductCategory_ID == 0) {
                    //-------  Define Product_ID or ProductCategory_ID --------------------
                    clsProductsCodes klsProductCode = new clsProductsCodes();
                    klsProductCode.Record_ID = klsOrder.Security_Share_ID;
                    klsProductCode.GetRecord();
                    klsOrder.Product_ID = klsProductCode.Product_ID;
                    klsOrder.ProductCategory_ID = klsProductCode.ProductCategory_ID;

                    iProduct_ID = klsProductCode.Product_ID;
                    iProductCategory_ID = klsProductCode.ProductCategory_ID;
                }

                //------------- Define Recieved Files List ------------------
                clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = iRec_ID;
                klsOrder2.GetRecievedFiles();

                fgRecieved.Redraw = false;
                fgRecieved.Rows.Count = 1;
                foreach(DataRow dtRow in klsOrder2.List.Rows)
                    fgRecieved.AddItem(dtRow["DateIns"] + "\t" +dtRow["Method_Title"] + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" +dtRow["Method_ID"] + "\t" +"");   //drList("FilePath")

                fgRecieved.Redraw = true;

                //--- Define Allocation list -------------------------------------
                fgSingleOrders.Redraw = false;
                fgSingleOrders.Rows.Count = 2;

                if (fltAllocationPercent > 0) {

                    if (iBulcCommand2_ID != 0)                                                      // iBulcCommand2_ID != 0 means that Allocation records are in Commands table 
                        DefineAllocation_From_Commands();
                                        
                    if (fgSingleOrders.Rows.Count == 2 && iDPM_ID > 0) {                          // if from Commands table wasn't found any Allocation record
                        i = 0;                                                                      // let's find Allocation records into DPMOrders_Recs table 
                        sgTemp = 0;
                        sgTemp1 = 0;
                        clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                        OrdersDPM_Recs.DPM_ID = iDPM_ID;
                        OrdersDPM_Recs.GetList();
                        foreach (DataRow dtRow in OrdersDPM_Recs.List.Rows)
                        {
                            i = i + 1;
                            fgSingleOrders.AddItem(i + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                                         dAktionDate.Value.ToString("dd/MM/yyyy") + "\t" + dtRow["Price"] + "\t" + dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" +
                                                         "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "1");

                            if (Global.IsNumeric(dtRow["Quantity"] + "")) sgTemp = sgTemp + Convert.ToDecimal(dtRow["Quantity"]);
                            if (Global.IsNumeric(dtRow["Amount"] + "")) sgTemp1 = sgTemp1 + Convert.ToDecimal(dtRow["Amount"]);
                        }
                        
                        lblQuantity_Sum.Text = sgTemp.ToString();
                        lblAmount_Sum.Text = sgTemp1.ToString();
                    }
                }
                fgSingleOrders.Redraw = true;
                DefineSums();

                if (iRightsLevel < 2) {
                    tslCancel.Enabled = false;
                    tsbSave.Enabled = false;
                }
                else
                {
                    if (iEditable == 0) {
                        tslCancel.Enabled = false;
                        tsbSave.Enabled = false;
                    }
                }

                if (iBusinessType == 2) {
                    lblWarning.Text = "Διαβίβαση και Εκτέλεση αυτης της εντολής γίνετε απο Basket";
                    panWarning.Visible = true;
                    pan4.Enabled = false;
                    pan5.Enabled = false;
                }
                else {
                    lblWarning.Text = "";
                    panWarning.Visible = false;
                    pan4.Enabled = true;
                    pan5.Enabled = true;
                }

                txtNotes.Focus();
            }
            else {
                this.Width = 936;
                this.Height = 336;
                dAktionDate.Value = DateTime.Now;
                lstType.SelectedIndex = 0;
                lstType.Enabled = true;
                lblPelatis.Text = "HellasFin";
                cmbConstant.SelectedIndex = 0;
                txtAction.Enabled = true;
                txtAction.Focus();
            }

            btnExecuted.Enabled = true;

            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Refresh();
        }
        #endregion
        #region --- Top toolbar -----------------------------------------------------------------
        private void tslCancel_Click(object sender, EventArgs e)
        {
            int j = 0;
            clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();

            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)  {
                klsOrder.BulkCommand = "";
                klsOrder.ExecuteDate = Convert.ToDateTime("1900/01/01 00:00:00");
                klsOrder.RealPrice = 0;
                klsOrder.RealQuantity = 0;
                klsOrder.RealAmount = 0;
                klsOrder.SentDate = Convert.ToDateTime("1900/01/01 00:00:00");
                klsOrder.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);
                klsOrder.Status = iStatus;
                klsOrder.EditRecord();

                for (j = 2; j <= fgSingleOrders.Rows.Count - 1; j++) {
                    if (Convert.ToInt32(fgSingleOrders[j, 12]) != 0) {
                        klsOrder2 = new clsOrdersSecurity();
                        klsOrder2.Record_ID = Convert.ToInt32(fgSingleOrders[j, 12]);
                        klsOrder2.CommandType_ID = 1;
                        klsOrder2.GetRecord();
                        klsOrder2.BulkCommand = "";
                        klsOrder2.ExecuteDate = Convert.ToDateTime("1900/01/01 00:00:00");
                        klsOrder2.RealPrice = 0;
                        klsOrder2.RealQuantity = 0;
                        klsOrder2.RealAmount = 0;
                        klsOrder2.SentDate = Convert.ToDateTime("1900/01/01 00:00:00");
                        klsOrder2.EditRecord();
                    }
                }

                iLastAktion = 1;             // was saved (cancel)
                this.Close();
            }
        }
        private void tsbCopyID_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(iRec_ID + "");
        }
        private void tsbHistory_Click(object sender, EventArgs e)
        {
            frmShowHistory locShowHistory = new frmShowHistory();
            locShowHistory.RecType = 10;                                                     // 10 - OrdersSecurity
            locShowHistory.SrcRec_ID = iRec_ID;
            locShowHistory.ShowDialog();
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            int i = 0, j = 0, k = 0, iRecieveMethod_ID = 0;

            bContinue = true;
            sgTemp = 0;
            if (Convert.ToDecimal(lblAmount_Sum.Text) != 0) {
                sgTemp = Math.Abs((Convert.ToDecimal(lblAmount_Sum.Text) - Convert.ToDecimal(lblRealAmount_Sum.Text)) / Convert.ToDecimal(lblAmount_Sum.Text));
            }
            if ((sgTemp != 0) && (sgTemp < (Convert.ToDecimal(0.05) * Convert.ToDecimal(lblAmount_Sum.Text))))
            {
                if (txtPrice.Text == "M") txtPrice.Text = "0";
                if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0)
                {
                    bContinue = false;
                    txtPrice.BackColor = Color.Red;
                    txtPrice.Focus();
                }
                else
                {
                    if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0)
                    {
                        bContinue = false;
                        txtQuantity.BackColor = Color.Red;
                        txtQuantity.Focus();
                    }
                    else
                    {
                        if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0)
                        {
                            bContinue = false;
                            txtAmount.BackColor = Color.Red;
                            txtAmount.Focus();
                        }
                    }
                }
            }
            else {
                if (Convert.ToDecimal(lblAmount_Sum.Text) == 0)
                    if (fgSingleOrders.Rows.Count > 2) { 
                       bContinue = false;
                       if (MessageBox.Show("Wrong Sums !!! \n\n Να αποθηκευτούν τα στοιχεία της εντολής;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                           bContinue = true;
                    }
            }

            if (bContinue) {
                //DefineComission()
                if (iRec_ID > 0) {
                    //--- Edit DPM Order ----------------------------------
                    klsOrder.Record_ID = iRec_ID;
                    klsOrder.BusinessType_ID = iBusinessType;
                    klsOrder.Client_ID = klsOrder.Client_ID;
                    klsOrder.Company_ID = iManager_ID;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbServiceProvider.SelectedValue);
                    if (!Global.IsNumeric(lblQuantity_Sum.Text)) lblQuantity_Sum.Text = "0";
                    if (Convert.ToSingle(txtQuantity.Text) != 0)
                         klsOrder.AllocationPercent = Convert.ToSingle(lblQuantity_Sum.Text) * 100 / Convert.ToSingle(txtQuantity.Text);
                    else klsOrder.AllocationPercent = 0;
                    klsOrder.Aktion = txtAction.Text == "BUY" ? 1 : 2; 
                    klsOrder.AktionDate = dAktionDate.Value;
                    klsOrder.PriceType = lstType.SelectedIndex;
                    if (Global.IsNumeric(txtPrice.Text)) klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                    else klsOrder.Price = 0;
                    klsOrder.Quantity = Convert.ToDecimal(txtQuantity.Text);
                    klsOrder.Amount = Convert.ToDecimal(txtAmount.Text);
                    klsOrder.Curr = lblCurr.Text;
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "";
                    klsOrder.Pinakidio = 0;
                    klsOrder.FeesNotes = "";
                    klsOrder.FeesPercent = 0;
                    klsOrder.FeesAmount = 0;
                    klsOrder.FeesDiscountPercent = 0;
                    klsOrder.FeesDiscountAmount = 0;
                    klsOrder.FinishFeesPercent = 0;
                    klsOrder.FinishFeesAmount = 0;
                    klsOrder.TicketFee = 0;
                    klsOrder.TicketFeeDiscountPercent = 0;
                    klsOrder.TicketFeeDiscountAmount = 0;
                    klsOrder.FinishTicketFee = 0;
                    klsOrder.CompanyFeesPercent = 0;
                    klsOrder.RecieveMethod_ID = 0;
                    if (dSend.Text.Trim() != "")  {
                        dTemp = Convert.ToDateTime(dSend.Text);
                        sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtSendHour.Text.Trim() == "" ? "00" : txtSendHour.Text.Trim()) + ":" +
                                             (txtSendMinute.Text.Trim() == "" ? "00" : txtSendMinute.Text.Trim()) + ":" +
                                             (txtSendSecond.Text.Trim() == "" ? "00" : txtSendSecond.Text.Trim());

                    }
                    else sTemp = "1900/01/01 00:00:00";
                    klsOrder.SentDate = Convert.ToDateTime(sTemp);
                    klsOrder.SendCheck = cbChecked.Checked ? 1 : 0;

                    if (dExecute.Text.Trim() != "") {
                        dTemp = dExecute.Value;
                        sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                                                                     (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                                                                     (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());

                        if (Convert.ToInt32(cmbStockExchanges.SelectedValue) != 0)
                            klsOrder.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                    }
                    else sTemp = "1900/01/01 00:00:00";
                    klsOrder.ExecuteDate = Convert.ToDateTime(sTemp);

                    dTemp = dRecieved;
                    i = 0;
                    if (fgRecieved.Rows.Count > 1)
                    {
                        dTemp = Convert.ToDateTime(fgRecieved[1, 0]);        // last recieved file date
                        i = Convert.ToInt32(fgRecieved[1, 4]);               // last recieved file method
                    }
                    klsOrder.RecieveDate = dTemp;
                    klsOrder.RecieveMethod_ID = i;


                    decRealPrice = Convert.ToDecimal(txtRealPrice.Text);
                    klsOrder.RealPrice = decRealPrice;
                    decRealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
                    klsOrder.RealQuantity = decRealQuantity;
                    decRealAmount = Convert.ToDecimal(txtRealAmount.Text);
                    klsOrder.RealAmount = decRealAmount;
                    klsOrder.ProviderFees = 0;
                    klsOrder.FeesDiff = 0;
                    klsOrder.FeesMarket = 0;
                    klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);

                    klsOrder.InformationMethod_ID = 0;
                    klsOrder.Notes = txtNotes.Text;
                    klsOrder.FeesCalcMode = iFeesCalcMode;
                    klsOrder.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);
                    klsOrder.EditRecord();
                }
                
                //--- saves fgRecieved records ------------------------------------------------------------------------------------------------------------------------
                for (i = 1; i <= fgRecieved.Rows.Count - 1; i++)  {

                    sNewFileName = fgRecieved[i, 2] + "";

                    if ((fgRecieved[i, "FilePath"] + "") != "") {
                        sNewFileName = Global.DMS_UploadFile(fgRecieved[i, "FilePath"] + "", "Customers/OrdersAcception", sNewFileName);
                        if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                        else MessageBox.Show("Αρχείο " + fgRecieved[i, 2] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
                    Orders_Recieved.Command_ID = iRec_ID;
                    Orders_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, "RecieveDate"]);
                    Orders_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, "RecieveMethod_ID"]);
                    Orders_Recieved.FilePath = fgRecieved[i, "FilePath"] + "";
                    Orders_Recieved.FileName = sNewFileName;

                    if (Convert.ToInt32(fgRecieved[i, "ID"]) == 0) {
                        Orders_Recieved.SourceCommand_ID = iRec_ID;
                        Orders_Recieved.InsertRecord();
                    }
                    else {
                        Orders_Recieved.Record_ID = Convert.ToInt32(fgRecieved[i, "ID"]);
                        Orders_Recieved.EditRecord();
                    }
                }


                //--- start Save Allocation data ------------------------------------------------------------------------------
                if (fgSingleOrders.Rows.Count == 2) { 
                    Global.SyncDPM_SingleOrder(iRec_ID, Convert.ToDecimal(txtRealPrice.Text), Convert.ToDecimal(txtRealQuantity.Text));
                }
                else { 
                    //--- define BulkCommand  ---------------------------------------------------------------------------------
                    if (iBulcCommand2_ID == 0)
                    {
                        clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                        iBulcCommand2_ID = klsOrder2.GetNextBulkCommand();
                    }
                    sBulkCommand = "<" + iBulcCommand_ID + ">/<" + iBulcCommand2_ID + ">";
                    klsOrder.BulkCommand = sBulkCommand;
                    klsOrder.EditRecord();


                    //--- define RecieveMethod_ID -----------------------------------------------------------------------------
                    iRecieveMethod_ID = 0;
                    if (fgRecieved.Rows.Count > 1)
                    {
                        iRecieveMethod_ID = Convert.ToInt32(fgRecieved[1, 4]);
                        dTemp = Convert.ToDateTime(fgRecieved[1, 0]);
                    }
                    else dTemp = DateTime.Now;

                    //--- save Allocation records ----------------------------------------------------------------------------
                    clsOrdersSecurity klsSimpleOrder = new clsOrdersSecurity();
                    for (j = 2; j <= fgSingleOrders.Rows.Count - 1; j++)
                    {
                        fgSingleOrders[j, "RealPrice"] = txtRealPrice.Text;
                        if (Global.IsNumeric(fgSingleOrders[j, "RealPrice"]) && Global.IsNumeric(fgSingleOrders[j, "RealQuantity"]))
                            fgSingleOrders[j, "RealAmount"] = Convert.ToDecimal(fgSingleOrders[j, "RealPrice"]) * Convert.ToDecimal(fgSingleOrders[j, "RealQuantity"]);

                        if (Convert.ToInt32(fgSingleOrders[j, "ID"]) == 0)
                        {
                            clsContracts klsContract = new clsContracts();
                            klsContract.Code = fgSingleOrders[j, 3] + "";
                            klsContract.Portfolio = fgSingleOrders[j, 4] + "";
                            klsContract.GetRecord_Code_Portfolio();

                            fgSingleOrders[j, 2] = klsContract.ContractTitle;
                            klsSimpleOrder.BulkCommand = "<" + iBulcCommand2_ID + ">";
                            klsSimpleOrder.BusinessType_ID = 1;
                            klsSimpleOrder.CommandType_ID = 1;
                            klsSimpleOrder.Client_ID = klsContract.Client_ID;
                            klsSimpleOrder.Company_ID = Global.Company_ID;
                            klsSimpleOrder.ServiceProvider_ID = klsOrder.ServiceProvider_ID;
                            klsSimpleOrder.StockExchange_ID = klsOrder.StockExchange_ID;
                            klsSimpleOrder.CustodyProvider_ID = klsOrder.ServiceProvider_ID;
                            klsSimpleOrder.Depository_ID = klsOrder.Depository_ID;
                            klsSimpleOrder.II_ID = 0;
                            klsSimpleOrder.Parent_ID = 0;
                            klsSimpleOrder.Contract_ID = klsContract.Record_ID;
                            klsSimpleOrder.CFP_ID = klsContract.Packages.CFP_ID;
                            klsSimpleOrder.Contract_Details_ID = klsContract.Contract_Details_ID;
                            klsSimpleOrder.Contract_Packages_ID = klsContract.Contract_Packages_ID;
                            klsSimpleOrder.Code = klsContract.Code;
                            klsSimpleOrder.ProfitCenter = klsContract.Portfolio;
                            klsSimpleOrder.AllocationPercent = 100;                                               // 100 - because it's SingleOrder
                            klsSimpleOrder.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                            klsSimpleOrder.AktionDate = Convert.ToDateTime(fgSingleOrders[j, 5]);
                            klsSimpleOrder.Share_ID = klsOrder.Share_ID;
                            klsSimpleOrder.Product_ID = klsOrder.Product_ID;
                            klsSimpleOrder.ProductCategory_ID = klsOrder.ProductCategory_ID;
                            klsSimpleOrder.PriceType = klsOrder.PriceType;
                            klsSimpleOrder.Price = (!Global.IsNumeric(txtPrice.Text) ? 0 : Convert.ToDecimal(txtPrice.Text));
                            klsSimpleOrder.Quantity = (!Global.IsNumeric(fgSingleOrders[j, "Quantity"]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, "Quantity"]));
                            klsSimpleOrder.Amount = klsSimpleOrder.Price * klsSimpleOrder.Quantity;   // (!Global.IsNumeric(fgSingleOrders[j, "Amount"]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, "Amount"]));
                            klsSimpleOrder.Curr = lblCurr.Text;
                            klsSimpleOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text);
                            klsSimpleOrder.Constant = cmbConstant.SelectedIndex;
                            klsSimpleOrder.ConstantDate = (Convert.ToInt32(cmbConstant.SelectedIndex) == 2 ? dConstant.Value.ToString() : "");
                            klsSimpleOrder.SentDate = klsOrder.SentDate;
                            klsSimpleOrder.SendCheck = cbChecked.Checked ? 1 : 0;
                            klsSimpleOrder.FIX_A = -1;
                            klsSimpleOrder.ExecuteDate = klsOrder.ExecuteDate;
                            klsSimpleOrder.RealPrice = (!Global.IsNumeric(fgSingleOrders[j, "RealPrice"]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, "RealPrice"]));
                            klsSimpleOrder.RealQuantity = (!Global.IsNumeric(fgSingleOrders[j, "RealQuantity"]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, "RealQuantity"]));
                            klsSimpleOrder.RealAmount = klsSimpleOrder.RealPrice * klsSimpleOrder.RealQuantity;  //(!Global.IsNumeric(fgSingleOrders[j, "RealAmount"]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, "RealAmount"]));
                            klsSimpleOrder.InformationMethod_ID = 7;                // 7 -  Προσωπικά for simple DMP orders
                            klsSimpleOrder.MainCurr = lblCurr.Text;
                            klsSimpleOrder.FeesCalcMode = 1;
                            klsSimpleOrder.CalcFees();
                            klsSimpleOrder.RecieveDate = dTemp;
                            klsSimpleOrder.RecieveMethod_ID = iRecieveMethod_ID;
                            klsSimpleOrder.User_ID = Global.User_ID;
                            klsSimpleOrder.DateIns = DateTime.Now;
                            k = klsSimpleOrder.InsertRecord();
                            fgSingleOrders[j, 12] = k;

                            Global.AddInformingRecord(1, k, 7, 5, klsSimpleOrder.Client_ID, klsContract.Record_ID, "", "", Global.GetLabel("update_execution_command"),
                                "", "", "", DateTime.Now.ToString(), 1, 1, "");       // 7 - Προσωπικά 
                        }
                        else
                        {
                            if (Convert.ToDecimal(lblQuantity_Sum.Text) != 0)
                                fgSingleOrders[j, 10] = Convert.ToDecimal(fgSingleOrders[j, 7]) * Convert.ToDecimal(txtRealQuantity.Text) / Convert.ToDecimal(lblQuantity_Sum.Text);

                            if (iProduct_ID == 2) fgSingleOrders[j, 11] = Convert.ToDecimal(fgSingleOrders[j, 9]) * Convert.ToDecimal(fgSingleOrders[j, 10]) / 100;
                            else fgSingleOrders[j, 11] = Convert.ToDecimal(fgSingleOrders[j, 9]) * Convert.ToDecimal(fgSingleOrders[j, 10]);

                            klsSimpleOrder = new clsOrdersSecurity();
                            klsSimpleOrder.Record_ID = Convert.ToInt32(fgSingleOrders[j, 12]);
                            klsSimpleOrder.CommandType_ID = 1;
                            klsSimpleOrder.GetRecord();
                            if (Convert.ToInt32(cmbStockExchanges.SelectedValue) != 0)
                                klsSimpleOrder.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                            klsSimpleOrder.SendCheck = cbChecked.Checked ? 1 : 0;

                            klsSimpleOrder.Price = Convert.ToDecimal(fgSingleOrders[j, "Price"]);
                            klsSimpleOrder.Quantity = Convert.ToDecimal(fgSingleOrders[j, "Quantity"]);
                            klsSimpleOrder.Amount = Convert.ToDecimal(fgSingleOrders[j, "Amount"]);
                            klsSimpleOrder.SentDate = klsOrder.SentDate;

                            klsSimpleOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text);
                            klsSimpleOrder.RealPrice = (!Global.IsNumeric(fgSingleOrders[j, 9]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, 9]));
                            klsSimpleOrder.RealQuantity = (!Global.IsNumeric(fgSingleOrders[j, 10]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, 10]));
                            klsSimpleOrder.RealAmount = (!Global.IsNumeric(fgSingleOrders[j, 11]) ? 0 : Convert.ToDecimal(fgSingleOrders[j, 11]));
                            klsSimpleOrder.RecieveMethod_ID = iRecieveMethod_ID;
                            klsSimpleOrder.RecieveDate = dTemp;

                            if (klsOrder.ExecuteDate != Convert.ToDateTime("1900/01/01"))
                            {
                                sTemp = dExecute.Value.ToString("yyyy-MM-dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                                                                                (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                                                                                (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());
                                klsSimpleOrder.ExecuteDate = Convert.ToDateTime(sTemp);
                            }
                            else klsSimpleOrder.ExecuteDate = Convert.ToDateTime("01/01/1900");

                            klsSimpleOrder.EditRecord();
                        }

                        //--- Delete ALL (SourceCommand_ID = 0) Recieved files from SingleOrder ----------------------
                        clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
                        Orders_Recieved.Command_ID = Convert.ToInt32(fgSingleOrders[j, "ID"]);
                        Orders_Recieved.SourceCommand_ID = 0;  // was iRec_ID
                        Orders_Recieved.DeleteAllRecords();

                        //--- Add Recieved files for current SingleOrder ----------------------------------------------
                        for (i = fgRecieved.Rows.Count - 1; i >= 1; i--) {                                
                            if ((fgRecieved[i, "FilePath"] + "") != "") {
                                sTemp = fgRecieved[i, "FilePath"] + "";
                                sNewFileName = Path.GetFileName(sTemp);
                                sNewFileName = Global.DMS_UploadFile(sTemp, "Customers/OrdersAcception", sNewFileName);
                                if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                                else MessageBox.Show("Αρχείο " + fgRecieved[i, "File_Name"] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            clsOrders_Recieved Order_Recieved = new clsOrders_Recieved();
                            Order_Recieved.Command_ID = Convert.ToInt32(fgSingleOrders[j, "ID"]);
                            Order_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                            Order_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, "RecieveMethod_ID"]);
                            Order_Recieved.FilePath = fgRecieved[i, "FilePath"] + "";
                            Order_Recieved.FileName = sNewFileName;
                            Order_Recieved.SourceCommand_ID = iRec_ID;
                            Order_Recieved.InsertRecord();
                        }
                    }
                }
                DefineSums();
                //--- finish Save Allocation data ------------------------------------------------------------------------------

                this.Close();
                iLastAktion = 1;             // was saved (added)
            }
        }
        #endregion
        #region --- Edit record -----------------------------------------------------------------
        private void cmbServiceProvider_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private void txtAction_TextChanged(object sender, EventArgs e)
        {
            switch (txtAction.Text.Substring(0, 1))
            {
                case "B":
                case "b":
                case "Β":
                case "β":
                case "A":
                case "a":
                case "Α":
                case "α":
                    txtAction.Text = "BUY";
                    ucPS.txtShareTitle.Focus();
                    break;
                case "S":
                case "s":
                case "Σ":
                case "σ":
                case "ς":
                case "P":
                case "p":
                case "Π":
                case "π":
                    txtAction.Text = "SELL";
                    ucPS.txtShareTitle.Focus();
                    break;
                default:
                    txtAction.Text = "";
                    txtAction.Focus();
                    break;
            }
        }
        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetText(lnkISIN.Text + "");
        }

        private void picEmptyProduct_Click(object sender, EventArgs e)
        {
            klsOrder.Share_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareTitle.Text = "";
            lblProduct.Text = "";
            lblProductCategory.Text = "";
            lblProductStockExchange_Title.Text = "";
            lblCurr.Text = "";
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2) {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void dSend_ValueChanged(object sender, EventArgs e)
        {
            dSend.CustomFormat = "dd/MM/yyyy";
            if (txtSendHour.Text.Length == 0)
            {
                txtSendHour.Text = dSend.Value.Hour.ToString();
                txtSendMinute.Text = dSend.Value.Minute.ToString();
                txtSendSecond.Text = dSend.Value.Second.ToString();
            }
        }
        private void picClean_Send_Click(object sender, EventArgs e)
        {
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;
            dSend.Enabled = false;
            txtSendHour.Text = "";
            txtSendHour.Enabled = false;
            txtSendMinute.Text = "";
            txtSendMinute.Enabled = false;
            txtSendSecond.Text = "";
            txtSendSecond.Enabled = false;
        }
        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(lstType.SelectedIndex))
            {
                case 0:
                    txtPrice.Enabled = true;
                    break;
                case 1:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
                case 2:
                    txtPrice.Enabled = true;
                    break;
                case 3:
                    txtPrice.Enabled = true;
                    break;
                case 4:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
                case 5:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            dTemp = DateTime.Now;
            dSend.Value = dTemp;
            txtSendHour.Text = dTemp.Hour.ToString();
            txtSendMinute.Text = dTemp.Minute.ToString();
            txtSendSecond.Text = dTemp.Second.ToString();

            dSend.Enabled = true;
            txtSendHour.Enabled = true;
            txtSendMinute.Enabled = true;
            txtSendSecond.Enabled = true;

            dSend.Focus();

            btnExecuted.Enabled = true;
        }
        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if ((!Global.IsNumeric(txtPrice.Text) && txtPrice.Text != "M") || txtPrice.Text.IndexOf(".") > 0) {
                txtPrice.BackColor = Color.Red;
                txtPrice.Focus();
            }
            else {
                txtPrice.BackColor = Color.White;
                if (klsOrder.Product_ID == 2) 
                    if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                       txtAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100)).ToString("0.00");
                else 
                    if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                       txtAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)).ToString("0.00");
            }
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (lstType.SelectedIndex != 1) {                                             // != 1 - isn't Market
                if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0) {
                    txtQuantity.BackColor = Color.Red;
                    txtQuantity.Focus();
                }
                else {
                    txtQuantity.BackColor = Color.White;
                    if (klsOrder.Product_ID == 2) txtAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100)).ToString("0.00");
                    else txtAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)).ToString("0.00");
                }
            }
        }
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0) {
                txtAmount.BackColor = Color.Red;
                txtAmount.Focus();
            }
            else txtAmount.BackColor = Color.White;
        }
        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            txtNotes.Text = txtNotes.Text.Replace("\t", "");
        }
        private void picEmptyExecute_Click(object sender, EventArgs e)
        {
            dExecute.MinDate = Convert.ToDateTime("1900/01/01");
            dExecute.Value = Convert.ToDateTime("1900/01/01");
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            txtExecuteHour.Text = "";
            txtExecuteMinute.Text = "";
            txtExecuteSecond.Text = "";

            txtRealPrice.Text = "0";
            txtRealQuantity.Text = "0";
            txtRealAmount.Text = "0";
            txtAccruedInterest.Text = "0";
            lblInvestAmount.Text = "0";

            cmbStockExchanges.SelectedValue = 0;
        }
        private void btnExecuted_Click(object sender, EventArgs e)
        {
            bContinue = false;
            if (fgSingleOrders.Rows.Count > 2) bContinue = true;
            else
               if (MessageBox.Show("Θέλετε να προχωρήσετε χωρίς Allocation;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) bContinue = true;
            
            if (bContinue) {                
                dTemp = DateTime.Now;
                dExecute.MaxDate = dTemp;
                dExecute.MinDate = dSend.Value;

                dExecute.CustomFormat = "dd/MM/yyyy";
                dExecute.Value = dTemp;

                txtExecuteHour.Text = dTemp.Hour.ToString();
                txtExecuteMinute.Text = dTemp.Minute.ToString();
                txtExecuteSecond.Text = dTemp.Second.ToString();

                dExecute.Enabled = true;
                txtExecuteHour.Enabled = true;
                txtExecuteMinute.Enabled = true;
                txtExecuteSecond.Enabled = true;

                txtRealPrice.Text = (Global.IsNumeric(txtPrice.Text) ? txtPrice.Text : "0");
                txtRealPrice.Enabled = true;

                txtRealQuantity.Text = txtQuantity.Text;
                txtRealQuantity.Enabled = true;

                if (Global.IsNumeric(txtPrice.Text))
                {
                    if (klsOrder.Product_ID == 2)
                        txtRealAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                    else
                        txtRealAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)).ToString("0.0000");
                }

                txtRealAmount.Enabled = true;
                lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.00");

                cmbStockExchanges.SelectedValue = iStockExchange_ID;

                if (lblCurr.Text != "EUR") {
                    clsProductsCodes ProductCode = new clsProductsCodes();
                    ProductCode.DateIns = dAktionDate.Value;
                    ProductCode.Code = "EUR" + lblCurr.Text + "=";
                    ProductCode.GetPrice_Code();
                    lblCurrRate.Text = ProductCode.LastClosePrice.ToString("0.####");
                }
                else lblCurrRate.Text = "1";

                klsOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text);
                klsOrder.ExecuteDate = dExecute.Value;
                klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
                klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
                klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
                //klsOrder.AccruedInterest = txtAccruedInterest.Text;

                DefineCurrRate();

                DefineSimpleCommandsData();

                txtRealPrice.Enabled = true;
                txtRealPrice.Focus();

                panExecute.Enabled = true;
            }
        }
        private void DefineCurrRate()
        {
            if (lblCurr.Text == "EUR") lblCurrRate.Text = "1";
            else
            {
                clsProductsCodes ProductCode = new clsProductsCodes();
                ProductCode.DateIns = dAktionDate.Value;
                ProductCode.Code = "EUR" + lblCurr.Text + "=";
                ProductCode.GetPrice_Code();
                lblCurrRate.Text = ProductCode.LastClosePrice.ToString("0.####");
            }
        }
        private void tsbEditSimpleCommand_Click(object sender, EventArgs e)
        {
            EditSimpleCommand();
        }
        private void tsbDelSimpleCommand_Click(object sender, EventArgs e)
        {
            if (fgSingleOrders.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    i = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, "ID"]);

                    if (i != 0)
                    {
                        clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                        klsOrder2.Record_ID = i;
                        klsOrder2.Status = -1;                                                          // NOT DELETED - CANCEL COMMAND RECORD
                        klsOrder2.EditRecord();

                        fgSingleOrders.RemoveItem(fgSingleOrders.Row);
                        DefineSums();
                    }
                }
            }
        }
        private void tsbAddSimpleCommand_Click(object sender, EventArgs e)
        {
            fgChilds.Rows.Count = 1;
            panAddClients.Visible = true;
            btnInsert_Excel.Visible = false;

            ucCS.StartInit(700, 400, 460, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1 AND Service_ID = 3 ";
            ucCS.ListType = 1;
            EmptyChild();
        }
        private void tabChild_SelectedIndexChanged(Object sender, EventArgs e)
        {
            switch (Convert.ToInt32(tabChild.SelectedIndex))
            {
                case 0:
                    ucCS.Visible = true;
                    break;
                case 1:
                    ucCS.Visible = false;
                    break;       
            }
        }
        private void btnInsert_Manual_Click(object sender, EventArgs e)
        {
            fgChilds.AddItem(fgChilds.Rows.Count + "\t" + lblChildClientName.Text + "\t" + ucCS.txtContractTitle.Text + "\t" + lblChildCode.Text + "\t" +
                 lblChildPortfolio.Text + "\t" + txtChildQuantity.Text.Replace(".", ",") + "\t" + txtChildRealQuantity.Text.Replace(".", ",") + "\t" + iChild_ID);

            EmptyChild();
        }
        private void btnInsert_Excel_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;

            var ExApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            while (true)
            {
                i = i + 1;

                sTemp = (xlRange.Cells[i, 2].Value + "").ToString();
                if (sTemp == "") break;

                j = fgChilds.Rows.Count;
                fgChilds.AddItem(j + "\t" + "" + "\t" + xlRange.Cells[i, 1].Value + "\t" + xlRange.Cells[i, 2].Value + "\t" +
                                             xlRange.Cells[i, 3].Value + "\t" + xlRange.Cells[i, 4].Value + "\t" +
                                             xlRange.Cells[i, 5].Value + "\t" + "0");                                   // Add empty row into fgList       
            }
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            ExApp.Quit();
            Marshal.ReleaseComObject(ExApp);

            this.Cursor = Cursors.Default;
        }
        private void tsbEMail_Click(object sender, EventArgs e)
        {
            string sTemp = "";
            txtThema.Text = "Allocation " + lblShareTitle.Text + " " + lnkISIN.Text + " " + sTemp;
            txtEMail.Text = "";

            if (txtAction.Text == "SELL") sTemp = "SOLD";
            else
               if (txtAction.Text == "BUY") sTemp = "BOUGHT";

            txtBody.Text = "Dear all, " + "\t" + "Please allocate as follows: " + "\t" +
                           "Order type: " + sTemp + "\t" +
                           "Product / ISIN : " + lblShareTitle.Text + " " + lnkISIN.Text + "\t" +
                           "Nominal: " + txtQuantity.Text + "\t" +
                           "Execution Price: " + txtRealPrice.Text;
            panEMail.Visible = true;
        }
        private void lnkDivision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Global.IsNumeric(txtRealQuantity.Text) && Global.IsNumeric(lblQuantity_Sum.Text)) {
                if (Convert.ToDecimal(txtRealQuantity.Text) != 0) {
                    sgTemp = Convert.ToDecimal(lblQuantity_Sum.Text) / Convert.ToDecimal(txtRealQuantity.Text);
                    for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++) {
                        fgSingleOrders[i, 9] = txtRealPrice.Text;
                        fgSingleOrders[i, 10] = Convert.ToDecimal(fgSingleOrders[i, 7]) * Convert.ToDecimal(sgTemp);
                        fgSingleOrders[i, 11] = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(fgSingleOrders[i, 10])).ToString("0.00");
                    }
                }
            }
        }
          private void fgSingleOrders_DoubleClick(object sender, EventArgs e)
        {
            EditSimpleCommand();
        }
        private void EditSimpleCommand() {
            if (Convert.ToInt32(fgSingleOrders.Row) > 1) {
                lblClientName.Text = fgSingleOrders[fgSingleOrders.Row, 2] + "";
                txtPrice_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 6] + "";
                txtQuantity_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 7] + "";
                txtAmount_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 8] + "";
                txtRealPrice_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 9] + "";
                txtRealQuantity_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 10] + "";
                txtRealAmount_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 11] + "";

                clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, 12]);
                klsOrder2.GetRecord();
                klsOrder2.Price = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 6]);
                klsOrder2.Quantity = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 7]);
                klsOrder2.Amount = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 8]);
                klsOrder2.RealPrice = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 9]);
                klsOrder2.RealQuantity = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 10]);
                klsOrder2.RealAmount = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 11]);
                klsOrder2.EditRecord();

                panEdit.Visible = true;
            }
        }
        private void picCloseAddClients_Click(object sender, EventArgs e)
        {
            panAddClients.Visible = false;
        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
            if (txtFilePath.Text != "") btnInsert_Excel.Visible = true;
            else btnInsert_Excel.Visible = false;
        }

        private void txtRealPrice_LostFocus(object sender, EventArgs e)
        {
            RecalcAmounts();

            klsOrder.ExecuteDate = dExecute.Value;
            klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
            klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
            klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);

            DefineSimpleCommandsData();
        }
        private void txtRealQuantity_LostFocus(object sender, EventArgs e)
        {

            RecalcAmounts();

            klsOrder.ExecuteDate = dExecute.Value;
            klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
            klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
            klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);
                        
            DefineSimpleCommandsData();
        }
        private void txtRealAmount_LostFocus(object sender, EventArgs e)
        {
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");
        }
        private void txtAccruedInterest_LostFocus(object sender, EventArgs e)
        {
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");
        }
        private void RecalcAmounts()
        {
            if (!Global.IsNumeric(txtRealPrice.Text)) txtRealPrice.Text = "0";
            if (!Global.IsNumeric(txtRealQuantity.Text)) txtRealQuantity.Text = "0";
            if (!Global.IsNumeric(txtRealAmount.Text)) txtRealAmount.Text = "0";
            if (!Global.IsNumeric(txtAccruedInterest.Text)) txtAccruedInterest.Text = "0";

            if (klsOrder.Product_ID == 2)
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text) / 100).ToString("0.####");
            else
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text)).ToString("0.####");

            txtRealAmount.Enabled = true;
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");
        }
        private void DefineSimpleCommandsData()
        {
            if (fgSingleOrders.Rows.Count == 3)  {
                fgSingleOrders[2, "RealPrice"] = txtRealPrice.Text;
                fgSingleOrders[2, "RealQuantity"] = txtRealQuantity.Text;
                fgSingleOrders[2, "RealAmount"] = (Convert.ToDecimal(fgSingleOrders[2, "RealPrice"]) * Convert.ToDecimal(fgSingleOrders[2, "RealQuantity"])).ToString("0.00##");
            }
            else {
                 for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++) {
                     fgSingleOrders[i, "RealPrice"] = txtRealPrice.Text;
                     fgSingleOrders[i, "RealQuantity"] = fgSingleOrders[i, "Quantity"];
                     fgSingleOrders[i, "RealAmount"] = (Convert.ToDecimal(fgSingleOrders[i, "RealPrice"]) * Convert.ToDecimal(fgSingleOrders[i, "RealQuantity"])).ToString("0.00##");
                 }
            }
            DefineSums();
        }
        private void lnkISIN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.ShareCode_ID = iShare_ID;
            locProductData.Product_ID = iProduct_ID;
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }
        private void picClean_Child_Click(object sender, EventArgs e)
        {
            EmptyChild();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sBlockedContracts = "";
            decimal sgPrice = 0;
            clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();

            fgSingleOrders.Redraw = false;
            for (i = 1; i <= fgChilds.Rows.Count - 1; i++)
            {
                clsContracts klsContract = new clsContracts();
                klsContract.Code = fgChilds[i, "Code"] + "";
                klsContract.Portfolio = fgChilds[i, "Portfolio"] + "";
                klsContract.GetRecord_Code_Portfolio();

                klsContract_Blocks = new clsContract_Blocks();
                klsContract_Blocks.Contract_ID = klsContract.Record_ID;
                klsContract_Blocks.Record_ID = 0;
                klsContract_Blocks.GetRecord_Contract();
                if (klsContract_Blocks.Record_ID == 0) { 

                    sTemp = (fgChilds[i, "Quantity"] + "").Replace(".", ",");
                    sgPrice = Global.IsNumeric(txtPrice.Text) ? Convert.ToDecimal(txtPrice.Text) : 0;
                    sgTemp = sgPrice * Convert.ToDecimal(sTemp);
                    if (iProduct_ID == 2) sgTemp = sgTemp / 100;                   // 2 - Omologo

                    sTemp1 = (fgChilds[i, "RealQuantity"] + "").Replace(".", ",");
                    sgTemp1 = Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(sTemp1);
                    if (iProduct_ID == 2) sgTemp1 = sgTemp1 / 100;                 // 2 - Omologo

                    fgSingleOrders.AddItem((fgSingleOrders.Rows.Count - 1) + "\t" + fgChilds[i, "ClientName"] + "\t" + fgChilds[i, "ContractTitle"] + "\t" + 
                                              fgChilds[i, "Code"] + "\t" + fgChilds[i, "Portfolio"] + "\t" + dAktionDate.Value.ToString("dd/MM/yyyy") + "\t" +
                                              sgPrice + "\t" + sTemp + "\t" + sgTemp + "\t" + txtRealPrice.Text + "\t" + sTemp1 + "\t" + sgTemp1 + "\t" + "0" + "\t" + "1");
                }
                else
                    sBlockedContracts = sBlockedContracts + "-   " + fgChilds[i, "ContractTitle"] + ".  Κωδικός: " + fgChilds[i, "Code"] + ". Portfolio:  " + fgChilds[i, "Portfolio"] + "\n";
            }
            fgSingleOrders.Redraw = true;

            if (sBlockedContracts.Length > 0) {
                sBlockedContracts = "Blocked Accounts : \n\n" + sBlockedContracts;
                MessageBox.Show(sBlockedContracts, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            DefineSums();
            panAddClients.Visible = false;
        }
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            sTemp = txtBody.Text.Replace("\n", "<br/>") + "\n" + "<br/><br/><table width='600' border='1'>" +
                    "<tr><td>N</td><td>CIF</td><td>Subacc</td><td>nominal</td></tr>";
            for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
                sTemp = sTemp + "<tr><td>" + (i - 1) + "</td><td>" + fgSingleOrders[i, 3] + "</td><td>" + fgSingleOrders[i, 4] + "</td><td>" + fgSingleOrders[i, 10] + "</td></tr>";

            sTemp = sTemp + "</table><br/><br/><br/>";
            sTemp = sTemp + Global.UserName + "<br/><br/>" +
                            "<strong>HellasFin</strong><br/>" +
                            "<strong>Global Wealth Management</strong><br/><br/>" +
                            "90, 26th Oktovriou Str. Office 507<br/>" +
                            "P.C.546 27, Thessaloniki, Greece<br/>" +
                            "T. +30 2310 517800<br/>" +
                            "F. +30 2310 515053<br/>" +
                            "E. " + Global.UserEMail + "<br/>" +
                            "W.www.hellasfin.gr</p>";

            Global.AddInformingRecord(0, 0, 5, 1, 0, 0, txtEMail.Text, "rto@hellasfin.gr", txtThema.Text, sTemp, "", "", "", 0, 0, "");                       // 5 - e-mail
            panEMail.Visible = false;
        }

        private void btnCancelMail_Click(object sender, EventArgs e)
        {
            panEMail.Visible = false;
        }
        #endregion
        #region --- fgRecieved functions ------------------------------------------------
        private void picAddRecieved_Click(object sender, EventArgs e)
        {
            fgRecieved.AddItem(Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "", 1);
            if (dSend.Text != "")
                if ((Convert.ToDateTime(dSend.Value) != Convert.ToDateTime("1900/01/01")) && (DateTime.Now >= Convert.ToDateTime(dSend.Value)))
                    MessageBox.Show("Wrong Date: Ημερομηνία Λήψης δεν μπορεί να είναι μεγαλίτερη απο Ημερομηνία Διαβίβασης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picDelRecieved_Click(object sender, EventArgs e)
        {
            if (fgRecieved.Row > 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
                    Orders_Recieved.Record_ID = Convert.ToInt32(fgRecieved[fgRecieved.Row, "ID"]);
                    Orders_Recieved.DeleteRecord();
                    fgRecieved.RemoveItem(fgRecieved.Row);
                }
            }
        }
        private void picShowRecieved_Click(object sender, EventArgs e)
        {
            if ((fgRecieved[fgRecieved.Row, 5]+"").Trim() != "") 
                System.Diagnostics.Process.Start(fgRecieved[fgRecieved.Row, 5] + "");
            else
               if ((fgRecieved[fgRecieved.Row, 2] + "").Trim() != "") 
                    Global.DMS_ShowFile("Customers/OrdersAcception", (fgRecieved[fgRecieved.Row, 2]+""));
        }
        private void picCopyRecievedClipboard_Click(object sender, EventArgs e)
        {
            if (!Convert.IsDBNull(Clipboard.GetText()))
            {
                sTemp = "";
                if (fgRecieved[fgRecieved.Row, 2] + "" != "") sTemp = Global.DocFilesPath_FTP + "~" + "Customers/OrdersAcception/" + fgRecieved[fgRecieved.Row, 2];

                Clipboard.SetText(fgRecieved[fgRecieved.Row, 0] + "~" + fgRecieved[fgRecieved.Row, 1] + "~" + fgRecieved[fgRecieved.Row, 4] + "~" +
                                   fgRecieved[fgRecieved.Row, 2] + "~" + sTemp);
            }
        }
        private void picPasteRecievedClipboard_Click(object sender, EventArgs e)
        {
            string[] tokens = Clipboard.GetText().ToString().Split('~');
            if (tokens.Length > 0)
                fgRecieved.AddItem(tokens[0] + "\t" + tokens[1] + "\t" + Path.GetFileName(tokens[4]) + "\t" + "0" + "\t" + tokens[2] + "\t" + "", 1);
        }
        private void fgRecieved_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 1) fgRecieved[e.Row, 4] = fgRecieved[e.Row, 1];
        }
        private void fgRecieved_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 2) {
                fgRecieved[fgRecieved.Row, 5] = Global.FileChoice(Global.DefaultFolder);
                fgRecieved[fgRecieved.Row, 2] = Path.GetFileName(fgRecieved[fgRecieved.Row, 5]+"");
            }
        }
        #endregion  
        #region --- panEdit functions ----------------------------------------------------
        private void txtPrice_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtPrice_Edit.Text) && Global.IsNumeric(txtQuantity_Edit.Text)) {
                if (iProduct_ID == 2) txtAmount_Edit.Text = (Convert.ToDecimal(txtPrice_Edit.Text) * Convert.ToDecimal(txtQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtAmount_Edit.Text = (Convert.ToDecimal(txtPrice_Edit.Text) * Convert.ToDecimal(txtQuantity_Edit.Text)).ToString("0.0000");
            }
        }
        private void txtQuantity_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtPrice_Edit.Text) && Global.IsNumeric(txtQuantity_Edit.Text))
            {
                if (iProduct_ID == 2) txtAmount_Edit.Text = (Convert.ToDecimal(txtPrice_Edit.Text) * Convert.ToDecimal(txtQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtAmount_Edit.Text = (Convert.ToDecimal(txtPrice_Edit.Text) * Convert.ToDecimal(txtQuantity_Edit.Text)).ToString("0.0000");
            }
        }

        private void txtRealPrice_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtRealPrice_Edit.Text) && Global.IsNumeric(txtRealQuantity_Edit.Text))
            {
                if (iProduct_ID == 2) txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text)).ToString("0.0000");
            }
        }
        private void txtRealQuantity_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtRealPrice_Edit.Text) && Global.IsNumeric(txtRealQuantity_Edit.Text))
            {
                if (iProduct_ID == 2) txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text)).ToString("0.0000");
            }
        }
        private void btnOK_Edit_Click(object sender, EventArgs e)
        {
            fgSingleOrders[fgSingleOrders.Row, 6] = txtPrice_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 7] = txtQuantity_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 8] = txtAmount_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 9] = txtRealPrice_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 10] = txtRealQuantity_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 11] = txtRealAmount_Edit.Text;
            DefineSums();

            clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
            klsOrder2.Record_ID = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, 12]);
            klsOrder2.CommandType_ID = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, 13]);
            klsOrder2.GetRecord();
            klsOrder2.Price = (Global.IsNumeric(fgSingleOrders[fgSingleOrders.Row, 6]) ? Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 6]): 0);
            klsOrder2.Quantity = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 7]);
            klsOrder2.Amount = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 8]);
            klsOrder2.RealPrice = (Global.IsNumeric(fgSingleOrders[fgSingleOrders.Row, 9]) ? Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 9]) : 0);
            klsOrder2.RealQuantity = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 10]);
            klsOrder2.RealAmount = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 11]);
            klsOrder2.EditRecord();

            panEdit.Visible = false;
        }

        private void btnCancel_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        #endregion
        #region --- Common functions ----------------------------------------------------
        private void DefineAllocation_From_Commands()
        {
            if (sBulkCommand.Length > 0) {
                string[] tokens = sBulkCommand.Split('/');
                if (tokens.Length > 0)
                {
                    iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                    if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                }
            }

            sgTemp = 0;
            sgTemp2 = 0;
            i = 0;
            clsOrdersSecurity Orders3 = new clsOrdersSecurity();
            Orders3.AktionDate = dAktionDate.Value;
            Orders3.BulkCommand = iBulcCommand2_ID +"";
            Orders3.GetList_BulkCommand();
            foreach (DataRow dtRow in Orders3.List.Rows)
            {
                if (Convert.ToInt32(dtRow["CommandType_ID"]) == 1) {
                    i = i + 1;
                    fgSingleOrders.AddItem(i + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                                dAktionDate.Value.ToString("dd/MM/yyyy") + "\t" + dtRow["Price"] + "\t" + 
                                                string.Format("{0:#0.0######}", dtRow["Quantity"]) + "\t" + string.Format("{0:#0.00}", dtRow["Amount"]) + "\t" + 
                                                string.Format("{0:#0.00##}", dtRow["RealPrice"]) + "\t" + string.Format("{0:#0.0######}", dtRow["RealQuantity"]) + "\t" + 
                                                string.Format("{0:#0.00}", dtRow["RealAmount"]) + "\t" + dtRow["ID"] + "\t" + dtRow["CommandType_ID"]);

                    sgTemp = sgTemp + Convert.ToDecimal(dtRow["Quantity"]);
                    sgTemp2 = sgTemp2 + Convert.ToDecimal(dtRow["RealQuantity"]);
                }
            }
        }
        private void DefineSums()
        {
            decimal sgTemp = 0, sgTemp1 = 0, sgTemp2 = 0, sgTemp3 = 0;
            for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
            {
                sgTemp = sgTemp + Convert.ToDecimal(fgSingleOrders[i, "Quantity"]);
                sgTemp1 = sgTemp1 + Convert.ToDecimal(fgSingleOrders[i, "Amount"]);
                sgTemp2 = sgTemp2 + Convert.ToDecimal(fgSingleOrders[i, "RealQuantity"]);
                sgTemp3 = sgTemp3 + Convert.ToDecimal(fgSingleOrders[i, "RealAmount"]);
            }
            lblQuantity_Sum.Text = string.Format("{0:#0.00##}", sgTemp);
            lblAmount_Sum.Text = string.Format("{0:#0.00##}", sgTemp1);
            lblRealQuantity_Sum.Text = string.Format("{0:#0.00##}", sgTemp2);
            lblRealAmount_Sum.Text = string.Format("{0:#0.00##}", sgTemp3);
        }
        private void EmptyChild()
        {
            iChild_ID = 0;
            lblChildClientName.Text = "";
            lblChildCode.Text = "";
            lblChildPortfolio.Text = "";
            txtChildQuantity.Text = "0";
            txtChildRealQuantity.Text = "0";

            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();            
            stContract = ucCS.SelectedContractData;
            iChild_ID = stContract.Contract_ID;
            lblChildClientName.Text = stContract.ClientName;
            lblChildCode.Text = stContract.Code;
            lblChildPortfolio.Text = stContract.Portfolio;
            klsOrder.Client_ID = stContract.Client_ID;
            klsOrder.Contract_ID = stContract.Contract_ID;
            klsOrder.ServiceProvider_ID = stContract.Provider_ID;

            txtAction.Focus();
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            lnkISIN.Text = stProduct.ISIN;
            lblShareTitle.Text = stProduct.Title;
            lblProduct.Text = stProduct.Product_Title;
            iProductCategory_ID = stProduct.ProductCategory_ID;
            lblProductCategory.Text = stProduct.Product_Category;
            lblProductStockExchange_Title.Text = stProduct.StockExchange_Code;
            iShare_ID = stProduct.ShareCode_ID;
            lblCurr.Text = stProduct.Currency;
        }
        #endregion

        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int CommandType_ID { get { return this.iCommandType_ID; } set { this.iCommandType_ID = value; } }
        public int BusinessType { get { return this.iBusinessType; } set { this.iBusinessType = value; } }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public int Editable { get { return this.iEditable; } set { this.iEditable = value; } }
    }
}
