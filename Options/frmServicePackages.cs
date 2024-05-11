using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class frmServicePackages : Form
    {
        DataTable dtList;
        DataRow dtRow;
        DataRow[] foundRows;
        int i, iID, iAction, iRightsLevel, iBrokerageOption_ID, iRTOOption_ID, iAdvisoryOption_ID, iDealAdvisoryOption_ID, iDiscretOption_ID,
            iCustodyOption1_ID, iCustodyOption2_ID, iAdminOption_ID, iLombardOption_ID, iFXOption1_ID, iFXOption2_ID, iSettlementsOption_ID, 
            iAdvisoryInvestment_Profile, iAdvisoryInvestment_Policy, iDiscretInvestment_Profile, iDiscretInvestment_Policy, iDealAdvisoryInvestment_Policy;
        string sExtra;
        bool bCheckList, bCheckOptions, bCheckInvestPolicies;   
        CellRange rng;
        clsCompanyPackages klsCompanyPackage = new clsCompanyPackages();
        clsClientsBrokerageFees klsClientsBrokerageFees = new clsClientsBrokerageFees();
        clsClientsRTOFees klsClientsRTOFees = new clsClientsRTOFees();
        clsClientsFXFees klsClientsFXFees = new clsClientsFXFees();
        clsClientsAdvisoryFees klsClientsAdvisoryFees = new clsClientsAdvisoryFees();
        clsClientsCustodyFees klsClientsCustodyFees = new clsClientsCustodyFees();
        clsClientsAdminFees klsClientsAdminFees = new clsClientsAdminFees();
        clsClientsDiscretFees klsClientsDiscretFees = new clsClientsDiscretFees();
        clsClientsSettlementFees klsClientsSettlementFees = new clsClientsSettlementFees();
        clsServiceProvidersOptions klsServiceProvidersOptions = new clsServiceProvidersOptions();
        public frmServicePackages()
        {
            InitializeComponent();
        }

        private void frmServicePackages_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bCheckOptions = false;
            bCheckInvestPolicies = false;

            panFilter.Left = 8;
            panFilter.Top = 36;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgBrokerageFees ----------------------------
            fgBrokerageFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBrokerageFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

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

            //------- fgRTOFees ----------------------------
            fgRTOFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRTOFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

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

            //------- fgAdvisoryFees ----------------------------
            fgAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgAdvisoryFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdvisoryFees.ShowCellLabels = true;

            fgAdvisoryFees.Styles.Normal.WordWrap = true;
            fgAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdvisoryFees.Rows[0].AllowMerging = true;

            rng = fgAdvisoryFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgAdvisoryFees[1, 0] = "από";
            fgAdvisoryFees[1, 1] = "εώς";

            fgAdvisoryFees.Cols[2].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            fgAdvisoryFees.Cols[3].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 3, 1, 3);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgAdvisoryFees.GetCellRange(0, 4, 0, 5);
            rng.Data = "Μεταβλητές";

            fgAdvisoryFees[1, 4] = "Κείμενο";
            fgAdvisoryFees[1, 5] = "%";

            //------- fgDiscretFees ----------------------------
            fgDiscretFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgDiscretFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgDiscretFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDiscretFees.ShowCellLabels = true;

            fgDiscretFees.Styles.Normal.WordWrap = true;
            fgDiscretFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDiscretFees.Rows[0].AllowMerging = true;

            rng = fgDiscretFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgDiscretFees[1, 0] = "από";
            fgDiscretFees[1, 1] = "εώς";

            fgDiscretFees.Cols[2].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            fgDiscretFees.Cols[3].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 3, 1, 3);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgDiscretFees.GetCellRange(0, 4, 0, 5);
            rng.Data = "Μεταβλητές";

            fgDiscretFees[1, 4] = "Κείμενο";
            fgDiscretFees[1, 5] = "%";


            //------- fgCustodyFees1 ----------------------------
            fgCustodyFees1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgCustodyFees1.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgCustodyFees1.DrawMode = DrawModeEnum.OwnerDraw;
            fgCustodyFees1.ShowCellLabels = true;

            fgCustodyFees1.Styles.Normal.WordWrap = true;
            fgCustodyFees1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgCustodyFees1.Rows[0].AllowMerging = true;

            rng = fgCustodyFees1.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgCustodyFees1[1, 0] = "από";
            fgCustodyFees1[1, 1] = "εώς";

            fgCustodyFees1.Cols[2].AllowMerging = true;
            rng = fgCustodyFees1.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            //------- fgCustodyFees2 ----------------------------
            fgCustodyFees2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgCustodyFees2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgCustodyFees2.DrawMode = DrawModeEnum.OwnerDraw;
            fgCustodyFees2.ShowCellLabels = true;

            fgCustodyFees2.Styles.Normal.WordWrap = true;
            fgCustodyFees2.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgCustodyFees2.Rows[0].AllowMerging = true;

            rng = fgCustodyFees2.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgCustodyFees2[1, 0] = "από";
            fgCustodyFees2[1, 1] = "εώς";

            fgCustodyFees2.Cols[2].AllowMerging = true;
            rng = fgCustodyFees2.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            //------- fgAdminFees1 ----------------------------
            fgAdminFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgAdminFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgAdminFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgAdminFees.ShowCellLabels = true;

            fgAdminFees.Styles.Normal.WordWrap = true;
            fgAdminFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgAdminFees.Rows[0].AllowMerging = true;

            rng = fgAdminFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgAdminFees[1, 0] = "από";
            fgAdminFees[1, 1] = "εώς";

            fgAdminFees.Cols[2].AllowMerging = true;
            rng = fgAdminFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";


            //------- fgDealAdvisoryFees ----------------------------
            fgDealAdvisoryFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgDealAdvisoryFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgDealAdvisoryFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgDealAdvisoryFees.ShowCellLabels = true;

            fgDealAdvisoryFees.Styles.Normal.WordWrap = true;
            fgDealAdvisoryFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgDealAdvisoryFees.Rows[0].AllowMerging = true;

            rng = fgDealAdvisoryFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgDealAdvisoryFees[1, 0] = "από";
            fgDealAdvisoryFees[1, 1] = "εώς";

            fgDealAdvisoryFees.Cols[2].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            fgDealAdvisoryFees.Cols[3].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 3, 1, 3);
            rng.Data = "Νόμισμα";

            fgDealAdvisoryFees.Cols[4].AllowMerging = true;
            rng = fgDealAdvisoryFees.GetCellRange(0, 4, 1, 4);
            rng.Data = "Αμοιβή Υπεραπόδοσης";

            rng = fgDealAdvisoryFees.GetCellRange(0, 5, 0, 6);
            rng.Data = "Μεταβλητές";

            fgDealAdvisoryFees[1, 5] = "Κείμενο";
            fgDealAdvisoryFees[1, 6] = "%";

            //------- fgLombardFees ----------------------------
            fgLombardFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgLombardFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            //------- fgFXFees1 ----------------------------
            fgFXFees1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgFXFees1.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgFXFees1.DrawMode = DrawModeEnum.OwnerDraw;
            fgFXFees1.ShowCellLabels = true;

            fgFXFees1.Styles.Normal.WordWrap = true;
            fgFXFees1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgFXFees1.Rows[0].AllowMerging = true;

            rng = fgFXFees1.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgFXFees1[1, 0] = "από";
            fgFXFees1[1, 1] = "εώς";

            fgFXFees1.Cols[2].AllowMerging = true;
            rng = fgFXFees1.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            //------- fgFXFees2 ----------------------------
            fgFXFees2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgFXFees2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgFXFees2.DrawMode = DrawModeEnum.OwnerDraw;
            fgFXFees2.ShowCellLabels = true;

            fgFXFees2.Styles.Normal.WordWrap = true;
            fgFXFees2.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgFXFees2.Rows[0].AllowMerging = true;

            rng = fgFXFees2.GetCellRange(0, 0, 0, 1);
            rng.Data = "Κλίμακα";

            fgFXFees2[1, 0] = "από";
            fgFXFees2[1, 1] = "εώς";

            fgFXFees2.Cols[2].AllowMerging = true;
            rng = fgFXFees2.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            //------- fgSettlementsFees ----------------------------
            fgSettlementsFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSettlementsFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

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

            //-------------- Define Service Providers List ------------------      
            dtList = Global.dtServiceProviders.Copy();
            foundRows = dtList.Select("ID = 0");
            foundRows[0]["Title"] = "-";
            cmbServiceProviders.DataSource = dtList;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";

            //----- initialize FINANCE SERVICES List ------- 
            cmbFinanceServices.DataSource = Global.dtServices.Copy();
            cmbFinanceServices.DisplayMember = "Title";
            cmbFinanceServices.ValueMember = "ID";

            //-------------- Define Service Providers List ------------------          
            cmbBrokerageServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbBrokerageServiceProviders.DisplayMember = "Title";
            cmbBrokerageServiceProviders.ValueMember = "ID";

            //-------------- Define Service Providers List ------------------          
            cmbRTOServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbRTOServiceProviders.DisplayMember = "Title";
            cmbRTOServiceProviders.ValueMember = "ID";

            //--------- Define cmbAdvisoryServiceProviders colllection ---------------
            cmbAdvisoryServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbAdvisoryServiceProviders.DisplayMember = "Title";
            cmbAdvisoryServiceProviders.ValueMember = "ID";

            //--------- Define cmbDealAdvisoryServiceProviders colllection ---------------
            cmbDealAdvisoryServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbDealAdvisoryServiceProviders.DisplayMember = "Title";
            cmbDealAdvisoryServiceProviders.ValueMember = "ID";

            //--------- Define cmbDiscretServiceProviders colllection ---------------
            cmbDiscretServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbDiscretServiceProviders.DisplayMember = "Title";
            cmbDiscretServiceProviders.ValueMember = "ID";

            //--------- Define cmbCustodyServiceProviders colllection ---------------
            cmbCustodyServiceProviders1.DataSource = Global.dtServiceProviders.Copy();
            cmbCustodyServiceProviders1.DisplayMember = "Title";
            cmbCustodyServiceProviders1.ValueMember = "ID";

            cmbCustodyServiceProviders2.DataSource = Global.dtServiceProviders.Copy();
            cmbCustodyServiceProviders2.DisplayMember = "Title";
            cmbCustodyServiceProviders2.ValueMember = "ID";

            //--------- Define cmbAdministrationServiceProviders colllection ---------------
            cmbAdministrationServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbAdministrationServiceProviders.DisplayMember = "Title";
            cmbAdministrationServiceProviders.ValueMember = "ID";

            //-------------- Define cmbLombard Service Providers List ------------------          
            cmbLombardServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbLombardServiceProviders.DisplayMember = "Title";
            cmbLombardServiceProviders.ValueMember = "ID";

            //-------------- Define cmbFXServiceProviders Service Providers List ------------------          
            cmbFXServiceProviders1.DataSource = Global.dtServiceProviders.Copy();
            cmbFXServiceProviders1.DisplayMember = "Title";
            cmbFXServiceProviders1.ValueMember = "ID";

            cmbFXServiceProviders2.DataSource = Global.dtServiceProviders.Copy();
            cmbFXServiceProviders2.DisplayMember = "Title";
            cmbFXServiceProviders2.ValueMember = "ID";

            //-------------- Define Settlements Service Providers List ------------------          
            cmbSettlementsServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbSettlementsServiceProviders.DisplayMember = "Title";
            cmbSettlementsServiceProviders.ValueMember = "ID";

            //----- initialize EPENDITIKES PROFILES List -------         
            cmbAdvisoryInvestmentProfile.DataSource = Global.dtCustomersProfiles.Copy();
            cmbAdvisoryInvestmentProfile.DisplayMember = "Title";
            cmbAdvisoryInvestmentProfile.ValueMember = "ID";

            //----- initialize EPENDITIKES POLITIKES List -------         
            cmbAdvisoryInvestmentPolicy.DataSource = Global.dtInvestPolicies.Copy(); 
            cmbAdvisoryInvestmentPolicy.DisplayMember = "Title";
            cmbAdvisoryInvestmentPolicy.ValueMember = "ID";

            //----- initialize EPENDITIKES POLITIKES List -------
            cmbDealAdvisoryFinanceTools.DataSource = Global.dtFinanceTools.Copy();
            cmbDealAdvisoryFinanceTools.DisplayMember = "Title";
            cmbDealAdvisoryFinanceTools.ValueMember = "ID";


            //----- initialize EPENDITIKES PROFILES List -------         
            cmbDiscretInvestmentProfile.DataSource = Global.dtCustomersProfiles.Copy();
            cmbDiscretInvestmentProfile.DisplayMember = "Title";
            cmbDiscretInvestmentProfile.ValueMember = "ID";

            //----- initialize EPENDITIKES POLITIKES List -------
            cmbDiscretInvestmentPolicy.DataSource = Global.dtInvestPolicies.Copy();
            cmbDiscretInvestmentPolicy.DisplayMember = "Title";
            cmbDiscretInvestmentPolicy.ValueMember = "ID";

            //-------------- Define Service Providers List into Filter ------------------          
            cmbServiceProviders_Filter.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders_Filter.DisplayMember = "Title";
            cmbServiceProviders_Filter.ValueMember = "ID";
            cmbServiceProviders_Filter.SelectedValue = 0;

            //----- initialize FINANCE SERVICES List into Filter ------- 
            cmbFinanceServices_Filter.DataSource = Global.dtServices.Copy();
            cmbFinanceServices_Filter.DisplayMember = "Title";
            cmbFinanceServices_Filter.ValueMember = "ID";
            cmbFinanceServices_Filter.SelectedValue = 0;

            cmbBusinessType_Filter.SelectedIndex = 0;

            bCheckOptions = true;
            DefineList();
            bCheckList = true;
            bCheckInvestPolicies = true;

            if (iRightsLevel == 1) {
                tsbAdd.Enabled = false;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
                toolBrokerageFees.Enabled = false;
            }

            ChangeMode(1);
        } 
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 86;

            grpData.Width = this.Width - 348;
            grpData.Height = this.Height - 80;

            tabData.Width = grpData.Width - 20;
            tabData.Height = grpData.Height - 184;

            fgBrokerageFees.Width = tabData.Width - 32;
            fgBrokerageFees.Height = tabData.Height - 108;

            fgRTOFees.Width = tabData.Width - 32;
            fgRTOFees.Height = tabData.Height - 108;

            fgFXFees1.Height = tabData.Height - 108;
            fgFXFees2.Height = tabData.Height - 108;
        }
        private void DefineList()
        {
            bCheckList = false;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            klsCompanyPackage = new clsCompanyPackages();
            klsCompanyPackage.Provider_ID = Convert.ToInt32(cmbServiceProviders_Filter.SelectedValue);
            klsCompanyPackage.PackageType_ID = Convert.ToInt32(cmbFinanceServices_Filter.SelectedValue);
            klsCompanyPackage.BusinessType_ID = cmbBusinessType_Filter.SelectedIndex;
            klsCompanyPackage.CheckActuality = chkActivity_Filter.Checked ? 1 : 0;
            klsCompanyPackage.ActualDate = DateTime.Now;
            klsCompanyPackage.Title = txtFilter.Text;
            klsCompanyPackage.GetList();
            foreach (DataRow dtRow in klsCompanyPackage.List.Rows) { 
                fgList.AddItem(dtRow["Title"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["ID"] + "\t" + dtRow["Notes"]);
            }
            fgList.Redraw = true;

            if (fgList.Rows.Count > 1) {
                fgList.Focus();
                ShowRecord();
            }

            bCheckList = true;
        }              
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            if (bCheckList) 
                if (fgList.Row > 0) ShowRecord();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            EmptyDetails();
            txtVersion.Text = "1";
            iAction = 0;                      // 0 - ADD Mode
            ChangeMode(2);
            txtTitle.Focus();
        }
        private void tsbCopy_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) {
                klsCompanyPackage = new clsCompanyPackages();
                klsCompanyPackage.Record_ID = Convert.ToInt32(fgList[fgList.Row, 2]);
                klsCompanyPackage.GetRecord();
                txtVersion.Text = (Convert.ToInt32(klsCompanyPackage.PackageVersion) + 1).ToString();
                iAction = 0;
                SaveRec();
                iAction = 1;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;                        // 1 - EDIT Mode
            ChangeMode(2);
            txtTitle.Focus();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            clsContracts_Packages Contracts_Packages = new clsContracts_Packages();
            Contracts_Packages.CFP_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            Contracts_Packages.GetList();
            if (Contracts_Packages.List.Rows.Count == 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    klsCompanyPackage = new clsCompanyPackages();
                    klsCompanyPackage.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    klsCompanyPackage.DeleteRecord();

                    fgList.RemoveItem(fgList.Row);
                }
            }
            else
                MessageBox.Show("Το πακέτο δεν μπορεί να διαγραφεί εφόσον χρησιμοποιείτε σε κάποια σύμβαση", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            fgList.Focus();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {

        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            txtTitle_Filter.Text = txtFilter.Text;
            panFilter.Visible = true;
        }
        private void btnFilter_OK_Click(object sender, EventArgs e)
        {
            txtFilter.Text = txtTitle_Filter.Text;
            DefineList();
            panFilter.Visible = false;
        }
        private void btnFilter_Cancel_Click(object sender, EventArgs e)
        {
            panFilter.Visible = false;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveRec();
        }
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            ChangeMode(1);
        }
        private void SaveRec()
        {
            if (txtTitle.Text.Length != 0)
            {

                klsCompanyPackage = new clsCompanyPackages();

                if (iAction == 1)
                {                          // 0 - ADD Mode, 1 - EDIT Mode
                    klsCompanyPackage.Record_ID = iID;
                    klsCompanyPackage.GetRecord();
                }

                klsCompanyPackage.BusinessType_ID = Convert.ToInt32(cmbBusinessType.SelectedIndex);
                klsCompanyPackage.Title = txtTitle.Text;
                klsCompanyPackage.MIFID = (chkMIIFID_2.Checked ? 2 : 1);
                klsCompanyPackage.PackageProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                klsCompanyPackage.PackageType_ID = Convert.ToInt32(cmbFinanceServices.SelectedValue);
                klsCompanyPackage.PackageVersion = Convert.ToInt32(txtVersion.Text);
                klsCompanyPackage.ClientTipos_ID = Convert.ToInt32(cmbClientTipos.SelectedValue);
                klsCompanyPackage.DateStart = dStart.Value;
                klsCompanyPackage.DateFinish = dFinish.Value;
                klsCompanyPackage.Notes = txtNotes.Text;
                klsCompanyPackage.BrokerageServiceProvider_ID = Convert.ToInt32(cmbBrokerageServiceProviders.SelectedValue);
                klsCompanyPackage.BrokerageOption_ID = Convert.ToInt32(cmbBrokerageOptions.SelectedValue);
                klsCompanyPackage.RTOServiceProvider_ID = Convert.ToInt32(cmbRTOServiceProviders.SelectedValue);
                klsCompanyPackage.RTOOption_ID = Convert.ToInt32(cmbRTOOptions.SelectedValue);
                klsCompanyPackage.AdvisoryServiceProvider_ID = Convert.ToInt32(cmbAdvisoryServiceProviders.SelectedValue);
                klsCompanyPackage.AdvisoryOption_ID = Convert.ToInt32(cmbAdvisoryOptions.SelectedValue);
                klsCompanyPackage.AdvisoryInvestmentProfile_ID = Convert.ToInt32(cmbAdvisoryInvestmentProfile.SelectedValue);
                klsCompanyPackage.AdvisoryInvestmentPolicy_ID = Convert.ToInt32(cmbAdvisoryInvestmentPolicy.SelectedValue);
                klsCompanyPackage.CustodyServiceProvider_ID = Convert.ToInt32(cmbCustodyServiceProviders1.SelectedValue);
                klsCompanyPackage.CustodyOption_ID = Convert.ToInt32(cmbCustodyOptions1.SelectedValue);
                klsCompanyPackage.AdministrationServiceProvider_ID = Convert.ToInt32(cmbAdministrationServiceProviders.SelectedValue);
                klsCompanyPackage.AdministrationOption_ID = Convert.ToInt32(cmbAdministrationOptions.SelectedValue);
                klsCompanyPackage.DealAdvisoryServiceProvider_ID = Convert.ToInt32(cmbDealAdvisoryServiceProviders.SelectedValue);
                klsCompanyPackage.DealAdvisoryOption_ID = Convert.ToInt32(cmbDealAdvisoryOptions.SelectedValue);
                klsCompanyPackage.DealAdvisoryInvestmentPolicy_ID = Convert.ToInt32(cmbDealAdvisoryFinanceTools.SelectedValue);
                klsCompanyPackage.DiscretServiceProvider_ID = Convert.ToInt32(cmbDiscretServiceProviders.SelectedValue);
                klsCompanyPackage.DiscretOption_ID = Convert.ToInt32(cmbDiscretOptions.SelectedValue);
                klsCompanyPackage.DiscretInvestmentProfile_ID = Convert.ToInt32(cmbDiscretInvestmentProfile.SelectedValue);
                klsCompanyPackage.DiscretInvestmentPolicy_ID = Convert.ToInt32(cmbDiscretInvestmentPolicy.SelectedValue);
                klsCompanyPackage.LombardServiceProvider_ID = Convert.ToInt32(cmbLombardServiceProviders.SelectedValue);
                klsCompanyPackage.LombardOption_ID = Convert.ToInt32(cmbLombardOptions.SelectedValue);
                klsCompanyPackage.FXServiceProvider_ID = Convert.ToInt32(cmbFXServiceProviders1.SelectedValue);
                klsCompanyPackage.FXOption_ID = Convert.ToInt32(cmbFXOptions1.SelectedValue);
                klsCompanyPackage.SettlementsServiceProvider_ID = Convert.ToInt32(cmbSettlementsServiceProviders.SelectedValue);
                klsCompanyPackage.SettlementsOption_ID = Convert.ToInt32(cmbSettlementsOptions.SelectedValue);

                if (iAction == 0) iID = klsCompanyPackage.InsertRecord();
                else klsCompanyPackage.EditRecord();

                bCheckList = false;
                i = fgList.FindRow(iID.ToString(), 1, 2, false);
                DefineList();
                bCheckList = true;
                if (i > 0) fgList.Row = i;
                iAction = 1;
            }
            else MessageBox.Show("Η εισαγωγή του τίτλου είναι υποχρεωτική", "Πάκετα Υπηρεσιών", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            ChangeMode(1);
        }
        private void ShowRecord()
        {
            bCheckList = false;
            bCheckOptions = false;
            EmptyDetails();
            iBrokerageOption_ID = 0;
            iRTOOption_ID = 0;
            iAdvisoryOption_ID = 0;
            iDealAdvisoryOption_ID = 0;
            iDiscretOption_ID = 0;
            iCustodyOption1_ID = 0;
            iCustodyOption2_ID = 0;
            iAdminOption_ID = 0;
            iLombardOption_ID = 0;
            iFXOption1_ID = 0;
            iFXOption2_ID = 0;
            iSettlementsOption_ID = 0;
            iAdvisoryInvestment_Profile = 0;
            iAdvisoryInvestment_Policy = 0;
            iDiscretInvestment_Profile = 0;
            iDiscretInvestment_Policy = 0;
            iDealAdvisoryInvestment_Policy = 0;

            iID = Convert.ToInt32(fgList[fgList.Row, 2]);
            txtTitle.Text = fgList[fgList.Row, 0]+"";
            txtVersion.Text = fgList[fgList.Row, 1] + "";

            klsCompanyPackage = new clsCompanyPackages();
            klsCompanyPackage.Record_ID = iID;
            klsCompanyPackage.GetRecord();
            cmbBusinessType.SelectedIndex = klsCompanyPackage.BusinessType_ID;
            cmbClientTipos.SelectedIndex = klsCompanyPackage.ClientTipos_ID;
            cmbServiceProviders.SelectedValue = klsCompanyPackage.PackageProvider_ID;
            cmbFinanceServices.SelectedValue = klsCompanyPackage.PackageType_ID;
            dStart.Value = klsCompanyPackage.DateStart;
            dFinish.Value = klsCompanyPackage.DateFinish;
            txtNotes.Text = klsCompanyPackage.Notes;

            cmbClientTipos.SelectedIndex = klsCompanyPackage.ClientTipos_ID;
            cmbFinanceServices.SelectedValue = klsCompanyPackage.PackageType_ID;
            txtNotes.Text = klsCompanyPackage.Notes;
            chkMIIFID_2.Checked = klsCompanyPackage.MIFID == 2 ? true : false;

            cmbBrokerageServiceProviders.SelectedValue = klsCompanyPackage.BrokerageServiceProvider_ID;
            iBrokerageOption_ID = klsCompanyPackage.BrokerageOption_ID;

            cmbRTOServiceProviders.SelectedValue = klsCompanyPackage.RTOServiceProvider_ID;
            iRTOOption_ID = klsCompanyPackage.RTOOption_ID;

            iAdvisoryOption_ID = klsCompanyPackage.AdvisoryOption_ID;
            cmbAdvisoryServiceProviders.SelectedValue = klsCompanyPackage.AdvisoryServiceProvider_ID;
            iAdvisoryInvestment_Profile = klsCompanyPackage.AdvisoryInvestmentProfile_ID;
            iAdvisoryInvestment_Policy = klsCompanyPackage.AdvisoryInvestmentPolicy_ID;

            iDiscretOption_ID = klsCompanyPackage.DiscretOption_ID;
            cmbDiscretServiceProviders.SelectedValue = klsCompanyPackage.DiscretServiceProvider_ID;
            iDiscretInvestment_Profile = klsCompanyPackage.DiscretInvestmentProfile_ID;
            iDiscretInvestment_Policy = klsCompanyPackage.DiscretInvestmentPolicy_ID;

            iCustodyOption1_ID = klsCompanyPackage.CustodyOption_ID;
            cmbCustodyServiceProviders1.SelectedValue = klsCompanyPackage.CustodyServiceProvider_ID;
            //iCustodyOption2_ID = klsCompanyPackage.CustodyOption2_ID;
            //cmbCustodyServiceProviders2.SelectedValue = klsCompanyPackage.CustodyServiceProvider2_ID;

            iAdminOption_ID = klsCompanyPackage.AdministrationOption_ID;
            cmbAdministrationServiceProviders.SelectedValue = klsCompanyPackage.AdministrationServiceProvider_ID;

            iDealAdvisoryOption_ID = klsCompanyPackage.DealAdvisoryOption_ID;
            cmbDealAdvisoryServiceProviders.SelectedValue = klsCompanyPackage.DealAdvisoryServiceProvider_ID;
            iDealAdvisoryInvestment_Policy = klsCompanyPackage.DealAdvisoryInvestmentPolicy_ID;

            iLombardOption_ID = klsCompanyPackage.LombardOption_ID;
            cmbLombardServiceProviders.SelectedValue = klsCompanyPackage.LombardServiceProvider_ID;

            iFXOption1_ID = klsCompanyPackage.FXOption_ID;
            cmbFXServiceProviders1.SelectedValue = klsCompanyPackage.FXServiceProvider_ID;
            //iFXOption2_ID = klsCompanyPackage.FXOption2_ID;
            //cmbFXServiceProviders2.SelectedValue = klsCompanyPackage.FXServiceProvider2_ID;

            iSettlementsOption_ID = klsCompanyPackage.SettlementsOption_ID;
            cmbSettlementsServiceProviders.SelectedValue = klsCompanyPackage.SettlementsServiceProvider_ID;


            bCheckOptions = true;
            DefineOptionsList(Convert.ToInt32(cmbBrokerageServiceProviders.SelectedValue), 1, 1);
            cmbBrokerageOptions.SelectedValue = iBrokerageOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbRTOServiceProviders.SelectedValue), 9, 1);
            cmbRTOOptions.SelectedValue = iRTOOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbAdvisoryServiceProviders.SelectedValue), 2, 1);
            cmbAdvisoryOptions.SelectedValue = iAdvisoryOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbDiscretServiceProviders.SelectedValue), 3, 1);
            cmbDiscretOptions.SelectedValue = iDiscretOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbCustodyServiceProviders1.SelectedValue), 4, 1);
            cmbCustodyOptions1.SelectedValue = iCustodyOption1_ID;

            //DefineOptionsList(Convert.ToInt32(cmbCustodyServiceProviders2.SelectedValue), 4, 2);
            //cmbCustodyOptions2.SelectedValue = iCustodyOption2_ID;

            DefineOptionsList(Convert.ToInt32(cmbAdministrationServiceProviders.SelectedValue), 10, 1);
            cmbAdministrationOptions.SelectedValue = iAdminOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbDealAdvisoryServiceProviders.SelectedValue), 5, 1);
            cmbDealAdvisoryOptions.SelectedValue = iDealAdvisoryOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbLombardServiceProviders.SelectedValue), 6, 1);
            cmbLombardOptions.SelectedValue = iLombardOption_ID;

            DefineOptionsList(Convert.ToInt32(cmbFXServiceProviders1.SelectedValue), 7, 1);
            cmbFXOptions1.SelectedValue = iFXOption1_ID;

            DefineOptionsList(Convert.ToInt32(cmbFXServiceProviders2.SelectedValue), 7, 2);
            cmbFXOptions2.SelectedValue = iFXOption2_ID;

            DefineOptionsList(Convert.ToInt32(cmbSettlementsServiceProviders.SelectedValue), 8, 1);
            cmbSettlementsOptions.SelectedValue = iSettlementsOption_ID;


            bCheckInvestPolicies = true;
            cmbAdvisoryInvestmentProfile.SelectedValue = iAdvisoryInvestment_Profile;
            cmbAdvisoryInvestmentPolicy.SelectedValue = iAdvisoryInvestment_Policy;
            bCheckList = true;

            bCheckInvestPolicies = true;
            cmbDealAdvisoryFinanceTools.SelectedValue = iDealAdvisoryInvestment_Policy;
            bCheckList = true;

            bCheckInvestPolicies = true;
            cmbDiscretInvestmentProfile.SelectedValue = iDiscretInvestment_Profile;
            cmbDiscretInvestmentPolicy.SelectedValue = iDiscretInvestment_Policy;
            bCheckList = true;
        }
        private void cmbBrokerageServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbBrokerageServiceProviders.SelectedValue), 1, 1);
        }

        private void cmbBrokerageOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                iBrokerageOption_ID = Convert.ToInt32(cmbBrokerageOptions.SelectedValue);
                fgBrokerageFees.Redraw = false;
                fgBrokerageFees.Rows.Count = 2;

                clsClientsBrokerageFees klsClientsBrokerageFees = new clsClientsBrokerageFees();
                klsClientsBrokerageFees.Option_ID = iBrokerageOption_ID;
                klsClientsBrokerageFees.DateFrom = DateTime.Now;
                klsClientsBrokerageFees.DateTo = DateTime.Now;
                klsClientsBrokerageFees.Contract_ID = 0;
                //klsClientsBrokerageFees.ClientFees = 0;                          // 0 - Don't add into table client's fees
                klsClientsBrokerageFees.GetList();
                foreach (DataRow dtRow in klsClientsBrokerageFees.List.Rows)
                {
                    fgBrokerageFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["StockExchanges_Title"] + "\t" +
                                    dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                    dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                    dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                    dtRow["ProductCategory_ID"] + "\t" + dtRow["ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
                fgBrokerageFees.Redraw = true;
            }
        }
        private void cmbRTOServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbRTOServiceProviders.SelectedValue), 9, 1);
        }

        private void cmbRTOOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {

                fgRTOFees.Redraw = false;
                fgRTOFees.Rows.Count = 2;

                iRTOOption_ID = Convert.ToInt32(cmbRTOOptions.SelectedValue);
                clsClientsRTOFees klsClientsRTOFees = new clsClientsRTOFees();
                klsClientsRTOFees.Option_ID = iRTOOption_ID;
                klsClientsRTOFees.DateFrom = DateTime.Now;
                klsClientsRTOFees.DateTo = DateTime.Now;
                klsClientsRTOFees.Contract_ID = 0;
                klsClientsRTOFees.GetList();
                foreach (DataRow dtRow in klsClientsRTOFees.List.Rows)
                {
                    fgRTOFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["StockExchanges_Title"] + "\t" +
                                    dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                    dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                    dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                    dtRow["ProductCategory_ID"] + "\t" + dtRow["ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
                fgRTOFees.Redraw = true;

            }
        }

        private void cmbFXServiceProviders1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbFXServiceProviders1.SelectedValue), 7, 1);
        }

        private void cmbFXOptions1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckOptions) {

                fgFXFees1.Redraw = false;
                fgFXFees1.Rows.Count = 2;

                klsClientsFXFees = new clsClientsFXFees();
                klsClientsFXFees.ServiceProvider_ID = Convert.ToInt32(cmbFXServiceProviders1.SelectedValue);
                klsClientsFXFees.Option_ID = Convert.ToInt32(cmbFXOptions1.SelectedValue);
                klsClientsFXFees.GetList();
                foreach (DataRow dtRow in klsClientsFXFees.List.Rows)
                {
                    fgFXFees1.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FXFees"] + "\t" + dtRow["ID"]);
                }
                fgFXFees1.Redraw = true;
            }
        }
        private void cmbFXServiceProviders2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbFXServiceProviders2.SelectedValue), 7, 1);
        }

        private void cmbFXOptions2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckOptions) {
                fgFXFees2.Redraw = false;
                fgFXFees2.Rows.Count = 2;

                klsClientsFXFees = new clsClientsFXFees();
                klsClientsFXFees.ServiceProvider_ID = Convert.ToInt32(cmbFXServiceProviders2.SelectedValue);
                klsClientsFXFees.Option_ID = Convert.ToInt32(cmbFXOptions2.SelectedValue);
                klsClientsFXFees.GetList();
                foreach (DataRow dtRow in klsClientsFXFees.List.Rows)
                {
                    fgFXFees2.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FXFees"] + "\t" + dtRow["ID"]);
                }
                fgFXFees2.Redraw = true;
            }
        }
        private void cmbCustodyServiceProviders1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbCustodyServiceProviders1.SelectedValue), 4, 1);
        }

        private void cmbCustodyOptions1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckOptions) {

                fgCustodyFees1.Redraw = false;
                fgCustodyFees1.Rows.Count = 2;
                lblCustodyMonthMinAmount1.Text = "";

                clsClientsCustodyFees klsClientsCustodyFees = new clsClientsCustodyFees();
                klsClientsCustodyFees.ServiceProvider_ID = Convert.ToInt32(cmbCustodyServiceProviders1.SelectedValue);
                klsClientsCustodyFees.Option_ID = Convert.ToInt32(cmbCustodyOptions1.SelectedValue);
                klsClientsCustodyFees.GetList_Provider_ID();
                foreach (DataRow dtRow in klsClientsCustodyFees.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["SPO_ID"]) == Convert.ToInt32(cmbCustodyOptions1.SelectedValue))
                       fgCustodyFees1.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" + dtRow["ID"]);
                }
                fgCustodyFees1.Redraw = true;
            }
        }

        private void cmbAdministrationServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbAdministrationServiceProviders.SelectedValue), 10, 1);
        }
        private void cmbAdministrationOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckOptions)  {

                fgAdminFees.Redraw = false;
                fgAdminFees.Rows.Count = 2;
                //lblCustodyMonthMinAmount1.Text = "";

                clsClientsAdminFees klsClientsAdminFees = new clsClientsAdminFees();
                klsClientsAdminFees.ServiceProvider_ID = Convert.ToInt32(cmbAdministrationServiceProviders.SelectedValue);
                klsClientsAdminFees.Option_ID = Convert.ToInt32(cmbAdministrationOptions.SelectedValue);
                klsClientsAdminFees.GetList_Provider_ID();
                foreach (DataRow dtRow in klsClientsAdminFees.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["SPO_ID"]) == Convert.ToInt32(cmbAdministrationOptions.SelectedValue))
                        fgAdminFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesPercent"] + "\t" + dtRow["ID"]);
                }
                fgAdminFees.Redraw = true;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            bCheckList = false;
            DefineList();
            bCheckList = true;
            txtFilter.Focus();
        }

        private void cmbSettlementsServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbSettlementsServiceProviders.SelectedValue), 8, 1);
        }

        private void cmbSettlementsOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                iSettlementsOption_ID = Convert.ToInt32(cmbSettlementsOptions.SelectedValue);
                fgSettlementsFees.Redraw = false;
                fgSettlementsFees.Rows.Count = 2;

                clsClientsSettlementFees klsClientsSettlementsFees = new clsClientsSettlementFees();
                klsClientsSettlementsFees.Option_ID = iSettlementsOption_ID;
                klsClientsSettlementsFees.DateFrom = DateTime.Now;
                klsClientsSettlementsFees.DateTo = DateTime.Now;
                klsClientsSettlementsFees.Contract_ID = 0;
                //klsClientsSettlementsFees.ClientFees = 0;                          // 0 - Don't add into table client's fees
                klsClientsSettlementsFees.GetList();
                foreach (DataRow dtRow in klsClientsSettlementsFees.List.Rows)
                {
                    fgSettlementsFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["Depositories_Title"] + "\t" +
                                    dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                    dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                    dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" + dtRow["ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                    dtRow["ProductCategory_ID"] + "\t" + dtRow["ID"] + "\t" + dtRow["Depositories_ID"]);
                }
                fgSettlementsFees.Redraw = true;
            }
        }

        private void cmbAdvisoryServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbAdvisoryServiceProviders.SelectedValue), 2, 1);
        }

        private void cmbAdvisoryOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineAdvisoryFeesList();
        }

        private void cmbAdvisoryInvestmentProfile_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineAdvisoryFeesList();
        }

        private void cmbAdvisoryInvestmentPolicy_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineAdvisoryFeesList();
        }
        private void DefineAdvisoryFeesList()
        {
            if (bCheckInvestPolicies) {

                fgAdvisoryFees.Redraw = false;
                fgAdvisoryFees.Rows.Count = 2;
                lblAdvisoryMonthMinAmount.Text = "";
                lblAdvisoryOpenAmount.Text = "";
                lblAdvisoryServiceAmount.Text = "";
                lblAdvisoryMinAmount.Text = "";

                clsClientsAdvisoryFees klsClientsAdvisoryFees = new clsClientsAdvisoryFees();
                klsClientsAdvisoryFees.ServiceProvider_ID = Convert.ToInt32(cmbAdvisoryServiceProviders.SelectedValue);
                klsClientsAdvisoryFees.Option_ID = Convert.ToInt32(cmbAdvisoryOptions.SelectedValue);
                klsClientsAdvisoryFees.InvestmentProfile_ID = Convert.ToInt32(cmbAdvisoryInvestmentProfile.SelectedValue);
                klsClientsAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(cmbAdvisoryInvestmentPolicy.SelectedValue);
                klsClientsAdvisoryFees.GetList();
                foreach (DataRow dtRow in klsClientsAdvisoryFees.List.Rows)
                {
                    fgAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdvisoryFees"] + "\t" + dtRow["ID"]);

                    lblAdvisoryMonthMinAmount.Text = dtRow["MonthMinAmount"] + " " + dtRow["MonthMinCurr"];
                    lblAdvisoryOpenAmount.Text = dtRow["OpenAmount"] + " " + dtRow["OpenCurr"];
                    lblAdvisoryServiceAmount.Text = dtRow["ServiceAmount"] + " " + dtRow["ServiceCurr"];
                    lblAdvisoryMinAmount.Text = dtRow["MinAmount"] + " " + dtRow["MinCurr"];
                }
                fgAdvisoryFees.Redraw = true;
            }
        }
        private void cmbDiscretServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbDiscretServiceProviders.SelectedValue), 3, 1);
        }

        private void cmbDiscretOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineDiscretFeesList();
        }

        private void cmbDiscretInvestmentProfile_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineDiscretFeesList();
        }

        private void cmbDiscretInvestmentPolicy_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineDiscretFeesList();
        }
        private void DefineDiscretFeesList()
        {
            if (bCheckInvestPolicies)
            {

                fgDiscretFees.Redraw = false;
                fgDiscretFees.Rows.Count = 2;
                lblDiscretMonthMinAmount.Text = "";
                lblDiscretOpenAmount.Text = "";
                lblDiscretServiceAmount.Text = "";
                lblDiscretMinAmount.Text = "";

                clsClientsDiscretFees klsClientsDiscretFees = new clsClientsDiscretFees();
                klsClientsDiscretFees.ServiceProvider_ID = Convert.ToInt32(cmbDiscretServiceProviders.SelectedValue);
                klsClientsDiscretFees.Option_ID = Convert.ToInt32(cmbDiscretOptions.SelectedValue);
                klsClientsDiscretFees.InvestmentProfile_ID = Convert.ToInt32(cmbDiscretInvestmentProfile.SelectedValue);
                klsClientsDiscretFees.InvestmentPolicy_ID = Convert.ToInt32(cmbDiscretInvestmentPolicy.SelectedValue);
                klsClientsDiscretFees.GetList();
                foreach (DataRow dtRow in klsClientsDiscretFees.List.Rows)
                {
                    fgDiscretFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DiscretFees"] + "\t" + dtRow["ID"]);

                    lblDiscretMonthMinAmount.Text = dtRow["MonthMinAmount"] + " " + dtRow["MonthMinCurr"];
                    lblDiscretOpenAmount.Text = dtRow["OpenAmount"] + " " + dtRow["OpenCurr"];
                    lblDiscretServiceAmount.Text = dtRow["ServiceAmount"] + " " + dtRow["ServiceCurr"];
                    lblDiscretMinAmount.Text = dtRow["MinAmount"] + " " + dtRow["MinCurr"];
                }
                fgDiscretFees.Redraw = true;
            }
        }
        private void cmbDealAdvisoryServiceProvide_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefineOptionsList(Convert.ToInt32(cmbDealAdvisoryServiceProviders.SelectedValue), 5, 1);
        }

        private void cmbDealAdvisoryOptions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckInvestPolicies) {

                fgDealAdvisoryFees.Redraw = false;
                fgDealAdvisoryFees.Rows.Count = 2;

                clsClientsDealAdvisoryFees klsClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                klsClientsDealAdvisoryFees.ServiceProvider_ID = Convert.ToInt32(cmbDealAdvisoryServiceProviders.SelectedValue);
                klsClientsDealAdvisoryFees.Option_ID = Convert.ToInt32(cmbDealAdvisoryOptions.SelectedValue);
                klsClientsDealAdvisoryFees.InvestmentPolicy_ID = Convert.ToInt32(cmbDealAdvisoryFinanceTools.SelectedValue);
                klsClientsDealAdvisoryFees.GetList();
                foreach (DataRow dtRow in klsClientsDealAdvisoryFees.List.Rows)
                {
                    fgDealAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FeesAmount"] + "\t" + dtRow["FeesCurr"] + "\t" +
                                           dtRow["YperReturn"] + "\t" + dtRow["Variable1"] + "\t" + dtRow["Variable2"] + "\t" +
                                           dtRow["ID"]);

                    lblDealAdvisoryMonthMinAmount.Text = dtRow["MonthMinAmount"] + " " + dtRow["MonthMinCurr"];
                    lblDealAdvisoryOpenAmount.Text = dtRow["OpenAmount"] + " " + dtRow["OpenCurr"];
                    lblDealAdvisoryServiceAmount.Text = dtRow["ServiceAmount"] + " " + dtRow["ServiceCurr"];
                    lblDealAdvisoryMinAmount.Text = dtRow["MinAmount"] + " " + dtRow["MinCurr"];

                }
                fgDealAdvisoryFees.Redraw = true;
            }
        }

        private void cmbDealAdvisoryFinanceTools_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbLombardServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbLombardOptions_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void DefineOptionsList(int iServiceProvider, int iServiceType, int iListNumber)
        {
            if (bCheckOptions) {

                bCheckList = false;

                //-------------- Define Options List of searched Service Provider ------------------
                klsServiceProvidersOptions = new clsServiceProvidersOptions();
                klsServiceProvidersOptions.ServiceProvider_ID = iServiceProvider;
                klsServiceProvidersOptions.ServiceType_ID = iServiceType;
                klsServiceProvidersOptions.GetList();


                bCheckOptions = false;
                switch (iServiceType)
                {
                    case 1:
                        cmbBrokerageOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbBrokerageOptions.DisplayMember = "Title";
                        cmbBrokerageOptions.ValueMember = "ID";

                        fgBrokerageFees.Rows.Count = 2;
                        break;
                    case 2:
                        cmbAdvisoryOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbAdvisoryOptions.DisplayMember = "Title";
                        cmbAdvisoryOptions.ValueMember = "ID";

                        fgAdvisoryFees.Rows.Count = 2;
                        lblAdvisoryMonthMinAmount.Text = "";
                        lblAdvisoryMinAmount.Text = "";
                        lblAdvisoryOpenAmount.Text = "";
                        lblAdvisoryServiceAmount.Text = "";
                        break;
                    case 3:
                        cmbDiscretOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbDiscretOptions.DisplayMember = "Title";
                        cmbDiscretOptions.ValueMember = "ID";

                        fgDiscretFees.Rows.Count = 2;
                        lblDiscretMonthMinAmount.Text = "";
                        lblDiscretMinAmount.Text = "";
                        lblDiscretOpenAmount.Text = "";
                        lblDiscretServiceAmount.Text = "";
                        break;
                    case 4:
                        if (iListNumber == 1) {
                            cmbCustodyOptions1.DataSource = klsServiceProvidersOptions.List;
                            cmbCustodyOptions1.DisplayMember = "Title";
                            cmbCustodyOptions1.ValueMember = "ID";

                            fgCustodyFees1.Rows.Count = 2;
                            lblCustodyMonthMinAmount1.Text = "";
                        }
                        else {
                            cmbCustodyOptions2.DataSource = klsServiceProvidersOptions.List;
                            cmbCustodyOptions2.DisplayMember = "Title";
                            cmbCustodyOptions2.ValueMember = "ID";

                            fgCustodyFees2.Rows.Count = 2;
                            lblCustodyMonthMinAmount2.Text = "";
                        }
                        break;
                    case 5:
                        cmbDealAdvisoryOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbDealAdvisoryOptions.DisplayMember = "Title";
                        cmbDealAdvisoryOptions.ValueMember = "ID";

                        fgDealAdvisoryFees.Rows.Count = 2;
                        lblDealAdvisoryMonthMinAmount.Text = "";
                        lblDealAdvisoryMinAmount.Text = "";
                        lblDealAdvisoryOpenAmount.Text = "";
                        lblDealAdvisoryServiceAmount.Text = "";
                        break;
                    case 6:
                        cmbLombardOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbLombardOptions.DisplayMember = "Title";
                        cmbLombardOptions.ValueMember = "ID";

                        fgLombardFees.Rows.Count = 1;
                        break;
                    case 7:
                        if (iListNumber == 1) {
                            cmbFXOptions1.DataSource = klsServiceProvidersOptions.List;
                            cmbFXOptions1.DisplayMember = "Title";
                            cmbFXOptions1.ValueMember = "ID";

                            fgFXFees1.Rows.Count = 2;
                        }
                        else {
                            cmbFXOptions2.DataSource = klsServiceProvidersOptions.List;
                            cmbFXOptions2.DisplayMember = "Title";
                            cmbFXOptions2.ValueMember = "ID";

                            fgFXFees2.Rows.Count = 2;
                        }
                        break;
                    case 8:
                        cmbSettlementsOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbSettlementsOptions.DisplayMember = "Title";
                        cmbSettlementsOptions.ValueMember = "ID";

                        fgSettlementsFees.Rows.Count = 2;
                        break;
                    case 9:
                        cmbRTOOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbRTOOptions.DisplayMember = "Title";
                        cmbRTOOptions.ValueMember = "ID";

                        fgRTOFees.Rows.Count = 2;
                        break;
                    case 10:
                        cmbAdministrationOptions.DataSource = klsServiceProvidersOptions.List;
                        cmbAdministrationOptions.DisplayMember = "Title";
                        cmbAdministrationOptions.ValueMember = "ID";

                        fgAdminFees.Rows.Count = 2;
                        lblAdminMonthMinAmount.Text = "";
                        break;
                }
                bCheckOptions = true;
                bCheckList = true;
            }            
        }
        private void ChangeMode(int iStatus)
        {
            switch (iStatus)
            {
                case 1:
                    toolLeft.Enabled = true;
                    fgList.Enabled = true;

                    toolRight.Enabled = false;
                    toolBrokerageFees.Enabled = false;
                    toolRTOFees.Enabled = false;
                    toolAdvisoryFees.Enabled = false;
                    toolDealAdvisoryFees.Enabled = false;
                    toolCustodyFees1.Enabled = false;
                    toolCustodyFees2.Enabled = false;
                    toolAdminFees.Enabled = false;
                    toolDiscretFees.Enabled = false;
                    toolLombardFees.Enabled = false;
                    toolSettlementsFees.Enabled = false;
                    toolFXFees1.Enabled = false;
                    toolFXFees2.Enabled = false;

                    cmbBrokerageServiceProviders.Enabled = false;
                    cmbBrokerageOptions.Enabled = false;
                    cmbRTOServiceProviders.Enabled = false;
                    cmbRTOOptions.Enabled = false;
                    cmbAdvisoryServiceProviders.Enabled = false;
                    cmbAdvisoryInvestmentProfile.Enabled = false;
                    cmbAdvisoryInvestmentPolicy.Enabled = false;
                    cmbAdvisoryOptions.Enabled = false;
                    cmbDealAdvisoryServiceProviders.Enabled = false;
                    cmbDealAdvisoryFinanceTools.Enabled = false;
                    cmbDealAdvisoryOptions.Enabled = false;
                    cmbCustodyServiceProviders1.Enabled = false;
                    cmbCustodyOptions1.Enabled = false;
                    cmbCustodyServiceProviders2.Enabled = false;
                    cmbCustodyOptions2.Enabled = false;
                    cmbAdministrationServiceProviders.Enabled = false;
                    cmbAdministrationOptions.Enabled = false;
                    cmbCustodyOptions1.Enabled = false;
                    cmbCustodyServiceProviders2.Enabled = false;
                    cmbCustodyOptions2.Enabled = false;
                    cmbDiscretInvestmentProfile.Enabled = false;
                    cmbDiscretServiceProviders.Enabled = false;
                    cmbDiscretInvestmentPolicy.Enabled = false;
                    cmbDiscretOptions.Enabled = false;
                    cmbLombardServiceProviders.Enabled = false;
                    cmbLombardOptions.Enabled = false;
                    cmbFXServiceProviders1.Enabled = false;
                    cmbFXServiceProviders2.Enabled = false;
                    cmbFXOptions1.Enabled = false;
                    cmbFXOptions2.Enabled = false;
                    cmbSettlementsServiceProviders.Enabled = false;
                    cmbSettlementsOptions.Enabled = false;

                    fgList.Focus();
                    ShowRecord();
                    break;
                case 2:
                    toolLeft.Enabled = false;
                    fgList.Enabled = false;

                    toolRight.Enabled = true;
                    toolBrokerageFees.Enabled = true;
                    toolRTOFees.Enabled = true;
                    toolAdvisoryFees.Enabled = true;
                    toolDealAdvisoryFees.Enabled = true;
                    toolCustodyFees1.Enabled = true;
                    toolCustodyFees2.Enabled = true;
                    toolAdminFees.Enabled = true;
                    toolDiscretFees.Enabled = true;
                    toolLombardFees.Enabled = true;
                    toolSettlementsFees.Enabled = true;
                    toolFXFees1.Enabled = true;
                    toolFXFees2.Enabled = true;

                    cmbBrokerageServiceProviders.Enabled = true;
                    cmbBrokerageOptions.Enabled = true;
                    cmbRTOServiceProviders.Enabled = true;
                    cmbRTOOptions.Enabled = true;
                    cmbAdvisoryServiceProviders.Enabled = true;
                    cmbAdvisoryInvestmentProfile.Enabled = true;
                    cmbAdvisoryInvestmentPolicy.Enabled = true;
                    cmbAdvisoryOptions.Enabled = true;
                    cmbDealAdvisoryServiceProviders.Enabled = true;
                    cmbDealAdvisoryFinanceTools.Enabled = true;
                    cmbDealAdvisoryOptions.Enabled = true;
                    cmbCustodyServiceProviders1.Enabled = true;
                    cmbCustodyOptions1.Enabled = true;
                    cmbCustodyServiceProviders2.Enabled = true;
                    cmbCustodyOptions2.Enabled = true;
                    cmbAdministrationServiceProviders.Enabled = true;
                    cmbAdministrationOptions.Enabled = true;
                    cmbCustodyOptions1.Enabled = true;
                    cmbCustodyServiceProviders2.Enabled = true;
                    cmbCustodyOptions2.Enabled = true;
                    cmbDiscretServiceProviders.Enabled = true;
                    cmbDiscretInvestmentProfile.Enabled = true;
                    cmbDiscretInvestmentPolicy.Enabled = true;
                    cmbDiscretOptions.Enabled = true;
                    cmbLombardServiceProviders.Enabled = true;
                    cmbLombardOptions.Enabled = true;
                    cmbFXServiceProviders1.Enabled = true;
                    cmbFXServiceProviders2.Enabled = true;
                    cmbFXOptions1.Enabled = true;
                    cmbFXOptions2.Enabled = true;
                    cmbSettlementsServiceProviders.Enabled = true;
                    cmbSettlementsOptions.Enabled = true;
                    break;
            }        
        }
        private void EmptyDetails()
        {
            iID = 0;
            txtTitle.Text = "";
            cmbServiceProviders.SelectedValue = 0;
            cmbBusinessType.SelectedIndex = 1;
            cmbFinanceServices.SelectedValue = 0;
            cmbClientTipos.SelectedIndex = 0;
            dStart.Value = DateTime.Now.AddDays(-1);
            dFinish.Value = Convert.ToDateTime("2070/12/31");
            txtNotes.Text = "";
            chkMIIFID_2.Checked = false;

            cmbBrokerageServiceProviders.SelectedValue = 0;
            cmbBrokerageOptions.SelectedValue = 0;
            fgBrokerageFees.Rows.Count = 2;

            cmbRTOServiceProviders.SelectedValue = 0;
            cmbRTOOptions.SelectedValue = 0;
            fgRTOFees.Rows.Count = 2;

            cmbAdvisoryServiceProviders.SelectedValue = 0;
            cmbAdvisoryInvestmentPolicy.SelectedValue = 0;
            lblAdvisoryMonthMinAmount.Text = "";
            lblAdvisoryOpenAmount.Text = "";
            lblAdvisoryServiceAmount.Text = "";
            lblAdvisoryMinAmount.Text = "";
            fgAdvisoryFees.Rows.Count = 2;

            cmbDiscretServiceProviders.SelectedValue = 0;
            cmbDiscretInvestmentPolicy.SelectedValue = 0;
            lblDiscretMonthMinAmount.Text = "";
            lblDiscretOpenAmount.Text = "";
            lblDiscretServiceAmount.Text = "";
            lblDiscretMinAmount.Text = "";
            fgDiscretFees.Rows.Count = 2;

            cmbCustodyServiceProviders1.SelectedValue = 0;
            lblCustodyMonthMinAmount1.Text = "";
            fgCustodyFees1.Rows.Count = 2;

            cmbCustodyServiceProviders2.SelectedValue = 0;
            lblCustodyMonthMinAmount2.Text = "";
            fgCustodyFees2.Rows.Count = 2;

            cmbAdministrationServiceProviders.SelectedValue = 0;
            lblAdminMonthMinAmount.Text = "";
            fgAdminFees.Rows.Count = 2;

            cmbDealAdvisoryServiceProviders.SelectedValue = 0;
            cmbDealAdvisoryFinanceTools.SelectedValue = 0;
            lblDealAdvisoryMonthMinAmount.Text = "";
            lblDealAdvisoryOpenAmount.Text = "";
            lblDealAdvisoryServiceAmount.Text = "";
            lblDealAdvisoryMinAmount.Text = "";
            fgDealAdvisoryFees.Rows.Count = 2;

            cmbLombardOptions.SelectedValue = 0;
            cmbLombardServiceProviders.SelectedValue = 0;
            lblAMR.Text = "";
            fgLombardFees.Rows.Count = 1;

            cmbFXServiceProviders1.SelectedValue = 0;
            fgFXFees1.Rows.Count = 2;

            cmbFXServiceProviders2.SelectedValue = 0;
            fgFXFees2.Rows.Count = 2;

            cmbSettlementsServiceProviders.SelectedValue = 0;
            cmbSettlementsOptions.SelectedValue = 0;
            fgSettlementsFees.Rows.Count = 2;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
