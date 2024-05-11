using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using C1.Win.C1FlexGrid;
using System.Collections;

namespace Core
{
    public partial class ucContract : UserControl
    {
        DataTable dtList, dtPackages, dtAdvisoryFeesDiscounts, dtDiscretFeesDiscounts, dtCustodyFeesDiscounts, dtDealAdvisoryFeesDiscounts, dtBankAccs;
        DataView dtView;
        DataRow[] foundRows;
        DataRow dtRow;
        DataColumn dtCol;
        int i, iRec_ID, jAktion, iMode, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iPackageVersion, iRightsLevel,
            iBrokerageOption_ID, iRTOOption_ID, iAdvisoryOption_ID, iAdvisoryProvider_ID, iCustodyOption_ID, iCustodyProvider_ID, iDocFiles_ID,
            iAdvisoryInvestmentProfile_ID, iAdvisoryInvestmentPolicy_ID, iDiscretOption_ID, iDiscretProvider_ID, iLombardOption_ID, iAdminProvider_ID, iAdminOption_ID,
            iDiscretInvestmentProfile_ID, iDiscretInvestmentPolicy_ID, iLombardProvider_ID, iFXOption_ID, iFXProvider_ID, iSettlementsOption_ID,
            iDealAdvisoryProvider_ID, iDealAdvisoryOption_ID, iDealAdvisoryInvestmentPolicy_ID, iNewPackage_ID, iOldContract_ID, iOldContract_Packages_ID;
        string sTemp, sFileName, sFullFileName;
        DateTime dTemp;
        bool bCheckPackages;
        C1.Win.C1FlexGrid.CellRange rng;
        CellStyle csAdvisoryDiscount, csAdvisoryFinish, csDiscretDiscount, csDiscretFinish;
        SortedList lstCurr = new SortedList();
        public ucContract()
        {
            InitializeComponent();
            panEdit_BankAccount.Left = 146;
            panEdit_BankAccount.Top = 50;

            panChangePackage.Left = 456;
            panChangePackage.Top = 36;

            panNotes.Left = 688;
            panNotes.Top = 114;
        }

