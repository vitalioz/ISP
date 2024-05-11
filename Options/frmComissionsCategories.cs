using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class frmComissionsCategories : Form
    {
        int i, iRightsLevel;
        string sExtra;
        bool bCheckGrid;
        clsProductsCategories clsProductsCategories = new clsProductsCategories();
        public frmComissionsCategories()
        {
            InitializeComponent();
        }

        private void frmComissionsCategories_Load(object sender, EventArgs e)
        {
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgLogs ----------------------------
            fgCategories.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCategories.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");


            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            foreach (DataRow dtRow in Global.dtProductTypes.Copy().Rows)
            {
                if ((Convert.ToInt32(dtRow["ID"]) != 0) && (Convert.ToInt32(dtRow["CalcFees"]) == 1))
                   fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"]);
            }

            fgList.Redraw = true;
            bCheckGrid = true;
            fgList.Row = 1;
            DefineCategoriesList();

            if (iRightsLevel == 1) {
                tsbAdd.Enabled = false;
                tsbDelete.Enabled = false;
                tsbSave.Enabled = false;
            }
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                lblTitles.Text = "";
                i = fgList.Row;
                if (i > 0) {
                    lblTitles.Text = fgList[fgList.Row, 0]+"";
                    DefineCategoriesList();
                }
            }
        }
        private void DefineCategoriesList()
        {
            fgCategories.Redraw = false;
            fgCategories.Rows.Count = 1;

            foreach (DataRow dtRow in Global.dtProductsCategories.Copy().Rows)
            {
                if (Convert.ToInt32(fgList[fgList.Row, 1]) == Convert.ToInt32(dtRow["Product_ID"]))
                    fgCategories.AddItem(dtRow["Title"] + "\t" + dtRow["ID"]);
            }
            fgCategories.Redraw = true;

            if (fgList.Rows.Count > 0) lblTitles.Text = fgList[i, 0]+"";
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            fgCategories.AddItem("" + "\t" + "0");
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgCategories.Row > 0)

                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsProductsCategories = new clsProductsCategories();
                    clsProductsCategories.Record_ID = Convert.ToInt32(fgCategories[fgCategories.Row, "ID"]);
                    clsProductsCategories.DeleteRecord();

                    Global.GetProductCategories();
                    DefineCategoriesList();
                }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            for (i=1; i < fgCategories.Rows.Count - 1; i++)
            {

            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
