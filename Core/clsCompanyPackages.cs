using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCompanyPackages
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iBusinessType_ID;
        private string _sTitle;
        private int _iMIFID;
        private int _iPackageProvider_ID;
        private int _iPackageType_ID;
        private int _iPackageVersion;
        private int _iClientTipos_ID;
        private DateTime _dStart;
        private DateTime _dFinish;
        private string _sNotes;
        private int _iBrokerageServiceProvider_ID;
        private int _iBrokerageOption_ID;
        private int _iRTOServiceProvider_ID;
        private int _iRTOOption_ID;
        private int _iAdvisoryServiceProvider_ID;
        private int _iAdvisoryOption_ID;
        private int _iAdvisoryInvestmentProfile_ID;
        private int _iAdvisoryInvestmentPolicy_ID;
        private int _iCustodyServiceProvider_ID;
        private int _iCustodyOption_ID;
        private int _iAdministrationServiceProvider_ID;
        private int _iAdministrationOption_ID;
        private int _iDealAdvisoryServiceProvider_ID;
        private int _iDealAdvisoryOption_ID;
        private int _iDealAdvisoryInvestmentPolicy_ID;
        private int _iDiscretServiceProvider_ID;
        private int _iDiscretOption_ID;
        private int _iDiscretInvestmentProfile_ID;
        private int _iDiscretInvestmentPolicy_ID;
        private int _iLombardServiceProvider_ID;
        private int _iLombardOption_ID;
        private int _iFXServiceProvider_ID;
        private int _iFXOption_ID;
        private int _iSettlementsServiceProvider_ID;
        private int _iSettlementsOption_ID;
        private string _sTipos;

        private int _iProvider_ID;
        private int _iCheckActuality;
        private DateTime _dActualDate;
        private DataTable _dtList;

        public clsCompanyPackages()
        {
            this._iRecord_ID = 0;
            this._iBusinessType_ID = 0;
            this._sTitle = "";
            this._iMIFID = 0;
            this._iPackageProvider_ID = 0;
            this._iPackageType_ID = 0;
            this._iPackageVersion = 0;
            this._iClientTipos_ID = 0;
            this._dStart = Convert.ToDateTime("1900/01/01");
            this._dFinish = Convert.ToDateTime("1900/01/01");
            this._sNotes = "";
            this._iBrokerageServiceProvider_ID = 0;
            this._iBrokerageOption_ID = 0;
            this._iRTOServiceProvider_ID = 0;
            this._iRTOOption_ID = 0;
            this._iAdvisoryServiceProvider_ID = 0;
            this._iAdvisoryOption_ID = 0;
            this._iAdvisoryInvestmentProfile_ID = 0;
            this._iAdvisoryInvestmentPolicy_ID = 0;
            this._iCustodyServiceProvider_ID = 0;
            this._iCustodyOption_ID = 0;
            this._iAdministrationServiceProvider_ID = 0;
            this._iAdministrationOption_ID = 0;
            this._iDealAdvisoryServiceProvider_ID = 0;
            this._iDealAdvisoryOption_ID = 0;
            this._iDealAdvisoryInvestmentPolicy_ID = 0;
            this._iDiscretServiceProvider_ID = 0;
            this._iDiscretOption_ID = 0;
            this._iDiscretInvestmentPolicy_ID = 0;
            this._iDiscretInvestmentProfile_ID = 0;
            this._iLombardServiceProvider_ID = 0;
            this._iLombardOption_ID = 0;
            this._iFXServiceProvider_ID = 0;
            this._iFXOption_ID = 0;
            this._iSettlementsServiceProvider_ID = 0;
            this._iSettlementsOption_ID = 0;
            this._sTipos = "";

            this._iProvider_ID = 0;
            this._iCheckActuality = 0;
            this._dActualDate = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "CompanyFeesPackages"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]); 
                    this._sTitle = drList["Title"] + "";
                    this._iMIFID = Convert.ToInt32(drList["MIFID"]);
                    this._iPackageProvider_ID = Convert.ToInt32(drList["PackageProvider_ID"]);
                    this._iPackageType_ID = Convert.ToInt32(drList["PackageType_ID"]);
                    this._iPackageVersion = Convert.ToInt32(drList["PackageVersion"]);
                    this._iClientTipos_ID = Convert.ToInt32(drList["ClientTipos_ID"]);
                    this._dStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._sNotes = drList["Notes"] + "";
                    this._iBrokerageServiceProvider_ID = Convert.ToInt32(drList["BrokerageServiceProvider_ID"]);
                    this._iBrokerageOption_ID = Convert.ToInt32(drList["BrokerageOption_ID"]);
                    this._iRTOServiceProvider_ID = Convert.ToInt32(drList["RTOServiceProvider_ID"]);
                    this._iRTOOption_ID = Convert.ToInt32(drList["RTOOption_ID"]);
                    this._iAdvisoryServiceProvider_ID = Convert.ToInt32(drList["AdvisoryServiceProvider_ID"]);
                    this._iAdvisoryOption_ID = Convert.ToInt32(drList["AdvisoryOption_ID"]);
                    this._iAdvisoryInvestmentProfile_ID = Convert.ToInt32(drList["AdvisoryInvestmentProfile_ID"]);
                    this._iAdvisoryInvestmentPolicy_ID = Convert.ToInt32(drList["AdvisoryInvestmentPolicy_ID"]);
                    this._iCustodyServiceProvider_ID = Convert.ToInt32(drList["CustodyServiceProvider_ID"]);
                    this._iCustodyOption_ID = Convert.ToInt32(drList["CustodyOption_ID"]);
                    this._iAdministrationServiceProvider_ID = Convert.ToInt32(drList["AdministrationServiceProvider_ID"]);
                    this._iAdministrationOption_ID = Convert.ToInt32(drList["AdministrationOption_ID"]);
                    this._iDealAdvisoryServiceProvider_ID = Convert.ToInt32(drList["DealAdvisoryServiceProvider_ID"]);
                    this._iDealAdvisoryOption_ID = Convert.ToInt32(drList["DealAdvisoryOption_ID"]);
                    this._iDealAdvisoryInvestmentPolicy_ID = Convert.ToInt32(drList["DealAdvisoryInvestmentPolicy_ID"]);
                    this._iDiscretServiceProvider_ID = Convert.ToInt32(drList["DiscretServiceProvider_ID"]);
                    this._iDiscretOption_ID = Convert.ToInt32(drList["DiscretOption_ID"]);
                    this._iDiscretInvestmentProfile_ID = Convert.ToInt32(drList["DiscretInvestmentProfile_ID"]);
                    this._iDiscretInvestmentPolicy_ID = Convert.ToInt32(drList["DiscretInvestmentPolicy_ID"]);
                    this._iLombardServiceProvider_ID = Convert.ToInt32(drList["LombardServiceProvider_ID"]);
                    this._iLombardOption_ID = Convert.ToInt32(drList["LombardOption_ID"]);
                    this._iFXServiceProvider_ID = Convert.ToInt32(drList["FXServiceProvider_ID"]);
                    this._iFXOption_ID = Convert.ToInt32(drList["FXOption_ID"]);
                    this._iSettlementsServiceProvider_ID = Convert.ToInt32(drList["SettlementsServiceProvider_ID"]);
                    this._iSettlementsOption_ID = Convert.ToInt32(drList["SettlementsOption_ID"]);
                    this._sTipos = drList["Tipos"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("CompanyPackagesList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("PackageType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("PackageVersion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TitleFull", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MIFID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateStart", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateFinish", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BrokerageOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("RTOOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AdvisoryOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AdvisoryProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentProfile_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Advisory_MonthMinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Advisory_MonthMinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CustodyOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("CustodyProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Custody_MonthMinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Custody_MonthMinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdminOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AdminProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Admin_MonthMinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Admin_MonthMinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DealAdvisoryOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DealAdvisoryProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DiscretOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DiscretProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentProfile_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Discret_MonthMinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discret_MonthMinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LombardOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("LombardProvider_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FXOption_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FXProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SettlementsOption_ID", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetCompanyFeesPackages", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Provider_ID", _iProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@PackageType_ID", _iPackageType_ID));
                cmd.Parameters.Add(new SqlParameter("@BusinessType_ID", _iBusinessType_ID));
                cmd.Parameters.Add(new SqlParameter("@CheckActuality", _iCheckActuality));
                cmd.Parameters.Add(new SqlParameter("@ActualDate", _dActualDate.ToString("yyyy/MM/dd")));
                cmd.Parameters.Add(new SqlParameter("@Title", "%" + _sTitle + "%"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["PackageType_ID"] = drList["PackageType_ID"];
                    this.dtRow["PackageVersion"] = drList["PackageVersion"];
                    this.dtRow["Title"] = drList["Title"];
                    this.dtRow["TitleFull"] = drList["Title"] + "   ver. " + drList["PackageVersion"];
                    this.dtRow["MIFID"] = drList["MIFID"];
                    this.dtRow["DateStart"] = drList["DateStart"];
                    this.dtRow["DateFinish"] = drList["DateFinish"];
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["BrokerageOption_ID"] = drList["BrokerageOption_ID"];
                    this.dtRow["RTOOption_ID"] = drList["RTOOption_ID"];
                    this.dtRow["AdvisoryOption_ID"] = drList["AdvisoryOption_ID"];
                    this.dtRow["AdvisoryProvider_ID"] = drList["AdvisoryServiceProvider_ID"];
                    this.dtRow["AdvisoryInvestmentProfile_ID"] = drList["AdvisoryInvestmentProfile_ID"];
                    this.dtRow["AdvisoryInvestmentPolicy_ID"] = drList["AdvisoryInvestmentPolicy_ID"];
                    this.dtRow["Advisory_MonthMinAmount"] = drList["Advisory_MonthMinAmount"];
                    this.dtRow["Advisory_MonthMinCurr"] = drList["Advisory_MonthMinCurr"] + "";
                    this.dtRow["CustodyOption_ID"] = drList["CustodyOption_ID"];
                    this.dtRow["CustodyProvider_ID"] = drList["CustodyServiceProvider_ID"];
                    this.dtRow["Custody_MonthMinAmount"] = drList["Custody_MonthMinAmount"];
                    this.dtRow["Custody_MonthMinCurr"] = drList["Custody_MonthMinCurr"] + "";                   
                    this.dtRow["AdminOption_ID"] = drList["AdministrationOption_ID"];
                    this.dtRow["AdminProvider_ID"] = drList["AdministrationServiceProvider_ID"];
                    this.dtRow["Admin_MonthMinAmount"] = drList["Admin_MonthMinAmount"];
                    this.dtRow["Admin_MonthMinCurr"] = drList["Admin_MonthMinCurr"] + "";
                    this.dtRow["DealAdvisoryOption_ID"] = drList["DealAdvisoryOption_ID"];
                    this.dtRow["DealAdvisoryProvider_ID"] = drList["DealAdvisoryServiceProvider_ID"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_ID"] = drList["DealAdvisoryInvestmentPolicy_ID"];
                    this.dtRow["DiscretOption_ID"] = drList["DiscretOption_ID"];
                    this.dtRow["DiscretProvider_ID"] = drList["DiscretServiceProvider_ID"];
                    this.dtRow["DiscretInvestmentProfile_ID"] = drList["DiscretInvestmentProfile_ID"];
                    this.dtRow["DiscretInvestmentPolicy_ID"] = drList["DiscretInvestmentPolicy_ID"];
                    this.dtRow["Discret_MonthMinAmount"] = drList["Discret_MonthMinAmount"];
                    this.dtRow["Discret_MonthMinCurr"] = drList["Discret_MonthMinCurr"] + "";
                    this.dtRow["LombardOption_ID"] = drList["LombardOption_ID"];
                    this.dtRow["LombardProvider_ID"] = drList["LombardServiceProvider_ID"];
                    this.dtRow["FXOption_ID"] = drList["FXOption_ID"];
                    this.dtRow["FXProvider_ID"] = drList["FXServiceProvider_ID"];
                    this.dtRow["SettlementsOption_ID"] = drList["SettlementsOption_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        } 
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertCompanyFeesPackage", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;    
                    cmd.Parameters.Add("@BusinessType_ID", SqlDbType.Int).Value = _iBusinessType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@MIFID", SqlDbType.Int).Value = _iMIFID;
                    cmd.Parameters.Add("@PackageProvider_ID", SqlDbType.Int).Value = _iPackageProvider_ID;
                    cmd.Parameters.Add("@PackageType_ID", SqlDbType.Int).Value = _iPackageType_ID;
                    cmd.Parameters.Add("@PackageVersion", SqlDbType.Int).Value = _iPackageVersion;
                    cmd.Parameters.Add("@ClientTipos_ID", SqlDbType.Int).Value = _iClientTipos_ID;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = _dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = _dFinish;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@BrokerageServiceProvider_ID", SqlDbType.Int).Value = _iBrokerageServiceProvider_ID;
                    cmd.Parameters.Add("@BrokerageOption_ID", SqlDbType.Int).Value = _iBrokerageOption_ID;
                    cmd.Parameters.Add("@RTOServiceProvider_ID", SqlDbType.Int).Value = _iRTOServiceProvider_ID;
                    cmd.Parameters.Add("@RTOOption_ID", SqlDbType.Int).Value = _iRTOOption_ID;
                    cmd.Parameters.Add("@AdvisoryServiceProvider_ID", SqlDbType.Int).Value = _iAdvisoryServiceProvider_ID;
                    cmd.Parameters.Add("@AdvisoryOption_ID", SqlDbType.Int).Value = _iAdvisoryOption_ID;
                    cmd.Parameters.Add("@AdvisoryInvestmentProfile_ID", SqlDbType.Int).Value = _iAdvisoryInvestmentProfile_ID;
                    cmd.Parameters.Add("@AdvisoryInvestmentPolicy_ID", SqlDbType.Int).Value = _iAdvisoryInvestmentPolicy_ID;
                    cmd.Parameters.Add("@CustodyServiceProvider_ID", SqlDbType.Int).Value = _iCustodyServiceProvider_ID;
                    cmd.Parameters.Add("@CustodyOption_ID", SqlDbType.Int).Value = _iCustodyOption_ID;
                    cmd.Parameters.Add("@AdministrationServiceProvider_ID", SqlDbType.Int).Value = _iAdministrationServiceProvider_ID;
                    cmd.Parameters.Add("@AdministrationOption_ID", SqlDbType.Int).Value = _iAdministrationOption_ID;
                    cmd.Parameters.Add("@DealAdvisoryServiceProvider_ID", SqlDbType.Int).Value = _iDealAdvisoryServiceProvider_ID;
                    cmd.Parameters.Add("@DealAdvisoryOption_ID", SqlDbType.Int).Value = _iDealAdvisoryOption_ID;
                    cmd.Parameters.Add("@DealAdvisoryInvestmentPolicy_ID", SqlDbType.Int).Value = _iDealAdvisoryInvestmentPolicy_ID;
                    cmd.Parameters.Add("@DiscretServiceProvider_ID", SqlDbType.Int).Value = _iDiscretServiceProvider_ID;
                    cmd.Parameters.Add("@DiscretOption_ID", SqlDbType.Int).Value = _iDiscretOption_ID;
                    cmd.Parameters.Add("@DiscretInvestmentProfile_ID", SqlDbType.Int).Value = _iDiscretInvestmentProfile_ID;
                    cmd.Parameters.Add("@DiscretInvestmentPolicy_ID", SqlDbType.Int).Value = _iDiscretInvestmentPolicy_ID;
                    cmd.Parameters.Add("@LombardServiceProvider_ID", SqlDbType.Int).Value = _iLombardServiceProvider_ID;
                    cmd.Parameters.Add("@LombardOption_ID", SqlDbType.Int).Value = _iLombardOption_ID;
                    cmd.Parameters.Add("@FXServiceProvider_ID", SqlDbType.Int).Value = _iFXServiceProvider_ID;
                    cmd.Parameters.Add("@FXOption_ID", SqlDbType.Int).Value = _iFXOption_ID;
                    cmd.Parameters.Add("@SettlementsServiceProvider_ID", SqlDbType.Int).Value = _iSettlementsServiceProvider_ID;
                    cmd.Parameters.Add("@SettlementsOption_ID", SqlDbType.Int).Value = _iSettlementsOption_ID;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditCompanyFeesPackage", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@BusinessType_ID", SqlDbType.Int).Value = _iBusinessType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@MIFID", SqlDbType.Int).Value = _iMIFID;
                    cmd.Parameters.Add("@PackageProvider_ID", SqlDbType.Int).Value = _iPackageProvider_ID;
                    cmd.Parameters.Add("@PackageType_ID", SqlDbType.Int).Value = _iPackageType_ID;
                    cmd.Parameters.Add("@PackageVersion", SqlDbType.Int).Value = _iPackageVersion;
                    cmd.Parameters.Add("@ClientTipos_ID", SqlDbType.Int).Value = _iClientTipos_ID;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = _dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = _dFinish;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@BrokerageServiceProvider_ID", SqlDbType.Int).Value = _iBrokerageServiceProvider_ID;
                    cmd.Parameters.Add("@BrokerageOption_ID", SqlDbType.Int).Value = _iBrokerageOption_ID;
                    cmd.Parameters.Add("@RTOServiceProvider_ID", SqlDbType.Int).Value = _iRTOServiceProvider_ID;
                    cmd.Parameters.Add("@RTOOption_ID", SqlDbType.Int).Value = _iRTOOption_ID;
                    cmd.Parameters.Add("@AdvisoryServiceProvider_ID", SqlDbType.Int).Value = _iAdvisoryServiceProvider_ID;
                    cmd.Parameters.Add("@AdvisoryOption_ID", SqlDbType.Int).Value = _iAdvisoryOption_ID;
                    cmd.Parameters.Add("@AdvisoryInvestmentProfile_ID", SqlDbType.Int).Value = _iAdvisoryInvestmentProfile_ID;
                    cmd.Parameters.Add("@AdvisoryInvestmentPolicy_ID", SqlDbType.Int).Value = _iAdvisoryInvestmentPolicy_ID;
                    cmd.Parameters.Add("@CustodyServiceProvider_ID", SqlDbType.Int).Value = _iCustodyServiceProvider_ID;
                    cmd.Parameters.Add("@CustodyOption_ID", SqlDbType.Int).Value = _iCustodyOption_ID;
                    cmd.Parameters.Add("@AdministrationServiceProvider_ID", SqlDbType.Int).Value = _iAdministrationServiceProvider_ID;
                    cmd.Parameters.Add("@AdministrationOption_ID", SqlDbType.Int).Value = _iAdministrationOption_ID;
                    cmd.Parameters.Add("@DealAdvisoryServiceProvider_ID", SqlDbType.Int).Value = _iDealAdvisoryServiceProvider_ID;
                    cmd.Parameters.Add("@DealAdvisoryOption_ID", SqlDbType.Int).Value = _iDealAdvisoryOption_ID;
                    cmd.Parameters.Add("@DealAdvisoryInvestmentPolicy_ID", SqlDbType.Int).Value = _iDealAdvisoryInvestmentPolicy_ID;
                    cmd.Parameters.Add("@DiscretServiceProvider_ID", SqlDbType.Int).Value = _iDiscretServiceProvider_ID;
                    cmd.Parameters.Add("@DiscretOption_ID", SqlDbType.Int).Value = _iDiscretOption_ID;
                    cmd.Parameters.Add("@DiscretInvestmentProfile_ID", SqlDbType.Int).Value = _iDiscretInvestmentProfile_ID;
                    cmd.Parameters.Add("@DiscretInvestmentPolicy_ID", SqlDbType.Int).Value = _iDiscretInvestmentPolicy_ID;
                    cmd.Parameters.Add("@LombardServiceProvider_ID", SqlDbType.Int).Value = _iLombardServiceProvider_ID;
                    cmd.Parameters.Add("@LombardOption_ID", SqlDbType.Int).Value = _iLombardOption_ID;
                    cmd.Parameters.Add("@FXServiceProvider_ID", SqlDbType.Int).Value = _iFXServiceProvider_ID;
                    cmd.Parameters.Add("@FXOption_ID", SqlDbType.Int).Value = _iFXOption_ID;
                    cmd.Parameters.Add("@SettlementsServiceProvider_ID", SqlDbType.Int).Value = _iSettlementsServiceProvider_ID;
                    cmd.Parameters.Add("@SettlementsOption_ID", SqlDbType.Int).Value = _iSettlementsOption_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CompanyFeesPackages";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int BusinessType_ID { get { return this._iBusinessType_ID; } set { this._iBusinessType_ID = value; } }
        public string Title { get { return this._sTitle; } set { this._sTitle = value; } }
        public int MIFID { get { return this._iMIFID; } set { this._iMIFID = value; } }
        public int PackageProvider_ID { get { return this._iPackageProvider_ID; } set { this._iPackageProvider_ID = value; } }
        public int PackageType_ID { get { return this._iPackageType_ID; } set { this._iPackageType_ID = value; } }
        public int PackageVersion { get { return this._iPackageVersion; } set { this._iPackageVersion = value; } }
        public int ClientTipos_ID { get { return this._iClientTipos_ID; } set { this._iClientTipos_ID = value; } }
        public DateTime DateStart { get { return this._dStart; } set { this._dStart = value; } }
        public DateTime DateFinish { get { return this._dFinish; } set { this._dFinish = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public int BrokerageServiceProvider_ID { get { return this._iBrokerageServiceProvider_ID; } set { this._iBrokerageServiceProvider_ID = value; } }
        public int BrokerageOption_ID { get { return this._iBrokerageOption_ID; } set { this._iBrokerageOption_ID = value; } }
        public int RTOServiceProvider_ID { get { return this._iRTOServiceProvider_ID; } set { this._iRTOServiceProvider_ID = value; } }
        public int RTOOption_ID { get { return this._iRTOOption_ID; } set { this._iRTOOption_ID = value; } }
        public int AdvisoryServiceProvider_ID { get { return this._iAdvisoryServiceProvider_ID; } set { this._iAdvisoryServiceProvider_ID = value; } }
        public int AdvisoryOption_ID { get { return this._iAdvisoryOption_ID; } set { this._iAdvisoryOption_ID = value; } }
        public int AdvisoryInvestmentProfile_ID { get { return this._iAdvisoryInvestmentProfile_ID; } set { this._iAdvisoryInvestmentProfile_ID = value; } }
        public int AdvisoryInvestmentPolicy_ID { get { return this._iAdvisoryInvestmentPolicy_ID; } set { this._iAdvisoryInvestmentPolicy_ID = value; } }
        public int CustodyServiceProvider_ID { get { return this._iCustodyServiceProvider_ID; } set { this._iCustodyServiceProvider_ID = value; } }
        public int CustodyOption_ID { get { return this._iCustodyOption_ID; } set { this._iCustodyOption_ID = value; } }
        public int AdministrationServiceProvider_ID { get { return this._iAdministrationServiceProvider_ID; } set { this._iAdministrationServiceProvider_ID = value; } }
        public int AdministrationOption_ID { get { return this._iAdministrationOption_ID; } set { this._iAdministrationOption_ID = value; } }
        public int DealAdvisoryServiceProvider_ID { get { return this._iDealAdvisoryServiceProvider_ID; } set { this._iDealAdvisoryServiceProvider_ID = value; } }
        public int DealAdvisoryOption_ID { get { return this._iDealAdvisoryOption_ID; } set { this._iDealAdvisoryOption_ID = value; } }
        public int DealAdvisoryInvestmentPolicy_ID { get { return this._iDealAdvisoryInvestmentPolicy_ID; } set { this._iDealAdvisoryInvestmentPolicy_ID = value; } }
        public int DiscretServiceProvider_ID { get { return this._iDiscretServiceProvider_ID; } set { this._iDiscretServiceProvider_ID = value; } }
        public int DiscretOption_ID { get { return this._iDiscretOption_ID; } set { this._iDiscretOption_ID = value; } }
        public int DiscretInvestmentProfile_ID { get { return this._iDiscretInvestmentProfile_ID; } set { this._iDiscretInvestmentProfile_ID = value; } }
        public int DiscretInvestmentPolicy_ID { get { return this._iDiscretInvestmentPolicy_ID; } set { this._iDiscretInvestmentPolicy_ID = value; } }
        public int LombardServiceProvider_ID { get { return this._iLombardServiceProvider_ID; } set { this._iLombardServiceProvider_ID = value; } }
        public int LombardOption_ID { get { return this._iLombardOption_ID; } set { this._iLombardOption_ID = value; } }
        public int FXServiceProvider_ID { get { return this._iFXServiceProvider_ID; } set { this._iFXServiceProvider_ID = value; } }
        public int FXOption_ID { get { return this._iFXOption_ID; } set { this._iFXOption_ID = value; } }
        public int SettlementsServiceProvider_ID { get { return this._iSettlementsServiceProvider_ID; } set { this._iSettlementsServiceProvider_ID = value; } }
        public int SettlementsOption_ID { get { return this._iSettlementsOption_ID; } set { this._iSettlementsOption_ID = value; } }
        public string Tipos { get { return this._sTipos; } set { this._sTipos = value; } }
        public int Provider_ID { get { return this._iProvider_ID; } set { this._iProvider_ID = value; } }
        public int CheckActuality { get { return this._iCheckActuality; } set { this._iCheckActuality = value; } }
        public DateTime ActualDate { get { return this._dActualDate; } set { this._dActualDate = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






