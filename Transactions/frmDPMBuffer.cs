using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmDPMBuffer : Form
    {
        int i = 0, j = 0, iPriceType, iProvider_ID, iProduct_ID, iCheckedRecsCount = 0;
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        decimal decPrice, decQuantity, decAmount, decCurrRate;
        DateTime dDateFrom, dDateTo;
        DataRow[] foundRows;

        clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
        public frmDPMBuffer()
        {
            InitializeComponent();

            iProvider_ID = 0;
            iProduct_ID = 0;
            iCheckedRecsCount = 0;
        }

        private void frmDPMBuffer_Load(object sender, EventArgs e)
        {
            btnAgree.Enabled = false;
            btnNotAgree.Enabled = false;

            //-------------- Define cmbRecievedMethods List ------------------
            cmbRecieveMethod3.DataSource = Global.dtRecieveMethods.Copy();
            cmbRecieveMethod3.DisplayMember = "Title";
            cmbRecieveMethod3.ValueMember = "ID";
            cmbRecieveMethod3.SelectedValue = 8;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_AfterEdit);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            DefineList();
            fgList.Row = 0;
            if (fgList.Rows.Count > 1) fgList.Row = 1;
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void btnClear_Filter_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
        }
        private void DefineList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            clsOrdersDPM klsOrdersDPM = new clsOrdersDPM();
            klsOrdersDPM.DateFrom = dDateFrom;
            klsOrdersDPM.DateTo = dDateTo;
            klsOrdersDPM.User_ID = 0;
            klsOrdersDPM.GetList_NewOrders();

            i = 0;
            foreach (DataRow dtRow in klsOrdersDPM.List.Rows) {
                if (((dtRow["ContractTitle"] + "").IndexOf(txtFilter.Text.ToUpper()) >= 0))  { 

                   if (iProvider_ID == 0 || Convert.ToInt32(dtRow["StockCompany_ID"]) == iProvider_ID) {
                        i = i + 1;
                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["Diax_Fullname"] + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                      dtRow["StockCompany_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                      (Convert.ToInt32(dtRow["Aktion"]) == 1? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                      dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                      Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                      (Convert.ToDecimal(dtRow["Quantity"]) == 0? "" : (Convert.ToInt32(dtRow["Product_ID"]) == 6? dtRow["Quantity"]+"": Convert.ToDecimal(dtRow["Quantity"]).ToString("0.00"))) + "\t" +
                                      (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : (dtRow["Amount"] + "")) + "\t" + dtRow["Currency"] + "\t" + 
                                      sConstant[Convert.ToInt32(dtRow["Constant"])] + "\t" + dtRow["ProductStockExchange_Code"] + "\t" + dtRow["SentDate"] + "\t" + dtRow["DPM_Notes"] + "\t" + 
                                      dtRow["ID"] + "\t" + dtRow["DPM_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" + dtRow["Client_ID"] + "\t" + 
                                      dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + dtRow["Share_ID"] + "\t" + 
                                      dtRow["Status"] + "\t" + dtRow["PriceType"] + "\t" + dtRow["Constant"] + "\t" + dtRow["ConstantDate"] + "\t" + dtRow["Product_ID"] + "\t" + 
                                      dtRow["ProductCategory_ID"] + "\t" + dtRow["OrderType"] + "\t" + dtRow["Diax_ID"]);
                   }
                }
            }
            fgList.Redraw = true;
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;

            Empty_Row();
            if (chkList.Checked)
            {
                iCheckedRecsCount = fgList.Rows.Count - 1;
                btnAgree.Enabled = true;
                btnNotAgree.Enabled = true;
            }
            else
            {
                iCheckedRecsCount = 0;
                btnAgree.Enabled = false;
                btnNotAgree.Enabled = false;
            }
        }

        private void picRecieveVoicePath_Click(object sender, EventArgs e)
        {
            txtRecieveVoicePath.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void picPlayRecieveVoice_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtRecieveVoicePath.Text);
        }

        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;  
        }
        private void fgList_AfterEdit(object sender, RowColEventArgs e)
        {
            iCheckedRecsCount = 0;
            for (j = 1; j <= fgList.Rows.Count - 1; j++)
                if (Convert.ToBoolean(fgList[j, 0])) iCheckedRecsCount = iCheckedRecsCount + 1;

            if (iCheckedRecsCount == 0) {
                btnAgree.Enabled = false;
                btnNotAgree.Enabled = false;
            }
            else {
                btnAgree.Enabled = true;
                btnNotAgree.Enabled = true;
            }

            if (iCheckedRecsCount != 1) Empty_Row();
            else {
                panPre_Data.Enabled = true;
                i = fgList.Row;
                if (i > 0)  {
                    lblContractTitle.Text = fgList[i, "ContractTitle"] + "";
                    lblCode.Text = fgList[i, "Code"] + "";
                    lblPortfolio.Text = fgList[i, "Portfolio"] + "";
                    lblAction.Text = fgList[i, "Aktion"] + "";
                    lblProduct.Text = fgList[i, "Product_Category"] + "";
                    cmbConstant.Text = fgList[i, "Duration"] + "";
                    lblTitle.Text = fgList[i, "Share_Title"] + "";
                    lblISIN.Text = fgList[i, "ISIN"] + "";
                    lblReuters.Text = fgList[i, "Share_Code"] + "";
                    txtPrice.Text = fgList[i, "Price"] + "";
                    txtQuantity.Text = fgList[i, "Quantity"] + "";
                    lstType.SelectedIndex = Convert.ToInt32(fgList[i, "Type"]);
                    txtAmount.Text = fgList[i, "Amount"] + "";
                    lblCurr.Text = fgList[i, "Currency"] + "";
                    cmbConstant.Text = fgList[i, "Duration"] + "";
                    if (Convert.ToInt32(cmbConstant.SelectedIndex) == 2) {
                        dConstant.Visible = true;
                        dConstant.Value = Convert.ToDateTime(fgList[i, "ConstantDate"]);
                    }
                    else dConstant.Visible = false;
                    iProduct_ID = Convert.ToInt32(fgList[i, "Product_ID"]);

                    lblNotes.Text = fgList[i, "Notes"] + "";
                    switch (Convert.ToInt32(fgList[i, "Product_ID"]))
                    {
                        case 1:
                        case 4:
                            lblPre_Price.Visible = true;
                            txtPrice.Visible = true;
                            lblCurr.Visible = true;
                            lblPre_Quantity.Visible = true;
                            txtQuantity.Visible = true;
                            lblPre_Quantity.Text = Global.GetLabel("pieces");
                            break;
                        case 2:
                            lblPre_Price.Visible = true;
                            txtPrice.Visible = true;
                            lblCurr.Visible = true;
                            lblPre_Quantity.Visible = true;
                            txtQuantity.Visible = true;
                            lblPre_Quantity.Text = Global.GetLabel("nomical_value");
                            break;
                        case 6:
                            lblPre_Price.Visible = false;
                            txtPrice.Visible = false;
                            lblCurr.Visible = false;
                            lblPre_Quantity.Text = Global.GetLabel("shares");
                            break;
                    }
                }
            };
        }
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0) {
                txtPrice.BackColor = Color.Red;
                txtPrice.Focus();
            }
            else {
                txtPrice.BackColor = Color.White;
                DefineNums(1);
            }
        }
        private void txtQuantity_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0)  {
                txtQuantity.BackColor = Color.Red;
                txtQuantity.Focus();
            }
            else  {
                txtQuantity.BackColor = Color.White;
                DefineNums(2);
            }
        }
        private void DefineNums(int iField)
        {
            if (Convert.ToInt32(lstType.SelectedValue) != 1) {
                if (Global.IsNumeric(txtPrice.Text)) {
                    decPrice = Convert.ToDecimal(txtPrice.Text);
                    decQuantity = (Global.IsNumeric(txtQuantity.Text) ? Convert.ToDecimal(txtQuantity.Text) : 0);
                    decAmount = (Global.IsNumeric(txtAmount.Text) ? Convert.ToDecimal(txtAmount.Text) : 0);

                    if (iField == 1 || iField == 2) {
                        txtAmount.Text = (decPrice * decQuantity).ToString("0.00");

                        if (iProduct_ID == 2) txtAmount.Text = (Convert.ToSingle(txtAmount.Text) / 100).ToString("0.00");
                    }
                    else  {
                        if (decQuantity == 0) {
                            if (decPrice != 0) txtQuantity.Text = Math.Round(decAmount / decPrice).ToString("0.00");
                            else txtQuantity.Text = "0";
                        }
                    }
                }
                else txtQuantity.Text = "0";
            }
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtQuantity.Text) || iCheckedRecsCount > 1) {
                for (j = 1; j <= fgList.Rows.Count - 1; j++)
                    if (Convert.ToBoolean(fgList[j, 0])) Confirmation(j);

                iCheckedRecsCount = 0;
                DefineList();
                Empty_Row();
            }
            else  MessageBox.Show(Global.GetLabel("wrong_amount"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnNotAgree_Click(object sender, EventArgs e)
        {
            for (j = 1; j <= fgList.Rows.Count - 1; j++)
                if (Convert.ToBoolean(fgList[j, 0])) NonConfirmation(j);

            iCheckedRecsCount = 0;
            DefineList();
            Empty_Row();
        }
        private void NonConfirmation(int iRow)
        {
            ChangeDPMOrders_Status(iRow, 2);                                                    // 2 - Non Confirm
        }
        private void Confirmation(int iRow)
        {
            int iRec_ID = 0;
            int iProviderType = 0, iBusinessType_ID = 1;
            string sNotes = "";

            clsOrdersSecurity Orders = new clsOrdersSecurity();
            clsOrdersSecurity Orders2 = new clsOrdersSecurity();

            if (iCheckedRecsCount == 1) {

                if (Global.IsNumeric(txtPrice.Text)) {
                    iPriceType = 0;
                    decPrice = Convert.ToDecimal(txtPrice.Text);
                }
                else {
                    iPriceType = 1;
                    decPrice = 0;
                } 

                if (Global.IsNumeric(txtQuantity.Text)) decQuantity = Convert.ToDecimal(txtQuantity.Text);
                else decQuantity = 0;

                if (Global.IsNumeric(txtAmount.Text)) decAmount = Convert.ToDecimal(txtAmount.Text);
                else decAmount = 0;

                if (lblNotes.Text != "" || txtRTONotes.Text != "") sNotes = lblNotes.Text.Trim() + "/" + txtRTONotes.Text.Trim();
            }
            else {
                if (Global.IsNumeric(fgList[iRow, "Price"])) {
                    iPriceType = 0;
                    decPrice = Convert.ToDecimal(fgList[iRow, "Price"]); ;
                }
                else {
                    iPriceType = 1;
                    decPrice = 0;
                }                

                if (Global.IsNumeric(fgList[iRow, "Quantity"])) decQuantity = Convert.ToDecimal(fgList[iRow, "Quantity"]);
                else decQuantity = 0;

                if (Global.IsNumeric(fgList[iRow, "Amount"])) decAmount = Convert.ToDecimal(fgList[iRow, "Amount"]);
                else decAmount = 0;

                if ((fgList[iRow, "Notes"] + "") != "" || txtRTONotes.Text != "") sNotes = fgList[iRow, "Notes"] + "/" + txtRTONotes.Text.Trim();
            }
            decCurrRate = 0;            

            //--- define iBusinessType_ID -------------------------------------------------------
            foundRows = Global.dtServiceProviders.Select("ID = " + fgList[iRow, "Provider_ID"]);
            if (foundRows.Length > 0)
                iProviderType = Convert.ToInt32(foundRows[0]["ProviderType"]);

            if (iProviderType == 1) iBusinessType_ID = 1;                                                               // 1 - CreditSuisse  
            if (iProviderType == 2) iBusinessType_ID = 2;

            //--- Confirm DPMOrder by Client (OrderType = 1) -----------------------------------------------
            if (Convert.ToInt32(fgList[iRow, "OrderType"]) == 1) {                                                      // 1 - DPMOrder by Client

                Orders2 = new clsOrdersSecurity();
                Orders2.AktionDate = DateTime.Now;
                Orders2.BulkCommand = "";
                Orders2.BusinessType_ID = iBusinessType_ID;
                Orders2.CommandType_ID = 1;                                                                             // confirm it as SingleOrder - it will be shown on SigleOrders Tab
                Orders2.Client_ID = Convert.ToInt32(fgList[iRow, "Client_ID"]);
                Orders2.Company_ID = Global.Company_ID;
                Orders2.ServiceProvider_ID = Convert.ToInt32(fgList[iRow, "Provider_ID"]);
                Orders2.Executor_ID = 0;
                Orders2.StockExchange_ID = Convert.ToInt32(fgList[iRow, "StockExchange_ID"]);
                Orders2.CustodyProvider_ID = Convert.ToInt32(fgList[iRow, "Provider_ID"]);
                Orders2.Depository_ID = 0;
                Orders2.II_ID = Convert.ToInt32(fgList[iRow, "DPM_ID"]);
                Orders2.Parent_ID = 0;
                Orders2.Contract_ID = Convert.ToInt32(fgList[iRow, "Contract_ID"]);
                Orders2.Contract_Details_ID = Convert.ToInt32(fgList[iRow, "Contract_Details_ID"]);
                Orders2.Contract_Packages_ID = Convert.ToInt32(fgList[iRow, "Contract_Packages_ID"]);
                Orders2.Code = fgList[iRow, "Code"] + "";
                Orders2.ProfitCenter = fgList[iRow, "Portfolio"] + "";
                Orders2.AllocationPercent = 100;                                                                         //  DPMOrder by Client confirm with Allocation, so it's = 100                     
                Orders2.Aktion = ((fgList[iRow, "Aktion"] + "") == "BUY" ? 1 : 2);
                Orders2.AktionDate = DateTime.Now;
                Orders2.Share_ID = Convert.ToInt32(fgList[iRow, "Share_ID"]);
                Orders2.Product_ID = Convert.ToInt32(fgList[iRow, "Product_ID"]);
                Orders2.ProductCategory_ID = Convert.ToInt32(fgList[iRow, "ProductCategory_ID"]);
                Orders2.PriceType = iPriceType;
                Orders2.Price = decPrice;
                Orders2.Quantity = decQuantity;
                Orders2.Amount = decAmount;
                Orders2.Curr = fgList[iRow, "Currency"] + "";
                Orders2.Constant = Convert.ToInt32(fgList[iRow, "Constant"]);
                Orders2.ConstantDate = fgList[iRow, "ConstantDate"] + "";
                Orders2.RecieveDate = DateTime.Now;
                Orders2.RecieveMethod_ID = Convert.ToInt32(cmbRecieveMethod3.SelectedValue);                             // 8 - DPM Order
                Orders2.SentDate = Convert.ToDateTime("1900/01/01");
                Orders2.FIX_A = -1;
                Orders2.ExecuteDate = Convert.ToDateTime("1900/01/01");
                Orders2.CurrRate = decCurrRate;
                Orders2.MinFeesRate = 0;
                Orders2.Notes = fgList[iRow, "Notes"] + "";
                Orders2.User_ID = Global.User_ID;
                Orders2.DateIns = DateTime.Now;
                iRec_ID = Orders2.InsertRecord();

                //--- Add Order_Recieved for Single Order (CommandType_ID = 1) ----------------------------
                Orders_Recieved = new clsOrders_Recieved();
                Orders_Recieved.Command_ID = iRec_ID;
                Orders_Recieved.DateIns = DateTime.Now;
                Orders_Recieved.Method_ID = Convert.ToInt32(cmbRecieveMethod3.SelectedValue);
                Orders_Recieved.FilePath = txtRecieveVoicePath.Text;
                Orders_Recieved.FileName = Path.GetFileName(txtRecieveVoicePath.Text);
                Orders_Recieved.SourceCommand_ID = iRec_ID;
                Orders_Recieved.InsertRecord();
            }

            //--- Confirm DPMOrder by Product (OrderType = 2) -----------------------------------------------
            if (Convert.ToInt32(fgList[iRow, "OrderType"]) == 2) {                                                     // 2 - DPMOrder by Product

                //--- Add DPM Order (CommandType_ID = 4) ----------------------------------------------
                Orders = new clsOrdersSecurity();
                Orders.BulkCommand = "";
                Orders.BusinessType_ID = iBusinessType_ID;
                Orders.CommandType_ID = 4;                                                                              // confirm it as DPM Order - it will be shown on DPM Tab
                Orders.Client_ID = Convert.ToInt32(fgList[iRow, "Client_ID"]);
                Orders.Company_ID = Convert.ToInt32(fgList[iRow, "Diax_ID"]);                                           // must be Diaxiristis 
                Orders.ServiceProvider_ID = Convert.ToInt32(fgList[iRow, "Provider_ID"]);
                Orders.Executor_ID = 0;
                Orders.StockExchange_ID = Convert.ToInt32(fgList[iRow, "StockExchange_ID"]);
                Orders.CustodyProvider_ID = Convert.ToInt32(fgList[iRow, "Provider_ID"]);
                Orders.Depository_ID = 0;
                Orders.II_ID = Convert.ToInt32(fgList[iRow, "DPM_ID"]);                                                 // it's DPMOrders.ID
                Orders.Parent_ID = 0;
                Orders.Contract_ID = Convert.ToInt32(fgList[iRow, "Contract_ID"]);
                Orders.Contract_Details_ID = Convert.ToInt32(fgList[iRow, "Contract_Details_ID"]);
                Orders.Contract_Packages_ID = Convert.ToInt32(fgList[iRow, "Contract_Packages_ID"]);
                Orders.Code = fgList[iRow, "Code"] + "";
                Orders.ProfitCenter = fgList[iRow, "Portfolio"] + "";
                Orders.AllocationPercent = 0;                                                                           //  DPMOrder by Product confirm without Allocation, so it's = 0
                Orders.Aktion = ((fgList[iRow, "Aktion"] + "") == "BUY" ? 1 : 2);
                Orders.AktionDate = DateTime.Now;
                Orders.Share_ID = Convert.ToInt32(fgList[iRow, "Share_ID"]);
                Orders.Product_ID = Convert.ToInt32(fgList[iRow, "Product_ID"]);
                Orders.ProductCategory_ID = Convert.ToInt32(fgList[iRow, "ProductCategory_ID"]);
                Orders.PriceType = iPriceType;
                Orders.Price = decPrice;
                Orders.Quantity = decQuantity;
                Orders.Amount = decAmount;
                Orders.Curr = fgList[iRow, "Currency"] + "";
                Orders.Constant = Convert.ToInt32(fgList[iRow, "Constant"]);
                if (Global.IsDate(fgList[iRow, "ConstantDate"] + "")) Orders.ConstantDate = Convert.ToDateTime(fgList[iRow, "ConstantDate"] + "").ToString("dd/MM/yyyy");
                else Orders.ConstantDate = "";
                Orders.RecieveDate = DateTime.Now;
                Orders.RecieveMethod_ID = Convert.ToInt32(cmbRecieveMethod3.SelectedValue);          // 8 - DPM Order
                Orders.SentDate = Convert.ToDateTime("1900/01/01");
                Orders.FIX_A = -1;
                Orders.ExecuteDate = Convert.ToDateTime("1900/01/01");
                Orders.CurrRate = decCurrRate;
                Orders.Notes = sNotes;
                Orders.MinFeesRate = 0;
                Orders.User_ID = Global.User_ID;
                Orders.DateIns = DateTime.Now;
                iRec_ID = Orders.InsertRecord();

                Orders_Recieved = new clsOrders_Recieved();
                Orders_Recieved.Command_ID = iRec_ID;
                Orders_Recieved.DateIns = DateTime.Now;
                Orders_Recieved.Method_ID = Convert.ToInt32(cmbRecieveMethod3.SelectedValue);
                Orders_Recieved.FilePath = txtRecieveVoicePath.Text;
                Orders_Recieved.FileName = Path.GetFileName(txtRecieveVoicePath.Text);
                Orders_Recieved.SourceCommand_ID = iRec_ID;
                Orders_Recieved.InsertRecord();
            }

            ChangeDPMOrders_Status(iRow, 3);                                                       // 3 - Confirm
        }
        private void ChangeDPMOrders_Status(int iRow, int iStatus)
        {
            //--- Change status of ...----------------------------------------------
            if (Convert.ToInt32(fgList[iRow, "OrderType"]) == 1) {                                               // ... DPMOrders_Recs  for DPMOrder by Client
                clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                OrdersDPM_Recs.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                OrdersDPM_Recs.GetRecord();
                OrdersDPM_Recs.Status = iStatus;
                OrdersDPM_Recs.EditRecord();
            }
            else   {                                                                                             // ... DPMOrders for DPMOrder by Product
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.Record_ID = Convert.ToInt32(fgList[iRow, "DPM_ID"]);
                OrdersDPM.GetRecord();
                OrdersDPM.Status = iStatus;
                OrdersDPM.EditRecord();
            }
        }
        private void Empty_Row()
        {
            lblII_ID.Text = "";
            lblContractTitle.Text = "";
            lblCode.Text = "";
            lblPortfolio.Text = "";
            lblAction.Text = "";
            cmbConstant.SelectedIndex = 0;
            dConstant.Visible = false;
            lblProduct.Text = "";
            lblTitle.Text = "";
            lblISIN.Text = "";
            lblReuters.Text = "";
            txtQuantity.Text = "";
            txtAmount.Text = "";
            lstType.SelectedIndex = 0;
            txtPrice.Text = "";
            lblCurr.Text = "";
            txtPriceUp.Text = "";
            txtPriceDown.Text = "";
            txtRecieveVoicePath.Text = "";
            lblNotes.Text = "";
            lblTel.Text = "";
            lblMobile.Text = "";
            cmbRecieveMethod3.SelectedValue = 8;
            panPre_Data.Enabled = false;
        }
        public DateTime DateFrom { get { return dDateFrom; } set { dDateFrom = value; } }
        public DateTime DateTo { get { return dDateTo; } set { dDateTo = value; } }
    }
}
