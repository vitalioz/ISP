using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsTrxCharges
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private string _sFeeDescription_Gr;
        private string _sFeeDescription_En;
        private int    _iTrxType_ID;
        private int    _iWhoPays_ID;
        private int    _iReturnedTo_ID;
        private string _sHowCalculate;
        private int    _iFeesTypes_ID;
        private int    _iFeesSubTypes_ID;
        private int    _iConnectedFee1_ID;
        private int    _iOperation_ID;
        private int    _iDerivesFrom2_ID;
        private int    _iExecProviderUse;
        private int    _iCustodianUse;
        private int    _iDepositoryUse;
        private int    _iAttributedSettlement;
        private int    _iAllowNegative;
        private string _sComments;
        private int    _iShowonReceipt;
        private int    _iShowonStatement;
        private int    _iDaysMonth;
        private int    _iDaysYear;
        private int    _iYearCalendarDays;
        private int    _iCalculationBasis_ID;
        private int    _iUseValuewithoutinterest;
        private int    _iUseNegativeAssets;
        private int    _iExPostCategory_ID;
        private int    _iExPostSubcategory_ID;
        private string _sVATrate; 

        private DataTable _dtList;

        public clsTrxCharges()
        {
            this._iRecord_ID = 0;
            this._sFeeDescription_Gr = "";
            this._sFeeDescription_En = "";
            this._iTrxType_ID = 0;
            this._iWhoPays_ID = 0;
            this._iReturnedTo_ID = 0;
            this._sHowCalculate = "";
            this._iFeesTypes_ID = 0;
            this._iFeesSubTypes_ID = 0;
            this._iConnectedFee1_ID = 0;
            this._iOperation_ID = 0;
            this._iDerivesFrom2_ID = 0;
            this._iExecProviderUse = 0;
            this._iCustodianUse = 0;
            this._iDepositoryUse = 0;
            this._iAttributedSettlement = 0;
            this._iAllowNegative = 0;
            this._sComments = "";
            this._iShowonReceipt = 0;
            this._iShowonStatement = 0;
            this._iDaysMonth = 0;
            this._iDaysYear = 0;
            this._iYearCalendarDays = 0;
            this._iCalculationBasis_ID = 0;
            this._iUseValuewithoutinterest = 0;
            this._iUseNegativeAssets = 0;
            this._iExPostCategory_ID = 0;
            this._iExPostSubcategory_ID = 0;
            this._sVATrate = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTrxCharges_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);  
                    this._sFeeDescription_Gr = drList["FeeDescription_Gr"] + "";
                    this._sFeeDescription_En = drList["FeeDescription_En"] + "";
                    this._iTrxType_ID = Convert.ToInt32(drList["TrxType_ID"]);
                    this._iWhoPays_ID = Convert.ToInt32(drList["WhoPays_ID"]);
                    this._iReturnedTo_ID = Convert.ToInt32(drList["ReturnedTo_ID"]);
                    this._sHowCalculate = drList["HowCalculate"] + "";
                    this._iFeesTypes_ID = Convert.ToInt32(drList["FeesTypes_ID"]);
                    this._iFeesSubTypes_ID = Convert.ToInt16(drList["FeesSubTypes_ID"]);
                    this._iConnectedFee1_ID = Convert.ToInt32(drList["ConnectedFee1_ID"]);
                    this._iOperation_ID = Convert.ToInt32(drList["Operation_ID"]);
                    this._iDerivesFrom2_ID = Convert.ToInt32(drList["DerivesFrom2_ID"]);
                    this._iExecProviderUse = Convert.ToInt32(drList["ExecProviderUse"]);
                    this._iCustodianUse = Convert.ToInt32(drList["CustodianUse"]);
                    this._iDepositoryUse = Convert.ToInt32(drList["DepositoryUse"]);
                    this._iAttributedSettlement = Convert.ToInt32(drList["AttributedSettlement"]);
                    this._iAllowNegative = Convert.ToInt32(drList["AllowNegative"]);
                    this._sComments = drList["Comments"] + "";
                    this._iShowonReceipt = Convert.ToInt32(drList["ShowonReceipt"]);
                    this._iShowonStatement = Convert.ToInt32(drList["ShowonStatement"]);
                    this._iDaysMonth = Convert.ToInt32(drList["DaysMonth"]);
                    this._iDaysYear = Convert.ToInt32(drList["DaysYear"]);
                    this._iYearCalendarDays = Convert.ToInt32(drList["YearCalendarDays"]);
                    this._iCalculationBasis_ID = Convert.ToInt32(drList["CalculationBasis_ID"]);
                    this._iUseValuewithoutinterest = Convert.ToInt32(drList["UseValuewithoutinterest"]);
                    this._iUseNegativeAssets = Convert.ToInt32(drList["UseNegativeAssets"]);
                    this._iExPostCategory_ID = Convert.ToInt32(drList["ExPostCategory_ID"]);
                    this._iExPostSubcategory_ID = Convert.ToInt32(drList["ExPostSubcategory_ID"]);
                    this._sVATrate = drList["VATrate"] + "";
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
                _dtList = new DataTable("TrxCharges_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FeeDescription_Gr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeeDescription_En", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("WhoPays_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ReturnedTo_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxActions_Title", System.Type.GetType("System.String"));                
                dtCol = _dtList.Columns.Add("HowCalculate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesTypes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FeesTypes_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesSubTypes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FeesSubTypes_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConnectedFee1_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectedFee1_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Operation_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DerivesFrom2_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DerivesFrom2_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecProviderUse", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CustodianUse", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DepositoryUse", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AttributedSettlement", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AllowNegative", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Comments", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShowonReceipt", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShowonStatement", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DaysMonth", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DaysYear", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("YearCalendarDays", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CalculationBasis_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("UseValuewithoutinterest", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("UseNegativeAssets", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExPostCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExPostSubcategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("VATrate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("GridColsView", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetTrxCharges_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["FeeDescription_Gr"] = drList["FeeDescription_Gr"] + "";
                    dtRow["FeeDescription_En"] = drList["FeeDescription_En"] + "";
                    dtRow["TrxType_ID"] = drList["TrxType_ID"];
                    dtRow["TrxType_Title"] = drList["TrxType_Title"] + "";
                    dtRow["WhoPays_ID"] = drList["WhoPays_ID"];
                    dtRow["ReturnedTo_ID"] = drList["ReturnedTo_ID"];
                    dtRow["TrxActions_Title"] = drList["TrxActions_Title"] + "";
                    dtRow["HowCalculate"] = drList["HowCalculate"] + "";
                    dtRow["FeesTypes_ID"] = drList["FeesTypes_ID"];
                    dtRow["FeesTypes_Title"] = drList["FeesType_Title"];
                    dtRow["FeesSubTypes_ID"] = drList["FeesSubTypes_ID"];
                    dtRow["FeesSubTypes_Title"] = drList["SubFeesTypes_Title"];
                    dtRow["ConnectedFee1_ID"] = drList["ConnectedFee1_ID"];
                    dtRow["ConnectedFee1_Title"] = drList["ConnectedFee1_Title"];
                    dtRow["Operation_ID"] = drList["Operation_ID"];
                    dtRow["DerivesFrom2_ID"] = drList["DerivesFrom2_ID"];
                    dtRow["DerivesFrom2_Title"] = drList["DerivesFrom2_Title"];
                    dtRow["ExecProviderUse"] = drList["ExecProviderUse"];
                    dtRow["CustodianUse"] = drList["CustodianUse"];
                    dtRow["DepositoryUse"] = drList["DepositoryUse"];
                    dtRow["AttributedSettlement"] = drList["AttributedSettlement"];
                    dtRow["AllowNegative"] = drList["AllowNegative"];
                    dtRow["Comments"] = drList["Comments"] + "";
                    dtRow["ShowonReceipt"] = drList["ShowonReceipt"];
                    dtRow["ShowonStatement"] = drList["ShowonStatement"];
                    dtRow["DaysMonth"] = drList["DaysMonth"];
                    dtRow["DaysYear"] = drList["DaysYear"];
                    dtRow["YearCalendarDays"] = drList["YearCalendarDays"];
                    dtRow["CalculationBasis_ID"] = drList["CalculationBasis_ID"];
                    dtRow["UseValuewithoutinterest"] = drList["UseValuewithoutinterest"];
                    dtRow["UseNegativeAssets"] = drList["UseNegativeAssets"];
                    dtRow["ExPostCategory_ID"] = drList["ExPostCategory_ID"];
                    dtRow["ExPostSubcategory_ID"] = drList["ExPostSubcategory_ID"];
                    dtRow["VATrate"] = drList["VATrate"] + "";
                    dtRow["GridColsView"] = drList["GridColsView"] + "";
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
                using (cmd = new SqlCommand("InsertTrx_Charges", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FeeDescription_Gr", SqlDbType.NVarChar, 100).Value = _sFeeDescription_Gr;
                    cmd.Parameters.Add("@FeeDescription_En", SqlDbType.NVarChar, 100).Value = _sFeeDescription_En;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@WhoPays_ID", SqlDbType.Int).Value = _iWhoPays_ID;
                    cmd.Parameters.Add("@ReturnedTo_ID", SqlDbType.Int).Value = _iReturnedTo_ID;
                    cmd.Parameters.Add("@HowCalculate", SqlDbType.NVarChar, 1000).Value = _sHowCalculate;
                    cmd.Parameters.Add("@FeesTypes_ID", SqlDbType.Int).Value = _iFeesTypes_ID;
                    cmd.Parameters.Add("@FeesSubTypes_ID", SqlDbType.Int).Value = _iFeesSubTypes_ID;
                    cmd.Parameters.Add("@ConnectedFee1_ID", SqlDbType.Int).Value = _iConnectedFee1_ID;
                    cmd.Parameters.Add("@Operation_ID", SqlDbType.Int).Value = _iOperation_ID;
                    cmd.Parameters.Add("@DerivesFrom2_ID", SqlDbType.Int).Value = _iDerivesFrom2_ID;
                    cmd.Parameters.Add("@ExecProviderUse", SqlDbType.Int).Value = _iExecProviderUse;
                    cmd.Parameters.Add("@CustodianUse", SqlDbType.Int).Value = _iCustodianUse;
                    cmd.Parameters.Add("@DepositoryUse", SqlDbType.Int).Value = _iDepositoryUse;
                    cmd.Parameters.Add("@AttributedSettlement", SqlDbType.Int).Value = _iAttributedSettlement;
                    cmd.Parameters.Add("@AllowNegative", SqlDbType.Int).Value = _iAllowNegative;
                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar, 100).Value = _sComments;
                    cmd.Parameters.Add("@ShowonReceipt", SqlDbType.Int).Value = _iShowonReceipt;
                    cmd.Parameters.Add("@ShowonStatement", SqlDbType.Int).Value = _iShowonStatement;
                    cmd.Parameters.Add("@DaysMonth", SqlDbType.Int).Value = _iDaysMonth;
                    cmd.Parameters.Add("@DaysYear", SqlDbType.Int).Value = _iDaysYear;
                    cmd.Parameters.Add("@YearCalendarDays", SqlDbType.Int).Value = _iYearCalendarDays;
                    cmd.Parameters.Add("@CalculationBasis_ID", SqlDbType.Int).Value = _iCalculationBasis_ID;
                    cmd.Parameters.Add("@UseValuewithoutinterest", SqlDbType.Int).Value = _iUseValuewithoutinterest;
                    cmd.Parameters.Add("@UseNegativeAssets", SqlDbType.Int).Value = _iUseNegativeAssets;
                    cmd.Parameters.Add("@ExPostCategory_ID", SqlDbType.Int).Value = _iExPostCategory_ID;
                    cmd.Parameters.Add("@ExPostSubcategory_ID", SqlDbType.Int).Value = _iExPostSubcategory_ID;
                    cmd.Parameters.Add("@VATrate", SqlDbType.NVarChar, 30).Value = _sVATrate;                    

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
                using (cmd = new SqlCommand("EditTrx_Charges", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@FeeDescription_Gr", SqlDbType.NVarChar, 100).Value = _sFeeDescription_Gr;
                    cmd.Parameters.Add("@FeeDescription_En", SqlDbType.NVarChar, 100).Value = _sFeeDescription_En;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@WhoPays_ID", SqlDbType.Int).Value = _iWhoPays_ID;
                    cmd.Parameters.Add("@ReturnedTo_ID", SqlDbType.Int).Value = _iReturnedTo_ID;
                    cmd.Parameters.Add("@HowCalculate", SqlDbType.NVarChar, 1000).Value = _sHowCalculate;
                    cmd.Parameters.Add("@FeesTypes_ID", SqlDbType.Int).Value = _iFeesTypes_ID;
                    cmd.Parameters.Add("@FeesSubTypes_ID", SqlDbType.Int).Value = _iFeesSubTypes_ID;
                    cmd.Parameters.Add("@ConnectedFee1_ID", SqlDbType.Int).Value = _iConnectedFee1_ID;
                    cmd.Parameters.Add("@Operation_ID", SqlDbType.Int).Value = _iOperation_ID;
                    cmd.Parameters.Add("@DerivesFrom2_ID", SqlDbType.Int).Value = _iDerivesFrom2_ID;
                    cmd.Parameters.Add("@ExecProviderUse", SqlDbType.Int).Value = _iExecProviderUse;
                    cmd.Parameters.Add("@CustodianUse", SqlDbType.Int).Value = _iCustodianUse;
                    cmd.Parameters.Add("@DepositoryUse", SqlDbType.Int).Value = _iDepositoryUse;
                    cmd.Parameters.Add("@AttributedSettlement", SqlDbType.Int).Value = _iAttributedSettlement;
                    cmd.Parameters.Add("@AllowNegative", SqlDbType.Int).Value = _iAllowNegative;
                    cmd.Parameters.Add("@Comments", SqlDbType.NVarChar, 100).Value = _sComments;
                    cmd.Parameters.Add("@ShowonReceipt", SqlDbType.Int).Value = _iShowonReceipt;
                    cmd.Parameters.Add("@ShowonStatement", SqlDbType.Int).Value = _iShowonStatement;
                    cmd.Parameters.Add("@DaysMonth", SqlDbType.Int).Value = _iDaysMonth;
                    cmd.Parameters.Add("@DaysYear", SqlDbType.Int).Value = _iDaysYear;
                    cmd.Parameters.Add("@YearCalendarDays", SqlDbType.Int).Value = _iYearCalendarDays;
                    cmd.Parameters.Add("@CalculationBasis_ID", SqlDbType.Int).Value = _iCalculationBasis_ID;
                    cmd.Parameters.Add("@UseValuewithoutinterest", SqlDbType.Int).Value = _iUseValuewithoutinterest;
                    cmd.Parameters.Add("@UseNegativeAssets", SqlDbType.Int).Value = _iUseNegativeAssets;
                    cmd.Parameters.Add("@ExPostCategory_ID", SqlDbType.Int).Value = _iExPostCategory_ID;
                    cmd.Parameters.Add("@ExPostSubcategory_ID", SqlDbType.Int).Value = _iExPostSubcategory_ID;
                    cmd.Parameters.Add("@VATrate", SqlDbType.NVarChar, 30).Value = _sVATrate;

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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Trx_Charges";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string FeeDescription_Gr { get { return this._sFeeDescription_Gr; } set { this._sFeeDescription_Gr = value; } }
        public string FeeDescription_En { get { return this._sFeeDescription_En; } set { this._sFeeDescription_En = value; } }        
        public int TrxType_ID { get { return this._iTrxType_ID; } set { this._iTrxType_ID = value; } }
        public int WhoPays_ID { get { return this._iWhoPays_ID; } set { this._iWhoPays_ID = value; } }
        public int ReturnedTo_ID { get { return this._iReturnedTo_ID; } set { this._iReturnedTo_ID = value; } }
        public string HowCalculate { get { return this._sHowCalculate; } set { this._sHowCalculate = value; } }        
        public int FeesTypes_ID { get { return this._iFeesTypes_ID; } set { this._iFeesTypes_ID = value; } }
        public int FeesSubTypes_ID { get { return this._iFeesSubTypes_ID; } set { this._iFeesSubTypes_ID = value; } }
        public int ConnectedFee1_ID { get { return this._iConnectedFee1_ID; } set { this._iConnectedFee1_ID = value; } }
        public int Operation_ID { get { return this._iOperation_ID; } set { this._iOperation_ID = value; } }
        public int DerivesFrom2_ID { get { return this._iDerivesFrom2_ID; } set { this._iDerivesFrom2_ID = value; } }
        public int ExecProviderUse { get { return this._iExecProviderUse; } set { this._iExecProviderUse = value; } }
        public int CustodianUse { get { return this._iCustodianUse; } set { this._iCustodianUse = value; } }
        public int DepositoryUse { get { return this._iDepositoryUse; } set { this._iDepositoryUse = value; } }
        public int AttributedSettlement { get { return this._iAttributedSettlement; } set { this._iAttributedSettlement = value; } }
        public int AllowNegative { get { return this._iAllowNegative; } set { this._iAllowNegative = value; } }
        public string Comments { get { return this._sComments; } set { this._sComments = value; } }
        public int ShowonReceipt { get { return this._iShowonReceipt; } set { this._iShowonReceipt = value; } }
        public int ShowonStatement { get { return this._iShowonStatement; } set { this._iShowonStatement = value; } }
        public int DaysMonth { get { return this._iDaysMonth; } set { this._iDaysMonth = value; } }
        public int DaysYear { get { return this._iDaysYear; } set { this._iDaysYear = value; } }
        public int YearCalendarDays { get { return this._iYearCalendarDays; } set { this._iYearCalendarDays = value; } }
        public int CalculationBasis_ID { get { return this._iCalculationBasis_ID; } set { this._iCalculationBasis_ID = value; } }
        public int UseValuewithoutinterest { get { return this._iUseValuewithoutinterest; } set { this._iUseValuewithoutinterest = value; } }
        public int UseNegativeAssets { get { return this._iUseNegativeAssets; } set { this._iUseNegativeAssets = value; } }
        public int ExPostCategory_ID { get { return this._iExPostCategory_ID; } set { this._iExPostCategory_ID = value; } }
        public int ExPostSubcategory_ID { get { return this._iExPostSubcategory_ID; } set { this._iExPostSubcategory_ID = value; } }
        public string VATrate { get { return this._sVATrate; } set { this._sVATrate = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}