        private void ucContract_Load(object sender, EventArgs e)
        {
            if (iContract_ID < 0)  {
                iContract_ID = 0;
                iPackageVersion = 0;
                iClient_ID = 0;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            tcFees.Height = this.Height - 236;
            fgBrokerageFees.Height = tcFees.Height - 80;
            fgRTOFees.Height = tcFees.Height - 80;
            fgFXFees.Height = tcFees.Height - 108;
            fgCustodyFees.Height = tcFees.Height - 156;
            fgAdminFees.Height = tcFees.Height - 106;
            fgSettlementsFees.Height = tcFees.Height - 108;
            fgAdvisoryFees.Height = tcFees.Height - 134;
            fgDiscretFees.Height = tcFees.Height - 134;
            fgDealAdvisoryFees.Height = tcFees.Height - 134;
            fgPerformFees.Height = tcFees.Height - 108;
            fgLombardFees.Height = tcFees.Height - 124;
            fgCashAccounts.Height = tcFees.Height - 82;
            fgBankAccounts.Height = tcFees.Height - 82;
        }
        public void ShowRecord(int iPackageType, int iReal_ID_Param, int iClient_ID_Param, int iContract_ID_Param, int iContract_Details_ID_Param,
                               int iContract_Packages_ID_Param, int iPackageVersion_Param, int iRightsLevel_Param)
        {
            bCheckPackages = false;

            cmbInvestmentPolicy.Enabled = false;
            tsbKey.Visible = true;
            tsbSave.Visible = false;

            lblRealClient_ID.Text = iReal_ID_Param.ToString();
            iClient_ID = iClient_ID_Param;
            iContract_ID = iContract_ID_Param;
            iContract_Details_ID = iContract_Details_ID_Param;
            iContract_Packages_ID = iContract_Packages_ID_Param;
            iPackageVersion = iPackageVersion_Param;
            iRightsLevel = iRightsLevel_Param;

            dPackageDateStart.Value = DateTime.Now;
            dPackageDateFinish.Value = Convert.ToDateTime("31/12/2070");

            //-------------- Define Investment Policies List ------------------    
            cmbInvestmentPolicy.DataSource = Global.dtInvestPolicies.Copy();
            cmbInvestmentPolicy.DisplayMember = "Title";
            cmbInvestmentPolicy.ValueMember = "ID";

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


            //------- fgRTOFees ----------------------------;
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

            fgAdvisoryFees.Cols[2].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgAdvisoryFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgAdvisoryFees[1, 3] = "Ημερ.από";
            fgAdvisoryFees[1, 4] = "Ημερ.εώς";
            fgAdvisoryFees[1, 5] = "% Έκπτωσης";

            fgAdvisoryFees.Cols[6].AllowMerging = true;
            rng = fgAdvisoryFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            csAdvisoryDiscount = fgAdvisoryFees.Styles.Add("Discount");
            csAdvisoryDiscount.BackColor = Color.PeachPuff;

            csAdvisoryFinish = fgAdvisoryFees.Styles.Add("Finish");
            csAdvisoryFinish.BackColor = Color.LightGreen;

            fgAdvisoryFees.Cols[3].Style = csAdvisoryDiscount;
            fgAdvisoryFees.Cols[4].Style = csAdvisoryDiscount;
            fgAdvisoryFees.Cols[5].Style = csAdvisoryDiscount;
            fgAdvisoryFees.Cols[8].Style = csAdvisoryDiscount;
            fgAdvisoryFees.Cols[9].Style = csAdvisoryDiscount;
            fgAdvisoryFees.Cols[10].Style = csAdvisoryDiscount;

            fgAdvisoryFees.Cols[6].Style = csAdvisoryFinish;
            fgAdvisoryFees.Cols[11].Style = csAdvisoryFinish;

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

            fgDiscretFees.Cols[2].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgDiscretFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgDiscretFees[1, 3] = "Ημερ.από";
            fgDiscretFees[1, 4] = "Ημερ.εώς";
            fgDiscretFees[1, 5] = "% Έκπτωσης";

            fgDiscretFees.Cols[6].AllowMerging = true;
            rng = fgDiscretFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            csDiscretDiscount = fgDiscretFees.Styles.Add("Discount");
            csDiscretDiscount.BackColor = Color.PeachPuff;

            csDiscretFinish = fgDiscretFees.Styles.Add("Finish");
            csDiscretFinish.BackColor = Color.LightGreen;

            fgDiscretFees.Cols[3].Style = csDiscretDiscount;
            fgDiscretFees.Cols[4].Style = csDiscretDiscount;
            fgDiscretFees.Cols[5].Style = csDiscretDiscount;
            fgDiscretFees.Cols[8].Style = csDiscretDiscount;
            fgDiscretFees.Cols[9].Style = csDiscretDiscount;
            fgDiscretFees.Cols[10].Style = csDiscretDiscount;

            fgDiscretFees.Cols[6].Style = csDiscretFinish;
            fgDiscretFees.Cols[11].Style = csDiscretFinish;

            //------- fgCustodyFees ----------------------------
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

            csAdvisoryDiscount = fgCustodyFees.Styles.Add("Discount");
            csAdvisoryDiscount.BackColor = Color.PeachPuff;

            csAdvisoryFinish = fgCustodyFees.Styles.Add("Finish");
            csAdvisoryFinish.BackColor = Color.LightGreen;

            fgCustodyFees.Cols[3].Style = csAdvisoryDiscount;
            fgCustodyFees.Cols[4].Style = csAdvisoryDiscount;
            fgCustodyFees.Cols[5].Style = csAdvisoryDiscount;

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

            csAdvisoryDiscount = fgAdminFees.Styles.Add("Discount");
            csAdvisoryDiscount.BackColor = Color.PeachPuff;

            csAdvisoryFinish = fgAdminFees.Styles.Add("Finish");
            csAdvisoryFinish.BackColor = Color.LightGreen;

            fgAdminFees.Cols[3].Style = csAdvisoryDiscount;
            fgAdminFees.Cols[4].Style = csAdvisoryDiscount;
            fgAdminFees.Cols[5].Style = csAdvisoryDiscount;


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

            csAdvisoryDiscount = fgDealAdvisoryFees.Styles.Add("Discount");
            csAdvisoryDiscount.BackColor = Color.PeachPuff;

            csAdvisoryFinish = fgDealAdvisoryFees.Styles.Add("Finish");
            csAdvisoryFinish.BackColor = Color.LightGreen;

            fgDealAdvisoryFees.Cols[3].Style = csAdvisoryDiscount;
            fgDealAdvisoryFees.Cols[4].Style = csAdvisoryDiscount;
            fgDealAdvisoryFees.Cols[5].Style = csAdvisoryDiscount;

            fgDealAdvisoryFees.Cols[6].Style = csAdvisoryFinish;

            //------- fgLombardFees ----------------------------
            fgLombardFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgLombardFees.Styles.ParseString(Global.GridStyle);

            //------- fgFXFees ----------------------------
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
            fgBankAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBankAccounts.Styles.ParseString(Global.GridStyle);
            fgBankAccounts.DoubleClick += new System.EventHandler(fgBankAccounts_DoubleClick);

            lstCurr.Clear();
            foreach (DataRow dtRow in Global.dtCurrencies.Rows) lstCurr.Add(dtRow["Title"], dtRow["Title"]);
            fgCashAccounts.Cols[4].DataMap = lstCurr;

            clsCompanyPackages klsCompanyPackage = new clsCompanyPackages();
            klsCompanyPackage.Provider_ID = 0;
            klsCompanyPackage.PackageType_ID = 0;
            klsCompanyPackage.BusinessType_ID = 0;
            klsCompanyPackage.CheckActuality = 0;
            klsCompanyPackage.ActualDate = DateTime.Now;
            klsCompanyPackage.Title = "";
            klsCompanyPackage.GetList();
            dtPackages = klsCompanyPackage.List.Copy();

            cmbNewPackage.DataSource = dtPackages.Copy();
            cmbNewPackage.DisplayMember = "TitleFull";
            cmbNewPackage.ValueMember = "ID";

            //----- initialize Company Packages List -------
            dtView = dtPackages.Copy().DefaultView;
            cmbCompanyPackages.DataSource = dtView;
            cmbCompanyPackages.DisplayMember = "TitleFull";
            cmbCompanyPackages.ValueMember = "ID";
            cmbCompanyPackages.SelectedValue = 0;

            //--------------------------------------------------------------------------
            clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();
            Clients_BankAccounts.Client_ID = iClient_ID;
            Clients_BankAccounts.GetList();
            dtBankAccs = Clients_BankAccounts.List.Copy();
            cmbBankAccounts.DataSource = dtBankAccs;
            cmbBankAccounts.DisplayMember = "AccNumber";
            cmbBankAccounts.ValueMember = "ID";

            //---------------------------------------------------------
            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";

            //---------------------------------------------------------
            cmbDocTypes_Notes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes_Notes.DisplayMember = "Title";
            cmbDocTypes_Notes.ValueMember = "ID";

            //-------------- Define FinanceServices List ------------------
            cmbFinanceServices.DataSource = Global.dtServices.Copy();
            cmbFinanceServices.DisplayMember = "Title";
            cmbFinanceServices.ValueMember = "ID";

            //-------------- Define Clients Profiles List ------------------    
            cmbProfile.DataSource = Global.dtCustomersProfiles.Copy();
            cmbProfile.DisplayMember = "Title";
            cmbProfile.ValueMember = "ID";

            if (iContract_ID != 0)  {
                this.Text = "Σύμβαση (" + iContract_ID + ")";
                //----- initialize Contract Data -------
                ShowContractData();
                ShowGridsData(true);
                ShowBankAccountsList();
                ShowCashAccounts();
                ShowFileldsOnOff(false, false);
            }
            else ShowFileldsOnOff(true, false);

            bCheckPackages = true;
        } 
        public void EditPackage()
        {
            lblEditMode.Text = "4";
            ShowFileldsOnOff(false, false);

            DefinePackagesList(2);
            cmbCurPackage.SelectedValue = (Global.IsNumeric(cmbCompanyPackages.SelectedValue) ? cmbCompanyPackages.SelectedValue : 0);
            cmbCurPackage.Enabled = false;
            dCurPackageDateStart.Value = dPackageDateStart.Value;
            dCurPackageDateStart.Enabled = false;
            if (dCurPackageDateStart.Value <= DateTime.Now) dCurPackageDateFinish.Value = DateTime.Now;
            else dCurPackageDateFinish.Value = dCurPackageDateStart.Value;
            dCurPackageDateFinish.MinDate = dCurPackageDateFinish.Value;
            dCurPackageDateFinish.Enabled = true;

            cmbNewPackage.SelectedValue = 0;
            dNewPackageDateStart.Value = dCurPackageDateFinish.Value.AddDays(1);
            dNewPackageDateStart.MinDate = dNewPackageDateStart.Value;
            dNewPackageDateStart.Enabled = true;
            dNewPackageDateFinish.Value = Convert.ToDateTime("31/12/2070");
            dNewPackageDateFinish.Enabled = true;

            panChangePackage.Visible = true;
        }
        public void EditPackageVersion()
        {
            string sPackageVersionFilter;
            int i;

            lblEditMode.Text = "5";
            ShowFileldsOnOff(false, false);

            DefinePackagesList(2);

            sPackageVersionFilter = "";
            foundRows = dtPackages.Select("ID = " + cmbCompanyPackages.SelectedValue);
            if (foundRows.Length > 0) sPackageVersionFilter = foundRows[0]["Title"] + "";

            dtView = dtPackages.Copy().DefaultView;
            dtView.RowFilter = "Title = '" + sPackageVersionFilter + "'";
            cmbNewPackage.DataSource = dtView;
            cmbNewPackage.DisplayMember = "TitleFull";
            cmbNewPackage.ValueMember = "ID";
            cmbNewPackage.SelectedValue = 0;

            cmbCurPackage.SelectedValue = (Global.IsNumeric(cmbCompanyPackages.SelectedValue) ? cmbCompanyPackages.SelectedValue : 0);
            cmbCurPackage.Enabled = false;
            dCurPackageDateStart.Value = dPackageDateStart.Value;
            dCurPackageDateStart.Enabled = false;
            if (dCurPackageDateStart.Value <= DateTime.Now) dCurPackageDateFinish.Value = DateTime.Now;
            dCurPackageDateFinish.Value = dCurPackageDateStart.Value;
            dCurPackageDateFinish.MinDate = dCurPackageDateFinish.Value;
            dCurPackageDateFinish.Enabled = true;

            cmbNewPackage.SelectedValue = 0;
            dNewPackageDateStart.Value = dCurPackageDateFinish.Value.AddDays(1);
            dNewPackageDateStart.MinDate = dNewPackageDateStart.Value;
            dNewPackageDateStart.Enabled = true;
            dNewPackageDateFinish.Value = Convert.ToDateTime("31/12/2070");
            dNewPackageDateFinish.Enabled = true;

            dtAdvisoryFeesDiscounts = new DataTable("AdvisoryFeesDiscounts");
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("DateFrom", System.Type.GetType("System.String"));
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("DateTo", System.Type.GetType("System.String"));
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("FeesDiscount", System.Type.GetType("System.Single"));
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("YR_DateFrom", System.Type.GetType("System.String"));
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("YR_DateTo", System.Type.GetType("System.String"));
            dtCol = dtAdvisoryFeesDiscounts.Columns.Add("YR_Discount", System.Type.GetType("System.Single"));
            for (i = 2; i <= fgAdvisoryFees.Rows.Count - 1; i++)
            {
                dtRow = dtAdvisoryFeesDiscounts.NewRow();
                dtRow["DateFrom"] = fgAdvisoryFees[i, 3];
                dtRow["DateTo"] = fgAdvisoryFees[i, 4];
                dtRow["FeesDiscount"] = fgAdvisoryFees[i, 5];
                dtRow["YR_DateFrom"] = fgAdvisoryFees[i, 8];
                dtRow["YR_DateTo"] = fgAdvisoryFees[i, 9];
                dtRow["YR_Discount"] = fgAdvisoryFees[i, 10];
                dtAdvisoryFeesDiscounts.Rows.Add(dtRow);
            }

            dtDiscretFeesDiscounts = new DataTable("DiscretFeesDiscounts");
            dtCol = dtDiscretFeesDiscounts.Columns.Add("DateFrom", System.Type.GetType("System.String"));
            dtCol = dtDiscretFeesDiscounts.Columns.Add("DateTo", System.Type.GetType("System.String"));
            dtCol = dtDiscretFeesDiscounts.Columns.Add("FeesDiscount", System.Type.GetType("System.Single"));
            dtCol = dtDiscretFeesDiscounts.Columns.Add("YR_DateFrom", System.Type.GetType("System.String"));
            dtCol = dtDiscretFeesDiscounts.Columns.Add("YR_DateTo", System.Type.GetType("System.String"));
            dtCol = dtDiscretFeesDiscounts.Columns.Add("YR_Discount", System.Type.GetType("System.Single"));
            for (i = 2; i <= fgDiscretFees.Rows.Count - 1; i++)
            {
                dtRow = dtDiscretFeesDiscounts.NewRow();
                dtRow["DateFrom"] = fgDiscretFees[i, 3];
                dtRow["DateTo"] = fgDiscretFees[i, 4];
                dtRow["FeesDiscount"] = fgDiscretFees[i, 5];
                dtRow["YR_DateFrom"] = fgDiscretFees[i, 8];
                dtRow["YR_DateTo"] = fgDiscretFees[i, 9];
                dtRow["YR_Discount"] = fgDiscretFees[i, 10];
                dtDiscretFeesDiscounts.Rows.Add(dtRow);
            }

            dtCustodyFeesDiscounts = new DataTable("CustodyFeesDiscounts");
            dtCol = dtCustodyFeesDiscounts.Columns.Add("DateFrom", System.Type.GetType("System.String"));
            dtCol = dtCustodyFeesDiscounts.Columns.Add("DateTo", System.Type.GetType("System.String"));
            dtCol = dtCustodyFeesDiscounts.Columns.Add("FeesDiscount", System.Type.GetType("System.Single"));
            for (i = 2; i <= fgCustodyFees.Rows.Count - 1; i++)
            {
                dtRow = dtCustodyFeesDiscounts.NewRow();
                dtRow["DateFrom"] = fgCustodyFees[i, 3];
                dtRow["DateTo"] = fgCustodyFees[i, 4];
                dtRow["FeesDiscount"] = fgCustodyFees[i, 5];
                dtCustodyFeesDiscounts.Rows.Add(dtRow);
            }

            dtDealAdvisoryFeesDiscounts = new DataTable("DealAdvisoryFeesDiscounts");
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("DateFrom", System.Type.GetType("System.String"));
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("DateTo", System.Type.GetType("System.String"));
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("FeesDiscount", System.Type.GetType("System.Single"));
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("YR_DateFrom", System.Type.GetType("System.String"));
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("YR_DateTo", System.Type.GetType("System.String"));
            dtCol = dtDealAdvisoryFeesDiscounts.Columns.Add("YR_Discount", System.Type.GetType("System.Single"));
            for (i = 2; i <= fgDealAdvisoryFees.Rows.Count - 1; i++)
            {
                dtRow = dtDealAdvisoryFeesDiscounts.NewRow();
                dtRow["DateFrom"] = fgDealAdvisoryFees[i, 3];
                dtRow["DateTo"] = fgDealAdvisoryFees[i, 4];
                dtRow["FeesDiscount"] = fgDealAdvisoryFees[i, 5];
                dtRow["YR_DateFrom"] = fgDealAdvisoryFees[i, 8];
                dtRow["YR_DateTo"] = fgDealAdvisoryFees[i, 9];
                dtRow["YR_Discount"] = fgDealAdvisoryFees[i, 10];
               dtDealAdvisoryFeesDiscounts.Rows.Add(dtRow);
            }

           panChangePackage.Visible = true;
        }
        private void DefinePackagesList(int iMode)
        {
            //----- initialize Service Packages List -------
            if (bCheckPackages) {
                dtList = new DataTable("PackagesList");
                dtCol = dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = dtList.Columns.Add("PackageType_ID", System.Type.GetType("System.Int32"));

                foreach (DataRow dtRow1 in dtPackages.Rows)
                {
                    if ( (Convert.ToInt32(dtRow1["ID"]) == 0) ||
                         ((Convert.ToDateTime(dtRow1["DateStart"]) <= dPackageDateStart.Value) && Convert.ToDateTime(dtRow1["DateFinish"]) >= dPackageDateStart.Value))  { 
                        dtRow = dtList.NewRow();
                        dtRow["ID"] = dtRow1["ID"];
                        dtRow["Title"] = dtRow1["TitleFull"];
                        dtRow["PackageType_ID"] = dtRow1["PackageType_ID"];
                        dtList.Rows.Add(dtRow);
                    }
                }

                if (iMode == 1) {
                    bCheckPackages = false;
                    dtView = dtList.Copy().DefaultView;
                    if (Convert.ToInt32(cmbFinanceServices.SelectedValue) > 0)  dtView.RowFilter = "PackageType_ID = 0 OR PackageType_ID = " + cmbFinanceServices.SelectedValue;
                    else dtView.RowFilter = "PackageType_ID >= 0";
                    cmbCompanyPackages.DataSource = dtView;
                    cmbCompanyPackages.DisplayMember = "Title";
                    cmbCompanyPackages.ValueMember = "ID";
                    bCheckPackages = true;
                }

                cmbCurPackage.DataSource = dtPackages.Copy();
                cmbCurPackage.DisplayMember = "TitleFull";
                cmbCurPackage.ValueMember = "ID";

                cmbNewPackage.DataSource = dtPackages.Copy();
                cmbNewPackage.DisplayMember = "TitleFull";
                cmbNewPackage.ValueMember = "ID";
            }
        }
        private void btnChangeOK_Click(object sender, EventArgs e)
        {
            string sError = "";

            if (Convert.ToInt32(cmbNewPackage.SelectedValue) == 0) sError = "Επιλέξτε νέο πακέτο" + "\n";
            if (dCurPackageDateFinish.Text == "") sError = sError + "Καταχωρήστε την ημερομηνία λήξης του τρέχων πακέτου" + "\n";
            if (dNewPackageDateStart.Text.Trim() == "") sError = sError + "Καταχωρήστε την ημερομηνία έναρξης του νέου πακέτου" + "\n";
            if (dNewPackageDateFinish.Text.Trim() == "") sError = sError + "Καταχωρήστε την ημερομηνία λήξης του νέου πακέτου" + "\n";

            if (sError == "") {
                iNewPackage_ID = Convert.ToInt32(cmbNewPackage.SelectedValue);
                iContract_ID = 0;

                cmbFinanceServices.SelectedValue = 0;
                cmbProfile.SelectedValue = 0;
                foundRows = dtPackages.Select("ID = " + cmbNewPackage.SelectedValue);
                if (foundRows.Length > 0)
                {
                    cmbFinanceServices.SelectedValue = foundRows[0]["PackageType_ID"];

                    switch (cmbFinanceServices.SelectedValue)
                    {
                        case 2:
                            cmbProfile.SelectedValue = foundRows[0]["AdvisoryInvestmentProfile_ID"];
                            break;
                        case 3:
                            cmbProfile.SelectedValue = foundRows[0]["DiscretInvestmentProfile_ID"];
                            break;
                    }
                }
                dPackageDateStart.Value = dNewPackageDateStart.Value;
                dPackageDateFinish.Value = dNewPackageDateFinish.Value;
                cmbCompanyPackages.SelectedValue = iNewPackage_ID;
                panChangePackage.Visible = false;
            }
            else MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnChangeCancel_Click(object sender, EventArgs e)
        {
            panChangePackage.Visible = false;
        }
        private void ShowContractData()
        {
            if (iContract_ID != 0 && lblEditMode.Text != "4") {

                clsContracts klsContract = new clsContracts();
                klsContract.Record_ID = iContract_ID;
                klsContract.Contract_Details_ID = iContract_Details_ID;
                klsContract.Contract_Packages_ID = iContract_Packages_ID;
                klsContract.GetRecord();

                chkMIIFID_2.Checked = (klsContract.MiFID_2 == 1? true : false);
                cmbFinanceServices.SelectedValue = klsContract.Packages.Service_ID;
                dPackageDateStart.Value = klsContract.Packages.DateStart;
                dPackageDateFinish.Value = klsContract.Packages.DateFinish;
                bCheckPackages = false;
                cmbCompanyPackages.SelectedValue = klsContract.Packages.CFP_ID;
                cmbProfile.SelectedValue = klsContract.Packages.Profile_ID;
                cmbInvestmentPolicy.SelectedValue = klsContract.Details.InvestmentPolicy_ID;

                txtPackageNotes.Text = klsContract.Packages.ContractNotes;
                iBrokerageOption_ID = klsContract.BrokerageOption_ID;
                lblBrokerageServiceProvider.Text = klsContract.BrokerageServiceProvider_Title;
                lblBrokerageOption.Text = klsContract.BrokerageOption_Title;

                iRTOOption_ID = klsContract.RTOOption_ID;
                lblRTOServiceProvider.Text = klsContract.RTOServiceProvider_Title;
                lblRTOOption.Text = klsContract.RTOOption_Title;

                lblAdvisoryServiceProvider.Text = klsContract.AdvisoryServiceProvider_Title + "";
                iAdvisoryProvider_ID = klsContract.AdvisoryServiceProvider_ID;
                lblAdvisoryOption.Text = klsContract.AdvisoryOption_Title + "";
                iAdvisoryOption_ID = klsContract.AdvisoryOption_ID;
                lblAdvisoryInvestmentProfile.Text = klsContract.AdvisoryInvestmentProfile_Title + "";
                iAdvisoryInvestmentProfile_ID = klsContract.AdvisoryInvestmentProfile_ID;
                lblAdvisoryInvestmentPolice.Text = klsContract.AdvisoryInvestmentPolicy_Title + "";
                iAdvisoryInvestmentPolicy_ID = klsContract.AdvisoryInvestmentPolicy_ID;

                lblDiscretServiceProvider.Text = klsContract.DiscretServiceProvider_Title + "";
                iDiscretProvider_ID = klsContract.DiscretServiceProvider_ID;
                lblDiscretOption.Text = klsContract.DiscretOption_Title + "";
                iDiscretOption_ID = klsContract.DiscretOption_ID;
                lblDiscretInvestmentProfile.Text = klsContract.DiscretInvestmentProfile_Title + "";
                iDiscretInvestmentProfile_ID = klsContract.DiscretInvestmentProfile_ID;
                lblDiscretInvestmentPolice.Text = klsContract.DiscretInvestmentPolicy_Title + "";
                iDiscretInvestmentPolicy_ID = klsContract.DiscretInvestmentPolicy_ID;

                lblCustodyServiceProvider.Text = klsContract.CustodyServiceProvider_Title + "";
                iCustodyProvider_ID = klsContract.CustodyServiceProvider_ID;
                lblCustodyOption.Text = klsContract.CustodyOption_Title + "";
                iCustodyOption_ID = klsContract.CustodyOption_ID;
                dCustodyFrom_Month3.Value = dPackageDateStart.Value;
                dCustodyTo_Month3.Value = dPackageDateFinish.Value;
                lblCustodyMonthMinAmount.Text = (klsContract.Custody_MonthMinAmount > 0) ? klsContract.Custody_MonthMinAmount.ToString() : "0";
                lblCustodyMonthMinCurrency.Text = klsContract.Custody_MonthMinCurr + "";

                lblAdminServiceProvider.Text = klsContract.AdminServiceProvider_Title + "";
                iAdminProvider_ID = klsContract.AdminServiceProvider_ID;
                lblAdminOption.Text = klsContract.AdminOption_Title + "";
                iAdminOption_ID = klsContract.AdminOption_ID;
                lblAdmin_MonthMinAmount.Text = (klsContract.Admin_MonthMinAmount > 0) ? klsContract.Admin_MonthMinAmount.ToString() : "0";
                lblAdmin_MonthMinCurr.Text = klsContract.Admin_MonthMinCurr + "";

                lblDealAdvisoryServiceProvider.Text = klsContract.DealAdvisoryServiceProvider_Title + "";
                iDealAdvisoryProvider_ID = klsContract.DealAdvisoryServiceProvider_ID;
                lblDealAdvisoryOption.Text = klsContract.DealAdvisoryOption_Title + "";
                iDealAdvisoryOption_ID = klsContract.DealAdvisoryOption_ID;
                lblDealAdvisoryFinanceTools.Text = klsContract.DealAdvisoryInvestmentPolicy_Title + "";
                iDealAdvisoryInvestmentPolicy_ID = klsContract.DealAdvisoryInvestmentPolicy_ID;

                lblLombardServiceProvider.Text = klsContract.LombardServiceProvider_Title + "";
                iLombardProvider_ID = klsContract.LombardServiceProvider_ID;
                lblLombardOption.Text = klsContract.LombardOption_Title + "";
                iLombardOption_ID = klsContract.LombardOption_ID;
                lblLombardAMR.Text = klsContract.Lombard_AMR + "";

                lblFXServiceProvider.Text = klsContract.FXServiceProvider_Title + "";
                iFXProvider_ID = klsContract.FXServiceProvider_ID;
                lblFXOption.Text = klsContract.FXOption_Title + "";
                iFXOption_ID = klsContract.FXOption_ID;

                iSettlementsOption_ID = klsContract.SettlementsOption_ID;
                lblSettlementsOption.Text = klsContract.SettlementsOption_Title + "";
                lblSettlementsServiceProvider.Text = klsContract.SettlementsServiceProvider_Title + "";
            }
            else  {
                bCheckPackages = false;
                foundRows = dtPackages.Select("ID = " + cmbCompanyPackages.SelectedValue);
                if (foundRows.Length > 0)  {
                    chkMIIFID_2.Checked = (Convert.ToInt32(foundRows[0]["MIFID"]) == 2 ? true : false);
                    cmbFinanceServices.SelectedValue = foundRows[0]["PackageType_ID"];
                    
                    switch (foundRows[0]["PackageType_ID"])
                    {
                        case 2:                                                                                       // Advisory
                            cmbProfile.SelectedValue = foundRows[0]["AdvisoryInvestmentProfile_ID"];
                            cmbInvestmentPolicy.SelectedValue = foundRows[0]["AdvisoryInvestmentPolicy_ID"];
                            break;
                        case 3:                                                                                      // Discret
                            cmbProfile.SelectedValue = foundRows[0]["DiscretInvestmentProfile_ID"];
                            cmbInvestmentPolicy.SelectedValue = foundRows[0]["DiscretInvestmentPolicy_ID"];
                            break;
                        default:
                            cmbProfile.SelectedValue = 0;
                            break;
                    }

                    txtPackageNotes.Text = foundRows[0]["Notes"] + "";
                    iBrokerageOption_ID = Convert.ToInt32(foundRows[0]["BrokerageOption_ID"]);
                    iAdvisoryOption_ID = Convert.ToInt32(foundRows[0]["AdvisoryOption_ID"]);
                    iAdvisoryProvider_ID = Convert.ToInt32(foundRows[0]["AdvisoryProvider_ID"]);
                    iAdvisoryInvestmentProfile_ID = Convert.ToInt32(foundRows[0]["AdvisoryInvestmentProfile_ID"]);
                    iAdvisoryInvestmentPolicy_ID = Convert.ToInt32(foundRows[0]["AdvisoryInvestmentPolicy_ID"]);

                    iCustodyOption_ID = Convert.ToInt32(foundRows[0]["CustodyOption_ID"]);
                    iCustodyProvider_ID = Convert.ToInt32(foundRows[0]["CustodyProvider_ID"]);
                    iAdminOption_ID = Convert.ToInt32(foundRows[0]["AdminOption_ID"]);
                    iAdminProvider_ID = Convert.ToInt32(foundRows[0]["AdminProvider_ID"]);
                    iDealAdvisoryOption_ID = Convert.ToInt32(foundRows[0]["DealAdvisoryOption_ID"]);
                    iDealAdvisoryProvider_ID = Convert.ToInt32(foundRows[0]["DealAdvisoryProvider_ID"]);
                    iDealAdvisoryInvestmentPolicy_ID = Convert.ToInt32(foundRows[0]["DealAdvisoryInvestmentPolicy_ID"]);
                    iDiscretOption_ID = Convert.ToInt32(foundRows[0]["DiscretOption_ID"]);
                    iDiscretProvider_ID = Convert.ToInt32(foundRows[0]["DiscretProvider_ID"]);
                    iDiscretInvestmentPolicy_ID = Convert.ToInt32(foundRows[0]["DiscretInvestmentPolicy_ID"]);
                    iDiscretInvestmentProfile_ID = Convert.ToInt32(foundRows[0]["DiscretInvestmentProfile_ID"]);
                    iLombardOption_ID = Convert.ToInt32(foundRows[0]["LombardOption_ID"]);
                    iLombardProvider_ID = Convert.ToInt32(foundRows[0]["LombardProvider_ID"]);
                    iFXOption_ID = Convert.ToInt32(foundRows[0]["FXOption_ID"]);
                    iFXProvider_ID = Convert.ToInt32(foundRows[0]["FXProvider_ID"]);
                    iSettlementsOption_ID = Convert.ToInt32(foundRows[0]["SettlementsOption_ID"]);

                    if (Global.IsNumeric(foundRows[0]["Custody_MonthMinAmount"])) {
                        lblCustodyMonthMinAmount.Text = foundRows[0]["Custody_MonthMinAmount"] + "";
                        lblCustodyMonthMinCurrency.Text = foundRows[0]["Custody_MonthMinCurr"] + "";
                        dCustodyFrom_Month3.Value = dPackageDateStart.Value;
                        dCustodyTo_Month3.Value = dPackageDateFinish.Value;
                        txtCustodyMonth3_Discount.Text = "0";
                        lblCustodyMonth3_Fees.Text = foundRows[0]["Custody_MonthMinAmount"] + "";
                    }

                    if (Global.IsNumeric(foundRows[0]["Admin_MonthMinAmount"])) {
                        lblAdmin_MonthMinAmount.Text = foundRows[0]["Admin_MonthMinAmount"] + "";
                        lblAdmin_MonthMinCurr.Text = foundRows[0]["Admin_MonthMinCurr"] + "";
                        txtAdminMinimumFees_Discount.Text = "0";
                        txtAdminMinimumFees.Text = foundRows[0]["Admin_MonthMinAmount"] + "";
                    }
                }
                bCheckPackages = true;
            }
        }
        private void ShowGridsData(bool bIncludeDiscount)
        {
            bCheckPackages = false;

            //------------------ initialize fgBrokerageFees grid ------------------
            fgBrokerageFees.Redraw = false;
            fgBrokerageFees.Rows.Count = 2;
            if (iBrokerageOption_ID != 0)  {

                clsClientsBrokerageFees klsClientsBrokerageFees = new clsClientsBrokerageFees();
                klsClientsBrokerageFees.Option_ID = iBrokerageOption_ID;
                klsClientsBrokerageFees.DateFrom = dPackageDateStart.Value;
                klsClientsBrokerageFees.DateTo = dPackageDateFinish.Value;
                klsClientsBrokerageFees.Contract_ID = iContract_ID;
                klsClientsBrokerageFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsBrokerageFees.IncludeDiscount = bIncludeDiscount;                                   // false;- Don't add     true - Add discounts into table client's fees
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
                                dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
            }
            fgBrokerageFees.Redraw = true;

            //------------------ initialize fgRTOFees grid ------------------
            fgRTOFees.Redraw = false;
            fgRTOFees.Rows.Count = 2;
            if (iRTOOption_ID != 0)
            {
                clsClientsRTOFees klsClientsRTOFees = new clsClientsRTOFees();
                klsClientsRTOFees.Option_ID = iRTOOption_ID;
                klsClientsRTOFees.DateFrom = dPackageDateStart.Value;
                klsClientsRTOFees.DateTo = dPackageDateFinish.Value;
                klsClientsRTOFees.Contract_ID = iContract_ID;
                klsClientsRTOFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsRTOFees.IncludeDiscount = bIncludeDiscount;                                   // false;- Don't add     true - Add discounts into table client's fees
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
                            dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"]);
                }
            }
            fgRTOFees.Redraw = true;

            //------------------ initialize fgAdvisoryFees grid ------------------
            fgAdvisoryFees.Redraw = false;
            fgAdvisoryFees.Rows.Count = 2;
            if (iAdvisoryProvider_ID != 0 && iAdvisoryOption_ID != 0)
            {
                if (!bIncludeDiscount)                       // it's NEW contract - so theere aren't clients fees data, contract_id, contract_packages_id etc. 
                {
                    sTemp = "";
                    clsClientsAdvisoryFees klsClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    klsClientsAdvisoryFees.ServiceProvider_ID = iAdvisoryProvider_ID;
                    klsClientsAdvisoryFees.Option_ID = iAdvisoryOption_ID;
                    klsClientsAdvisoryFees.InvestmentProfile_ID = iAdvisoryInvestmentProfile_ID;
                    klsClientsAdvisoryFees.InvestmentPolicy_ID = iAdvisoryInvestmentPolicy_ID;
                    klsClientsAdvisoryFees.DateFrom = dPackageDateStart.Value;
                    klsClientsAdvisoryFees.DateTo = dPackageDateFinish.Value;
                    klsClientsAdvisoryFees.GetList();
                    foreach (DataRow dtRow in klsClientsAdvisoryFees.List.Rows)
                    {
                        if (sTemp.Length == 0) sTemp = dtRow["AdvisoryFees"].ToString();
                        else sTemp = sTemp + "~" + dtRow["AdvisoryFees"];

                        fgAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdvisoryFees"] + "\t" +
                                   dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                   dtRow["AdvisoryFees_Discount"] + "\t" + dtRow["FinishAdvisoryFees"] + "\t" + "0" + "\t" + dtRow["ID"] + "\t" +
                                   dtRow["MonthMinAmount"] + "\t" + "0" + "\t" + dtRow["MonthMinAmount"] + "\t" +
                                   dtRow["MonthMinCurr"] + "\t" + dtRow["MonthMinAmount"]);

                        lblAdvisory_MonthMinAmount.Text = dtRow["MonthMinAmount"].ToString();
                        lblAdvisory_MonthMinCurr.Text = dtRow["MonthMinCurr"].ToString();
                        txtAdvisory_MinimumFees_Discount.Text = "0";
                        txtAdvisory_MinimumFees.Text = dtRow["MonthMinAmount"].ToString();
                        lblAdvisory_AllManFees.Text = sTemp;
                    }
                }
                else                        // it's EXISTING contract
                {
                    dTemp = Convert.ToDateTime("1900/01/01");
                    iOldContract_ID = -999;
                    iOldContract_Packages_ID = -999;
                    clsClientsAdvisoryFees klsClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    klsClientsAdvisoryFees.ServiceProvider_ID = iAdvisoryProvider_ID;
                    klsClientsAdvisoryFees.Option_ID = iAdvisoryOption_ID;
                    klsClientsAdvisoryFees.InvestmentProfile_ID = iAdvisoryInvestmentProfile_ID;
                    klsClientsAdvisoryFees.InvestmentPolicy_ID = iAdvisoryInvestmentPolicy_ID;
                    klsClientsAdvisoryFees.DateFrom = dPackageDateStart.Value;
                    klsClientsAdvisoryFees.DateTo = dPackageDateFinish.Value;
                    klsClientsAdvisoryFees.Contract_ID = iContract_ID;
                    klsClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                    klsClientsAdvisoryFees.GetList_Package_ID();
                    foreach (DataRow dtRow in klsClientsAdvisoryFees.List.Rows)
                    {
                        if ((dTemp == Convert.ToDateTime("1900/01/01")) || (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"])) && 
                            (iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && (iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"]))))
                        {
                            dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                            iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                            iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);

                            fgAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdvisoryFees"] + "\t" +
                                        dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AdvisoryFees_Discount"] + "\t" +
                                        dtRow["FinishAdvisoryFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPAF_ID"] + "\t" +
                                        dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                        dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"]);

                            lblAdvisory_MonthMinAmount.Text = dtRow["MonthMinAmount"] + "";
                            lblAdvisory_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                            txtAdvisory_MinimumFees_Discount.Text = dtRow["MinimumFees_Discount"] + "";
                            txtAdvisory_MinimumFees.Text = dtRow["MinimumFees"] + "";
                            lblAdvisory_AllManFees.Text = dtRow["AllManFees"] + "";
                        }
                    }
                }
            }
            fgAdvisoryFees.Redraw = true;

            //------------------ initialize fgDiscretFees grid ------------------
            fgDiscretFees.Redraw = false;
            fgDiscretFees.Rows.Count = 2;
            if (iDiscretProvider_ID != 0 && iDiscretOption_ID != 0)
            {
                if (bIncludeDiscount)                       // it's NEW contract - so theere aren't clients fees data, contract_id, contract_packages_id etc. 
                {
                    sTemp = "";
                    clsClientsDiscretFees klsClientsDiscretFees = new clsClientsDiscretFees();
                    klsClientsDiscretFees.ServiceProvider_ID = iDiscretProvider_ID;
                    klsClientsDiscretFees.Option_ID = iDiscretOption_ID;
                    klsClientsDiscretFees.InvestmentProfile_ID = iDiscretInvestmentProfile_ID;
                    klsClientsDiscretFees.InvestmentPolicy_ID = iDiscretInvestmentPolicy_ID;
                    klsClientsDiscretFees.DateFrom = dPackageDateStart.Value;
                    klsClientsDiscretFees.DateTo = dPackageDateFinish.Value;
                    klsClientsDiscretFees.GetList();
                    foreach (DataRow dtRow in klsClientsDiscretFees.List.Rows)
                    {
                        if (sTemp.Length == 0) sTemp = dtRow["DiscretFees"].ToString();
                        else sTemp = sTemp + "~" + dtRow["DiscretFees"];

                        fgDiscretFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DiscretFees"] + "\t" +
                                   dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                   dtRow["DiscretFees_Discount"] + "\t" + dtRow["FinishDiscretFees"] + "\t" + "0" + "\t" + dtRow["ID"] + "\t" +
                                   dtRow["MonthMinAmount"] + "\t" + "0" + "\t" + dtRow["MonthMinAmount"] + "\t" +
                                   dtRow["MonthMinCurr"] + "\t" + dtRow["MonthMinAmount"]);

                        lblDiscret_MonthMinAmount.Text = dtRow["MonthMinAmount"].ToString();
                        lblDiscret_MonthMinCurr.Text = dtRow["MonthMinCurr"].ToString();
                        txtDiscret_MinimumFees_Discount.Text = "0";
                        txtDiscret_MinimumFees.Text = dtRow["MonthMinAmount"].ToString();
                        lblDiscret_AllManFees.Text = sTemp;
                    }
                }
                else                        // it's EXISTING contract
                {
                    dTemp = Convert.ToDateTime("1900/01/01");
                    iOldContract_ID = -999;
                    iOldContract_Packages_ID = -999;
                    clsClientsDiscretFees klsClientsDiscretFees = new clsClientsDiscretFees();
                    klsClientsDiscretFees.ServiceProvider_ID = iDiscretProvider_ID;
                    klsClientsDiscretFees.Option_ID = iDiscretOption_ID;
                    klsClientsDiscretFees.InvestmentProfile_ID = iDiscretInvestmentProfile_ID;
                    klsClientsDiscretFees.InvestmentPolicy_ID = iDiscretInvestmentPolicy_ID;
                    klsClientsDiscretFees.DateFrom = dPackageDateStart.Value;
                    klsClientsDiscretFees.DateTo = dPackageDateFinish.Value;
                    klsClientsDiscretFees.Contract_ID = iContract_ID;
                    klsClientsDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
                    klsClientsDiscretFees.GetList_Package_ID();
                    foreach (DataRow dtRow in klsClientsDiscretFees.List.Rows)
                    {
                        if ((dTemp == Convert.ToDateTime("1900/01/01")) || (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"])) && 
                            (iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && (iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"]))))
                        {
                            dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                            iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                            iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);

                            fgDiscretFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DiscretFees"] + "\t" +
                                        dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["DiscretFees_Discount"] + "\t" +
                                        dtRow["FinishDiscretFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPDF_ID"] + "\t" +
                                        dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                        dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"]);

                            lblDiscret_MonthMinAmount.Text = dtRow["MonthMinAmount"] + "";
                            lblDiscret_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                            txtDiscret_MinimumFees_Discount.Text = dtRow["MinimumFees_Discount"] + "";
                            txtDiscret_MinimumFees.Text = dtRow["MinimumFees"] + "";
                            lblDiscret_AllManFees.Text = dtRow["AllManFees"] + "";
                        }
                    }
                }
            }
            fgDiscretFees.Redraw = true;

            //---- initialize fgCustodyFees grid ------------------
            fgCustodyFees.Redraw = false;
            fgCustodyFees.Rows.Count = 2;

             if (iCustodyProvider_ID != 0 && iCustodyOption_ID != 0)
             {
                clsClientsCustodyFees klsClientsCustodyFees = new clsClientsCustodyFees();
                klsClientsCustodyFees.ServiceProvider_ID = iCustodyProvider_ID;
                klsClientsCustodyFees.Option_ID = iCustodyOption_ID;
                klsClientsCustodyFees.DateFrom = dPackageDateStart.Value;
                klsClientsCustodyFees.DateTo = dPackageDateFinish.Value;
                klsClientsCustodyFees.Contract_ID = iContract_ID;
                klsClientsCustodyFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsCustodyFees.GetList();
                foreach (DataRow dtRow in klsClientsCustodyFees.List.Rows)
                {
                    fgCustodyFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["CustodyFees"] + "\t" +
                             dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["CustodyFees_Discount"] + "\t" +
                             dtRow["FinishCustodyFees"] + "\t" + dtRow["ID"]);
                }
             }
             fgCustodyFees.Redraw = true;

            //------------------ initialize Admin Fees Data ------------------
            fgAdminFees.Redraw = false;
            fgAdminFees.Rows.Count = 2;

            if (iAdminProvider_ID != 0 && iAdminOption_ID != 0)
            {
                if (!bIncludeDiscount)  {                                                           // it's NEW contract - so theere aren't clients fees data, contract_id, contract_packages_id etc. 
                    clsClientsAdminFees klsClientsAdminFees = new clsClientsAdminFees();
                    klsClientsAdminFees.ServiceProvider_ID = iAdminProvider_ID;
                    klsClientsAdminFees.Option_ID = iAdminOption_ID;
                    klsClientsAdminFees.DateFrom = dPackageDateStart.Value;
                    klsClientsAdminFees.DateTo = dPackageDateFinish.Value;
                    klsClientsAdminFees.Contract_ID = iContract_ID;
                    klsClientsAdminFees.Contract_Packages_ID = iContract_Packages_ID;
                    //klsClientsAdminFees.IncludeDiscount = bIncludeDiscount;                           // 0- Don't add     1 - Add into table client's fees
                    klsClientsAdminFees.GetList();
                    foreach (DataRow dtRow in klsClientsAdminFees.List.Rows)
                    {
                        fgAdminFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdminFees"] + "\t" +
                                     dPackageDateStart.Value + "\t" + dPackageDateFinish.Value + "\t" + "0" + "\t" + dtRow["AdminFees"] + "\t" + "0" + "\t" + "0" + "\t" +
                                      dtRow["MonthMinAmount"] + "\t" + "0" + "\t" + dtRow["MonthMinAmount"] + "\t" + dtRow["MonthMinCurr"] + "\t" + "" + "\t" + "0");

                        lblAdmin_MonthMinAmount.Text = dtRow["MonthMinAmount"] + "";
                        lblAdmin_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                        txtAdminMinimumFees_Discount.Text = "0";
                        txtAdminMinimumFees.Text = "0";
                    }
                }
                else  { 
                    dTemp = Convert.ToDateTime("1900/01/01");
                    iOldContract_ID = -999;
                    iOldContract_Packages_ID = -999;
                        clsClientsAdminFees klsClientsAdminFees = new clsClientsAdminFees();
                        klsClientsAdminFees.ServiceProvider_ID = iAdminProvider_ID;
                    klsClientsAdminFees.Option_ID = iAdminOption_ID;
                    klsClientsAdminFees.DateFrom = dPackageDateStart.Value;
                    klsClientsAdminFees.DateTo = dPackageDateFinish.Value;
                    klsClientsAdminFees.Contract_ID = iContract_ID;
                    klsClientsAdminFees.Contract_Packages_ID = iContract_Packages_ID;
                    klsClientsAdminFees.GetList_Package_ID();
                    foreach (DataRow dtRow in klsClientsAdminFees.List.Rows)
                    {
                        if ((dTemp == Convert.ToDateTime("1900/01/01")) || (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) && 
                                (iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && (iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))))
                        {
                            dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                            iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                            iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                            fgAdminFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdminFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["AdminFees_Discount"] + "\t" +
                                       dtRow["FinishAdminFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPAF_ID"] + "\t" +
                                       dtRow["MonthMinAmount"] + "\t" + dtRow["MinimumFees_Discount"] + "\t" + dtRow["MinimumFees"] + "\t" +
                                       dtRow["MonthMinCurr"] + "\t" + dtRow["AllManFees"] + "\t" + dtRow["SPAF_ID"]);

                            lblAdmin_MonthMinAmount.Text = dtRow["MonthMinAmount"] + "";
                            lblAdmin_MonthMinCurr.Text = dtRow["MonthMinCurr"] + "";
                            txtAdminMinimumFees_Discount.Text = dtRow["MinimumFees_Discount"] + "";
                            txtAdminMinimumFees.Text = dtRow["MinimumFees"] + "";
                        }
                    }
                }
            }
            fgAdminFees.Redraw = true;

            //------------------ initialize fgDealAdvisoryFees grid ------------------
            fgDealAdvisoryFees.Redraw = false;
            fgDealAdvisoryFees.Rows.Count = 2;
            if (!bIncludeDiscount)                                                        // it's NEW contract - so theere aren't clients fees data, contract_id, contract_packages_id etc. 
            {
                fgDealAdvisoryFees.Redraw = false;
                fgDealAdvisoryFees.Rows.Count = 2;
                clsClientsDealAdvisoryFees klsClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                klsClientsDealAdvisoryFees.ServiceProvider_ID = iDealAdvisoryProvider_ID;
                klsClientsDealAdvisoryFees.Option_ID = iDealAdvisoryOption_ID;
                klsClientsDealAdvisoryFees.InvestmentPolicy_ID = iDealAdvisoryInvestmentPolicy_ID;
                klsClientsDealAdvisoryFees.DateFrom = dPackageDateStart.Value;
                klsClientsDealAdvisoryFees.DateTo = dPackageDateFinish.Value;
                klsClientsDealAdvisoryFees.GetList();
                foreach (DataRow dtRow in klsClientsDealAdvisoryFees.List.Rows)
                {
                    fgDealAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DealAdvisoryFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                       dtRow["DealAdvisoryFees_Discount"] + "\t" + dtRow["FinishDealAdvisoryFees"] + "\t" + "0" + "\t" + dtRow["ID"]);
                }
            }
            else
            {
                dTemp = Convert.ToDateTime("1900/01/01");
                iOldContract_ID = -999;
                iOldContract_Packages_ID = -999;
                clsClientsDealAdvisoryFees klsClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                klsClientsDealAdvisoryFees.ServiceProvider_ID = iDealAdvisoryProvider_ID;
                klsClientsDealAdvisoryFees.Option_ID = iDealAdvisoryOption_ID;
                klsClientsDealAdvisoryFees.InvestmentPolicy_ID = iDealAdvisoryInvestmentPolicy_ID;
                klsClientsDealAdvisoryFees.DateFrom = dPackageDateStart.Value;
                klsClientsDealAdvisoryFees.DateTo = dPackageDateFinish.Value;
                klsClientsDealAdvisoryFees.Contract_ID = iContract_ID;
                klsClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                klsClientsDealAdvisoryFees.GetList_Package_ID();
                foreach (DataRow dtRow in klsClientsDealAdvisoryFees.List.Rows)
                {
                    if ((dTemp == Convert.ToDateTime("1900/01/01")) || (dTemp == Convert.ToDateTime(dtRow["DiscountDateFrom"]) &&
                            (iOldContract_ID == Convert.ToInt32(dtRow["Contract_ID"]) && (iOldContract_Packages_ID == Convert.ToInt32(dtRow["Contract_Packages_ID"])))))
                    {
                        dTemp = Convert.ToDateTime(dtRow["DiscountDateFrom"]);
                        iOldContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                        iOldContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                        fgDealAdvisoryFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DealAdvisoryFees"] + "\t" +
                                           dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["DealAdvisoryFees_Discount"] + "\t" +
                                           dtRow["FinishDealAdvisoryFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["SPDAF_ID"]);
                    }
                }
            }
            fgDealAdvisoryFees.Redraw = true;

            //------------------ initialize fgLombardFees grid ------------------
            fgLombardFees.Redraw = false;
            fgLombardFees.Rows.Count = 1;
            clsClientsLombardFees klsClientsLombardFees = new clsClientsLombardFees();
            klsClientsLombardFees.ServiceProvider_ID = iLombardProvider_ID;
            klsClientsLombardFees.Option_ID = iLombardOption_ID;
            //klsClientsLombardFees.DateFrom = dPackageDateStart.Value;
            //klsClientsLombardFees.DateTo = dPackageDateFinish.Value;
            //klsClientsLombardFees.Contract_ID = iContract_ID;
            //klsClientsLombardFees.Contract_Packages_ID = iContract_Packages_ID;
            //klsClientsLombardFees.ClientFees = iClientFees;                        // 0- Don't add     1 - Add into table client's fees
            klsClientsLombardFees.GetList();
            foreach (DataRow dtRow in klsClientsLombardFees.List.Rows)
                fgLombardFees.AddItem(dtRow["Currency"] + "\t" + dtRow["ID"]);
            fgLombardFees.Redraw = true;

            //------------------ initialize fgFXFees grid ------------------
            fgFXFees.Redraw = false;
            fgFXFees.Rows.Count = 2;

            if (iFXProvider_ID != 0 && iFXOption_ID != 0)
            {   clsClientsFXFees klsClientsFXFees = new clsClientsFXFees();
                klsClientsFXFees.ServiceProvider_ID = iFXProvider_ID;
                klsClientsFXFees.Option_ID = iFXOption_ID;
                klsClientsFXFees.DateFrom = dPackageDateStart.Value;
                klsClientsFXFees.DateTo = dPackageDateFinish.Value;
                klsClientsFXFees.Contract_ID = iContract_ID;
                klsClientsFXFees.Contract_Packages_ID = iContract_Packages_ID;
                //klsClientsFXFees.ClientFees = iClientFees;                          ' 0- Don't add     1 - Add into table client's fees
                klsClientsFXFees.GetList();
                foreach (DataRow dtRow in klsClientsFXFees.List.Rows)
                {
                    fgFXFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["FXFees"] + "\t" +
                                 dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" + dtRow["FXFees_Discount"] + "\t" +
                                 dtRow["FinishFXFees"] + "\t" + dtRow["ID"]);
                }
            }
            fgFXFees.Redraw = true;

            //------------------ initialize fgSettlementsFees grid ------------------
            clsClientsSettlementFees klsClientsSettlementFees = new clsClientsSettlementFees();
            klsClientsSettlementFees.Option_ID = iSettlementsOption_ID;
            klsClientsSettlementFees.DateFrom = dPackageDateStart.Value;
            klsClientsSettlementFees.DateTo = dPackageDateFinish.Value;
            klsClientsSettlementFees.Contract_ID = iContract_ID;
            klsClientsSettlementFees.Contract_Packages_ID = iContract_Packages_ID;
            //klsClientsSettlementFees.ClientFees = iClientFees;                                            // 0- Don't add     1 - Add into table client's fees
            klsClientsSettlementFees.GetList();
            foreach (DataRow dtRow in klsClientsSettlementFees.List.Rows)
            {
                fgSettlementsFees.AddItem(dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                          dtRow["Depositories_Title"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" +
                                          dtRow["BuyFeesPercent"] + "\t" + dtRow["SellFeesPercent"] + "\t" +
                                          dtRow["TicketFeesBuyAmount"] + "\t" + dtRow["TicketFeesSellAmount"] + "\t" + dtRow["TicketFeesCurr"] + "\t" +
                                          dtRow["MinimumFees"] + "\t" + dtRow["MinimumFeesCurr"] + "\t" +
                                          dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                          dtRow["FeesDiscountPercent"] + "\t" + dtRow["TicketFeesDiscountPercent"] + "\t" +
                                          dtRow["FinishBuyFeesPercent"] + "\t" + dtRow["FinishSellFeesPercent"] + "\t" +
                                          dtRow["TicketFinishBuyFeesAmount"] + "\t" + dtRow["TicketFinishSellFeesAmount"] + "\t" +
                                          dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["ID"] + "\t" + dtRow["Depositories_ID"]);
            }
            fgSettlementsFees.Redraw = true;

            bCheckPackages = true;
        }
        private void cmbCompanyPackages_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckPackages) {
                if (Convert.ToInt32(cmbCompanyPackages.SelectedValue) != 0) {
                    ShowContractData();
                    ShowGridsData(false);                                                   // false - Dont'a Add client's fees data
                    lblContract_ID.Text = cmbCompanyPackages.SelectedValue + "";
                    cmbFinanceServices.Enabled = false;
                }
                else  {
                    cmbFinanceServices.SelectedValue = 0;
                    cmbFinanceServices.Enabled = true;
                    fgBrokerageFees.Rows.Count = 2;
                    fgAdvisoryFees.Rows.Count = 2;
                    fgDiscretFees.Rows.Count = 2;
                    fgCustodyFees.Rows.Count = 2;
                    fgDealAdvisoryFees.Rows.Count = 2;
                    fgLombardFees.Rows.Count = 1;
                    fgFXFees.Rows.Count = 2;
                }
            }
        }
        //--- panNotes functions ---------------------------------------------------------------------
        private void tsbKey_Click(object sender, EventArgs e)
        {
            cmbInvestmentPolicy.Enabled = true;
            tsbKey.Visible = false;
            tsbSave.Visible = true;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            lblFinishAktion.Text = "1";
            cmbInvestmentPolicy.Enabled = false;
            tsbKey.Visible = true;
            tsbSave.Visible = false;
            panNotes.Visible = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName_Notes.Text = sFullFileName;
        }

