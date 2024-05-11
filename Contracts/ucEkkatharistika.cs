using C1.Win.C1FlexGrid;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucEkkatharistika : UserControl
    {
        int i = 0, j = 0, iClient_ID = 0, iDocType1_ID = 0, iTaxDeclarations1Year = 0, iTaxDeclarationsLastYear = 0;
        string sTemp = "", sID = "", sDescription = "", sClientName = "", sFileName = "", sNewFileName = "", sFileFullPath = "";
        string[] bokens;
        bool bCheckList = false;
        //SortedList lstDocTypes = new SortedList();
        ListDictionary lstDocTypes = new ListDictionary();
        DataRow[] foundRows;
        EkkData Ekk_Data;
        clsOptions Options = new clsOptions();
        clsClientsDocFiles ClientDocFiles = new clsClientsDocFiles();
        public ucEkkatharistika()
        {
            InitializeComponent();
        }

        private void ucEkkatharistika_Load(object sender, EventArgs e)
        {

        }
        // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - OK (after checking), 4 - problem (after checking), -1 - Cancelled
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            Options = new clsOptions();
            Options.GetRecord();
            iTaxDeclarations1Year = Options.TaxDeclarations1Year;
            iTaxDeclarationsLastYear = Options.TaxDeclarationsLastYear;

            sClientName = sCN;
            iClient_ID = iKlient_ID;
            iDocType1_ID = iDefaultDocType1_ID;


            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.DrawMode = DrawModeEnum.OwnerDraw;
            fgDocs.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellButtonClick);

            Column col1 = fgDocs.Cols[1];
            col1.Name = "Image";
            col1.DataType = typeof(String);
            col1.ComboList = "...";

            //------- fgDocs2 ----------------------------
            fgDocs2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs2.Styles.ParseString(Global.GridStyle);
            fgDocs2.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs2_CellChanged);
            fgDocs2.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs2_BeforeEdit);
            fgDocs2.Cols[1].DataMap = lstDocTypes;

            bCheckList = false;

            //--- define request's parameters -------------------------------------------------

            ClientDocFiles = new clsClientsDocFiles();
            ClientDocFiles.Client_ID = iClient_ID;
            ClientDocFiles.PreContract_ID = 0;
            ClientDocFiles.Contract_ID = 0;
            ClientDocFiles.DocTypes = 0;
            ClientDocFiles.GetList();

            fgDocs.Redraw = false;
            fgDocs.Rows.Count = 1;

            for (i = iTaxDeclarationsLastYear; i >= iTaxDeclarations1Year; i--)
            {
                foundRows = ClientDocFiles.List.Select("FileName LIKE 'ΕΚΚΑΘΑΡΙΣΤΙΚΟ " + i + "%' AND Status = 2");
                if (foundRows.Length == 0)
                    fgDocs.AddItem(i + "\t\t0\t\t0");
            }

            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                //--- define value of DenExo ---------------------------------------------------
                Ekk_Data = JsonConvert.DeserializeObject<EkkData>(tokens[0]);
                chkDenExo.Checked = Ekk_Data.tax_declaration == 1 ? true : false;

                //--- define ΕΚΚΑΘΑΡΙΣΤΙΚΟ files data (file_name and status) ------------------------
                bokens = tokens[1].Replace("{", "").Replace("}", "").Split('^');
                for (i = 0; i <= bokens.Length - 1; i++)
                {
                    sFileName = bokens[i].Trim();
                    if (sFileName.Length > 0)
                    {
                        sTemp = sFileName.Substring(14, 4);
                        j = fgDocs.FindRow(sTemp, 1, 0, false);
                        if (j > 0)
                        {
                            sID = "0";
                            foreach (DataRow dtRow in ClientDocFiles.List.Rows)
                                if ((dtRow["FileName"] + "").Trim() == sFileName)
                                    sID = dtRow["ID"] + "";

                            fgDocs[j, 1] = sFileName;
                            fgDocs[j, 2] = sID;
                            fgDocs[j, 3] = "";
                            fgDocs[j, 4] = "1";
                        }
                    }

                }
                if (fgDocs.Rows.Count > 0) fgDocs.Row = 1;

                //--- define extra files data (file_name and file_type) ------------------------
                fgDocs2.Redraw = false;
                fgDocs2.Rows.Count = 1;

                bokens = tokens[2].Replace("{", "").Replace("}", "").Split('^');
                for (i = 0; i <= bokens.Length - 1; i++)
                {
                    if (bokens[i].Trim().Length > 0)
                    {
                        Ekk_Data = JsonConvert.DeserializeObject<EkkData>("{" + bokens[i] + "}");

                        sID = "0";
                        sTemp = "";                                                                                              // sTemp = DocTypes.Title  
                        foundRows = Global.dtDocTypes.Select("ID = " + Ekk_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";

                        foreach (DataRow dtRow in ClientDocFiles.List.Rows)
                            if ((dtRow["FileName"] + "").Trim() != "" && Convert.ToInt32(dtRow["Status"]) > 0)
                                if (Ekk_Data.file_name == dtRow["FileName"] + "")
                                    sID = dtRow["ID"] + "";

                        fgDocs2.AddItem(Ekk_Data.file_name + "\t" + sTemp + "\t" + sID + "\t" + Ekk_Data.file_type + "\t" + "" + "\t" + "1");
                    }
                }
                fgDocs2.Redraw = true;
            }
            fgDocs.Redraw = true;
            bCheckList = true;

            CreateDescription();
        }
        private void chkDenExo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDenExo.Checked)
            {
                for (i = 1; i < fgDocs.Rows.Count; i++) fgDocs[i, 1] = "";
                fgDocs.Enabled = false;
            }
            else fgDocs.Enabled = true;
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgDocs.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if ((fgDocs[fgDocs.Row, "ID"] + "").Trim() != "")
                    {
                        ClientDocFiles = new clsClientsDocFiles();
                        ClientDocFiles.Record_ID = Convert.ToInt32(fgDocs[fgDocs.Row, "ID"]);
                        ClientDocFiles.DeleteRecord();
                    }
                    fgDocs[fgDocs.Row, 1] = "";
                    fgDocs[fgDocs.Row, 2] = "";
                    fgDocs[fgDocs.Row, 3] = "";
                    fgDocs[fgDocs.Row, 4] = "";
                }
                CreateDescription();
            }
        }

        private void fgDocs_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 1)
            {
                sFileFullPath = Global.FileChoice(Global.DefaultFolder);
                if (sFileFullPath.Length > 0)
                {
                    sNewFileName = "ΕΚΚΑΘΑΡΙΣΤΙΚΟ " + fgDocs[fgDocs.Row, 0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sFileFullPath);

                    if (Path.GetDirectoryName(sFileFullPath) != Global.DMSTransferPoint)
                    {   // Source file isn't in DMS TransferPoint folder, so ...
                        File.Copy(sFileFullPath, Global.DMSTransferPoint + "/" + sNewFileName);                                    // ... copy this file into DMS TransferPoint folder
                    }
                    fgDocs[fgDocs.Row, 3] = sFileFullPath;
                    fgDocs[fgDocs.Row, 1] = sNewFileName;
                    fgDocs[fgDocs.Row, 4] = "1";

                    clsServerJobs ServerJobs = new clsServerJobs();
                    ServerJobs.JobType_ID = 19;
                    ServerJobs.Source_ID = 0;
                    ServerJobs.Parameters = "{'source_file_full_name': '" + sFileFullPath.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" +
                                            iDocType1_ID + "', " + "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                            iClient_ID + "', 'status' : '1'}";

                    ServerJobs.DateStart = DateTime.Now;
                    ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                    ServerJobs.PubKey = "";
                    ServerJobs.PrvKey = "";
                    ServerJobs.Attempt = 0;
                    ServerJobs.Status = 0;
                    ServerJobs.InsertRecord();

                    CreateDescription();
                }
            }
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            if ((fgDocs[fgDocs.Row, "FullFilePath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, "FullFilePath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs[fgDocs.Row, 1] + "");
        }
        private void fgDocs2_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col != 1) e.Cancel = true;
            else e.Cancel = false;
        }
        private void fgDocs2_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Row == fgDocs2.Row && e.Col == 1)
                    fgDocs2[fgDocs2.Row, 3] = fgDocs2[fgDocs2.Row, 1];
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                sNewFileName = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);
                fgDocs2.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "" + "\t" + "0");
                fgDocs2[fgDocs2.Rows.Count - 1, "File_Name"] = sNewFileName;
                fgDocs2[fgDocs2.Rows.Count - 1, "FullFilePath"] = sTemp;
                fgDocs2[fgDocs2.Rows.Count - 1, "Status"] = "0";                                                       // 0 - Status of new file that will be uploaded later

                if (Path.GetDirectoryName(sTemp) != Global.DMSTransferPoint)
                {   // Source file isn't in DMS TransferPoint folder, so ...
                    File.Copy(sTemp, Global.DMSTransferPoint + "/" + sNewFileName);                                    // ... copy this file into DMS TransferPoint folder
                }

                clsServerJobs ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 19;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'source_file_full_name': '" + sTemp.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" +
                                        fgDocs2[fgDocs2.Rows.Count - 1, "DocType_ID"] + "', " + "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                        iClient_ID + "', 'status' : '1'}";

                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                CreateDescription();
            }
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (fgDocs2.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    fgDocs2.RemoveItem(fgDocs2.Row);
                CreateDescription();
            }
        }

        private void tsbShow_Click(object sender, EventArgs e)
        {
            if ((fgDocs2[fgDocs2.Row, "FullFilePath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs2[fgDocs2.Row, "FullFilePath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs2[fgDocs2.Row, "File_Name"] + "");
        }

        private void CreateDescription()
        {
            sTemp = "{'tax_declaration' : '" + (chkDenExo.Checked ? "1" : "0") + "'}~";

            sTemp = sTemp + "{";
            if (fgDocs.Rows.Count > 1)
            {
                for (i = 1; i < fgDocs.Rows.Count; i++)
                    if (fgDocs[i, 1] + "" != "" && (fgDocs[i, 4] + "" == "1" || fgDocs[i, 4] + "" == "2")) sTemp = sTemp + fgDocs[i, 1] + "^";
            }
            sTemp = sTemp + "}~";

            if (fgDocs2.Rows.Count > 1)
            {
                for (i = 1; i < fgDocs2.Rows.Count; i++)
                    sTemp = sTemp + "{'file_name' : '" + fgDocs2[i, "File_Name"] + "','file_type' : " + fgDocs2[i, "DocType_ID"] + "}^";
            }

            sDescription = sTemp;
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }

        public class EkkData
        {
            public int tax_declaration { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }
            public string videochat { get; set; }
        }
    }
}
