using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace Accounting
{
    public partial class frmPortfolio : Form
    {
        int i, iCDP_ID, iContract_ID, iContracts_Balances_ID, iRightsLevel;
        string sContractTitle;
        DataRow[] foundRows;
        DateTime _dDateControl;
        clsContracts_Balances Contracts_Balances = new clsContracts_Balances();
        clsContracts_BalancesRecs Contracts_BalancesRecs = new clsContracts_BalancesRecs();
        public frmPortfolio()
        {
            InitializeComponent();
        }

        private void frmPortfolio_Load(object sender, EventArgs e)
        {
            ucCS.StartInit(700, 400, 500, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1 And Contract_ID > 0";
            ucCS.Mode = 1;
            ucCS.ListType = 1;            
            ucCS.Visible = true;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            if (iCDP_ID != 0)
            {
                dDateControl.Value = _dDateControl;

                foundRows = Global.dtContracts.Select("CDP_ID = " + iCDP_ID);
                iContract_ID = Convert.ToInt32(foundRows[0]["Contract_ID"]);
                sContractTitle = foundRows[0]["ContractTitle"] + "";
                lblCode.Text = foundRows[0]["Code"] + "";
                lblPortfolio.Text = foundRows[0]["Portfolio"] + "";
                lblCurrency.Text = foundRows[0]["Currency"] + "";
                lblMiFID_2.Text = Convert.ToInt32(foundRows[0]["MIFID_2"]) == 1 ? "Yes" : "";
                lblXAA.Text = Convert.ToInt32(foundRows[0]["XAA"]) == 1 ? "Yes" : "";

                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = sContractTitle;
                ucCS.Contract_ID.Text = iContract_ID.ToString();
                ucCS.ShowClientsList = true;
                DefineList();
            }
            else
            {
                dDateControl.Value = DateTime.Now.Date;
            }
            
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 324;
            fgList.Width = this.Width - 30;
            //panTools.Width = this.Width - 30;

            //panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            //panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (iContracts_Balances_ID == 0)
            {
                Contracts_Balances = new clsContracts_Balances();
                Contracts_Balances.DateIns = dDateControl.Value.Date;
                Contracts_Balances.GetList();
                foreach (DataRow dtRow in Contracts_Balances.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["Contract_ID"]) == iContract_ID)
                    {
                        iContracts_Balances_ID = Convert.ToInt32(dtRow["ID"]);
                        break;
                    }
                }
            }
            DefineList();
        }
        private void DefineList()
        {
            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            Contracts_Balances = new clsContracts_Balances();
            Contracts_Balances.Record_ID = iContracts_Balances_ID;
            Contracts_Balances.GetRecord();
            lblTotalValue.Text = Contracts_Balances.TotalValue.ToString("###,###,##0.00");

            Contracts_BalancesRecs = new clsContracts_BalancesRecs();
            Contracts_BalancesRecs.CDP_ID = iCDP_ID;
            Contracts_BalancesRecs.DateFrom = dDateControl.Value;
            Contracts_BalancesRecs.DateTo = dDateControl.Value;
            Contracts_BalancesRecs.GetList();

            foreach (DataRow dtRow in Contracts_BalancesRecs.List.Rows)
            {
                i = i + 1;
                fgList.AddItem(i + "\t" + dtRow["ShareCodes_Title"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Participation_PRC"] + "\t" + dtRow["CurrentValue_RepCcy"] + "\t" + dtRow["TotalUnits"] + "\t" +
                               dtRow["CurrentPrice"] + "\t" + dtRow["Curr"] + "\t" + "" + "\t" + dtRow["CountryRisk_Title"] + "\t" + dtRow["CountriesGroups_Title"] + "\t" + dtRow["Sectors_Title"] + "\t" +
                               dtRow["RiskCurr"] + "\t" + dtRow["GlobalBroad_Title"] + "\t" + dtRow["Product_Type"] + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + 
                               "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["ShareCodes_ID"]);
            }
            fgList.Redraw = true;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            if (ucCS.Mode == 1)
            {
                //Global.ContractData stContract = new Global.ContractData();
                //stContract = ucCS.SelectedContractData;
                if (ucCS.Contract_ID.Text != "0")
                {
                    iContract_ID = Convert.ToInt32(ucCS.Contract_ID.Text);
                    switch (ucCS.ListType)
                    {
                        case 1:
                        case 2:
                            //iContract_ID = stContract.Contract_ID;
                            foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID);
                            sContractTitle = foundRows[0]["ContractTitle"] + "";
                            lblCode.Text = foundRows[0]["Code"] + "";
                            lblPortfolio.Text = foundRows[0]["Portfolio"] + "";
                            lblCurrency.Text = foundRows[0]["Currency"] + "";
                            lblMiFID_2.Text = Convert.ToInt32(foundRows[0]["MIFID_2"]) == 1 ? "Yes" : "";
                            lblXAA.Text = Convert.ToInt32(foundRows[0]["XAA"]) == 1 ? "Yes" : "";
                            break;
                        case 3:
                          
                            //iClientType = stContract.Category;
                            break;
                    }
                }
            }

        }
        private void lnkAdvisoryMonitoring_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPortfolio_MonitoringOLD locPortfolio_Monitoring = new frmPortfolio_MonitoringOLD();
            locPortfolio_Monitoring.Extra = "";
            locPortfolio_Monitoring.RightsLevel = iRightsLevel;
            locPortfolio_Monitoring.ShowDialog();
        }

        private void fgList_Click(object sender, EventArgs e)
        {

        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }

        private void menuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) Clipboard.SetText(fgList[fgList.Row, "ISIN"] + "");
        }

        private void menuCopyReuters_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) Clipboard.SetText(fgList[fgList.Row, "Reuters"] + "");
        }

        private void menuCopyBloomberg_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) Clipboard.SetText(fgList[fgList.Row, "Bloomberg"] + "");
        }

        private void menuProductData_Click(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = Convert.ToInt32(fgList[fgList.Row, "Product_ID"]);
            locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ShareCodes_ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }

        public DateTime DateControl { get { return this._dDateControl; } set { this._dDateControl = value; } }
        public int Contracts_Balances_ID { get { return this.iContracts_Balances_ID; } set { this.iContracts_Balances_ID = value; } }
        public int CDP_ID { get { return this.iCDP_ID; } set { this.iCDP_ID = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
