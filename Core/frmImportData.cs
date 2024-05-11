using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace Core
{
    public partial class frmImportData : Form
    {
        DataColumn dtCol;
        DataRow dtRow;
        int i, j, iFileType, iShema, iReadMode, iAktion;
        string sTemp;
        char cCSV_Delimiter;
        bool bCheckList;
        DataTable dtResult;
        public frmImportData()
        {
            InitializeComponent();
        }

        private void frmImportData_Load(object sender, EventArgs e)
        {
            clsSystem System = new clsSystem();
            System.GetList_Schemas();
            cmbSchemas.DataSource = System.List.Copy();
            cmbSchemas.DisplayMember = "Title";
            cmbSchemas.ValueMember = "ID";

            btnImport.Enabled = true;
            bCheckList = true;

            cmbFileType.SelectedIndex = iFileType;
            if (iFileType < 3) {
                // --- EXCEL files
                grpSchema.Enabled = true;
                if (cmbSchemas.Items.Count == 0) cmbSchemas.SelectedIndex = 0;
                else                             cmbSchemas.SelectedValue = iShema;
            }
            else {
                // --- doc or txt files
                grpSchema.Enabled = false;
                btnImport.Enabled = true;
            }

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            switch (cmbFileType.SelectedIndex)
            {
            case 0:                                       // 0 - .xlsx file
                    switch (iReadMode)
                    {
                        case 1:
                            ReadXLS_1();                 // mode 1 - read cell by cell
                            break;
                        case 2:
                            ReadXLS_2();                 // mode 2 - read all file 
                            break;
                    }
                    break;
            case 1:                                      // 1 - .xls file
                    switch (iReadMode)
                    {
                        case 1:
                            ReadXLS_1();                 // mode 1 - read cell by cell
                            break;
                        case 2:
                            ReadXLS_2();                 // mode 2 - read all file 
                            break;
                    }
                    break;
            case 2:                                      // 2 - .csv file
                    ReadCSV(cCSV_Delimiter);
                    break;
            case 3:                                      // 3 - .docx file
                    break;
            case 4:                                      // 4 - .doc file
                    break;
            case 5:                                      // 5 - .txt file
                    break;
            }
        }
        private void ReadXLS_1()
        {

        }
        private void ReadXLS_2()
        {
            int iSourceCols, iTargetCols;
            string sFileName = txtFilePath.Text;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            if (iFileType < 3)
            {
                //--- define NUMERIC Source Columns  ---------------------
                if (Global.IsNumeric(txtSourceColumns.Text)) iSourceCols = Convert.ToInt32(txtSourceColumns.Text);
                else iSourceCols = 0;

                //--- define NUMERIC Target Columns  ---------------------
                if (Global.IsNumeric(txtTargetColumns.Text)) iTargetCols = Convert.ToInt32(txtTargetColumns.Text);
                else iTargetCols = 0;

                if (iSourceCols != 0 && iTargetCols != 0)
                {
                    Microsoft.Office.Interop.Excel.Application oXL;
                    Microsoft.Office.Interop.Excel._Workbook oWB;
                    Microsoft.Office.Interop.Excel._Worksheet oSheet;

                    oXL = new Microsoft.Office.Interop.Excel.Application();
                    oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(sFileName));
                    oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                    oSheet.Name = "Sheet1";
                    sFileName = Path.GetDirectoryName(sFileName) + "\\" + Path.GetFileNameWithoutExtension(sFileName) + "_" + 
                                DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sFileName);
                    oWB.SaveAs(sFileName);
                    oWB.Close();

                    var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; data source={0}; Extended Properties=Excel 8.0;", sFileName);
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    //fgList.Rows.Count = 0;
                    //fgList.Cols.Count = 0;

                    adapter.Fill(ds);
                    DataTable data = ds.Tables["anyNameHere"];
                    fgList.DataSource = ds.Tables[0];

                    fgList.Redraw = true;
                }

                this.Refresh();
                this.Cursor = Cursors.Default;

                panOK.Visible = true;
            }
        }
        private void ReadCSV(char sDelimiter)
        {
            string path = txtFilePath.Text;
            string sTemp = "", sLine2;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            fgList.Redraw = false;
            fgList.Rows.Count = 0;
            fgList.Cols.Count = 50;

            string[] csvLines = System.IO.File.ReadAllLines(path);
            foreach (string sLine in csvLines)
            {
                sTemp = "";
                sLine2 = sLine.Replace("\"", "").Replace("\t", "");
                string[] columns = sLine2.Split(sDelimiter);
                foreach (string column in columns)
                {
                    sTemp = sTemp + column + "\t";
                }
                fgList.AddItem(sTemp);
            }
            fgList.Redraw = true;

            this.Refresh();
            this.Cursor = Cursors.Default;

            panOK.Visible = true;
        }

        private void cmbSchemas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {

                txtSheetNumber.Text = "0";
                txtSourceColumns.Text = "0";
                txtTargetColumns.Text = "0";
                txtHeaderLines.Text = "0";
                txtFinishColumn.Text = "0";
                cCSV_Delimiter = char.Parse(",");
                iShema = Convert.ToInt32(cmbSchemas.SelectedValue);
                if (iShema == 0) {
                    fgList.Rows.Count = 0;
                    fgList.Cols.Count = 0;
                    btnImport.Enabled = false;
                }
                else {
                    clsImportData ImportData = new clsImportData();
                    ImportData.Record_ID = Convert.ToInt32(cmbSchemas.SelectedValue);
                    ImportData.GetRecord();
                    txtSheetNumber.Text = ImportData.SheetNumber.ToString();
                    txtSourceColumns.Text = ImportData.SourceColumnsCount.ToString();
                    txtTargetColumns.Text = ImportData.TargetColumnsCount.ToString();
                    txtHeaderLines.Text = ImportData.HeaderLines.ToString();
                    txtFinishColumn.Text = ImportData.TableFinish.ToString();
                    cCSV_Delimiter = ImportData.CSV_Delimiter;
                    btnImport.Enabled = true;
                }
            } 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dtResult = new DataTable("List");
            for (i = 0; i <= fgList.Cols.Count - 1; i++) {
                sTemp = "f" + (i + 1);
                dtCol = dtResult.Columns.Add(sTemp, System.Type.GetType("System.String"));
            }

            for (j = Convert.ToInt32(txtHeaderLines.Text); j <= fgList.Rows.Count - 1; j++) {
                if (fgList[j, Convert.ToInt32(txtFinishColumn.Text)] + "" != "") {
                    dtRow = dtResult.NewRow();
                    for (i = 0; i <= fgList.Cols.Count - 1; i++) dtRow["f" + (i + 1)] = fgList[j, i] + "";
                    dtResult.Rows.Add(dtRow);
                }
            }

            iAktion = 1;
            panOK.Visible = false;

            this.Close();
        }
        public int FileType { get { return this.iFileType; } set { this.iFileType = value; } }
        public int Shema { get { return this.iShema; } set { this.iShema = value; } }
        public int ReadMode { get { return this.iReadMode; } set { this.iReadMode = value; } }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public DataTable Result { get { return this.dtResult; } set { this.dtResult = value; } }
    }
}
