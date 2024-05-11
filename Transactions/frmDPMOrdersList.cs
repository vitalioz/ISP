using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmDPMOrdersList : Form
    {
        DataView dtView;
        int i,  iRow, iRightsLevel, iDiaxiristis_ID = 0;
        string sExtra;
        DateTime dTemp;
        bool bCheckList, bFilter;
        CellRange rng;
        CellStyle[] csStatus = new CellStyle[5];

        #region --- Start functions ---------------------------------------------------------
        public frmDPMOrdersList()
        {
            InitializeComponent();

            bCheckList = false;
            panOrderType.Left = 4;
            panOrderType.Top = 86;

            csStatus[0] = fgList.Styles.Add("New");
            csStatus[0].BackColor = Color.Transparent;

            csStatus[1] = fgList.Styles.Add("Sent");
            csStatus[1].BackColor = Color.Yellow;

            csStatus[2] = fgList.Styles.Add("NonConfirmed");
            csStatus[2].BackColor = Color.LightCoral;

            csStatus[3] = fgList.Styles.Add("Confirmed");
            csStatus[3].BackColor = Color.LightGreen;

            csStatus[4] = fgList.Styles.Add("Cancelled");
            csStatus[4].BackColor = Color.Coral;
        }
        private void frmDPMOrdersList_Load(object sender, EventArgs e)
        {     
            dToday.Value = DateTime.Now;
            
            ucDates.DateFrom = DateTime.Now;  //.AddDays(-3);
            ucDates.DateTo = DateTime.Now;

            //-------------- Define Diaxiristes List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Diaxiristis = 1 AND Aktive = 1";
            cmbDiaxiristes.DataSource = dtView;
            cmbDiaxiristes.DisplayMember = "Title";
            cmbDiaxiristes.ValueMember = "ID";

            if (Global.Diaxiristis == 1) {                                                         // 1 - current user is Diaxiristis
                iDiaxiristis_ID = Global.User_ID;
                cmbDiaxiristes.SelectedValue = iDiaxiristis_ID;
                if (Global.IsNumeric(sExtra)) {
                    if (Convert.ToInt32(sExtra) > 0) cmbDiaxiristes.Enabled = false;
                    else cmbDiaxiristes.Enabled = true;
                }
            }
            else {                                                                                // 2 - current user isn't Diaxiristis. He's helper (author of DPM Order)
                if (Global.IsNumeric(sExtra)) iDiaxiristis_ID = Convert.ToInt32(sExtra);
                cmbDiaxiristes.SelectedValue = iDiaxiristis_ID;
                cmbDiaxiristes.Enabled = false;
            }

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.CellRange;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.CellChanged += fgList_CellChanged;

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = "";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Ημερομηνία";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Αρ.Εντολής";

            rng = fgList.GetCellRange(0, 4, 0, 8);
            rng.Data = Global.GetLabel("customer");

            fgList[1, 4] = Global.GetLabel("customer_name");
            fgList[1, 5] = Global.GetLabel("contract"); 
            fgList[1, 6] = Global.GetLabel("provider");
            fgList[1, 7] = Global.GetLabel("code");
            fgList[1, 8] = Global.GetLabel("portfolio");    

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("products");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("notes");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = "Ωρα Αποστολής στο RTO";

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            bCheckList = true;
            DefineList();
            DefineRecsList();
        }
        protected override void OnResize(EventArgs e)
        {
            tabMain.Width = this.Width - 24;
            tabMain.Height = this.Height - 48;

            panCritiries.Width = tabMain.Width - 20;

            fgList.Width = tabMain.Width - 20;
            fgList.Height = tabMain.Height - 114;

            fgRecs.Width = tabMain.Width - 20;
            fgRecs.Height = tabMain.Height - 164;
        }
        #endregion
        #region --- fgList functions ------------------------------------------------   
        public void DefineList()
        {
            if (bCheckList) { 
                clsOrdersDPM klsOrdersDPM = new clsOrdersDPM();

                i = 0;
                fgList.Redraw = false;
                fgList.Rows.Count = 2;

                klsOrdersDPM.DateFrom = dToday.Value;
                klsOrdersDPM.DateTo = dToday.Value;
                klsOrdersDPM.User_ID = Convert.ToInt32(cmbDiaxiristes.SelectedValue);
                klsOrdersDPM.GetList();
                foreach (DataRow dtRow in klsOrdersDPM.List.Rows)  {
                    bFilter = true;

                    if (bFilter) {
                        i = i + 1;
                        fgList.AddItem(false + "\t" + i + "\t" + Convert.ToDateTime(dtRow["AktionDate"]).ToString("dd/MM/yyyy") + "\t" + dtRow["ID"] + "\t" + 
                                       (dtRow["ClientSurname"] + " " + dtRow["ClientFirstname"]).Trim() + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Provider_Title"] + "\t" + 
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Products"] + "\t" + dtRow["Notes"] + "\t" +
                                       ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("dd/MM/yyyy hh:mm:ss") : "") + "\t" +
                                       dtRow["Client_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Status"]);
                    }
                    fgList.Sort(SortFlags.Descending, 3);     // 3 - ID
                }
                fgList.Redraw = true;
                if (fgList.Rows.Count > 2) fgList.Row = 2;
                fgList.Focus();
            }
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1) {
                if (e.Col == 14) {                                                                                            // 14 - Status         
                    if (Convert.ToInt32(fgList[e.Row, "Status"]) < 0) fgList.Rows[e.Row].Style = csStatus[3];                 // 3 - Cancelled
                    else fgList.Rows[e.Row].Style = csStatus[Convert.ToInt32(fgList[e.Row, "Status"])];                       // 0 - New, 1 - Sent
                }
            }
        }
        #endregion
        #region --- toolbar functions ---------------------------------------------------
        private void dToday_ValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void cmbDiaxiristes_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            panOrderType.Visible = true;
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditRow();
        }
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1)  {
                i = fgList.Row;
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                OrdersDPM.GetRecord();
                OrdersDPM.Status = 4;                                                                             // 4 - cancelled
                OrdersDPM.EditRecord();

                fgList[i, 0] = false;
                fgList[i, "Status"] = 4;
            }
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count -1; i++) { 
                if ((Convert.ToBoolean(fgList[i, 0])) && ((fgList[i, "SentDate"]+"") == "")) { 
                    dTemp = DateTime.Now;

                    clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                    OrdersDPM.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    OrdersDPM.GetRecord();
                    OrdersDPM.SentDate = dTemp;
                    OrdersDPM.Status = 1;                                                      // 1 - sent to RTO
                    OrdersDPM.EditRecord();

                    clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                    OrdersDPM_Recs.DPM_ID = Convert.ToInt32(fgList[i, "ID"]);
                    OrdersDPM_Recs.GetList();
                    foreach (DataRow dtRow in OrdersDPM_Recs.List.Rows)
                    {
                        OrdersDPM_Recs.Record_ID = Convert.ToInt32(dtRow["ID"]);
                        OrdersDPM_Recs.GetRecord();
                        OrdersDPM_Recs.Status = 1;                                            // 1 - sent to RTO                                       
                        OrdersDPM_Recs.EditRecord();
                    }

                    fgList[i, 0] = false;
                    fgList[i, "SentDate"] = dTemp.ToString("dd/MM/yyyy hh:mm:ss");
                    fgList[i, "Status"] = 1;
                }
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void EditRow()
        {
            iRow = fgList.Row;
            if (iRow > 0)  {
                if (Convert.ToInt32(fgList[iRow, "Contract_ID"]) != 0)  {
                    frmDPMOrder_Client locDPMOrder_Client = new frmDPMOrder_Client();
                    locDPMOrder_Client.Today = dToday.Value;
                    locDPMOrder_Client.II_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                    locDPMOrder_Client.ShowDialog();
                }
                else  {
                    frmDPMOrder_Product locDPMOrder_Product = new frmDPMOrder_Product();
                    locDPMOrder_Product.DPM_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                    locDPMOrder_Product.Today = dToday.Value;
                    locDPMOrder_Product.ShowDialog();
                };
                DefineList();
            }
        }
        private void lnkCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panOrderType.Visible = false;
            frmDPMOrder_Client locDPMOrder_Client = new frmDPMOrder_Client();
            locDPMOrder_Client.Today = dToday.Value;
            locDPMOrder_Client.Diaxiristis_ID = iDiaxiristis_ID;
            locDPMOrder_Client.II_ID = 0;
            locDPMOrder_Client.ShowDialog();
            if (locDPMOrder_Client.LastAktion == 1)  DefineList();
        }
        private void lnkProduct_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panOrderType.Visible = false;
            frmDPMOrder_Product locDPMOrder_Product = new frmDPMOrder_Product();
            locDPMOrder_Product.Today = dToday.Value;
            locDPMOrder_Product.Diaxiristis_ID = iDiaxiristis_ID;
            locDPMOrder_Product.ShowDialog();
            if (locDPMOrder_Product.LastAktion == 1) DefineList();
        }
        #endregion
        #region --- fgRecs funcions ---------------------------------------------
        private void DefineRecsList()
        {
            if (bCheckList)  {
                clsOrdersSecurity klsOrders = new clsOrdersSecurity();

                i = 0;
                fgRecs.Redraw = false;
                fgRecs.Rows.Count = 1;

                klsOrders.CommandType_ID = 4;                               //  4 - DPM Orders tou RTO
                klsOrders.DateFrom = ucDates.DateFrom;
                klsOrders.DateTo = ucDates.DateTo;
                klsOrders.ServiceProvider_ID = 0;
                klsOrders.User_ID = Convert.ToInt32(cmbDiaxiristes.SelectedValue);
                klsOrders.Sent = 0;
                klsOrders.Actions = 0;
                klsOrders.GetDPMList(); 
                foreach (DataRow dtRow in klsOrders.List.Rows)
                {
                    bFilter = true;

                    if (bFilter)  {
                        i = i + 1;
                        fgRecs.AddItem(i + "\t" + dtRow["DPM_ID"] + "\t" + "" + "\t" + Convert.ToDateTime(dtRow["AktionDate"]).ToString("dd/MM/yyyy") + "\t" + 
                                       dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                        dtRow["ServiceProvider_Title"] + "\t" + dtRow["Aktion"] + "\t" + dtRow["Product_Title"] + "\t" + dtRow["Notes"] + "\t" +
                                       ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("dd/MM/yyyy") : "") + "\t" +
                                       dtRow["Client_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Status"]);
                    }
                    //fgRecs.Sort(SortFlags.Descending, 0);     // 
                }
                fgRecs.Redraw = true;
                if (fgRecs.Rows.Count > 1) fgRecs.Row = 1;
                fgRecs.Focus();
            }
        }
        #endregion
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
