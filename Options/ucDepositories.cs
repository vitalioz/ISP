using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace Options
{
    public partial class ucDepositories : UserControl
    {
        int i, iID, iAction, iAddMode, iRightsLevel;
        bool bCheckList;
        SortedList lstProviders = new SortedList();
        DataView dtView;
        clsDepositories Depositories = new clsDepositories();
        clsDepositories_Alias Depositories_Alias = new clsDepositories_Alias();
        public ucDepositories()
        {
            InitializeComponent();
        }

        private void ucDepositories_Load(object sender, EventArgs e)
        {
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgAliases ----------------------------
            fgAliases.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAliases.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, BackColor:LightSteelBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, BackColor:Transparent; ForeColor:Black;}");
            fgAliases.CellChanged += new RowColEventHandler(fgAliases_CellChanged);
        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Height = this.Height - 6;
            grpData.Width = this.Width - 20;
            fgList.Height = this.Height - 92;
        }
        public void StartInit(int iParentList_ID, int iParentCashTables_ID, string sParentTableName, string sParentListTitle)
        {
            lblListItemTitle.Text = sParentListTitle;

            //-------------- Define Countries List ------------------
            dtView = Global.dtCountries.Copy().DefaultView;
            dtView.RowFilter = "Tipos = 1";
            cmbCountry.DataSource = dtView;
            cmbCountry.DisplayMember = "Title";
            cmbCountry.ValueMember = "ID";

            //-------------- Define Providers List ------------------
            lstProviders.Clear();
            foreach (DataRow dtRow in Global.dtServiceProviders.Rows)
                lstProviders.Add(dtRow["ID"], dtRow["Title"]);

            fgAliases.Cols[0].DataMap = lstProviders;

            DefineList();

            if (fgList.Rows.Count > 1)
            {
                fgList.Row = 1;
                ShowRecord();
            }
            if (iRightsLevel == 1) toolLeft.Enabled = false;
        }
        private void DefineList()
        {
            try
            {
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                Depositories = new clsDepositories();
                Depositories.GetList();
                foreach (DataRow dtRow in Depositories.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["ID"]) != 0)
                       fgList.AddItem(dtRow["Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["ID"]);
                }

                fgList.Redraw = true;
                bCheckList = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            if (bCheckList)
                if (fgList.Row > 0) ShowRecord();
        }
        private void ShowRecord()
        {
            Depositories = new clsDepositories();
            Depositories.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            Depositories.GetRecord();
            txtTitle.Text = Depositories.Title;
            txtCode.Text = Depositories.Code;
            txtBIC.Text = Depositories.BIC;
            cmbCountry.SelectedValue = Depositories.Country_ID;

            fgAliases.Redraw = false;
            fgAliases.Rows.Count = 1;

            Depositories_Alias = new clsDepositories_Alias();
            Depositories_Alias.Item_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            Depositories_Alias.GetList();
            foreach (DataRow dtRow in Depositories_Alias.List.Rows)
            {
                fgAliases.AddItem(dtRow["ServiceProvider_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["ID"] + "\t" + dtRow["ServiceProvider_ID"]);
            }
            fgAliases.Redraw = true;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;                                       // 0 - Add
            iAddMode = 1;
            txtTitle.Text = "";
            txtCode.Text = "";
            txtBIC.Text = "";
            cmbCountry.SelectedValue = 0;
            fgAliases.Rows.Count = 1;
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle.Focus();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;                                         // 1 - Add
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle.Focus();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
                if (fgAliases.Rows.Count == 1)
                {
                    if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Depositories = new clsDepositories();
                        Depositories.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        Depositories.DeleteRecord();
                        fgList.RemoveItem(fgList.Row);

                        if (fgList.Rows.Count > 1)
                        {
                            fgList.Focus();
                            iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        }
                        fgList.Redraw = true;

                        iAction = 1;                                                          // 1 - EDIT Mode
                        ShowRecord();
                    }
                }
                else MessageBox.Show("Can't delete stock exchange", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picAdd_Alias_Click(object sender, EventArgs e)
        {
            fgAliases.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0");
        }
        private void fgAliases_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) fgAliases[e.Row, 3] = fgAliases[e.Row, 0];
        }
        private void picDel_Alias_Click(object sender, EventArgs e)
        {
            if (fgAliases.Row > 1)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Depositories_Alias = new clsDepositories_Alias();
                    Depositories_Alias.Record_ID = Convert.ToInt32(fgAliases[fgAliases.Row, "ID"]);
                    Depositories_Alias.DeleteRecord();
                    fgAliases.RemoveItem(fgAliases.Row);
                }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length != 0)
            {
                if (iAction == 0)
                {                                    // 0 - ADD Mode
                    Depositories = new clsDepositories();
                    Depositories.Title = txtTitle.Text;
                    Depositories.Code = txtCode.Text;
                    Depositories.BIC = txtBIC.Text;
                    Depositories.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                    iID = Depositories.InsertRecord();

                    if (iAddMode == 1)
                    {
                        Depositories = new clsDepositories();
                        Depositories.Record_ID = iID;
                        Depositories.GetRecord();
                        Depositories.Title = txtTitle.Text;
                        Depositories.Code = txtCode.Text;
                        Depositories.BIC = txtBIC.Text;
                        Depositories.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                        Depositories.EditRecord();
                    }
                }
                else
                {
                    iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);

                    Depositories = new clsDepositories();
                    Depositories.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    Depositories.GetRecord();
                    Depositories.Title = txtTitle.Text;
                    Depositories.Code = txtCode.Text;
                    Depositories.BIC = txtBIC.Text;
                    Depositories.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                    Depositories.EditRecord();

                    for (i = 1; i <= fgAliases.Rows.Count - 1; i++)
                    {
                        if (Convert.ToInt32(fgAliases[i, "ID"]) == 0)
                        {
                            Depositories_Alias = new clsDepositories_Alias();
                            Depositories_Alias.Item_ID = iID;
                            Depositories_Alias.ServiceProvider_ID = Convert.ToInt32(fgAliases[i, "ServiceProvider_ID"]);
                            Depositories_Alias.Code = fgAliases[i, "Code"] + "";
                            Depositories_Alias.InsertRecord();
                        }
                        else
                        {
                            Depositories_Alias = new clsDepositories_Alias();
                            Depositories_Alias.Record_ID = Convert.ToInt32(fgAliases[i, "ID"]);
                            Depositories_Alias.GetRecord();
                            Depositories_Alias.ServiceProvider_ID = Convert.ToInt32(fgAliases[i, "ServiceProvider_ID"]);
                            Depositories_Alias.Code = fgAliases[i, "Code"] + "";
                            Depositories_Alias.EditRecord();
                        }
                    }

                }
                panDetails.Enabled = false;
                DefineList();

                i = fgList.FindRow(iID, 1, 2, false);
                if (i > 0) fgList.Row = i;
                else fgList.Row = 1;

                ShowRecord();
            }
            else MessageBox.Show("Η εισαγωγή του τίτλου είναι υποχρεωτική", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
