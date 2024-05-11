using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Globalization;
using Core;

namespace Accounting
{
    public partial class frmAcc_Contracts : Form
    {
        int i, iFT_ID, iAT_ID, iIndex, iOldContract_ID, iOldContract_Packages_ID, iRightsLevel;
        int iContract_ID, iContract_Details_ID, iContract_Packages_ID;
        int iBrokerageOption_ID, iRTOOption_ID, iAdvisoryOption_ID, iAdvisoryProvider_ID, iDiscretOption_ID, iDiscretProvider_ID;
        int iAdminOption_ID, iAdminProvider_ID, iDealAdvisoryOption_ID, iDealAdvisoryProvider_ID, iCustodyOption_ID, iCustodyProvider_ID;
        int iLombardOption_ID, iLombardProvider_ID, iFXOption_ID, iFXProvider_ID, iSettlementsOption_ID;
        int iAdvisoryInvestmentProfile_ID, iAdvisoryInvestmentPolicy_ID, iDealAdvisoryInvestmentPolicy_ID, iDiscretInvestmentProfile_ID, iDiscretInvestmentPolicy_ID;
        string sTemp, sServicesList = "", sExtra;
        DateTime dTemp, dStart, dFinish, dPackageDateStart, dPackageDateFinish;
        bool bCheckList = false;
        bool bCheckPackages = false;
        SortedList lstCurr = new SortedList();
        DataView dtView;
        C1.Win.C1FlexGrid.CellRange rng;

        clsContracts Contracts = new clsContracts();

        public frmAcc_Contracts()
        {
            InitializeComponent();

            panTools.Visible = false;
            fgList.Visible = false;
            tcFees.Visible = false;
        }
        private void frmAcc_Contracts_Load(object sender, EventArgs e)
        {
            ucDates.DateFrom = DateTime.Now;
            ucDates.Left = 572;
            ucDates.Top = 12;

            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);
            cmbYear.Text = DateTime.Now.Year.ToString();

            DefineLastQ();

            cmbServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedItem = 1;

            //-------------- Define Advisorys List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";

            //-------------- Define RMs List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "RM = 1";
            cmbRM.DataSource = dtView;
            cmbRM.DisplayMember = "Title";
            cmbRM.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            //------- fgServices ----------------------------
            sServicesList = ",";
            fgServices.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgServices.Styles.ParseString(Global.GridStyle);
            fgServices.Redraw = false;
            foreach (DataRow dtRow in Global.dtServices.Rows)
            {
                if (Convert.ToInt32(dtRow["ID"]) != 0)
                {
                    fgServices.AddItem(true + "\t" + dtRow["Title"] + "\t" + dtRow["ID"]);
                }
            }
            fgServices.Redraw = true;
            DefineServicesList();

            //------- fgBrokerageFees ----------------------------
            fgBrokerageFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBrokerageFees.Styles.ParseString(Global.GridStyle);
            fgBrokerageFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgBrokerageFees.ShowCellLabels = true;
            fgBrokerageFees.Styles.Normal.WordWrap = true;
            fgBrokerageFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgBrokerageFees.Rows[0].AllowMerging = true;


            fgBrokerageFees.Cols[0].AllowMerging = true;
            rng = fgBrokerageFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Προϊον";

            fgBrokerageFees.Cols[1].AllowMerging = true;
            rng = fgBrokerageFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Κατηγορία";

            fgBrokerageFees.Cols[2].AllowMerging = true;
            rng = fgBrokerageFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Χρηματιστήριο";

            rng = fgBrokerageFees.GetCellRange(0, 3, 0, 4);
            rng.Data = "Κλίμακα";

            fgBrokerageFees[1, 3] = "από";
            fgBrokerageFees[1, 4] = "εώς";

            rng = fgBrokerageFees.GetCellRange(0, 5, 0, 6);
            rng.Data = "Προμήθεια συναλλαγής";

            fgBrokerageFees[1, 5] = "αγοράς";
            fgBrokerageFees[1, 6] = "πώλησης";

            rng = fgBrokerageFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgBrokerageFees[1, 7] = "αγοράς";
            fgBrokerageFees[1, 8] = "πώλησης";
            fgBrokerageFees[1, 9] = "Νόμισμα";

            rng = fgBrokerageFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgBrokerageFees[1, 10] = "Ποσό";
            fgBrokerageFees[1, 11] = "Νόμισμα";

            rng = fgBrokerageFees.GetCellRange(0, 12, 0, 15);
            rng.Data = "Έκπτωση προμήθειας";

            fgBrokerageFees[1, 12] = "Ημερ.από";
            fgBrokerageFees[1, 13] = "Ημερ.εώς";
            fgBrokerageFees[1, 14] = "% Έκπτωσης";
            fgBrokerageFees[1, 15] = "% Ticket Fees";

            rng = fgBrokerageFees.GetCellRange(0, 16, 0, 17);
            rng.Data = "Τελική προμήθεια";

            fgBrokerageFees[1, 16] = "αγοράς";
            fgBrokerageFees[1, 17] = "πώλησης";

            rng = fgBrokerageFees.GetCellRange(0, 18, 0, 19);
            rng.Data = "Τελικό Ticket Fees";

            fgBrokerageFees[1, 18] = "αγοράς";
            fgBrokerageFees[1, 19] = "πώλησης";


            //------- fgRTOFees ----------------------------
            fgRTOFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRTOFees.Styles.ParseString(Global.GridStyle);
            fgRTOFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgRTOFees.ShowCellLabels = true;
            fgRTOFees.Styles.Normal.WordWrap = true;
            fgRTOFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgRTOFees.Rows[0].AllowMerging = true;

            fgRTOFees.Cols[0].AllowMerging = true;
            rng = fgRTOFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Προϊον";

            fgRTOFees.Cols[1].AllowMerging = true;
            rng = fgRTOFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Κατηγορία";

            fgRTOFees.Cols[2].AllowMerging = true;
            rng = fgRTOFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Χρηματιστήριο";

            rng = fgRTOFees.GetCellRange(0, 3, 0, 4);
            rng.Data = "Κλίμακα";

            fgRTOFees[1, 3] = "από";
            fgRTOFees[1, 4] = "εώς";

            rng = fgRTOFees.GetCellRange(0, 5, 0, 6);
            rng.Data = "Προμήθεια συναλλαγής";

            fgRTOFees[1, 5] = "αγοράς";
            fgRTOFees[1, 6] = "πώλησης";

            rng = fgRTOFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgRTOFees[1, 7] = "αγοράς";
            fgRTOFees[1, 8] = "πώλησης";
            fgRTOFees[1, 9] = "Νόμισμα";

            rng = fgRTOFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgRTOFees[1, 10] = "Ποσό";
            fgRTOFees[1, 11] = "Νόμισμα";

            rng = fgRTOFees.GetCellRange(0, 12, 0, 16);
            rng.Data = "Έκπτωση προμήθειας";

            fgRTOFees[1, 12] = "Ημερ.από";
            fgRTOFees[1, 13] = "Ημερ.εώς";
            fgRTOFees[1, 14] = "% Έκπτωσης";
            fgRTOFees[1, 15] = "% Ticket Fees";
            fgRTOFees[1, 16] = "% Min.Fees";

            rng = fgRTOFees.GetCellRange(0, 17, 0, 18);
            rng.Data = "Τελική προμήθεια";

            fgRTOFees[1, 17] = "αγοράς";
            fgRTOFees[1, 18] = "πώλησης";

            rng = fgRTOFees.GetCellRange(0, 19, 0, 20);
            rng.Data = "Τελικό Ticket Fees";

            fgRTOFees[1, 19] = "αγοράς";
            fgRTOFees[1, 20] = "πώλησης";

            fgRTOFees.Cols[21].AllowMerging = true;
            rng = fgRTOFees.GetCellRange(0, 21, 1, 21);
            rng.Data = "Τελικο Min.Fees";

            //------- fgAdvisoryFees ----------------------------
            fgAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdvisoryFees.Styles.ParseString(Global.GridStyle);
            fgAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdvisoryFees.ShowCellLabels = true;

            fgAdvisoryFees.Styles.Normal.WordWrap = true;
            fgAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdvisoryFees.Rows[0].AllowMerging = true;

            rng = fgAdvisoryFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgAdvisoryFees[1, 0] = "από";
            fgAdvisoryFees[1, 1] = "εώς";

            rng = fgAdvisoryFees.GetCellRange(0, 2, 0, 6);
            rng.Data = "Managment Fees";

            fgAdvisoryFees[1, 2] = "Αμοιβή";
            fgAdvisoryFees[1, 3] = "Ημερ.από";
            fgAdvisoryFees[1, 4] = "Ημερ.εώς";
            fgAdvisoryFees[1, 5] = "% Έκπτωσης";
            fgAdvisoryFees[1, 6] = "Τελική Αμοιβή";

            //------- fgDiscretFees ----------------------------
            fgDiscretFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDiscretFees.Styles.ParseString(Global.GridStyle);
            fgDiscretFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDiscretFees.ShowCellLabels = true;

            fgDiscretFees.Styles.Normal.WordWrap = true;
            fgDiscretFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDiscretFees.Rows[0].AllowMerging = true;

            rng = fgDiscretFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgDiscretFees[1, 0] = "από";
            fgDiscretFees[1, 1] = "εώς";

            rng = fgDiscretFees.GetCellRange(0, 2, 0, 6);
            rng.Data = "Managment Fees";

            fgDiscretFees[1, 2] = "Αμοιβή";
            fgDiscretFees[1, 3] = "Ημερ.από";
            fgDiscretFees[1, 4] = "Ημερ.εώς";
            fgDiscretFees[1, 5] = "% Έκπτωσης";
            fgDiscretFees[1, 6] = "Τελική Αμοιβή";

            //------- fgCustodyFees1 ----------------------------
            fgCustodyFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCustodyFees.Styles.ParseString(Global.GridStyle);
            fgCustodyFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgCustodyFees.ShowCellLabels = true;

            fgCustodyFees.Styles.Normal.WordWrap = true;
            fgCustodyFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgCustodyFees.Rows[0].AllowMerging = true;

            rng = fgCustodyFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgCustodyFees[1, 0] = "από";
            fgCustodyFees[1, 1] = "εώς";

            fgCustodyFees.Cols[2].AllowMerging = true;
            rng = fgCustodyFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgCustodyFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgCustodyFees[1, 3] = "Ημερ.από";
            fgCustodyFees[1, 4] = "Ημερ.εώς";
            fgCustodyFees[1, 5] = "% Έκπτωσης";

            fgCustodyFees.Cols[6].AllowMerging = true;
            rng = fgCustodyFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";          

            //------- fgAdminFees ----------------------------
            fgAdminFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdminFees.Styles.ParseString(Global.GridStyle);
            fgAdminFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdminFees.ShowCellLabels = true;

            fgAdminFees.Styles.Normal.WordWrap = true;
            fgAdminFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdminFees.Rows[0].AllowMerging = true;

            rng = fgAdminFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgAdminFees[1, 0] = "από";
            fgAdminFees[1, 1] = "εώς";

            fgAdminFees.Cols[2].AllowMerging = true;
            rng = fgAdminFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgAdminFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgAdminFees[1, 3] = "Ημερ.από";
            fgAdminFees[1, 4] = "Ημερ.εώς";
            fgAdminFees[1, 5] = "% Έκπτωσης";

            fgAdminFees.Cols[6].AllowMerging = true;
            rng = fgAdminFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            //------- fgDealAdvisoryFees ----------------------------
            fgDealAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDealAdvisoryFees.Styles.ParseString(Global.GridStyle);
            fgDealAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDealAdvisoryFees.ShowCellLabels = true;

            fgDealAdvisoryFees.Styles.Normal.WordWrap = true;
            fgDealAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDealAdvisoryFees.Rows[0].AllowMerging = true;

            rng = fgDealAdvisoryFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgDealAdvisoryFees[1, 0] = "από";
            fgDealAdvisoryFees[1, 1] = "εώς";

            fgDealAdvisoryFees.Cols[2].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgDealAdvisoryFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgDealAdvisoryFees[1, 3] = "Ημερ.από";
            fgDealAdvisoryFees[1, 4] = "Ημερ.εώς";
            fgDealAdvisoryFees[1, 5] = "% Έκπτωσης";

            fgDealAdvisoryFees.Cols[6].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            //------- fgSuccessFees ----------------------------
            fgSuccessFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSuccessFees.Styles.ParseString(Global.GridStyle);
            fgSuccessFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgSuccessFees.ShowCellLabels = true;

            fgSuccessFees.Styles.Normal.WordWrap = true;
            fgSuccessFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSuccessFees.Rows[0].AllowMerging = true;

            rng = fgSuccessFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgSuccessFees[1, 0] = "από";
            fgSuccessFees[1, 1] = "εώς";

            fgSuccessFees.Cols[2].AllowMerging = true;
            rng = fgSuccessFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgSuccessFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgSuccessFees[1, 3] = "Ημερ.από";
            fgSuccessFees[1, 4] = "Ημερ.εώς";
            fgSuccessFees[1, 5] = "% Έκπτωσης";

            fgSuccessFees.Cols[6].AllowMerging = true;
            rng = fgSuccessFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            //------- fgLombardFees ----------------------------
            fgLombardFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgLombardFees.Styles.ParseString(Global.GridStyle);

            //------- fgFXFees1 ----------------------------
            fgFXFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFXFees.Styles.ParseString(Global.GridStyle);
            fgFXFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgFXFees.ShowCellLabels = true;
            fgFXFees.Styles.Normal.WordWrap = true;
            fgFXFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            fgFXFees.Rows[0].AllowMerging = true;

            rng = fgFXFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgFXFees[1, 0] = "από";
            fgFXFees[1, 1] = "εώς";

            fgFXFees.Cols[2].AllowMerging = true;
            rng = fgFXFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgFXFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgFXFees[1, 3] = "Ημερ.από";
            fgFXFees[1, 4] = "Ημερ.εώς";
            fgFXFees[1, 5] = "% Έκπτωσης";

            fgFXFees.Cols[6].AllowMerging = true;
            rng = fgFXFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            //------- fgSettlementsFees ----------------------------
            fgSettlementsFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSettlementsFees.Styles.ParseString(Global.GridStyle);
            fgSettlementsFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgSettlementsFees.ShowCellLabels = true;

            fgSettlementsFees.Styles.Normal.WordWrap = true;
            fgSettlementsFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSettlementsFees.Rows[0].AllowMerging = true;

            fgSettlementsFees.Cols[0].AllowMerging = true;
            rng = fgSettlementsFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Προϊον";

            fgSettlementsFees.Cols[1].AllowMerging = true;
            rng = fgSettlementsFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Κατηγορία";

            fgSettlementsFees.Cols[2].AllowMerging = true;
            rng = fgSettlementsFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αποθετήριο";

            rng = fgSettlementsFees.GetCellRange(0, 3, 0, 4);
            rng.Data = "Κλίμακα";

            fgSettlementsFees[1, 3] = "από";
            fgSettlementsFees[1, 4] = "εώς";

