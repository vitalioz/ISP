using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class ucSectorsList : UserControl
    {
        int i, iList_ID, iMax1Level, iCashTables_ID, iRightsLevel, row, level;
        string sTableName, sLevel;
        bool bCheckList, bCollapsed;
        clsSectors Sectors = new clsSectors();
        public ucSectorsList()
        {
            InitializeComponent();            
        }

        private void ucSectorsList_Load(object sender, EventArgs e)
        {

        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Height = this.Height - 6;
            grpData.Width = this.Width - 20;
            fgList.Height = this.Height - 92;
        }
        public void StartInit(int iParentList_ID, int iParentCashTables_ID, string sParentTableName, string sParentListTitle)
        {
            iList_ID = iParentList_ID;
            iCashTables_ID = iParentCashTables_ID;
            sTableName = sParentTableName;
            lblListItemTitle.Text = sParentListTitle;
            bCollapsed = false;

            DefineTree();

            if (fgList.Rows.Count > 1) {
                fgList.Row = 1;
                ShowRecord();
            }

            if (iRightsLevel == 1) toolLeft.Enabled = false;
        }
        private void DefineTree()
        {
            bCheckList = false;
            fgList.Redraw = false;
            fgList.Tree.Column = 0;
            fgList.Rows.Count = 1;
            iMax1Level = 0;

            Sectors = new clsSectors();
            Sectors.L1 = -1;                    // -1 - means ALL
            Sectors.L2 = -1;                    // -1 - means ALL
            Sectors.L3 = -1;                    // -1 - means ALL
            Sectors.GetList();

            if (Sectors.List.Rows.Count > 0) {
                row = 0;
                foreach (DataRow dtRow in Sectors.List.Rows)
                {

                    row = row + 1;
                    level = 1;
                    sLevel = dtRow["L1"] + "";

                    if (Convert.ToInt32(dtRow["L2"]) != 0) {
                        level = 2;
                        sLevel = sLevel + "." + dtRow["L2"];
                    }
                    if (Convert.ToInt32(dtRow["L3"]) != 0)
                    {
                        level = 3;
                        sLevel = sLevel + "." + dtRow["L3"];
                    }
                    if (Convert.ToInt32(dtRow["L4"]) != 0)
                    {
                        level = 4;
                        sLevel = sLevel + "." + dtRow["L4"];
                    }
                    if (Convert.ToInt32(dtRow["L5"]) != 0)
                    {
                        level = 5;
                        sLevel = sLevel + "." + dtRow["L5"];
                    }

                    if (level == 1) {
                        i = Convert.ToInt32(dtRow["L1"]);
                        if (i > iMax1Level) iMax1Level = i;
                    }

                    fgList.Rows.InsertNode(row, level);
                    fgList[row, 0] = sLevel + " " + dtRow["Title"];
                    fgList[row, 1] = dtRow["ID"];
                    fgList[row, 2] = dtRow["L1"];
                    fgList[row, 3] = dtRow["L2"];
                    fgList[row, 4] = dtRow["L3"];
                    fgList[row, 5] = dtRow["L4"];
                    fgList[row, 6] = dtRow["L5"];
                    fgList[row, 7] = dtRow["Title"];
                    fgList[row, 8] = level;
                    fgList[row, 9] = 0; // dtRow["Terminal")
                }
            }

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
                fgList.Rows[i].Node.Collapsed = bCollapsed;

            fgList.Redraw = true;
            tsbCollapse.Visible = false;
            tsbExtend.Visible = true;
            bCheckList = true;
        }
        private void ShowRecord()
        {
            txtTitle.Text = fgList[fgList.Row, 1] + "";
            txtL1.Text = fgList[fgList.Row, 2] + "";
            txtL2.Text = fgList[fgList.Row, 3] + "";
            txtL3.Text = fgList[fgList.Row, 4] + "";
            txtL4.Text = fgList[fgList.Row, 5] + "";
            txtL5.Text = fgList[fgList.Row, 6] + "";
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
