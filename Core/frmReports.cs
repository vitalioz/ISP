using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Core
{
    public partial class frmReports : Form
    {
        int _iReportID;
        string _sParams;
        DataTable _dtShowResult;
        public frmReports()
        {
            InitializeComponent();
        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            switch (_iReportID)
            {
                case 1:
                    break;
                case 18:
                    Report18();                                         // Official Informing - RTO List
                    break;
                case 19:
                    Report19();                                         // Official Informing - Daily
                    break;
                case 20:
                    Report20();                                         // Official Informing List
                    break;
                case 21:
                    Report21();                                         // Managment Fees
                    break;
                case 22:
                    Report22();                                         // Admin Fees
                    break;
                case 25:
                    Report25();                                         // Official Informing - Perioadical Evaluation' 
                    break;
            }
        }
        private void Report18()
        {
            string[] tokens = _sParams.Split('~');
            ReportDocument repOfficialInforming_RTOList = new ReportDocument();
            repOfficialInforming_RTOList.Load(Application.StartupPath + @"\Reports\repOfficialInforming_RTOList.rpt");

            repOfficialInforming_RTOList.Database.Tables[0].SetDataSource(_dtShowResult);
            repOfficialInforming_RTOList.SetParameterValue(0, tokens[0]);
            repOfficialInforming_RTOList.SetParameterValue(1, tokens[1]);
            repOfficialInforming_RTOList.SetParameterValue(2, tokens[2]);
            repOfficialInforming_RTOList.SetParameterValue(3, tokens[3]);
            repOfficialInforming_RTOList.SetParameterValue(4, tokens[4]);
            crwReport.Visible = true;
            crwReport.ReportSource = repOfficialInforming_RTOList;
        }
        private void Report19()
        {
            ReportDocument repOfficialInforming = new ReportDocument();
            repOfficialInforming.Load(Application.StartupPath + @"\Reports\repOfficialInforming.rpt");
            repOfficialInforming.Database.Tables[0].SetDataSource(_dtShowResult);
            //repOfficialInforming.PrintToPrinter(1, true, 1, 999);
            crwReport.Visible = true;
            crwReport.ReportSource = repOfficialInforming;
        }
        private void Report20()
        {
            string[] tokens = _sParams.Split('~');
            ReportDocument rptOfficialInformingList = new ReportDocument();
            rptOfficialInformingList.Load(Application.StartupPath + @"\Reports\repOfficialInformingList.rpt");
            rptOfficialInformingList.Database.Tables[0].SetDataSource(_dtShowResult);
            rptOfficialInformingList.SetParameterValue(0, tokens[0]);
            rptOfficialInformingList.SetParameterValue(1, tokens[1]);
            rptOfficialInformingList.SetParameterValue(2, tokens[2]);
            rptOfficialInformingList.SetParameterValue(3, tokens[3]);
            rptOfficialInformingList.SetParameterValue(4, tokens[4]);
            crwReport.Visible = true;
            crwReport.ReportSource = rptOfficialInformingList;
        }
        private void Report21()
        {
            string[] tokens = _sParams.Split('~');
            ReportDocument repOfficialInformingManFees_List = new ReportDocument();
            repOfficialInformingManFees_List.Load(Application.StartupPath + @"\Reports\repOfficialInformingManFees_List.rpt");
            repOfficialInformingManFees_List.Database.Tables[0].SetDataSource(_dtShowResult);
            repOfficialInformingManFees_List.SetParameterValue(0, tokens[0]);
            repOfficialInformingManFees_List.SetParameterValue(1, tokens[1]);
            repOfficialInformingManFees_List.SetParameterValue(2, tokens[2]);
            repOfficialInformingManFees_List.SetParameterValue(3, tokens[3]);
            crwReport.Visible = true;
            crwReport.ReportSource = repOfficialInformingManFees_List;
        }
        private void Report22()
        {
            string[] tokens = _sParams.Split('~');
            ReportDocument repOfficialInformingAdminFees_List = new ReportDocument();
            repOfficialInformingAdminFees_List.Load(Application.StartupPath + @"\Reports\repOfficialInformingAdminFees_List.rpt");
            repOfficialInformingAdminFees_List.Database.Tables[0].SetDataSource(_dtShowResult);
            repOfficialInformingAdminFees_List.SetParameterValue(0, tokens[0]);         // Provider
            repOfficialInformingAdminFees_List.SetParameterValue(1, tokens[1]);         // Period
            repOfficialInformingAdminFees_List.SetParameterValue(2, tokens[2]);         // UserName
            repOfficialInformingAdminFees_List.SetParameterValue(3, tokens[3]);         // Company
            repOfficialInformingAdminFees_List.SetParameterValue(4, tokens[4]);         // Title
            crwReport.Visible = true;
            crwReport.ReportSource = repOfficialInformingAdminFees_List;
        }
        private void Report25()
        {
            string[] tokens = _sParams.Split('~');
            ReportDocument rptOfficialInforming_PeriodicalEvaluation = new ReportDocument();
            rptOfficialInforming_PeriodicalEvaluation.Load(Application.StartupPath + @"\Reports\repOfficialInforming_PeriodicalEvaluation.rpt");
            rptOfficialInforming_PeriodicalEvaluation.Database.Tables[0].SetDataSource(_dtShowResult);
            rptOfficialInforming_PeriodicalEvaluation.SetParameterValue(0, tokens[0]);         // Provider
            rptOfficialInforming_PeriodicalEvaluation.SetParameterValue(1, tokens[1]);         // Period
            rptOfficialInforming_PeriodicalEvaluation.SetParameterValue(2, tokens[2]);         // UserName
            rptOfficialInforming_PeriodicalEvaluation.SetParameterValue(3, tokens[3]);         // Company
            crwReport.Visible = true;
            crwReport.ReportSource = rptOfficialInforming_PeriodicalEvaluation;
        }
        public int ReportID { get { return this._iReportID; } set { this._iReportID = value; } }
        public string Params { get { return this._sParams; } set { this._sParams = value; } }
        public DataTable ShowResult { get { return this._dtShowResult; } set { this._dtShowResult = value; } }
    }
}
