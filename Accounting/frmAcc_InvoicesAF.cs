using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using C1.Win.C1FlexGrid;
using Excel = Microsoft.Office.Interop.Excel;
using Core;

namespace Accounting
{
    public partial class frmAcc_InvoicesAF : Form
    {
        int i, j, iID, iAT_ID, iAF_Quart, iClient_ID, iClientType, iContract_ID, iContract_Details_ID, iContract_Packages_ID,
            iRightsLevel, iNum, iInvoiceType, iInvoiceFisiko, iInvoiceNomiko,
            iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko, iCopies, iSourceRows, iFoundRows;
        string sCode, sPortfolio, sSeira, sInvoicePrinter, sCodeAkyrotiko = "", sInvTitleFisikoGr = "", sInvTitleFisikoEn = "", sInvoiceCodeFisiko = "",
               sInvTitleNomikoGr = "", sInvTitleNomikoEn = "", sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "",
               sSeiraPistotikoFisiko = "", sSeiraPistotikoNomiko = "", sSeiraAkyrotiko = "", sInvoiceAFTemplate = "", 
               sInvoiceCodePistotikoFisiko = "", sInvTitlePistotikoFisikoGr = "", sInvTitlePistotikoFisikoEn = "", sInvoiceTypePistotikoFisiko = "",
               sInvoiceCodePistotikoNomiko = "", sInvTitlePistotikoNomikoGr = "", sInvTitlePistotikoNomikoEn = "", sInvoiceTypePistotikoNomiko = "",
               sInvoiceCodeAkyrotiko = "", sInvTitleAkyrotikoGr = "", sInvTitleAkyrotikoEn = "", sInvoiceTypeAkyrotiko = "",
               sSeiraFisiko = "", sSeiraNomiko = "", sUnfoundRows, sExtra;
        decimal decSourceAmount, decFoundAmount;
        DateTime dStart, dFinish, dIssueDate;
        DataView dtView;
        DataRow[] foundRows;
        bool bCheckList;
        C1.Win.C1FlexGrid.CellRange rng;
        Hashtable imgMap = new Hashtable();
        clsAdminFees_Titles klsAdminFees_Titles = new clsAdminFees_Titles();
        clsAdminFees_Recs klsAdminFees_Recs = new clsAdminFees_Recs();
        Point position;
        bool pMove;

        public frmAcc_InvoicesAF()
        {
            InitializeComponent();

            panImport.Left = 4;
            panImport.Top = 92;

            panAUM.Left = 4;
            panAUM.Top = 92;
        }

        private void frmAcc_InvoicesAF_Load(object sender, EventArgs e)
        {

            bCheckList = false;

            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            for (i = 0; i < imgFiles.Images.Count; i++)
            {
                imgMap.Add(i, imgFiles.Images[i]);
            }

            for (i = 2010; i <= DateTime.Now.Year; i++)
            {
                cmbYear.Items.Add(i);
            }

            i = (DateTime.Now.Month + 2) / 3;
            if (i == 1)
            {
                i = 4;
                cmbYear.SelectedIndex = cmbYear.Items.Count - 2;
            }
            else
            {
                i = i - 1;
                cmbYear.SelectedIndex = cmbYear.Items.Count - 1;
            }

            switch (i)
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

            ucCS.StartInit(700, 300, 540, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);

            cmbServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedItem = 1;

            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";
            cmbAdvisors.SelectedItem = 1;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            fgList.ShowCellLabels = true;
            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";
            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = " ";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = Global.GetLabel("from");

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("until_");

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("contract");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("subaccount");

            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("currency");

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("package");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("aum");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("days");

            rng = fgList.GetCellRange(0, 12, 0, 16);
            rng.Data = "Έξοδα Λήψης & Διαβίβασης";
            fgList[1, 12] = "% σύμβασης";
            fgList[1, 13] = "ποσό πρίν την έκπτωση";
            fgList[1, 14] = "% έκπτωση";
            fgList[1, 15] = "% μετά την έκπτωση σύμβασης";
            fgList[1, 16] = "Αξία μετά την έκπτωση";

            rng = fgList.GetCellRange(0, 17, 0, 20);
            rng.Data = "Minimum Εξοδο λήψης & Διαβίβασης";
            fgList[1, 17] = "προ έκπτωσης";
            fgList[1, 18] = "% έκπτωσης";
            fgList[1, 19] = "% έκπτωσης";
            fgList[1, 20] = "τελικό";

            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);
            rng.Data = "Τελική Αξία";

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = "ΦΠΑ %";

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = "Ποσό ΦΠΑ";

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = "Πληρωτέο Ποσό";

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = "Τελική Αξία % ετήσια";

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = "Αρ.Παραστατικου";

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "MaxDays";

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = "AverageAUM";

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = "Weights";

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = "Min.Yearly";

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = "Ημερ.Χρέωσεις";

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = Global.GetLabel("service");

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "Επενδ.πολιτική";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = Global.GetLabel("profile");

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Advisor";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "RM";

            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = "intro";

            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = "diax";

            fgList.Cols[39].AllowMerging = true;
            rng = fgList.GetCellRange(0, 39, 1, 39);
            rng.Data = "1ος Δικαιούχος";

            fgList.Cols[40].AllowMerging = true;
            rng = fgList.GetCellRange(0, 40, 1, 40);
            rng.Data = Global.GetLabel("address");

            fgList.Cols[41].AllowMerging = true;
            rng = fgList.GetCellRange(0, 41, 1, 41);
            rng.Data = Global.GetLabel("city");

            fgList.Cols[42].AllowMerging = true;
            rng = fgList.GetCellRange(0, 42, 1, 42);
            rng.Data = Global.GetLabel("zip");

            fgList.Cols[43].AllowMerging = true;
            rng = fgList.GetCellRange(0, 43, 1, 43);
            rng.Data = Global.GetLabel("country");

            fgList.Cols[44].AllowMerging = true;
            rng = fgList.GetCellRange(0, 44, 1, 44);
            rng.Data = Global.GetLabel("afm");

            fgList.Cols[45].AllowMerging = true;
            rng = fgList.GetCellRange(0, 45, 1, 45);
            rng.Data = Global.GetLabel("doy");

            fgList.Cols[46].AllowMerging = true;
            rng = fgList.GetCellRange(0, 46, 1, 46);
            rng.Data = "ID εντολής";

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;

