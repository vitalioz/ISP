using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Transactions
{
    public partial class frmTransfer : Form
    {
        DataRow[] foundRows;
        DateTime dDateFrom;
        int iRec_ID;
        clsOrdersSecurity klsOrder;
        public frmTransfer()
        {
            InitializeComponent();
            dFrom.MaxDate = DateTime.Now.Date.AddDays(-1);
            dTo.MaxDate = DateTime.Now.Date;
        }
        private void frmTransfer_Load(object sender, EventArgs e)
        {
            dFrom.Value = dDateFrom.Date;
            dTo.Value = dFrom.Value.Date.AddDays(1);            
        }
        private void dFrom_ValueChanged(object sender, EventArgs e)
        {
            dTo.Value = dFrom.Value.AddDays(1);
        }
        private void dTo_ValueChanged(object sender, EventArgs e)
        {
            if (dFrom.Value >= dTo.Value)  dFrom.Value = dTo.Value.AddDays(-1);
        }        
        private void btnOK_Click(object sender, EventArgs e)
        {
            int i = 0, iParent_ID, iOldParent_ID;
            string sTemp = "";
            string sChildBulkCommand = "";

            //--- transfer Single Orders ----------------------------------------
            clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
            klsOrderSecurity.DateFrom = dFrom.Value;
            klsOrderSecurity.User_ID = 0;
            klsOrderSecurity.GetList_ConstantNonContinue();
            foreach (DataRow dtRow in klsOrderSecurity.List.Rows)
            {
                if ((Convert.ToInt32(dtRow["Constant"]) == 0) ||                                                                           // if  it's daily order or
                    (Convert.ToInt32(dtRow["Constant"]) == 2 && (dTo.Value.Date > Convert.ToDateTime(dtRow["ConstantDate"]).Date)) ||      // it will expire today or    
                    (Convert.ToInt32(dtRow["CommandType_ID"]) == 2)                                                                        // it's Execution order         
                   )
                {
                    dtRow["TransferFlag"] = "0";                                                                                           // don't transfer this order 

                    if (Convert.ToInt32(dtRow["FIX_A"]) >= 0)
                    {                                                                            // if it's FIX order  
                        if ((Convert.ToInt32(dtRow["CommandType_ID"]) > 1) && ((dtRow["BulkCommand"] + "") != ""))
                        {
                            sTemp = dtRow["BulkCommand"] + "";
                            i = sTemp.IndexOf("/");
                            if (i > 0) sChildBulkCommand = sTemp.Substring(i + 2);
                            else sChildBulkCommand = dtRow["BulkCommand"] + "";


                            //--- define all orders from Single Orders list, that are children of this bulk/DPM/execution order ------
                            foundRows = klsOrderSecurity.List.Select("BulkCommand='" + sChildBulkCommand + "'");
                            for (i = 0; i <= (foundRows.Length - 1); i++)
                            {
                                foundRows[i]["BulkCommand"] = "";
                                foundRows[i]["SentDate"] = Convert.ToDateTime("1900/01/01");
                            }
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(dtRow["SendOrders"]) == 0)                                                     // transfer without SentDate (ΠΕΙΡΕΑΥΣ)
                    {
                        dtRow["BulkCommand"] = "";
                        dtRow["SentDate"] = Convert.ToDateTime("1900/01/01");
                    }
                }
            }            

            iParent_ID = 0;
            iOldParent_ID = 0;
            
            foreach (DataRow dtRow in klsOrderSecurity.List.Rows) { 
                if (Convert.ToInt32(dtRow["TransferFlag"]) > 0) {
                    if ((Convert.ToInt32(dtRow["Constant"]) < 2) || (Convert.ToInt32(dtRow["Constant"]) == 2 && (dTo.Value.Date <= Convert.ToDateTime(dtRow["ConstantDate"]).Date)))
                    {
                        if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iParent_ID = 0;       //  isn't scenario command or scenario's 1st command                        
                        else iParent_ID = iOldParent_ID;                                    //  it's scenario's children command

                        klsOrder = new clsOrdersSecurity();
                        klsOrder.BulkCommand = dtRow["BulkCommand"] + "";
                        klsOrder.BusinessType_ID = Convert.ToInt32(dtRow["BusinessType_ID"]);
                        klsOrder.CommandType_ID = Convert.ToInt32(dtRow["CommandType_ID"]);
                        klsOrder.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                        klsOrder.Company_ID = Convert.ToInt32(dtRow["Company_ID"]);
                        klsOrder.ServiceProvider_ID = Convert.ToInt32(dtRow["ServiceProvider_ID"]);
                        klsOrder.StockExchange_ID = Convert.ToInt32(dtRow["StockExchange_ID"]);
                        klsOrder.CustodyProvider_ID = Convert.ToInt32(dtRow["CustodyProvider_ID"]);
                        klsOrder.II_ID = Convert.ToInt32(dtRow["II_ID"]);
                        klsOrder.Parent_ID = Convert.ToInt32(dtRow["ID"]);                   
                        klsOrder.Contract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                        klsOrder.Contract_Details_ID = Convert.ToInt32(dtRow["Contract_Details_ID"]);
                        klsOrder.Contract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                        klsOrder.Code = dtRow["Code"] + "";
                        klsOrder.ProfitCenter = dtRow["Portfolio"] + "";
                        klsOrder.AllocationPercent = Convert.ToSingle(dtRow["AllocationPercent"]); 
                        klsOrder.Aktion = Convert.ToInt32(dtRow["Aktion"]);
                        klsOrder.AktionDate = dTo.Value;
                        klsOrder.Share_ID = Convert.ToInt32(dtRow["Share_ID"]);
                        klsOrder.Product_ID = Convert.ToInt32(dtRow["Product_ID"]);
                        klsOrder.ProductCategory_ID = Convert.ToInt32(dtRow["ProductCategory_ID"]);
                        klsOrder.PriceType = Convert.ToInt32(dtRow["PriceType"]);
                        klsOrder.Price = Convert.ToDecimal(dtRow["Price"]);
                        klsOrder.Quantity = Convert.ToDecimal(dtRow["Quantity"]) - Convert.ToDecimal(dtRow["RealQuantity"]);
                        klsOrder.Amount = Convert.ToDecimal(dtRow["Amount"]);
                        klsOrder.Curr = dtRow["Curr"] + "";
                        klsOrder.Constant = Convert.ToInt32(dtRow["Constant"]);
                        klsOrder.ConstantDate = dtRow["ConstantDate"] + "";
                        klsOrder.ConstantContinue = 0;
                        klsOrder.RecieveDate = Convert.ToDateTime(dtRow["RecieveDate"]);
                        klsOrder.RecieveMethod_ID = Convert.ToInt32(dtRow["RecieveMethod_ID"]);
                        klsOrder.BestExecution = Convert.ToInt32(dtRow["BestExecution"]);
                        klsOrder.SentDate = Convert.ToDateTime(dtRow["SentDate"]);
                        klsOrder.SendCheck = 0;                                              // was Convert.ToInt32(dtRow["SendCheck"]);
                        klsOrder.FIX_A = -1;
                        klsOrder.FIX_RecievedDate = Convert.ToDateTime("1900/01/01");
                        klsOrder.Notes = dtRow["Notes"] + "";
                        klsOrder.User_ID = Convert.ToInt32(dtRow["User_ID"]);
                        klsOrder.DateIns = Convert.ToDateTime(dtRow["DateIns"]);
                        iRec_ID = klsOrder.InsertRecord();

                        if (Convert.ToInt32(dtRow["PriceType"]) == 3)                    // it's scenario's  command
                        {                 
                            if (iOldParent_ID == 0) iOldParent_ID = iRec_ID;             // if it's scenario's 1st command
                        }
                        else iOldParent_ID = 0;                                          // isn't scenario command


                        //--- Copy Commands_Recieved records ------------------
                        clsOrders_Recieved klsOrders_Recieved = new clsOrders_Recieved();
                        klsOrders_Recieved.Command_ID = Convert.ToInt32(dtRow["ID"]);
                        klsOrders_Recieved.GetList();
                        foreach (DataRow dtRow2 in klsOrders_Recieved.List.Rows)
                        {
                            klsOrders_Recieved.Command_ID = iRec_ID;
                            klsOrders_Recieved.DateIns = Convert.ToDateTime(dtRow2["DateIns"]);
                            klsOrders_Recieved.Method_ID = Convert.ToInt32(dtRow2["Method_ID"]);
                            klsOrders_Recieved.FilePath = dtRow2["FilePath"] + "";
                            klsOrders_Recieved.FileName = dtRow2["FileName"] + "";
                            klsOrders_Recieved.SourceCommand_ID = iRec_ID;
                            klsOrders_Recieved.InsertRecord();
                        }

                        clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                        klsOrder2.Record_ID = Convert.ToInt32(dtRow["ID"]);
                        klsOrder2.EditConstantContinue();
                    }
                }
            }

            //--- transfer FX Orders ----------------------------------------
            clsOrdersFX klsOrderFX = new clsOrdersFX();
            klsOrderFX.DateFrom = dFrom.Value;
            klsOrderFX.User_ID = 0;
            klsOrderFX.GetList_ConstantNonContinue();
            foreach (DataRow dtRow in klsOrderFX.List.Rows)
            {
                if ((Convert.ToInt32(dtRow["Constant"]) < 2) || (Convert.ToInt32(dtRow["Constant"]) == 2 && (dTo.Value.Date <= Convert.ToDateTime(dtRow["ConstantDate"]).Date)))
                {
                    klsOrderFX = new clsOrdersFX();
                    klsOrderFX.BulkCommand = dtRow["BulkCommand"] + "";
                    klsOrderFX.BusinessType_ID = Convert.ToInt32(dtRow["BusinessType_ID"]);
                    klsOrderFX.CommandType_ID = Convert.ToInt32(dtRow["CommandType_ID"]);
                    klsOrderFX.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                    klsOrderFX.Company_ID = Global.Company_ID;
                    klsOrderFX.StockCompany_ID = Convert.ToInt32(dtRow["StockCompany_ID"]);
                    klsOrderFX.StockExchange_ID = Convert.ToInt32(dtRow["StockExchange_ID"]);
                    klsOrderFX.CustodyProvider_ID = Convert.ToInt32(dtRow["CustodyProvider_ID"]);
                    klsOrderFX.II_ID = Convert.ToInt32(dtRow["II_ID"]);
                    klsOrderFX.Contract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                    klsOrderFX.Contract_Details_ID = Convert.ToInt32(dtRow["Contract_Details_ID"]);
                    klsOrderFX.Contract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                    klsOrderFX.Code = dtRow["Code"] + "";
                    klsOrderFX.Portfolio = dtRow["Portfolio"] + "";
                    klsOrderFX.AktionDate = Convert.ToDateTime(dTo.Value);
                    klsOrderFX.Tipos = Convert.ToInt32(dtRow["PriceType"]);
                    klsOrderFX.AmountFrom = dtRow["AmountFrom"] + "";
                    klsOrderFX.CurrFrom = dtRow["CurrFrom"] + "";
                    klsOrderFX.CashAccountFrom_ID = Convert.ToInt32(dtRow["CashAccountFrom_ID"]);
                    klsOrderFX.AmountTo = dtRow["AmountTo"] + "";
                    klsOrderFX.CurrTo = dtRow["CurrTo"] + "";
                    klsOrderFX.CashAccountTo_ID = Convert.ToInt32(dtRow["CashAccountTo_ID"]);
                    klsOrderFX.Rate = Convert.ToDecimal(dtRow["Rate"]);
                    klsOrderFX.Constant = Convert.ToInt32(dtRow["Constant"]);
                    klsOrderFX.ConstantDate = dtRow["ConstantDate"] + "";
                    klsOrderFX.RecieveDate = Convert.ToDateTime(dtRow["RecieveDate"]);
                    klsOrderFX.RecieveMethod_ID = Convert.ToInt32(dtRow["RecieveMethod_ID"]);
                    klsOrderFX.Notes = dtRow["Notes"] + "";
                    klsOrderFX.User_ID = Convert.ToInt32(dtRow["User_ID"]);
                    klsOrderFX.DateIns = Convert.ToDateTime(dtRow["DateIns"]);
                    iRec_ID = klsOrderFX.InsertRecord();

                    //--- Copy Commands_Recieved records ------------------
                    clsOrdersFX_Recieved klsOrdersFX_Recieved = new clsOrdersFX_Recieved();
                    klsOrdersFX_Recieved.CommandFX_ID = Convert.ToInt32(dtRow["ID"]);
                    klsOrdersFX_Recieved.GetList();
                    foreach (DataRow dtRow2 in klsOrdersFX_Recieved.List.Rows)
                    {
                        klsOrdersFX_Recieved.CommandFX_ID = iRec_ID;
                        klsOrdersFX_Recieved.DateIns = Convert.ToDateTime(dtRow2["DateIns"]);
                        klsOrdersFX_Recieved.Method_ID = Convert.ToInt32(dtRow2["Method_ID"]);
                        klsOrdersFX_Recieved.FilePath = dtRow2["FilePath"] + "";
                        klsOrdersFX_Recieved.FileName = dtRow2["FileName"] + "";
                        klsOrdersFX_Recieved.InsertRecord();
                    }

                    clsOrdersFX klsOrderFX2 = new clsOrdersFX();
                    klsOrderFX2.Record_ID = Convert.ToInt32(dtRow["ID"]);
                    klsOrderFX2.EditConstantContinue();
                }
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public DateTime DateFrom { get { return dDateFrom; } set { dDateFrom = value; } }
    }
}