            rng = fgSettlementsFees.GetCellRange(0, 5, 0, 6);
            rng.Data = "Προμήθεια συναλλαγής";

            fgSettlementsFees[1, 5] = "αγοράς";
            fgSettlementsFees[1, 6] = "πώλησης";

            rng = fgSettlementsFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgSettlementsFees[1, 7] = "αγοράς";
            fgSettlementsFees[1, 8] = "πώλησης";
            fgSettlementsFees[1, 9] = "Νόμισμα";

            rng = fgSettlementsFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgSettlementsFees[1, 10] = "Ποσό";
            fgSettlementsFees[1, 11] = "Νόμισμα";

            rng = fgSettlementsFees.GetCellRange(0, 12, 0, 15);
            rng.Data = "Έκπτωση προμήθειας";

            fgSettlementsFees[1, 12] = "Ημερ.από";
            fgSettlementsFees[1, 13] = "Ημερ.εώς";
            fgSettlementsFees[1, 14] = "% Έκπτωσης";
            fgSettlementsFees[1, 15] = "% Ticket Fees";

            rng = fgSettlementsFees.GetCellRange(0, 16, 0, 17);
            rng.Data = "Τελική προμήθεια";

            fgSettlementsFees[1, 16] = "αγοράς";
            fgSettlementsFees[1, 17] = "πώλησης";

            rng = fgSettlementsFees.GetCellRange(0, 18, 0, 19);
            rng.Data = "Τελικό Ticket Fees";

            fgSettlementsFees[1, 18] = "αγοράς";
            fgSettlementsFees[1, 19] = "πώλησης";

            //------- fgCashAccounts ----------------------------
            fgCashAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCashAccounts.Styles.ParseString(Global.GridStyle);

            //------- fgAccounts ----------------------------
            fgAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAccounts.Styles.ParseString(Global.GridStyle);

            lstCurr.Clear();
            foreach (DataRow row in Global.dtCurrencies.Rows)
            {
                lstCurr.Add(row["Title"], row["Title"]);
            }
            fgCashAccounts.Cols[4].DataMap = lstCurr;


            //------ fgHistory ----------------------------
            fgHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgHistory.Styles.ParseString(Global.GridStyle);
            fgHistory.DrawMode = DrawModeEnum.OwnerDraw;
            fgHistory.ShowCellLabels = true;

            fgHistory.Styles.Normal.WordWrap = true;
            fgHistory.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgHistory.Rows[0].AllowMerging = true;

            rng = fgHistory.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ημερομηνία";

            fgHistory[1, 0] = "από";
            fgHistory[1, 1] = "εώς";

            rng = fgHistory.GetCellRange(0, 2, 0, 6);
            rng.Data = "Managment Fees";

            fgHistory[1, 2] = "Ποσό από";
            fgHistory[1, 3] = "Ποσό εώς";
            fgHistory[1, 4] = "Αμοιβή";
            fgHistory[1, 5] = "% Έκπτωσης";
            fgHistory[1, 6] = "Τελική Αμοιβή";

            rng = fgHistory.GetCellRange(0, 7, 0, 9);
            rng.Data = "Minimum Fees";

            fgHistory[1, 7] = "Αρχική Τιμή";
            fgHistory[1, 8] = "% 'Εκπτωσης";
            fgHistory[1, 9] = "Τελική Τιμή";

            cmbViewType.SelectedIndex = 0;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = this.Width - 144;
            fgList.Width = this.Width - 30;
            fgList.Height = this.Height - 560;
            tcFees.Top = this.Height - 448;
            tcFees.Height = 404;
            fgBrokerageFees.Height = tcFees.Height - 114;
            fgRTOFees.Height = tcFees.Height - 114;
            fgAdvisoryFees.Height = tcFees.Height - 138;
            fgDiscretFees.Height = tcFees.Height - 138;
        }

        private void DefineLastQ()
        {
            iIndex = (ucDates.DateFrom.Month + 2) / 3;
            if (iIndex == 1)
            {
                //iIndex = 4;
                //cmbYear.SelectedIndex = cmbYear.Items.Count - 2;
            }
            else
            {
                //iIndex = iIndex - 1;
                //cmbYear.SelectedIndex = cmbYear.Items.Count - 1;
            }

            switch (iIndex)
            {
                case 1:
                    rb1.Checked = true;
                    break;
                case 2:
                    rb2.Checked = true;
                    break;
                case 3:
                    rb3.Checked = true;
                    break;
                case 4:
                    rb4.Checked = true;
                    break;
            }
        }
        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fgList.Rows.Count = 1;
            fgList.Redraw = true;
            panTools.Visible = false;
            tcFees.Visible = false;