            //------- fgPortfolios ----------------------------
            fgPortfolios.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPortfolios.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgPortfolios.Styles.ParseString(Global.GridStyle);
            fgPortfolios.DrawMode = DrawModeEnum.OwnerDraw;
            fgPortfolios.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgPortfolios_BeforeEdit);
            fgPortfolios.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgPortfolios_AfterEdit);
            //fgPortfolios.DoubleClick += new System.EventHandler(fgPortfolios_DoubleClick);
            //fgPortfolios.MouseDown += new MouseEventHandler(fgPortfolios_MouseDown);

            DefineOptions();

            btnSearch.Enabled = false;
            cmbFilter.SelectedIndex = 0;
            bCheckList = true;
        }
        public void frmAcc_InvoicesMF()
        {
            InitializeComponent();
        } 
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 140;
            fgList.Width = this.Width - 30;
            panTools.Width = this.Width - 30;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }  

        private void txtDiscount_Amount2_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtDiscount_Percent2_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtVAT_Percent_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtAxiaAfter_LostFocus(object sender, EventArgs e)
        {

        }
        private void txtAUM_LostFocus(object sender, EventArgs e)
        {

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            fgList.Visible = false;
            fgList.Rows.Count = 2;

            if (rb1.Checked)
            {
                iAF_Quart = 1;
                dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
            }
            else
            {
                if (rb2.Checked)
                {
                    iAF_Quart = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }
                else
                {
                    if (rb3.Checked)
                    {
                        iAF_Quart = 3;
                        dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                        dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
                    }
                    else
                    {
                        if (rb4.Checked)
                        {
                            iAF_Quart = 4;
                            dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
                            dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                        }
                    }
                }
            }

            klsAdminFees_Titles = new clsAdminFees_Titles();
            klsAdminFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
            klsAdminFees_Titles.AF_Year = Convert.ToInt32(cmbYear.Text);
            klsAdminFees_Titles.AF_Quart = Convert.ToInt32(iAF_Quart);
            klsAdminFees_Titles.GetRecord_Title();
            iAT_ID = klsAdminFees_Titles.Record_ID;
            if (iAT_ID > 0)
            {
                this.Text = "Administration Fees";
                toolLeft.Left = 4;
                toolLeft.Visible = true;
                DefineList();
                ShowList();
            }
            else
               if (MessageBox.Show("ΠΡΟΣΟΧΗ! Νέο τρίμηνο.\n Είστε σίγουρος για αυτό;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                    toolLeft.Visible = true;
                    cmbFilter.Visible = true;

                    clsAdminFees_Titles AdminFees_Titles = new clsAdminFees_Titles();
                    AdminFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    AdminFees_Titles.AF_Quart = iAF_Quart;
                    AdminFees_Titles.AF_Year = Convert.ToInt32(cmbYear.Text);
                    AdminFees_Titles.DateIns = DateTime.Now;
                    AdminFees_Titles.Author_ID = Global.User_ID;
                    iAT_ID = AdminFees_Titles.InsertRecord();
               }

            panTools.Visible = true;
            chkPrint.Visible = true;
            fgList.Visible = true;
        }
        private void cmbServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
        }
        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count - 2; i++) fgList[i, 0] = chkPrint.Checked;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {

        }

        private void cmbAdvisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iClient_ID = 0;
            ucCS.Filters = "Status = 1";
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            ucCS.ListType = 1;

            fgPortfolios.Rows.Count = 1;
            fgPortfolios.Redraw = true;

            panEdit.Visible = true;
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditRow();
        }
        private void EditRow()
        {
            if (fgList.Row > 1)  {
                if (fgList.Col == 1) ShowInvoice();
                else {
                    ucCS.ShowClientsList = false;
                    ucCS.txtContractTitle.Text = fgList[fgList.Row, "ContractTitle"].ToString();
                    ucCS.ShowClientsList = true;
                    EditFees();
                }
            }
        }

        private void picClose_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        private void fgList_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                if (fgList.Col == 0)
                {
                    if (Convert.ToBoolean(fgList[fgList.Row, 0]))
                    {
                        if (fgList[fgList.Row, "Invoice_File"].ToString() != "")
                        {
                            fgList[fgList.Row, 0] = false;
                            MessageBox.Show("Invoice was issued", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
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
        private void DefineList()
        {
            //clsAdminFees_Recs klsAdminFees_Recs = new clsAdminFees_Recs();
            klsAdminFees_Recs.AT_ID = iAT_ID;
            klsAdminFees_Recs.GetList();
        }
        private void ShowList()
        {
            if (bCheckList)
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 2;
                int i = 0;

                foreach (DataRow dtRow in klsAdminFees_Recs.List.Rows)
                {
                    if (((Convert.ToInt32(cmbAdvisors.SelectedValue) == 0) || (Convert.ToInt32(dtRow["User1_ID"]) == Convert.ToInt32(cmbAdvisors.SelectedValue))) &&
                              ((cmbFilter.SelectedIndex < 1) || (cmbFilter.SelectedIndex == 1 && dtRow["Invoice_Num"].ToString() != "") || (cmbFilter.SelectedIndex == 2 && dtRow["Invoice_Num"].ToString() == "")) &&
                              (txtCode.Text.Trim() == "" || dtRow["Code"].ToString().Contains(txtCode.Text)))
                    {
                        i = i + 1;
                        fgList.AddItem(false + "\t" + Convert.ToInt16(dtRow["ImageType"]) + "\t" + i + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Currency"] + "\t" + dtRow["PackageTitle"] + "\t" + dtRow["AUM"] + "\t" +
                                       dtRow["Days"] + "\t" + dtRow["AmoiviPro"] + "\t" + dtRow["AxiaPro"] + "\t" + dtRow["Discount_Percent"] + "\t" + dtRow["AmoiviAfter"] + "\t" + dtRow["AxiaAfter"] + "\t" +
                                       dtRow["MinAmoivi"] + "\t" + dtRow["MinAmoivi_Percent"] + "\t" + dtRow["MinAmoivi_Percent2"] + "\t" + dtRow["FinishMinAmoivi"] + "\t" +
                                       dtRow["LastAmount"] + "\t" + dtRow["VAT_Percent"] + "\t" + dtRow["VAT_Amount"] + "\t" + dtRow["FinishAmount"] + "\t" + dtRow["LastAmount_Percent"] + "\t" +
                                       dtRow["Invoice_Num"] + "\t" + dtRow["MaxDays"] + "\t" + dtRow["AverageAUM"] + "\t" + dtRow["Weights"] + "\t" + dtRow["MinYearly"] + "\t" +
                                       dtRow["DateFees"] + "\t" + dtRow["Service_Title"] + "\t" + dtRow["InvestmentPolicy"] + "\t" + dtRow["InvestmentProfile"] + "\t" + 
                                       dtRow["Advisory_Name"] + "\t" + dtRow["RM_Name"] + "\t" + dtRow["Introducer_Name"] + "\t" + dtRow["Diaxiristis_Name"] + "\t" + dtRow["User1_Name"] + "\t" +
                                       dtRow["Address"] + "\t" + dtRow["City"] + "\t" + dtRow["Zip"] + "\t" + dtRow["Country"] + "\t" + dtRow["AFM"] + "\t" + dtRow["DOY"] + "\t" +
                                       dtRow["ID"] + "\t" + dtRow["ClientType"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Invoice_ID"] + "\t" + dtRow["Invoice_Type"] + "\t" + dtRow["L4"] + "\t" +
                                       dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t" + dtRow["Service_ID"] + "\t" + 
                                       dtRow["Status"] + "\t" + dtRow["Discount_Percent1"] + "\t" + dtRow["Discount_Amount1"] + "\t" + 
                                       dtRow["Discount_Percent2"] + "\t" + dtRow["Discount_Amount2"] + "\t" + dtRow["Discount_Amount"] + "\t" + dtRow["User1_ID"] + "\t" +
                                       dtRow["ConnectionMethod"] + "\t" + dtRow["Invoice_Num"] + "\t" + dtRow["Invoice_File"] + "\t" + dtRow["User_ID"] + "\t" + dtRow["ContractDateStart"] + "\t" + 
                                       dtRow["CountryEnglish"]);
                    }
                }
                fgList.Redraw = true;
                DefineSums();
            }
        }
        private void DefineSums()
        {
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 10, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 21, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 23, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 24, "");
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }
        private void PrintInvoice()
        {
            sInvoicePrinter = Global.InvoicePrinter;
            frmPrintInvoiceOptions PrintInvoiceOptions = new frmPrintInvoiceOptions();
            PrintInvoiceOptions.InvoicePrinter = Global.InvoicePrinter;
            PrintInvoiceOptions.NumCopies = iCopies;
            PrintInvoiceOptions.DateIssue = DateTime.Now;
            PrintInvoiceOptions.ShowDialog();
            if (PrintInvoiceOptions.LastAktion == 1)
            {
                sInvoicePrinter = PrintInvoiceOptions.InvoicePrinter;
                iCopies = PrintInvoiceOptions.NumCopies;
                dIssueDate = PrintInvoiceOptions.DateIssue;

                PrintingInvoices();
            }
        }
        private void PrintingInvoices()
        {
            int iLine;
            string sTemp, sPDF_FullPath, sInvoiceCode, sAitiologia, sApo, sEos, sInvTitleGr, sInvTitleEn, sCountry, sNewFile, sNum, sInvType, sFileName, sEafdss;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            bCheckList = false;
            iInvoiceType = 0;
            iClientType = 0;
            iNum = 0;
            iLine = 0;
            sNum = "";
            sSeira = "";
            sAitiologia = "";
            sApo = "";
            sEos = "";
            sPDF_FullPath = "";
            sInvTitleGr = "";
            sInvTitleEn = "";
            sInvoiceCode = "";
            sCountry = "";
            sInvType = "";
            sEafdss = "";
            sAitiologia = "";

            try
            {
                for (iLine = 2; iLine <= (fgList.Rows.Count - 1); iLine++)
                {
                    if (Convert.ToBoolean(fgList[iLine, 0]))
                    {
                        iInvoiceType = 0;
                        iClientType = Convert.ToInt16(fgList[iLine, "ClientType"]);
                        sInvoiceCode = "";
                        sInvType = "";

                        switch (Convert.ToInt16(fgList[iLine, "Invoice_Type"]))
                        {
                            case 1:
                                iInvoiceType = iInvoiceFisiko;
                                sInvoiceCode = sInvoiceCodeFisiko;
                                sInvTitleGr = sInvTitleFisikoGr;
                                sInvTitleEn = sInvTitleFisikoEn;
                                sInvType = sInvoiceTypeFisiko;
                                sSeira = sSeiraFisiko;
                                break;
                            case 2:
                                iInvoiceType = iInvoiceNomiko;
                                sInvoiceCode = sInvoiceCodeNomiko;
                                sInvTitleGr = sInvTitleNomikoGr;
                                sInvTitleEn = sInvTitleNomikoEn;
                                sInvType = sInvoiceTypeNomiko;
                                sSeira = sSeiraNomiko;
                                break;
                            case 4:                                     //  4 - ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ
                                if (iClientType == 1)
                                {
                                    iInvoiceType = iInvoicePistotikoFisiko;
                                    sInvoiceCode = sInvoiceCodePistotikoFisiko;
                                    sSeira = sSeiraPistotikoFisiko;
                                    sInvTitleGr = sInvTitlePistotikoFisikoGr;
                                    sInvTitleEn = sInvTitlePistotikoFisikoEn;
                                    sInvType = sInvoiceTypePistotikoFisiko;
                                }
                                else
                                {
                                    iInvoiceType = iInvoicePistotikoNomiko;
                                    sInvoiceCode = sInvoiceCodePistotikoNomiko;
                                    sSeira = sSeiraPistotikoNomiko;
                                    sInvTitleGr = sInvTitlePistotikoNomikoGr;
                                    sInvTitleEn = sInvTitlePistotikoNomikoEn;
                                    sInvType = sInvoiceTypePistotikoNomiko;
                                }
                                break;
                            case 5:                                             // 5 - ΑΚΥΡΩΤΙΚΟ ΣΗΜΕΙΩΜΑ
                                iInvoiceType = iInvoiceAkyrotiko;
                                sInvoiceCode = sCodeAkyrotiko;
                                sSeira = fgList[iLine, "Invoice_External"].ToString();
                                sInvTitleGr = sInvTitleAkyrotikoGr;
                                sInvTitleEn = sInvTitleAkyrotikoEn;
                                sInvType = sInvoiceTypeAkyrotiko;
                                break;
                        }

                        sApo = Convert.ToDateTime(fgList[iLine, "DateFrom"]).ToString("dd/MM/yyyy");
                        sEos = Convert.ToDateTime(fgList[iLine, "DateTo"]).ToString("dd/MM/yyyy");

                        clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
                        InvoiceTitles.Tipos = iInvoiceType;
                        InvoiceTitles.Seira = sSeira;
                        iNum = Convert.ToInt32(InvoiceTitles.GetInvoice_LastNumber()) + 1;

                        switch (Convert.ToInt32(fgList[iLine, "Service_ID"]))
                        {
                            case 2:                                                                 // 2 - Advisory
                                sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                break;
                            case 3:                                                                 // 3 - Discretionary
                                sAitiologia = "Αμοιβή Διαχείρισης";
                                break;
                            default:
                                sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                break;
                        }

                        // --- Country : Greece or Not ------------
                        if (fgList[iLine, "Country"].ToString() == "" || fgList[iLine, "Country"].ToString() == "Ελλάδα" || fgList[iLine, "Country"].ToString() == "Greece") sCountry = fgList[iLine, "Country"].ToString();
                        else sCountry = fgList[iLine, "CountryEnglish"].ToString();

                        sNum = iNum.ToString();
                        sTemp = sNum;
                        i = sTemp.Trim().Length;
                        switch (i)
                        {
                            case 1: { sNum = "00000000" + iNum; break; }
                            case 2: { sNum = "0000000" + iNum; break; }
                            case 3: { sNum = "000000" + iNum; break; }
                            case 4: { sNum = "00000" + iNum; break; }
                            case 5: { sNum = "0000" + iNum; break; }
                            case 6: { sNum = "000" + iNum; break; }
                            case 7: { sNum = "00" + iNum; break; }
                            case 8: { sNum = "0" + iNum; break; }
                        }

                        WordApp.Visible = false;

                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "\\Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\AdmF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceAFTemplate, sTemp);
                        curDoc = WordApp.Documents.Open(sTemp);

                        sEafdss = "<%SL ;;" + fgList[iLine, "AFM"] + ";;;;;;" + sInvType + ";;" + sNum + ";0;0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00") +
                                  ";0;0;0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00") + ";0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00") + ";" + "EUR" + ";>";

                        curDoc.Content.Find.Execute(FindText: "{title_gr}", ReplaceWith: sInvTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{title_en}", ReplaceWith: sInvTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{surname}", ReplaceWith: fgList[iLine, "User1Name"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{firstname}", ReplaceWith: "", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: iNum, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dIssueDate.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{mfaitiologia_gr}", ReplaceWith: sAitiologia, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{fpa}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString(), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sNewFile = sPDF_FullPath + "\\InvoiceAF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sNewFile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.ScreenUpdating = false;
                        WordApp.Documents.Close();

                        sFileName = Global.DMS_UploadFile(sNewFile, "Customers/" + fgList[iLine, "ContractTitle"] + "/Invoices", Path.GetFileName(sNewFile));

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sFileName), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));                        

                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "Invoice_File"] = Path.GetFileName(sFileName);
                        fgList.Refresh();

                        clsAdminFees_Recs AF_Recs = new clsAdminFees_Recs();
                        AF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        AF_Recs.GetRecord();
                        AF_Recs.Invoice_ID = iID;
                        AF_Recs.DateFees = dIssueDate;
                        AF_Recs.Status = 1;
                        AF_Recs.EditRecord();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            {
                WordApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                sTemp = sPDF_FullPath + "\\AdmF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\AdmF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\AdmF_" + sNum + "_sig.pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);
            }

            bCheckList = true;
        }
        private int SaveRecord(int iRow, int iInvType, string sSeira, int iArithmos, string sInvoiceFile, int iSource_ID, int iContract_ID)
        {
            int iRecord_ID = 0;

            clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
            InvoiceTitles.DateIssued = dIssueDate;
            InvoiceTitles.Tipos = iInvType;
            InvoiceTitles.Seira = sSeira;
            InvoiceTitles.Arithmos = iArithmos;
            InvoiceTitles.Selida = "";
            InvoiceTitles.Client_ID = Convert.ToInt32(fgList[iRow, "Client_ID"]);
            InvoiceTitles.TroposApostolis = 0;
            InvoiceTitles.TroposPliromis = 1;
            InvoiceTitles.Posotita = 0;
            InvoiceTitles.AxiaMikti = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceTitles.Ekptosi = 0;
            InvoiceTitles.AxiaKathari = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceTitles.AxiaFPA = Convert.ToSingle(fgList[iRow, "VAT_Amount"]);
            InvoiceTitles.AxiaTeliki = Convert.ToSingle(fgList[iRow, "FinishAmount"]);
            InvoiceTitles.FileName = sInvoiceFile;
            InvoiceTitles.SourceType = 4;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
            InvoiceTitles.Source_ID = iSource_ID;                                           // Commands.ID
            InvoiceTitles.Contract_ID = iContract_ID;
            InvoiceTitles.OfficialInformingDate = "";
            InvoiceTitles.Author_ID = Global.User_ID;
            InvoiceTitles.DateIns = DateTime.Now;
            iRecord_ID = InvoiceTitles.InsertRecord();

            clsInvoiceRecs InvoiceRecs = new clsInvoiceRecs();
            InvoiceRecs.IT_ID = iRecord_ID;
            InvoiceRecs.Good_Type = 2;                                             // 1-Good, 2- Service
            InvoiceRecs.Good_Code = "";
            InvoiceRecs.Good_Title = "";
            InvoiceRecs.Good_MM = "";
            InvoiceRecs.Price = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.Posotita = 1;
            InvoiceRecs.AxiaMikti = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.EkptosiPercent = 0;
            InvoiceRecs.EkptosiAxia = 0;
            InvoiceRecs.AxiaKathari = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.FPAPercent = 0;
            InvoiceRecs.FPAAxia = Convert.ToSingle(fgList[iRow, "VAT_Amount"]);
            InvoiceRecs.AxiaTeliki = Convert.ToSingle(fgList[iRow, "FinishAmount"]);
            InvoiceRecs.InsertRecord();

            return iRecord_ID;
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
            EXL.Cells[1, 3].Value = "Τιμολόγηση Administration Fees";

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            for (i = 0; i <= (fgList.Rows.Count - 1); i++)
                for (this.j = 2; this.j <= 45; this.j++)
                    EXL.Cells[i + 2, j - 1].Value = fgList[i, j];


            this.Cursor = Cursors.Default;

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void tsbSettings_Click(object sender, EventArgs e)
        {
            frmOptions locOptions = new frmOptions();
            locOptions.StartPosition = FormStartPosition.CenterScreen;
            locOptions.RightsLevel = 2;
            locOptions.VisualFlags = "00000001";
            locOptions.Show();

            DefineOptions();
        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, 1].ToString();
            locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
            locContract.ShowDialog();
        }

        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }
        private void mnuPistotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowRecord();
        }
        private void mnuAkyrotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowRecord();
        }
        private void mnuPrintInvoice_Click(object sender, EventArgs e)
        {
            {
                fgList[fgList.Row, 0] = true;
                PrintInvoice();
            }
        }
        private void ShowInvoice()
        {
            if (fgList[fgList.Row, "Invoice_File"].ToString().Length > 0)
            {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "ContractTitle"] + "\\Invoices", fgList[fgList.Row, "Invoice_File"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }
        }
 
        private void fgPortfolios_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 1 || e.Col == 2 || e.Col == 3 || e.Col == 5 || e.Col == 8 || e.Col == 13)  e.Cancel = false;
            else  e.Cancel = true;
        }
        private void fgPortfolios_AfterEdit(object sender, RowColEventArgs e)
        {
            int i = 0;

            if (e.Col == 1 || e.Col == 2)     // 1 - DateFrom,  2 - DateTo
            {
                i = fgPortfolios.Row;
                TimeSpan t = Convert.ToDateTime(fgPortfolios[i, "DateTo"]) - Convert.ToDateTime(fgPortfolios[i, "DateFrom"]);
                fgPortfolios[i, "Days"] = t.TotalDays + 1;
            }
            if (e.Col == 8)     // Discount_Percent2
            {
                i = fgPortfolios.Row;
                fgPortfolios[i, "AmoiviAfter"] = (float)fgPortfolios[i, "AmoiviPro"] - (float)fgPortfolios[i, "AmoiviPro"] * ((float)fgPortfolios[i, "Discount_Percent1"] + (float)fgPortfolios[i, "Discount_Percent2"]) / 100;
                fgPortfolios[i, "AxiaAfter"] = Convert.ToSingle(fgPortfolios[i, "AUM"]) * Convert.ToSingle(fgPortfolios[i, "AmoiviAfter"]) * Convert.ToSingle(fgPortfolios[i, "Days"]) / (360 * 100);
            }
            if (e.Col == 13)    // MinAmoivi_Percent2
            {
                i = fgPortfolios.Row;
                fgPortfolios[i, "FinishMinAmoivi"] = (float)fgPortfolios[i, "MinAmoivi"] - (float)fgPortfolios[i, "MinAmoivi"] * ((float)fgPortfolios[i, "MinAmoivi_Percent"] + (float)fgPortfolios[i, "MinAmoivi_Percent2"]) / 100;
            }
        }
        private void EditFees()
        {            
            CalcFees_Step1(fgList[fgList.Row, "Code"].ToString());
            panEdit.Visible = true;
        } 
        private void btnCalcFees_Click(object sender, EventArgs e)
        {
            CalcFees_Step2();
        }
        private void tsbSave_Edit_Click(object sender, EventArgs e)
        {
            CalcFees_Step3();
            panEdit.Visible = false;
        }
        private void CalcFees_Step1(string sCode)
        {
            int i, j;

            fgPortfolios.Redraw = false;
            fgPortfolios.Rows.Count = 1;
            j = fgList.Rows.Count - 2;
            for (i = 2; i <= j; i++)
            {
                if (fgList[i, "Code"].ToString() == sCode)
                    fgPortfolios.AddItem(fgList[i, "Portfolio"] + "\t" + Convert.ToDateTime(fgList[i, "DateFrom"]).ToString("dd/MM/yyyy") + "\t" +
                                         Convert.ToDateTime(fgList[i, "DateTo"]).ToString("dd/MM/yyyy") + "\t" + fgList[i, "Days"] + "\t" + fgList[i, "Currency"] + "\t" +
                                         fgList[i, "AUM"] + "\t" + fgList[i, "AmoiviPro"] + "\t" + fgList[i, "Discount_Percent1"] + "\t" + fgList[i, "Discount_Percent2"] + "\t" +
                                         fgList[i, "AmoiviAfter"] + "\t" + fgList[i, "AxiaAfter"] + "\t" + fgList[i, "MinAmoivi"] + "\t" + fgList[i, "MinAmoivi_Percent"] + "\t" +
                                         fgList[i, "MinAmoivi_Percent2"] + "\t" + fgList[i, "FinishMinAmoivi"] + "\t" + fgList[i, "LastAmount"] + "\t" + fgList[i, "VAT_Percent"] + "\t" +
                                         fgList[i, "VAT_Amount"] + "\t" + fgList[i, "FinishAmount"] + "\t" + fgList[i, "LastAmount_Percent"] + "\t" + fgList[i, "ID"] + "\t" +
                                         "0" + "\t" + "0" + "\t" + fgList[i, "MinAmoivi"] + "\t" + fgList[i, "ClientType"] + "\t" + i);
            }
            fgPortfolios.Redraw = true;
        }
        private void CalcFees_Step2()
        {
            int i, j, iDays = 0, iMaxDays = 0;
            float sgMinPro = 0, sgAUM = 0;

            j = fgPortfolios.Rows.Count - 1;
            for (i = 1; i <= j; i++)
            {
                if ((float)fgPortfolios[i, "AUM"] != 0)
                {
                    iDays = Convert.ToInt32(fgPortfolios[i, "Days"]);
                    if (iDays > iMaxDays) iMaxDays = iDays;
                    sgAUM = sgAUM + (float)fgPortfolios[i, "AUM"];
                }
            }

            sgAUM = 0;
            for (i = 1; i <= j; i++)
            {
                if ((float)fgPortfolios[i, "AUM"] != 0)
                {
                    iDays = Convert.ToInt32(fgPortfolios[i, "Days"]);
                    fgPortfolios[i, "AverageAUM"] = (float)fgPortfolios[i, "AUM"] * iDays / iMaxDays;
                    sgAUM = sgAUM + (float)fgPortfolios[i, "AverageAUM"];
                }
            }

            sgMinPro = Convert.ToSingle(fgPortfolios[1, "MinAmoivi_Contract"]) * 4 * iMaxDays / 360;
            for (i = 1; i <= j; i++)
            {
                if (Convert.ToSingle(fgPortfolios[i, "AUM"]) != 0)
                {
                    iDays = Convert.ToInt32(fgPortfolios[i, "Days"]);

                    fgPortfolios[i, "Weights"] = Convert.ToSingle(fgPortfolios[i, "AverageAUM"]) / sgAUM;
                    fgPortfolios[i, "AmoiviAfter"] = Convert.ToSingle(fgPortfolios[i, "AmoiviPro"]) - Convert.ToSingle(fgPortfolios[i, "AmoiviPro"]) * (Convert.ToSingle(fgPortfolios[i, "Discount_Percent1"]) + Convert.ToSingle(fgPortfolios[i, "Discount_Percent2"])) / 100;
                    fgPortfolios[i, "AxiaAfter"] = Convert.ToSingle(fgPortfolios[i, "AUM"]) * iDays * Convert.ToSingle(fgPortfolios[i, "AmoiviAfter"]) / 36000;

                    fgPortfolios[i, "MinAmoivi"] = sgMinPro * Convert.ToSingle(fgPortfolios[i, "Weights"]);
                    fgPortfolios[i, "FinishMinAmoivi"] = Convert.ToSingle(fgPortfolios[i, "MinAmoivi"]) - Convert.ToSingle(fgPortfolios[i, "MinAmoivi"]) * (Convert.ToSingle(fgPortfolios[i, "MinAmoivi_Percent"]) + Convert.ToSingle(fgPortfolios[i, "MinAmoivi_Percent2"])) / 100;
                    if (Convert.ToSingle(fgPortfolios[i, "AxiaAfter"]) >= Convert.ToSingle(fgPortfolios[i, "FinishMinAmoivi"])) fgPortfolios[i, "LastAmount"] = fgPortfolios[i, "AxiaAfter"];
                    else fgPortfolios[i, "LastAmount"] = fgPortfolios[i, "FinishMinAmoivi"];
                    fgPortfolios[i, "VAT_Amount"] = Convert.ToSingle(fgPortfolios[i, "LastAmount"]) * Convert.ToSingle(fgPortfolios[i, "VAT_Percent"]) / 100;
                    fgPortfolios[i, "FinishAmount"] = Convert.ToSingle(fgPortfolios[i, "LastAmount"]) + Convert.ToSingle(fgPortfolios[i, "VAT_Amount"]);

                    fgPortfolios[i, "LastAmount_Percent"] = Convert.ToSingle(fgPortfolios[i, "LastAmount"]) * 36000 / (Convert.ToSingle(fgPortfolios[i, "AUM"]) * iDays);
                }
            }
        }
        private void CalcFees_Step3()
        {
            int i, j;
            clsAdminFees_Recs klsAdminFees_Rec = new clsAdminFees_Recs();

            for (i = 1; i <= fgPortfolios.Rows.Count - 1; i++)
            {
                klsAdminFees_Rec = new clsAdminFees_Recs();
                if (Convert.ToInt32(fgPortfolios[i, "ID"]) == 0)  {
                    klsAdminFees_Rec.AT_ID = iAT_ID;
                    klsAdminFees_Rec.Client_ID = iClient_ID;
                    klsAdminFees_Rec.DateFrom = Convert.ToDateTime(fgPortfolios[i, "DateFrom"]);
                    klsAdminFees_Rec.DateTo = Convert.ToDateTime(fgPortfolios[i, "DateTo"]);
                    klsAdminFees_Rec.Days = Convert.ToInt32(fgPortfolios[i, "Days"]);
                    klsAdminFees_Rec.Code = sCode;
                    klsAdminFees_Rec.Portfolio = sPortfolio;
                    klsAdminFees_Rec.Currency = fgPortfolios[i, "Currency"] +"";
                    klsAdminFees_Rec.Contract_ID = iContract_ID;
                    klsAdminFees_Rec.Contract_Details_ID = iContract_Details_ID;
                    klsAdminFees_Rec.Contract_Packages_ID = iContract_Packages_ID;    
                }
                else {
                    klsAdminFees_Rec.Record_ID = Convert.ToInt32(fgPortfolios[i, "ID"]);
                    klsAdminFees_Rec.GetRecord();
                }
                klsAdminFees_Rec.AUM = Convert.ToDecimal(fgPortfolios[i, "AUM"]);
                klsAdminFees_Rec.AmoiviPro = Convert.ToSingle(fgPortfolios[i, "AmoiviPro"]);
                klsAdminFees_Rec.AmoiviAfter = Convert.ToSingle(fgPortfolios[i, "AmoiviAfter"]);
                klsAdminFees_Rec.AxiaAfter = Convert.ToSingle(fgPortfolios[i, "AxiaAfter"]);
                klsAdminFees_Rec.Discount_Percent1 = Convert.ToSingle(fgPortfolios[i, "Discount_Percent1"]);
                klsAdminFees_Rec.Discount_Percent2 = Convert.ToSingle(fgPortfolios[i, "Discount_Percent2"]);
                klsAdminFees_Rec.Discount_Percent = Convert.ToSingle(fgPortfolios[i, "Discount_Percent1"]) + Convert.ToSingle(fgPortfolios[i, "Discount_Percent2"]);
                klsAdminFees_Rec.MinAmoivi = Convert.ToSingle(fgPortfolios[i, "MinAmoivi"]);
                klsAdminFees_Rec.MinAmoivi_Percent = Convert.ToSingle(fgPortfolios[i, "MinAmoivi_Percent"]);
                klsAdminFees_Rec.MinAmoivi_Percent2 = Convert.ToSingle(fgPortfolios[i, "MinAmoivi_Percent2"]);
                klsAdminFees_Rec.FinishMinAmoivi = Convert.ToDecimal(fgPortfolios[i, "FinishMinAmoivi"]);
                klsAdminFees_Rec.LastAmount = Convert.ToDecimal(fgPortfolios[i, "LastAmount"]);
                klsAdminFees_Rec.VAT_Percent = Convert.ToSingle(fgPortfolios[i, "VAT_Percent"]);
                klsAdminFees_Rec.VAT_Amount = Convert.ToSingle(fgPortfolios[i, "VAT_Amount"]);
                klsAdminFees_Rec.FinishAmount = Convert.ToDecimal(fgPortfolios[i, "FinishAmount"]);
                klsAdminFees_Rec.LastAmount_Percent = Convert.ToSingle(fgPortfolios[i, "LastAmount_Percent"]);
                //klsAdminFees_Rec.Invoice_Type = Convert.ToInt32(fgPortfolios[i, "ClientType"]);      // ClientType=1-idiotis -> Invoice_Type=1-ΑΠΥ, ClientType=2-Etairia -> Invoice_Type=2-ΤΠΥ
                klsAdminFees_Rec.User_ID = Global.User_ID;
                klsAdminFees_Rec.DateEdit = DateTime.Now;
                if (Convert.ToInt32(fgPortfolios[i, "ID"]) != 0)
                {
                    klsAdminFees_Rec.EditRecord();

                    j = Convert.ToInt32(fgPortfolios[i, "fgList_Row"]);
                    fgList[j, "AUM"] = fgPortfolios[i, "AUM"];
                    fgList[j, "Discount_Percent1"] = fgPortfolios[i, "Discount_Percent1"];
                    fgList[j, "Discount_Percent2"] = fgPortfolios[i, "Discount_Percent2"];
                    fgList[j, "MinAmoivi_Percent2"] = fgPortfolios[i, "MinAmoivi_Percent2"];
                    fgList[j, "AmoiviAfter"] = fgPortfolios[i, "AmoiviAfter"];
                    fgList[j, "AxiaAfter"] = fgPortfolios[i, "AxiaAfter"];
                    fgList[j, "FinishMinAmoivi"] = fgPortfolios[i, "FinishMinAmoivi"];
                    fgList[j, "LastAmount"] = fgPortfolios[i, "LastAmount"];
                    fgList[j, "VAT_Percent"] = fgPortfolios[i, "VAT_Percent"];
                    fgList[j, "VAT_Amount"] = fgPortfolios[i, "VAT_Amount"];
                    fgList[j, "FinishAmount"] = fgPortfolios[i, "FinishAmount"];
                    fgList[j, "LastAmount_Percent"] = fgPortfolios[i, "LastAmount_Percent"];
                    fgList[j, "Invoice_Type"] = fgPortfolios[i, "ClientType"];                         // ClientType=1-idiotis -> Invoice_Type=1-ΑΠΥ, ClientType=2-Etairia -> Invoice_Type=2-ΤΠΥ
                }
                else  {
                    klsAdminFees_Rec.InsertRecord();

                    DefineList();
                    ShowList();
                }
            }
        }
        private void ShowRecord()
        {
            panEdit.Visible = true;
        }
        //--- Import file wiath AdminFees Data functions ------------------------------------
        private void tsbImport_Click(object sender, EventArgs e)
        {
            lblYear.Text = cmbYear.Text;
            rbc1.Checked = rb1.Checked;
            rbc2.Checked = rb2.Checked;
            rbc3.Checked = rb3.Checked;
            rbc4.Checked = rb4.Checked;
            rbc5.Checked = rb5.Checked;
            rbc6.Checked = rb6.Checked;
            panImport.Height = 148;
            panImport.Visible = true;
        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void btnGetImport_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Length > 0)
            {
                int iIndex = 0;
                string sTemp = "";

                iAT_ID = 0;

                if (rbc1.Checked) iIndex = 1;
                if (rbc2.Checked) iIndex = 2;
                if (rbc3.Checked) iIndex = 3;
                if (rbc4.Checked) iIndex = 4;
                if (rbc5.Checked) iIndex = 5;
                if (rbc6.Checked) iIndex = 6;

                clsAdminFees_Titles klsAdminFees_Title = new clsAdminFees_Titles();
                klsAdminFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                klsAdminFees_Title.AF_Year = Convert.ToInt32(cmbYear.Text);
                klsAdminFees_Title.AF_Quart = iIndex;
                klsAdminFees_Title.GetRecord_Title();
                iAT_ID = klsAdminFees_Title.Record_ID;
                if (iAT_ID == 0)
                {
                    var ExApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
                    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                    klsAdminFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    klsAdminFees_Title.AF_Quart = iIndex;
                    klsAdminFees_Title.AF_Year = Convert.ToInt32(cmbYear.Text);
                    klsAdminFees_Title.DateIns = DateTime.Now;
                    klsAdminFees_Title.Author_ID = Global.User_ID;
                    iAT_ID = klsAdminFees_Title.InsertRecord();

                    this.Refresh();
                    this.Cursor = Cursors.WaitCursor;

                    i = 1;
                    while (true)
                    {
                        i = i + 1;

                        sTemp = (xlRange.Cells[i, 2].Value + "").ToString();
                        if (sTemp == "") break;

                        clsContracts klsContract = new clsContracts();
                        klsContract.Code = xlRange.Cells[i, 5].Value.ToString();
                        klsContract.Portfolio = xlRange.Cells[i, 6].Value.ToString();
                        klsContract.GetRecord_Code_Portfolio();

                        clsAdminFees_Recs klsAdminFees_Rec = new clsAdminFees_Recs();
                        klsAdminFees_Rec.AT_ID = iAT_ID;
                        klsAdminFees_Rec.Client_ID = Convert.ToInt32(klsContract.Client_ID);
                        klsAdminFees_Rec.DateFrom = Convert.ToDateTime(xlRange.Cells[i, 2].Value.ToString());
                        klsAdminFees_Rec.DateTo = Convert.ToDateTime(xlRange.Cells[i, 3].Value.ToString());
                        klsAdminFees_Rec.Code = xlRange.Cells[i, 5].Value.ToString();
                        klsAdminFees_Rec.Portfolio = xlRange.Cells[i, 6].Value.ToString();
                        klsAdminFees_Rec.Currency = klsContract.Currency + "";
                        klsAdminFees_Rec.Contract_ID = Convert.ToInt32(klsContract.Record_ID);
                        klsAdminFees_Rec.Contract_Details_ID = Convert.ToInt32(klsContract.Contract_Details_ID);
                        klsAdminFees_Rec.Contract_Packages_ID = Convert.ToInt32(klsContract.Contract_Packages_ID);
                        klsAdminFees_Rec.AUM = Convert.ToDecimal(xlRange.Cells[i, 7].Value);
                        klsAdminFees_Rec.Days = Convert.ToInt16(xlRange.Cells[i, 8].Value);
                        klsAdminFees_Rec.AmoiviPro = Convert.ToSingle(xlRange.Cells[i, 9].Value * 100);
                        klsAdminFees_Rec.AxiaPro = 0;
                        klsAdminFees_Rec.AmoiviAfter = Convert.ToSingle(xlRange.Cells[i, 11].Value * 100);
                        klsAdminFees_Rec.AxiaAfter = Convert.ToSingle(xlRange.Cells[i, 12].Value);
                        klsAdminFees_Rec.Discount_Percent1 = 0;
                        klsAdminFees_Rec.Discount_Amount1 = 0;
                        klsAdminFees_Rec.Discount_Percent2 = 0;
                        klsAdminFees_Rec.Discount_Amount2 = 0;
                        klsAdminFees_Rec.Discount_Percent = Convert.ToSingle(xlRange.Cells[i, 10].Value * 100);
                        klsAdminFees_Rec.Discount_Amount = 0;
                        klsAdminFees_Rec.MinAmoivi = Convert.ToSingle(xlRange.Cells[i, 13].Value);
                        klsAdminFees_Rec.MinAmoivi_Percent = Convert.ToSingle(xlRange.Cells[i, 14].Value * 100);
                        klsAdminFees_Rec.MinAmoivi_Percent2 = 0;
                        klsAdminFees_Rec.FinishMinAmoivi = Convert.ToDecimal(xlRange.Cells[i, 15].Value);
                        klsAdminFees_Rec.LastAmount = Convert.ToDecimal(xlRange.Cells[i, 16].Value);
                        klsAdminFees_Rec.LastAmount_Percent = Convert.ToSingle(xlRange.Cells[i, 19].Value * 100);
                        klsAdminFees_Rec.VAT_Amount = Convert.ToSingle(xlRange.Cells[i, 17].Value);
                        klsAdminFees_Rec.VAT_Percent = Convert.ToSingle(klsAdminFees_Rec.VAT_Amount) * 100 / Convert.ToSingle(klsAdminFees_Rec.LastAmount);
                        klsAdminFees_Rec.FinishAmount = Convert.ToDecimal(xlRange.Cells[i, 18].Value);
                        klsAdminFees_Rec.MaxDays = 0;
                        klsAdminFees_Rec.AverageAUM = 0;
                        klsAdminFees_Rec.Weights = 0;
                        klsAdminFees_Rec.MinYearly = 0;
                        klsAdminFees_Rec.Service_ID = klsContract.Service_ID;
                        klsAdminFees_Rec.Invoice_ID = 0;
                        //klsAdminFees_Rec.Invoice_Num = "";
                        //klsAdminFees_Rec.Invoice_File = "";
                        klsAdminFees_Rec.DateFees = Convert.ToDateTime("1900/01/01");
                        //klsAdminFees_Rec.Invoice_Type = 0;
                        klsAdminFees_Rec.Tipos = 0;                                      // 0 - regular record, 4 - pistotiko, 5 - akyrotiko
                        klsAdminFees_Rec.Status = 1;                                     // 1 - Active, 2 - Cancelled
                        klsAdminFees_Rec.User_ID = Global.User_ID;
                        klsAdminFees_Rec.DateEdit = DateTime.Now;
                        klsAdminFees_Rec.InsertRecord();
                    }
                    //cleanup
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //release com objects to fully kill excel process from running in the background
                    Marshal.ReleaseComObject(xlRange);
                    Marshal.ReleaseComObject(xlWorksheet);

                    //close and release
                    xlWorkbook.Close();
                    Marshal.ReleaseComObject(xlWorkbook);

                    //quit and release
                    ExApp.Quit();
                    Marshal.ReleaseComObject(ExApp);

                    DefineList();
                    ShowList();
                    this.Cursor = Cursors.Default;

                    panImport.Height = 328;
                }
                else
                    MessageBox.Show("Snapshot για αυτό το περίοδο ήδη υπάρχει", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Καταχώρήστε το αρχείο εισαγωγής", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picClose_Import_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }
        //-----------------------------------------------------------------------------------
        private void tsbAUM_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = "";
            lblSourceRows.Text = "";
            lblSourceAmount.Text = "";
            lblFoundRows.Text = "";
            lblFoundAmount.Text = "";
            txtUnfound.Text = "";
            panAUM.Height = 132;
            panAUM.Visible = true;
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
            {
                case 2:

                    dialog.Filter = "CSV Files|*.csv;*.txt";
                    dialog.InitialDirectory = @"C:\";
                    dialog.Title = "Please select an file ";

                    if (dialog.ShowDialog() == DialogResult.OK) txtAUMFilePath.Text = dialog.FileName;
                    break;
                default:
                    dialog.Filter = "Excel Files|*.xlsx;*.xls";
                    dialog.InitialDirectory = @"C:\";
                    dialog.Title = "Please select an Excel file ";

                    if (dialog.ShowDialog() == DialogResult.OK) txtAUMFilePath.Text = dialog.FileName;
                    break;
            }
        }

        private void picClose_AUM_Click(object sender, EventArgs e)
        {
            panAUM.Visible = false;
        }
        private void btnOK_AUM_Click(object sender, EventArgs e)
        {
            string sCode = "", sPortfolio = "", sInvoiceExternal = "";
            Decimal decAUM;
            DateTime dFrom, dTo;
            Excel.Range range;

            iSourceRows = 0;
            iFoundRows = 0;
            decSourceAmount = 0;
            decFoundAmount = 0;
            sUnfoundRows = "";

            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
            {
                case 2:                                                                 // 2 - ΠΕΙΡΑΙΩΣ Α.Ε.Π.Ε.Υ.  - ειδικό αρχειο με AUM
                    using (var reader = new StreamReader(@txtAUMFilePath.Text))
                    {
                        List<string> listA = new List<string>();
                        List<string> listB = new List<string>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(';');

                            sPortfolio = values[4];
                            sCode = values[5];
                            dFrom = Convert.ToDateTime(values[0]);
                            dTo = Convert.ToDateTime(values[1]);
                            sInvoiceExternal = values[7];
                            decAUM = Convert.ToDecimal(values[8]);

                            iSourceRows = iSourceRows + 1;
                            decSourceAmount = decSourceAmount + decAUM;
                            if (decAUM != 0) SaveAUM(sCode, sPortfolio, sInvoiceExternal, dFrom, dTo, decAUM, false);
                        }
                    }
                    break;

                default:                                                               // other providers - same file for all other providers that make HF Accounting Officer
                    Excel.Application excelApp = new Excel.Application();
                    if (excelApp != null)
                    {
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(@txtAUMFilePath.Text, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                        Excel.Range excelRange = excelWorksheet.UsedRange;
                        int rowCount = excelRange.Rows.Count;

                        try
                        {
                            for (int i = 2; i <= rowCount; i++)
                            {

                                range = (excelWorksheet.Cells[i, 1] as Excel.Range);
                                dFrom = Convert.ToDateTime(range.Value.ToString());

                                range = (excelWorksheet.Cells[i, 2] as Excel.Range);
                                dTo = Convert.ToDateTime(range.Value.ToString());

                                range = (excelWorksheet.Cells[i, 3] as Excel.Range);
                                sCode = range.Value.ToString();

                                range = (excelWorksheet.Cells[i, 4] as Excel.Range);
                                sPortfolio = range.Value.ToString();

                                range = (excelWorksheet.Cells[i, 5] as Excel.Range);
                                decAUM = Convert.ToDecimal(range.Value);

                                iSourceRows = iSourceRows + 1;
                                decSourceAmount = decSourceAmount + decAUM;
                                if (decAUM != 0) SaveAUM(sCode, sPortfolio, sInvoiceExternal, dFrom, dTo, decAUM, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\nError Line N = " + i.ToString() + "  Code = " + sCode + "   Portfolio = " + sPortfolio,
                                            Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            excelWorkbook.Close();
                            excelApp.Quit();
                        }
                    }
                    break;
            }

            DefineList();
            ShowList();

            //--- Calc Admin Fees ---------------------------------
            j = fgList.Rows.Count - 2;
            for (i = 2; i <= j; i++)
            {
                //--- step 1. prepare fgPortfolios
                CalcFees_Step1(fgList[i, "Code"].ToString());

                //--- step 2. calc fees -------------------------------
                CalcFees_Step2();

                //--- step 3. save fees into AdminFees_Recs table -----
                CalcFees_Step3();

            }

            DefineList();
            ShowList();

            lblSourceRows.Text = iSourceRows.ToString();
            lblSourceAmount.Text = decSourceAmount.ToString();
            lblFoundRows.Text = iFoundRows.ToString();
            lblFoundAmount.Text = decFoundAmount.ToString();
            txtUnfound.Text = sUnfoundRows;
            panAUM.Height = 316;
        }
        private void SaveAUM(string sCode, string sPortfolio, string sInvoiceExternal, DateTime dFrom, DateTime dTo, decimal decAUM, bool bCheckDates)
        {
            DataRow[] foundRows;
            string sFilter = "";

            if (bCheckDates) sFilter = "Code= '" + sCode + "' and Portfolio = '" + sPortfolio + "' and DateFrom = '" + dFrom.ToString("dd/MM/yyyy") + "' and DateTo = '" + dTo.ToString("dd/MM/yyyy") + "'";
            else sFilter = "Code= '" + sCode + "' and Portfolio = '" + sPortfolio + "'";
            
            foundRows = klsAdminFees_Recs.List.Select(sFilter);
            if (foundRows.Length > 0)
            {
                //--- save AUM  ---------------------------------
                clsAdminFees_Recs AdminFees_Recs = new clsAdminFees_Recs();
                AdminFees_Recs.Record_ID = Convert.ToInt32(foundRows[0]["ID"]);
                AdminFees_Recs.GetRecord();
                AdminFees_Recs.AUM = decAUM;
                AdminFees_Recs.EditRecord();

                iFoundRows = iFoundRows + 1;
                decFoundAmount = decFoundAmount + decAUM;
            }
            else
                sUnfoundRows = sUnfoundRows + "Code= '" + sCode + "' Portfolio = '" + sPortfolio + "'   Amount =" + decAUM.ToString() + (char)13 + (char)10;
        }

        //-----------------------------------------------------------------------------------
        private void DefineOptions()
        {
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            //sFeesFilePath = Options.FeesFilePath;
            sInvoicePrinter = Options.InvoicePrinter;
            iCopies = Options.InvoiceCopies;

            iInvoiceFisiko = Options.InvoiceFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitleFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitleFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeFisiko = foundRows[0]["Type"].ToString();
            }
            //sSeiraFisiko = Options.SeiraFisiko;

            iInvoiceNomiko = Options.InvoiceNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitleNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitleNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeNomiko = foundRows[0]["Type"].ToString();
            }
            //sSeiraNomiko = Options.SeiraNomiko;

            iInvoicePistotikoFisiko = Options.InvoicePistotikoFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoFisiko = foundRows[0]["Type"].ToString();
            }

            iInvoicePistotikoNomiko = Options.InvoicePistotikoNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoNomiko = foundRows[0]["Type"].ToString();
            }

            iInvoiceAkyrotiko = Options.InvoiceAkyrotiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceAkyrotiko);
            if (foundRows.Length > 0)
            {
                sInvTitleAkyrotikoGr = foundRows[0]["Title"].ToString();
                sInvTitleAkyrotikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeAkyrotiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeAkyrotiko = foundRows[0]["Type"].ToString();
            }

            sInvoiceAFTemplate = Options.InvoiceAFTemplate;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            int i = 0, iClientType = 0;
            float fltVAT_Percent = 0;

            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            iClient_ID = stContract.Client_ID;
            iContract_ID = stContract.Contract_ID;
            iContract_Details_ID = stContract.Contracts_Details_ID;
            iContract_Packages_ID = stContract.Contracts_Packages_ID;            
            sCode = stContract.Code;
            sPortfolio = stContract.Portfolio;
            fltVAT_Percent = stContract.VAT_Percent;
            iClientType = stContract.ClientType;

            clsContracts Contract = new clsContracts();
            Contract.Code = sCode;
            Contract.DateStart = dStart;
            Contract.DateFinish = dFinish;
            Contract.GetPortfolio_Code();
            foreach (DataRow dtRow in Contract.List.Rows)
            {
                i = i + 1;
                fgPortfolios.AddItem(dtRow["Portfolio"] + "\t" + Convert.ToDateTime(dStart).ToString("dd/MM/yyyy") + "\t" +
                     Convert.ToDateTime(dFinish).ToString("dd/MM/yyyy") + "\t" + "90" + "\t" + dtRow["Currency"] + "\t" +
                     "0" + "\t" + dtRow["AdminFeesPercent"] + "\t" + dtRow["AdminFees_Discount"] + "\t" + "0" + "\t" +
                     dtRow["AdminFeesPercent"] + "\t" + "0" + "\t" + dtRow["Admin_MonthMinAmount"] + "\t" + "0" + "\t" +
                     "0"+ "\t" + "0" + "\t" + "0" + "\t" + fltVAT_Percent + "\t" +
                     "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                     "0" + "\t" + "0" + "\t" + "0" + "\t" + iClientType + "\t" + i);
            }
            fgPortfolios.Redraw = true;

        }
        private void panEdit_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panEdit.Location = new Point(this.panEdit.Location.X + e.X - this.position.X, this.panEdit.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panEdit_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