        private void btnSave_Notes_Click(object sender, EventArgs e)
        {
            sFullFileName = sFullFileName + "";
            clsContracts_Details klsContract_Details = new clsContracts_Details();
            klsContract_Details.Record_ID = iContract_Details_ID;
            klsContract_Details.GetRecord();
            sTemp = klsContract_Details.InvestmentPolicy_Title;
            klsContract_Details.InvestmentPolicy_ID = Convert.ToInt32(cmbInvestmentPolicy.SelectedValue);
            klsContract_Details.EditRecord();

            clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
            ClientsDocFiles.PreContract_ID = 0;
            ClientsDocFiles.Contract_ID = iContract_ID;
            ClientsDocFiles.Client_ID = iClient_ID;
            ClientsDocFiles.ClientName = lblContractTitle.Text.Replace(".", "_");
            ClientsDocFiles.ContractCode = lblCode.Text;
            ClientsDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes_Notes.SelectedValue);
            ClientsDocFiles.DMS_Files_ID = 0;
            ClientsDocFiles.OldFileName = "";
            ClientsDocFiles.NewFileName = txtFileName_Notes.Text;
            ClientsDocFiles.FullFileName = sFullFileName  ;
            ClientsDocFiles.DateIns = DateTime.Now;
            ClientsDocFiles.User_ID = Global.User_ID;
            ClientsDocFiles.Status = 2;                                           // 2 - document confirmed
            iDocFiles_ID = ClientsDocFiles.InsertRecord();

