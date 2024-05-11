using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmClientsSearch : Form
    {
        int i, j, iRightsLevel;
        string sSpecials;
        string[] sCategory = { "ΙΔΙΩΤΗΣ", "ΕΤΑΙΡΕΙΑ", "ΘΕΣΜΙΚΟΣ", "" };
        CellStyle csCancel;
        clsClients Clients = new clsClients();
        public frmClientsSearch()
        {
            InitializeComponent();

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;
        }

        private void frmClientsSearch_Load(object sender, EventArgs e)
        {
            cmbTypos.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 3;

            cmbCitizen.DataSource = Global.dtCountries.Copy();
            cmbCitizen.DisplayMember = "Title";
            cmbCitizen.ValueMember = "ID";
            cmbCitizen.SelectedValue = 0;

            cmbXora.DataSource = Global.dtCountries.Copy();
            cmbXora.DisplayMember = "Title";
            cmbXora.ValueMember = "ID";
            cmbXora.SelectedValue = 0;

            cmbSpecial.DataSource = Global.dtSpecials.Copy();
            cmbSpecial.DisplayMember = "Title";
            cmbSpecial.ValueMember = "ID";
            cmbSpecial.SelectedValue = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_CellChanged);

            sSpecials = ",";
            for (j = 1; j <= fgSpecials.Rows.Count - 1; j++)
                if (Convert.ToBoolean(fgSpecials[j, 0]))
                    sSpecials = sSpecials + fgSpecials[j, 2] + ",";

            if (sSpecials == ",") sSpecials = "";
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = this.Width - 144;
            fgList.Width = this.Width - 24;
            fgList.Height = this.Height - 180;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            i = 0;
            switch (Convert.ToInt32(cmbTypos.SelectedIndex))
            {
                case 0:
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[8].Visible = true;
                    fgList.Cols[9].Visible = true;
                    fgList.Cols[10].Visible = true;
                    fgList.Cols[11].Visible = true;
                    fgList.Cols[13].Visible = true;
                    fgList.Cols[14].Visible = true;
                    fgList.Cols[15].Visible = true;
                    fgList.Cols[16].Visible = true;
                    fgList.Cols[17].Visible = true;
                    fgList.Cols[18].Visible = false;
                    fgList.Cols[19].Visible = false;

                    fgList.Redraw = false;
                    fgList.Rows.Count = 1;

                    Clients = new clsClients();
                    Clients.Surname = txtSurname.Text;
                    Clients.Firstname = txtFirstname.Text;
                    Clients.Category = cmbCategory.SelectedIndex;
                    Clients.Country_ID = Convert.ToInt32(cmbXora.SelectedValue);
                    Clients.Citizen_ID = Convert.ToInt32(cmbCitizen.SelectedValue);
                    Clients.Risk = cmbRisk.SelectedIndex;
                    Clients.AFM = txtAFM.Text;
                    Clients.GetList();
                    foreach (DataRow dtRow in Clients.List.Rows)
                    {
                        if (!chkAktive.Checked || (Convert.ToInt32(dtRow["Status"]) != 0))
                        {
                            i = i + 1;
                            fgList.AddItem(i + "\t" + dtRow["Surname"] + " " + dtRow["Firstname"] + "\t" + dtRow["FirstnameFather"] + "\t" + dtRow["Group"] + "\t" +
                                           sCategory[Convert.ToInt16(dtRow["Category"])] + "\t" + dtRow["DoB"] + "\t" + dtRow["ADT"] + "\t" + dtRow["DOY"] + "\t" +
                                           dtRow["AFM"] + "\t" + dtRow["Address"] + "\t" + dtRow["Zip"] + "\t" + dtRow["City"] + "\t" + dtRow["Country_Title"] + "\t" +
                                           dtRow["Tel"] + "\t" + dtRow["Fax"] + "\t" + dtRow["Mobile"] + "\t" + dtRow["EMail"] + "\t" + dtRow["Spec_Title"] + "\t" +
                                           dtRow["CountryTaxes_Title"] + "\t" + dtRow["Citizen_Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Status"]);
                        }
                    }
                    fgList.Redraw = true;
                    break;
                case 1:
                    fgList.Cols[3].Visible = false;
                    fgList.Cols[5].Visible = false;
                    fgList.Cols[6].Visible = false;
                    fgList.Cols[7].Visible = false;
                    fgList.Cols[8].Visible = false;
                    fgList.Cols[9].Visible = false;
                    fgList.Cols[10].Visible = false;
                    fgList.Cols[11].Visible = false;
                    fgList.Cols[13].Visible = false;
                    fgList.Cols[14].Visible = false;
                    fgList.Cols[15].Visible = false;
                    fgList.Cols[16].Visible = false;
                    fgList.Cols[17].Visible = false;
                    fgList.Cols[18].Visible = true;
                    fgList.Cols[19].Visible = true;

                    fgList.Redraw = false;
                    fgList.Rows.Count = 1;

                    Clients = new clsClients();
                    Clients.Surname = txtSurname.Text;
                    Clients.Firstname = txtFirstname.Text;
                    Clients.Category = cmbCategory.SelectedIndex;
                    Clients.Country_ID = Convert.ToInt32(cmbXora.SelectedValue);
                    Clients.Citizen_ID = Convert.ToInt32(cmbCitizen.SelectedValue);
                    Clients.Risk = cmbRisk.SelectedIndex;
                    Clients.AFM = txtAFM.Text;
                    Clients.GetList();
                    foreach (DataRow dtRow in Clients.List.Rows)
                    {
                        if (!chkAktive.Checked || (Convert.ToInt32(dtRow["Status"]) != 0))
                        {
                            i = i + 1;
                            fgList.AddItem(i + "\t" + dtRow["Surname"] + " " + dtRow["Firstname"] + "\t" + dtRow["FirstnameFather"] + "\t" + dtRow["Group"] + "\t" +
                                           sCategory[Convert.ToInt16(dtRow["Category"])] + "\t" + dtRow["DoB"] + "\t" + dtRow["ADT"] + "\t" + dtRow["DOY"] + "\t" +
                                           dtRow["AFM"] + "\t" + dtRow["Address"] + "\t" + dtRow["Zip"] + "\t" + dtRow["City"] + "\t" + dtRow["Country_Title"] + "\t" +
                                           dtRow["Tel"] + "\t" + dtRow["Fax"] + "\t" + dtRow["Mobile"] + "\t" + dtRow["EMail"] + "\t" + dtRow["Spec_Title"] + "\t" +
                                           dtRow["CountryTaxes_Title"] + "\t" + dtRow["Citizen_Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Status"]);
                        }
                    }
                    fgList.Redraw = true;
                    break;
            }


        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US"]
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Αναζήτηση Πελατών";

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            for (i = 0; i <= (fgList.Rows.Count - 1); i++)
                for (this.j = 2; this.j <= (fgList.Cols.Count - 1); this.j++)
                    if (fgList.Cols[j].Visible)
                        EXL.Cells[i + 2, j - 1].Value = fgList[i, j - 1];

            this.Cursor = Cursors.Default;

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1)
            {
                if (Convert.ToInt32(fgList[e.Row, "Status"]) == 0) fgList.Rows[e.Row].Style = csCancel;
                else fgList.Rows[e.Row].Style = null;
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