            switch (cmbViewType.SelectedIndex)
            {
                case 0:                                 // 0 - Geniki
                    panPeriod.Visible = false;
                    ucDates.Left = 572;
                    chkServices.Checked = true;
                    tsbSnapshot.Visible = false;
                    tss2.Visible = false;
                    break;
                case 1:                                 // 1 - Managment Fees
                    panPeriod.Left = 542;
                    panPeriod.Visible = true;
                    lblSemestr.Visible = false;
                    rb5.Visible = false;
                    rb6.Visible = false;
                    ucDates.Left = 1014;
                    chkServices.Checked = false;
                    fgServices[2, 0] = true;
                    fgServices[3, 0] = true;
                    fgServices[5, 0] = true;
                    tsbSnapshot.Visible = true;
                    tss2.Visible = true;
                    break;
                case 2:                                 // 2 - Admin Fees
                    panPeriod.Left = 542;
                    panPeriod.Visible = true;
                    lblSemestr.Visible = true;
                    rb5.Visible = true;
                    rb6.Visible = true;
                    ucDates.Left = 1014;
                    chkServices.Checked = true;
                    tsbSnapshot.Visible = true;
                    tss2.Visible = true;
                    break;
            }
            DefineLastQ();
            DefineServicesList();
        }
        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            iIndex = 1;
            dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }
        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            i = 2;
            dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            iIndex = 3;
            dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }

        private void mnuContractData_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            { 
                frmContract locContract = new frmContract();
                locContract.Aktion = 1;
                locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
                locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
                locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                //locContract.ClientType = 1;
                locContract.ClientFullName = fgList[fgList.Row, 1].ToString();
                locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
                locContract.ShowDialog();
            }
        }

        private void mnuClientData_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            {
                frmClientData locClientData = new frmClientData();
                locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                locClientData.Show();
            }
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            iIndex = 4;
            dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }
        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            iIndex = 1;
            dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }
        private void rb6_CheckedChanged(object sender, EventArgs e)
        {
            iIndex = 2;
            dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
            dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
            ucDates.DateFrom = dStart;
            ucDates.DateTo = dFinish;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineLastQ();
            DefineGridColumns();
            DefineServicesList();

            bCheckPackages = false;

            Contracts.PackageType = 1;
            Contracts.DateStart = ucDates.DateFrom;
            Contracts.DateFinish = ucDates.DateTo;
            Contracts.PackageProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
            Contracts.Client_ID = 0;
            Contracts.Advisor_ID = 0;
            Contracts.Service_ID = 0;
            Contracts.MiFID_2 = ((chkMiFID_2.Checked) ? 1 : 0);
            Contracts.Status = -1;
            Contracts.GetActualList();

            bCheckList = true;
            panTools.Visible = true;
            fgList.Visible = true;
            tcFees.Visible = true;
            fgList.Row = 0;
            ShowList();
            bCheckPackages = true;

            if (fgList.Rows.Count > 1)
            {
                fgList.Row = 1;
                fgList.Focus();
                ShowDetails();

                fgList.Visible = true;
                tcFees.Visible = true;
            }

        }
        private void tsbEditFees_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 1;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            sTemp = fgBrokerageFees[fgBrokerageFees.Row, 12].ToString();
            locContractDiscounts.dFrom1.Value = Convert.ToDateTime(((sTemp == "") ? "01/01/2010" : sTemp)); 
            sTemp = fgBrokerageFees[fgBrokerageFees.Row, 13].ToString();
            locContractDiscounts.dTo1.Value = Convert.ToDateTime(((sTemp == "") ? "31/12/2070" : sTemp));

            locContractDiscounts.lblBuy.Text = fgBrokerageFees[fgBrokerageFees.Row, 5].ToString();
            locContractDiscounts.txtBuyPercent.Text = fgBrokerageFees[fgBrokerageFees.Row, 14].ToString();
            locContractDiscounts.txtBuyFinish.Text = fgBrokerageFees[fgBrokerageFees.Row, 16].ToString();

            locContractDiscounts.lblSell.Text = fgBrokerageFees[fgBrokerageFees.Row, 6].ToString();
            locContractDiscounts.txtSellFinish.Text = fgBrokerageFees[fgBrokerageFees.Row, 17].ToString();

            locContractDiscounts.lblTicketFeesBuy.Text = fgBrokerageFees[fgBrokerageFees.Row, 7].ToString();
            locContractDiscounts.txtTicketFeesPercent.Text = fgBrokerageFees[fgBrokerageFees.Row, 15].ToString();
            locContractDiscounts.txtTicketFeesBuy.Text = fgBrokerageFees[fgBrokerageFees.Row, 18].ToString();
            locContractDiscounts.lblTicketFeesSell.Text = fgBrokerageFees[fgBrokerageFees.Row, 8].ToString();
            locContractDiscounts.txtTicketFeesSell.Text = fgBrokerageFees[fgBrokerageFees.Row, 19].ToString();
            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgBrokerageFees[fgBrokerageFees.Row, 12] = locContractDiscounts.dFrom1.Value.ToString("d");
                fgBrokerageFees[fgBrokerageFees.Row, 13] = locContractDiscounts.dTo1.Value.ToString("d");
                fgBrokerageFees[fgBrokerageFees.Row, 14] = locContractDiscounts.txtBuyPercent.Text;
                fgBrokerageFees[fgBrokerageFees.Row, 15] = locContractDiscounts.txtTicketFeesPercent.Text;
                fgBrokerageFees[fgBrokerageFees.Row, 16] = locContractDiscounts.txtBuyFinish.Text;
                fgBrokerageFees[fgBrokerageFees.Row, 17] = locContractDiscounts.txtSellFinish.Text;
                fgBrokerageFees[fgBrokerageFees.Row, 18] = locContractDiscounts.txtTicketFeesBuy.Text;
                fgBrokerageFees[fgBrokerageFees.Row, 19] = locContractDiscounts.txtTicketFeesSell.Text;
            }
        }
        private void tsbEditMultiBrokerageFees_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.dFrom11.Value = ucDates.DateFrom;
            locContractDiscounts.dTo11.Value = ucDates.DateTo;
            locContractDiscounts.txtFeesDiscount11.Text = "0";
            locContractDiscounts.txtTicketFeesDiscount11.Text = "0";
            locContractDiscounts.Mode = 11;
            locContractDiscounts.Text = "Μαζικές Εκπτώσεις";
            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                for (this.i = 2; i <= (fgBrokerageFees.Rows.Count - 1); i++)
                {
                    fgBrokerageFees[i, 12] = locContractDiscounts.dFrom11.Value;
                    fgBrokerageFees[i, 13] = locContractDiscounts.dTo11.Value;
                    fgBrokerageFees[i, 14] = locContractDiscounts.txtFeesDiscount11.Text;
                    fgBrokerageFees[i, 15] = locContractDiscounts.txtTicketFeesDiscount11.Text;
                    sTemp = fgBrokerageFees[i, 5] + "";
                    fgBrokerageFees[i, 16] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgBrokerageFees[i, 6] + "";
                    fgBrokerageFees[i, 17] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgBrokerageFees[i, 7] + "";
                    fgBrokerageFees[i, 18] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtTicketFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgBrokerageFees[i, 8] + "";
                    fgBrokerageFees[i, 19] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtTicketFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                }
            }
        }
        private void btnBrokerageFees_Click(object sender, EventArgs e)
        {
            clsClientsBrokerageFees ContractBrokerageFees = new clsClientsBrokerageFees();

            for (i = 2; i <= fgBrokerageFees.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgBrokerageFees[i, "ID"]) == 0)
                {
                    ContractBrokerageFees.Contract_ID = iContract_ID;
                    ContractBrokerageFees.Contract_Packages_ID = iContract_Packages_ID;
                    ContractBrokerageFees.SPBF_ID = Convert.ToInt32(fgBrokerageFees[i, 21]);
                    ContractBrokerageFees.Product_ID = Convert.ToInt32(fgBrokerageFees[i, 22]);
                    ContractBrokerageFees.ProductCategory_ID = Convert.ToInt32(fgBrokerageFees[i, 23]);
                    ContractBrokerageFees.DateFrom = Convert.ToDateTime(fgBrokerageFees[i, 12]);
                    ContractBrokerageFees.DateTo = Convert.ToDateTime(fgBrokerageFees[i, 13]);
                    ContractBrokerageFees.BrokerageFeesDiscount = Convert.ToSingle(fgBrokerageFees[i, 14]);
                    ContractBrokerageFees.TicketFeesDiscount = Convert.ToSingle(fgBrokerageFees[i, 15]);
                    ContractBrokerageFees.BrokerageFeesBuy = Convert.ToSingle(fgBrokerageFees[i, 16]);
                    ContractBrokerageFees.BrokerageFeesSell = Convert.ToSingle(fgBrokerageFees[i, 17]);
                    ContractBrokerageFees.TicketFeesBuy = Convert.ToSingle(fgBrokerageFees[i, 18]);
                    ContractBrokerageFees.TicketFeesSell = Convert.ToSingle(fgBrokerageFees[i, 19]);
                    ContractBrokerageFees.InsertRecord();
                }
                else
                {
                    ContractBrokerageFees.Record_ID = Convert.ToInt32(fgBrokerageFees[i, "ID"]);
                    ContractBrokerageFees.GetRecord();
                    ContractBrokerageFees.DateFrom = Convert.ToDateTime(fgBrokerageFees[i, 12]);
                    ContractBrokerageFees.DateTo = Convert.ToDateTime(fgBrokerageFees[i, 13]);
                    ContractBrokerageFees.BrokerageFeesDiscount = Convert.ToSingle(fgBrokerageFees[i, 14]);
                    ContractBrokerageFees.TicketFeesDiscount = Convert.ToSingle(fgBrokerageFees[i, 15]);
                    ContractBrokerageFees.BrokerageFeesBuy = Convert.ToSingle(fgBrokerageFees[i, 16]);
                    ContractBrokerageFees.BrokerageFeesSell = Convert.ToSingle(fgBrokerageFees[i, 17]);
                    ContractBrokerageFees.TicketFeesBuy = Convert.ToSingle(fgBrokerageFees[i, 18]);
                    ContractBrokerageFees.TicketFeesSell = Convert.ToSingle(fgBrokerageFees[i, 19]);
                    ContractBrokerageFees.EditRecord();
                }
            }
        }
        //--- EDIT RTO Fees --------------------------
        private void tsbEditRTOFees_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 1;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            sTemp = fgRTOFees[fgRTOFees.Row, 12].ToString();
            locContractDiscounts.dFrom1.Value = Convert.ToDateTime(((sTemp == "") ? "01/01/2010" : sTemp));
            sTemp = fgRTOFees[fgRTOFees.Row, 13].ToString();
            locContractDiscounts.dTo1.Value = Convert.ToDateTime(((sTemp == "") ? "31/12/2070" : sTemp));

            locContractDiscounts.lblBuy.Text = fgRTOFees[fgRTOFees.Row, 5].ToString();
            locContractDiscounts.txtBuyPercent.Text = fgRTOFees[fgRTOFees.Row, 14].ToString();
            locContractDiscounts.txtBuyFinish.Text = fgRTOFees[fgRTOFees.Row, 17].ToString();

            locContractDiscounts.lblSell.Text = fgRTOFees[fgRTOFees.Row, 6].ToString();
            locContractDiscounts.txtSellFinish.Text = fgRTOFees[fgRTOFees.Row, 18].ToString();

            locContractDiscounts.lblTicketFeesBuy.Text = fgRTOFees[fgRTOFees.Row, 7].ToString();
            locContractDiscounts.txtTicketFeesPercent.Text = fgRTOFees[fgRTOFees.Row, 15].ToString();
            locContractDiscounts.txtTicketFeesBuy.Text = fgRTOFees[fgRTOFees.Row, 19].ToString();
            locContractDiscounts.lblTicketFeesSell.Text = fgRTOFees[fgRTOFees.Row, 8].ToString();
            locContractDiscounts.txtTicketFeesSell.Text = fgRTOFees[fgRTOFees.Row, 20].ToString();
            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgRTOFees[fgRTOFees.Row, 12] = locContractDiscounts.dFrom1.Value.ToString("d");
                fgRTOFees[fgRTOFees.Row, 13] = locContractDiscounts.dTo1.Value.ToString("d");
                fgRTOFees[fgRTOFees.Row, 14] = locContractDiscounts.txtBuyPercent.Text;
                fgRTOFees[fgRTOFees.Row, 15] = locContractDiscounts.txtTicketFeesPercent.Text;
                fgRTOFees[fgRTOFees.Row, 17] = locContractDiscounts.txtBuyFinish.Text;
                fgRTOFees[fgRTOFees.Row, 18] = locContractDiscounts.txtSellFinish.Text;
                fgRTOFees[fgRTOFees.Row, 19] = locContractDiscounts.txtTicketFeesBuy.Text;
                fgRTOFees[fgRTOFees.Row, 20] = locContractDiscounts.txtTicketFeesSell.Text;
            }
        }
        private void tsbEditMultiRTOFees_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.dFrom11.Value = ucDates.DateFrom;
            locContractDiscounts.dTo11.Value = ucDates.DateTo;
            locContractDiscounts.txtFeesDiscount11.Text = "0";
            locContractDiscounts.txtTicketFeesDiscount11.Text = "0";
            locContractDiscounts.Mode = 11;
            locContractDiscounts.Text = "Μαζικές Εκπτώσεις";
            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                for (this.i = 2; i <= (fgRTOFees.Rows.Count - 1); i++)
                {
                    fgRTOFees[i, 12] = locContractDiscounts.dFrom11.Value;
                    fgRTOFees[i, 13] = locContractDiscounts.dTo11.Value;
                    fgRTOFees[i, 14] = locContractDiscounts.txtFeesDiscount11.Text;
                    fgRTOFees[i, 15] = locContractDiscounts.txtTicketFeesDiscount11.Text;
                    sTemp = fgRTOFees[i, 5] + "";
                    fgRTOFees[i, 17] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgRTOFees[i, 6] + "";
                    fgRTOFees[i, 18] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgRTOFees[i, 7] + "";
                    fgRTOFees[i, 19] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtTicketFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                    sTemp = fgRTOFees[i, 8] + "";
                    fgRTOFees[i, 20] = Math.Round((100 - Convert.ToDouble(locContractDiscounts.txtTicketFeesDiscount11.Text)) * Convert.ToDouble(sTemp.Replace("%", "")) / 100.0, 2);
                }
            }
        }
        private void btnRTOFees_Click(object sender, EventArgs e)
        {
            clsClientsRTOFees ContractRTOFees = new clsClientsRTOFees();

            for (i = 2; i <= fgRTOFees.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgRTOFees[i, "ID"]) == 0)
                {
                    ContractRTOFees.Contract_ID = iContract_ID;
                    ContractRTOFees.Contract_Packages_ID = iContract_Packages_ID;
                    ContractRTOFees.SPBF_ID = Convert.ToInt32(fgRTOFees[i, 23]);
                    ContractRTOFees.Product_ID = Convert.ToInt32(fgRTOFees[i, 24]);
                    ContractRTOFees.ProductCategory_ID = Convert.ToInt32(fgRTOFees[i, 25]);
                    ContractRTOFees.DateFrom = Convert.ToDateTime(fgRTOFees[i, 12]);
                    ContractRTOFees.DateTo = Convert.ToDateTime(fgRTOFees[i, 13]);
                    ContractRTOFees.RTOFeesDiscount = Convert.ToDecimal(fgRTOFees[i, 14]);
                    ContractRTOFees.TicketFeesDiscount = Convert.ToDecimal(fgRTOFees[i, 15]);
                    ContractRTOFees.RTOFeesBuy = Convert.ToDecimal(fgRTOFees[i, 17]);
                    ContractRTOFees.RTOFeesSell = Convert.ToDecimal(fgRTOFees[i, 18]);
                    ContractRTOFees.TicketFeesBuy = Convert.ToSingle(fgRTOFees[i, 19]);
                    ContractRTOFees.TicketFeesSell = Convert.ToSingle(fgRTOFees[i, 20]);
                    ContractRTOFees.InsertRecord();
                }
                else
                {
                    ContractRTOFees.Record_ID = Convert.ToInt32(fgRTOFees[i, "ID"]);
                    ContractRTOFees.GetRecord();
                    ContractRTOFees.DateFrom = Convert.ToDateTime(fgRTOFees[i, 12]);
                    ContractRTOFees.DateTo = Convert.ToDateTime(fgRTOFees[i, 13]);
                    ContractRTOFees.RTOFeesDiscount = Convert.ToDecimal(fgRTOFees[i, 14]);
                    ContractRTOFees.TicketFeesDiscount = Convert.ToDecimal(fgRTOFees[i, 15]);
                    ContractRTOFees.RTOFeesBuy = Convert.ToDecimal(fgRTOFees[i, 17]);
                    ContractRTOFees.RTOFeesSell = Convert.ToDecimal(fgRTOFees[i, 18]);
                    ContractRTOFees.TicketFeesBuy = Convert.ToSingle(fgRTOFees[i, 19]);
                    ContractRTOFees.TicketFeesSell = Convert.ToSingle(fgRTOFees[i, 20]);
                    ContractRTOFees.EditRecord();
                }
            }
        }
        //--- EDIT FX Fees --------------------------------
        private void tsbEditFX_Click(object sender, EventArgs e)
        {
            if (fgFXFees.Rows.Count > 2 && fgFXFees.Row < 2) fgFXFees.Row = 2;

            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 6;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFromFX.Value = Convert.ToDateTime(fgFXFees[fgFXFees.Row, 3]);
            locContractDiscounts.dToFX.Value = Convert.ToDateTime(fgFXFees[fgFXFees.Row, 4]);

            locContractDiscounts.lblFeesFX.Text = fgFXFees[fgFXFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscountFX.Text = fgFXFees[fgFXFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFeesFX.Text = fgFXFees[fgFXFees.Row, 6].ToString();

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1) {
                fgFXFees[fgFXFees.Row, 3] = locContractDiscounts.dFromFX.Value.ToString("d");
                fgFXFees[fgFXFees.Row, 4] = locContractDiscounts.dToFX.Value.ToString("d");
                fgFXFees[fgFXFees.Row, 5] = locContractDiscounts.txtFeesDiscountFX.Text;
                fgFXFees[fgFXFees.Row, 6] = locContractDiscounts.txtFinalFeesFX.Text;

                ShowFeesButtons();
            }
        }
        private void btnFXFees_Click(object sender, EventArgs e)
        {
            clsClientsFXFees ClientsFXFees = new clsClientsFXFees();

            for (i = 2; i <= fgFXFees.Rows.Count - 1; i++) {
                if (Convert.ToInt32(fgFXFees[i, "ID"]) == 0) {
                    /*
                    ClientsFXFees = new clsClientsFXFees();
                    ClientsFXFees.Record_ID = Convert.ToInt32(fgFXFees[i, "ID"]);
                    ClientsFXFees.GetRecord();
                    dTemp = Convert.ToDateTime(fgFXFees[i, "DiscountDateFrom"]);
                    ClientsFXFees.DateTo = dTemp.AddDays(-1);
                    ClientsFXFees.EditRecord();
                    */

                    ClientsFXFees = new clsClientsFXFees();
                    ClientsFXFees.Contract_ID = iContract_ID;
                    ClientsFXFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsFXFees.SPFF_ID = Convert.ToInt32(fgFXFees[i, "SPFF_ID"]);
                    ClientsFXFees.AmountFrom = Convert.ToSingle(fgFXFees[i, "AmountFrom"]);
                    ClientsFXFees.AmountTo = Convert.ToSingle(fgFXFees[i, "AmountTo"]);
                    ClientsFXFees.DateFrom = Convert.ToDateTime(fgFXFees[i, "DiscountDateFrom"]);
                    ClientsFXFees.DateTo = Convert.ToDateTime(fgFXFees[i, "DiscountDateTo"]);
                    ClientsFXFees.FXFees_Discount = Convert.ToSingle(fgFXFees[i, "FXFees_Discount"]);
                    ClientsFXFees.FinishFXFees = Convert.ToSingle(fgFXFees[i, "FinishFXFee"]);
                    ClientsFXFees.InsertRecord();
                }
                else  {
                    ClientsFXFees = new clsClientsFXFees();
                    ClientsFXFees.Record_ID = Convert.ToInt32(fgFXFees[i, "ID"]);
                    ClientsFXFees.GetRecord();
                    ClientsFXFees.AmountFrom = Convert.ToSingle(fgFXFees[i, "AmountFrom"]);
                    ClientsFXFees.AmountTo = Convert.ToSingle(fgFXFees[i, "AmountTo"]);
                    ClientsFXFees.DateFrom = Convert.ToDateTime(fgFXFees[i, "DiscountDateFrom"]);
                    ClientsFXFees.DateTo = Convert.ToDateTime(fgFXFees[i, "DiscountDateTo"]);
                    ClientsFXFees.FXFees_Discount = Convert.ToSingle(fgFXFees[i, "FXFees_Discount"]);
                    ClientsFXFees.FinishFXFees = Convert.ToSingle(fgFXFees[i, "FinishFXFee"]);
                    ClientsFXFees.EditRecord();
                }
            }

            ShowFXFees();
        }
        //--- EDIT Advisory Fees --------------------------
        private void tsbAddAdvisoryFeesDiscount_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 2;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFrom.Value = ucDates.DateFrom;
            locContractDiscounts.dTo.Value = Convert.ToDateTime("31/12/2070");

            locContractDiscounts.lblFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 6].ToString();

            locContractDiscounts.lblMinFees.Text = lblAdvisory_MonthMinAmount.Text;
            locContractDiscounts.txtMinFeesDiscount.Text = txtAdvisory_MinimumFees_Discount.Text;
            locContractDiscounts.txtFinalMinFees.Text = txtAdvisory_MinimumFees.Text;

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgAdvisoryFees[fgAdvisoryFees.Row, 3] = locContractDiscounts.dFrom.Value.ToString("d");
                fgAdvisoryFees[fgAdvisoryFees.Row, 4] = locContractDiscounts.dTo.Value.ToString("d");
                fgAdvisoryFees[fgAdvisoryFees.Row, 5] = locContractDiscounts.txtFeesDiscount.Text;
                fgAdvisoryFees[fgAdvisoryFees.Row, 6] = locContractDiscounts.txtFinalFees.Text;

                sTemp = "";
                for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
                {
                    if (sTemp.Length == 0) sTemp = fgAdvisoryFees[i, 2].ToString();
                    else sTemp = sTemp + "-" + fgAdvisoryFees[i, 2];
                }
                for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
                {
                    fgAdvisoryFees[i, 10] = locContractDiscounts.txtMinFeesDiscount.Text;
                    fgAdvisoryFees[i, 11] = locContractDiscounts.txtFinalMinFees.Text;
                    fgAdvisoryFees[i, 13] = sTemp;
                }

                lblAdvisory_MonthMinAmount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 9].ToString();
                txtAdvisory_MinimumFees_Discount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 10].ToString();
                txtAdvisory_MinimumFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 11].ToString();
                lblAdvisory_MonthMinCurr.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 12].ToString();
                lblAdvisory_AllManFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 13].ToString();

                ShowFeesButtons();
            }
        }

        private void tsbEditAdvisoryFeesDiscount_Click(object sender, EventArgs e)
        {
            if (fgAdvisoryFees.Rows.Count > 2 && fgAdvisoryFees.Row < 2) fgAdvisoryFees.Row = 2;

            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 2;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFrom.Value = Convert.ToDateTime(fgAdvisoryFees[fgAdvisoryFees.Row, 3]);
            locContractDiscounts.dTo.Value = Convert.ToDateTime(fgAdvisoryFees[fgAdvisoryFees.Row, 4]);

            locContractDiscounts.lblFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 6].ToString();

            locContractDiscounts.lblMinFees.Text = lblAdvisory_MonthMinAmount.Text;
            locContractDiscounts.txtMinFeesDiscount.Text = txtAdvisory_MinimumFees_Discount.Text;
            locContractDiscounts.txtFinalMinFees.Text = txtAdvisory_MinimumFees.Text;

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgAdvisoryFees[fgAdvisoryFees.Row, 3] = locContractDiscounts.dFrom.Value.ToString("d");
                fgAdvisoryFees[fgAdvisoryFees.Row, 4] = locContractDiscounts.dTo.Value.ToString("d");
                fgAdvisoryFees[fgAdvisoryFees.Row, 5] = locContractDiscounts.txtFeesDiscount.Text;
                fgAdvisoryFees[fgAdvisoryFees.Row, 6] = locContractDiscounts.txtFinalFees.Text;

                sTemp = "";
                for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
                {
                    if (sTemp.Length == 0) sTemp = fgAdvisoryFees[i, 2].ToString();
                    else sTemp = sTemp + "-" + fgAdvisoryFees[i, 2];
                }
                for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
                {
                    fgAdvisoryFees[i, 10] = locContractDiscounts.txtMinFeesDiscount.Text;
                    fgAdvisoryFees[i, 11] = locContractDiscounts.txtFinalMinFees.Text;
                    fgAdvisoryFees[i, 13] = sTemp;
                }

                lblAdvisory_MonthMinAmount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 9].ToString();
                txtAdvisory_MinimumFees_Discount.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 10].ToString();
                txtAdvisory_MinimumFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 11].ToString();
                lblAdvisory_MonthMinCurr.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 12].ToString();
                lblAdvisory_AllManFees.Text = fgAdvisoryFees[fgAdvisoryFees.Row, 13].ToString();

                ShowFeesButtons();
            }
        }

        private void btnAdvisoryFees_Click(object sender, EventArgs e)
        {
            clsClientsAdvisoryFees ClientsAdvisoryFees = new clsClientsAdvisoryFees();

            for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgAdvisoryFees[i, "ID"]) == 0)
                {
                    ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    ClientsAdvisoryFees.Record_ID = Convert.ToInt32(fgAdvisoryFees[i, "ID"]);
                    ClientsAdvisoryFees.GetRecord();
                    dTemp = Convert.ToDateTime(fgAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsAdvisoryFees.DateTo = dTemp.AddDays(-1);
                    ClientsAdvisoryFees.EditRecord();

                    ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    ClientsAdvisoryFees.Contract_ID = iContract_ID;
                    ClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsAdvisoryFees.SPAF_ID = Convert.ToInt32(fgAdvisoryFees[i, "SPAF_ID"]);
                    ClientsAdvisoryFees.AmountFrom = Convert.ToSingle(fgAdvisoryFees[i, "AmountFrom"]);
                    ClientsAdvisoryFees.AmountTo = Convert.ToSingle(fgAdvisoryFees[i, "AmountTo"]);
                    ClientsAdvisoryFees.DateFrom = Convert.ToDateTime(fgAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsAdvisoryFees.DateTo = Convert.ToDateTime(fgAdvisoryFees[i, "DiscountDateTo"]);
                    ClientsAdvisoryFees.AdvisoryFees_Discount = Convert.ToDecimal(fgAdvisoryFees[i, "AdvisoryFees_Discount"]);
                    ClientsAdvisoryFees.FinishAdvisoryFees = Convert.ToDecimal(fgAdvisoryFees[i, "FinishAdvisoryFee"]);
                    ClientsAdvisoryFees.MinimumFees_Discount = Convert.ToSingle(txtAdvisory_MinimumFees_Discount.Text);
                    ClientsAdvisoryFees.MinimumFees = Convert.ToSingle(txtAdvisory_MinimumFees.Text);
                    ClientsAdvisoryFees.AllManFees = lblAdvisory_AllManFees.Text;
                    ClientsAdvisoryFees.InsertRecord();
                }
                else
                {
                    ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    ClientsAdvisoryFees.Record_ID = Convert.ToInt32(fgAdvisoryFees[i, "ID"]);
                    ClientsAdvisoryFees.GetRecord();
                    ClientsAdvisoryFees.DateFrom = Convert.ToDateTime(fgAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsAdvisoryFees.DateTo = Convert.ToDateTime(fgAdvisoryFees[i, "DiscountDateTo"]);
                    ClientsAdvisoryFees.AdvisoryFees_Discount = Convert.ToDecimal(fgAdvisoryFees[i, "AdvisoryFees_Discount"]);
                    ClientsAdvisoryFees.FinishAdvisoryFees = Convert.ToDecimal(fgAdvisoryFees[i, "FinishAdvisoryFee"]);
                    ClientsAdvisoryFees.MinimumFees_Discount = Convert.ToSingle(txtAdvisory_MinimumFees_Discount.Text);
                    ClientsAdvisoryFees.MinimumFees = Convert.ToSingle(txtAdvisory_MinimumFees.Text);
                    ClientsAdvisoryFees.AllManFees = lblAdvisory_AllManFees.Text;
                    ClientsAdvisoryFees.EditRecord();
                }
            }

            ShowAdvisoryFees();
        }

        private void tsbHistoryAdvisoryFees_Click(object sender, EventArgs e)
        {
            fgHistory.Redraw = false;
            fgHistory.Rows.Count = 2;

            clsClientsAdvisoryFees klsClientsAdvisoryFees = new clsClientsAdvisoryFees();
            klsClientsAdvisoryFees.ServiceProvider_ID = iAdvisoryProvider_ID;
            klsClientsAdvisoryFees.Option_ID = iAdvisoryOption_ID;
            klsClientsAdvisoryFees.InvestmentProfile_ID = iAdvisoryInvestmentProfile_ID;
            klsClientsAdvisoryFees.InvestmentPolicy_ID = iAdvisoryInvestmentPolicy_ID;
            klsClientsAdvisoryFees.DateFrom = dPackageDateStart;
            klsClientsAdvisoryFees.DateTo = dPackageDateFinish;
            klsClientsAdvisoryFees.Contract_ID = iContract_ID;
            klsClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
            klsClientsAdvisoryFees.GetList_Package_ID();
            foreach (DataRow dtRow in klsClientsAdvisoryFees.List.Rows)
            {
                fgHistory.AddItem(dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                                  dtRow["AdvisoryFees"] + "\t" + dtRow["AdvisoryFees_Discount"] + "\t" + dtRow["FinishAdvisoryFees"] + "\t" +
                                  dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" + dtRow["ID"]);
            }
            fgHistory.Redraw = true;
            panHistory.Visible = true;
        }

        //--- EDIT Discret Fees --------------------------
        private void tsbAddDiscretFeesDiscount_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 2;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFrom.Value = ucDates.DateFrom;
            locContractDiscounts.dTo.Value = Convert.ToDateTime("31/12/2070");

            locContractDiscounts.lblFees.Text = fgDiscretFees[fgDiscretFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscount.Text = fgDiscretFees[fgDiscretFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFees.Text = fgDiscretFees[fgDiscretFees.Row, 6].ToString();

            locContractDiscounts.lblMinFees.Text = lblDiscret_MonthMinAmount.Text;
            locContractDiscounts.txtMinFeesDiscount.Text = txtDiscret_MinimumFees_Discount.Text;
            locContractDiscounts.txtFinalMinFees.Text = txtDiscret_MinimumFees.Text;

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgDiscretFees[fgDiscretFees.Row, 3] = locContractDiscounts.dFrom.Value.ToString("d");
                fgDiscretFees[fgDiscretFees.Row, 4] = locContractDiscounts.dTo.Value.ToString("d");
                fgDiscretFees[fgDiscretFees.Row, 5] = locContractDiscounts.txtFeesDiscount.Text;
                fgDiscretFees[fgDiscretFees.Row, 6] = locContractDiscounts.txtFinalFees.Text;

                sTemp = "";
                for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
                {
                    if (sTemp.Length == 0) sTemp = fgDiscretFees[i, 2].ToString();
                    else sTemp = sTemp + "-" + fgDiscretFees[i, 2];
                }
                for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
                {
                    fgDiscretFees[i, 10] = locContractDiscounts.txtMinFeesDiscount.Text;
                    fgDiscretFees[i, 11] = locContractDiscounts.txtFinalMinFees.Text;
                    fgDiscretFees[i, 13] = sTemp;
                }

                lblDiscret_MonthMinAmount.Text = fgDiscretFees[fgDiscretFees.Row, 9].ToString();
                txtDiscret_MinimumFees_Discount.Text = fgDiscretFees[fgDiscretFees.Row, 10].ToString();
                txtDiscret_MinimumFees.Text = fgDiscretFees[fgDiscretFees.Row, 11].ToString();
                lblDiscret_MonthMinCurr.Text = fgDiscretFees[fgDiscretFees.Row, 12].ToString();
                lblDiscret_AllManFees.Text = fgDiscretFees[fgDiscretFees.Row, 13].ToString();

                ShowFeesButtons();
            }
        }
        private void tsbEditDiscretFeesDiscount_Click(object sender, EventArgs e)
        {
            if (fgDiscretFees.Rows.Count > 2 && fgDiscretFees.Row < 2) fgDiscretFees.Row = 2;

            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 2;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFrom.Value = Convert.ToDateTime(fgDiscretFees[fgDiscretFees.Row, 3]);
            locContractDiscounts.dTo.Value = Convert.ToDateTime(fgDiscretFees[fgDiscretFees.Row, 4]);

            locContractDiscounts.lblFees.Text = fgDiscretFees[fgDiscretFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscount.Text = fgDiscretFees[fgDiscretFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFees.Text = fgDiscretFees[fgDiscretFees.Row, 6].ToString();

            locContractDiscounts.lblMinFees.Text = lblDiscret_MonthMinAmount.Text;
            locContractDiscounts.txtMinFeesDiscount.Text = txtDiscret_MinimumFees_Discount.Text;
            locContractDiscounts.txtFinalMinFees.Text = txtDiscret_MinimumFees.Text;

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgDiscretFees[fgDiscretFees.Row, 3] = locContractDiscounts.dFrom.Value.ToString("d");
                fgDiscretFees[fgDiscretFees.Row, 4] = locContractDiscounts.dTo.Value.ToString("d");
                fgDiscretFees[fgDiscretFees.Row, 5] = locContractDiscounts.txtFeesDiscount.Text;
                fgDiscretFees[fgDiscretFees.Row, 6] = locContractDiscounts.txtFinalFees.Text;

                sTemp = "";
                for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
                {
                    if (sTemp.Length == 0) sTemp = fgDiscretFees[i, 2].ToString();
                    else sTemp = sTemp + "-" + fgDiscretFees[i, 2];
                }
                for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
                {
                    fgDiscretFees[i, 10] = locContractDiscounts.txtMinFeesDiscount.Text;
                    fgDiscretFees[i, 11] = locContractDiscounts.txtFinalMinFees.Text;
                    fgDiscretFees[i, 13] = sTemp;
                }

                lblDiscret_MonthMinAmount.Text = fgDiscretFees[fgDiscretFees.Row, 9].ToString();
                txtDiscret_MinimumFees_Discount.Text = fgDiscretFees[fgDiscretFees.Row, 10].ToString();
                txtDiscret_MinimumFees.Text = fgDiscretFees[fgDiscretFees.Row, 11].ToString();
                lblDiscret_MonthMinCurr.Text = fgDiscretFees[fgDiscretFees.Row, 12].ToString();
                lblDiscret_AllManFees.Text = fgDiscretFees[fgDiscretFees.Row, 13].ToString();

                ShowFeesButtons();
            }
        }

        private void tsbHistoryDiscretFees_Click(object sender, EventArgs e)
        {
            fgHistory.Redraw = false;
            fgHistory.Rows.Count = 2;

            clsClientsDiscretFees klsClientsDiscretFees = new clsClientsDiscretFees();
            klsClientsDiscretFees.ServiceProvider_ID = iDiscretProvider_ID;
            klsClientsDiscretFees.Option_ID = iDiscretOption_ID;
            klsClientsDiscretFees.InvestmentProfile_ID = iDiscretInvestmentProfile_ID;
            klsClientsDiscretFees.InvestmentPolicy_ID = iDiscretInvestmentPolicy_ID;
            klsClientsDiscretFees.DateFrom = dPackageDateStart;
            klsClientsDiscretFees.DateTo = dPackageDateFinish;
            klsClientsDiscretFees.Contract_ID = iContract_ID;
            klsClientsDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
            klsClientsDiscretFees.GetList_Package_ID();
            foreach (DataRow dtRow in klsClientsDiscretFees.List.Rows)
            {
                fgHistory.AddItem(dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                                  dtRow["DiscretFees"] + "\t" + dtRow["DiscretFees_Discount"] + "\t" + dtRow["FinishDiscretFees"] + "\t" +
                                  dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" + dtRow["ID"]);
            }
            fgHistory.Redraw = true;
            panHistory.Visible = true;
        }
        private void btnDiscretFees_Click(object sender, EventArgs e)
        {
            clsClientsDiscretFees ContractDiscretFees = new clsClientsDiscretFees();

            for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgDiscretFees[i, "ID"]) == 0)
                {
                    ContractDiscretFees = new clsClientsDiscretFees();
                    ContractDiscretFees.Record_ID = Convert.ToInt32(fgDiscretFees[i, "ID"]);
                    ContractDiscretFees.GetRecord();
                    dTemp = Convert.ToDateTime(fgDiscretFees[i, "DiscountDateFrom"]);
                    ContractDiscretFees.DateTo = dTemp.AddDays(-1);
                    ContractDiscretFees.EditRecord();

                    ContractDiscretFees = new clsClientsDiscretFees();
                    ContractDiscretFees.Contract_ID = iContract_ID;
                    ContractDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
                    ContractDiscretFees.SPDF_ID = Convert.ToInt32(fgDiscretFees[i, "SPDF_ID"]);
                    ContractDiscretFees.AmountFrom = Convert.ToSingle(fgDiscretFees[i, "AmountFrom"]);
                    ContractDiscretFees.AmountTo = Convert.ToSingle(fgDiscretFees[i, "AmountTo"]);
                    ContractDiscretFees.DateFrom = Convert.ToDateTime(fgDiscretFees[i, "DiscountDateFrom"]);
                    ContractDiscretFees.DateTo = Convert.ToDateTime(fgDiscretFees[i, "DiscountDateTo"]);
                    ContractDiscretFees.DiscretFees_Discount = Convert.ToDecimal(fgDiscretFees[i, "DiscretFees_Discount"]);
                    ContractDiscretFees.FinishDiscretFees = Convert.ToDecimal(fgDiscretFees[i, "FinishDiscretFee"]);
                    ContractDiscretFees.MinimumFees_Discount = Convert.ToSingle(txtDiscret_MinimumFees_Discount.Text);
                    ContractDiscretFees.MinimumFees = Convert.ToSingle(txtDiscret_MinimumFees.Text);
                    ContractDiscretFees.AllManFees = lblDiscret_AllManFees.Text;
                    ContractDiscretFees.InsertRecord();
                }
                else
                {
                    ContractDiscretFees = new clsClientsDiscretFees();
                    ContractDiscretFees.Record_ID = Convert.ToInt32(fgDiscretFees[i, "ID"]);
                    ContractDiscretFees.GetRecord();
                    ContractDiscretFees.DateFrom = Convert.ToDateTime(fgDiscretFees[i, "DiscountDateFrom"]);
                    ContractDiscretFees.DateTo = Convert.ToDateTime(fgDiscretFees[i, "DiscountDateTo"]);
                    ContractDiscretFees.DiscretFees_Discount = Convert.ToDecimal(fgDiscretFees[i, "DiscretFees_Discount"]);
                    ContractDiscretFees.FinishDiscretFees = Convert.ToDecimal(fgDiscretFees[i, "FinishDiscretFee"]);
                    ContractDiscretFees.MinimumFees_Discount = Convert.ToSingle(txtDiscret_MinimumFees_Discount.Text);
                    ContractDiscretFees.MinimumFees = Convert.ToSingle(txtDiscret_MinimumFees.Text);
                    ContractDiscretFees.AllManFees = lblDiscret_AllManFees.Text;
                    ContractDiscretFees.EditRecord();
                }
            }
            ShowDiscretFees();
        }
        //--- EDIT Deal Advisory Fees --------------------------
        private void tsbAddDealAdvisoryFeesDiscount_Click(object sender, EventArgs e)
        {
            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 6;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFromFX.Value = ucDates.DateFrom;
            locContractDiscounts.dToFX.Value = Convert.ToDateTime("31/12/2070");

            locContractDiscounts.lblFeesFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscountFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFeesFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 6].ToString();

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 3] = locContractDiscounts.dFromFX.Value.ToString("d");
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 4] = locContractDiscounts.dToFX.Value.ToString("d");
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 5] = locContractDiscounts.txtFeesDiscountFX.Text;
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 6] = locContractDiscounts.txtFinalFeesFX.Text;

                ShowFeesButtons();
            }
        }
        private void btnCustodyFees_Click(object sender, EventArgs e)
        {

        }

        private void btnAdminFees_Click(object sender, EventArgs e)
        {

        }

        private void btnSettlementFees_Click(object sender, EventArgs e)
        {

        }

        private void tsbEditAdminFeesDiscount_Click(object sender, EventArgs e)
        {

        }

        private void tsbEditDealAdvisoryFeesDiscount_Click(object sender, EventArgs e)
        {
            if (fgDealAdvisoryFees.Rows.Count > 2 && fgDealAdvisoryFees.Row < 2) fgDealAdvisoryFees.Row = 2;

            frmContractDiscounts locContractDiscounts = new frmContractDiscounts();
            locContractDiscounts.Mode = 6;
            locContractDiscounts.Text = "Διόρθωση Εκπτώσεων";
            locContractDiscounts.dFromFX.Value = Convert.ToDateTime(fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 3]);
            locContractDiscounts.dToFX.Value = Convert.ToDateTime(fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 4]);

            locContractDiscounts.lblFeesFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 2].ToString();
            locContractDiscounts.txtFeesDiscountFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 5].ToString();
            locContractDiscounts.txtFinalFeesFX.Text = fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 6].ToString();

            locContractDiscounts.ShowDialog();
            if (locContractDiscounts.LastAktion == 1)
            {
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 3] = locContractDiscounts.dFromFX.Value.ToString("d");
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 4] = locContractDiscounts.dToFX.Value.ToString("d");
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 5] = locContractDiscounts.txtFeesDiscountFX.Text;
                fgDealAdvisoryFees[fgDealAdvisoryFees.Row, 6] = locContractDiscounts.txtFinalFeesFX.Text;

                ShowFeesButtons();
            }
        }
        private void tsbHistoryDealAdvisoryFees_Click(object sender, EventArgs e)
        {
            fgHistory.Redraw = false;
            fgHistory.Rows.Count = 2;

            clsClientsDealAdvisoryFees ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
            ClientsDealAdvisoryFees.ServiceProvider_ID = iDealAdvisoryProvider_ID;
            ClientsDealAdvisoryFees.Option_ID = iDealAdvisoryOption_ID;
            ClientsDealAdvisoryFees.InvestmentPolicy_ID = iDealAdvisoryInvestmentPolicy_ID;
            ClientsDealAdvisoryFees.DateFrom = dPackageDateStart;
            ClientsDealAdvisoryFees.DateTo = dPackageDateFinish;
            ClientsDealAdvisoryFees.Contract_ID = iContract_ID;
            ClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
            ClientsDealAdvisoryFees.GetList_Package_ID();
            foreach (DataRow dtRow in ClientsDealAdvisoryFees.List.Rows)
            {
                fgHistory.AddItem(dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                                  dtRow["DealAdvisoryFees"] + "\t" + dtRow["DealAdvisoryFees_Discount"] + "\t" + dtRow["FinishDealAdvisoryFees"] + "\t" +
                                  dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" + dtRow["ID"]);
            }
            fgHistory.Redraw = true;
            panHistory.Visible = true;
        }
        private void btnDealAdvisoryFees_Click(object sender, EventArgs e)
        {
            clsClientsDealAdvisoryFees ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees(); 

            for (i = 2; i <= fgDealAdvisoryFees.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgDealAdvisoryFees[i, "ID"]) == 0)
                {
                    ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ClientsDealAdvisoryFees.Record_ID = Convert.ToInt32(fgDealAdvisoryFees[i, "ID"]);
                    ClientsDealAdvisoryFees.GetRecord();
                    dTemp = Convert.ToDateTime(fgDealAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsDealAdvisoryFees.DateTo = dTemp.AddDays(-1);
                    ClientsDealAdvisoryFees.EditRecord();

                    ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ClientsDealAdvisoryFees.Contract_ID = iContract_ID;
                    ClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsDealAdvisoryFees.SPDAF_ID = Convert.ToInt32(fgDealAdvisoryFees[i, "SPDAF_ID"]);
                    ClientsDealAdvisoryFees.AmountFrom = Convert.ToSingle(fgDealAdvisoryFees[i, "AmountFrom"]);
                    ClientsDealAdvisoryFees.AmountTo = Convert.ToSingle(fgDealAdvisoryFees[i, "AmountTo"]);
                    ClientsDealAdvisoryFees.DateFrom = Convert.ToDateTime(fgDealAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsDealAdvisoryFees.DateTo = Convert.ToDateTime(fgDealAdvisoryFees[i, "DiscountDateTo"]);
                    ClientsDealAdvisoryFees.DealAdvisoryFees_Discount = Convert.ToDecimal(fgDealAdvisoryFees[i, "DealAdvisoryFees_Discount"]);
                    ClientsDealAdvisoryFees.FinishDealAdvisoryFees = Convert.ToDecimal(fgDealAdvisoryFees[i, "FinishDealAdvisoryFee"]);
                    //ClientsDealAdvisoryFees.MinimumFees_Discount = Convert.ToSingle(txtDealAdvisory_MinimumFees_Discount.Text);
                    //ClientsDealAdvisoryFees.MinimumFees = Convert.ToSingle(txtDealAdvisory_MinimumFees.Text);
                    //ClientsDealAdvisoryFees.AllManFees = lblDealAdvisory_AllManFees.Text;
                    ClientsDealAdvisoryFees.InsertRecord();
                }
                else
                {
                    ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ClientsDealAdvisoryFees.Record_ID = Convert.ToInt32(fgDealAdvisoryFees[i, "ID"]);
                    ClientsDealAdvisoryFees.GetRecord();
                    ClientsDealAdvisoryFees.DateFrom = Convert.ToDateTime(fgDealAdvisoryFees[i, "DiscountDateFrom"]);
                    ClientsDealAdvisoryFees.DateTo = Convert.ToDateTime(fgDealAdvisoryFees[i, "DiscountDateTo"]);
                    ClientsDealAdvisoryFees.DealAdvisoryFees_Discount = Convert.ToDecimal(fgDealAdvisoryFees[i, "DealAdvisoryFees_Discount"]);
                    ClientsDealAdvisoryFees.FinishDealAdvisoryFees = Convert.ToDecimal(fgDealAdvisoryFees[i, "FinishDealAdvisoryFee"]);
                    //ClientsDealAdvisoryFees.MinimumFees_Discount = Convert.ToSingle(txtDealAdvisory_MinimumFees_Discount.Text);
                    //ClientsDealAdvisoryFees.MinimumFees = Convert.ToSingle(txtDealAdvisory_MinimumFees.Text);
                    //ClientsDealAdvisoryFees.AllManFees = lblDealAdvisory_AllManFees.Text;
                    ClientsDealAdvisoryFees.EditRecord();
                }
            }

            ShowAdvisoryFees();
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckPackages)
            {
                ShowDetails();
                fgList.Focus();
            }
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void chkMiFID_2_CheckedChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void cmbAdvisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void cmbRM_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void DefineGridColumns()
        {
            switch (cmbViewType.SelectedIndex)
            {
                case 0:                                 // 0 - Geniki
                    fgList.Cols["AUM"].Visible = false;
                    fgList.Cols["MF_AmoiviPro"].Visible = false;
                    fgList.Cols["MF_Discount_Percent"].Visible = false;
                    fgList.Cols["MF_AmoiviAfter"].Visible = false;
                    fgList.Cols["MF_Climakas"].Visible = false;
                    fgList.Cols["MF_MinAmoivi"].Visible = false;
                    fgList.Cols["MF_MinAmoivi_Percent"].Visible = false;
                    fgList.Cols["MF_MinFinish"].Visible = false;
                    fgList.Cols["AF_AmoiviPro"].Visible = false;
                    fgList.Cols["AF_Discount_Percent"].Visible = false;
                    fgList.Cols["AF_AmoiviAfter"].Visible = false;
                    fgList.Cols["AF_MinAmoivi"].Visible = false;
                    fgList.Cols["AF_MinAmoivi_Percent"].Visible = false;
                    fgList.Cols["AF_MinFinish"].Visible = false;
                    break;
                case 1:                                 // 1 - Managment Fees
                    fgList.Cols["AUM"].Visible = true;
                    fgList.Cols["MF_AmoiviPro"].Visible = true;
                    fgList.Cols["MF_Discount_Percent"].Visible = true;
                    fgList.Cols["MF_AmoiviAfter"].Visible = true;
                    fgList.Cols["MF_Climakas"].Visible = true;
                    fgList.Cols["MF_MinAmoivi"].Visible = true;
                    fgList.Cols["MF_MinAmoivi_Percent"].Visible = true;
                    fgList.Cols["MF_MinFinish"].Visible = true;
                    fgList.Cols["AF_AmoiviPro"].Visible = false;
                    fgList.Cols["AF_Discount_Percent"].Visible = false;
                    fgList.Cols["AF_AmoiviAfter"].Visible = false;
                    fgList.Cols["AF_MinAmoivi"].Visible = false;
                    fgList.Cols["AF_MinAmoivi_Percent"].Visible = false;
                    fgList.Cols["AF_MinFinish"].Visible = false;
                    break;
                case 2:                                 // 2 - Admin Fees
                    fgList.Cols["AUM"].Visible = true;
                    fgList.Cols["MF_AmoiviPro"].Visible = false;
                    fgList.Cols["MF_Discount_Percent"].Visible = false;
                    fgList.Cols["MF_AmoiviAfter"].Visible = false;
                    fgList.Cols["MF_Climakas"].Visible = false;
                    fgList.Cols["MF_MinAmoivi"].Visible = false;
                    fgList.Cols["MF_MinAmoivi_Percent"].Visible = false;
                    fgList.Cols["MF_MinFinish"].Visible = false;
                    fgList.Cols["AF_AmoiviPro"].Visible = true;
                    fgList.Cols["AF_Discount_Percent"].Visible = true;
                    fgList.Cols["AF_AmoiviAfter"].Visible = true;
                    fgList.Cols["AF_MinAmoivi"].Visible = true;
                    fgList.Cols["AF_MinAmoivi_Percent"].Visible = true;
                    fgList.Cols["AF_MinFinish"].Visible = true;
                    break;
            }
        }
        private void DefineServicesList()
        {
            string sTemp = "";
            sServicesList = ",";
            for (i = 1; i <= fgServices.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgServices[i, 0]))
                {
                    sTemp = sTemp + fgServices[i, 1] + " / ";
                    sServicesList = sServicesList + fgServices[i, 2] + ",";
                }
            }
            lblServices.Text = sTemp;
            panServices.Visible = false;
        }  
        private void chkServices_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServices.Checked) for (i = 1; (i <= fgServices.Rows.Count - 1); i++) fgServices[i, 0] = true;
            else for (i = 1; (i <= fgServices.Rows.Count - 1); i++) fgServices[i, 0] = false;
        }
        private void lnkServices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panServices.Visible = true;
        }

        private void picClose_Services_Click(object sender, EventArgs e)
        {
            panServices.Visible = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DefineServicesList();
            ShowList();
        }
        private void tsbSnapshot_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            switch (cmbViewType.SelectedIndex)
            {
                case 1:                                 // 2 - Managment Fees
                    iFT_ID = 0;
                    clsManagmentFees_Titles ManagmentFees_Title = new clsManagmentFees_Titles();
                    ManagmentFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    ManagmentFees_Title.MF_Year = Convert.ToInt32(cmbYear.Text);
                    ManagmentFees_Title.MF_Quart = iIndex;
                    ManagmentFees_Title.GetRecord_Title();
                    iFT_ID = ManagmentFees_Title.Record_ID;
                    if (iFT_ID == 0) 
                    {
                        ManagmentFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                        ManagmentFees_Title.MF_Quart = iIndex;
                        ManagmentFees_Title.MF_Year = Convert.ToInt32(cmbYear.Text);
                        ManagmentFees_Title.DateIns = DateTime.Now;
                        ManagmentFees_Title.Author_ID = Global.User_ID;
                        iFT_ID = ManagmentFees_Title.InsertRecord();
                    }
                    CreateSnapshot_MF();
                    break;
                case 2:                                 // 3 - Admin Fees

                    iAT_ID = 0;
                    clsAdminFees_Titles klsAdminFees_Title = new clsAdminFees_Titles();
                    klsAdminFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    klsAdminFees_Title.AF_Year = Convert.ToInt32(cmbYear.Text);
                    klsAdminFees_Title.AF_Quart = iIndex;
                    klsAdminFees_Title.GetRecord_Title();
                    iAT_ID = klsAdminFees_Title.Record_ID;
                    if (iAT_ID == 0)
                    {
                        klsAdminFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                        klsAdminFees_Title.AF_Quart = iIndex;
                        klsAdminFees_Title.AF_Year = Convert.ToInt32(cmbYear.Text);
                        klsAdminFees_Title.DateIns = DateTime.Now;
                        klsAdminFees_Title.Author_ID = Global.User_ID;
                        iAT_ID = klsAdminFees_Title.InsertRecord();
                    }
                    CreateSnapshot_AF();
                    break;
            }

            this.Cursor = Cursors.Default;
        }
        private void CreateSnapshot_MF()
        {
            clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgList[i, "BrokerageServiceProvider_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue))
                {
                    MF_Recs = new clsManagmentFees_Recs();

                    MF_Recs.FT_ID = iFT_ID;
                    MF_Recs.DateFrom = Convert.ToDateTime(fgList[i, "DateFrom"]);
                    MF_Recs.DateTo = Convert.ToDateTime(fgList[i, "DateTo"]);
                    MF_Recs.Code = fgList[i, "Code"] + "";
                    MF_Recs.Portfolio = fgList[i, "Portfolio"] + "";
                    MF_Recs.CheckRecord();
                    if (MF_Recs.Record_ID == 0)                             // == 0 - MF record with this FT_ID, DateFrom, DateTo, Code, Portfolio DONT'T EXISTS, so add it into ManFees with FT_ID
                    {
                        MF_Recs = new clsManagmentFees_Recs();
                        MF_Recs.FT_ID = iFT_ID;
                        MF_Recs.Client_ID = Convert.ToInt32(fgList[i, "Client_ID"]);
                        MF_Recs.DateFrom = Convert.ToDateTime(fgList[i, "DateFrom"]);
                        MF_Recs.DateTo = Convert.ToDateTime(fgList[i, "DateTo"]);
                        MF_Recs.Code = fgList[i, "Code"] + "";
                        MF_Recs.Portfolio = fgList[i, "Portfolio"] + "";
                        MF_Recs.Currency = fgList[i, "Currency"] + "";
                        MF_Recs.Contract_ID = Convert.ToInt32(fgList[i, "ID"]);
                        MF_Recs.Contract_Details_ID = Convert.ToInt32(fgList[i, "Contract_Details_ID"]);
                        MF_Recs.Contract_Packages_ID = Convert.ToInt32(fgList[i, "Contract_Packages_ID"]);
                        MF_Recs.AUM = 0;
                        MF_Recs.Days = Convert.ToInt32(fgList[i, "Days"]);
                        MF_Recs.AmoiviPro = Convert.ToSingle(fgList[i, "MF_AmoiviPro"]);
                        MF_Recs.AxiaPro = 0;                                                       
                        MF_Recs.Climakas = fgList[i, "MF_Climakas"] + "";
                        MF_Recs.Discount_DateFrom = "";
                        MF_Recs.Discount_DateTo = fgList[i, "MF_Discount_DateTo"] + "";  
                        MF_Recs.Discount_Percent1 = Convert.ToSingle(fgList[i, "MF_Discount_Percent"]);
                        MF_Recs.Discount_Amount1 = 0;                                              
                        MF_Recs.Discount_Percent2 = 0;                                               
                        MF_Recs.Discount_Amount2 = 0;
                        MF_Recs.Discount_Percent = MF_Recs.Discount_Percent1;
                        MF_Recs.Discount_Amount = MF_Recs.Discount_Amount1;
                        MF_Recs.AmoiviAfter = Convert.ToSingle(fgList[i, "MF_AmoiviAfter"]);
                        MF_Recs.AxiaAfter = Convert.ToSingle(fgList[i, "MF_AmoiviAfter"]);          
                        MF_Recs.MinAmoivi = Convert.ToSingle(fgList[i, "MF_MinAmoivi"]);
                        MF_Recs.MinAmoivi_Percent = Convert.ToSingle(fgList[i, "MF_MinAmoivi_Percent"]);
                        MF_Recs.FinishMinAmoivi = 0;                                               
                        MF_Recs.LastAmount = 0;                                                     
                        MF_Recs.LastAmount_Percent = 0;                                             
                        MF_Recs.VAT_Percent = Convert.ToSingle(fgList[i, "VAT_Percent"]);
                        MF_Recs.VAT_Amount = 0;                                                     
                        MF_Recs.FinishAmount = 0;                                                   
                        MF_Recs.Service_ID = Convert.ToInt32(fgList[i, "Service_ID"]);
                        MF_Recs.Invoice_ID = 0;
                        MF_Recs.Invoice_Num = "";
                        MF_Recs.Invoice_File = "";
                        MF_Recs.DateFees = Convert.ToDateTime("1900/01/01");
                        MF_Recs.Invoice_Type = Convert.ToInt32(fgList[i, "ClientType"]);      // ClientType=1-idiotis -> Invoice_Type=1-ΑΠΥ, ClientType=2-Etairia -> Invoice_Type=2-ΤΠΥ
                        if (Convert.ToInt32(fgList[i, "Client_ID"]) == 1) MF_Recs.Invoice_Type = 1;
                        if (Convert.ToInt32(fgList[i, "Client_ID"]) == 2) MF_Recs.Invoice_Type = 2;
                        MF_Recs.Notes = "";
                        MF_Recs.Invoice_External = "";
                        MF_Recs.User_ID = 0;
                        MF_Recs.DateEdit = Convert.ToDateTime("1900/01/01");                                                       
                        MF_Recs.Status = 1;                                                            // 1 - Active, 2 - Cancelled
                        MF_Recs.CalcFees();
                        MF_Recs.InsertRecord();
                    }
                }
            }
        }
        private void CreateSnapshot_AF()
        {
            clsAdminFees_Recs AF_Recs = new clsAdminFees_Recs();

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgList[i, "BrokerageServiceProvider_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue))
                {
                    AF_Recs = new clsAdminFees_Recs();
                    AF_Recs.AT_ID = iAT_ID;
                    AF_Recs.DateFrom = Convert.ToDateTime(fgList[i, "DateFrom"]);
                    AF_Recs.DateTo = Convert.ToDateTime(fgList[i, "DateTo"]);
                    AF_Recs.Code = fgList[i, "Code"] + "";
                    AF_Recs.Portfolio = fgList[i, "Portfolio"] + "";
                    AF_Recs.CheckRecord();
                    if (AF_Recs.Record_ID == 0)                             // == 0 - AF record with this AT_ID, DateFrom, DateTo, Code, Portfolio DONT'T EXISTS, so add it into AdminFees with AT_ID
                    {
                        AF_Recs = new clsAdminFees_Recs();
                        AF_Recs.AT_ID = iAT_ID;
                        AF_Recs.Client_ID = Convert.ToInt32(fgList[i, "Client_ID"]);
                        AF_Recs.DateFrom = Convert.ToDateTime(fgList[i, "DateFrom"]);
                        AF_Recs.DateTo = Convert.ToDateTime(fgList[i, "DateTo"]);
                        AF_Recs.Code = fgList[i, "Code"] + "";
                        AF_Recs.Portfolio = fgList[i, "Portfolio"] + "";
                        AF_Recs.Currency = fgList[i, "Currency"] + "";
                        AF_Recs.Contract_ID = Convert.ToInt32(fgList[i, "ID"]);
                        AF_Recs.Contract_Details_ID = Convert.ToInt32(fgList[i, "Contract_Details_ID"]);
                        AF_Recs.Contract_Packages_ID = Convert.ToInt32(fgList[i, "Contract_Packages_ID"]);
                        AF_Recs.AUM = 0;
                        AF_Recs.Days = Convert.ToInt32(fgList[i, "Days"]);
                        AF_Recs.AmoiviPro = Convert.ToSingle(fgList[i, "AF_AmoiviPro"]);
                        AF_Recs.AxiaPro = 0;
                        AF_Recs.AmoiviAfter = Convert.ToSingle(fgList[i, "AF_AmoiviAfter"]);
                        AF_Recs.AxiaAfter = 0;
                        AF_Recs.Discount_Percent1 = Convert.ToSingle(fgList[i, "AF_Discount_Percent"]);
                        AF_Recs.Discount_Amount1 = 0;
                        AF_Recs.Discount_Percent2 = 0;
                        AF_Recs.Discount_Amount2 = 0;
                        AF_Recs.Discount_Percent = 0;
                        AF_Recs.Discount_Amount = 0;
                        AF_Recs.MinAmoivi = Convert.ToSingle(fgList[i, "AF_MinAmoivi"]);
                        AF_Recs.MinAmoivi_Percent = Convert.ToSingle(fgList[i, "AF_MinAmoivi_Percent"]); 
                        AF_Recs.MinAmoivi_Percent2 = 0;
                        AF_Recs.FinishMinAmoivi = Convert.ToDecimal(fgList[i, "AF_MinFinish"]); 
                        AF_Recs.LastAmount = 0;
                        AF_Recs.LastAmount_Percent = 0;
                        AF_Recs.VAT_Percent = Convert.ToSingle(fgList[i, "VAT_Percent"]);
                        AF_Recs.VAT_Amount = 0;
                        AF_Recs.FinishAmount = 0;
                        AF_Recs.MaxDays = 0;
                        AF_Recs.AverageAUM = 0;
                        AF_Recs.Weights = 0;
                        AF_Recs.MinYearly = 0;
                        AF_Recs.Service_ID = Convert.ToInt32(fgList[i, "Service_ID"]);
                        AF_Recs.Invoice_ID = 0;
                        //AF_Recs.Invoice_Num = "";
                        //AF_Recs.Invoice_File = "";
                        AF_Recs.DateFees = Convert.ToDateTime("1900/01/01");
                        //AF_Recs.Invoice_Type = Convert.ToInt32(fgList[i, "ClientType"]);      // ClientType=1-idiotis -> Invoice_Type=1-ΑΠΥ, ClientType=2-Etairia -> Invoice_Type=2-ΤΠΥ
                        AF_Recs.Tipos = 0;                                                    // 0 - regular record, 4 - pistotiko, 5 - akyrotiko
                        AF_Recs.Status = 1;                                                   // 1 - Active, 2 - Cancelled
                        AF_Recs.User_ID = 0;
                        AF_Recs.DateEdit = Convert.ToDateTime("1900/01/01");
                        AF_Recs.InsertRecord();
                    }
                }
            }
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            int j, k;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Λίστα συμβάέων";
            var loopTo = fgList.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                k = 1;
                for (j = 0; j <= 31; j++) if (fgList.Cols[j].Visible) EXL.Cells[i + 2, k++].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }  
 
        private void ShowList()
        {
            int i = 0;
            int k = 0;
            int iDays = 0;
            int iSumDays = 0;
            int iOld_Contract_ID = 0;
            int iOld_Contract_Packages_ID = 0;
            float sgMF_AmoiviPro = 0, sgMF_Discount_Percent = 0, sgMF_AmoiviAfter = 0, sgMF_MinAmoivi = 0, sgMF_MinDiscount = 0,
                  sgMF_MinFinish = 0, sgAF_AmoiviPro = 0, sgAF_Discount_Percent = 0, sgAF_AmoiviAfter = 0, sgAF_MinAmoivi = 0, sgAF_MinDiscount = 0, sgAF_Month3_Fees = 0;
            string sTemp = "";
            string sMF_Climakas = "", sMF_Discount_DateTo = "";
            DateTime dStart, dFinish;

            dStart = Convert.ToDateTime("1900/01/01");
            dFinish = Convert.ToDateTime("1900/01/01");

            if (bCheckList)
            {

                bCheckPackages = false;
                iOld_Contract_ID = -999;
                i = 0;
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                foreach (DataRow row in Contracts.List.Rows)
                {
                    if (iOld_Contract_Packages_ID != Convert.ToInt32(row["Contracts_Packages_ID"]))
                    {
                        iOld_Contract_Packages_ID = Convert.ToInt32(row["Contracts_Packages_ID"]);

                        if ((Convert.ToInt32(cmbAdvisors.SelectedValue) == 0 || row["User1_ID"] == cmbAdvisors.SelectedValue) &&
                            (Convert.ToInt32(cmbRM.SelectedValue) == 0 || row["User2_ID"] == cmbRM.SelectedValue) &&
                            (txtCode.Text == "" || row["Code"].ToString().Contains(txtCode.Text)) &&
                            (!chkMiFID_2.Checked || Convert.ToInt32(row["MIFID_2"]) == 1) &&
                            sServicesList.Contains(row["Service_ID"].ToString()))
                        {

                            if ((cmbViewType.SelectedIndex == 1) || (cmbViewType.SelectedIndex == 2))
                            {                                                  // Trimino mode
                                if (Convert.ToDateTime(row["DateStart"]) < ucDates.DateFrom) { dStart = ucDates.DateFrom; }
                                else { dStart = Convert.ToDateTime(row["DateStart"]); }

                                if (Convert.ToDateTime(row["DateFinish"]) > ucDates.DateTo) { dFinish = ucDates.DateTo; }
                                else { dFinish = Convert.ToDateTime(row["DateFinish"]); }
                            }
                            else
                            {
                                dStart = Convert.ToDateTime(row["DateStart"]);
                                dFinish = Convert.ToDateTime(row["DateFinish"]);
                            }

                            iDays = Convert.ToInt32((dFinish - dStart).TotalDays) + 1;
                            if (Convert.ToInt32(row["ID"]) != iOld_Contract_ID)
                            {
                                if (iDays > 90) iDays = 90;
                                iSumDays = iDays;
                            }
                            else
                            {
                                iSumDays = iSumDays + iDays;
                                if (iSumDays > 90)
                                {
                                    k = iSumDays - 90;
                                    iSumDays = 90;
                                    iDays = iDays - k;
                                }
                            }
                            iOld_Contract_ID = Convert.ToInt32(row["ID"]);

                            sgMF_AmoiviPro = 0;
                            sgMF_Discount_Percent = 0;
                            sgMF_AmoiviAfter = 0;
                            sMF_Climakas = "";
                            sMF_Discount_DateTo = "";
                            sgMF_MinAmoivi = 0;
                            sgMF_MinDiscount = 0;
                            sgMF_MinFinish = 0;
                            sgAF_AmoiviPro = 0;
                            sgAF_Discount_Percent = 0;
                            sgAF_AmoiviAfter = 0;
                            sgAF_MinAmoivi = 0;
                            sgAF_MinDiscount = 0;
                            sgAF_Month3_Fees = 0;

                            switch (row["Service_ID"])
                            {
                                case 2:         // Advisory
                                    if (!String.IsNullOrEmpty(row["Advisory_AmoiviPro"].ToString()))
                                    {
                                        sTemp = row["Advisory_Climakas"] + "";
                                        if (sTemp.Length > 0 && sTemp.IndexOf("-") > 0) sMF_Climakas = row["Advisory_Climakas"].ToString() + "";
                                        else
                                        {
                                            sgMF_AmoiviPro = Convert.ToSingle(row["Advisory_AmoiviPro"]);
                                            if ((cmbViewType.SelectedIndex == 0 || (Convert.ToDateTime(row["AdvisoryDiscount_DateFrom"]) <= ucDates.DateTo && Convert.ToDateTime(row["AdvisoryDiscount_DateTo"]) >= ucDates.DateFrom)))
                                            {
                                                sgMF_Discount_Percent = Convert.ToSingle(row["Advisory_Discount_Percent"]);
                                                sgMF_AmoiviAfter = Convert.ToSingle(row["Advisory_AmoiviAfter"]);
                                            }
                                            else
                                            {
                                                sgMF_Discount_Percent = 0;
                                                sgMF_AmoiviAfter = Convert.ToSingle(row["Advisory_AmoiviPro"]);
                                            }
                                            sMF_Climakas = "";
                                        }
                                    }
                                    sMF_Discount_DateTo = "";
                                    if ((float)row["Advisory_Discount_Percent"] != 0 ) sMF_Discount_DateTo =  Convert.ToDateTime(row["AdvisoryDiscount_DateTo"]).ToString("dd/MM/yyyy");

                                    if (!String.IsNullOrEmpty(row["Advisory_MonthMinAmount"].ToString())) sgMF_MinAmoivi = Convert.ToSingle(row["Advisory_MonthMinAmount"]);

                                    if (!String.IsNullOrEmpty(row["AdvisoryMonth3_Discount"].ToString()))
                                    { 
                                        sgMF_MinDiscount = Convert.ToSingle(row["AdvisoryMonth3_Discount"]);
                                        sgMF_MinFinish = Convert.ToSingle(Math.Round(sgMF_MinAmoivi - sgMF_MinAmoivi * sgMF_MinDiscount / 100.0));
                                    }
                                    break;

                                case 3:         // Dicretionary
                                    if (!String.IsNullOrEmpty(row["Discret_AmoiviPro"].ToString()))
                                    {
                                        sTemp = row["Discret_Climakas"] + "";
                                        if (sTemp.Length > 0 && sTemp.IndexOf("-") > 0) sMF_Climakas = row["Discret_Climakas"].ToString() + "";
                                        else
                                        {
                                            sgMF_AmoiviPro = Convert.ToSingle(row["Discret_AmoiviPro"]);
                                            if ((cmbViewType.SelectedIndex == 0 || (Convert.ToDateTime(row["DiscretDiscount_DateFrom"]) <= ucDates.DateTo && Convert.ToDateTime(row["DiscretDiscount_DateTo"]) >= ucDates.DateFrom)))
                                            {
                                                sgMF_Discount_Percent = Convert.ToSingle(row["Discret_Discount_Percent"]);
                                                sgMF_AmoiviAfter = Convert.ToSingle(row["Discret_AmoiviAfter"]);
                                            }
                                            else
                                            {
                                                sgMF_Discount_Percent = 0;
                                                sgMF_AmoiviAfter = Convert.ToSingle(row["Discret_AmoiviPro"]);
                                            }
                                            sMF_Climakas = "";
                                        }
                                    }
                                    sMF_Discount_DateTo = "";
                                    if ((float)row["Discret_Discount_Percent"] != 0) sMF_Discount_DateTo = Convert.ToDateTime(row["DiscretDiscount_DateTo"]).ToString("dd/MM/yyyy");

                                    if (!String.IsNullOrEmpty(row["Discret_MonthMinAmount"].ToString())) sgMF_MinAmoivi = Convert.ToSingle(row["Discret_MonthMinAmount"]);

                                    if (!String.IsNullOrEmpty(row["DiscretMonth3_Discount"].ToString()))
                                    {
                                        sgMF_MinDiscount = Convert.ToSingle(row["DiscretMonth3_Discount"]);
                                        sgMF_MinFinish = Convert.ToSingle(Math.Round(sgMF_MinAmoivi - sgMF_MinAmoivi * sgMF_MinDiscount / 100.0));
                                    }
                                    break;

                                case 5:         // DealAdvisory
                                    sgMF_AmoiviPro = Convert.ToSingle(row["DealAdvisoryFeesAmount"]);
                                    sgMF_Discount_Percent = Convert.ToSingle(row["DealAdvisoryFees_Discount"]);
                                    sgMF_AmoiviAfter = Convert.ToSingle(row["DealAdvisoryFees"]);
                                    sMF_Climakas = "";
                                    sgMF_MinAmoivi = Convert.ToSingle(row["DealAdvisory_MonthMinAmount"]);
                                    sgMF_MinDiscount = Convert.ToSingle(row["DealAdvisoryMonth3_Discount"]);
                                    sgMF_MinFinish = Convert.ToSingle(Math.Round(sgMF_MinAmoivi - sgMF_MinAmoivi * sgMF_MinDiscount / 100.0));
                                    break;
                            }

                            if (!String.IsNullOrEmpty(row["AdminFeesPercent"].ToString()))
                            {
                                sgAF_AmoiviPro = Convert.ToSingle(row["AdminFeesPercent"]);

                                if (!String.IsNullOrEmpty(row["AdminFees_Discount"].ToString()))
                                {
                                    sgAF_Discount_Percent = Convert.ToSingle(row["AdminFees_Discount"]);
                                    sgAF_AmoiviAfter = Convert.ToSingle(row["AdminFees"]);
                                }

                                sMF_Discount_DateTo = "";

                                if (!String.IsNullOrEmpty(row["Admin_MonthMinAmount"].ToString()))
                                {
                                    sgAF_MinAmoivi = Convert.ToSingle(row["Admin_MonthMinAmount"]);
                                }
                                if (!String.IsNullOrEmpty(row["AdminMonth3_Discount"].ToString()))
                                {
                                    sgAF_MinDiscount = Convert.ToSingle(row["AdminMonth3_Discount"]);
                                }
                                if (!String.IsNullOrEmpty(row["AdminMonth3_Fees"].ToString()))
                                {
                                    sgAF_Month3_Fees = Convert.ToSingle(row["AdminMonth3_Fees"]);
                                }
                            }

                            i += 1;
                            fgList.AddItem(i + "\t" + row["ContractTitle"] + "\t" + row["PackageProvider_Title"] + "\t" + row["Code"] + "\t" + row["Portfolio"] + "\t" +
                                       row["Currency"] + "\t" + dStart.ToString("dd/MM/yyyy") + "\t" + dFinish.ToString("dd/MM/yyyy") + "\t" + iDays + "\t" + row["PackageTitle"] + "\t" + 
                                       row["Service_Title"] + "\t" + row["InvestmentProfile_Title"] + "\t" + row["InvestmentPolicy_Title"] + "\t" + row["AUM"] + "\t" +
                                       sgMF_AmoiviPro + "\t" + sgMF_Discount_Percent + "\t" + sgMF_AmoiviAfter + "\t" + sMF_Discount_DateTo + "\t" + sMF_Climakas + "\t" + 
                                       sgMF_MinAmoivi + "\t" + sgMF_MinDiscount + "\t" + 
                                       sgMF_MinFinish + "\t" + sgAF_AmoiviPro + "\t" + sgAF_Discount_Percent + "\t" + sgAF_AmoiviAfter + "\t" + sgAF_MinAmoivi + "\t" + sgAF_MinDiscount + "\t" + 
                                       sgAF_Month3_Fees + "\t" + row["AdvisorName"] + "\t" + row["RMName"] + "\t" + row["IntroName"] + "\t" + row["DiaxName"] + "\t" + row["User1Name"] + "\t" +
                                       row["ID"] + "\t" + row["Contracts_Details_ID"] + "\t" + row["Contracts_Packages_ID"] + "\t" + row["CFP_ID"] + "\t" + row["Client_ID"] + "\t" + 
                                       row["Service_ID"] + "\t" + row["ClientType"] + "\t" + row["VATPercent"] + "\t" + row["Status"] + "\t" + "" + "\t" + row["User1_ID"] + "\t" + 
                                       row["User2_ID"] + "\t" + row["User3_ID"] + "\t" + row["User4_ID"] + "\t" + row["BrokerageServiceProvider_ID"] + "\t" + 
                                       row["AdvisoryServiceProvider_ID"] + "\t" + row["DiscretServiceProvider_ID"] + "\t" + row["DealAdvisoryServiceProvider_ID"] + "\t" + row["AdminServiceProvider_ID"]);
                        }
                    }
                }
            }

            fgList.Redraw = true;
            bCheckPackages = true;
        }
        private void ShowDetails()
        {
            if (fgList.Rows.Count > 1)
            {
                if (fgList.Row > 0)
                {
                    dPackageDateStart = Convert.ToDateTime(fgList[fgList.Row, "DateFrom"]);
                    dPackageDateFinish = Convert.ToDateTime(fgList[fgList.Row, "DateTo"]);
                    iContract_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    iContract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
                    iContract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);

                    ShowGridsData(1);

                    switch (Convert.ToInt32(fgList[fgList.Row, "Service_ID"]))
                    {
                        case 1:                                      // 1 - RTO
                            tcFees.SelectedIndex = 1;
                            break;
                        case 2:                                      // 2 - Advisory
                            tcFees.SelectedIndex = 6;
                            break;
                        case 3:                                      // 3 - Discret
                            tcFees.SelectedIndex = 7;
                            break;
                        case 4:                                      // 4 - Safekeeping
                            tcFees.SelectedIndex = 3;
                            break;
                        case 5:                                      // 5 - Dealing Advisory
                            tcFees.SelectedIndex = 8;
                            break;
                        case 6:                                      // 6 - Lombard Lending
                            tcFees.SelectedIndex = 10;
                            break;
                        default:
                            tcFees.SelectedIndex = 0;
                            break;
                    }
                }
            }
        }

        private void ShowGridsData(int iClientFees)
        {
            bCheckPackages = false;

            clsContracts klsContract = new clsContracts();
            klsContract.Record_ID = iContract_ID;
            klsContract.Contract_Details_ID = iContract_Details_ID;
            klsContract.Contract_Packages_ID = iContract_Packages_ID;
            klsContract.GetRecord();

            iBrokerageOption_ID = klsContract.BrokerageOption_ID;
            lblBrokerageServiceProvider.Text = klsContract.BrokerageServiceProvider_Title;
            lblBrokerageOption.Text = klsContract.BrokerageOption_Title;

            iRTOOption_ID = klsContract.RTOOption_ID;
            lblRTOServiceProvider.Text = klsContract.RTOServiceProvider_Title;
            lblRTOOption.Text = klsContract.RTOOption_Title;

            lblAdvisoryServiceProvider.Text = klsContract.AdvisoryServiceProvider_Title;
            iAdvisoryProvider_ID = klsContract.AdvisoryServiceProvider_ID;
            lblAdvisoryOption.Text = klsContract.AdvisoryOption_Title;
            iAdvisoryOption_ID = klsContract.AdvisoryOption_ID;
            lblAdvisoryInvestmentProfile.Text = klsContract.AdvisoryInvestmentProfile_Title;
            iAdvisoryInvestmentProfile_ID = klsContract.AdvisoryInvestmentProfile_ID;
            lblAdvisoryInvestmentPolice.Text = klsContract.AdvisoryInvestmentPolicy_Title;
            iAdvisoryInvestmentPolicy_ID = klsContract.AdvisoryInvestmentPolicy_ID;

            lblDiscretServiceProvider.Text = klsContract.DiscretServiceProvider_Title;
            iDiscretProvider_ID = klsContract.DiscretServiceProvider_ID;
            lblDiscretOption.Text = klsContract.DiscretOption_Title;
            iDiscretOption_ID = klsContract.DiscretOption_ID;
            lblDiscretInvestmentProfile.Text = klsContract.DiscretInvestmentProfile_Title;
            iDiscretInvestmentProfile_ID = klsContract.DiscretInvestmentProfile_ID;
            lblDiscretInvestmentPolice.Text = klsContract.DiscretInvestmentPolicy_Title;
            iDiscretInvestmentPolicy_ID = klsContract.DiscretInvestmentPolicy_ID;
            if (!String.IsNullOrEmpty(klsContract.Discret_MonthMinAmount + "")) lblDiscret_MonthMinAmount.Text = klsContract.Discret_MonthMinAmount + "";
            else lblDiscret_MonthMinAmount.Text = "0";
            lblDiscret_MonthMinCurr.Text = klsContract.Discret_MonthMinCurr;

            lblCustodyServiceProvider.Text = klsContract.CustodyServiceProvider_Title;
            iCustodyProvider_ID = klsContract.CustodyServiceProvider_ID;
            lblCustodyOption.Text = klsContract.CustodyOption_Title;
            iCustodyOption_ID = klsContract.CustodyOption_ID;
            //dCustodyFrom_Month3_.Value = dPackageDateStart;
            //dCustodyTo_Month3_.Value = dPackageDateFinish;
            if (!String.IsNullOrEmpty(klsContract.Custody_MonthMinAmount + "")) lblCustodyMonthMinAmount.Text = klsContract.Custody_MonthMinAmount + "";
            else lblCustodyMonthMinAmount.Text = "0";
            lblCustodyMonthMinCurrency.Text = klsContract.Custody_MonthMinCurr;

            lblAdminServiceProvider.Text = klsContract.AdminServiceProvider_Title;
            iAdminProvider_ID = klsContract.AdminServiceProvider_ID;
            lblAdminOption.Text = klsContract.AdminOption_Title;
            iAdminOption_ID = klsContract.AdminOption_ID;
            if (!String.IsNullOrEmpty(klsContract.Admin_MonthMinAmount + "")) lblAdmin_MonthMinAmount.Text = klsContract.Admin_MonthMinAmount + "";
            else lblAdmin_MonthMinAmount.Text = "0";
            lblAdmin_MonthMinCurr.Text = klsContract.Admin_MonthMinCurr;

            lblDealAdvisoryServiceProvider.Text = klsContract.DealAdvisoryServiceProvider_Title;
            iDealAdvisoryProvider_ID = klsContract.DealAdvisoryServiceProvider_ID;
            lblDealAdvisoryOption.Text = klsContract.DealAdvisoryOption_Title;
            iDealAdvisoryOption_ID = klsContract.DealAdvisoryOption_ID;
            lblDealAdvisoryFinanceTools.Text = klsContract.DealAdvisoryInvestmentPolicy_Title;
            iDealAdvisoryInvestmentPolicy_ID = klsContract.DealAdvisoryInvestmentPolicy_ID;

            lblLombardServiceProvider.Text = klsContract.LombardServiceProvider_Title;
            iLombardProvider_ID = klsContract.LombardServiceProvider_ID;
            lblLombardOption.Text = klsContract.LombardOption_Title;
            iLombardOption_ID = klsContract.LombardOption_ID;
            lblLombardAMR.Text = klsContract.Lombard_AMR;

            lblFXServiceProvider.Text = klsContract.FXServiceProvider_Title;
            iFXProvider_ID = klsContract.FXServiceProvider_ID;
            lblFXOption.Text = klsContract.FXOption_Title;
            iFXOption_ID = klsContract.FXOption_ID;

            iSettlementsOption_ID = klsContract.SettlementsOption_ID;
            lblSettlementsOption.Text = klsContract.SettlementsOption_Title;
            lblSettlementsServiceProvider.Text = klsContract.SettlementsServiceProvider_Title;

            //------------------ Show Fees Grids ------------------
            ShowBrokerageFees();
            ShowRTOFees();
            ShowAdvisoryFees();
            ShowDiscretFees();
            ShowCustodytFees();
            ShowAdminFees();
            ShowSettlementFees();
            ShowDealAdvisoryFees();
            ShowLombardFees();
            ShowFXFees();

            ShowFeesButtons();

            bCheckPackages = true;
        }
        private void ShowBrokerageFees()
        {
            //------------------ initialize fgBrokerageFees grid ------------------
            fgBrokerageFees.Redraw = false;
            fgBrokerageFees.Rows.Count = 2;
            if (iBrokerageOption_ID != 0)
            {
                clsClientsBrokerageFees klsClientsBrokerageFees = new clsClientsBrokerageFees();
                klsClientsBrokerageFees.Option_ID = iBrokerageOption_ID;
                klsClientsBrokerageFees.DateFrom = dPackageDateStart;
                klsClientsBrokerageFees.DateTo = dPackageDateFinish;
                klsClientsBrokerageFees.Contract_ID = iContract_ID;
                klsClientsBrokerageFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsBrokerageFees.IncludeDiscount = true;
                klsClientsBrokerageFees.GetList();

                foreach (DataRow dtRow in klsClientsBrokerageFees.List.Rows)
                {
                    fgBrokerageFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                               dtRow["StockExchanges_Title"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                               dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                               dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                               dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" +
                               dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                               dtRow["FeesDiscountPercent"] + "\t" + dtRow["TicketFeesDiscountPercent"] + "\t" +
                               dtRow["FinishBuyFeesPercent"] + "\t" + dtRow["FinishSellFeesPercent"] + "\t" +
                               dtRow["TicketFinishBuyFeesAmount"] + "\t" + dtRow["TicketFinishSellFeesAmount"] + "\t" + dtRow["ID"] + "\t" +
                               dtRow["SPBF_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
            }
            fgBrokerageFees.Redraw = true;
        }
        private void ShowRTOFees()
        {
            fgRTOFees.Redraw = false;
            fgRTOFees.Rows.Count = 2;
            if (iRTOOption_ID != 0)
            {
                clsClientsRTOFees klsClientsRTOFees = new clsClientsRTOFees();
                klsClientsRTOFees.Option_ID = iRTOOption_ID;
                klsClientsRTOFees.DateFrom = dPackageDateStart;
                klsClientsRTOFees.DateTo = dPackageDateFinish;
                klsClientsRTOFees.Contract_ID = iContract_ID;
                klsClientsRTOFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsRTOFees.IncludeDiscount = true;                                   
                klsClientsRTOFees.GetList();

                foreach (DataRow dtRow in klsClientsRTOFees.List.Rows)
                {
                    fgRTOFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                            dtRow["StockExchanges_Title"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                            dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                            dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                            dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" +
                            dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                            dtRow["FeesDiscountPercent"] + "\t" + dtRow["TicketFeesDiscountPercent"] + "\t" + "" + "\t" +
                            dtRow["FinishBuyFeesPercent"] + "\t" + dtRow["FinishSellFeesPercent"] + "\t" +
                            dtRow["TicketFinishBuyFeesAmount"] + "\t" + dtRow["TicketFinishSellFeesAmount"] + "\t" + dtRow["MinimumFees"] + "\t" + dtRow["ID"] + "\t" +
                            dtRow["SPBF_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
            }
            fgRTOFees.Redraw = true;
        }
        private void ShowAdvisoryFees()
        {
            bCheckPackages = false;
            fgAdvisoryFees.Redraw = false;
            fgAdvisoryFees.Rows.Count = 2;
            if (iAdvisoryProvider_ID != 0 & iAdvisoryOption_ID != 0)
            {
                clsClientsAdvisoryFees ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                dTemp = Convert.ToDateTime("1900/01/01");
                iOldContract_ID = -999;
                iOldContract_Packages_ID = -999;
                ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                ClientsAdvisoryFees.ServiceProvider_ID = iAdvisoryProvider_ID;
                ClientsAdvisoryFees.Option_ID = iAdvisoryOption_ID;
                ClientsAdvisoryFees.InvestmentProfile_ID = iAdvisoryInvestmentProfile_ID;
                ClientsAdvisoryFees.InvestmentPolicy_ID = iAdvisoryInvestmentPolicy_ID;
                ClientsAdvisoryFees.DateFrom = ucDates.DateFrom;                      // dPackageDateStart
                ClientsAdvisoryFees.DateTo = ucDates.DateTo;                          // dPackageDateFinish
                ClientsAdvisoryFees.Contract_ID = iContract_ID;
                ClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsAdvisoryFees.GetList_Package_ID();
                foreach (DataRow dtRow in ClientsAdvisoryFees.List.Rows)
                {
                    if (dTemp == Convert.ToDateTime("1900/01/01") ||
                        (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) && iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))
                    {
                        dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                        iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                        iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);

                        fgAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdvisoryFees"] + "\t" +
                                               dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AdvisoryFees_Discount"] + "\t" +
                                               dtRow["FinishAdvisoryFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPAF_ID"] + "\t" +
                                               dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                               dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"]);

                        lblAdvisory_MonthMinAmount.Text = dtRow["MonthMinAmount"].ToString();
                        lblAdvisory_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                        txtAdvisory_MinimumFees_Discount.Text = dtRow["MinimumFees_Discount"].ToString();
                        txtAdvisory_MinimumFees.Text = dtRow["MinimumFees"].ToString();
                        lblAdvisory_AllManFees.Text = dtRow["AllManFees"] + "";
                    }
                }
            }

            fgAdvisoryFees.Row = 0;
            fgAdvisoryFees.Redraw = true;
            bCheckPackages = true;
            if (fgAdvisoryFees.Rows.Count > 2)
            {
                fgAdvisoryFees.Row = 1;
                //    fgAdvisoryFees.Row = 2;
                //    if (fgAdvisoryFees.Rows.Count == 3)
                //    {
                //        fgList[fgList.Row, "MF_Discount_Percent"] = fgAdvisoryFees[2, "AdvisoryFees_Discount"];
                //        fgList[fgList.Row, "MF_AmoiviAfter"] = fgAdvisoryFees[2, "FinishAdvisoryFee"];
                //        fgList[fgList.Row, "MF_Climakas"] = "";
                //    }
                //    else
                //    {
                //        fgList[fgList.Row, "MF_Discount_Percent"] = "0";
                //        fgList[fgList.Row, "MF_AmoiviAfter"] = "0";
                //        fgList[fgList.Row, "MF_Climakas"] = lblAdvisory_AllManFees.Text;
                //    }

                //    fgList[fgList.Row, "MF_MinAmoivi"] = lblAdvisory_MonthMinAmount.Text;
                //    fgList[fgList.Row, "MF_MinAmoivi_Percent"] = txtAdvisory_MinimumFees_Discount.Text;
                //    fgList[fgList.Row, "MF_MinFinish"] = txtAdvisory_MinimumFees.Text;
            }
            ShowFeesButtons();
        }

        private void ShowDiscretFees()
        {
            bCheckPackages = false;
            fgDiscretFees.Redraw = false;
            fgDiscretFees.Rows.Count = 2;
            if (iDiscretProvider_ID != 0 & iDiscretOption_ID != 0)
            {
                clsClientsDiscretFees ClientsDiscretFees = new clsClientsDiscretFees();
                dTemp = Convert.ToDateTime("1900/01/01");
                iOldContract_ID = -999;
                iOldContract_Packages_ID = -999;
                ClientsDiscretFees = new clsClientsDiscretFees();
                ClientsDiscretFees.ServiceProvider_ID = iDiscretProvider_ID;
                ClientsDiscretFees.Option_ID = iDiscretOption_ID;
                ClientsDiscretFees.InvestmentProfile_ID = iDiscretInvestmentProfile_ID;
                ClientsDiscretFees.InvestmentPolicy_ID = iDiscretInvestmentPolicy_ID;
                ClientsDiscretFees.DateFrom = ucDates.DateFrom;                        // dPackageDateStart
                ClientsDiscretFees.DateTo = ucDates.DateTo;                            // dPackageDateFinish
                ClientsDiscretFees.Contract_ID = iContract_ID;
                ClientsDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsDiscretFees.GetList_Package_ID();
                foreach (DataRow dtRow in ClientsDiscretFees.List.Rows)
                {
                    if (dTemp == Convert.ToDateTime("1900/01/01") ||
                        (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) && iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))
                    {
                        dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                        iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                        iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);

                        fgDiscretFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DiscretFees"] + "\t" +
                                               dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["DiscretFees_Discount"] + "\t" +
                                               dtRow["FinishDiscretFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPDF_ID"] + "\t" +
                                               dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                               dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"]);

                        lblDiscret_MonthMinAmount.Text = dtRow["MonthMinAmount"].ToString();
                        lblDiscret_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                        txtDiscret_MinimumFees_Discount.Text = dtRow["MinimumFees_Discount"].ToString();
                        txtDiscret_MinimumFees.Text = dtRow["MinimumFees"].ToString();
                        lblDiscret_AllManFees.Text = dtRow["AllManFees"] + "";
                    }
                }
            }

            fgDiscretFees.Row = 0;
            fgDiscretFees.Redraw = true;
            bCheckPackages = true;
            //if (fgDiscretFees.Rows.Count > 2)
            //{
                //fgDiscretFees.Row = 2;
                //if (fgDiscretFees.Rows.Count == 3)
                //{
                    //fgList[fgList.Row, "MF_Discount_Percent"] = fgDiscretFees[2, "DiscretFees_Discount"];
                    //fgList[fgList.Row, "MF_AmoiviAfter"] = fgDiscretFees[2, "FinishDiscretFee"];
                    //fgList[fgList.Row, "MF_Climakas"] = "";
                //}
                //else
                //{
                    //fgList[fgList.Row, "MF_Discount_Percent"] = "0";
                    //fgList[fgList.Row, "MF_AmoiviAfter"] = "0";
                    //fgList[fgList.Row, "MF_Climakas"] = lblDiscret_AllManFees.Text;
                //}

                //fgList[fgList.Row, "MF_MinAmoivi"] = lblDiscret_MonthMinAmount.Text;
                //fgList[fgList.Row, "MF_MinAmoivi_Percent"] = txtDiscret_MinimumFees_Discount.Text;
                //fgList[fgList.Row, "MF_MinFinish"] = txtDiscret_MinimumFees.Text;
            //}

            ShowFeesButtons();
        }
        private void ShowCustodytFees()
        {

        }
        private void ShowAdminFees()
        {
            bCheckPackages = false;
            fgAdminFees.Redraw = false;
            fgAdminFees.Rows.Count = 2;
            if (iAdminProvider_ID != 0 && iAdminOption_ID != 0) {
                dTemp = Convert.ToDateTime("1900/01/01");
                iOldContract_ID = -999;
                iOldContract_Packages_ID = -999;
                clsClientsAdminFees klsClientsAdminFees = new clsClientsAdminFees();
                klsClientsAdminFees.ServiceProvider_ID = iAdminProvider_ID;
                klsClientsAdminFees.Option_ID = iAdminOption_ID;
                klsClientsAdminFees.DateFrom = dPackageDateStart;
                klsClientsAdminFees.DateTo = dPackageDateFinish;
                klsClientsAdminFees.Contract_ID = iContract_ID;
                klsClientsAdminFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsAdminFees.GetList_Package_ID();
                foreach (DataRow dtRow in klsClientsAdminFees.List.Rows) {
                    if ((dTemp == Convert.ToDateTime("1900/01/01")) ||
                        (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) && iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))
                    {
                        dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                        iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                        iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                        fgAdminFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdminFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AdminFees_Discount"] + "\t" +
                                       dtRow["FinishAdminFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPAF_ID"] + "\t" +
                                       dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                       dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"]);

                        lblAdmin_MonthMinAmount.Text = dtRow["MonthMinAmount"] + "";
                        lblAdmin_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                        txtAdmin_MinimumFees_Discount.Text = dtRow["MinimumFees_Discount"] + "";
                        txtAdmin_MinimumFees.Text = dtRow["MinimumFees"] + "";
                    }
                }
            }

            fgAdminFees.Row = 0;
            fgAdminFees.Redraw = true;
            bCheckPackages = true;
            if (fgAdminFees.Rows.Count > 2) {
                fgAdminFees.Row = 2;

                if (fgAdminFees.Rows.Count == 3)
                {
                    fgList[fgList.Row, "AF_Discount_Percent"] = fgAdminFees[2, "AdminFees_Discount"];
                    fgList[fgList.Row, "AF_AmoiviAfter"] = fgAdminFees[2, "FinishAdminFee"];
                }
                else
                {
                    fgList[fgList.Row, "AF_Discount_Percent"] = "0";
                    fgList[fgList.Row, "AF_AmoiviAfter"] = "0";
                }

                fgList[fgList.Row, "AF_MinAmoivi"] = Convert.ToDecimal(lblAdmin_MonthMinAmount.Text);
                fgList[fgList.Row, "AF_MinAmoivi_Percent"] = txtAdmin_MinimumFees_Discount.Text;
                fgList[fgList.Row, "AF_MinFinish"] = Convert.ToDecimal(txtAdmin_MinimumFees.Text);
            }

            ShowFeesButtons();
        }
        private void ShowSettlementFees()
        {

        }
        private void ShowDealAdvisoryFees()
        {
            dTemp = Convert.ToDateTime("1900/01/01");
            iOldContract_ID = -999;
            iOldContract_Packages_ID = -999;

            fgDealAdvisoryFees.Redraw = false;
            fgDealAdvisoryFees.Rows.Count = 2;
            clsClientsDealAdvisoryFees ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
            ClientsDealAdvisoryFees.ServiceProvider_ID = iDealAdvisoryProvider_ID;
            ClientsDealAdvisoryFees.Option_ID = iDealAdvisoryOption_ID;
            ClientsDealAdvisoryFees.InvestmentPolicy_ID = iDealAdvisoryInvestmentPolicy_ID;
            ClientsDealAdvisoryFees.DateFrom = dPackageDateStart;
            ClientsDealAdvisoryFees.DateTo = dPackageDateFinish;
            ClientsDealAdvisoryFees.Contract_ID = iContract_ID;
            ClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
            ClientsDealAdvisoryFees.GetList_Package_ID();
            foreach (DataRow dtRow in ClientsDealAdvisoryFees.List.Rows)
            {
                if (dTemp == Convert.ToDateTime("1900/01/01") ||
                   (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) && iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))
                {
                    dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                    iOldContract_ID = (int)dtRow["Contract_ID"];
                    iOldContract_Packages_ID = (int)dtRow["Contract_Packages_ID"];
                    fgDealAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DealAdvisoryFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["DealAdvisoryFees_Discount"] + "\t" +
                                       dtRow["FinishDealAdvisoryFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPDAF_ID"]);
                }
            }
            fgDealAdvisoryFees.Redraw = true;

            ShowFeesButtons();
        }
        private void ShowLombardFees()
        {
            fgLombardFees.Redraw = false;
            fgLombardFees.Rows.Count = 1;
            clsClientsLombardFees klsClientsLombardFees = new clsClientsLombardFees();
            klsClientsLombardFees.ServiceProvider_ID = iLombardProvider_ID;
            klsClientsLombardFees.Option_ID = iLombardOption_ID;
            klsClientsLombardFees.DateFrom = dPackageDateStart;
            klsClientsLombardFees.DateTo = dPackageDateFinish;
            klsClientsLombardFees.Contract_ID = iContract_ID;
            klsClientsLombardFees.Contract_Packages_ID = iContract_Packages_ID;
            klsClientsLombardFees.GetList();
            foreach (DataRow dtRow in klsClientsLombardFees.List.Rows)
                  fgLombardFees.AddItem(dtRow["Currency"] + "\t" + dtRow["ID"]);
            fgLombardFees.Redraw = true;
        }
        private void ShowFXFees()
        {
            fgFXFees.Redraw = false;
            fgFXFees.Rows.Count = 2;

            if (iFXProvider_ID != 0 && iFXOption_ID != 0 )
            {
                clsClientsFXFees klsClientsFXFees = new clsClientsFXFees();
                klsClientsFXFees.ServiceProvider_ID = iFXProvider_ID;
                klsClientsFXFees.Option_ID = iFXOption_ID;
                klsClientsFXFees.DateFrom = dPackageDateStart;
                klsClientsFXFees.DateTo = dPackageDateFinish;
                klsClientsFXFees.Contract_ID = iContract_ID;
                klsClientsFXFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsFXFees.IncludeDiscount = true;
                klsClientsFXFees.GetList_Package_ID();
                foreach (DataRow dtRow in klsClientsFXFees.List.Rows)
                    fgFXFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FXFees"] + "\t" +
                             dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["FXFees_Discount"] + "\t" +
                             dtRow["FinishFXFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPFF_ID"]);
            }
            fgFXFees.Redraw = true;
        }
        private void ShowFeesButtons()
        {

        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }

    }
}
