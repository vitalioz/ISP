using System;
using System.Data;
using System.Windows.Forms;
using Core;
using C1.Win.C1FlexGrid;

namespace Transactions
{
    public partial class frmTransmissionList : Form
    {
        int i, k, iOddEvenBlock, iStyle;
        string sInvestPolicy, sBulkCommand;
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        DateTime dToday;
        clsOrdersSecurity Orders = new clsOrdersSecurity();
        clsOrdersSecurity Orders2 = new clsOrdersSecurity();
        clsOrdersSecurity Orders3 = new clsOrdersSecurity();
        public frmTransmissionList()
        {
            InitializeComponent();
        }

        private void frmTransmissionList_Load(object sender, EventArgs e)
        {
           
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;

            fgList.Rows.Count = 1;
            fgList.Redraw = false;

            k = 0;
            iOddEvenBlock = 0;             // pseudo even block

            Orders = new clsOrdersSecurity();
            Orders.CommandType_ID = 2;
            Orders.DateFrom = dToday;
            Orders.DateTo = dToday;
            Orders.ServiceProvider_ID = 0;
            Orders.Sent = 0;
            Orders.Actions = 0;
            Orders.SendCheck = 0;
            Orders.User_ID = 0;
            Orders.User1_ID = 0;
            Orders.User4_ID = 0;
            Orders.Division_ID = 0;
            Orders.Code = "";
            Orders.ShowCancelled = 0;
            Orders.GetExecutionList();
            foreach (DataRow dtRow in Orders.List.Rows)
            {
                if (Convert.ToInt32(dtRow["StockCompany_ID"]) == 20)
                {
                    if (Convert.ToInt32(dtRow["Type"]) == 3 && Convert.ToInt32(dtRow["Parent_ID"]) == 0)
                    {     // if it's scenario first command
                        if (iOddEvenBlock == 1) iOddEvenBlock = 2;                                             // define odd/even block
                        else iOddEvenBlock = 1;

                        iStyle = iOddEvenBlock;
                    }
                    else if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iStyle = 0;                             // it's simple command

                    sInvestPolicy = "";
                    if (Convert.ToInt32(dtRow["AdvisoryInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["AdvisoryInvestmentPolicy_Title"] + "";

                    if (Convert.ToInt32(dtRow["DiscretInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["DiscretInvestmentPolicy_Title"] + "";

                    if (Convert.ToInt32(dtRow["DealAdvisoryInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["DealAdvisoryInvestmentPolicy_Title"] + "";

                    sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                    sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                    if (Convert.ToDateTime(dtRow["SentDate"]) == Convert.ToDateTime("01/01/1900"))
                    {
                        k = k + 1;
                        fgList.AddItem(false + "\t" + k + "\t" + sBulkCommand + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["StockCompanyTitle"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" +
                                       dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" + dtRow["Share_Title"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                       dtRow["Share_Code"] + "\t" + Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                       (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                       (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" + dtRow["Currency"] + "\t" +
                                       sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + dtRow["StockExchange_MIC"] + "\t" +
                                       dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + dtRow["ID"] + "\t" +
                                       dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + iStyle + "\t" + dtRow["Share_ID"] + "\t" +
                                       dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" +
                                       dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["BestExecution"] + "\t" + dtRow["StockExchange_ID"] + "\t" +
                                       dtRow["PriceType"] + "\t" + dtRow["Aktion"]);
                    }
                }
            }            
            fgList.Sort(SortFlags.Descending, 1);     // 1- Num
            fgList.Redraw = true;
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                Global.AddNewOrders(Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "ID"]), fgList[i, "Aktion"] + "",
                                    Convert.ToInt32(fgList[i, "Provider_ID"]), fgList[i, "Share_ISIN"] + "", Convert.ToInt32(fgList[i, "PriceType"]),
                                    fgList[i, "Currency"] + "", Convert.ToDecimal(fgList[i, "Price"]), Convert.ToDecimal(fgList[i, "Quantity"]),
                                    Convert.ToInt32(fgList[i, "BestExecution"]), Convert.ToInt32(fgList[i, "StockExchange_ID"]));

                Orders = new clsOrdersSecurity();
                Orders.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                Orders.GetRecord();
                sBulkCommand = Orders.BulkCommand.Replace("<", "").Replace(">", "").Trim();
                Orders.SentDate = dToday;
                Orders.SendCheck = 0;
                Orders.FIX_A = 0;
                Orders.EditRecord();

                if (sBulkCommand != "")
                {
                    Orders2 = new clsOrdersSecurity();
                    Orders2.AktionDate = dToday;
                    Orders2.BulkCommand = sBulkCommand;
                    Orders2.GetList_BulkCommand();
                    foreach (DataRow dtRow in Orders2.List.Rows)
                    {
                        Orders3 = new clsOrdersSecurity();
                        Orders3.Record_ID = Convert.ToInt32(dtRow["ID"]);
                        Orders3.GetRecord();
                        Orders3.SentDate = dToday;
                        Orders3.SendCheck = 0;
                        Orders3.EditRecord();
                    }
                }
            }

            this.Close();
        }
        public DateTime Today { get { return dToday; } set { dToday = value; } }
    }
}
