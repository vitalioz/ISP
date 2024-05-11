using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmFIXReport : Form
    {
        int iServiceProvider_ID;
        string  sTemp, sClOrdID;
        DataRow[] foundRows;
        clsExecutionReports ExecutionReports = new clsExecutionReports();
        clsOrdersSecurity Orders = new clsOrdersSecurity();
        public frmFIXReport()
        {
            InitializeComponent();
        }

        private void frmFIXReport_Load(object sender, EventArgs e)
        {

            lblClOrdID.Text = sClOrdID;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;

            fgList.Rows.Count = 1;
            fgList.Redraw = false;

            foundRows = Global.dtServiceProviders.Select("ID = " + iServiceProvider_ID);
            if (foundRows.Length > 0 && (foundRows[0]["FIX_DB"] + "") != "") Global.connFIXStr = Global.FIX_DB_Server_Path + "database=" + foundRows[0]["FIX_DB"];

            if (sClOrdID != "0")
            {
                ExecutionReports = new clsExecutionReports();
                ExecutionReports.ClOrdID = sClOrdID;
                ExecutionReports.GetList();
                foreach (DataRow dtRow in ExecutionReports.List.Rows)
                {
                    switch (dtRow["OrdStatus"]+"")
                    {
                        case "0":
                            sTemp = "New";
                            break;
                        case "1":
                            sTemp = "Partially filled";
                            break;
                        case "2":
                            sTemp = "Filled";
                            break;
                        case "3":
                            sTemp = "Done for day";
                            break;
                        case "4":
                            sTemp = "Canceled";
                            break;
                        case "5":
                            sTemp = "Replaced";
                            break;
                        case "6":
                            sTemp = "Pending Cancel";
                            break;
                        case "7":
                            sTemp = "Stopped";
                            break;
                        case "8":
                            sTemp = "Rejected";
                            break;
                        case "9":
                            sTemp = "Suspended";
                            break;
                        case "A":
                            sTemp = "Pending New";
                            break;
                        case "B":
                            sTemp = "Calculated";
                            break;
                        case "C":
                            sTemp = "Expired";
                            break;
                        case "D":
                            sTemp = "Accepted for bidding";
                            break;
                        case "E":
                            sTemp = "Pending Replace";
                            break;
                        default:
                            sTemp = "-";
                            break;
                    }

                    fgList.AddItem(dtRow["ID"] + "\t" + dtRow["CurrentTimestamp"] + "\t" + dtRow["MsgType"] + "\t" + dtRow["SequenceNumber"] + "\t" + 
                                   dtRow["ClOrdID"] + "\t" + dtRow["OrigClOrdID"] + "\t" + dtRow["OrdStatus"] + " - " + sTemp + "\t" + 
                                   dtRow["Text"] + "\t" + dtRow["Account"] + "\t" + dtRow["Side"] + "\t" + dtRow["OrderQty"]);
                }

                //Orders = new clsOrdersSecurity();
                //Orders.Record_ID = Convert.ToInt32(sClOrdID.Replace("C", ""));
                //Orders.GetRecord();
                //sClOrdID = Orders.Parent_ID.ToString() + "";
            }
            fgList.Redraw = true;

        }
        public int ServiceProvider_ID { get { return iServiceProvider_ID; } set { iServiceProvider_ID = value; } }
        public string ClOrdID { get { return sClOrdID; } set { sClOrdID = value; } }
    }
}
