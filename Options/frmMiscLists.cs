using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class frmMiscLists : Form
    {
        int iRightsLevel;
        string sExtra;
        bool bCheckTree = false;

        public frmMiscLists()
        {
            InitializeComponent();
        }

        private void frmMiscList_Load(object sender, EventArgs e)
        {
            bCheckTree = false;

            this.Controls.Add(ucDL);
            HideAllUCs();

            //------- fgTablesList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.Rows.Count = 1;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            clsSystem System = new clsSystem();
            System.GetListsTables();
            foreach (DataRow dtRow in System.List.Rows)
            {
                if ((dtRow["ListGroup"] + "") != "-") 
                    fgList.AddItem(dtRow["ListGroup"] + "\t" + dtRow["ListTitle"] + "\t" + dtRow["ID"] + "\t" + dtRow["TableName"] + "\t" + dtRow["CashTables_ID"]);

            }
            fgList.AllowMerging = AllowMergingEnum.Free;
            fgList.Cols[0].AllowMerging = true;
            fgList.Redraw = true;
            fgList.Row = 0;
            bCheckTree = true;
            fgList.Row = 1;

        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 62;

            ucDL.Height = this.Height - 50;                                 // Default List
            ucDL.Width = this.Width - 460;
            ucSE.Height = this.Height - 50;                                 // StockExchanges List
            ucSE.Width = this.Width - 460;
            ucSL.Height = this.Height - 50;                                 // Sectors List
            ucSL.Width = this.Width - 460;
            ucCurrenciesList.Width = this.Width - 460;
            ucCurrenciesList.Height = this.Height - 50;
            ucCountriesList.Width = this.Width - 460;
            ucCountriesList.Height = this.Height - 50;
            ucDepositories.Width = this.Width - 460;
            ucDepositories.Height = this.Height - 50;
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckTree)
            {
                HideAllUCs();

                switch (Convert.ToInt32(fgList[fgList.Row, "ID"])) {
                    case 6:             // 6 - Sectors
                        ucSL.Top = 2;
                        ucSL.Left = 460;
                        ucSL.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucSL.Visible = true;
                        break;
                    case 13:             // 13-Έγγραφα πελατών
                        ucDL.Top = 2;
                        ucDL.Left = 460;
                        ucDL.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucDL.Visible = true;
                        break;
                    case 23:             // 23-Stock Exchanges
                        ucSE.Top = 2;
                        ucSE.Left = 460;
                        ucSE.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucSE.Visible = true;
                        break;
                    case 26:             // 26 - Currencies
                        ucCurrenciesList.Top = 2;
                        ucCurrenciesList.Left = 460;
                        ucCurrenciesList.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucCurrenciesList.Visible = true;
                        break;
                    case 27:             // 27-Countries                        
                        ucCountriesList.Top = 2;
                        ucCountriesList.Left = 460;
                        ucCountriesList.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucCountriesList.Visible = true;
                        break;
                    case 38:             // 38-Depositories
                        ucDepositories.Top = 2;
                        ucDepositories.Left = 460;
                        ucDepositories.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucDepositories.Visible = true;
                        break;
                    case 58:             // 58 - RatingsList
                        break;
                    default:           // misc simple lists
                        ucDL.Top = 2;
                        ucDL.Left = 460;
                        ucDL.StartInit(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "CashTables_ID"]), fgList[fgList.Row, "TableName"].ToString(), fgList[fgList.Row, "Title"].ToString());
                        ucDL.Visible = true;
                        break;
                }
            }
            fgList.Focus();
        }
        private void HideAllUCs()
        {
            ucDL.Top = -2000;
            ucSE.Top = -2000;
            ucSL.Top = -2000;
            ucCurrenciesList.Top = -2000;
            ucCountriesList.Top = -2000;
            ucDepositories.Top = -2000;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
