using C1.Win.C1FlexGrid;
using Core;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucSpecCateg : UserControl
    {
        int i = 0, j = 0, iClient_ID = 0;
        string sDescription = "", sCategoriesList = "", sClientName = "", sSpecialCategory = "", sFileFullPath = "", sNewFileName = "", sID = "0";
        string[] tokens;
        bool bCheckList;
        DataRow[] foundRows;
        SpecCategData SpecCateg_Data;
        clsClients Clients = new clsClients();
        clsClients_SpecialCategories Clients_SpecialCategories = new clsClients_SpecialCategories();
        clsClientsDocFiles ClientDocFiles = new clsClientsDocFiles();
        public ucSpecCateg()
        {
            InitializeComponent();
        }

        private void ucSpecCateg_Load(object sender, EventArgs e)
        {

        }
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            bCheckList = false;
            chkDenAniko.Checked = false;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_CellChanged);
            CellStyle cellStyleWrapped = fgList.Styles.Add("Wrapped", fgList.Styles.Normal);
            cellStyleWrapped.WordWrap = true;

            sClientName = sCN;
            iClient_ID = iKlient_ID;
            Clients = new clsClients();
            Clients.Record_ID = iClient_ID;
            Clients.GetRecord();
            sSpecialCategory = Clients.SpecialCategory;

            if (sSpecialCategory.Trim().Length == 0) chkDenAniko.Checked = true;
            else chkDenAniko.Checked = false;

            fgList.AddItem("1" + "\t" + "" + "\t" + "Μέλος  του  διοικητικού  συμβουλίου  ή  πρόσωπο  που  ασκεί  διευθυντικά  καθήκοντα  σε  εταιρία  εισηγμένη  σε  Χρηματιστήριο  ή πρόσωπο που έχει στενό δεσμό  µε τα παραπάνω πρόσωπα " + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("2" + "\t" + "" + "\t" + "Πρόσωπο  που  έχει  περιληφθεί  σε  κατάλογο  προσώπων  που  έχουν  πρόσβαση  σε  προνομιακές  πληροφορίες  κατά  την  έννοια  της Απόφασης 3/347/12.7.2005 του Δ.Σ. της Ε.Κ." + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("3" + "\t" + "" + "\t" + "Νομικό πρόσωπο ή εµπίστευµα, όπου τα διευθυντικά καθήκοντα ασκούνται από πρόσωπο των περιπτώσεων 1 και 2" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("4" + "\t" + "" + "\t" + "Μέτοχος  µε  συµµετοχή  µμεγαλύτερη  του  3%  του  κεφαλαίου  εταιρίας  εισηγμένης  σε  Χρηματιστήριο  ή  εταιρίας  συνδεδεμένης  µε εισηγμένη" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("5" + "\t" + "" + "\t" + "Μέλος  του  Διοικητικού  Συμβουλίου  ή  διευθυντικό  στέλεχος ή άλλο αρμόδιο πρόσωπο ΕΠΕΥ,  Πιστωτικού  Ιδρύματος, συνδεδεμένου  αντιπροσώπου  αυτών  ή  ΑΕΕΔ" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("6" + "\t" + "" + "\t" + "Μέλος  του Διοικητικού Συμβουλίου ή  διευθυντικό στέλεχος ή  και µέλος Επενδυτικών Επιτροπών) Εταιρίας Επενδύσεων Χαρτοφυλακίου, Εταιρίας Διαχείρισης Αμοιβαίων Κεφαλαίων και θεσμικών επενδυτών" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("7" + "\t" + "" + "\t" + "Μέλος του Διοικητικού Συμβουλίου ή πρόσωπο που κατέχει διευθυντική ή απλή υπαλληλική θέση σε οργανωμένη αγορά, ΠΜΔ, ΜΟΔ" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("8" + "\t" + "" + "\t" + "Μέλος  Διοικητικού  Συμβουλίου,  πρόσωπο  που  κατέχει  διευθυντική  θέση  ή  ελεγκτής  στην  Επιτροπή  Κεφαλαιαγοράς  ή  σε  άλλη αντίστοιχη εποπτική αρχή οποιουδήποτε κράτους" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("9" + "\t" + "" + "\t" + "Μέλος  των  οργάνων  διοίκησης  σωματείων  ή  άλλων  ενώσεων  προσώπων,  που  εκπροσωπούν  µέλη  Χρηματιστηρίου,  θεσμικούς επενδυτές, µετόχους" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("10" + "\t" + "" + "\t" + "Δημοσιογράφος ή προσφέρων δημοσιογραφικές  υπηρεσίες  σε  Μ.Μ.Ε.,  που  προσφέρει  πληροφόρηση  ή  σχολιασμό  σε  τακτική  βάση  επί  θεμάτων  που αφορούν την αγορά κεφαλαίων" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("11" + "\t" + "" + "\t" + "Πολιτικώς Εκτεθειμένου Προσώπου, κατά το τελευταίο έτος, κατά την έννοια του νόμου 4557/2018" + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            fgList.AddItem("12" + "\t" + "" + "\t" + "Σύζυγος ή συγγενής α' βαθμού (γονέας, τέκνο) µε πρόσωπο που έχει µία από τις ανωτέρω   ιδιότητες 1,2 και 4 μέχρι 12." + "\t" + "" + "\t0" + "\t" + "" + "\t0");
            for (i = 1; i <= 12; i++) fgList.SetCellStyle(i, 2, cellStyleWrapped);
            fgList.AutoSizeRows();
            fgList.Redraw = true;

            Clients_SpecialCategories = new clsClients_SpecialCategories();
            Clients_SpecialCategories.Client_ID = iClient_ID;
            Clients_SpecialCategories.GetList();

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                tokens = sDescription.Split('~');

                fgList.Redraw = false;
                for (i = 0; i < tokens.Length; i++)
                {
                    if ((tokens[i] + "") != "")
                    {
                        SpecCateg_Data = JsonConvert.DeserializeObject<SpecCategData>(tokens[i]);
                        j = Convert.ToInt32(SpecCateg_Data.cat);
                        if (j == 0)
                        {
                            chkDenAniko.Checked = true;
                            fgList.Enabled = false;
                        }
                        else
                        {
                            chkDenAniko.Checked = false;
                            fgList[j, 1] = true;
                            fgList[j, "File_Name"] = SpecCateg_Data.file_name + "";

                            sID = "0";
                            if (Clients_SpecialCategories.List.Rows.Count > 0)
                            {
                                foundRows = Clients_SpecialCategories.List.Select("SpecCategory_ID = " + j);
                                if (foundRows.Length > 0) sID = foundRows[0]["ClientsDocFiles_ID"] + "";
                            }

                            fgList[j, "ID"] = sID;
                            fgList[j, "FullFileName"] = "";
                            fgList[j, "Edit"] = "1";
                            fgList.Enabled = true;
                        }
                    }
                }
                fgList.Redraw = true;
                fgList.Visible = true;
            }
            else
            {
                tokens = sCategoriesList.Split(';');
                for (i = 0; i < tokens.Length; i++)
                    if (tokens[i] != "")
                    {
                        j = Convert.ToInt32(tokens[i]);                     // j - searched category
                        fgList[j, 1] = true;

                        foundRows = Clients_SpecialCategories.List.Select("SpecCategory_ID = " + j);
                        if (foundRows.Length > 0)
                        {
                            fgList[j, "ID"] = foundRows[0]["ClientsDocFiles_ID"] + "";
                            fgList[j, 3] = foundRows[0]["FileName"] + "";
                            fgList[j, "FullFileName"] = "";
                            fgList[j, "Edit"] = "0";
                        }
                        fgList.Enabled = true;
                    }
            }
            CreateDescription();
            bCheckList = true;
        }
        private void chkDenAniko_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                if (chkDenAniko.Checked)
                {
                    bCheckList = false;
                    for (i = 1; i < fgList.Rows.Count; i++)
                    {
                        fgList[i, 1] = false;
                        fgList[i, "File_Name"] = "";
                        fgList[i, "ID"] = "0";
                        fgList[i, "FullFileName"] = "";
                        fgList[i, "Edit"] = "0";
                    }
                    fgList.Enabled = false;
                    bCheckList = true;
                }
                else fgList.Enabled = true;

                CreateDescription();
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            int iSpecCategory_ID = 0;
            if (bCheckList)
            {
                if (e.Col == 1)
                {
                    if (Convert.ToBoolean(fgList[fgList.Row, 1] + ""))
                    {
                        iSpecCategory_ID = e.Row;
                        sFileFullPath = Global.FileChoice(Global.DefaultFolder);
                        if (sFileFullPath.Length > 0)
                        {
                            sNewFileName = Path.GetFileNameWithoutExtension(sFileFullPath) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sFileFullPath);
                            fgList[e.Row, "File_Name"] = sNewFileName;
                            fgList[e.Row, "ID"] = "0";
                            fgList[e.Row, "FullFileName"] = sFileFullPath;
                            fgList[e.Row, "Edit"] = "1";

                            if (Path.GetDirectoryName(sFileFullPath) != Global.DMSTransferPoint)
                            {   // Source file isn't in DMS TransferPoint folder, so ...
                                if (File.Exists(Global.DMSTransferPoint + "/" + sNewFileName))
                                    sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                                File.Copy(sFileFullPath, Global.DMSTransferPoint + "/" + sNewFileName);         // ... copy this file into DMS TransferPoint folder
                            }

                            clsServerJobs ServerJobs = new clsServerJobs();
                            ServerJobs.JobType_ID = 19;
                            ServerJobs.Source_ID = 0;
                            ServerJobs.Parameters = "{'source_file_full_name': '" + sFileFullPath.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '9434', " +
                                                    "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                                    iClient_ID + "', 'status' : '1'}";
                            ServerJobs.DateStart = DateTime.Now;
                            ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                            ServerJobs.PubKey = "";
                            ServerJobs.PrvKey = "";
                            ServerJobs.Attempt = 0;
                            ServerJobs.Status = 0;
                            ServerJobs.InsertRecord();

                            foundRows = Clients_SpecialCategories.List.Select("SpecCategory_ID = " + iSpecCategory_ID);
                            if (foundRows.Length == 0)
                            {
                                Clients_SpecialCategories = new clsClients_SpecialCategories();
                                Clients_SpecialCategories.Client_ID = iClient_ID;
                                Clients_SpecialCategories.SpecCategory_ID = iSpecCategory_ID;
                                Clients_SpecialCategories.FileName = sNewFileName;
                                Clients_SpecialCategories.InsertRecord();
                            }
                            else
                            {
                                Clients_SpecialCategories = new clsClients_SpecialCategories();
                                Clients_SpecialCategories.Record_ID = 0;
                                Clients_SpecialCategories.GetRecord();
                                Clients_SpecialCategories.FileName = sNewFileName;
                                Clients_SpecialCategories.EditRecord();
                            }

                            Clients_SpecialCategories = new clsClients_SpecialCategories();
                            Clients_SpecialCategories.Client_ID = iClient_ID;
                            Clients_SpecialCategories.GetList();
                        }
                        else fgList[e.Row, 1] = false;
                    }
                    else
                    {
                        ClientDocFiles = new clsClientsDocFiles();
                        ClientDocFiles.Record_ID = Convert.ToInt32(fgList[e.Row, "ID"]);
                        ClientDocFiles.GetRecord();
                        ClientDocFiles.Status = 0;                                                 // 0 - document cancel
                        ClientDocFiles.EditStatus();

                        fgList[e.Row, "File_Name"] = "";
                        fgList[e.Row, "ID"] = "0";
                        fgList[e.Row, "FullFileName"] = "";
                        fgList[e.Row, "Edit"] = "1";
                    }
                }
                CreateDescription();
            }
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(fgList[fgList.Row, 1]))
            {
                if ((fgList[fgList.Row, "FullFileName"] + "").Trim() != "") Global.DMS_ShowFile("", fgList[fgList.Row, "FullFileName"] + "");
                else Global.DMS_ShowFile("Customers/" + sClientName, fgList[fgList.Row, "File_Name"] + "");
            }
        }
        private void CreateDescription()
        {
            string sTemp = "";
            if (chkDenAniko.Checked) sTemp = "{ 'cat' : '0','file_name' : ''}~";
            else
            {
                for (i = 1; i < fgList.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(fgList[i, 1]) && ((fgList[i, "File_Name"] + "") != ""))
                        sTemp = sTemp + "{'cat' : '" + fgList[i, 0] + "','file_name' : '" + fgList[i, "File_Name"] + "'}~";
                }
            }

            sDescription = sTemp;

            if (sTemp.Length > 0) lblStatus.Text = "1";
            else lblStatus.Text = "0";
        }

        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public string CategoriesList { get { return this.sCategoriesList; } set { this.sCategoriesList = value; } }
        public class SpecCategData
        {
            public string cat { get; set; }
            public string file_name { get; set; }
        }
    }
}
