using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Custody
{
    public partial class frmTrx_Charges : Form
    {
        DataView dtView;
        int i = 0, iID = 0, iAction = 0, iRightsLevel;
        string sExtra;
        bool bCheckList;
        string[] WhoPays = { "-", "Πελάτη", "HellasFin"};
        string[] Operations = { "-", "Αφαιρείται", "Προστίθεται" };
        string[] CalculationBasis = { "-", "Αξία", "Αξία και δεδ.τόκοι" };
        string[] CategoriesExPost = { "-", "Επενδυτικές Υπηρεσίες" };
        string[] SubcategoriesExPost = { "-", "Κόστη Συναλλαγών", "Περιοδικά Έξοδα", "Φόροι", "Λοιπά Έξοδα" };

        CellRange rng;
        clsTrxCharges TrxCharges = new clsTrxCharges();        
        
        public frmTrx_Charges()
        {
            InitializeComponent();
        }

        private void frmTrx_Charges_Load(object sender, EventArgs e)
        {
            bCheckList = false;


            //-------------- Define cmbTrxTypes List ------------------
            cmbTrxTypes.DataSource = Global.dtTrxTypes.Copy();
            cmbTrxTypes.DisplayMember = "Title";
            cmbTrxTypes.ValueMember = "ID";
            cmbTrxTypes.SelectedValue = 0;

            //-------------- Define cmbReturnedTo List ------------------
            cmbReturnedTo.DataSource = Global.dtTrxActions.Copy();
            cmbReturnedTo.DisplayMember = "Title";
            cmbReturnedTo.ValueMember = "ID";
            cmbReturnedTo.SelectedValue = 0;

            //-------------- Define cmbTrxFeesTypes List ------------------
            cmbFeesTypes.DataSource = Global.dtTrxFeesTypes.Copy();
            cmbFeesTypes.DisplayMember = "Title";
            cmbFeesTypes.ValueMember = "ID";
            cmbFeesTypes.SelectedValue = 0;

            //-------------- Define cmbExtraFees List ------------------
            cmbConnectedFee1.DataSource = Global.dtTrxClientsFees.Copy();
            cmbConnectedFee1.DisplayMember = "Title";
            cmbConnectedFee1.ValueMember = "ID";
            cmbConnectedFee1.SelectedValue = 0;

            //-------------- Define cmbExtraFees2 List ------------------
            cmbDerivesFrom2.DataSource = Global.dtTrxClientsFees.Copy();
            cmbDerivesFrom2.DisplayMember = "Title";
            cmbDerivesFrom2.ValueMember = "ID";
            cmbDerivesFrom2.SelectedValue = 0;
            

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgPackages ----------------------------
            fgPackages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPackages.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            //------- fgFees ----------------------------
            fgFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFees.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            fgFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgFees.ShowCellLabels = true;

            fgFees.Styles.Normal.WordWrap = true;
            fgFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgFees.Rows[0].AllowMerging = true;
            fgFees.Cols[0].AllowMerging = true;
            //rng = fgFees.GetCellRange(0, 0, 1, 0);
            //rng.Data = " ";

            fgFees.Cols[0].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 0, 1, 0);
            rng.Data = "AA";

            fgFees.Cols[1].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 1, 1, 1);
            rng.Data = "Ειδος Πελάτη";

            fgFees.Cols[2].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Χωρα Φορολογικής Κατοικίας";

            fgFees.Cols[3].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 3, 1, 3);
            rng.Data = "Τυπος Προϊοντος";

            fgFees.Cols[4].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 4, 1, 4);
            rng.Data = "Κατηγορία";

            fgFees.Cols[5].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 5, 1, 5);
            rng.Data = "Execution Provider";

            fgFees.Cols[6].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Χρηματιστήριο";

            fgFees.Cols[7].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 7, 1, 7);
            rng.Data = "Χωρα Εκδότη";

            fgFees.Cols[8].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 8, 1, 8);
            rng.Data = "Custody Provider";

            fgFees.Cols[9].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 9, 1, 9);
            rng.Data = "Αποθετήριο";

            fgFees.Cols[10].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 10, 1, 10);
            rng.Data = "Αιτιολογία";

            fgFees.Cols[11].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 11, 1, 11);
            rng.Data = "Αλγοριθμός Υπολογισμού";

            fgFees.Cols[12].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 12, 1, 12);
            rng.Data = "Αξία υπολογισμού";

            fgFees.Cols[13].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 13, 1, 13);
            rng.Data = "Περιοδος Υπολογισμού";

            fgFees.Cols[14].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 14, 1, 14);
            rng.Data = "Τύπος Κίνησης";

            fgFees.Cols[15].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 15, 1, 15);
            rng.Data = "Νόμισμα ορίων";

            rng = fgFees.GetCellRange(0, 16, 0, 17);
            rng.Data = "Κλιμακα";

            fgFees[1, 16] = "Από";
            fgFees[1, 17] = "Εως";

            rng = fgFees.GetCellRange(0, 18, 0, 20);
            rng.Data = "Αρχικό";

            fgFees[1, 18] = "Rate %";
            fgFees[1, 19] = "Minimum";
            fgFees[1, 20] = "Maximum";

            rng = fgFees.GetCellRange(0, 21, 0, 23);
            rng.Data = "Εκπτωση";

            fgFees[1, 21] = "Rate %";
            fgFees[1, 22] = "Minimum";
            fgFees[1, 23] = "Maximum";

            rng = fgFees.GetCellRange(0, 24, 0, 26);
            rng.Data = "Τελικό";

            fgFees[1, 24] = "Rate %";
            fgFees[1, 25] = "Minimum";
            fgFees[1, 26] = "Maximum";

            fgFees.Cols[27].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 27, 1, 27);
            rng.Data = "Επι Προσόδων";

            fgFees.Cols[28].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 28, 1, 28);
            rng.Data = "Ανά τιτλο";

            fgFees.Cols[29].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 29, 1, 29);
            rng.Data = "Ανά τεμάχιο";

            fgFees.Cols[30].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 30, 1, 30);
            rng.Data = "Χρηση ονομαστικής αξίας";

            //------- fgFee2 ----------------------------
            fgFees2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFees2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            DefineList();
            if (fgList.Rows.Count > 0)
            {
                fgList.Row = 1; 
                ShowRecord();
            }
            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Width = this.Width - 32;
            fgList.Height = this.Height - 580;

            fgPackages.Left = 6;
            fgPackages.Top = fgList.Top + fgList.Height + 10;
            fgPackages.Width = this.Width - 32;
            fgPackages.Height = 160;
            
            fgFees.Left = 6;
            fgFees.Top = fgList.Top + fgList.Height + fgPackages.Height + 20;
            fgFees.Width = this.Width - 32; 
            fgFees.Height = Screen.PrimaryScreen.Bounds.Height - fgFees.Top - 72;

            fgFees2.Left = 6;
            fgFees2.Top = fgList.Top + fgList.Height + fgPackages.Height + 20;
            fgFees2.Height = Screen.PrimaryScreen.Bounds.Height - fgFees.Top - 72;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }
        private void DefineList()
        {
            try
            {
                bCheckList = false;
                i = 0;
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                TrxCharges = new clsTrxCharges();
                TrxCharges.GetList();
                foreach (DataRow dtRow in TrxCharges.List.Rows)
                {
                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtRow["FeeDescription_Gr"] + "\t" + dtRow["FeeDescription_En"] + "\t" + dtRow["TrxType_Title"] + "\t" +
                                   dtRow["FeesTypes_Title"] + "\t" + dtRow["FeesSubTypes_Title"] + "\t" + WhoPays[Convert.ToInt32(dtRow["WhoPays_ID"])] + "\t" + 
                                   dtRow["TrxActions_Title"] + "\t" + dtRow["HowCalculate"] + "\t" + dtRow["ConnectedFee1_Title"] + "\t" + 
                                   Operations[Convert.ToInt32(dtRow["Operation_ID"])] + "\t" + dtRow["DerivesFrom2_Title"] + "\t" + dtRow["ExecProviderUse"] + "\t" + 
                                   dtRow["CustodianUse"] + "\t" + dtRow["DepositoryUse"] + "\t" + dtRow["AttributedSettlement"] + "\t" + dtRow["AllowNegative"] + "\t" + 
                                   dtRow["Comments"] + "\t" + dtRow["ShowonReceipt"] + "\t" + dtRow["ShowonStatement"] + "\t" + dtRow["DaysMonth"] + "\t" + dtRow["DaysYear"] + "\t" + 
                                   dtRow["YearCalendarDays"] + "\t" + CalculationBasis[Convert.ToInt32(dtRow["CalculationBasis_ID"])] + "\t" + 
                                   dtRow["UseValuewithoutinterest"] + "\t" + dtRow["UseNegativeAssets"] + "\t" + CategoriesExPost[Convert.ToInt32(dtRow["ExPostCategory_ID"])] + "\t" + 
                                   SubcategoriesExPost[Convert.ToInt32(dtRow["ExPostSubcategory_ID"])] + "\t" + dtRow["VATrate"] + "\t" + dtRow["ID"] + "\t" + 
                                   dtRow["TrxType_ID"] + "\t" + dtRow["WhoPays_ID"] + "\t" + dtRow["ReturnedTo_ID"] + "\t" + dtRow["FeesTypes_ID"] + "\t" +
                                   dtRow["FeesSubTypes_ID"] + "\t" + dtRow["ConnectedFee1_ID"] + "\t" + dtRow["Operation_ID"] + "\t" + dtRow["DerivesFrom2_ID"] + "\t" +
                                   dtRow["CalculationBasis_ID"] + "\t" + dtRow["ExPostCategory_ID"] + "\t" + dtRow["ExPostSubcategory_ID"] + "\t" + dtRow["GridColsView"]);
                }

                fgList.Redraw = true;
                bCheckList = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                iID = 0;
                iAction = 1;
                panEdit.Enabled = false;
                tsbSave.Enabled = false;

                if (fgList.Row > 0)
                    ShowRecord();
            }
        }
        private void ShowRecord()
        {
            string sGridColsView = "";

            iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            sGridColsView = fgList[fgList.Row, "GridColsView"] + "";

            if (sGridColsView.Substring(0, 1) != "#") {
                fgFees.Visible = true;
                fgFees2.Visible = false;
                for (i = 0; i < sGridColsView.Length; i++)
                    if (sGridColsView.Substring(i, 1) == "1") fgFees.Cols[i].Visible = true;
                    else fgFees.Cols[i].Visible = false;

                TrxCharges = new clsTrxCharges();
                TrxCharges.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                TrxCharges.GetList();
                //txtTitle.Text = TrxCharges.Title_ISP;
            }
            else
            {
                fgFees.Visible = false;
                fgFees2.Visible = true;
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;

            txtFeeDescription_Gr.Text = "";
            txtFeeDescription_En.Text =  "";
            cmbTrxTypes.SelectedValue = 0;
            cmbWhoPays.SelectedIndex = 0;
            cmbReturnedTo.SelectedValue = 0;
            cmbFeesTypes.SelectedValue = 0;
            cmbFeesSubTypes.SelectedValue = 0;
            cmbConnectedFee1.SelectedValue = 0;
            cmbOperations.SelectedIndex = 0;
            cmbDerivesFrom2.SelectedValue = 0;
            txtDaysMonth.Text = "0";
            txtDaysYear.Text = "0";
            chkYearCalendarDays.Checked = false;
            cmbCalculationBasis.SelectedIndex = 0;
            cmbExPostCategory.SelectedIndex = 0;
            cmbExPostSubcategory.SelectedIndex = 0;
            txtVATrate.Text = "";
            chkExecProviderUse.Checked = false;
            chkCustodianUse.Checked = false;
            chkDepositoryUse.Checked = false;
            chkAttributedSettlement.Checked = false;
            chkAllowNegative.Checked = false;
            chkShowonReceipt.Checked = false;
            chkShowonStatement.Checked = false;
            chkUseValuewithoutinterest.Checked = false;
            chkUseNegativeAssets.Checked = false;
            txtHowCalculate.Text = "";
            txtComments.Text = "";

            tsbSave.Enabled = false;
            panEdit.Visible = true;
            panEdit.Enabled = true;
            txtFeeDescription_Gr.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditMain();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditMain();
        }
        private void EditMain()
        {
            if (fgList.Row > 0)
            {                
                iAction = 1;
                i = fgList.Row;
                txtFeeDescription_Gr.Text = fgList[i, "FeeDescription_Gr"] + "";
                txtFeeDescription_En.Text = fgList[i, "FeeDescription_En"] + "";
                cmbTrxTypes.SelectedValue = Convert.ToInt32(fgList[i, "TrxType_ID"]);
                cmbWhoPays.SelectedIndex = Convert.ToInt32(fgList[i, "WhoPays_ID"]);
                cmbReturnedTo.SelectedValue = Convert.ToInt32(fgList[i, "ReturnedTo_ID"]);
                cmbFeesTypes.SelectedValue = Convert.ToInt32(fgList[i, "FeesTypes_ID"]);
                cmbFeesSubTypes.SelectedValue = Convert.ToInt32(fgList[i, "FeesSubTypes_ID"]);
                cmbConnectedFee1.SelectedValue = Convert.ToInt32(fgList[i, "ConnectedFee1_ID"]);
                cmbOperations.SelectedIndex = Convert.ToInt32(fgList[i, "Operations_ID"]);
                cmbDerivesFrom2.SelectedValue = Convert.ToInt32(fgList[i, "DerivesFrom2_ID"]);
                txtDaysMonth.Text = fgList[i, "DaysMonth"] + "";
                txtDaysYear.Text = fgList[i, "DaysYear"] + "";
                chkYearCalendarDays.Checked = Convert.ToBoolean(fgList[i, "YearCalendarDays"]) ? true : false;
                cmbCalculationBasis.SelectedIndex = Convert.ToInt32(fgList[i, "CalculationBasis_ID"]);
                cmbExPostCategory.SelectedIndex = Convert.ToInt32(fgList[i, "ExPostCategory_ID"]);
                cmbExPostSubcategory.SelectedIndex = Convert.ToInt32(fgList[i, "ExPostSubcategory_ID"]);
                txtVATrate.Text = fgList[i, "VATrate"] + "";
                chkExecProviderUse.Checked = Convert.ToBoolean(fgList[i, "ExecProviderUse"]) ? true : false;
                chkCustodianUse.Checked = Convert.ToBoolean(fgList[i, "CustodianUse"])? true : false;
                chkDepositoryUse.Checked = Convert.ToBoolean(fgList[i, "DepositoryUse"]) ? true : false;
                chkAttributedSettlement.Checked = Convert.ToBoolean(fgList[i, "AttributedSettlement"]) ? true : false;
                chkAllowNegative.Checked = Convert.ToBoolean(fgList[i, "AllowNegative"]) ? true : false;
                chkShowonReceipt.Checked = Convert.ToBoolean(fgList[i, "ShowonReceipt"]) ? true : false;
                chkShowonStatement.Checked = Convert.ToBoolean(fgList[i, "ShowonStatement"]) ? true : false;
                chkUseValuewithoutinterest.Checked = Convert.ToBoolean(fgList[i, "UseValuewithoutinterest"]) ? true : false;
                chkUseNegativeAssets.Checked = Convert.ToBoolean(fgList[i, "UseNegativeAssets"]) ? true : false;
                txtHowCalculate.Text = fgList[i, "HowCalculate"] + "";
                txtComments.Text = fgList[i, "Comments"] + "";

                tsbSave.Enabled = true;
                panEdit.Visible = true;
                panEdit.Enabled = true;
                txtFeeDescription_Gr.Focus();
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show(Global.GetLabel("attention_you_ask_for_deletion") + "." + "\n" + Global.GetLabel("are_you_sure_for_deletion"), Global.AppTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    if (Convert.ToInt32(fgList[fgList.Row, "ID"]) != 0)
                    {
                        clsSystem System = new clsSystem();
                        System.ExecSQL("DELETE Trx_Charges WHERE ID = " + fgList[fgList.Row, "ID"]);
                    }

                    txtFeeDescription_Gr.Text = "";

                    fgList.RemoveItem(fgList.Row);
                    fgList.Row = 0;
                    if (fgList.Rows.Count <= i) i = fgList.Rows.Count - 1;
                    fgList.Row = i;
                    fgList.Focus();
                }
            }
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Λίστα";
            var loopTo = fgList.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                EXL.Cells[i + 2, 1].Value = fgList[i, 0];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }


        private void picClose_Import_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        private void cmbTrxTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            tsbSave.Enabled = CheckMandatoryData();
        }
        private void cmbFeesTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            //-------------- Define cmbTrxFeesSubTypes List ------------------
            if (bCheckList)
            {
                dtView = Global.dtTrxFeesSubTypes.Copy().DefaultView;
                dtView.RowFilter = "FT_ID = " + cmbFeesTypes.SelectedValue;
                cmbFeesSubTypes.DataSource = dtView;
                cmbFeesSubTypes.DisplayMember = "Title";
                cmbFeesSubTypes.ValueMember = "ID";

                tsbSave.Enabled = CheckMandatoryData();
            }
        }
        private void cmbFeesSubTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            tsbSave.Enabled = CheckMandatoryData();
        }

        private void txtFeeDescription_Gr_LostFocus(object sender, EventArgs e)
        {
            tsbSave.Enabled = CheckMandatoryData();
        }

        private void txtFeeDescription_En_LostFocus(object sender, EventArgs e)
        {
            tsbSave.Enabled = CheckMandatoryData();
        }
        private Boolean CheckMandatoryData()
        {
            Boolean bResult = false;

            if (txtFeeDescription_Gr.Text.Length > 0 && txtFeeDescription_En.Text.Length > 0 && Convert.ToInt32(cmbTrxTypes.SelectedValue) > 0 && 
                Convert.ToInt32(cmbFeesTypes.SelectedValue) > 0)  bResult = true;

            return bResult;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            TrxCharges = new clsTrxCharges();
            if (iAction == 1)
            {
                TrxCharges.Record_ID = iID;
                TrxCharges.GetRecord();
            }
            TrxCharges.FeeDescription_Gr = txtFeeDescription_Gr.Text + "";
            TrxCharges.FeeDescription_En = txtFeeDescription_En.Text + "";
            TrxCharges.TrxType_ID = Convert.ToInt32(cmbTrxTypes.SelectedValue);
            TrxCharges.WhoPays_ID = Convert.ToInt32(cmbWhoPays.SelectedIndex);
            TrxCharges.ReturnedTo_ID = Convert.ToInt32(cmbReturnedTo.SelectedValue);
            TrxCharges.HowCalculate = txtHowCalculate.Text + "";
            TrxCharges.FeesTypes_ID = Convert.ToInt32(cmbFeesTypes.SelectedValue);
            TrxCharges.FeesSubTypes_ID = Convert.ToInt16(cmbFeesSubTypes.SelectedValue);
            TrxCharges.ConnectedFee1_ID = Convert.ToInt32(cmbConnectedFee1.SelectedValue);
            TrxCharges.Operation_ID = Convert.ToInt32(cmbOperations.SelectedIndex);
            TrxCharges.DerivesFrom2_ID = Convert.ToInt32(cmbDerivesFrom2.SelectedValue);
            TrxCharges.ExecProviderUse = Convert.ToInt32(chkExecProviderUse.Checked);
            TrxCharges.CustodianUse = Convert.ToInt32(chkCustodianUse.Checked);
            TrxCharges.DepositoryUse = Convert.ToInt32(chkDepositoryUse.Checked);
            TrxCharges.AttributedSettlement = Convert.ToInt32(chkAttributedSettlement.Checked);
            TrxCharges.AllowNegative = Convert.ToInt32(chkAllowNegative.Checked);
            TrxCharges.Comments = txtComments.Text + "";
            TrxCharges.ShowonReceipt = Convert.ToInt32(chkShowonReceipt.Checked);
            TrxCharges.ShowonStatement = Convert.ToInt32(chkShowonStatement.Checked);
            TrxCharges.DaysMonth = Convert.ToInt32(txtDaysMonth.Text);
            TrxCharges.DaysYear = Convert.ToInt32(txtDaysYear.Text);
            TrxCharges.YearCalendarDays = Convert.ToInt32(chkYearCalendarDays.Checked);
            TrxCharges.CalculationBasis_ID = Convert.ToInt32(cmbCalculationBasis.SelectedIndex);
            TrxCharges.UseValuewithoutinterest = Convert.ToInt32(chkUseValuewithoutinterest.Checked);
            TrxCharges.UseNegativeAssets = Convert.ToInt32(chkUseNegativeAssets.Checked);
            TrxCharges.ExPostCategory_ID = Convert.ToInt32(cmbExPostCategory.SelectedIndex);
            TrxCharges.ExPostSubcategory_ID = Convert.ToInt32(cmbExPostSubcategory.SelectedIndex);
            TrxCharges.VATrate = txtVATrate.Text + "";

            if (iAction == 0) iID = TrxCharges.InsertRecord();
            else TrxCharges.EditRecord();

            DefineList();

            i = fgList.FindRow(iID.ToString(), 1, 29, false);
            fgList.Row = i;
            fgList.Focus();

            panEdit.Visible = false;
        }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
