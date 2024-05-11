using System;
using System.Data;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Threading;
using Core;

namespace Reports
{
    public partial class frmPeriodicalEvaluation : Form
    {
        DataTable dtCommandsList;
        DataColumn dtCol;
        DataRow dtRow;
        int i, j, iRow, iRightsLevel;
        string[] sEkthesiKatalilotitas = new string[11];
        clsExPostCost_Title klsExPostCost_Title = new clsExPostCost_Title();
        clsExPostCost_Recs klsExPostCost_Recs = new clsExPostCost_Recs();
        clsContracts klsContract = new clsContracts();
        clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
        clsContracts_PeriodicalEvaluation klsContracts_PeriodicalEvaluation = new clsContracts_PeriodicalEvaluation();
        public frmPeriodicalEvaluation()
        {
            InitializeComponent();

            panFinish.Left = (Screen.PrimaryScreen.Bounds.Width - panFinish.Width) / 2;
            panFinish.Top = (Screen.PrimaryScreen.Bounds.Height - panFinish.Height) / 2;
        }

        private void frmPeriodicalEvaluation_Load(object sender, EventArgs e)
        {
            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);
            cmbYear.SelectedItem = DateTime.Now.Year;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.ShowCellLabels = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 140;
            fgList.Width = this.Width - 30;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            clsContracts klsContract = new clsContracts();
            klsContract.DateStart = Convert.ToDateTime("01/01/" + cmbYear.Text);
            klsContract.DateFinish = Convert.ToDateTime("31/12/" + cmbYear.Text);
            klsContract.Record_ID = 0;
            klsContract.Status = -1;
            klsContract.GetPeriodicalEvaluation();
            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;
            foreach (DataRow dtRow in klsContract.List.Rows) {
                if (Convert.ToInt32(dtRow["ExecutedCommandsCount"]) > 0) {
                    i = i + 1;
                    fgList.AddItem(false + "\t" + i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["PackageProvider_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                   dtRow["ServiceTitle"] + "\t" + dtRow["InvestProfile"] + "\t" + dtRow["PortfolioManager"] + "\t" + dtRow["ContactDetails"] + "\t" +
                                   Convert.ToDateTime(dtRow["DateStart"]).ToString("dd/MM/yyyy") + "\t" + Convert.ToDateTime(dtRow["DateFinish"]).ToString("dd/MM/yyyy") + "\t" + 
                                   dtRow["Days"] + "\t" + dtRow["FileName"] + "\t" +  dtRow["DateSent"] + "\t" + dtRow["ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + 
                                   dtRow["Contracts_Packages_ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["MiFID_Risk"] + "\t" + dtRow["Contracts_PeriodicalEvaluation_ID"]);
                }
            }
            fgList.Redraw = true;
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
            EXL.Cells[1, 3].Value = "ExPostCost";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 1; this.j <= 14; this.j++)
                    EXL.Cells[i + 2, j].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }    

        private void tsbCreatePDF_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;
            panFinish.Visible = true;

            dtCommandsList = new DataTable("List");
            dtCol = dtCommandsList.Columns.Add("AA", Type.GetType("System.Int32"));
            dtCol = dtCommandsList.Columns.Add("Aktion", Type.GetType("System.String"));
            dtCol = dtCommandsList.Columns.Add("DateIns", Type.GetType("System.String"));
            dtCol = dtCommandsList.Columns.Add("Product_Type", Type.GetType("System.String"));
            dtCol = dtCommandsList.Columns.Add("Title", Type.GetType("System.String"));
            dtCol = dtCommandsList.Columns.Add("ISIN", Type.GetType("System.String"));
            dtCol = dtCommandsList.Columns.Add("Currency", Type.GetType("System.String"));


            string sApo, sEos, sPDF_FullPath, sPeriodicalEvaluationTemplate, sNewFile, sLastFileName;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            clsOptions klsOptions = new clsOptions();
            klsOptions.GetRecord();

            sPeriodicalEvaluationTemplate = "PeriodicSuitabilityTemplate.docx";   

            sPDF_FullPath = "";
            for (iRow = 1; iRow <= fgList.Rows.Count - 1; iRow++) {
                if (Convert.ToBoolean(fgList[iRow, 0])) {
                    try {
                        j = 0;
                        dtCommandsList.Clear();
                        klsOrderSecurity = new clsOrdersSecurity();
                        klsOrderSecurity.CommandType_ID = 0;
                        klsOrderSecurity.DateFrom = Convert.ToDateTime(fgList[iRow, "DateStart"]);
                        klsOrderSecurity.DateTo = Convert.ToDateTime(fgList[iRow, "DateFinish"]);
                        klsOrderSecurity.ServiceProvider_ID = 0;
                        klsOrderSecurity.Sent = 0;
                        klsOrderSecurity.Actions = 0;
                        klsOrderSecurity.User1_ID = 0;
                        klsOrderSecurity.User4_ID = 0;
                        klsOrderSecurity.Division_ID = 0;
                        klsOrderSecurity.Code = fgList[iRow, "Code"] + "";
                        klsOrderSecurity.GetList();
                        foreach (DataRow dtRow1 in klsOrderSecurity.List.Rows)
                        {
                            if (Convert.ToDateTime(dtRow1["ExecuteDate"]) != Convert.ToDateTime("1900/01/01") && (dtRow1["Portfolio"]+"" == fgList[iRow, "Portfolio"]+""))
                            {
                                j = j + 1;
                                dtRow = dtCommandsList.NewRow();
                                dtRow["AA"] = j;
                                dtRow["Aktion"] = (Convert.ToInt32(dtRow1["Aktion"]) == 1 ? "Αγορά" : "Πώληση");
                                dtRow["DateIns"] = Convert.ToDateTime(dtRow1["ExecuteDate"]).ToString("dd/MM/yyyy");
                                dtRow["Product_Type"] = dtRow1["Product_Title"] + "";
                                dtRow["Title"] = dtRow1["Share_Title"] + "";
                                dtRow["ISIN"] = dtRow1["Share_ISIN"] + "";
                                dtRow["Currency"] = dtRow1["Currency"] + "";
                                dtCommandsList.Rows.Add(dtRow);
                            }
                        }
                        WordApp.Visible = false;

                        sApo = Convert.ToDateTime(fgList[iRow, "DateStart"] + "").ToString("dd/MM/yyyy");
                        sEos = Convert.ToDateTime(fgList[iRow, "DateFinish"] + "").ToString("dd/MM/yyyy");


                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "/Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        if (File.Exists(sPDF_FullPath + "/" + sPeriodicalEvaluationTemplate)) File.Delete(sPDF_FullPath + "/" + sPeriodicalEvaluationTemplate);

                        sNewFile = "PeriodicSuitability_" + fgList[iRow, "Code"] + "_" + fgList[iRow, "AA"] + Path.GetExtension(sPeriodicalEvaluationTemplate);
                        if (File.Exists(sPDF_FullPath + "/" + sNewFile)) File.Delete(sPDF_FullPath + "/" + sNewFile);

                        File.Copy(Application.StartupPath + "/Templates/" + sPeriodicalEvaluationTemplate, sPDF_FullPath + "/" + sNewFile);
                        curDoc = WordApp.Documents.Open(sPDF_FullPath + "/" + sNewFile);

                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iRow, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iRow, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iRow, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iRow, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{custodian_title}", ReplaceWith: fgList[iRow, "ProviderTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{profile_title}", ReplaceWith: fgList[iRow, "InvestProfile"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio_manager}", ReplaceWith: fgList[iRow, "PortfolioManager"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contact_details}", ReplaceWith: fgList[iRow, "ContactDetails"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        curDoc.Content.Find.Execute(FindText: "{iss_date}", ReplaceWith: DateTime.Now.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{reference_period}", ReplaceWith: sApo + " - " + sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{days}", ReplaceWith: fgList[iRow, "Days"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        //--- таблица со списком использованных инструментов -------------------------
                        Microsoft.Office.Interop.Word.Table oTable1;
                        Microsoft.Office.Interop.Word.Paragraph oImageScale, oTable1Paragraf;

                        oImageScale = curDoc.Content.Paragraphs.Add(curDoc.Bookmarks["image_place"].Range);
                        oImageScale.Range.Select();

                        switch (Convert.ToInt32(fgList[iRow, "MiFID_Risk"])) {
                            case 1:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Χαμηλού Κινδύνου (Low Risk) - Εισοδήματος";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος ";
                                sEkthesiKatalilotitas[9] = "(ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). ";
                                sEkthesiKatalilotitas[10] = "Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 3 έτη.";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 1 ½ έτος ";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 2";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_1.png");
                                break;
                            case 2:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Μεσαίου Κινδύνου (Medium Risk) - Εισοδήματος";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος ";
                                sEkthesiKatalilotitas[9] = "(ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). ";
                                sEkthesiKatalilotitas[10] = "Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 7 έτη.";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 3 έτη";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 4";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_2.png");
                                break;
                            case 3:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Μεσαίου Κινδύνου (Medium Risk) – Εισοδήματος και Κεφαλαιακής Ανάπτυξης";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος) ";
                                sEkthesiKatalilotitas[9] = "αλλά και η επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μικτά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια. ";
                                sEkthesiKatalilotitas[10] = " Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 7 έτη.";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 5 έτη";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 5";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_3.png");
                                break;
                            case 4:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Εισοδήματος";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος και κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος ";
                                sEkthesiKatalilotitas[9] = "(ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). ";
                                sEkthesiKatalilotitas[10] = "Δεν υπάρχει μέγιστη διάρκεια χρηματοπιστωτικών μέσων εισοδήματος.";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς και άλλα νομίσματα";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 7";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_4.png");
                                break;
                            case 5:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Εισοδήματος και Κεφαλαιακής Ανάπτυξης";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος)";
                                sEkthesiKatalilotitas[9] = " αλλά και η επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχές, μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μικτά αμοιβαία κεφάλαια ";
                                sEkthesiKatalilotitas[10] = " και διαπραγματεύσιμα αμοιβαία κεφάλαια. Δεν υπάρχει μέγιστη διάρκεια χρηματοπιστωτικών μέσων εισοδήματος.";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς και άλλα νομίσματα";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 7";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_5.png");
                                break;
                            case 6:
                                sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                                sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Κεφαλαιακής Ανάπτυξης";
                                sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                                sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχές, ";
                                sEkthesiKatalilotitas[9] = "μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, και μέσα χρηματαγοράς.";
                                sEkthesiKatalilotitas[10] = "";
                                sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς και άλλα νομίσματα";
                                sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                                sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                                sEkthesiKatalilotitas[8] = "έως 7";
                                oImageScale.Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/images/EK_6.png");
                                break;
                        }
 
                        curDoc.Content.Find.Execute(FindText: "{profile_title2}", ReplaceWith: sEkthesiKatalilotitas[2], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{client_type}", ReplaceWith: sEkthesiKatalilotitas[3], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_goal}", ReplaceWith: sEkthesiKatalilotitas[4], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_goal2}", ReplaceWith: sEkthesiKatalilotitas[9], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_goal3}", ReplaceWith: sEkthesiKatalilotitas[10], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{currency_text}", ReplaceWith: sEkthesiKatalilotitas[5], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_horizont}", ReplaceWith: sEkthesiKatalilotitas[6], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{loss_text}", ReplaceWith: sEkthesiKatalilotitas[7], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{risk_category}", ReplaceWith: sEkthesiKatalilotitas[8], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
     
                        oTable1Paragraf = curDoc.Content.Paragraphs.Add(curDoc.Bookmarks["commands_table"].Range);
                        oTable1 = curDoc.Content.Tables.Add(oTable1Paragraf.Range, dtCommandsList.Rows.Count + 1, 7);
                        oTable1.Range.Font.Name = "Arial";
                        oTable1.Range.ParagraphFormat.SpaceBefore = 0;
                        oTable1.Range.ParagraphFormat.SpaceAfter = 0;
                        oTable1.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        oTable1.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        WordApp.Selection.Font.Underline = 0;
                        WordApp.Selection.Font.Italic = 0;

                        oTable1.Cell(1, 1).Select();
                        WordApp.Selection.TypeText("ΑΑ");

                        oTable1.Cell(1, 2).Select();
                        WordApp.Selection.TypeText("Πράξη");

                        oTable1.Cell(1, 3).Select();
                        WordApp.Selection.TypeText("Ημερομηνία εκτέλεσης");

                        oTable1.Cell(1, 4).Select();
                        WordApp.Selection.TypeText("Τύπος προϊόντος");

                        oTable1.Cell(1, 5).Select();
                        WordApp.Selection.TypeText("Τίτλος");

                        oTable1.Cell(1, 6).Select();
                        WordApp.Selection.TypeText("ISIN");

                        oTable1.Cell(1, 7).Select();
                        WordApp.Selection.TypeText("Νόμισμα");

                        oTable1.Rows[1].Range.Font.Name = "Arial";
                        oTable1.Rows[1].Range.Font.Size = 8;
                        oTable1.Rows[1].Range.Font.Bold = 1;
                        oTable1.Rows[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        i = 1;
                        foreach (DataRow dtRow in dtCommandsList.Rows) {
                            i = i + 1;
                            oTable1.Cell(i, 1).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["AA"]+"");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

                            oTable1.Cell(i, 2).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["Aktion"] + "");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                            oTable1.Cell(i, 3).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            oTable1.Cell(i, 3).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["DateIns"] + "");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                            oTable1.Cell(i, 4).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            oTable1.Cell(i, 4).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["Product_Type"] + "");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                            oTable1.Cell(i, 5).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            oTable1.Cell(i, 5).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["Title"] + "");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                            oTable1.Cell(i, 6).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            oTable1.Cell(i, 6).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["ISIN"] + "");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                            oTable1.Cell(i, 7).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            oTable1.Cell(i, 7).Select();
                            //WordApp.Selection.Font.Underline = 0;
                            //WordApp.Selection.Font.Bold = 0;
                            //WordApp.Selection.Font.Italic = 0;
                            WordApp.Selection.TypeText(dtRow["Currency"]+"");
                            WordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }

