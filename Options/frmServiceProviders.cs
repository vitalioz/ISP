using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class frmServiceProviders : Form
    {
        DataTable dtPackages, dtOptions, dtList;
        DataRow dtRow, dtRow1;
        DataRow[] foundRows;
        DataView dtView, dtView2;
        CellRange rng;
        int i, iID, iRow, iAction = 0, iService, iLocAktion, iOption_ID, iFees_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iRightsLevel;
        string sTemp, sExtra;
        string[] sDistrib = { "", "Σταθερής Χρέωσης", "Ποσοστέα Επιστροφή" };
        bool bCheckList, bCheckBrokerageFees, bCheckRTOFees, bCheckAdvisoryFees, bCheckDealAdvisoryFees, bCheckDiscretFees, bCheckSafekeepingFees, bCheckAdministrationFees,
             bCheckLombardFees, bCheckFXFees, bCheckSettlementsFees, bListChanged;
        SortedList lstCurr = new SortedList();
        clsContracts Contracts = new clsContracts();
        clsContracts_Details Contract_Details = new clsContracts_Details();
        clsContracts_Packages Contract_Packages = new clsContracts_Packages();
        clsServiceProviders ServiceProviders = new clsServiceProviders();
        clsServiceProvidersOptions ServiceProvidersOptions = new clsServiceProvidersOptions();
        clsServiceProviderBrokerageFees ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();
        clsServiceProviderRTOFees ServiceProviderRTOFees = new clsServiceProviderRTOFees();
        clsServiceProviderFXFees ServiceProviderFXFees = new clsServiceProviderFXFees();
        clsServiceProviderAdvisoryFees ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
        clsServiceProviderDiscretFees ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
        clsServiceProviderDealAdvisoryFees ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
        clsServiceProviderCustodyFees ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
        clsServiceProviderAdminFees ServiceProviderAdminFees = new clsServiceProviderAdminFees();
        clsServiceProviderLombardFees ServiceProviderLombardFees = new clsServiceProviderLombardFees();
        clsServiceProviderSettlementsFees ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();
        clsCompanyPackages CompanyPackage = new clsCompanyPackages();

        #region --- Start functions ----------------------------------------------------------------------------------------
        public frmServiceProviders()
        {
            InitializeComponent();

            iAction = 0;
            iLocAktion = 0;
        }

        private void frmServiceProviders_Load(object sender, EventArgs e)
        {
            //--- define currencies list ------------------------
            cmbMonthMinCurr.DataSource = Global.dtCurrencies.Copy();
            cmbMonthMinCurr.DisplayMember = "Title";
            cmbMonthMinCurr.ValueMember = "ID";
            cmbMonthMinCurr.SelectedValue = 0;

            cmbMinCurr.DataSource = Global.dtCurrencies.Copy();
            cmbMinCurr.DisplayMember = "Title";
            cmbMinCurr.ValueMember = "ID";
            cmbMinCurr.SelectedValue = 0;

            cmbOpenCurr.DataSource = Global.dtCurrencies.Copy();
            cmbOpenCurr.DisplayMember = "Title";
            cmbOpenCurr.ValueMember = "ID";
            cmbOpenCurr.SelectedValue = 0;

            cmbServiceCurr.DataSource = Global.dtCurrencies.Copy();
            cmbServiceCurr.DisplayMember = "Title";
            cmbServiceCurr.ValueMember = "ID";
            cmbServiceCurr.SelectedValue = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.Rows.Count = 1;


            //------- fgBrokerageOptions ----------------------------
            fgBrokerageOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBrokerageOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgBrokerageOptions.RowColChange += new EventHandler(fgBrokerageOptions_RowColChange);

            //------- fgBrokerageFees ----------------------------
            fgBrokerageFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBrokerageFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgBrokerageFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgBrokerageFees.ShowCellLabels = true;

            fgBrokerageFees.Styles.Normal.WordWrap = true;
            fgBrokerageFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgBrokerageFees.Rows[0].AllowMerging = true;
            fgBrokerageFees.Rows[1].AllowMerging = true;

            fgBrokerageFees.Cols[0].AllowMerging = true;
            fgBrokerageFees.Cols[1].AllowMerging = true;
            fgBrokerageFees.Cols[2].AllowMerging = true;

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
            rng.Data = "Προμήθεια";

            fgBrokerageFees[1, 5] = "Αγοράς";
            fgBrokerageFees[1, 6] = "Πώλησης";

            rng = fgBrokerageFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgBrokerageFees[1, 7] = "Αγοράς";
            fgBrokerageFees[1, 8] = "Πώλησης";
            fgBrokerageFees[1, 9] = "Νόμισμα";

            rng = fgBrokerageFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgBrokerageFees[1, 10] = "Ποσό";
            fgBrokerageFees[1, 11] = "Νόμισμα";

            rng = fgBrokerageFees.GetCellRange(0, 12, 0, 14);
            rng.Data = "Επιστροφές";

            fgBrokerageFees[1, 12] = "Τρόπος";
            fgBrokerageFees[1, 13] = "Πάροχος";
            fgBrokerageFees[1, 14] = Global.CompanyName;


            fgBrokerageFees.Cols[15].AllowMerging = true;
            rng = fgBrokerageFees.GetCellRange(0, 15, 1, 15);
            rng.Data = "Settlement Provider";

            //------- fgRTOOptions ----------------------------
            fgRTOOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRTOOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgRTOOptions.RowColChange += new EventHandler(fgRTOOptions_RowColChange);

            //------- fgRTOFees ----------------------------
            fgRTOFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRTOFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgRTOFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgRTOFees.ShowCellLabels = true;

            fgRTOFees.Styles.Normal.WordWrap = true;
            fgRTOFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgRTOFees.Rows[0].AllowMerging = true;
            fgRTOFees.Rows[1].AllowMerging = true;

            fgRTOFees.Cols[0].AllowMerging = true;
            fgRTOFees.Cols[1].AllowMerging = true;
            fgRTOFees.Cols[2].AllowMerging = true;

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
            rng.Data = "Προμήθεια";

            fgRTOFees[1, 5] = "Αγοράς";
            fgRTOFees[1, 6] = "Πώλησης";

            rng = fgRTOFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgRTOFees[1, 7] = "Αγοράς";
            fgRTOFees[1, 8] = "Πώλησης";
            fgRTOFees[1, 9] = "Νόμισμα";

            rng = fgRTOFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgRTOFees[1, 10] = "Ποσό";
            fgRTOFees[1, 11] = "Νόμισμα";

            rng = fgRTOFees.GetCellRange(0, 12, 0, 14);
            rng.Data = "Επιστροφές";

            fgRTOFees[1, 12] = "Τρόπος";
            fgRTOFees[1, 13] = "Πάροχος";
            fgRTOFees[1, 14] = Global.CompanyName;


            fgRTOFees.Cols[15].AllowMerging = true;
            rng = fgRTOFees.GetCellRange(0, 15, 1, 15);
            rng.Data = "Settlement Provider";

            //------- fgAdvisoryOptions ----------------------------
            fgAdvisoryOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdvisoryOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgAdvisoryOptions.RowColChange += new EventHandler(fgAdvisoryOptions_RowColChange);

            fgAdvisoryOptions.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdvisoryOptions.ShowCellLabels = true;

            fgAdvisoryOptions.Styles.Normal.WordWrap = true;
            fgAdvisoryOptions.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdvisoryOptions.Rows[0].AllowMerging = true;

            fgAdvisoryOptions.Cols[0].AllowMerging = true;
            rng = fgAdvisoryOptions.GetCellRange(0, 0, 1, 0);
            rng.Data = "Τίτλος";

            fgAdvisoryOptions.Cols[1].AllowMerging = true;
            rng = fgAdvisoryOptions.GetCellRange(0, 1, 1, 1);
            rng.Data = "Έναρξη";

            fgAdvisoryOptions.Cols[2].AllowMerging = true;
            rng = fgAdvisoryOptions.GetCellRange(0, 2, 1, 2);
            rng.Data = "Λήξη";

            rng = fgAdvisoryOptions.GetCellRange(0, 3, 0, 4);
            rng.Data = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
            fgAdvisoryOptions[1, 3] = "ποσό";
            fgAdvisoryOptions[1, 4] = "νόμισμα";

            rng = fgAdvisoryOptions.GetCellRange(0, 5, 0, 6);
            rng.Data = "Ελάχιστο ποσό χρέωσης";
            fgAdvisoryOptions[1, 5] = "ποσό";
            fgAdvisoryOptions[1, 6] = "νόμισμα";

            rng = fgAdvisoryOptions.GetCellRange(0, 7, 0, 8);
            rng.Data = "Έξοδα Ανοίγματος Λογαριασμού";
            fgAdvisoryOptions[1, 7] = "ποσό";
            fgAdvisoryOptions[1, 8] = "νόμισμα";

            rng = fgAdvisoryOptions.GetCellRange(0, 9, 0, 10);
            rng.Data = "Έξοδα Διατήρησης Λογαριασμού";
            fgAdvisoryOptions[1, 9] = "ποσό";
            fgAdvisoryOptions[1, 10] = "νόμισμα";

            //------- fgAdvisoryFees ----------------------------
            fgAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdvisoryFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdvisoryFees.ShowCellLabels = true;

            fgAdvisoryFees.Styles.Normal.WordWrap = true;
            fgAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdvisoryFees.Rows[0].AllowMerging = true;

            fgAdvisoryFees.Cols[0].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Επενδυτικό Προφίλ";

            fgAdvisoryFees.Cols[1].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Επενδυτικη Πολιτική";

            rng = fgAdvisoryFees.GetCellRange(0, 2, 0, 3);
            rng.Data = "Κλίμακα";

            fgAdvisoryFees[1, 2] = "από";
            fgAdvisoryFees[1, 3] = "εώς";

            fgAdvisoryFees.Cols[4].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 4, 1, 4);
            rng.Data = "Αμοιβή";

            fgAdvisoryFees.Cols[5].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 5, 1, 5);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgAdvisoryFees.GetCellRange(0, 6, 0, 7);
            rng.Data = "Μεταβλητές";

            fgAdvisoryFees[1, 6] = "Κείμενο";
            fgAdvisoryFees[1, 7] = "%";

            //------- fgDiscretOptions ----------------------------
            fgDiscretOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDiscretOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgDiscretOptions.RowColChange += new EventHandler(fgDiscretOptions_RowColChange);

            fgDiscretOptions.DrawMode = DrawModeEnum.OwnerDraw;
            fgDiscretOptions.ShowCellLabels = true;

            fgDiscretOptions.Styles.Normal.WordWrap = true;
            fgDiscretOptions.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDiscretOptions.Rows[0].AllowMerging = true;

            fgDiscretOptions.Cols[0].AllowMerging = true;
            rng = fgDiscretOptions.GetCellRange(0, 0, 1, 0);
            rng.Data = "Τίτλος";

            fgDiscretOptions.Cols[1].AllowMerging = true;
            rng = fgDiscretOptions.GetCellRange(0, 1, 1, 1);
            rng.Data = "Έναρξη";

            fgDiscretOptions.Cols[2].AllowMerging = true;
            rng = fgDiscretOptions.GetCellRange(0, 2, 1, 2);
            rng.Data = "Λήξη";

            rng = fgDiscretOptions.GetCellRange(0, 3, 0, 4);
            rng.Data = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
            fgDiscretOptions[1, 3] = "ποσό";
            fgDiscretOptions[1, 4] = "νόμισμα";

            rng = fgDiscretOptions.GetCellRange(0, 5, 0, 6);
            rng.Data = "Ελάχιστο ποσό χρέωσης";
            fgDiscretOptions[1, 5] = "ποσό";
            fgDiscretOptions[1, 6] = "νόμισμα";

            rng = fgDiscretOptions.GetCellRange(0, 7, 0, 8);
            rng.Data = "Έξοδα Ανοίγματος Λογαριασμού";
            fgDiscretOptions[1, 7] = "ποσό";
            fgDiscretOptions[1, 8] = "νόμισμα";

            rng = fgDiscretOptions.GetCellRange(0, 9, 0, 10);
            rng.Data = "Έξοδα Διατήρησης Λογαριασμού";
            fgDiscretOptions[1, 9] = "ποσό";
            fgDiscretOptions[1, 10] = "νόμισμα";

            //------- fgDiscretFees ----------------------------
            fgDiscretFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDiscretFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgDiscretFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDiscretFees.ShowCellLabels = true;

            fgDiscretFees.Styles.Normal.WordWrap = true;
            fgDiscretFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDiscretFees.Rows[0].AllowMerging = true;

            fgDiscretFees.Cols[0].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Επενδυτικό Προφίλ";

            fgDiscretFees.Cols[1].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Επενδυτικη Πολιτική";

            rng = fgDiscretFees.GetCellRange(0, 2, 0, 3);
            rng.Data = "Κλίμακα";

            fgDiscretFees[1, 2] = "από";
            fgDiscretFees[1, 3] = "εώς";

            fgDiscretFees.Cols[4].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 4, 1, 4);
            rng.Data = "Αμοιβή";

            fgDiscretFees.Cols[5].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 5, 1, 5);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgDiscretFees.GetCellRange(0, 6, 0, 7);
            rng.Data = "Μεταβλητές";

            fgDiscretFees[1, 6] = "Κείμενο";
            fgDiscretFees[1, 7] = "%";


            //------- fgSafekeepingOptions ----------------------------
            fgSafekeepingOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSafekeepingOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgSafekeepingOptions.RowColChange += new EventHandler(fgSafekeepingOptions_RowColChange);

            fgSafekeepingOptions.DrawMode = DrawModeEnum.OwnerDraw;
            fgSafekeepingOptions.ShowCellLabels = true;

            fgSafekeepingOptions.Styles.Normal.WordWrap = true;
            fgSafekeepingOptions.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSafekeepingOptions.Rows[0].AllowMerging = true;

            fgSafekeepingOptions.Cols[0].AllowMerging = true;
            rng = fgSafekeepingOptions.GetCellRange(0, 0, 1, 0);
            rng.Data = "Τίτλος";

            fgSafekeepingOptions.Cols[1].AllowMerging = true;
            rng = fgSafekeepingOptions.GetCellRange(0, 1, 1, 1);
            rng.Data = "Έναρξη";

            fgSafekeepingOptions.Cols[2].AllowMerging = true;
            rng = fgSafekeepingOptions.GetCellRange(0, 2, 1, 2);
            rng.Data = "Λήξη";

            rng = fgSafekeepingOptions.GetCellRange(0, 3, 0, 4);
            rng.Data = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
            fgSafekeepingOptions[1, 3] = "ποσό";
            fgSafekeepingOptions[1, 4] = "νόμισμα";

            rng = fgSafekeepingOptions.GetCellRange(0, 5, 0, 6);
            rng.Data = "Ελάχιστο ποσό χρέωσης";
            fgSafekeepingOptions[1, 5] = "ποσό";
            fgSafekeepingOptions[1, 6] = "νόμισμα";

            rng = fgSafekeepingOptions.GetCellRange(0, 7, 0, 8);
            rng.Data = "Έξοδα Ανοίγματος Λογαριασμού";
            fgSafekeepingOptions[1, 7] = "ποσό";
            fgSafekeepingOptions[1, 8] = "νόμισμα";

            rng = fgSafekeepingOptions.GetCellRange(0, 9, 0, 10);
            rng.Data = "Έξοδα Διατήρησης Λογαριασμού";
            fgSafekeepingOptions[1, 9] = "ποσό";
            fgSafekeepingOptions[1, 10] = "νόμισμα";

            rng = fgSafekeepingOptions.GetCellRange(0, 11, 0, 13);
            rng.Data = "-";
            fgSafekeepingOptions[1, 11] = "AUM";
            fgSafekeepingOptions[1, 12] = "Securities";
            fgSafekeepingOptions[1, 13] = "Cash";

            //------- fgSafekeepingFees ----------------------------
            fgSafekeepingFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSafekeepingFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgSafekeepingFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgSafekeepingFees.ShowCellLabels = true;

            fgSafekeepingFees.Styles.Normal.WordWrap = true;
            fgSafekeepingFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSafekeepingFees.Rows[0].AllowMerging = true;

            rng = fgSafekeepingFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgSafekeepingFees[1, 0] = "από";
            fgSafekeepingFees[1, 1] = "εώς";

            fgSafekeepingFees.Cols[2].AllowMerging = true;
            rng = fgSafekeepingFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgSafekeepingFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Επιστροφές";

            fgSafekeepingFees[1, 3] = "Τρόπος";
            fgSafekeepingFees[1, 4] = "Πάροχος";
            fgSafekeepingFees[1, 5] = Global.CompanyName;


            //------- fgAdministrationOptions ----------------------------
            fgAdministrationOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdministrationOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgAdministrationOptions.RowColChange += new EventHandler(fgAdministrationOptions_RowColChange);

            fgAdministrationOptions.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdministrationOptions.ShowCellLabels = true;

            fgAdministrationOptions.Styles.Normal.WordWrap = true;
            fgAdministrationOptions.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdministrationOptions.Rows[0].AllowMerging = true;

            fgAdministrationOptions.Cols[0].AllowMerging = true;
            rng = fgAdministrationOptions.GetCellRange(0, 0, 1, 0);
            rng.Data = "Τίτλος";

            fgAdministrationOptions.Cols[1].AllowMerging = true;
            rng = fgAdministrationOptions.GetCellRange(0, 1, 1, 1);
            rng.Data = "Έναρξη";

            fgAdministrationOptions.Cols[2].AllowMerging = true;
            rng = fgAdministrationOptions.GetCellRange(0, 2, 1, 2);
            rng.Data = "Λήξη";

            rng = fgAdministrationOptions.GetCellRange(0, 3, 0, 4);
            rng.Data = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
            fgAdministrationOptions[1, 3] = "ποσό";
            fgAdministrationOptions[1, 4] = "νόμισμα";

            rng = fgAdministrationOptions.GetCellRange(0, 5, 0, 6);
            rng.Data = "Ελάχιστο ποσό χρέωσης";
            fgAdministrationOptions[1, 5] = "ποσό";
            fgAdministrationOptions[1, 6] = "νόμισμα";

            rng = fgAdministrationOptions.GetCellRange(0, 7, 0, 8);
            rng.Data = "Έξοδα Ανοίγματος Λογαριασμού";
            fgAdministrationOptions[1, 7] = "ποσό";
            fgAdministrationOptions[1, 8] = "νόμισμα";

            rng = fgAdministrationOptions.GetCellRange(0, 9, 0, 10);
            rng.Data = "Έξοδα Διατήρησης Λογαριασμού";
            fgAdministrationOptions[1, 9] = "ποσό";
            fgAdministrationOptions[1, 10] = "νόμισμα";

            rng = fgAdministrationOptions.GetCellRange(0, 11, 0, 13);
            rng.Data = "-";
            fgAdministrationOptions[1, 11] = "AUM";
            fgAdministrationOptions[1, 12] = "Securities";
            fgAdministrationOptions[1, 13] = "Cash";

            //------- fgAdministrationFees ----------------------------
            fgAdministrationFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAdministrationFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgAdministrationFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdministrationFees.ShowCellLabels = true;

            fgAdministrationFees.Styles.Normal.WordWrap = true;
            fgAdministrationFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdministrationFees.Rows[0].AllowMerging = true;

            rng = fgAdministrationFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgAdministrationFees[1, 0] = "από";
            fgAdministrationFees[1, 1] = "εώς";

            fgAdministrationFees.Cols[2].AllowMerging = true;
            rng = fgAdministrationFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgAdministrationFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Επιστροφές";

            fgAdministrationFees[1, 3] = "Τρόπος";
            fgAdministrationFees[1, 4] = "Πάροχος";
            fgAdministrationFees[1, 5] = Global.CompanyName;

            //------- fgDealAdvisoryOptions ----------------------------
            fgDealAdvisoryOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDealAdvisoryOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgDealAdvisoryOptions.RowColChange += new EventHandler(fgDealAdvisoryOptions_RowColChange);

            fgDealAdvisoryOptions.DrawMode = DrawModeEnum.OwnerDraw;
            fgDealAdvisoryOptions.ShowCellLabels = true;

            fgDealAdvisoryOptions.Styles.Normal.WordWrap = true;
            fgDealAdvisoryOptions.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDealAdvisoryOptions.Rows[0].AllowMerging = true;

            fgDealAdvisoryOptions.Cols[0].AllowMerging = true;
            rng = fgDealAdvisoryOptions.GetCellRange(0, 0, 1, 0);
            rng.Data = "Τίτλος";

            fgDealAdvisoryOptions.Cols[1].AllowMerging = true;
            rng = fgDealAdvisoryOptions.GetCellRange(0, 1, 1, 1);
            rng.Data = "Έναρξη";

            fgDealAdvisoryOptions.Cols[2].AllowMerging = true;
            rng = fgDealAdvisoryOptions.GetCellRange(0, 2, 1, 2);
            rng.Data = "Λήξη";

            rng = fgDealAdvisoryOptions.GetCellRange(0, 3, 0, 4);
            rng.Data = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
            fgDealAdvisoryOptions[1, 3] = "ποσό";
            fgDealAdvisoryOptions[1, 4] = "νόμισμα";

            rng = fgDealAdvisoryOptions.GetCellRange(0, 5, 0, 6);
            rng.Data = "Ελάχιστο ποσό χρέωσης";
            fgDealAdvisoryOptions[1, 5] = "ποσό";
            fgDealAdvisoryOptions[1, 6] = "νόμισμα";

            rng = fgDealAdvisoryOptions.GetCellRange(0, 7, 0, 8);
            rng.Data = "Έξοδα Ανοίγματος Λογαριασμού";
            fgDealAdvisoryOptions[1, 7] = "ποσό";
            fgDealAdvisoryOptions[1, 8] = "νόμισμα";

            rng = fgDealAdvisoryOptions.GetCellRange(0, 9, 0, 10);
            rng.Data = "Έξοδα Διατήρησης Λογαριασμού";
            fgDealAdvisoryOptions[1, 9] = "ποσό";
            fgDealAdvisoryOptions[1, 10] = "νόμισμα";

            //------- fgDealAdvisoryFees ----------------------------
            fgDealAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDealAdvisoryFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgDealAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDealAdvisoryFees.ShowCellLabels = true;

            fgDealAdvisoryFees.Styles.Normal.WordWrap = true;
            fgDealAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDealAdvisoryFees.Rows[0].AllowMerging = true;

            fgDealAdvisoryFees.Cols[0].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "Χρηματοπιστωτικά Μέσα";

            rng = fgDealAdvisoryFees.GetCellRange(0, 1, 0, 2);
            rng.Data = "Κλίμακα";

            fgDealAdvisoryFees[1, 1] = "από";
            fgDealAdvisoryFees[1, 2] = "εώς";

            fgDealAdvisoryFees.Cols[3].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 3, 1, 3);
            rng.Data = "Αμοιβή";

            fgDealAdvisoryFees.Cols[4].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 4, 1, 4);
            rng.Data = "Νόμισμα";

            fgDealAdvisoryFees.Cols[5].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 5, 1, 5);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgDealAdvisoryFees.GetCellRange(0, 6, 0, 7);
            rng.Data = "Μεταβλητές";

            fgDealAdvisoryFees[1, 6] = "Κείμενο";
            fgDealAdvisoryFees[1, 7] = "%";

            //------- fgLombardOptions ----------------------------
            fgLombardOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgLombardOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgLombardOptions.RowColChange += new EventHandler(fgLombardOptions_RowColChange);
            
            //------- fgLombardFees ----------------------------
            fgLombardFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgLombardFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgLombardFees.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgLombardFees_CellChanged);

            lstCurr.Clear();
            foreach (DataRow row in Global.dtCurrencies.Rows)
            {
                lstCurr.Add(row["Title"], row["Title"]);
            }
            fgLombardFees.Cols[0].DataMap = lstCurr;

            //------- fgFXOptions ----------------------------
            fgFXOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFXOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgFXOptions.RowColChange += new EventHandler(fgFXOptions_RowColChange);

            //------- fgFXFees ----------------------------
            fgFXFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFXFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgFXFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgFXFees.ShowCellLabels = true;

            fgFXFees.Styles.Normal.WordWrap = true;
            fgFXFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgFXFees.Rows[0].AllowMerging = true;

            rng = fgFXFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgFXFees[1, 0] = "από";
            fgFXFees[1, 1] = "εώς";

            fgFXFees.Cols[2].AllowMerging = true;
            rng = fgFXFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgFXFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Επιστροφές";

            fgFXFees[1, 3] = "Τρόπος";
            fgFXFees[1, 4] = "Πάροχος";
            fgFXFees[1, 5] = Global.CompanyName;

            //------- fgSettlementsOptions ----------------------------
            fgSettlementsOptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSettlementsOptions.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgSettlementsOptions.RowColChange += new EventHandler(fgSettlementsOptions_RowColChange);

            //------- fgSettlementsFees ----------------------------
            fgSettlementsFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSettlementsFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgSettlementsFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgSettlementsFees.ShowCellLabels = true;

            fgSettlementsFees.Styles.Normal.WordWrap = true;
            fgSettlementsFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSettlementsFees.Rows[0].AllowMerging = true;
            fgSettlementsFees.Rows[1].AllowMerging = true;

            fgSettlementsFees.Cols[0].AllowMerging = true;
            fgSettlementsFees.Cols[1].AllowMerging = true;
            fgSettlementsFees.Cols[2].AllowMerging = true;

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
            rng.Data = "Προμήθεια";

            fgSettlementsFees[1, 5] = "Αγοράς";
            fgSettlementsFees[1, 6] = "Πώλησης";

            rng = fgSettlementsFees.GetCellRange(0, 7, 0, 9);
            rng.Data = "Ticket Fees";

            fgSettlementsFees[1, 7] = "Αγοράς";
            fgSettlementsFees[1, 8] = "Πώλησης";
            fgSettlementsFees[1, 9] = "Νόμισμα";

            rng = fgSettlementsFees.GetCellRange(0, 10, 0, 11);
            rng.Data = "Minimum Fees";

            fgSettlementsFees[1, 10] = "Ποσό";
            fgSettlementsFees[1, 11] = "Νόμισμα";

            rng = fgSettlementsFees.GetCellRange(0, 12, 0, 14);
            rng.Data = "Επιστροφές";

            fgSettlementsFees[1, 12] = "Τρόπος";
            fgSettlementsFees[1, 13] = "Πάροχος";
            fgSettlementsFees[1, 14] = Global.CompanyName;

            //------- fgPackages ----------------------------
            fgPackages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPackages.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgPackages.RowColChange += new EventHandler(fgPackages_RowColChange);

            //-------------- Define Currencies List ------------------
            cmbCurrencies.DataSource = Global.dtCurrencies.Copy();
            cmbCurrencies.DisplayMember = "Title";
            cmbCurrencies.ValueMember = "ID";


            tscbProviderTypes.SelectedIndex = 0;
            bCheckList = true;

            if (iRightsLevel == 1)
            {
                tsbAdd.Enabled = false;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                tsbSave.Enabled = false;
                toolBrokerageFees.Enabled = false;
                toolPackages.Enabled = false;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 86;

            tcContracts.Width = this.Width - 268;
            tcContracts.Height = this.Height - 86;
            fgPackages.Height = tcContracts.Height - ucCC.Height - 20;
            ucCC.Top = tcContracts.Height - ucCC.Height + 48;

            fgBrokerageFees.Width = tcContracts.Width - 32;
            fgBrokerageFees.Height = tcContracts.Height - 340;

            fgRTOFees.Width = tcContracts.Width - 32;
            fgRTOFees.Height = tcContracts.Height - 340;

            fgFXFees.Height = tcContracts.Height - 340;
            fgSafekeepingFees.Height = tcContracts.Height - 340;
            fgAdministrationFees.Height = tcContracts.Height - 340;

            fgSettlementsFees.Width = tcContracts.Width - 32;
            fgSettlementsFees.Height = tcContracts.Height - 340;

            fgAdvisoryFees.Height = tcContracts.Height - 340;
            fgDiscretFees.Height = tcContracts.Height - 340;
            fgDealAdvisoryFees.Height = tcContracts.Height - 340;
            fgPerformFees.Height = tcContracts.Height - 340;
            fgLombardFees.Height = tcContracts.Height - 340;

            panPackage.Left = (Screen.PrimaryScreen.Bounds.Width - panPackage.Width) / 2;
            panPackage.Top = (Screen.PrimaryScreen.Bounds.Height - panPackage.Height) / 2;

            panOption.Left = (Screen.PrimaryScreen.Bounds.Width - panOption.Width) / 2;
            panOption.Top = (Screen.PrimaryScreen.Bounds.Height - panOption.Height) / 2;
        }
        #endregion ----------------------------------------------------------------------------------
        #region --- Toolbar functions --------------------------------------------------------------------------------------
        private void tscbProviderTypes_Click(object sender, EventArgs e)
        {
            i = 0;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            EmptyDetails();
            iAction = 0;                      // 0 - ADD Mode
            ChangeMode(2);
            bCheckList = true;
            txtTitle.Focus();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;                        // 1 - EDIT Mode
            ChangeMode(2);
            bCheckList = true;
            txtTitle.Focus();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {

            bListChanged = true;

            if (txtTitle.Text.Length != 0)
            {
                sTemp = "";

                ServiceProviders = new clsServiceProviders();
                if (iAction == 1)
                {                               // 0 - ADD Mode, 1 - EDIT mode
                    ServiceProviders.Record_ID = iID;
                    ServiceProviders.GetRecord();
                }
                ServiceProviders.ProviderType = tscbProviderTypes.SelectedIndex + 1;
                ServiceProviders.Title = txtTitle.Text;
                ServiceProviders.Alias = txtAlias.Text;
                ServiceProviders.Seira = txtSeira.Text;
                ServiceProviders.VAT_FP = Convert.ToSingle(txtVAT_FP.Text);
                ServiceProviders.VAT_NP = Convert.ToSingle(txtVAT_NP.Text);
                ServiceProviders.MainCurr = cmbCurrencies.Text;
                ServiceProviders.Informing_Statement = cmbStatement_File.SelectedIndex;
                ServiceProviders.Informing_Misc = cmbMisc_File.SelectedIndex;
                ServiceProviders.Informing_ConvertFile = chkConvert_File.Checked ? 1 : 0;
                ServiceProviders.SendOrders = cmbSendOrders.SelectedIndex;
                ServiceProviders.FeesMode = cmbFeesMode.SelectedIndex;
                ServiceProviders.EffectCode = txtEffectCode.Text;
                ServiceProviders.LEI = txtLEI.Text;
                ServiceProviders.FIX_DB = txtFIX_DB.Text;
                ServiceProviders.HFAccount_Own = txtHFAccount_Own.Text;
                ServiceProviders.HFAccount_Clients = txtHFAccount_Clients.Text;
                ServiceProviders.BestExecution = Convert.ToInt32(cmbBestExecution.SelectedIndex);
                ServiceProviders.PriceTable = txtPriceTable.Text;
                ServiceProviders.DepositoryTitle = txtDepositoryTitle.Text;
                ServiceProviders.Aktive = chkAktive.Checked ? 1 : 0;

                if (iAction == 1)                                                                 // 0 - ADD Mode, 1 - EDIT mode
                    ServiceProviders.EditRecord();
                else iID = ServiceProviders.InsertRecord();

                //-------- SAVE or DELETE ServiceProviderOptions records -------------
                foreach (DataRow dtRow in dtOptions.Rows)
                {
                    switch (Convert.ToInt32(dtRow["Status"]))
                    {
                        case 0:                                                                   // 0 - not edited
                            // nothing must to do
                            break;
                        case 3:                                                                   // 3 - deleted

                            ServiceProvidersOptions = new clsServiceProvidersOptions();
                            ServiceProvidersOptions.Record_ID = Convert.ToInt32(dtRow["ID"]);
                            ServiceProvidersOptions.DeleteRecord();
                            break;
                        default:                                                                 // 1- add, 2 - edit
                            ServiceProvidersOptions = new clsServiceProvidersOptions();
                            if (Convert.ToInt32(dtRow["Status"]) == 2)
                            {
                                ServiceProvidersOptions.Record_ID = Convert.ToInt32(dtRow["ID"]);
                                ServiceProvidersOptions.GetRecord();
                            }

                            ServiceProvidersOptions.ServiceProvider_ID = iID;
                            ServiceProvidersOptions.ServiceType_ID = Convert.ToInt32(dtRow["ServiceType_ID"]);
                            ServiceProvidersOptions.Title = dtRow["Title"] + "";
                            ServiceProvidersOptions.DateStart = Convert.ToDateTime(dtRow["DateStart"]);
                            ServiceProvidersOptions.DateFinish = Convert.ToDateTime(dtRow["DateFinish"]);
                            ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(dtRow["MonthMinAmount"]);
                            ServiceProvidersOptions.MonthMinCurr = dtRow["MonthMinCurr"] + "";
                            ServiceProvidersOptions.OpenAmount = Convert.ToSingle(dtRow["OpenAmount"]);
                            ServiceProvidersOptions.OpenCurr = dtRow["OpenCurr"] + "";
                            ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(dtRow["ServiceAmount"]);
                            ServiceProvidersOptions.ServiceCurr = dtRow["ServiceCurr"] + "";
                            ServiceProvidersOptions.MinAmount = Convert.ToSingle(dtRow["MinAmount"]);
                            ServiceProvidersOptions.MinCurr = dtRow["MinCurr"] + "";
                            ServiceProvidersOptions.CalcAUM = Convert.ToInt16(dtRow["CalcAUM"]);
                            ServiceProvidersOptions.CalcSecurities = Convert.ToInt16(dtRow["CalcSecurities"]);
                            ServiceProvidersOptions.CalcCash = Convert.ToInt16(dtRow["CalcCash"]);

                            if (Convert.ToInt32(dtRow["Status"]) == 2) iOption_ID = ServiceProvidersOptions.EditRecord();
                            iOption_ID = ServiceProvidersOptions.InsertRecord();
                            break;
                    }
                }

                bCheckList = false;

                if (iAction == 0) {                       // 0 - ADD Mode
                    Global.GetServiceProvidersList();     // Cash ServiceProviders List
                    DefineList();

                    i = fgList.FindRow(sTemp, 1, 1, false);
                    bCheckList = true;
                    if (i > 0) fgList.Row = i;
                }
                else bCheckList = true;

                iAction = 1;
                ChangeMode(1);
            }
            else MessageBox.Show("Η εισαγωγή του τίτλου είναι υποχρεωτική", "Λίστα Μετωχών", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            ShowRecord();            
            fgList.Focus();
        }
        private void ChangeMode(int iStatus)
        {
            switch (iStatus) {
                case 1:
                    toolLeft.Enabled = true;
                    fgList.Enabled = true;

                    toolRight.Enabled = false;
                    toolBrokerageOptions.Enabled = false;
                    toolBrokerageFees.Enabled = false;
                    toolRTOOptions.Enabled = false;
                    toolRTOFees.Enabled = false;
                    toolAdvisoryOptions.Enabled = false;
                    toolAdvisoryFees.Enabled = false;
                    toolDealAdvisoryOptions.Enabled = false;
                    toolDealAdvisoryFees.Enabled = false;
                    toolDiscretOptions.Enabled = false;
                    toolDiscretFees.Enabled = false;
                    toolSafekeepingOptions.Enabled = false;
                    toolSafekeepingFees.Enabled = false;
                    toolAdministrationOptions.Enabled = false;
                    toolAdministrationFees.Enabled = false;
                    toolLombardOptions.Enabled = false;
                    toolLombardFees.Enabled = false;
                    toolFXOptions.Enabled = false;
                    toolFXFees.Enabled = false;
                    toolSettlementsOptions.Enabled = false;
                    toolSettlementsFees.Enabled = false;
                    toolPackages.Enabled = false;
                    break;
                case 2:
                    //toolLeft.Enabled = false;
                    fgList.Enabled = false;

                    toolRight.Enabled = true;
                    toolBrokerageOptions.Enabled = true;
                    toolBrokerageFees.Enabled = true;
                    toolRTOOptions.Enabled = true;
                    toolRTOFees.Enabled = true;
                    toolAdvisoryOptions.Enabled = true;
                    toolAdvisoryFees.Enabled = true;
                    toolDealAdvisoryOptions.Enabled = true;
                    toolDealAdvisoryFees.Enabled = true;
                    toolDiscretOptions.Enabled = true;
                    toolDiscretFees.Enabled = true;
                    toolSafekeepingOptions.Enabled = true;
                    toolSafekeepingFees.Enabled = true;
                    toolAdministrationOptions.Enabled = true;
                    toolAdministrationFees.Enabled = true;
                    toolLombardOptions.Enabled = true;
                    toolLombardFees.Enabled = true;
                    toolFXOptions.Enabled = true;
                    toolFXFees.Enabled = true;
                    toolSettlementsOptions.Enabled = true;
                    toolSettlementsFees.Enabled = true;
                    toolPackages.Enabled = true;
                    break;
            }
        }
        #endregion
        #region --- Header functions ---------------------------------------------------------------------------------------
        private void tscbProviderTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            bCheckList = false;
            DefineList();
            bCheckList = true;
        }
        private void DefineList()
        {

            fgList.Redraw = false;
            fgList.Rows.Count = 1;
            foreach (DataRow dtRow in Global.dtServiceProviders.Copy().Rows)
            {
                if (Convert.ToInt32(dtRow["ID"]) != 0 && Convert.ToInt32(dtRow["ProviderType"]) == (tscbProviderTypes.SelectedIndex + 1))
                    fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"]);
            }
            fgList.Redraw = true;

            if (fgList.Rows.Count > 1)
            {
                iAction = 1;
                fgList.Focus();
                ShowRecord();
            }
        }

        private void tcContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(tcContracts.SelectedIndex))
            {
                case 0:                                                               // "tpRTO":

                    break;
                case 1:                                                              // "tpDPM":

                    break;
                case 2:                                                              //   "tpBulk":

                    break;
                case 15:                                                             //  "tpClientData":
                    CompanyPackage = new clsCompanyPackages();
                    CompanyPackage.Provider_ID = 0;
                    CompanyPackage.PackageType_ID = 0;
                    CompanyPackage.BusinessType_ID = 0;
                    CompanyPackage.CheckActuality = 0;
                    CompanyPackage.ActualDate = DateTime.Now;
                    CompanyPackage.Title = "";
                    CompanyPackage.GetList();
                    dtPackages = CompanyPackage.List.Copy();

                    //----- initialize Company Packages List -------
                    dtView = dtPackages.Copy().DefaultView;
                    cmbCompanyPackages.DataSource = dtView;
                    cmbCompanyPackages.DisplayMember = "TitleFull";
                    cmbCompanyPackages.ValueMember = "ID";
                    cmbCompanyPackages.SelectedValue = 0;

                    //-------------- Define FinanceServices List ------------------
                    cmbFinanceServices.DataSource = Global.dtServices.Copy();
                    cmbFinanceServices.DisplayMember = "Title";
                    cmbFinanceServices.ValueMember = "ID";

                    //-------------- Define Clients Profiles List ------------------    
                    cmbProfile.DataSource = Global.dtCustomersProfiles.Copy();
                    cmbProfile.DisplayMember = "Title";
                    cmbProfile.ValueMember = "ID";

                    //-------------- Define Investment Policies List ------------------    
                    cmbInvestmentPolicy.DataSource = Global.dtInvestPolicies.Copy();
                    cmbInvestmentPolicy.DisplayMember = "Title";
                    cmbInvestmentPolicy.ValueMember = "ID";

                    //-------------- Define NOMISMA ANAFORAS List ------------------
                    cmbPackageCurrency.DataSource = Global.dtCurrencies.Copy();
                    cmbPackageCurrency.DisplayMember = "Title";
                    cmbPackageCurrency.ValueMember = "ID";

                    bCheckList = true;
                    break;
            }
        }

        #endregion
        #region --- fgList functionality -----------------------------------------------------------------------------------
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            if (bCheckList)
                if (fgList.Row > 0) ShowRecord();
        }
        #endregion
        #region --- ShowRecord ---------------------------------------------------------------------------------------------
        private void ShowRecord()
        {
            iID = Convert.ToInt32(fgList[fgList.Row, 1]);
            txtTitle.Text = fgList[fgList.Row, 0] + "";

            ServiceProviders = new clsServiceProviders();
            ServiceProviders.Record_ID = iID;
            ServiceProviders.GetRecord();

            txtAlias.Text = ServiceProviders.Alias;
            txtSeira.Text = ServiceProviders.Seira;
            txtVAT_FP.Text = ServiceProviders.VAT_FP.ToString();
            txtVAT_NP.Text = ServiceProviders.VAT_NP.ToString();
            cmbCurrencies.Text = ServiceProviders.MainCurr;
            cmbStatement_File.SelectedIndex = ServiceProviders.Informing_Statement;
            cmbMisc_File.SelectedIndex = ServiceProviders.Informing_Misc;
            chkConvert_File.Checked = (ServiceProviders.Informing_ConvertFile == 1 ? true : false);
            cmbSendOrders.SelectedIndex = ServiceProviders.SendOrders;
            cmbFeesMode.SelectedIndex = ServiceProviders.FeesMode;
            txtPriceTable.Text = ServiceProviders.PriceTable;
            txtEffectCode.Text = ServiceProviders.EffectCode;
            txtLEI.Text = ServiceProviders.LEI;
            txtFIX_DB.Text = ServiceProviders.FIX_DB;
            cmbBestExecution.SelectedIndex = ServiceProviders.BestExecution;
            txtHFAccount_Own.Text = ServiceProviders.HFAccount_Own;
            txtHFAccount_Clients.Text = ServiceProviders.HFAccount_Clients;
            chkAktive.Checked = (ServiceProviders.Aktive == 1 ? true : false);
            txtDepositoryTitle.Text = ServiceProviders.DepositoryTitle;

            bCheckDiscretFees = false;
            bCheckRTOFees = false;
            bCheckAdvisoryFees = false;
            bCheckDealAdvisoryFees = false;
            bCheckDiscretFees = false;
            bCheckSafekeepingFees = false;
            bCheckAdministrationFees = false;
            bCheckLombardFees = false;
            bCheckFXFees = false;
            bCheckSettlementsFees = false;

            fgBrokerageOptions.Rows.Count = 1;
            fgBrokerageFees.Rows.Count = 2;
            fgRTOOptions.Rows.Count = 1;
            fgRTOFees.Rows.Count = 2;
            fgAdvisoryOptions.Rows.Count = 2;
            fgAdvisoryFees.Rows.Count = 2;
            fgDiscretOptions.Rows.Count = 2;
            fgDiscretFees.Rows.Count = 2;
            fgSafekeepingOptions.Rows.Count = 2;
            fgSafekeepingFees.Rows.Count = 2;
            fgAdministrationOptions.Rows.Count = 2;
            fgAdministrationFees.Rows.Count = 2;
            fgDealAdvisoryOptions.Rows.Count = 2;
            fgDealAdvisoryFees.Rows.Count = 2;
            fgLombardOptions.Rows.Count = 1;
            fgLombardFees.Rows.Count = 1;
            fgFXOptions.Rows.Count = 1;
            fgFXFees.Rows.Count = 2;
            fgSettlementsOptions.Rows.Count = 1;
            fgSettlementsFees.Rows.Count = 2;

            //--------------- Define Options List --------------------
            ServiceProvidersOptions = new clsServiceProvidersOptions();
            ServiceProvidersOptions.ServiceProvider_ID = iID;
            ServiceProvidersOptions.ServiceType_ID = 0;
            ServiceProvidersOptions.GetList();
            foreach (DataRow dtRow in ServiceProvidersOptions.List.Rows)
            {

                sTemp = dtRow["Title"] + "\t" + dtRow["DateStart"] + "\t" + dtRow["DateFinish"] + "\t" + dtRow["MonthMinAmount"] + "\t" + dtRow["MonthMinCurr"] + "\t" +
                        dtRow["OpenAmount"] + "\t" + dtRow["OpenCurr"] + "\t" + dtRow["ServiceAmount"] + "\t" + dtRow["ServiceCurr"] + "\t" +
                        dtRow["MinAmount"] + "\t" + dtRow["MinCurr"] + "\t" + dtRow["ID"];

                switch (Convert.ToInt32(dtRow["ServiceType_ID"]))
                {
                    case 1:                  // 1-Brokerage
                        fgBrokerageOptions.AddItem(sTemp);
                        break;
                    case 2:                  // 2-Advisory
                        fgAdvisoryOptions.AddItem(sTemp);
                        break;
                    case 3:                  // 4-Discretionary
                        fgDiscretOptions.AddItem(sTemp);
                        break;
                    case 4:                  // 3-Safekeeping
                        sTemp = dtRow["Title"] + "\t" + dtRow["DateStart"] + "\t" + dtRow["DateFinish"] + "\t" + dtRow["MonthMinAmount"] + "\t" + dtRow["MonthMinCurr"] + "\t" +
                                dtRow["MinAmount"] + "\t" + dtRow["MinCurr"] + "\t" + dtRow["OpenAmount"] + "\t" + dtRow["OpenCurr"] + "\t" + dtRow["ServiceAmount"] + "\t" +
                                dtRow["ServiceCurr"] + "\t" + dtRow["CalcAUM"] + "\t" + dtRow["CalcSecurities"] + "\t" + dtRow["CalcCash"] + "\t" + dtRow["ID"];
                        fgSafekeepingOptions.AddItem(sTemp);
                        break;
                    case 5:                  // 5-DealAdvisory
                        fgDealAdvisoryOptions.AddItem(sTemp);
                        break;
                    case 6:                  // 6-LombardLending
                        fgLombardOptions.AddItem(sTemp);
                        break;
                    case 7:                  // 7-FX
                        fgFXOptions.AddItem(sTemp);
                        break;
                    case 8:                  // 8-Settlements
                        fgSettlementsOptions.AddItem(sTemp);
                        break;
                    case 9:                  // 9-RTO
                        fgRTOOptions.AddItem(sTemp);
                        break;
                    case 10:                  // 10 - Administration
                        sTemp = dtRow["Title"] + "\t" + dtRow["DateStart"] + "\t" + dtRow["DateFinish"] + "\t" + dtRow["MonthMinAmount"] + "\t" + dtRow["MonthMinCurr"] + "\t" +
                                dtRow["MinAmount"] + "\t" + dtRow["MinCurr"] + "\t" + dtRow["OpenAmount"] + "\t" + dtRow["OpenCurr"] + "\t" + dtRow["ServiceAmount"] + "\t" +
                                dtRow["ServiceCurr"] + "\t" + dtRow["CalcAUM"] + "\t" + dtRow["CalcSecurities"] + "\t" + dtRow["CalcCash"] + "\t" + dtRow["ID"];
                        fgAdministrationOptions.AddItem(sTemp);
                        break;
                }
            }

            ServiceProvidersOptions.List.DefaultView.Sort = "ServiceType_ID, DateStart, Title";
            dtOptions = ServiceProvidersOptions.List.Copy();

            fgBrokerageOptions.Redraw = true;
            fgRTOOptions.Redraw = true;
            fgAdvisoryOptions.Redraw = true;
            fgDealAdvisoryOptions.Redraw = true;
            fgDiscretOptions.Redraw = true;
            fgSafekeepingOptions.Redraw = true;
            fgAdministrationOptions.Redraw = true;
            fgLombardOptions.Redraw = true;
            fgFXOptions.Redraw = true;
            fgSettlementsOptions.Redraw = true;

            //--- Define Clients Data -------------------------
            fgPackages.Redraw = false;
            fgPackages.Rows.Count = 1;

            Contracts = new clsContracts();
            Contracts.PackageType = 2;
            Contracts.DateStart = Convert.ToDateTime("1900/01/01");
            Contracts.DateFinish = Convert.ToDateTime("2070/12/31");
            Contracts.Client_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
            Contracts.Advisor_ID = 0;
            Contracts.Service_ID = 0;
            Contracts.ServiceProvider_ID = 0;
            Contracts.ClientName = "";
            Contracts.Status = -1;
            Contracts.ClientStatus = -1;
            Contracts.GetList();
            foreach (DataRow dtRow in Contracts.List.Rows)
                fgPackages.AddItem(dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t" + "0");

            fgPackages.Redraw = true;

            fgPackages.Row = 0;
            if (fgPackages.Rows.Count > 1)
            {
                bCheckList = true;
                fgPackages.Row = 1;
                bCheckList = false;
            }

            bCheckBrokerageFees = true;
            bCheckRTOFees = true;
            bCheckAdvisoryFees = true;
            bCheckDealAdvisoryFees = true;
            bCheckDiscretFees = true;
            bCheckSafekeepingFees = true;
            bCheckAdministrationFees = true;
            bCheckLombardFees = true;
            bCheckFXFees = true;
            bCheckSettlementsFees = true;

            fgBrokerageFees.Rows.Count = 2;
            if (fgBrokerageOptions.Rows.Count > 1)
            {
                fgBrokerageOptions.Row = 1;
                DefineBrokerageFeesList();
            }

            fgRTOFees.Rows.Count = 2;
            if (fgRTOOptions.Rows.Count > 1)
            {
                fgRTOOptions.Row = 1;
                DefineRTOFeesList();
            }

            fgFXFees.Rows.Count = 2;
            if (fgFXOptions.Rows.Count > 1)
            {
                fgFXOptions.Row = 1;
                DefineFXFeesList();
            }

            fgSafekeepingFees.Rows.Count = 2;
            if (fgSafekeepingOptions.Rows.Count > 2)
            {
                fgSafekeepingOptions.Row = 2;
                DefineSafekeepingFeesList();
            }

            fgAdvisoryFees.Rows.Count = 2;
            if (fgAdvisoryOptions.Rows.Count > 2)
            {
                fgAdvisoryOptions.Row = 2;
                DefineAdvisoryFeesList();
            }

            fgDiscretFees.Rows.Count = 2;
            if (fgDiscretOptions.Rows.Count > 2)
            {
                fgDiscretOptions.Row = 2;
                DefineDiscretFeesList();
            }

            fgAdministrationFees.Rows.Count = 2;
            if (fgAdministrationOptions.Rows.Count > 2)
            {
                fgAdministrationOptions.Row = 2;
                DefineAdministrationFeesList();
            }

            fgDealAdvisoryFees.Rows.Count = 2;
            if (fgDealAdvisoryOptions.Rows.Count > 2)
            {
                fgDealAdvisoryOptions.Row = 2;
                DefineDealAdvisoryFeesList();
            }

            fgLombardFees.Rows.Count = 1;
            if (fgLombardOptions.Rows.Count > 1)
            {
                fgLombardOptions.Row = 1;
                DefineLombardFeesList();
            }


            fgSettlementsFees.Rows.Count = 2;
            if (fgSettlementsOptions.Rows.Count > 1) {
                fgSettlementsOptions.Row = 1;
                DefineSettlementsFeesList();
            }
            ChangeMode(1);
        }
        #endregion
        #region --- fgPackage functionality --------------------------------------------------------------------------------
        private void fgPackages_RowColChange(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (fgPackages.Row > 0) {
                    ucCC.Mode = 2;                       // 1 - for PackageTypes=1,  2 - for PackageTypes=2
                    ucCC.ShowRecord(2, 0, Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_ID"]),
                                    Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_Details_ID"]), Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_Packages_ID"]), 1, iRightsLevel);
                }
            }
        }
        #endregion
        #region --- Brokerage functionality --------------------------------------------------------------------------------
        private void tsbAddBrokerageOption_Click(object sender, EventArgs e)
        {
            iService = 1;                                                                            // 1 - Brokerage
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            ShowEditOption();
        }
        private void tsbEditBrokerageOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 1;                                                                            // 1 - Brokerage
                iLocAktion = 1;
                txtOption.Text = fgBrokerageOptions[fgBrokerageOptions.Row, 0] + "";
                dStart.Value = Convert.ToDateTime(fgBrokerageOptions[fgBrokerageOptions.Row, 1]);
                dFinish.Value = Convert.ToDateTime(fgBrokerageOptions[fgBrokerageOptions.Row, 2]);
                txtMonthMinAmount.Text = "0";
                iOption_ID = Convert.ToInt32(fgBrokerageOptions[fgBrokerageOptions.Row, "ID"]);
                ShowEditOption();
            }
        }

        private void tsbDelBrokerageOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgBrokerageOptions[fgBrokerageOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgBrokerageOptions.RemoveItem(fgBrokerageOptions.Row);
            }
        }
        private void fgBrokerageOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckBrokerageFees && fgBrokerageOptions.Rows.Count > 1) DefineBrokerageFeesList();
        }
        private void tsbAddBrokerageFees_Click(object sender, EventArgs e)
        {
            iFees_ID = 0;
            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 0;                                              // 0 - ADD
            locServiceProviderFees.Product_ID = 0;
            locServiceProviderFees.Category_ID = 0;
            locServiceProviderFees.txtAmountFrom.Text = "0";
            locServiceProviderFees.txtAmountTo.Text = "90000000";
            locServiceProviderFees.txtBuyFees.Text = "0";
            locServiceProviderFees.txtSellFees.Text = "0";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = "0";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = "0";
            locServiceProviderFees.cmbTicketFeesCurrs.Text = "EUR";
            locServiceProviderFees.txtMinimumFeesAmount.Text = "0";
            locServiceProviderFees.cmbMinimumFeesCurrs.Text = "EUR";
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = 0;
            locServiceProviderFees.txtProvider.Text = "0";
            locServiceProviderFees.txtCompany.Text = "0";
            locServiceProviderFees.Mode = 1;                                                        // 1 - Brokerage
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1) {

                fgBrokerageFees.Redraw = false;

                dtList = Global.dtStockExchanges.Copy();
                foundRows = dtList.Select("ID = 0");
                foundRows[0]["Title"] = "Όλα";
       
                dtView2 = dtList.DefaultView;
                sTemp = "ID = " + locServiceProviderFees.cmbStockExchanges.SelectedValue;
                dtView2.RowFilter = sTemp;

                foreach (DataRowView dtViewRow2 in dtView2) {
                    if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == 0) {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0) {
                           dtView = Global.dtProductsCategories.DefaultView;
                           foreach (DataRowView dtViewRow in dtView) {
                                if (Convert.ToInt32(dtViewRow["ID"]) != 0)  {
                                    iFees_ID = SaveBrokerageFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }                                
                           }
                        }
                        else {
                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == Convert.ToInt32(dtViewRow["ID"])) {
                                    iFees_ID = SaveBrokerageFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                    }
                    else {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0) {

                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == Convert.ToInt32(dtViewRow["Product_ID"])) {
                                    iFees_ID = SaveBrokerageFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                        else {
                            iFees_ID = SaveBrokerageFees(0, locServiceProviderFees.Product_ID, locServiceProviderFees.cmbProducts.Text + "", locServiceProviderFees.Category_ID, 
                                locServiceProviderFees.cmbCategories.Text + "", Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                               Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                               Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                               Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                               locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                               locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                               Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                               locServiceProviderFees.cmbSettlementProviders.Text);
                        }
                    }
                }

                fgBrokerageFees.Rows[0].AllowMerging = true;
                fgBrokerageFees.Rows[1].AllowMerging = true;

                fgBrokerageFees.Cols[0].AllowMerging = true;
                fgBrokerageFees.Cols[1].AllowMerging = true;

                fgBrokerageFees.Redraw = true;
            }
        }
        private void tsbEditBrokerageFees_Click(object sender, EventArgs e)
        {
            iRow = fgBrokerageFees.Row;

            ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();
            ServiceProviderBrokerageFees.Record_ID = Convert.ToInt32(fgBrokerageFees[iRow, "ID"]);
            ServiceProviderBrokerageFees.GetRecord();            
            
            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 1;                                              // 1 - EDIT
            locServiceProviderFees.Product_ID = ServiceProviderBrokerageFees.Product_ID;
            locServiceProviderFees.Category_ID = ServiceProviderBrokerageFees.ProductCategory_ID;
            locServiceProviderFees.StockExchange_ID = ServiceProviderBrokerageFees.StockExchange_ID;
            locServiceProviderFees.txtAmountFrom.Text = ServiceProviderBrokerageFees.AmountFrom + "";
            locServiceProviderFees.txtAmountTo.Text = ServiceProviderBrokerageFees.AmountTo.ToString("0.##"); 
            locServiceProviderFees.txtBuyFees.Text = ServiceProviderBrokerageFees.BuyFeesPercent + "";
            locServiceProviderFees.txtSellFees.Text = ServiceProviderBrokerageFees.SellFeesPercent + "";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = ServiceProviderBrokerageFees.TicketFeesBuyAmount + "";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = ServiceProviderBrokerageFees.TicketFeesSellAmount + "";
            locServiceProviderFees.TicketFeesCurr = ServiceProviderBrokerageFees.TicketFeesCurr;
            locServiceProviderFees.txtMinimumFeesAmount.Text = ServiceProviderBrokerageFees.MinimumFees + "";
            locServiceProviderFees.MinimumFeesCurr = ServiceProviderBrokerageFees.MinimumFeesCurr;
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = ServiceProviderBrokerageFees.RetrosessionMethod;
            locServiceProviderFees.txtProvider.Text = ServiceProviderBrokerageFees.RetrosessionProvider + "";
            locServiceProviderFees.txtCompany.Text = ServiceProviderBrokerageFees.RetrosessionCompany + "";
            locServiceProviderFees.SettlementProviders_ID = ServiceProviderBrokerageFees.SettlementProvider_ID;
            locServiceProviderFees.Mode = 1;                                                        // 1 - Brokerage
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1) {
                iFees_ID = SaveBrokerageFees(Convert.ToInt32(fgBrokerageFees[iRow, "ID"]), locServiceProviderFees.Product_ID, 
                    locServiceProviderFees.cmbProducts.Text, locServiceProviderFees.Category_ID, locServiceProviderFees.cmbCategories.Text,
                    Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                    Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                    Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                    Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                    locServiceProviderFees.cmbTicketFeesCurrs.Text + "", Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), 
                    locServiceProviderFees.cmbMinimumFeesCurrs.Text + "", Convert.ToInt32(locServiceProviderFees.cmbDistribMethods.SelectedIndex),
                    Convert.ToSingle(locServiceProviderFees.txtProvider.Text), Convert.ToSingle(locServiceProviderFees.txtCompany.Text), 
                    Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue), locServiceProviderFees.cmbSettlementProviders.Text);

                DefineBrokerageFeesList();
                fgBrokerageFees.Row = iRow;
            }
        }
        private void tsbDelBrokerageFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();
                ServiceProviderBrokerageFees.Record_ID = Convert.ToInt32(fgBrokerageFees[fgBrokerageFees.Row, "ID"]);
                ServiceProviderBrokerageFees.DeleteRecord();

                fgBrokerageFees.RemoveItem(fgBrokerageFees.Row);
            }
        }
        private int SaveBrokerageFees(int iRec_ID, int iProduct_ID, string sProduct, int iCategory_ID, string sCategory, 
                                      int iStockExchange_ID, string sStockExchange, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees, 
                                      float fltTicketBuyFees, float fltTicketSellFees, string sTicketFeesCurr, float fltMinimumFees, string sMinimumFeesCurr,
                                      int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany, int iSettlementProvider_ID, string sSettlementProvider)
        {
            ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();

            if (iRec_ID > 0)                                           // 0 - ADD, > 0 - Edit
            {
                ServiceProviderBrokerageFees.Record_ID = iRec_ID;
                ServiceProviderBrokerageFees.GetRecord();
            }

            ServiceProviderBrokerageFees.SPO_ID = Convert.ToInt32(fgBrokerageOptions[fgBrokerageOptions.Row, "ID"]); 
            ServiceProviderBrokerageFees.ServiceProvider_ID = iID;
            ServiceProviderBrokerageFees.Product_ID = iProduct_ID;
            ServiceProviderBrokerageFees.ProductCategory_ID = iCategory_ID;
            ServiceProviderBrokerageFees.StockExchange_ID = iStockExchange_ID;
            ServiceProviderBrokerageFees.AmountFrom = fltAmountFrom;
            ServiceProviderBrokerageFees.AmountTo = fltAmountTo;
            ServiceProviderBrokerageFees.BuyFeesPercent = fltBuyFees;
            ServiceProviderBrokerageFees.SellFeesPercent = fltSellFees;
            ServiceProviderBrokerageFees.TicketFeesBuyAmount = fltTicketBuyFees;
            ServiceProviderBrokerageFees.TicketFeesSellAmount = fltTicketSellFees;
            ServiceProviderBrokerageFees.TicketFeesCurr = sTicketFeesCurr;
            ServiceProviderBrokerageFees.MinimumFees = fltMinimumFees;
            ServiceProviderBrokerageFees.MinimumFeesCurr = sMinimumFeesCurr;
            ServiceProviderBrokerageFees.RetrosessionMethod = iRetrosessionMethod;
            ServiceProviderBrokerageFees.RetrosessionProvider = fltRetrosessionProvider;
            ServiceProviderBrokerageFees.RetrosessionCompany = fltRetrosessionCompany;
            ServiceProviderBrokerageFees.SettlementProvider_ID = iSettlementProvider_ID;

            if (iRec_ID == 0) {
                 iFees_ID = ServiceProviderBrokerageFees.InsertRecord();

                 AddBrokerageFees(iProduct_ID, sProduct, iCategory_ID, sCategory, iStockExchange_ID, sStockExchange,
                                  fltAmountFrom, fltAmountTo, fltBuyFees, fltSellFees, fltTicketBuyFees, fltTicketSellFees, sTicketFeesCurr,
                                  fltMinimumFees, sMinimumFeesCurr, iRetrosessionMethod, fltRetrosessionProvider, fltRetrosessionCompany, 
                                  iSettlementProvider_ID, sSettlementProvider, iFees_ID);
            }
            else iFees_ID = ServiceProviderBrokerageFees.EditRecord();

            return iFees_ID;
        }
        private void AddBrokerageFees(int iProduct_ID, string sProduct, int iProductCategory_ID, string sProductCategory, int iStockExchange_ID,
                                      string sStockExchange_Title, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees,
                                      float fltTicketFeesBuyAmount, float fltTicketFeesSellAmount, string sTicketFeesCurrs, float fltMinimumFeesAmount,
                                      string sMinimumFeesCurrs, int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany,
                                      int iSettlementProviders_ID, string sSettlementProviders_Title, int iRec_ID)
        {
            fgBrokerageFees.Redraw = false;
            fgBrokerageFees.AddItem(sProduct + "\t" + sProductCategory + "\t" + sStockExchange_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" +
                                    fltBuyFees + "\t" + fltSellFees + "\t" + fltTicketFeesBuyAmount + "\t" + fltTicketFeesSellAmount + "\t" + sTicketFeesCurrs + "\t" +
                                    fltMinimumFeesAmount + "\t" + sMinimumFeesCurrs + "\t" + sDistrib[iRetrosessionMethod] + "\t" + fltRetrosessionProvider + "\t" +
                                    fltRetrosessionCompany + "\t" + sSettlementProviders_Title + "\t" + iRec_ID + "\t" + iProduct_ID + "\t" +
                                    iProductCategory_ID + "\t" + iRetrosessionMethod + "\t" + iStockExchange_ID + "\t" + iSettlementProviders_ID);
            fgBrokerageFees.Redraw = true;
        }
        private void DefineBrokerageFeesList()
        {

            ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();
            ServiceProviderBrokerageFees.ServiceProvider_ID = iID;
            ServiceProviderBrokerageFees.SPO_ID = Convert.ToInt32(fgBrokerageOptions[fgBrokerageOptions.Row, "ID"]);
            ServiceProviderBrokerageFees.GetFees();

            fgBrokerageFees.Redraw = false;
            fgBrokerageFees.Rows.Count = 2;
            if (fgBrokerageOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderBrokerageFees.List.Rows)
                    fgBrokerageFees.AddItem(dtRow["ProductTitle"] + "\t" + dtRow["ProductCategoryTitle"] + "\t" + dtRow["StockExchange_Title"] + "\t" +
                                dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                dtRow["MinimumFeesAmount"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" +
                                dtRow["RetrosessionProvider"] + "\t" + dtRow["RetrosessionCompany"] + "\t" + dtRow["SettlementProvider_Title"] + "\t" +
                                dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["RetrosessionMethod"] + "\t" + 
                                dtRow["StockExchange_ID"] + "\t" + dtRow["SettlementProvider_ID"]);
            }
            fgBrokerageFees.Redraw = true;

            if (fgBrokerageFees.Rows.Count > 2) tsbDelBrokerageOption.Enabled = false;
            else tsbDelBrokerageOption.Enabled = true;
        }
        #endregion
        #region --- RTO functionality --------------------------------------------------------------------------------------
        private void tsbAddRTOOption_Click(object sender, EventArgs e)
        {
            iService = 9;                                                                                // 9 - RTO
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            ShowEditOption();
        }

        private void tsbEditRTOOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 9;                                                                            // 9 - RTO
                iLocAktion = 1;
                txtOption.Text = fgRTOOptions[fgRTOOptions.Row, 0] + "";
                dStart.Value = Convert.ToDateTime(fgRTOOptions[fgRTOOptions.Row, 1]);
                dFinish.Value = Convert.ToDateTime(fgRTOOptions[fgRTOOptions.Row, 2]);
                txtMonthMinAmount.Text = "0";
                iOption_ID = Convert.ToInt32(fgRTOOptions[fgRTOOptions.Row, "ID"]);
                ShowEditOption();
            }
        }

        private void tsbDelRTOOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgRTOOptions[fgRTOOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgRTOOptions.RemoveItem(fgRTOOptions.Row);
            }
        }       
        private void fgRTOOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckRTOFees && fgRTOOptions.Rows.Count > 1) DefineRTOFeesList();
        }
        private void tsbAddRTOFees_Click(object sender, EventArgs e)
        {
            iFees_ID = 0;
            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 0;                                              // 0 - ADD
            locServiceProviderFees.Product_ID = 0;
            locServiceProviderFees.Category_ID = 0;
            locServiceProviderFees.txtAmountFrom.Text = "0";
            locServiceProviderFees.txtAmountTo.Text = "90000000";
            locServiceProviderFees.txtBuyFees.Text = "0";
            locServiceProviderFees.txtSellFees.Text = "0";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = "0";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = "0";
            locServiceProviderFees.cmbTicketFeesCurrs.Text = "EUR";
            locServiceProviderFees.txtMinimumFeesAmount.Text = "0";
            locServiceProviderFees.cmbMinimumFeesCurrs.Text = "EUR";
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = 0;
            locServiceProviderFees.txtProvider.Text = "0";
            locServiceProviderFees.txtCompany.Text = "0";
            locServiceProviderFees.Mode = 9;                                                        // 9 - RTO
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1)
            {
                fgRTOFees.Redraw = false;

                dtList = Global.dtStockExchanges.Copy();
                foundRows = dtList.Select("ID = 0");
                foundRows[0]["Title"] = "Όλα";

                dtView2 = dtList.DefaultView;
                sTemp = "ID = " + locServiceProviderFees.cmbStockExchanges.SelectedValue;
                dtView2.RowFilter = sTemp;

                foreach (DataRowView dtViewRow2 in dtView2)
                {
                    if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == 0)
                    {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0)
                        {
                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(dtViewRow["ID"]) != 0) {
                                    iFees_ID = SaveRTOFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                   Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                   Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                   Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                   Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                   locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                   locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                   Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                   locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                        else
                        {
                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == Convert.ToInt32(dtViewRow["ID"]))
                                {
                                    iFees_ID = SaveRTOFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0)
                        {

                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == Convert.ToInt32(dtViewRow["Product_ID"]))
                                {
                                    iFees_ID = SaveRTOFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                        else
                        {
                            iFees_ID = SaveRTOFees(0, locServiceProviderFees.Product_ID, locServiceProviderFees.cmbProducts.Text + "", locServiceProviderFees.Category_ID,
                                locServiceProviderFees.cmbCategories.Text + "", Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                               Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                               Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                               Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                               locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                               locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                               Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                               locServiceProviderFees.cmbSettlementProviders.Text);
                        }
                    }
                }

                fgRTOFees.Rows[0].AllowMerging = true;
                fgRTOFees.Rows[1].AllowMerging = true;

                fgRTOFees.Cols[0].AllowMerging = true;
                fgRTOFees.Cols[1].AllowMerging = true;

                fgRTOFees.Redraw = true;
            }
        }
        private void tsbEditRTOFees_Click(object sender, EventArgs e)
        {
            iRow = fgRTOFees.Row;

            ServiceProviderRTOFees = new clsServiceProviderRTOFees();
            ServiceProviderRTOFees.Record_ID = Convert.ToInt32(fgRTOFees[iRow, "ID"]);
            ServiceProviderRTOFees.GetRecord();

            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 1;                                              // 1 - EDIT
            locServiceProviderFees.Product_ID = ServiceProviderRTOFees.Product_ID;
            locServiceProviderFees.Category_ID = ServiceProviderRTOFees.ProductCategory_ID;
            locServiceProviderFees.StockExchange_ID = ServiceProviderRTOFees.StockExchange_ID;
            locServiceProviderFees.txtAmountFrom.Text = ServiceProviderRTOFees.AmountFrom + "";
            locServiceProviderFees.txtAmountTo.Text = ServiceProviderRTOFees.AmountTo.ToString("0.##");
            locServiceProviderFees.txtBuyFees.Text = ServiceProviderRTOFees.BuyFeesPercent + "";
            locServiceProviderFees.txtSellFees.Text = ServiceProviderRTOFees.SellFeesPercent + "";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = ServiceProviderRTOFees.TicketFeesBuyAmount + "";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = ServiceProviderRTOFees.TicketFeesSellAmount + "";
            locServiceProviderFees.TicketFeesCurr = ServiceProviderRTOFees.TicketFeesCurr;
            locServiceProviderFees.txtMinimumFeesAmount.Text = ServiceProviderRTOFees.MinimumFees + "";
            locServiceProviderFees.MinimumFeesCurr = ServiceProviderRTOFees.MinimumFeesCurr;
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = ServiceProviderRTOFees.RetrosessionMethod;
            locServiceProviderFees.txtProvider.Text = ServiceProviderRTOFees.RetrosessionProvider + "";
            locServiceProviderFees.txtCompany.Text = ServiceProviderRTOFees.RetrosessionCompany + "";
            locServiceProviderFees.SettlementProviders_ID = ServiceProviderRTOFees.SettlementProvider_ID;
            locServiceProviderFees.Mode = 9;                                                        // 9 - RTO
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1)
            {
                iFees_ID = SaveRTOFees(Convert.ToInt32(fgRTOFees[iRow, "ID"]), locServiceProviderFees.Product_ID,
                    locServiceProviderFees.cmbProducts.Text, locServiceProviderFees.Category_ID, locServiceProviderFees.cmbCategories.Text,
                    Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                    Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                    Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                    Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                    locServiceProviderFees.cmbTicketFeesCurrs.Text + "", Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text),
                    locServiceProviderFees.cmbMinimumFeesCurrs.Text + "", Convert.ToInt32(locServiceProviderFees.cmbDistribMethods.SelectedIndex),
                    Convert.ToSingle(locServiceProviderFees.txtProvider.Text), Convert.ToSingle(locServiceProviderFees.txtCompany.Text),
                    Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue), locServiceProviderFees.cmbSettlementProviders.Text);

                DefineRTOFeesList();
                fgRTOFees.Row = iRow;
            }
        }

        private void tsbDelRTOFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderRTOFees = new clsServiceProviderRTOFees();
                ServiceProviderRTOFees.Record_ID = Convert.ToInt32(fgRTOFees[fgRTOFees.Row, "ID"]);
                ServiceProviderRTOFees.DeleteRecord();

                fgRTOFees.RemoveItem(fgRTOFees.Row);
            }
        }
        private int SaveRTOFees(int iRec_ID, int iProduct_ID, string sProduct, int iCategory_ID, string sCategory,
                              int iStockExchange_ID, string sStockExchange, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees,
                              float fltTicketBuyFees, float fltTicketSellFees, string sTicketFeesCurr, float fltMinimumFees, string sMinimumFeesCurr,
                              int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany, int iSettlementProvider_ID, string sSettlementProvider)
        {
            ServiceProviderRTOFees = new clsServiceProviderRTOFees();

            if (iRec_ID > 0)                                           // 0 - ADD, > 0 - Edit
            {
                ServiceProviderRTOFees.Record_ID = iRec_ID;
                ServiceProviderRTOFees.GetRecord();
            }

            ServiceProviderRTOFees.SPO_ID = Convert.ToInt32(fgRTOOptions[fgRTOOptions.Row, "ID"]);
            ServiceProviderRTOFees.ServiceProvider_ID = iID;
            ServiceProviderRTOFees.Product_ID = iProduct_ID;
            ServiceProviderRTOFees.ProductCategory_ID = iCategory_ID;
            ServiceProviderRTOFees.StockExchange_ID = iStockExchange_ID;
            ServiceProviderRTOFees.AmountFrom = fltAmountFrom;
            ServiceProviderRTOFees.AmountTo = fltAmountTo;
            ServiceProviderRTOFees.BuyFeesPercent = fltBuyFees;
            ServiceProviderRTOFees.SellFeesPercent = fltSellFees;
            ServiceProviderRTOFees.TicketFeesBuyAmount = fltTicketBuyFees;
            ServiceProviderRTOFees.TicketFeesSellAmount = fltTicketSellFees;
            ServiceProviderRTOFees.TicketFeesCurr = sTicketFeesCurr;
            ServiceProviderRTOFees.MinimumFees = fltMinimumFees;
            ServiceProviderRTOFees.MinimumFeesCurr = sMinimumFeesCurr;
            ServiceProviderRTOFees.RetrosessionMethod = iRetrosessionMethod;
            ServiceProviderRTOFees.RetrosessionProvider = fltRetrosessionProvider;
            ServiceProviderRTOFees.RetrosessionCompany = fltRetrosessionCompany;
            ServiceProviderRTOFees.SettlementProvider_ID = iSettlementProvider_ID;

            if (iRec_ID == 0)
            {
                iFees_ID = ServiceProviderRTOFees.InsertRecord();

                AddRTOFees(iProduct_ID, sProduct, iCategory_ID, sCategory, iStockExchange_ID, sStockExchange,
                                 fltAmountFrom, fltAmountTo, fltBuyFees, fltSellFees, fltTicketBuyFees, fltTicketSellFees, sTicketFeesCurr,
                                 fltMinimumFees, sMinimumFeesCurr, iRetrosessionMethod, fltRetrosessionProvider, fltRetrosessionCompany,
                                 iSettlementProvider_ID, sSettlementProvider, iFees_ID);
            }
            else iFees_ID = ServiceProviderRTOFees.EditRecord();

            return iFees_ID;
        }
        private void AddRTOFees(int iProduct_ID, string sProduct, int iProductCategory_ID, string sProductCategory, int iStockExchange_ID,
                              string sStockExchange_Title, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees,
                              float fltTicketFeesBuyAmount, float fltTicketFeesSellAmount, string sTicketFeesCurrs, float fltMinimumFeesAmount,
                              string sMinimumFeesCurrs, int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany,
                              int iSettlementProviders_ID, string sSettlementProviders_Title, int iRec_ID)
        {
            fgRTOFees.Redraw = false;
            fgRTOFees.AddItem(sProduct + "\t" + sProductCategory + "\t" + sStockExchange_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" +
                                    fltBuyFees + "\t" + fltSellFees + "\t" + fltTicketFeesBuyAmount + "\t" + fltTicketFeesSellAmount + "\t" + sTicketFeesCurrs + "\t" +
                                    fltMinimumFeesAmount + "\t" + sMinimumFeesCurrs + "\t" + sDistrib[iRetrosessionMethod] + "\t" + fltRetrosessionProvider + "\t" +
                                    fltRetrosessionCompany + "\t" + sSettlementProviders_Title + "\t" + iRec_ID + "\t" + iProduct_ID + "\t" +
                                    iProductCategory_ID + "\t" + iRetrosessionMethod + "\t" + iStockExchange_ID + "\t" + iSettlementProviders_ID);
            fgRTOFees.Redraw = true;
        }
        private void DefineRTOFeesList()
        {
            ServiceProviderRTOFees = new clsServiceProviderRTOFees();
            ServiceProviderRTOFees.ServiceProvider_ID = iID;
            ServiceProviderRTOFees.SPO_ID = Convert.ToInt32(fgRTOOptions[fgRTOOptions.Row, "ID"]);
            ServiceProviderRTOFees.GetFees();

            fgRTOFees.Redraw = false;
            fgRTOFees.Rows.Count = 2;
            if (fgRTOOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderRTOFees.List.Rows)
                    fgRTOFees.AddItem(dtRow["ProductTitle"] + "\t" + dtRow["ProductCategoryTitle"] + "\t" + dtRow["StockExchange_Title"] + "\t" +
                               dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                               dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                               dtRow["MinimumFeesAmount"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" +
                               dtRow["RetrosessionProvider"] + "\t" + dtRow["RetrosessionCompany"] + "\t" + dtRow["SettlementProvider_Title"] + "\t" +
                               dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["RetrosessionMethod"] + "\t" +
                               dtRow["StockExchange_ID"] + "\t" + dtRow["SettlementProvider_ID"]);
            }
            fgRTOFees.Redraw = true;

            if (fgRTOFees.Rows.Count > 2) tsbDelRTOOption.Enabled = false;
            else tsbDelRTOOption.Enabled = true;
        }
        #endregion
        #region --- FX functionality ---------------------------------------------------------------------------------------
        private void tsbAddFXOption_Click(object sender, EventArgs e)
        {
            iService = 7;                                            // 7 - FX
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            ShowEditOption();
        }

        private void tsbEditFXOption_Click(object sender, EventArgs e)
        {
            iService = 7;                                                                            // 7 - FX
            iLocAktion = 1;
            txtOption.Text = fgFXOptions[fgFXOptions.Row, 0] + "";
            dStart.Value = Convert.ToDateTime(fgFXOptions[fgFXOptions.Row, 1]);
            dFinish.Value = Convert.ToDateTime(fgFXOptions[fgFXOptions.Row, 2]);
            txtMonthMinAmount.Text = "0";
            iOption_ID = Convert.ToInt32(fgFXOptions[fgFXOptions.Row, "ID"]);
            ShowEditOption();
        }
        private void tsbDelFXOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgFXOptions[fgFXOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgFXOptions.RemoveItem(fgFXOptions.Row);
            }
        }
        private void fgFXOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckFXFees && fgFXOptions.Rows.Count > 1) DefineFXFeesList();
        } 
        private void tsbFXAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 0;                      // 0 - ADD
            locServiceProviderFees2.txtAmountFrom.Text = "0";
            locServiceProviderFees2.txtAmountTo.Text = "90000000";
            locServiceProviderFees2.txtFees.Text = "0";
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1) {
                ServiceProviderFXFees = new clsServiceProviderFXFees();
                ServiceProviderFXFees.SPO_ID = Convert.ToInt32(fgFXOptions[fgFXOptions.Row, "ID"]);
                ServiceProviderFXFees.ServiceProvider_ID = iID;
                ServiceProviderFXFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderFXFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderFXFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderFXFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderFXFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderFXFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderFXFees.InsertRecord();

                AddFXFees(Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text),
                 Convert.ToSingle(locServiceProviderFees2.txtFees.Text), locServiceProviderFees2.cmbDistribMethods.SelectedIndex, 
                 Convert.ToSingle(locServiceProviderFees2.txtProvider.Text), Convert.ToSingle(locServiceProviderFees2.txtCompany.Text), iFees_ID);
            }
        }
        private void tsbFXEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgFXFees.Row;

            ServiceProviderFXFees = new clsServiceProviderFXFees();
            ServiceProviderFXFees.Record_ID = Convert.ToInt32(fgFXFees[iRow, "ID"]);
            ServiceProviderFXFees.GetRecord();

            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 1;                                                                 // 1 - EDIT
            locServiceProviderFees2.txtAmountFrom.Text = ServiceProviderFXFees.AmountFrom + "";
            locServiceProviderFees2.txtAmountTo.Text = ServiceProviderFXFees.AmountTo.ToString("0.##");
            locServiceProviderFees2.txtFees.Text = ServiceProviderFXFees.FeesPercent + "";
            locServiceProviderFees2.cmbDistribMethods.SelectedIndex = ServiceProviderFXFees.RetrosessionMethod;
            locServiceProviderFees2.txtProvider.Text = ServiceProviderFXFees.RetrosessionProvider + "";
            locServiceProviderFees2.txtCompany.Text = ServiceProviderFXFees.RetrosessionCompany + "";
            locServiceProviderFees2.Mode = 7;                                                                   // 7 - FX
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1)
            {
                ServiceProviderFXFees = new clsServiceProviderFXFees();
                ServiceProviderFXFees.Record_ID = Convert.ToInt32(fgFXFees[iRow, "ID"]);
                ServiceProviderFXFees.GetRecord();
                ServiceProviderFXFees.SPO_ID = Convert.ToInt32(fgFXOptions[fgFXOptions.Row, "ID"]);
                ServiceProviderFXFees.ServiceProvider_ID = iID;
                ServiceProviderFXFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderFXFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderFXFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderFXFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderFXFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderFXFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderFXFees.EditRecord();

                DefineFXFeesList();
                fgFXFees.Row = iRow;
            }
        }
        private void tsbDelFXFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderFXFees = new clsServiceProviderFXFees();
                ServiceProviderFXFees.Record_ID = Convert.ToInt32(fgFXFees[fgFXFees.Row, "ID"]);
                ServiceProviderFXFees.DeleteRecord();

                fgFXFees.RemoveItem(fgFXFees.Row);
            }
        }
        private void AddFXFees(float fltAmountFrom, float fltAmountTo, float fltFees, int iRetrosessionMethod, float fltRetrosessionProvider, 
                               float fltRetrosessionCompany, int iRec_ID)
        {
            fgFXFees.Redraw = false;
            fgFXFees.AddItem(fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" + sDistrib[iRetrosessionMethod] + "\t" + 
                             fltRetrosessionProvider + "\t" + fltRetrosessionCompany + "\t" + iRec_ID);
            fgFXFees.Redraw = true;
        }
        private void DefineFXFeesList()
        {
            ServiceProviderFXFees = new clsServiceProviderFXFees();
            ServiceProviderFXFees.ServiceProvider_ID = iID;
            ServiceProviderFXFees.SPO_ID = Convert.ToInt32(fgFXOptions[fgFXOptions.Row, "ID"]);
            ServiceProviderFXFees.GetFees();

            fgFXFees.Redraw = false;
            fgFXFees.Rows.Count = 2;
            if (fgFXOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderFXFees.List.Rows)
                    fgFXFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" +
                        sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" + dtRow["RetrosessionProvider"] + "\t" + dtRow["RetrosessionCompany"] + "\t" +
                        dtRow["ID"]);
            }
            fgFXFees.Cols[0].AllowMerging = true;
            fgFXFees.Redraw = true;

            if (fgFXFees.Rows.Count > 2) toolFXOptions.Enabled = false;
            else toolFXOptions.Enabled = true;
        }
        #endregion
        #region --- Safekeeping functionality ------------------------------------------------------------------------------
        private void tsbAddSafekeepingOption_Click(object sender, EventArgs e)
        {
            iService = 4;                                                                             // 4 - Safekeeping
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            cmbMonthMinCurr.Text = cmbCurrencies.Text;
            txtMinAmount.Text = "0";
            cmbMinCurr.Text = cmbCurrencies.Text;
            txtOpenAmount.Text = "0";
            cmbOpenCurr.Text = cmbCurrencies.Text;
            txtServiceAmount.Text = "0";
            cmbServiceCurr.Text = cmbCurrencies.Text;
            chkAUM.Checked = false;
            chkSecurities.Checked = false;
            chkCash.Checked = false;
            panOnlySafekeeping.Visible = true;
            ShowEditOption();
        }

        private void tsbEditSafekeepingOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 4;                                                                             // 4 - Safekeeping
                iLocAktion = 1;
                txtOption.Text = fgSafekeepingOptions[fgSafekeepingOptions.Row, 0] + "";
                dStart.Value = Convert.ToDateTime(fgSafekeepingOptions[fgSafekeepingOptions.Row, 1]);
                dFinish.Value = Convert.ToDateTime(fgSafekeepingOptions[fgSafekeepingOptions.Row, 2]);
                txtMonthMinAmount.Text = "0";
                iOption_ID = Convert.ToInt32(fgSafekeepingOptions[fgSafekeepingOptions.Row, "ID"]);
                ShowEditOption();
            }
        }

        private void tsbDelSafekeepingOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgSafekeepingOptions[fgSafekeepingOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgSafekeepingOptions.RemoveItem(fgSafekeepingOptions.Row);
            }
        }
        private void fgSafekeepingOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckSafekeepingFees && fgSafekeepingOptions.Rows.Count > 1) DefineSafekeepingFeesList();
        }
        private void tsbSafekeepingAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 0;                      // 0 - ADD
            locServiceProviderFees2.txtAmountFrom.Text = "0";
            locServiceProviderFees2.txtAmountTo.Text = "90000000";
            locServiceProviderFees2.txtFees.Text = "0";
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1)
            {
                ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
                ServiceProviderCustodyFees.SPO_ID = Convert.ToInt32(fgSafekeepingOptions[fgSafekeepingOptions.Row, "ID"]);
                ServiceProviderCustodyFees.ServiceProvider_ID = iID;
                ServiceProviderCustodyFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderCustodyFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderCustodyFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderCustodyFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderCustodyFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderCustodyFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderCustodyFees.InsertRecord();

                AddSafekeepingFees(Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text),
                 Convert.ToSingle(locServiceProviderFees2.txtFees.Text), locServiceProviderFees2.cmbDistribMethods.SelectedIndex,
                 Convert.ToSingle(locServiceProviderFees2.txtProvider.Text), Convert.ToSingle(locServiceProviderFees2.txtCompany.Text), iFees_ID);
            }
        }
        private void tsbSafekeepingEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgSafekeepingFees.Row;

            ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
            ServiceProviderCustodyFees.Record_ID = Convert.ToInt32(fgSafekeepingFees[iRow, "ID"]);
            ServiceProviderCustodyFees.GetRecord();

            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 1;                                                                 // 1 - EDIT
            locServiceProviderFees2.txtAmountFrom.Text = ServiceProviderCustodyFees.AmountFrom + "";
            locServiceProviderFees2.txtAmountTo.Text = ServiceProviderCustodyFees.AmountTo.ToString("0.##");
            locServiceProviderFees2.txtFees.Text = ServiceProviderCustodyFees.FeesPercent + "";
            locServiceProviderFees2.cmbDistribMethods.SelectedIndex = ServiceProviderCustodyFees.RetrosessionMethod;
            locServiceProviderFees2.txtProvider.Text = ServiceProviderCustodyFees.RetrosessionProvider + "";
            locServiceProviderFees2.txtCompany.Text = ServiceProviderCustodyFees.RetrosessionCompany + "";
            locServiceProviderFees2.Mode = 4;                                                                   // 4 - Safekeeping
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1)
            {
                ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
                ServiceProviderCustodyFees.Record_ID = Convert.ToInt32(fgSafekeepingFees[iRow, "ID"]);
                ServiceProviderCustodyFees.GetRecord();
                ServiceProviderCustodyFees.SPO_ID = Convert.ToInt32(fgSafekeepingOptions[fgSafekeepingOptions.Row, "ID"]);
                ServiceProviderCustodyFees.ServiceProvider_ID = iID;
                ServiceProviderCustodyFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderCustodyFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderCustodyFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderCustodyFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderCustodyFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderCustodyFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderCustodyFees.EditRecord();

                DefineSafekeepingFeesList();
                fgSafekeepingFees.Row = iRow;
            }
        }

        private void tsbSafekeepingDelFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
                ServiceProviderCustodyFees.Record_ID = Convert.ToInt32(fgSafekeepingFees[fgSafekeepingFees.Row, "ID"]);
                ServiceProviderCustodyFees.DeleteRecord();

                fgSafekeepingFees.RemoveItem(fgSafekeepingFees.Row);
            }
        }
        private void AddSafekeepingFees(float fltAmountFrom, float fltAmountTo, float fltFees, int iRetrosessionMethod, float fltRetrosessionProvider,
                       float fltRetrosessionCompany, int iRec_ID)
        {
            fgSafekeepingFees.Redraw = false;
            fgSafekeepingFees.AddItem(fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" + sDistrib[iRetrosessionMethod] + "\t" +
                             fltRetrosessionProvider + "\t" + fltRetrosessionCompany + "\t" + iRec_ID);
            fgSafekeepingFees.Redraw = true;
        }
        private void DefineSafekeepingFeesList()
        {
            ServiceProviderCustodyFees = new clsServiceProviderCustodyFees();
            ServiceProviderCustodyFees.ServiceProvider_ID = iID;
            ServiceProviderCustodyFees.SPO_ID = Convert.ToInt32(fgSafekeepingOptions[fgSafekeepingOptions.Row, "ID"]);
            ServiceProviderCustodyFees.GetFees();

            fgSafekeepingFees.Redraw = false;
            fgSafekeepingFees.Rows.Count = 2;
            if (fgSafekeepingOptions.Rows.Count > 2)
            {
                foreach (DataRow dtRow in ServiceProviderCustodyFees.List.Rows)
                    fgSafekeepingFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" +
                                 sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" + dtRow["RetrosessionProvider"] + "\t" +
                                 dtRow["RetrosessionCompany"] + "\t" + dtRow["ID"] + "\t" + dtRow["RetrosessionMethod"]);
            }
            fgSafekeepingFees.Cols[0].AllowMerging = true;
            fgSafekeepingFees.Redraw = true;

            if (fgSafekeepingFees.Rows.Count > 2) tsbDelSafekeepingOption.Enabled = false;
            else tsbDelSafekeepingOption.Enabled = true;
        }
        #endregion
        #region --- Advisory functionality ---------------------------------------------------------------------------------
        private void tsbAddAdvisoryOption_Click(object sender, EventArgs e)
        {
            iService = 2;                                                        // 2 - Advisory
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            cmbMonthMinCurr.Text = cmbCurrencies.Text;
            txtMinAmount.Text = "0";
            cmbMinCurr.Text = cmbCurrencies.Text;
            txtOpenAmount.Text = "0";
            cmbOpenCurr.Text = cmbCurrencies.Text;
            txtServiceAmount.Text = "0";
            cmbServiceCurr.Text = cmbCurrencies.Text;
            ShowEditOption();
        }
        private void tsbEditAdvisoryOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 2;                                                  // 2 - Advisory
                iLocAktion = 1;
                i = fgAdvisoryOptions.Row;
                txtOption.Text = fgAdvisoryOptions[i, 0] + "";
                dStart.Value = Convert.ToDateTime(fgAdvisoryOptions[i, 1]);
                dFinish.Value = Convert.ToDateTime(fgAdvisoryOptions[i, 2]);
                txtMonthMinAmount.Text = fgAdvisoryOptions[i, 3] + "";
                cmbMonthMinCurr.Text = fgAdvisoryOptions[i, 4] + "";
                txtMinAmount.Text = fgAdvisoryOptions[i, 5] + "";
                cmbMinCurr.Text = fgAdvisoryOptions[i, 6] + "";
                txtOpenAmount.Text = fgAdvisoryOptions[i, 7] + "";
                cmbOpenCurr.Text = fgAdvisoryOptions[i, 8] + "";
                txtServiceAmount.Text = fgAdvisoryOptions[i, 9] + "";
                cmbServiceCurr.Text = fgAdvisoryOptions[i, 10] + "";
                iOption_ID = Convert.ToInt32(fgAdvisoryOptions[i, 11]);
                ShowEditOption();
            }
        }
        private void tsbDelAdvisoryOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgAdvisoryOptions[fgAdvisoryOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgAdvisoryOptions.RemoveItem(fgAdvisoryOptions.Row);
            }
        }
        private void fgAdvisoryOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckAdvisoryFees && fgAdvisoryOptions.Rows.Count > 2) DefineAdvisoryFeesList();
        }
        private void tsbAdvisoryAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees3 locServiceProviderFees3 = new frmServiceProviderFees3();
            locServiceProviderFees3.Aktion = 0;                                                          // 0 - ADD
            locServiceProviderFees3.txtAmountFrom.Text = "0";
            locServiceProviderFees3.txtAmountTo.Text = "90000000";
            locServiceProviderFees3.txtFees.Text = "0";
            locServiceProviderFees3.txtYperReturn.Text = "0";
            locServiceProviderFees3.txtVariable1.Text = "";
            locServiceProviderFees3.txtVariable2.Text = "0";
            locServiceProviderFees3.ShowDialog();
            if (locServiceProviderFees3.Aktion == 1)
            {
                ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
                ServiceProviderAdvisoryFees.SPO_ID = Convert.ToInt32(fgAdvisoryOptions[fgAdvisoryOptions.Row, "ID"]);
                ServiceProviderAdvisoryFees.ServiceProvider_ID = iID;
                ServiceProviderAdvisoryFees.InvestmentProfile_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue);
                ServiceProviderAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue);
                ServiceProviderAdvisoryFees.AmountFrom = Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text);
                ServiceProviderAdvisoryFees.AmountTo = Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text);
                ServiceProviderAdvisoryFees.FeesPercent = Convert.ToSingle(locServiceProviderFees3.txtFees.Text);
                ServiceProviderAdvisoryFees.YperReturn = Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text);
                ServiceProviderAdvisoryFees.Variable1 = locServiceProviderFees3.txtVariable1.Text + "";
                ServiceProviderAdvisoryFees.Variable2 = Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text);
                iFees_ID = ServiceProviderAdvisoryFees.InsertRecord();

                AddAdvisoryFees(locServiceProviderFees3.cmbInvestmentProfile.Text, locServiceProviderFees3.cmbInvestmentPolicy.Text, 
                                  Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text),
                                  Convert.ToSingle(locServiceProviderFees3.txtFees.Text), Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text),
                                  locServiceProviderFees3.txtVariable1.Text, Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text), iFees_ID,
                                  Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue), Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue));
            }
        }

        private void tsbAdvisoryEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgAdvisoryFees.Row;

            ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
            ServiceProviderAdvisoryFees.Record_ID = Convert.ToInt32(fgAdvisoryFees[iRow, "ID"]);
            ServiceProviderAdvisoryFees.GetRecord();

            frmServiceProviderFees3 locServiceProviderFees3 = new frmServiceProviderFees3();
            locServiceProviderFees3.Aktion = 1;                                                                               // 1 - EDIT
            locServiceProviderFees3.InvestmentProfile_ID = ServiceProviderAdvisoryFees.InvestmentProfile_ID;
            locServiceProviderFees3.InvestmentPolicy_ID = ServiceProviderAdvisoryFees.InvestmentPolicy_ID;
            locServiceProviderFees3.txtAmountFrom.Text = ServiceProviderAdvisoryFees.AmountFrom + "";
            locServiceProviderFees3.txtAmountTo.Text = ServiceProviderAdvisoryFees.AmountTo.ToString("0.##");
            locServiceProviderFees3.txtFees.Text = ServiceProviderAdvisoryFees.FeesPercent + "";
            locServiceProviderFees3.txtYperReturn.Text = ServiceProviderAdvisoryFees.YperReturn + "";
            locServiceProviderFees3.txtVariable1.Text = ServiceProviderAdvisoryFees.Variable1 + "";
            locServiceProviderFees3.txtVariable2.Text = ServiceProviderAdvisoryFees.Variable2 + "";
            locServiceProviderFees3.ShowDialog();
            if (locServiceProviderFees3.Aktion == 1)
            {
                ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
                ServiceProviderAdvisoryFees.Record_ID = Convert.ToInt32(fgAdvisoryFees[iRow, "ID"]);
                ServiceProviderAdvisoryFees.GetRecord();
                ServiceProviderAdvisoryFees.SPO_ID = Convert.ToInt32(fgAdvisoryOptions[fgAdvisoryOptions.Row, "ID"]);
                ServiceProviderAdvisoryFees.ServiceProvider_ID = iID;
                ServiceProviderAdvisoryFees.InvestmentProfile_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue);
                ServiceProviderAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue);
                ServiceProviderAdvisoryFees.AmountFrom = Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text);
                ServiceProviderAdvisoryFees.AmountTo = Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text);
                ServiceProviderAdvisoryFees.FeesPercent = Convert.ToSingle(locServiceProviderFees3.txtFees.Text);
                ServiceProviderAdvisoryFees.YperReturn = Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text);
                ServiceProviderAdvisoryFees.Variable1 = locServiceProviderFees3.txtVariable1.Text;
                ServiceProviderAdvisoryFees.Variable2 = Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text);
                iFees_ID = ServiceProviderAdvisoryFees.EditRecord();

                DefineAdvisoryFeesList();
                fgAdvisoryFees.Row = iRow;
            }
        }

        private void tsbAdvisoryDelFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
                ServiceProviderAdvisoryFees.Record_ID = Convert.ToInt32(fgAdvisoryFees[fgAdvisoryFees.Row, "ID"]);
                ServiceProviderAdvisoryFees.DeleteRecord();

                fgAdvisoryFees.RemoveItem(fgAdvisoryFees.Row);
            }
        }
        private void DefineAdvisoryFeesList()
        {

            ServiceProviderAdvisoryFees = new clsServiceProviderAdvisoryFees();
            ServiceProviderAdvisoryFees.ServiceProvider_ID = iID;
            ServiceProviderAdvisoryFees.SPO_ID = Convert.ToInt32(fgAdvisoryOptions[fgAdvisoryOptions.Row, "ID"]);
            ServiceProviderAdvisoryFees.GetFees();

            fgAdvisoryFees.Redraw = false;
            fgAdvisoryFees.Rows.Count = 2;
            if (fgAdvisoryOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderAdvisoryFees.List.Rows)
                    fgAdvisoryFees.AddItem(dtRow["InvestmentProfile_Title"] + "\t" + dtRow["InvestmentPolicy_Title"] + "\t" + dtRow["AmountFrom"] + "\t" +
                                     dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" + dtRow["YperReturn"] + "\t" + dtRow["Variable1"] + "\t" +
                                      dtRow["Variable2"] + "\t" + dtRow["ID"] + "\t" + dtRow["InvestmentProfile_ID"] + "\t" + dtRow["InvestmentPolicy_ID"]);
            }
            fgAdvisoryFees.Cols[0].AllowMerging = true;
            fgAdvisoryFees.Redraw = true;

            if (fgAdvisoryFees.Rows.Count > 2) tsbDelAdvisoryOption.Enabled = false;
            else tsbDelAdvisoryOption.Enabled = true;
        }
        private void AddAdvisoryFees(string sInvestmentProfile_Title, string sInvestmentPolicy_Title, float fltAmountFrom, float fltAmountTo, float fltFees, 
                                     float fltYperReturn, string sVariable1, float fltVariable2, int iRec_ID, int iInvestmentProfile_ID, int iInvestmentPolicy_ID)
        {
            fgAdvisoryFees.Redraw = false;
            fgAdvisoryFees.AddItem(sInvestmentProfile_Title + "\t" + sInvestmentPolicy_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" +
                                  fltYperReturn + "\t" + sVariable1 + "\t" + fltVariable2 + "\t" + iRec_ID + "\t" + iInvestmentProfile_ID + "\t" + iInvestmentPolicy_ID);
            fgAdvisoryFees.Redraw = true;
        }
        #endregion
        #region --- Discret functionality ----------------------------------------------------------------------------------

        private void tsbAddDiscretOption_Click(object sender, EventArgs e)
        {
            iService = 3;                                                                // 3 - Discret
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            cmbMonthMinCurr.Text = cmbCurrencies.Text;
            txtMinAmount.Text = "0";
            cmbMinCurr.Text = cmbCurrencies.Text;
            txtOpenAmount.Text = "0";
            cmbOpenCurr.Text = cmbCurrencies.Text;
            txtServiceAmount.Text = "0";
            cmbServiceCurr.Text = cmbCurrencies.Text;
            ShowEditOption();
        }

        private void tsbEditDiscretOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 3;                                                                // 3 - Discret
                iLocAktion = 1;
                i = fgDiscretOptions.Row;
                txtOption.Text = fgDiscretOptions[i, 0] + "";
                dStart.Value = Convert.ToDateTime(fgDiscretOptions[i, 1]);
                dFinish.Value = Convert.ToDateTime(fgDiscretOptions[i, 2]);
                txtMonthMinAmount.Text = fgDiscretOptions[i, 3] + "";
                cmbMonthMinCurr.Text = fgDiscretOptions[i, 4] + "";
                txtMinAmount.Text = fgDiscretOptions[i, 5] + "";
                cmbMinCurr.Text = fgDiscretOptions[i, 6] + "";
                txtOpenAmount.Text = fgDiscretOptions[i, 7] + "";
                cmbOpenCurr.Text = fgDiscretOptions[i, 8] + "";
                txtServiceAmount.Text = fgDiscretOptions[i, 9] + "";
                cmbServiceCurr.Text = fgDiscretOptions[i, 10] + "";
                iOption_ID = Convert.ToInt32(fgDiscretOptions[i, 11]);
                ShowEditOption();
            }
        }

        private void tsbDelDiscretOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgDiscretOptions[fgDiscretOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgDiscretOptions.RemoveItem(fgDiscretOptions.Row);
            }
        }
        private void fgDiscretOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckDiscretFees && fgDiscretOptions.Rows.Count > 2) DefineDiscretFeesList();
        }
        private void tsbDiscretAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees3 locServiceProviderFees3 = new frmServiceProviderFees3();
            locServiceProviderFees3.Aktion = 0;                                                          // 0 - ADD
            locServiceProviderFees3.txtAmountFrom.Text = "0";
            locServiceProviderFees3.txtAmountTo.Text = "90000000";
            locServiceProviderFees3.txtFees.Text = "0";
            locServiceProviderFees3.txtYperReturn.Text = "0";
            locServiceProviderFees3.txtVariable1.Text = "";
            locServiceProviderFees3.txtVariable2.Text = "0";
            locServiceProviderFees3.ShowDialog();
            if (locServiceProviderFees3.Aktion == 1)
            {
                ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
                ServiceProviderDiscretFees.SPO_ID = Convert.ToInt32(fgDiscretOptions[fgDiscretOptions.Row, "ID"]);
                ServiceProviderDiscretFees.ServiceProvider_ID = iID;
                ServiceProviderDiscretFees.InvestmentProfile_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue);
                ServiceProviderDiscretFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue);
                ServiceProviderDiscretFees.AmountFrom = Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text);
                ServiceProviderDiscretFees.AmountTo = Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text);
                ServiceProviderDiscretFees.FeesPercent = Convert.ToSingle(locServiceProviderFees3.txtFees.Text);
                ServiceProviderDiscretFees.YperReturn = Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text);
                ServiceProviderDiscretFees.Variable1 = locServiceProviderFees3.txtVariable1.Text + "";
                ServiceProviderDiscretFees.Variable2 = Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text);
                iFees_ID = ServiceProviderDiscretFees.InsertRecord();

                AddDiscretFees(locServiceProviderFees3.cmbInvestmentProfile.Text, locServiceProviderFees3.cmbInvestmentPolicy.Text,
                                  Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text),
                                  Convert.ToSingle(locServiceProviderFees3.txtFees.Text), Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text),
                                  locServiceProviderFees3.txtVariable1.Text, Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text), iFees_ID,
                                  Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue), Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue));
            }
        }

        private void tsbDiscretEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgDiscretFees.Row;

            ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
            ServiceProviderDiscretFees.Record_ID = Convert.ToInt32(fgDiscretFees[iRow, "ID"]);
            ServiceProviderDiscretFees.GetRecord();

            frmServiceProviderFees3 locServiceProviderFees3 = new frmServiceProviderFees3();
            locServiceProviderFees3.Aktion = 1;                                                                               // 1 - EDIT
            locServiceProviderFees3.InvestmentProfile_ID = ServiceProviderDiscretFees.InvestmentProfile_ID;
            locServiceProviderFees3.InvestmentPolicy_ID = ServiceProviderDiscretFees.InvestmentPolicy_ID;
            locServiceProviderFees3.txtAmountFrom.Text = ServiceProviderDiscretFees.AmountFrom + "";
            locServiceProviderFees3.txtAmountTo.Text = ServiceProviderDiscretFees.AmountTo.ToString("0.##");
            locServiceProviderFees3.txtFees.Text = ServiceProviderDiscretFees.FeesPercent + "";
            locServiceProviderFees3.txtYperReturn.Text = ServiceProviderDiscretFees.YperReturn + "";
            locServiceProviderFees3.txtVariable1.Text = ServiceProviderDiscretFees.Variable1 + "";
            locServiceProviderFees3.txtVariable2.Text = ServiceProviderDiscretFees.Variable2 + "";
            locServiceProviderFees3.ShowDialog();
            if (locServiceProviderFees3.Aktion == 1)
            {
                ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
                ServiceProviderDiscretFees.Record_ID = Convert.ToInt32(fgDiscretFees[iRow, "ID"]);
                ServiceProviderDiscretFees.GetRecord();
                ServiceProviderDiscretFees.SPO_ID = Convert.ToInt32(fgDiscretOptions[fgDiscretOptions.Row, "ID"]);
                ServiceProviderDiscretFees.ServiceProvider_ID = iID;
                ServiceProviderDiscretFees.InvestmentProfile_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentProfile.SelectedValue);
                ServiceProviderDiscretFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees3.cmbInvestmentPolicy.SelectedValue);
                ServiceProviderDiscretFees.AmountFrom = Convert.ToSingle(locServiceProviderFees3.txtAmountFrom.Text);
                ServiceProviderDiscretFees.AmountTo = Convert.ToSingle(locServiceProviderFees3.txtAmountTo.Text);
                ServiceProviderDiscretFees.FeesPercent = Convert.ToSingle(locServiceProviderFees3.txtFees.Text);
                ServiceProviderDiscretFees.YperReturn = Convert.ToSingle(locServiceProviderFees3.txtYperReturn.Text);
                ServiceProviderDiscretFees.Variable1 = locServiceProviderFees3.txtVariable1.Text;
                ServiceProviderDiscretFees.Variable2 = Convert.ToSingle(locServiceProviderFees3.txtVariable2.Text);
                iFees_ID = ServiceProviderDiscretFees.EditRecord();

                DefineDiscretFeesList();
                fgDiscretFees.Row = iRow;
            }
        }

        private void tsbDiscretDelFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
                ServiceProviderDiscretFees.Record_ID = Convert.ToInt32(fgDiscretFees[fgDiscretFees.Row, "ID"]);
                ServiceProviderDiscretFees.DeleteRecord();

                fgDiscretFees.RemoveItem(fgDiscretFees.Row);
            }
        }
        private void DefineDiscretFeesList()
        {

            ServiceProviderDiscretFees = new clsServiceProviderDiscretFees();
            ServiceProviderDiscretFees.ServiceProvider_ID = iID;
            ServiceProviderDiscretFees.SPO_ID = Convert.ToInt32(fgDiscretOptions[fgDiscretOptions.Row, "ID"]);
            ServiceProviderDiscretFees.GetFees();

            fgDiscretFees.Redraw = false;
            fgDiscretFees.Rows.Count = 2;
            if (fgDiscretOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderDiscretFees.List.Rows)
                    fgDiscretFees.AddItem(dtRow["InvestmentProfile_Title"] + "\t" + dtRow["InvestmentPolicy_Title"] + "\t" + dtRow["AmountFrom"] + "\t" +
                                     dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" + dtRow["YperReturn"] + "\t" + dtRow["Variable1"] + "\t" +
                                      dtRow["Variable2"] + "\t" + dtRow["ID"] + "\t" + dtRow["InvestmentProfile_ID"] + "\t" + dtRow["InvestmentPolicy_ID"]);
            }
            fgDiscretFees.Cols[0].AllowMerging = true;
            fgDiscretFees.Redraw = true;

            if (fgDiscretFees.Rows.Count > 2) tsbDelDiscretOption.Enabled = false;
            else tsbDelDiscretOption.Enabled = true;
        }
        private void AddDiscretFees(string sInvestmentProfile_Title, string sInvestmentPolicy_Title, float fltAmountFrom, float fltAmountTo, float fltFees,
                                     float fltYperReturn, string sVariable1, float fltVariable2, int iRec_ID, int iInvestmentProfile_ID, int iInvestmentPolicy_ID)
        {
            fgDiscretFees.Redraw = false;
            fgDiscretFees.AddItem(sInvestmentProfile_Title + "\t" + sInvestmentPolicy_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" +
                                  fltYperReturn + "\t" + sVariable1 + "\t" + fltVariable2 + "\t" + iRec_ID + "\t" + iInvestmentProfile_ID + "\t" + iInvestmentPolicy_ID);
            fgDiscretFees.Redraw = true;
        }
        #endregion
        #region --- DealAdvisory functionality -----------------------------------------------------------------------------
        private void tsbAddDealAdvisoryOption_Click(object sender, EventArgs e)
        {
            iService = 5;                                                            // 5 - DealAdvisory
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            cmbMonthMinCurr.Text = cmbCurrencies.Text;
            txtMinAmount.Text = "0";
            cmbMinCurr.Text = cmbCurrencies.Text;
            txtOpenAmount.Text = "0";
            cmbOpenCurr.Text = cmbCurrencies.Text;
            txtServiceAmount.Text = "0";
            cmbServiceCurr.Text = cmbCurrencies.Text;
            ShowEditOption();
        }

        private void tsbEditDealAdvisoryOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 5;                                                        // 5 - DealAdvisory
                iLocAktion = 1;
                i = fgDealAdvisoryOptions.Row;
                txtOption.Text = fgDealAdvisoryOptions[i, 0] + "";
                dStart.Value = Convert.ToDateTime(fgDealAdvisoryOptions[i, 1]);
                dFinish.Value = Convert.ToDateTime(fgDealAdvisoryOptions[i, 2]);
                txtMonthMinAmount.Text = fgDealAdvisoryOptions[i, 3] + "";
                cmbMonthMinCurr.Text = fgDealAdvisoryOptions[i, 4] + "";
                txtMinAmount.Text = fgDealAdvisoryOptions[i, 5] + "";
                cmbMinCurr.Text = fgDealAdvisoryOptions[i, 6] + "";
                txtOpenAmount.Text = fgDealAdvisoryOptions[i, 7] + "";
                cmbOpenCurr.Text = fgDealAdvisoryOptions[i, 8] + "";
                txtServiceAmount.Text = fgDealAdvisoryOptions[i, 9] + "";
                cmbServiceCurr.Text = fgDealAdvisoryOptions[i, 10] + "";
                iOption_ID = Convert.ToInt32(fgDealAdvisoryOptions[i, 11]);
                ShowEditOption();
            }
        }

        private void tsbDelDealAdvisoryOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgDealAdvisoryOptions[fgDealAdvisoryOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgDealAdvisoryOptions.RemoveItem(fgDealAdvisoryOptions.Row);
            }
        }
        private void fgDealAdvisoryOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckDealAdvisoryFees && fgDealAdvisoryOptions.Rows.Count > 2) DefineDealAdvisoryFeesList();
        }
        private void tsbDealAdvisoryAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees4 locServiceProviderFees4 = new frmServiceProviderFees4();
            locServiceProviderFees4.Aktion = 0;                                                          // 0 - ADD
            locServiceProviderFees4.txtAmountFrom.Text = "0";
            locServiceProviderFees4.txtAmountTo.Text = "90000000";
            locServiceProviderFees4.txtFees.Text = "0";
            locServiceProviderFees4.txtYperReturn.Text = "0";
            locServiceProviderFees4.txtVariable1.Text = "";
            locServiceProviderFees4.txtVariable2.Text = "0";
            locServiceProviderFees4.ShowDialog();
            if (locServiceProviderFees4.Aktion == 1)
            {
                ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
                ServiceProviderDealAdvisoryFees.SPO_ID = Convert.ToInt32(fgDealAdvisoryOptions[fgDealAdvisoryOptions.Row, "ID"]);
                ServiceProviderDealAdvisoryFees.ServiceProvider_ID = iID;
                ServiceProviderDealAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees4.cmbFinanceTools.SelectedValue);
                ServiceProviderDealAdvisoryFees.AmountFrom = Convert.ToSingle(locServiceProviderFees4.txtAmountFrom.Text);
                ServiceProviderDealAdvisoryFees.AmountTo = Convert.ToSingle(locServiceProviderFees4.txtAmountTo.Text);
                ServiceProviderDealAdvisoryFees.FeesAmount = Convert.ToSingle(locServiceProviderFees4.txtFees.Text);
                ServiceProviderDealAdvisoryFees.FeesCurr = locServiceProviderFees4.cmbFeesCurrencies.Text + "";
                ServiceProviderDealAdvisoryFees.YperReturn = Convert.ToSingle(locServiceProviderFees4.txtYperReturn.Text);
                ServiceProviderDealAdvisoryFees.Variable1 = locServiceProviderFees4.txtVariable1.Text + "";
                ServiceProviderDealAdvisoryFees.Variable2 = Convert.ToSingle(locServiceProviderFees4.txtVariable2.Text);
                iFees_ID = ServiceProviderDealAdvisoryFees.InsertRecord();

                AddDealAdvisoryFees(locServiceProviderFees4.cmbFinanceTools.Text, Convert.ToSingle(locServiceProviderFees4.txtAmountFrom.Text), 
                                  Convert.ToSingle(locServiceProviderFees4.txtAmountTo.Text), Convert.ToSingle(locServiceProviderFees4.txtFees.Text),
                                  locServiceProviderFees4.cmbFeesCurrencies.Text, Convert.ToSingle(locServiceProviderFees4.txtYperReturn.Text), 
                                  locServiceProviderFees4.txtVariable1.Text, Convert.ToSingle(locServiceProviderFees4.txtVariable2.Text), iFees_ID, 
                                  Convert.ToInt32(locServiceProviderFees4.cmbFinanceTools.SelectedValue));
            }
        }

        private void tsbDealAdvisoryEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgDealAdvisoryFees.Row;

            ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
            ServiceProviderDealAdvisoryFees.Record_ID = Convert.ToInt32(fgDealAdvisoryFees[iRow, "ID"]);
            ServiceProviderDealAdvisoryFees.GetRecord();

            frmServiceProviderFees4 locServiceProviderFees4 = new frmServiceProviderFees4();
            locServiceProviderFees4.Aktion = 1;                                                                               // 1 - EDIT
            locServiceProviderFees4.FinanceTools_ID = ServiceProviderDealAdvisoryFees.InvestmentPolicy_ID;
            locServiceProviderFees4.txtAmountFrom.Text = ServiceProviderDealAdvisoryFees.AmountFrom + "";
            locServiceProviderFees4.txtAmountTo.Text = ServiceProviderDealAdvisoryFees.AmountTo.ToString("0.##");
            locServiceProviderFees4.txtFees.Text = ServiceProviderDealAdvisoryFees.FeesAmount.ToString("0.##");
            locServiceProviderFees4.FeesCurrency = ServiceProviderDealAdvisoryFees.FeesCurr + "";
            locServiceProviderFees4.txtYperReturn.Text = ServiceProviderDealAdvisoryFees.YperReturn + "";
            locServiceProviderFees4.txtVariable1.Text = ServiceProviderDealAdvisoryFees.Variable1 + "";
            locServiceProviderFees4.txtVariable2.Text = ServiceProviderDealAdvisoryFees.Variable2 + "";
            locServiceProviderFees4.ShowDialog();
            if (locServiceProviderFees4.Aktion == 1)
            {
                ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
                ServiceProviderDealAdvisoryFees.Record_ID = Convert.ToInt32(fgDealAdvisoryFees[iRow, "ID"]);
                ServiceProviderDealAdvisoryFees.GetRecord();
                ServiceProviderDealAdvisoryFees.SPO_ID = Convert.ToInt32(fgDealAdvisoryOptions[fgDealAdvisoryOptions.Row, "ID"]);
                ServiceProviderDealAdvisoryFees.ServiceProvider_ID = iID;
                ServiceProviderDealAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(locServiceProviderFees4.cmbFinanceTools.SelectedValue);
                ServiceProviderDealAdvisoryFees.AmountFrom = Convert.ToSingle(locServiceProviderFees4.txtAmountFrom.Text);
                ServiceProviderDealAdvisoryFees.AmountTo = Convert.ToSingle(locServiceProviderFees4.txtAmountTo.Text);
                ServiceProviderDealAdvisoryFees.FeesAmount = Convert.ToSingle(locServiceProviderFees4.txtFees.Text);
                ServiceProviderDealAdvisoryFees.FeesCurr = locServiceProviderFees4.cmbFeesCurrencies.Text + "";
                ServiceProviderDealAdvisoryFees.YperReturn = Convert.ToSingle(locServiceProviderFees4.txtYperReturn.Text);
                ServiceProviderDealAdvisoryFees.Variable1 = locServiceProviderFees4.txtVariable1.Text;
                ServiceProviderDealAdvisoryFees.Variable2 = Convert.ToSingle(locServiceProviderFees4.txtVariable2.Text);
                iFees_ID = ServiceProviderDealAdvisoryFees.EditRecord();

                DefineDealAdvisoryFeesList();
                fgDealAdvisoryFees.Row = iRow;
            }
        }

        private void tsbDealAdvisoryDelFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
                ServiceProviderDealAdvisoryFees.Record_ID = Convert.ToInt32(fgDealAdvisoryFees[fgDealAdvisoryFees.Row, "ID"]);
                ServiceProviderDealAdvisoryFees.DeleteRecord();

                fgDealAdvisoryFees.RemoveItem(fgDealAdvisoryFees.Row);
            }
        }
        private void DefineDealAdvisoryFeesList()
        {
            ServiceProviderDealAdvisoryFees = new clsServiceProviderDealAdvisoryFees();
            ServiceProviderDealAdvisoryFees.ServiceProvider_ID = iID;
            ServiceProviderDealAdvisoryFees.SPO_ID = Convert.ToInt32(fgDealAdvisoryOptions[fgDealAdvisoryOptions.Row, "ID"]);
            ServiceProviderDealAdvisoryFees.GetFees();

            fgDealAdvisoryFees.Redraw = false;
            fgDealAdvisoryFees.Rows.Count = 2;
            if (fgDealAdvisoryOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderDealAdvisoryFees.List.Rows)
                    fgDealAdvisoryFees.AddItem(dtRow["InvestmentPolicy_Title"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + 
                                               dtRow["FeesAmount"] + "\t" + dtRow["FeesCurr"] + "\t" + dtRow["YperReturn"] + "\t" + dtRow["Variable1"] + "\t" +
                                               dtRow["Variable2"] + "\t" + dtRow["ID"] + "\t" + dtRow["InvestmentPolicy_ID"]);
            }
            fgDealAdvisoryFees.Cols[0].AllowMerging = true;
            fgDealAdvisoryFees.Redraw = true;

            if (fgDealAdvisoryFees.Rows.Count > 2) tsbDelDealAdvisoryOption.Enabled = false;
            else tsbDelDealAdvisoryOption.Enabled = true;
        }
        private void AddDealAdvisoryFees(string sInvestmentPolicy_Title, float fltAmountFrom, float fltAmountTo, float fltFees, string sFeesCurr,
                                     float fltYperReturn, string sVariable1, float fltVariable2, int iRec_ID, int iInvestmentPolicy_ID)
        {
            fgDealAdvisoryFees.Redraw = false;
            fgDealAdvisoryFees.AddItem(sInvestmentPolicy_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" + sFeesCurr + "\t" +
                                  fltYperReturn + "\t" + sVariable1 + "\t" + fltVariable2 + "\t" + iRec_ID + "\t" + iInvestmentPolicy_ID);
            fgDealAdvisoryFees.Redraw = true;
        }
        #endregion
        #region --- Administration functionality ---------------------------------------------------------------------------
        private void tsbAddAdministrationOption_Click(object sender, EventArgs e)
        {
            iService = 10;                                                                     // 10 - Administration Fees
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            cmbMonthMinCurr.Text = cmbCurrencies.Text;
            txtMinAmount.Text = "0";
            cmbMinCurr.Text = cmbCurrencies.Text;
            txtOpenAmount.Text = "0";
            cmbOpenCurr.Text = cmbCurrencies.Text;
            txtServiceAmount.Text = "0";
            cmbServiceCurr.Text = cmbCurrencies.Text;
            chkAUM.Checked = false;
            chkSecurities.Checked = false;
            chkCash.Checked = false;
            panOnlySafekeeping.Visible = true;
            ShowEditOption();
        }
        private void tsbEditAdministrationOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 10;                                                                     // 10 - Administration Fees
                iLocAktion = 1;
                i = fgAdministrationOptions.Row;
                txtOption.Text = fgAdministrationOptions[i, 0] + "";
                dStart.Value = Convert.ToDateTime(fgAdministrationOptions[i, 1]);
                dFinish.Value = Convert.ToDateTime(fgAdministrationOptions[i, 2]);
                txtMonthMinAmount.Text = fgAdministrationOptions[i, 3] + "";
                cmbMonthMinCurr.Text = fgAdministrationOptions[i, 4] + "";
                txtMinAmount.Text = fgAdministrationOptions[i, 5] + "";
                cmbMinCurr.Text = fgAdministrationOptions[i, 6] + "";
                txtOpenAmount.Text = fgAdministrationOptions[i, 7] + "";
                cmbOpenCurr.Text = fgAdministrationOptions[i, 8] + "";
                txtServiceAmount.Text = fgAdministrationOptions[i, 9] + "";
                cmbServiceCurr.Text = fgAdministrationOptions[i, 10] + "";
                chkAUM.Checked = Convert.ToBoolean(fgAdministrationOptions[i, 11]);
                chkSecurities.Checked = Convert.ToBoolean(fgAdministrationOptions[i, 12]);
                chkCash.Checked = Convert.ToBoolean(fgAdministrationOptions[i, 13]);
                panOnlySafekeeping.Visible = true;
                iOption_ID = Convert.ToInt32(fgAdministrationOptions[i, 14]);
                ShowEditOption();
            }
        }
        private void tsbDelAdministrationOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgAdministrationOptions[fgAdministrationOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgAdministrationOptions.RemoveItem(fgAdministrationOptions.Row);
            }
        }
        private void fgAdministrationOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckAdministrationFees && fgAdministrationOptions.Rows.Count > 2) DefineAdministrationFeesList();
        }

        private void tsbAdministrationAddFees_Click(object sender, EventArgs e)
        {
            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 0;                                                   // 0 - ADD
            locServiceProviderFees2.txtAmountFrom.Text = "0";
            locServiceProviderFees2.txtAmountTo.Text = "90000000";
            locServiceProviderFees2.txtFees.Text = "0";
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1)
            {
                ServiceProviderAdminFees = new clsServiceProviderAdminFees();
                ServiceProviderAdminFees.SPO_ID = Convert.ToInt32(fgAdministrationOptions[fgAdministrationOptions.Row, "ID"]);
                ServiceProviderAdminFees.ServiceProvider_ID = iID;
                ServiceProviderAdminFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderAdminFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderAdminFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderAdminFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderAdminFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderAdminFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderAdminFees.InsertRecord();

                AddAdministrationFees(Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text),
                 Convert.ToSingle(locServiceProviderFees2.txtFees.Text), locServiceProviderFees2.cmbDistribMethods.SelectedIndex,
                 Convert.ToSingle(locServiceProviderFees2.txtProvider.Text), Convert.ToSingle(locServiceProviderFees2.txtCompany.Text), iFees_ID);
            }
        }

        private void tsbAdministrationEditFees_Click(object sender, EventArgs e)
        {
            iRow = fgAdministrationFees.Row;

            ServiceProviderAdminFees = new clsServiceProviderAdminFees();
            ServiceProviderAdminFees.Record_ID = Convert.ToInt32(fgAdministrationFees[iRow, "ID"]);            
            ServiceProviderAdminFees.GetRecord();

            frmServiceProviderFees2 locServiceProviderFees2 = new frmServiceProviderFees2();
            locServiceProviderFees2.Aktion = 1;                                                                 // 1 - EDIT
            locServiceProviderFees2.txtAmountFrom.Text = ServiceProviderAdminFees.AmountFrom + "";
            locServiceProviderFees2.txtAmountTo.Text = ServiceProviderAdminFees.AmountTo.ToString("0.##");
            locServiceProviderFees2.txtFees.Text = ServiceProviderAdminFees.FeesPercent + "";
            locServiceProviderFees2.cmbDistribMethods.SelectedIndex = ServiceProviderAdminFees.RetrosessionMethod;
            locServiceProviderFees2.txtProvider.Text = ServiceProviderAdminFees.RetrosessionProvider + "";
            locServiceProviderFees2.txtCompany.Text = ServiceProviderAdminFees.RetrosessionCompany + "";
            locServiceProviderFees2.Mode = 10;                                                                   // 10 - Admin
            locServiceProviderFees2.ShowDialog();
            if (locServiceProviderFees2.Aktion == 1)
            {
                ServiceProviderAdminFees = new clsServiceProviderAdminFees();
                ServiceProviderAdminFees.Record_ID = Convert.ToInt32(fgAdministrationFees[iRow, "ID"]);
                ServiceProviderAdminFees.GetRecord();
                ServiceProviderAdminFees.SPO_ID = Convert.ToInt32(fgAdministrationOptions[fgAdministrationOptions.Row, "ID"]);
                ServiceProviderAdminFees.ServiceProvider_ID = iID;
                ServiceProviderAdminFees.AmountFrom = Convert.ToSingle(locServiceProviderFees2.txtAmountFrom.Text);
                ServiceProviderAdminFees.AmountTo = Convert.ToSingle(locServiceProviderFees2.txtAmountTo.Text);
                ServiceProviderAdminFees.FeesPercent = Convert.ToSingle(locServiceProviderFees2.txtFees.Text);
                ServiceProviderAdminFees.RetrosessionMethod = Convert.ToInt32(locServiceProviderFees2.cmbDistribMethods.SelectedIndex);
                ServiceProviderAdminFees.RetrosessionProvider = Convert.ToSingle(locServiceProviderFees2.txtProvider.Text);
                ServiceProviderAdminFees.RetrosessionCompany = Convert.ToSingle(locServiceProviderFees2.txtCompany.Text);
                iFees_ID = ServiceProviderAdminFees.EditRecord();

                DefineAdministrationFeesList();
                fgAdministrationFees.Row = iRow;
            }
        }
        private void tsbAdministrationDelFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderAdminFees = new clsServiceProviderAdminFees();
                ServiceProviderAdminFees.Record_ID = Convert.ToInt32(fgAdministrationFees[fgAdministrationFees.Row, "ID"]);
                ServiceProviderAdminFees.DeleteRecord();

                fgAdministrationFees.RemoveItem(fgAdministrationFees.Row);
            }
        }
        private void DefineAdministrationFeesList()
        {
            fgAdministrationFees.Redraw = false;
            fgAdministrationFees.Rows.Count = 2;
            if (fgAdministrationOptions.Rows.Count > 2)
            {
                ServiceProviderAdminFees = new clsServiceProviderAdminFees();
                ServiceProviderAdminFees.ServiceProvider_ID = iID;
                ServiceProviderAdminFees.SPO_ID = Convert.ToInt32(fgAdministrationOptions[fgAdministrationOptions.Row, "ID"]);
                ServiceProviderAdminFees.GetFees();

                foreach (DataRow dtRow in ServiceProviderAdminFees.List.Rows)
                    fgAdministrationFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" +
                                  sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" + dtRow["RetrosessionProvider"] + "\t" +
                                  dtRow["RetrosessionCompany"] + "\t" + dtRow["ID"] + "\t" + dtRow["RetrosessionMethod"]);
            }
            fgAdministrationFees.Cols[0].AllowMerging = true;
            fgAdministrationFees.Redraw = true;

            if (fgAdministrationFees.Rows.Count > 2) tsbDelAdministrationOption.Enabled = false;
            else tsbDelAdministrationOption.Enabled = true;
        }
        private void AddAdministrationFees(float fltAmountFrom, float fltAmountTo, float fltFees, int iRetrosessionMethod, float fltRetrosessionProvider,
                                           float fltRetrosessionCompany, int iRec_ID)
        {
            fgAdministrationFees.Redraw = false;
            fgAdministrationFees.AddItem(fltAmountFrom + "\t" + fltAmountTo + "\t" + fltFees + "\t" + sDistrib[iRetrosessionMethod] + "\t" +
                             fltRetrosessionProvider + "\t" + fltRetrosessionCompany + "\t" + iRec_ID);
            fgAdministrationFees.Redraw = true;
        }

        private void tsbAddPackage_Click(object sender, EventArgs e)
        {
            iLocAktion = 0;
            txtCode.Text = "";
            txtPortfolio.Text = "";
            dPackageDateStart.Value = DateTime.Now.Date;
            dPackageDateFinish.Value = Convert.ToDateTime("2070/12/31");
            cmbPackageCurrency.Text = "EUR";
            cmbCompanyPackages.SelectedValue = 0;
            cmbFinanceServices.SelectedValue = 0;
            cmbProfile.SelectedValue = 0;
            cmbInvestmentPolicy.SelectedValue = 0;
            panPackage.Visible = true;
            txtCode.Focus();
        }

        private void tsbEditPackage_Click(object sender, EventArgs e)
        {
            iLocAktion = 1;
            txtCode.Text = fgPackages[fgPackages.Row, 0]+"";
            txtPortfolio.Text = fgPackages[fgPackages.Row, 1]+"";
            panPackage.Visible = true;
            txtCode.Focus();
        }

        private void tsbDelPackage_Click(object sender, EventArgs e)
        {
            if (fgPackages.Row > 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Contracts = new clsContracts();
                    Contracts.Record_ID = Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_ID"]);
                    Contracts.DeleteRecord();

                    Contract_Details = new clsContracts_Details();
                    Contract_Details.Record_ID = Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_Details_ID"]);
                    Contract_Details.DeleteRecord();

                    Contract_Packages = new clsContracts_Packages();
                    Contract_Packages.Record_ID = Convert.ToInt32(fgPackages[fgPackages.Row, "Contract_Packages_ID"]);
                    Contract_Packages.DeleteRecord();

                    fgPackages.RemoveItem(fgPackages.Row);
                }
            }
        }

        private void btnSave_Package_Click(object sender, EventArgs e)
        {
            if (iLocAktion == 0) {
                Contracts = new clsContracts();
                Contracts.PackageType = 2;
                Contracts.Client_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
                Contracts.ContractType = 0;
                Contracts.ClientsList = fgList[fgList.Row, 1] + "^^^1^0^0~";
                Contracts.ClientTipos = 0;
                Contracts.ContractTitle = fgList[fgList.Row, 0]+"";
                Contracts.Code = txtCode.Text;
                Contracts.Portfolio = txtPortfolio.Text;
                Contracts.Portfolio_Alias = "";
                Contracts.Portfolio_Type = "";
                Contracts.DateStart = dPackageDateStart.Value;
                Contracts.DateFinish = dPackageDateFinish.Value;
                Contracts.Currency = cmbPackageCurrency.Text;
                Contracts.NumberAccount = "";
                Contracts.Contract_Details_ID = 0;
                Contracts.Contract_Packages_ID = 0;
                Contracts.Status = 1;

                Contracts.Details.MIFIDCategory_ID = 1;
                Contracts.Details.AgreementNotes = "";
                Contracts.Details.InvestmentPolicy_ID = Convert.ToInt32(cmbInvestmentPolicy.SelectedValue);

                Contracts.Packages.Service_ID = Convert.ToInt32(cmbFinanceServices.SelectedValue);
                Contracts.Packages.CFP_ID = Convert.ToInt32(cmbCompanyPackages.SelectedValue);
                Contracts.Packages.DateStart = dPackageDateStart.Value;
                Contracts.Packages.DateFinish = dPackageDateFinish.Value;
                Contracts.Packages.Profile_ID = Convert.ToInt32(cmbProfile.SelectedValue);

                iContract_ID = Contracts.InsertRecord();
                iContract_Details_ID = Contracts.Details.Record_ID;
                iContract_Packages_ID = Contracts.Packages.Record_ID;

                ucCC.fgBrokerageFees.Redraw = false;
                ucCC.fgBrokerageFees.Rows.Count = 2;
                ucCC.fgBrokerageFees.Redraw = true;

                ucCC.fgFXFees.Redraw = false;
                ucCC.fgFXFees.Rows.Count = 2;
                ucCC.fgFXFees.Redraw = true;

                ucCC.fgCustodyFees.Redraw = false;
                ucCC.fgCustodyFees.Rows.Count = 2;
                ucCC.fgCustodyFees.Redraw = true;

                ucCC.fgSettlementsFees.Redraw = false;
                ucCC.fgSettlementsFees.Rows.Count = 2;
                ucCC.fgSettlementsFees.Redraw = true;

                ucCC.fgAdvisoryFees.Redraw = false;
                ucCC.fgAdvisoryFees.Rows.Count = 2;
                ucCC.fgAdvisoryFees.Redraw = true;

                ucCC.fgDiscretFees.Redraw = false;
                ucCC.fgDiscretFees.Rows.Count = 2;
                ucCC.fgDiscretFees.Redraw = true;

                ucCC.fgDealAdvisoryFees.Redraw = false;
                ucCC.fgDealAdvisoryFees.Rows.Count = 2;
                ucCC.fgDealAdvisoryFees.Redraw = true;

                ucCC.fgLombardFees.Redraw = false;
                ucCC.fgLombardFees.Rows.Count = 1;
                ucCC.fgLombardFees.Redraw = true;

                ucCC.fgCashAccounts.Redraw = false;
                ucCC.fgCashAccounts.Rows.Count = 1;
                ucCC.fgCashAccounts.Redraw = true;

                //ucCC.fgAccounts.Redraw = false;
                //ucCC.fgAccounts.Rows.Count = 1;
                //ucCC.fgAccounts.Redraw = true;

                bCheckList = false;
                fgPackages.AddItem(txtCode.Text + "\t" + txtPortfolio.Text + "\t" + iContract_ID + "\t" + iContract_Details_ID + "\t" + iContract_Packages_ID + "\t" + cmbCompanyPackages.SelectedValue);
                bCheckList = true;
                fgPackages.Row = fgPackages.Rows.Count - 1;
            }
            else
            {
                fgPackages[fgPackages.Row, 0] = txtCode.Text;
                fgPackages[fgPackages.Row, 1] = txtPortfolio.Text;
            }
            panPackage.Visible = false;
        }       

        private void btnCancel_Package_Click(object sender, EventArgs e)
        {
            panPackage.Visible = false;
        }
        #endregion

        #region --- Lombard functionality ----------------------------------------------------------------------------------

        private void tsbAddLombardOption_Click(object sender, EventArgs e)
        {
            iService = 6;                                                                           // 6 - Lombard
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            ShowEditOption();
        }

        private void tsbEditLombardOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 6;                                                                          // 6 - Lombard
                iLocAktion = 1;
                txtOption.Text = fgLombardOptions[fgLombardOptions.Row, 0] + "";
                dStart.Value = Convert.ToDateTime(fgLombardOptions[fgLombardOptions.Row, 1]);
                dFinish.Value = Convert.ToDateTime(fgLombardOptions[fgLombardOptions.Row, 2]);
                txtMonthMinAmount.Text = fgLombardOptions[fgLombardOptions.Row, "MonthMinAmount"] + "";
                iOption_ID = Convert.ToInt32(fgLombardOptions[fgLombardOptions.Row, "ID"]);
                ShowEditOption();
            }
        }

        private void tsbDelLombardOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgLombardOptions[fgLombardOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgLombardOptions.RemoveItem(fgLombardOptions.Row);
            }
        }
        private void fgLombardOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckLombardFees) DefineLombardFeesList();
        }
        private void tsbAddLombardData_Click(object sender, EventArgs e)
        {
            ServiceProviderLombardFees = new clsServiceProviderLombardFees();
            ServiceProviderLombardFees.SPO_ID = Convert.ToInt32(fgLombardOptions[fgLombardOptions.Row, "ID"]);
            ServiceProviderLombardFees.ServiceProvider_ID = iID;
            ServiceProviderLombardFees.Currency = "";
            iFees_ID = ServiceProviderLombardFees.InsertRecord();

            fgLombardFees.Redraw = false;
            fgLombardFees.AddItem("" + "\t" + iFees_ID);
            fgLombardFees.Redraw = true;
        }

        private void tsbDelLombardData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderLombardFees = new clsServiceProviderLombardFees();
                ServiceProviderLombardFees.Record_ID = Convert.ToInt32(fgLombardFees[fgLombardFees.Row, "ID"]);
                ServiceProviderLombardFees.DeleteRecord();

                fgLombardFees.RemoveItem(fgLombardFees.Row);
            }
        }
        private void fgLombardFees_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) {
                ServiceProviderLombardFees = new clsServiceProviderLombardFees();
                ServiceProviderLombardFees.Record_ID = Convert.ToInt32(fgLombardFees[fgLombardFees.Row, "ID"]);
                ServiceProviderLombardFees.GetRecord();
                ServiceProviderLombardFees.SPO_ID = Convert.ToInt32(fgLombardOptions[fgLombardOptions.Row, "ID"]);
                ServiceProviderLombardFees.ServiceProvider_ID = iID;
                ServiceProviderLombardFees.Currency = fgLombardFees[fgLombardFees.Row, "Currency"] + "";
                iFees_ID = ServiceProviderLombardFees.EditRecord();
            }
        }
        private void DefineLombardFeesList()
        {
            ServiceProviderLombardFees = new clsServiceProviderLombardFees();
            ServiceProviderLombardFees.ServiceProvider_ID = iID;
            ServiceProviderLombardFees.SPO_ID = Convert.ToInt32(fgLombardOptions[fgLombardOptions.Row, "ID"]);
            ServiceProviderLombardFees.GetFees();

            fgLombardFees.Redraw = false;
            fgLombardFees.Rows.Count = 1;
            if (fgLombardOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderLombardFees.List.Rows)
                    fgLombardFees.AddItem(dtRow["Currency"] + "\t" + dtRow["ID"]);
            }
            fgLombardFees.Cols[0].AllowMerging = true;
            fgLombardFees.Redraw = true;

            if (fgLombardFees.Rows.Count > 2) tsbDelLombardOption.Enabled = false;
            else tsbDelLombardOption.Enabled = true;
        }
        #endregion
        #region --- Settlements functionality ------------------------------------------------------------------------------
        private void tsbAddSettlementsOption_Click(object sender, EventArgs e)
        {
            iService = 8;                                                                            // 8 - Settlements
            iLocAktion = 0;
            iOption_ID = 0;
            txtOption.Text = "";
            dStart.Value = DateTime.Now;
            dFinish.Value = Convert.ToDateTime("2070-12-31");
            txtMonthMinAmount.Text = "0";
            ShowEditOption();
        }
        private void tsbEditSettlementsOption_Click(object sender, EventArgs e)
        {
            if (toolRight.Enabled)
            {
                iService = 8;                                                                        // 8 - Settlements
                iLocAktion = 1;
                txtOption.Text = fgSettlementsOptions[fgSettlementsOptions.Row, 0] + "";
                dStart.Value = Convert.ToDateTime(fgSettlementsOptions[fgSettlementsOptions.Row, 1]);
                dFinish.Value = Convert.ToDateTime(fgSettlementsOptions[fgSettlementsOptions.Row, 2]);
                txtMonthMinAmount.Text = "0";
                iOption_ID = Convert.ToInt32(fgSettlementsOptions[fgSettlementsOptions.Row, "ID"]);
                ShowEditOption();
            }
        }
        private void tsbDelSettlementsOption_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProvidersOptions = new clsServiceProvidersOptions();
                ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgSettlementsOptions[fgSettlementsOptions.Row, "ID"]);
                ServiceProvidersOptions.DeleteRecord();

                fgSettlementsOptions.RemoveItem(fgSettlementsOptions.Row);
            }
        }
        private void fgSettlementsOptions_RowColChange(object sender, EventArgs e)
        {
            if (bCheckSettlementsFees && fgSettlementsOptions.Rows.Count > 1) DefineSettlementsFeesList();
        }
        private void tsbAddSettlementsFees_Click(object sender, EventArgs e)
        {
            iFees_ID = 0;
            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 0;                                              // 0 - ADD
            locServiceProviderFees.Product_ID = 0;
            locServiceProviderFees.Category_ID = 0;
            locServiceProviderFees.txtAmountFrom.Text = "0";
            locServiceProviderFees.txtAmountTo.Text = "90000000";
            locServiceProviderFees.txtBuyFees.Text = "0";
            locServiceProviderFees.txtSellFees.Text = "0";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = "0";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = "0";
            locServiceProviderFees.cmbTicketFeesCurrs.Text = "EUR";
            locServiceProviderFees.txtMinimumFeesAmount.Text = "0";
            locServiceProviderFees.cmbMinimumFeesCurrs.Text = "EUR";
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = 0;
            locServiceProviderFees.txtProvider.Text = "0";
            locServiceProviderFees.txtCompany.Text = "0";
            locServiceProviderFees.Mode = 8;                                                        // 8 - Settlements
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1)
            {

                fgSettlementsFees.Redraw = false;

                dtList = Global.dtStockExchanges.Copy();
                foundRows = dtList.Select("ID = 0");
                foundRows[0]["Title"] = "Όλα";

                dtView2 = dtList.DefaultView;
                sTemp = "ID = " + locServiceProviderFees.cmbStockExchanges.SelectedValue;
                dtView2.RowFilter = sTemp;

                foreach (DataRowView dtViewRow2 in dtView2)
                {
                    if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == 0)
                    {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0)
                        {
                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(dtViewRow["ID"]) != 0)
                                {
                                    iFees_ID = SaveSettlementsFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                        else
                        {
                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == Convert.ToInt32(dtViewRow["ID"]))
                                {
                                    iFees_ID = SaveSettlementsFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(locServiceProviderFees.cmbCategories.SelectedValue) == 0)
                        {

                            dtView = Global.dtProductsCategories.DefaultView;
                            foreach (DataRowView dtViewRow in dtView)
                            {
                                if (Convert.ToInt32(locServiceProviderFees.cmbProducts.SelectedValue) == Convert.ToInt32(dtViewRow["Product_ID"]))
                                {
                                    iFees_ID = SaveSettlementsFees(0, Convert.ToInt32(dtViewRow["Product_ID"]), dtViewRow["ProductTitle"] + "", Convert.ToInt32(dtViewRow["ID"]), dtViewRow["Title"] + "",
                                       Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                                       Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                                       locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                                       locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                                       Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                                       locServiceProviderFees.cmbSettlementProviders.Text);
                                }
                            }
                        }
                        else
                        {
                            iFees_ID = SaveSettlementsFees(0, locServiceProviderFees.Product_ID, locServiceProviderFees.cmbProducts.Text + "", locServiceProviderFees.Category_ID,
                                locServiceProviderFees.cmbCategories.Text + "", Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                               Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                               Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                               Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                               locServiceProviderFees.cmbTicketFeesCurrs.Text, Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text), locServiceProviderFees.cmbMinimumFeesCurrs.Text,
                               locServiceProviderFees.cmbDistribMethods.SelectedIndex, Convert.ToSingle(locServiceProviderFees.txtProvider.Text),
                               Convert.ToSingle(locServiceProviderFees.txtCompany.Text), Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue),
                               locServiceProviderFees.cmbSettlementProviders.Text);
                        }
                    }
                }

                fgSettlementsFees.Rows[0].AllowMerging = true;
                fgSettlementsFees.Rows[1].AllowMerging = true;

                fgSettlementsFees.Cols[0].AllowMerging = true;
                fgSettlementsFees.Cols[1].AllowMerging = true;

                fgSettlementsFees.Redraw = true;
            }
        }

        private void tsbEditSettlementsFees_Click(object sender, EventArgs e)
        {
            iRow = fgSettlementsFees.Row;

            ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();
            ServiceProviderSettlementsFees.Record_ID = Convert.ToInt32(fgSettlementsFees[iRow, "ID"]);
            ServiceProviderSettlementsFees.GetRecord();

            frmServiceProviderFees locServiceProviderFees = new frmServiceProviderFees();
            locServiceProviderFees.Aktion = 1;                                              // 1 - EDIT
            locServiceProviderFees.Product_ID = ServiceProviderSettlementsFees.Product_ID;
            locServiceProviderFees.Category_ID = ServiceProviderSettlementsFees.ProductCategory_ID;
            locServiceProviderFees.StockExchange_ID = ServiceProviderSettlementsFees.Depositories_ID;
            locServiceProviderFees.txtAmountFrom.Text = ServiceProviderSettlementsFees.AmountFrom + "";
            locServiceProviderFees.txtAmountTo.Text = ServiceProviderSettlementsFees.AmountTo.ToString("0.##");
            locServiceProviderFees.txtBuyFees.Text = ServiceProviderSettlementsFees.BuyFeesPercent + "";
            locServiceProviderFees.txtSellFees.Text = ServiceProviderSettlementsFees.SellFeesPercent + "";
            locServiceProviderFees.txtTicketFeesBuyAmount.Text = ServiceProviderSettlementsFees.TicketFeesBuyAmount + "";
            locServiceProviderFees.txtTicketFeesSellAmount.Text = ServiceProviderSettlementsFees.TicketFeesSellAmount + "";
            locServiceProviderFees.TicketFeesCurr = ServiceProviderSettlementsFees.TicketFeesCurr;
            locServiceProviderFees.txtMinimumFeesAmount.Text = ServiceProviderSettlementsFees.MinimumFees + "";
            locServiceProviderFees.MinimumFeesCurr = ServiceProviderSettlementsFees.MinimumFeesCurr;
            locServiceProviderFees.cmbDistribMethods.SelectedIndex = ServiceProviderSettlementsFees.RetrosessionMethod;
            locServiceProviderFees.txtProvider.Text = ServiceProviderSettlementsFees.RetrosessionProvider + "";
            locServiceProviderFees.txtCompany.Text = ServiceProviderSettlementsFees.RetrosessionCompany + "";
            locServiceProviderFees.Mode = 8;                                                        // 8 - Settlements
            locServiceProviderFees.ShowDialog();
            if (locServiceProviderFees.Aktion == 1)
            {
                iFees_ID = SaveSettlementsFees(Convert.ToInt32(fgSettlementsFees[iRow, "ID"]), locServiceProviderFees.Product_ID,
                    locServiceProviderFees.cmbProducts.Text, locServiceProviderFees.Category_ID, locServiceProviderFees.cmbCategories.Text,
                    Convert.ToInt32(locServiceProviderFees.cmbStockExchanges.SelectedValue), locServiceProviderFees.cmbStockExchanges.Text,
                    Convert.ToSingle(locServiceProviderFees.txtAmountFrom.Text), Convert.ToSingle(locServiceProviderFees.txtAmountTo.Text),
                    Convert.ToSingle(locServiceProviderFees.txtBuyFees.Text), Convert.ToSingle(locServiceProviderFees.txtSellFees.Text),
                    Convert.ToSingle(locServiceProviderFees.txtTicketFeesBuyAmount.Text), Convert.ToSingle(locServiceProviderFees.txtTicketFeesSellAmount.Text),
                    locServiceProviderFees.cmbTicketFeesCurrs.Text + "", Convert.ToSingle(locServiceProviderFees.txtMinimumFeesAmount.Text),
                    locServiceProviderFees.cmbMinimumFeesCurrs.Text + "", Convert.ToInt32(locServiceProviderFees.cmbDistribMethods.SelectedIndex),
                    Convert.ToSingle(locServiceProviderFees.txtProvider.Text), Convert.ToSingle(locServiceProviderFees.txtCompany.Text),
                    Convert.ToInt32(locServiceProviderFees.cmbSettlementProviders.SelectedValue), locServiceProviderFees.cmbSettlementProviders.Text);

                DefineSettlementsFeesList();
                fgSettlementsFees.Row = iRow;
            }
        }

        private void tsbDelSettlementsFees_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();
                ServiceProviderSettlementsFees.Record_ID = Convert.ToInt32(fgSettlementsFees[fgSettlementsFees.Row, "ID"]);
                ServiceProviderSettlementsFees.DeleteRecord();

                fgSettlementsFees.RemoveItem(fgSettlementsFees.Row);
            }
        }
        private void DefineSettlementsFeesList()
        {
            ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();
            ServiceProviderSettlementsFees.ServiceProvider_ID = iID;
            ServiceProviderSettlementsFees.SPO_ID = Convert.ToInt32(fgSettlementsOptions[fgSettlementsOptions.Row, "ID"]);
            ServiceProviderSettlementsFees.GetFees();

            fgSettlementsFees.Redraw = false;
            fgSettlementsFees.Rows.Count = 2;
            if (fgSettlementsOptions.Rows.Count > 1)
            {
                foreach (DataRow dtRow in ServiceProviderSettlementsFees.List.Rows)
                    fgSettlementsFees.AddItem(dtRow["ProductTitle"] + "\t" + dtRow["ProductCategoryTitle"] + "\t" + dtRow["Depositories_Title"] + "\t" +
                                dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                dtRow["MinimumFeesAmount"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + sDistrib[Convert.ToInt32(dtRow["RetrosessionMethod"])] + "\t" +
                                dtRow["RetrosessionProvider"] + "\t" + dtRow["RetrosessionCompany"] + "\t" + dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                dtRow["ProductCategory_ID"] + "\t" + dtRow["RetrosessionMethod"] + "\t" + dtRow["Depositories_ID"]);
            }
            fgSettlementsFees.Redraw = true;

            if (fgSettlementsFees.Rows.Count > 2) tsbDelSettlementsOption.Enabled = false;
            else tsbDelSettlementsOption.Enabled = true;
        }
        private int SaveSettlementsFees(int iRec_ID, int iProduct_ID, string sProduct, int iCategory_ID, string sCategory,
                                int iStockExchange_ID, string sStockExchange, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees,
                                float fltTicketBuyFees, float fltTicketSellFees, string sTicketFeesCurr, float fltMinimumFees, string sMinimumFeesCurr,
                                int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany, int iSettlementProvider_ID, string sSettlementProvider)
        {
            ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();

            if (iRec_ID > 0)                                           // 0 - ADD, > 0 - Edit
            {
                ServiceProviderSettlementsFees.Record_ID = iRec_ID;
                ServiceProviderSettlementsFees.GetRecord();
            }

            ServiceProviderSettlementsFees.SPO_ID = Convert.ToInt32(fgSettlementsOptions[fgSettlementsOptions.Row, "ID"]);
            ServiceProviderSettlementsFees.ServiceProvider_ID = iID;
            ServiceProviderSettlementsFees.Product_ID = iProduct_ID;
            ServiceProviderSettlementsFees.ProductCategory_ID = iCategory_ID;
            ServiceProviderSettlementsFees.Depositories_ID = iStockExchange_ID;
            ServiceProviderSettlementsFees.AmountFrom = fltAmountFrom;
            ServiceProviderSettlementsFees.AmountTo = fltAmountTo;
            ServiceProviderSettlementsFees.BuyFeesPercent = fltBuyFees;
            ServiceProviderSettlementsFees.SellFeesPercent = fltSellFees;
            ServiceProviderSettlementsFees.TicketFeesBuyAmount = fltTicketBuyFees;
            ServiceProviderSettlementsFees.TicketFeesSellAmount = fltTicketSellFees;
            ServiceProviderSettlementsFees.TicketFeesCurr = sTicketFeesCurr;
            ServiceProviderSettlementsFees.MinimumFees = fltMinimumFees;
            ServiceProviderSettlementsFees.MinimumFeesCurr = sMinimumFeesCurr;
            ServiceProviderSettlementsFees.RetrosessionMethod = iRetrosessionMethod;
            ServiceProviderSettlementsFees.RetrosessionProvider = fltRetrosessionProvider;
            ServiceProviderSettlementsFees.RetrosessionCompany = fltRetrosessionCompany;
           
            if (iRec_ID == 0)
            {
                iFees_ID = ServiceProviderSettlementsFees.InsertRecord();

                AddSettlementsFees(iProduct_ID, sProduct, iCategory_ID, sCategory, iStockExchange_ID, sStockExchange,
                                 fltAmountFrom, fltAmountTo, fltBuyFees, fltSellFees, fltTicketBuyFees, fltTicketSellFees, sTicketFeesCurr,
                                 fltMinimumFees, sMinimumFeesCurr, iRetrosessionMethod, fltRetrosessionProvider, fltRetrosessionCompany, iFees_ID);
            }
            else iFees_ID = ServiceProviderSettlementsFees.EditRecord();

            return iFees_ID;
        }
        private void AddSettlementsFees(int iProduct_ID, string sProduct, int iProductCategory_ID, string sProductCategory, int iStockExchange_ID,
                              string sStockExchange_Title, float fltAmountFrom, float fltAmountTo, float fltBuyFees, float fltSellFees,
                              float fltTicketFeesBuyAmount, float fltTicketFeesSellAmount, string sTicketFeesCurrs, float fltMinimumFeesAmount,
                              string sMinimumFeesCurrs, int iRetrosessionMethod, float fltRetrosessionProvider, float fltRetrosessionCompany, int iRec_ID)
        {
            fgSettlementsFees.Redraw = false;
            fgSettlementsFees.AddItem(sProduct + "\t" + sProductCategory + "\t" + sStockExchange_Title + "\t" + fltAmountFrom + "\t" + fltAmountTo + "\t" +
                                    fltBuyFees + "\t" + fltSellFees + "\t" + fltTicketFeesBuyAmount + "\t" + fltTicketFeesSellAmount + "\t" + sTicketFeesCurrs + "\t" +
                                    fltMinimumFeesAmount + "\t" + sMinimumFeesCurrs + "\t" + sDistrib[iRetrosessionMethod] + "\t" + fltRetrosessionProvider + "\t" +
                                    fltRetrosessionCompany + "\t" + iRec_ID + "\t" + iProduct_ID + "\t" +
                                    iProductCategory_ID + "\t" + iRetrosessionMethod + "\t" + iStockExchange_ID);
            fgSettlementsFees.Redraw = true;
        }
        #endregion
        #region --- common functions ---------------------------------------------------------------------------------------
        private void EmptyDetails()
        {
            iID = 0;
            txtTitle.Text = "";
            txtAlias.Text = "";
            txtSeira.Text = "";
            txtVAT_FP.Text = "0";
            txtVAT_NP.Text = "0";
            cmbCurrencies.Text = "EUR";
            cmbStatement_File.SelectedIndex = 0;
            cmbMisc_File.SelectedIndex = 0;
            chkConvert_File.Checked = false;
            cmbSendOrders.SelectedIndex = 0;
            cmbFeesMode.SelectedIndex = 0;
            txtPriceTable.Text = "";
            txtEffectCode.Text = "";
            txtLEI.Text = "";
            txtFIX_DB.Text = "";
            cmbBestExecution.SelectedIndex = 0;
            txtDepositoryTitle.Text = "";
            chkAktive.Checked = false;

            bCheckBrokerageFees = false;
            bCheckRTOFees = false;
            bCheckAdvisoryFees = false;
            bCheckDealAdvisoryFees = false;
            bCheckDiscretFees = false;
            bCheckSafekeepingFees = false;
            bCheckAdministrationFees = false;
            bCheckLombardFees = false;
            bCheckFXFees = false;
            bCheckSettlementsFees = false;

            fgBrokerageOptions.Rows.Count = 1;
            fgBrokerageFees.Rows.Count = 2;
            fgAdvisoryOptions.Rows.Count = 2;
            fgAdvisoryFees.Rows.Count = 2;
            fgDealAdvisoryOptions.Rows.Count = 2;
            fgDealAdvisoryFees.Rows.Count = 2;
            fgDiscretOptions.Rows.Count = 2;
            fgDiscretFees.Rows.Count = 2;
            fgSafekeepingOptions.Rows.Count = 2;
            fgSafekeepingFees.Rows.Count = 2;
            fgAdministrationOptions.Rows.Count = 2;
            fgAdministrationFees.Rows.Count = 2;
            fgLombardOptions.Rows.Count = 1;
            fgLombardFees.Rows.Count = 1;
            fgFXOptions.Rows.Count = 1;
            fgFXFees.Rows.Count = 1;
            fgSettlementsOptions.Rows.Count = 1;
            fgSettlementsFees.Rows.Count = 2;

            bCheckBrokerageFees = true;
            bCheckRTOFees = true;
            bCheckAdvisoryFees = true;
            bCheckDealAdvisoryFees = true;
            bCheckDiscretFees = true;
            bCheckSafekeepingFees = true;
            bCheckAdministrationFees = true;
            bCheckLombardFees = true;
            bCheckFXFees = true;
            bCheckSettlementsFees = true;
        }
        private void ShowEditOption()
        {
            panOnlySafekeeping.Visible = false;

            switch (iService)
            {
                case 1:
                case 7:
                case 8:
                case 9:
                    lblMonthMinAmount.Visible = false;
                    txtMonthMinAmount.Visible = false;
                    cmbMonthMinCurr.Visible = false;

                    lblMinAmount.Visible = false;
                    txtMinAmount.Visible = false;
                    cmbMinCurr.Visible = false;

                    lblOpenAmount.Visible = false;
                    txtOpenAmount.Visible = false;
                    cmbOpenCurr.Visible = false;

                    lblServiceAmount.Visible = false;
                    txtServiceAmount.Visible = false;
                    cmbServiceCurr.Visible = false;

                    btnSave.Top = 120;
                    btnCancel.Top = 120;
                    panOption.Width = 427;
                    panOption.Height = 170;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 10:
                    lblMonthMinAmount.Visible = true;
                    lblMonthMinAmount.Text = "3 Μηνιαίο ελάχιστο ποσό αμοιβής";
                    txtMonthMinAmount.Visible = true;
                    cmbMonthMinCurr.Visible = true;

                    lblMinAmount.Visible = true;
                    txtMinAmount.Visible = true;
                    cmbMinCurr.Visible = true;

                    lblOpenAmount.Visible = true;
                    txtOpenAmount.Visible = true;
                    cmbOpenCurr.Visible = true;

                    lblServiceAmount.Visible = true;
                    txtServiceAmount.Visible = true;
                    cmbServiceCurr.Visible = true;

                    btnSave.Top = 282;
                    btnCancel.Top = 282;
                    panOption.Width = 427;
                    panOption.Height = 324;
                    if (iService == 4 || iService == 10) panOnlySafekeeping.Visible = true;                    
                    break;
                case 6:
                    lblMonthMinAmount.Visible = true;
                    lblMonthMinAmount.Text = "Additional Margin Rate";
                    txtMonthMinAmount.Visible = true;
                    cmbMonthMinCurr.Visible = false;

                    lblMinAmount.Visible = false;
                    txtMinAmount.Visible = false;
                    cmbMinCurr.Visible = false;

                    lblOpenAmount.Visible = false;
                    txtOpenAmount.Visible = false;
                    cmbOpenCurr.Visible = false;

                    lblServiceAmount.Visible = false;
                    txtServiceAmount.Visible = false;
                    cmbServiceCurr.Visible = false;

                    btnSave.Top = 158;
                    btnCancel.Top = 158;
                    panOption.Width = 427;
                    panOption.Height = 200;
                    break;
            }
            panOption.Top = (Screen.PrimaryScreen.Bounds.Height - panOption.Height) / 2;
            panOption.Left = (Screen.PrimaryScreen.Bounds.Width - panOption.Width) / 2;          
            panOption.Visible = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ServiceProvidersOptions = new clsServiceProvidersOptions();

            switch (iService) {
                case 1:                       // 1 - Brokerage
                    i = fgBrokerageOptions.Row;                    

                    if (iLocAktion == 0) {                                              // 0 - ADD mode
                        bCheckBrokerageFees = false;
                        fgBrokerageOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" + 
                                                   "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + "0");              // Last 0 - ID

                        fgBrokerageFees.Rows.Count = 2;
                        fgBrokerageFees.Redraw = true;
                        bCheckBrokerageFees = true;

                        i = fgBrokerageOptions.Rows.Count - 1;                        
                    }
                    else {                                                              // 1 - EDIT mode                        
                        fgBrokerageOptions[i, 0] = txtOption.Text;
                        fgBrokerageOptions[i, 1] = dStart.Value;
                        fgBrokerageOptions[i, 2] = dFinish.Value;
                        fgBrokerageOptions[i, 3] = txtMonthMinAmount.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgBrokerageOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 1;                           // 1 - Brokerage      
                    ServiceProvidersOptions.Title = fgBrokerageOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgBrokerageOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgBrokerageOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgBrokerageOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgBrokerageOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgBrokerageOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgBrokerageOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgBrokerageOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgBrokerageOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgBrokerageOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgBrokerageOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgBrokerageOptions[i, "ID"] = iOption_ID;
                    fgBrokerageOptions.Row = i;
                    break;

                case 2:                                                                          // 2 - Advisory
                    i = fgAdvisoryOptions.Row;

                    if (iLocAktion == 0) {
                        bCheckAdvisoryFees = false;
                        fgAdvisoryOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" + 
                                                  cmbMonthMinCurr.Text + "\t" + txtMinAmount.Text + "\t" + cmbMinCurr.Text + "\t" + txtOpenAmount.Text + "\t" +
                                                  cmbOpenCurr.Text + "\t" + txtServiceAmount.Text + "\t" + cmbServiceCurr.Text + "\t" + "0");

                        fgAdvisoryFees.Rows.Count = 2;
                        fgAdvisoryFees.Redraw = true;
                        bCheckAdvisoryFees = true;

                        i = fgAdvisoryOptions.Rows.Count - 1;
                    }
                    else {  
                        fgAdvisoryOptions[i, 0] = txtOption.Text;
                        fgAdvisoryOptions[i, 1] = dStart.Value;
                        fgAdvisoryOptions[i, 2] = dFinish.Value;
                        fgAdvisoryOptions[i, 3] = txtMonthMinAmount.Text;
                        fgAdvisoryOptions[i, 4] = cmbMonthMinCurr.Text;
                        fgAdvisoryOptions[i, 5] = txtMinAmount.Text;
                        fgAdvisoryOptions[i, 6] = cmbMinCurr.Text;
                        fgAdvisoryOptions[i, 7] = txtOpenAmount.Text;
                        fgAdvisoryOptions[i, 8] = cmbOpenCurr.Text;
                        fgAdvisoryOptions[i, 9] = txtServiceAmount.Text;
                        fgAdvisoryOptions[i, 10] = cmbServiceCurr.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgAdvisoryOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 2;                           // 2 - Advisory      
                    ServiceProvidersOptions.Title = fgAdvisoryOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgAdvisoryOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgAdvisoryOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgAdvisoryOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgAdvisoryOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgAdvisoryOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgAdvisoryOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgAdvisoryOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgAdvisoryOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgAdvisoryOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgAdvisoryOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgAdvisoryOptions[i, "ID"] = iOption_ID;
                    fgAdvisoryOptions.Row = i;
                    break;

                case 3:                                                                  // 3 - Discretionary
                    i = fgDiscretOptions.Row;

                    if (iLocAktion == 0) {
                        bCheckDiscretFees = false;
                        fgDiscretOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" +
                                                  cmbMonthMinCurr.Text + "\t" + txtMinAmount.Text + "\t" + cmbMinCurr.Text + "\t" + txtOpenAmount.Text + "\t" +
                                                  cmbOpenCurr.Text + "\t" + txtServiceAmount.Text + "\t" + cmbServiceCurr.Text + "\t" + "0");

                        fgDiscretFees.Rows.Count = 2;
                        fgDiscretFees.Redraw = true;
                        bCheckDiscretFees = true;

                        i = fgDiscretOptions.Rows.Count - 1;
                    }
                    else  {
                        fgDiscretOptions[i, 0] = txtOption.Text;
                        fgDiscretOptions[i, 1] = dStart.Value;
                        fgDiscretOptions[i, 2] = dFinish.Value;
                        fgDiscretOptions[i, 3] = txtMonthMinAmount.Text;
                        fgDiscretOptions[i, 4] = cmbMonthMinCurr.Text;
                        fgDiscretOptions[i, 5] = txtMinAmount.Text;
                        fgDiscretOptions[i, 6] = cmbMinCurr.Text;
                        fgDiscretOptions[i, 7] = txtOpenAmount.Text;
                        fgDiscretOptions[i, 8] = cmbOpenCurr.Text;
                        fgDiscretOptions[i, 9] = txtServiceAmount.Text;
                        fgDiscretOptions[i, 10] = cmbServiceCurr.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgDiscretOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 3;                           // 3 - Discret      
                    ServiceProvidersOptions.Title = fgDiscretOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgDiscretOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgDiscretOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgDiscretOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgDiscretOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgDiscretOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgDiscretOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgDiscretOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgDiscretOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgDiscretOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgDiscretOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgDiscretOptions[i, "ID"] = iOption_ID;
                    fgDiscretOptions.Row = i;
                    break;

                case 4:                                                                   // 4 - Safekeeping
                    i = fgSafekeepingOptions.Row;

                    if (iLocAktion == 0) {
                        bCheckSafekeepingFees = false;
                        fgSafekeepingOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" + 
                                                     cmbMonthMinCurr.Text + "\t" + txtMinAmount.Text + "\t" + cmbMinCurr.Text + "\t" + txtOpenAmount.Text + "\t" +
                                                     cmbOpenCurr.Text + "\t" + txtServiceAmount.Text + "\t" + cmbServiceCurr.Text + "\t" + chkAUM.Checked + "\t" + 
                                                     chkSecurities.Checked + "\t" + chkCash.Checked + "\t" + "0");
                        fgSafekeepingFees.Rows.Count = 2;
                        fgSafekeepingFees.Redraw = true;
                        bCheckSafekeepingFees = true;

                        i = fgSafekeepingOptions.Rows.Count - 1;
                    }
                    else {    
                        fgSafekeepingOptions[i, 0] = txtOption.Text;
                        fgSafekeepingOptions[i, 1] = dStart.Value;
                        fgSafekeepingOptions[i, 2] = dFinish.Value;
                        fgSafekeepingOptions[i, 3] = txtMonthMinAmount.Text;
                        fgSafekeepingOptions[i, 4] = cmbMonthMinCurr.Text;
                        fgSafekeepingOptions[i, 5] = txtMinAmount.Text;
                        fgSafekeepingOptions[i, 6] = cmbMinCurr.Text;
                        fgSafekeepingOptions[i, 7] = txtOpenAmount.Text;
                        fgSafekeepingOptions[i, 8] = cmbOpenCurr.Text;
                        fgSafekeepingOptions[i, 9] = txtServiceAmount.Text;
                        fgSafekeepingOptions[i, 10] = cmbServiceCurr.Text;
                        fgSafekeepingOptions[i, 11] = chkAUM.Checked;
                        fgSafekeepingOptions[i, 12] = chkSecurities.Checked;
                        fgSafekeepingOptions[i, 13] = chkCash.Checked;
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 4;                                   // 4 - Safekeeping      
                    ServiceProvidersOptions.Title = fgSafekeepingOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgSafekeepingOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgSafekeepingOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgSafekeepingOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgSafekeepingOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgSafekeepingOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgSafekeepingOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgSafekeepingOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgSafekeepingOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgSafekeepingOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgSafekeepingOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = Convert.ToBoolean(fgSafekeepingOptions[i, "CalcAUM"]) ? 1 : 0;
                    ServiceProvidersOptions.CalcSecurities = Convert.ToBoolean(fgSafekeepingOptions[i, "CalcSecurities"]) ? 1 : 0;
                    ServiceProvidersOptions.CalcCash = Convert.ToBoolean(fgSafekeepingOptions[i, "CalcCash"]) ? 1 : 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgSafekeepingOptions[i, "ID"] = iOption_ID;
                    fgSafekeepingOptions.Row = i;
                    break;

                case 5:                                                           // 5 - DealAdvisory
                    i = fgDealAdvisoryOptions.Row;

                    if (iLocAktion == 0)  {
                        bCheckDealAdvisoryFees = false;
                        fgDealAdvisoryOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" +
                                                  cmbMonthMinCurr.Text + "\t" + txtMinAmount.Text + "\t" + cmbMinCurr.Text + "\t" + txtOpenAmount.Text + "\t" +
                                                  cmbOpenCurr.Text + "\t" + txtServiceAmount.Text + "\t" + cmbServiceCurr.Text + "\t" + "0");

                        fgDealAdvisoryFees.Rows.Count = 2;
                        fgDealAdvisoryFees.Redraw = true;
                        bCheckDealAdvisoryFees = true;

                        i = fgDealAdvisoryOptions.Rows.Count - 1;                        
                    }
                    else {
                        fgDealAdvisoryOptions[i, 0] = txtOption.Text;
                        fgDealAdvisoryOptions[i, 1] = dStart.Value;
                        fgDealAdvisoryOptions[i, 2] = dFinish.Value;
                        fgDealAdvisoryOptions[i, 3] = txtMonthMinAmount.Text;
                        fgDealAdvisoryOptions[i, 4] = cmbMonthMinCurr.Text;
                        fgDealAdvisoryOptions[i, 5] = txtMinAmount.Text;
                        fgDealAdvisoryOptions[i, 6] = cmbMinCurr.Text;
                        fgDealAdvisoryOptions[i, 7] = txtOpenAmount.Text;
                        fgDealAdvisoryOptions[i, 8] = cmbOpenCurr.Text;
                        fgDealAdvisoryOptions[i, 9] = txtServiceAmount.Text;
                        fgDealAdvisoryOptions[i, 10] = cmbServiceCurr.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgDealAdvisoryOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 5;                           // 5 - DealAdvisory      
                    ServiceProvidersOptions.Title = fgDealAdvisoryOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgDealAdvisoryOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgDealAdvisoryOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgDealAdvisoryOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgDealAdvisoryOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgDealAdvisoryOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgDealAdvisoryOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgDealAdvisoryOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgDealAdvisoryOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgDealAdvisoryOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgDealAdvisoryOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgDealAdvisoryOptions[i, "ID"] = iOption_ID;
                    fgDealAdvisoryOptions.Row = i;
                    break;

                case 6:                                                                                          // 6 - Lombard Lending
                    i = fgLombardOptions.Row;

                    if (iLocAktion == 0) {
                        bCheckLombardFees = false;
                        fgLombardOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" + 
                                                 "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + "0");
                        fgLombardFees.Rows.Count = 1;
                        fgLombardFees.Redraw = true;
                        bCheckLombardFees = true;

                        i = fgLombardOptions.Rows.Count - 1;                        
                    }
                    else {   
                        fgLombardOptions[i, 0] = txtOption.Text;
                        fgLombardOptions[i, 1] = dStart.Value;
                        fgLombardOptions[i, 2] = dFinish.Value;
                        fgLombardOptions[i, 3] = txtMonthMinAmount.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgLombardOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 6;                                                        // 6 - Lombard      
                    ServiceProvidersOptions.Title = fgLombardOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgLombardOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgLombardOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgLombardOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgLombardOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgLombardOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgLombardOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgLombardOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgLombardOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgLombardOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgLombardOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgLombardOptions[i, "ID"] = iOption_ID;
                    fgLombardOptions.Row = i;
                    break;

                case 7:                                                  // 7 - FX
                    i = fgFXOptions.Row;

                    if (iLocAktion == 0) {           // 0 - ADD mode
                        bCheckFXFees = false;
                        fgFXOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" + 
                                            "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + "0");
                        fgFXFees.Rows.Count = 2;
                        fgFXFees.Redraw = true;
                        bCheckFXFees = true;

                        i = fgFXOptions.Rows.Count - 1;
                    }
                    else {
                        fgFXOptions[i, 0] = txtOption.Text;
                        fgFXOptions[i, 1] = dStart.Value;
                        fgFXOptions[i, 2] = dFinish.Value;
                        fgFXOptions[i, 3] = txtMonthMinAmount.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgFXOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 7;                           // 7 - FX      
                    ServiceProvidersOptions.Title = fgFXOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgFXOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgFXOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgFXOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgFXOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgFXOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgFXOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgFXOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgFXOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgFXOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgFXOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgFXOptions[i, "ID"] = iOption_ID;
                    fgFXOptions.Row = i;
                    break;

                case 8:                                              // 8 - Settlements
                    i = fgSettlementsOptions.Row;

                    if (iLocAktion == 0) {                                              // 0 - ADD mode
                        bCheckSettlementsFees = false;
                        fgSettlementsOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" +
                                                   "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + "0");              // Last 0 - ID

                        fgSettlementsFees.Rows.Count = 2;
                        fgSettlementsFees.Redraw = true;
                        bCheckSettlementsFees = true;

                        i = fgSettlementsOptions.Rows.Count - 1;
                    }
                    else  {                                                              // 1 - EDIT mode                        
                        fgSettlementsOptions[i, 0] = txtOption.Text;
                        fgSettlementsOptions[i, 1] = dStart.Value;
                        fgSettlementsOptions[i, 2] = dFinish.Value;
                        fgSettlementsOptions[i, 3] = txtMonthMinAmount.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgSettlementsOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 8;                                  // 8 - Settlements      
                    ServiceProvidersOptions.Title = fgSettlementsOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgSettlementsOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgSettlementsOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgSettlementsOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgSettlementsOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgSettlementsOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgSettlementsOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgSettlementsOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgSettlementsOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgSettlementsOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgSettlementsOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgSettlementsOptions[i, "ID"] = iOption_ID;
                    fgSettlementsOptions.Row = i;
                    break;
                case 9:          // 9 - RTO
                    i = fgRTOOptions.Row;

                    if (iLocAktion == 0) {                                              // 0 - ADD mode
                        bCheckRTOFees = false;
                        fgRTOOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" +
                                                   "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + 0 + "\t" + "" + "\t" + "0");              // Last 0 - ID

                        fgRTOFees.Rows.Count = 2;
                        fgRTOFees.Redraw = true;
                        bCheckRTOFees = true;

                        i = fgRTOOptions.Rows.Count - 1;                        
                    }
                    else {                                                              // 1 - EDIT mode                        
                        fgRTOOptions[i, 0] = txtOption.Text;
                        fgRTOOptions[i, 1] = dStart.Value;
                        fgRTOOptions[i, 2] = dFinish.Value;
                        fgRTOOptions[i, 3] = txtMonthMinAmount.Text;

                        ServiceProvidersOptions.Record_ID = Convert.ToInt32(fgRTOOptions[i, "ID"]);
                        ServiceProvidersOptions.GetRecord();
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 9;                           // 9 - RTO      
                    ServiceProvidersOptions.Title = fgRTOOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgRTOOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgRTOOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgRTOOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgRTOOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgRTOOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgRTOOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgRTOOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgRTOOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgRTOOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgRTOOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = 0;
                    ServiceProvidersOptions.CalcSecurities = 0;
                    ServiceProvidersOptions.CalcCash = 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgRTOOptions[i, "ID"] = iOption_ID;
                    fgRTOOptions.Row = i;
                    break;

                case 10:          // 10 - Administration
                    i = fgAdministrationOptions.Row;

                    if (iLocAktion == 0) {
                        bCheckAdministrationFees = false;
                        fgAdministrationOptions.AddItem(txtOption.Text + "\t" + dStart.Value + "\t" + dFinish.Value + "\t" + txtMonthMinAmount.Text + "\t" +
                                                     cmbMonthMinCurr.Text + "\t" + txtMinAmount.Text + "\t" + cmbMinCurr.Text + "\t" + txtOpenAmount.Text + "\t" +
                                                     cmbOpenCurr.Text + "\t" + txtServiceAmount.Text + "\t" + cmbServiceCurr.Text + "\t" + chkAUM.Checked + "\t" +
                                                     chkSecurities.Checked + "\t" + chkCash.Checked + "\t" + "0");
                        fgAdministrationFees.Rows.Count = 2;
                        fgAdministrationFees.Redraw = true;
                        bCheckAdministrationFees = true;

                        i = fgAdministrationOptions.Rows.Count - 1;                        
                    }
                    else {
                        fgAdministrationOptions[i, 0] = txtOption.Text;
                        fgAdministrationOptions[i, 1] = dStart.Value;
                        fgAdministrationOptions[i, 2] = dFinish.Value;
                        fgAdministrationOptions[i, 3] = txtMonthMinAmount.Text;
                        fgAdministrationOptions[i, 4] = cmbMonthMinCurr.Text;
                        fgAdministrationOptions[i, 5] = txtMinAmount.Text;
                        fgAdministrationOptions[i, 6] = cmbMinCurr.Text;
                        fgAdministrationOptions[i, 7] = txtOpenAmount.Text;
                        fgAdministrationOptions[i, 8] = cmbOpenCurr.Text;
                        fgAdministrationOptions[i, 9] = txtServiceAmount.Text;
                        fgAdministrationOptions[i, 10] = cmbServiceCurr.Text;
                        fgAdministrationOptions[i, 11] = chkAUM.Checked;
                        fgAdministrationOptions[i, 12] = chkSecurities.Checked;
                        fgAdministrationOptions[i, 13] = chkCash.Checked;
                    }

                    ServiceProvidersOptions.ServiceProvider_ID = iID;
                    ServiceProvidersOptions.ServiceType_ID = 10;                                   // 10 - Administration      
                    ServiceProvidersOptions.Title = fgAdministrationOptions[i, "Title"] + "";
                    ServiceProvidersOptions.DateStart = Convert.ToDateTime(fgAdministrationOptions[i, "DateStart"]);
                    ServiceProvidersOptions.DateFinish = Convert.ToDateTime(fgAdministrationOptions[i, "DateFinish"]);
                    ServiceProvidersOptions.MonthMinAmount = Convert.ToSingle(fgAdministrationOptions[i, "MonthMinAmount"]);
                    ServiceProvidersOptions.MonthMinCurr = fgAdministrationOptions[i, "MonthMinCurr"] + "";
                    ServiceProvidersOptions.OpenAmount = Convert.ToSingle(fgAdministrationOptions[i, "OpenAmount"]);
                    ServiceProvidersOptions.OpenCurr = fgAdministrationOptions[i, "OpenCurr"] + "";
                    ServiceProvidersOptions.ServiceAmount = Convert.ToSingle(fgAdministrationOptions[i, "ServiceAmount"]);
                    ServiceProvidersOptions.ServiceCurr = fgAdministrationOptions[i, "ServiceCurr"] + "";
                    ServiceProvidersOptions.MinAmount = Convert.ToSingle(fgAdministrationOptions[i, "MinAmount"]);
                    ServiceProvidersOptions.MinCurr = fgAdministrationOptions[i, "MinCurr"] + "";
                    ServiceProvidersOptions.CalcAUM = Convert.ToBoolean(fgAdministrationOptions[i, "CalcAUM"]) ? 1 : 0;
                    ServiceProvidersOptions.CalcSecurities = Convert.ToBoolean(fgAdministrationOptions[i, "CalcSecurities"]) ? 1 : 0;
                    ServiceProvidersOptions.CalcCash = Convert.ToBoolean(fgAdministrationOptions[i, "CalcCash"]) ? 1 : 0;

                    if (iLocAktion == 0) iOption_ID = ServiceProvidersOptions.InsertRecord();
                    else iOption_ID = ServiceProvidersOptions.EditRecord();
                    fgAdministrationOptions[i, "ID"] = iOption_ID;
                    fgAdministrationOptions.Row = i;
                    break;
            }
            panOption.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panOption.Visible = false;
        }
        #endregion
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