            Global.SaveHistory(7, iContract_Packages_ID, iClient_ID, iContract_ID, jAktion, sTemp, iDocFiles_ID, txtNotes_Notes.Text, DateTime.Now, Global.User_ID);

            panNotes.Visible = false;
        }
        private void btnCancel_Notes_Click(object sender, EventArgs e)
        {
            panNotes.Visible = false;
        }
        //--------------------------------------------------------------------------
        private void ShowFileldsOnOff(bool bOnOff1, bool bOnOff2)
        {
            cmbCompanyPackages.Enabled = bOnOff1;
            cmbFinanceServices.Enabled = bOnOff1;
            cmbProfile.Enabled = bOnOff1;
            dPackageDateStart.Enabled = bOnOff1;
            dPackageDateFinish.Enabled = bOnOff1;
            panCustodyMonth3_Discount.Enabled = bOnOff2;
        }
        //--- Cash Accounts functions ------------------------------------------
        private void tsbAddCash_Click(object sender, EventArgs e)
        {
            fgCashAccounts.AddItem(lblPortfolio.Text + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "EUR" + "\t" + "1" + "\t" + "0" + "\t" + iContract_ID);
        }
        private void tsbAddMultiCash_Click(object sender, EventArgs e)
        {
            lblCode_Cash.Text = lblCode.Text;
            lblPortfolio_Cash.Text = lblPortfolio.Text;
            txtAccNum_Cash.Text = lblCode.Text + "-" + lblPortfolio.Text;
            txtIBAN_Cash.Text = "";

            fgCurrencies_Cash.Redraw = false;
            fgCurrencies_Cash.Rows.Count = 1;
            foreach (DataRow dtRow in Global.dtCurrencies.Rows)
                if ((dtRow["Title"] + "") != "") fgCurrencies_Cash.AddItem(true + "\t" + dtRow["Title"]);

            fgCurrencies_Cash.Redraw = true;

            panAddMulti.Visible = true;
        }
        private void tsbDelCash_Click(object sender, EventArgs e)
        {
            if (fgCashAccounts.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsContracts_CashAccounts klsClientCashAccount = new clsContracts_CashAccounts();
                    klsClientCashAccount.Record_ID = Convert.ToInt32(fgCashAccounts[fgCashAccounts.Row, "ID"]);
                    klsClientCashAccount.DeleteRecord();
                    fgCashAccounts.RemoveItem(fgCashAccounts.Row);
                }
            }
        }
        private void tsbSaveCash_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgCashAccounts.Rows.Count - 1; i++)
            {
                clsContracts_CashAccounts klsClientCashAccount = new clsContracts_CashAccounts();
                klsClientCashAccount.Client_ID = iClient_ID;
                klsClientCashAccount.Contract_ID = iContract_ID;
                klsClientCashAccount.Code = lblCode.Text;
                klsClientCashAccount.Portfolio = fgCashAccounts[i, "Portfolio"] + "";
                klsClientCashAccount.AccountNumber = fgCashAccounts[i, "AccountNumber"] + "";
                klsClientCashAccount.AccountNumber2 = fgCashAccounts[i, "AccountNumber2"] + "";
                klsClientCashAccount.Currency = fgCashAccounts[i, "Currency"] + "";
                klsClientCashAccount.IBAN = fgCashAccounts[i, "IBAN"] + "";
                klsClientCashAccount.Status = Convert.ToInt32(fgCashAccounts[i, "Status"]);

                if (Convert.ToInt32(fgCashAccounts[i, "ID"]) == 0) fgCashAccounts[i, "ID"] = klsClientCashAccount.InsertRecord();
                else  {
                    klsClientCashAccount.Record_ID = Convert.ToInt32(fgCashAccounts[i, "ID"]);
                    klsClientCashAccount.EditRecord();
                }
            }
        }
        private void btnOK_Cash_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgCurrencies_Cash.Rows.Count - 1; i++)
                if (Convert.ToBoolean(fgCurrencies_Cash[i, 0]))
                    fgCashAccounts.AddItem(lblPortfolio_Cash.Text + "\t" + txtAccNum_Cash.Text + "-" + fgCurrencies_Cash[i, "Currency"] + "\t" + 
                                           txtAccNum_Cash.Text + "-" + fgCurrencies_Cash[i, "Currency"] + "\t" + txtIBAN_Cash.Text + "\t" + 
                                           fgCurrencies_Cash[i, "Currency"] + "\t" + "1" + "\t" + "0" + "\t" + lblContract_ID.Text);

            panAddMulti.Visible = false;
        }
        private void btnCancel_Cash_Click(object sender, EventArgs e)
        {
            panAddMulti.Visible = false;
        }
        private void chkCancelAccs_CheckedChanged(object sender, EventArgs e)
        {
            ShowCashAccounts();
        }
        private void ShowCashAccounts()
        {
            fgCashAccounts.Redraw = false;
            fgCashAccounts.Rows.Count = 1;

            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
            ClientCashAccounts.Client_ID = 0;
            ClientCashAccounts.Contract_ID = iContract_ID;
            ClientCashAccounts.GetList();
            foreach (DataRow dtRow in ClientCashAccounts.List.Rows)
                if (chkCancelAccs.Checked || Convert.ToInt32(dtRow["Status"]) == 1)
                   fgCashAccounts.AddItem(dtRow["Portfolio"] + "\t" + dtRow["AccountNumber"] + "\t" + dtRow["AccountNumber2"] + "\t" + dtRow["IBAN"] + "\t" +
                                          dtRow["Currency"] + "\t" + dtRow["Status"] + "\t" + dtRow["ID"] + "\t" + dtRow["Contract_ID"]);

            fgCashAccounts.Redraw = true;
        }

        //--- Bank Accounts functionality ------------------------------------------
        private void tsbAdd_BankAccounts_Click(object sender, EventArgs e)
        {
            jAktion = 0;                                                                            // 0 - Add, 1 - Edit 
            iRec_ID = 0;
            cmbBankAccounts.SelectedValue = 0;
            lblBank.Text = "";
            lblBalance.Text =  "";
            lblCurr.Text =  "";
            lblShare.Text =  "";
            txtNotes.Text = "";
            cmbDocTypes.SelectedValue = 0;
            txtFileName_BankAccount.Text = "";
            sFileName = "";
            panEdit_BankAccount.Visible = true;
            cmbBankAccounts.Focus();
        }
        private void fgBankAccounts_DoubleClick(object sender, EventArgs e)
        {
            EditBankAccountRecord();
        }
        private void tsbEdit_BankAccounts_Click(object sender, EventArgs e)
        {
            EditBankAccountRecord();
        }
        private void EditBankAccountRecord()
        {
            jAktion = 1;                                                                              // 0 - Add, 1 - Edit
            iRec_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "ID"]);
            cmbBankAccounts.SelectedValue = fgBankAccounts[fgBankAccounts.Row, "Account_ID"];
            lblBank.Text = fgBankAccounts[fgBankAccounts.Row, "BankTitle"] + "";
            lblBalance.Text = fgBankAccounts[fgBankAccounts.Row, "StartBalance"] + "";
            lblCurr.Text = fgBankAccounts[fgBankAccounts.Row, "Currency"] + "";
            lblShare.Text = fgBankAccounts[fgBankAccounts.Row, "Share"] + "";
            txtNotes.Text = "";
            cmbDocTypes.SelectedValue = 0;
            txtFileName_BankAccount.Text = "";
            sFileName = "";
            panEdit_BankAccount.Visible = true;
            cmbBankAccounts.Focus();
        }
        private void tsbDel_BankAccounts_Click(object sender, EventArgs e)
        {
            if (fgBankAccounts.Row > 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)  {
                    clsClients_Contracts_Account Clients_Contracts_Account = new clsClients_Contracts_Account();
                    Clients_Contracts_Account.Record_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "ID"]);
                    Clients_Contracts_Account.DeleteRecord();
                    fgBankAccounts.RemoveItem(fgBankAccounts.Row);
                }
            }
        }
        private void cmbBankAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Global.IsNumeric(cmbBankAccounts.SelectedValue)) { 
                foundRows = dtBankAccs.Select("ID = " + Convert.ToInt32(cmbBankAccounts.SelectedValue));
                if (foundRows.Length > 0) {
                    lblBank.Text = foundRows[0]["BankTitle"] + "";
                    lblBalance.Text = foundRows[0]["StartBalance"] + "";
                    lblCurr.Text = foundRows[0]["Currency"] + "";
                    lblShare.Text = foundRows[0]["AccOwners"] + "";
                }
            }
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName_BankAccount.Text = Path.GetFileName(sFullFileName);
        }
        private void btnCancel_BankAccount_Click(object sender, EventArgs e)
        {
            panEdit_BankAccount.Visible = false;
        }
        private void btnSave_BankAccount_Click(object sender, EventArgs e)
        {
            int iDocFiles_ID = 0;

            if (jAktion == 0 ) {
                clsClients_Contracts_Account Clients_Contracts_Account = new clsClients_Contracts_Account();
                Clients_Contracts_Account.Contract_ID = iContract_ID;
                Clients_Contracts_Account.Account_ID = Convert.ToInt32(cmbBankAccounts.SelectedValue);
                iRec_ID = Clients_Contracts_Account.InsertRecord();
            }
            //else  { // don't do anything //   }

            if (txtFileName_BankAccount.Text.Trim() != "")  {
                if (MessageBox.Show("Να προστεθεί αυτό το έγγραφο στο αρχείο εγγράφων του πελάτη;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                   clsClientsDocFiles ClientDocFiles = new clsClientsDocFiles();
                    ClientDocFiles.PreContract_ID = 0;
                    ClientDocFiles.Contract_ID = iContract_ID;
                    ClientDocFiles.Client_ID = iClient_ID;
                    ClientDocFiles.ClientName = lblContractTitle.Text;
                    ClientDocFiles.ContractCode = lblCode.Text;
                    ClientDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                    ClientDocFiles.DMS_Files_ID = 0;
                    ClientDocFiles.OldFileName = sFileName;
                    ClientDocFiles.NewFileName = txtFileName_BankAccount.Text;
                    ClientDocFiles.FullFileName = sFullFileName;
                    ClientDocFiles.DateIns = DateTime.Now;
                    ClientDocFiles.User_ID = Global.User_ID;
                    ClientDocFiles.Status = 2;                                           // 2 - document confirmed
                    iDocFiles_ID = ClientDocFiles.InsertRecord();
                }
            }

            sTemp = cmbBankAccounts.Text + "~" + lblBank.Text + "~" + lblBalance.Text + "~" + lblCurr.Text + "~" + lblShare.Text + "~" + "" + "~" +
                    txtFileName_BankAccount.Text + "~" + txtNotes.Text + "~" + cmbDocTypes.Text;

            Global.SaveHistory(4, iRec_ID, iClient_ID, iContract_ID, jAktion, sTemp, iDocFiles_ID, txtNotes.Text, DateTime.Now, Global.User_ID);       // 4 - Account Code

            panEdit_BankAccount.Visible = false;

            ShowBankAccountsList();
        }  
        private void ShowBankAccountsList()
        {
            fgBankAccounts.Redraw = false;
            fgBankAccounts.Rows.Count = 1;

            clsClients_Contracts_Account Clients_Contracts_Account = new clsClients_Contracts_Account();
            Clients_Contracts_Account.Contract_ID = iContract_ID;
            Clients_Contracts_Account.GetList();
            foreach(DataRow dtRow in Clients_Contracts_Account.List.Rows)
            fgBankAccounts.AddItem(dtRow["AccNumber"] + "\t" + dtRow["BankTitle"] + "\t" + dtRow["StartBalance"] + "\t" + dtRow["Currency"] + "\t" + 
                                   ((Convert.ToInt32(dtRow["AccType"]) == 0)? "ΟΧΙ": "ΝΑΙ") + "\t" + dtRow["AccOwners"] + "\t" + dtRow["ID"] + "\t" + dtRow["Account_ID"]);

            fgBankAccounts.Redraw = true;
        }
        // -----------------------------------------------------------------------
        public int Mode { get { return iMode; } set { iMode = value; } }
    }

}
