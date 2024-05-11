using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class ucClientSearch : UserControl
    {
        int iMode, iShowWidth, iShowHeight, iListType, iMaxWidth, iMaxHeight, iLocClient_ID, iOldClient_ID;
        string sTemp, sFilters, sSurnameGreek = "", sSurnameEnglish = "";
        bool bShowList;
        DataView dtView;
        CellStyle csCancel;

        public event EventHandler TextOfLabelChanged;
        public ucClientSearch()
        {
            InitializeComponent();
        }

        private void ucClientSearch_Load(object sender, EventArgs e)
        {
            if (iShowWidth != 0) txtClientName.Width = iShowWidth;
            if (iShowHeight != 0) txtClientName.Height = iShowHeight;

            this.Width = txtClientName.Width;
            this.Height = txtClientName.Height;

            iOldClient_ID = 0;
            bShowList = true;
            sTemp = "";
            sFilters = "ID > 0";

            //------- fgList ----------------------------
            fgList.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Pink; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Pink; ForeColor:Black;}");
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

        }
        public void StartInit(int iWidth, int iHeight, int iTxtWidth, int iTxtHeight, int iShownListType)
        {
            bShowList = false;
            txtClientName.Text = "";
            bShowList = true;

            iMaxWidth = iWidth;
            iMaxHeight = iHeight;
            iShowWidth = iTxtWidth;
            iShowHeight = iTxtHeight;
            iListType = iShownListType;

            Client_ID.Text = "-999";

            if (iShowWidth != 0) txtClientName.Width = iShowWidth;
            if (iShowHeight != 0) txtClientName.Height = iShowHeight;

            this.Width = txtClientName.Width;
            this.Height = txtClientName.Height;
        }
        protected override void OnResize(EventArgs e)
        {
            panList.Width = this.Width - 1;
            panList.Height = this.Height - 22;

            fgList.Height = this.Height - 60;
            fgList.Width = this.Width - 15;
            fgList.Cols[0].Width = fgList.Width - 100;

            picClose.Left = this.Width - 26;
        }
        private void txtContractTitle_TextChanged(object sender, EventArgs e)
        {
            if (bShowList)
            {
                this.Width = iMaxWidth;
                this.Height = iMaxHeight;
                DataFiltering();
            }
        }
        private void DataFiltering()
        {

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            Global.TranslateUserName(txtClientName.Text, out sSurnameGreek, out sSurnameEnglish);

            switch (iListType)
            {
                case 1:
                    iOldClient_ID = -999;
                    dtView = Global.dtClients.DefaultView;
                    sTemp = sFilters + " AND (Fullname LIKE '%" + sSurnameEnglish + "%' OR Fullname LIKE '%" + sSurnameGreek + "%')";
                    dtView.RowFilter = sTemp;

                    foreach (DataRowView dtViewRow in dtView)
                    {

                        iLocClient_ID = Convert.ToInt32(dtViewRow["ID"]);

                        if (iOldClient_ID != iLocClient_ID)
                        {
                            iOldClient_ID = iLocClient_ID;

                            fgList.AddItem(dtViewRow["Fullname"] + "\t" + dtViewRow["FirstnameFather"] + "\t" + dtViewRow["ADT"] + "\t" + dtViewRow["DOY"] + "\t" +
                                           dtViewRow["AFM"] + "\t" + dtViewRow["DOY2"] + "\t" + dtViewRow["AFM2"] + "\t" + dtViewRow["DoB"] + "\t" + 
                                           dtViewRow["SpecialTitle"] + "\t" + dtViewRow["ID"] + "\t" + dtViewRow["Status"]);
                        }
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

            fgList.Sort(SortFlags.Ascending, 0);
            fgList.Redraw = true;
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 10)
                if (Convert.ToInt32(fgList[e.Row, 10]) == 0) fgList.Rows[e.Row].Style = csCancel;
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            ClientSearch();
        }
        private void ClientSearch()
        {
            if (txtClientName.Text.Length > 0) DefineClientData();
            else Client_ID.Text = "0";

            this.Width = txtClientName.Width;
            this.Height = txtClientName.Height;
        }
        private void DefineClientData()
        {   
            bShowList = false;
            txtClientName.Text = fgList[fgList.Row, "Fullname"].ToString();
            bShowList = true;
            Client_ID.Text = fgList[fgList.Row, "ID"].ToString();
        }
        public void Client_ID_TextChanged(object sender, EventArgs e)
        {
            if (TextOfLabelChanged != null) TextOfLabelChanged(this, e);
        }   
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Width = txtClientName.Width;
            this.Height = txtClientName.Height;
        }
        public bool ShowClientsList { get { return this.bShowList; } set { this.bShowList = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int ShowWidth { get { return this.iShowWidth; } set { this.iShowWidth = value; } }
        public int ShowHeight { get { return this.iShowHeight; } set { this.iShowHeight = value; } }
        public string Filters { get { return this.sFilters; } set { this.sFilters = value; } }
        public int ListType { get { return this.iListType; } set { this.iListType = value; } }                     // 1 - Contracts, 2 - HF SS Codes, 3 - HF Accounts
    }
}