                        oTable1.Columns[1].Width = WordApp.InchesToPoints((float)0.4);
                        oTable1.Columns[2].Width = WordApp.InchesToPoints((float)0.6);
                        oTable1.Columns[3].Width = WordApp.InchesToPoints((float)0.8);
                        oTable1.Columns[4].Width = WordApp.InchesToPoints((float)0.8);
                        oTable1.Columns[5].Width = WordApp.InchesToPoints((float)2.8);
                        oTable1.Columns[6].Width = WordApp.InchesToPoints((float)1.0);
                        oTable1.Columns[7].Width = WordApp.InchesToPoints((float)0.7);
                        //--- finish таблица  -----------------

                        sLastFileName = sPDF_FullPath + "/PeriodicSuitability_" + fgList[iRow, "Code"] + "_" + fgList[iRow, "AA"] + ".pdf";
                        curDoc.SaveAs2(sLastFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        Thread.Sleep(3000);
                        sLastFileName = Global.DMS_UploadFile(sLastFileName, "Customers/" + fgList[iRow, "ContractTitle"] + "/Informing", Path.GetFileName(sLastFileName));

     
                        fgList[iRow, "FileName"] = Path.GetFileName(sLastFileName);

                        if (Convert.ToInt32(fgList[iRow, "Contracts_PeriodicalEvaluation_ID"]) == 0) {
                            klsContracts_PeriodicalEvaluation = new clsContracts_PeriodicalEvaluation();
                            klsContracts_PeriodicalEvaluation.Contract_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsContracts_PeriodicalEvaluation.Year = Convert.ToInt32(cmbYear.Text);
                            klsContracts_PeriodicalEvaluation.FileName = fgList[iRow, "FileName"]+"";
                            klsContracts_PeriodicalEvaluation.DateSent = "";
                            fgList[iRow, "Contracts_PeriodicalEvaluation_ID"] = klsContracts_PeriodicalEvaluation.InsertRecord();
                        }
                        else {
                            klsContracts_PeriodicalEvaluation = new clsContracts_PeriodicalEvaluation();
                            klsContracts_PeriodicalEvaluation.Record_ID = Convert.ToInt32(fgList[iRow, "Contracts_PeriodicalEvaluation_ID"]);
                            klsContracts_PeriodicalEvaluation.GetRecord();
                            klsContracts_PeriodicalEvaluation.FileName = fgList[iRow, "FileName"]+"";
                            klsContracts_PeriodicalEvaluation.DateSent = "";
                            klsContracts_PeriodicalEvaluation.EditRecord();
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    finally { }
                }  
                fgList[iRow, 0] = false;
            }  

            WordApp.Documents.Close();
            WordApp.Application.Quit();
            WordApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Thread.Sleep(5000);
            //--- delete temporary files .docx and .pdf ----------------------------------------
            foreach (string f in Directory.EnumerateFiles(sPDF_FullPath, "PeriodicSuitability_*.*"))
            {
                File.Delete(f);
            }

            panFinish.Visible = false;
            this.Refresh();
            this.Cursor = Cursors.Default;
        }    
        private void mnuShowPDF_Click(object sender, EventArgs e)
        {
            if (fgList[fgList.Row, "FileName"].ToString().Length > 0)
            {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "ContractTitle"] + "\\Informing", fgList[fgList.Row, "FileName"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }
        }

        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientFullName = fgList[fgList.Row, "ContractTitle"].ToString();
            locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
            locContract.ShowDialog();
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {

        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
