﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsServiceProviderDealAdvisoryFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iSPO_ID;
        private int    _iServiceProvider_ID;
        private int    _iInvestmentPolicy_ID;
        private float  _fltAmountFrom;
        private float  _fltAmountTo;
        private float  _fltFeesAmount;
        private string _sFeesCurr;
        private float  _fltYperReturn;
        private string _sVariable1;
        private float  _fltVariable2;

        private DataTable _dtList;

        public clsServiceProviderDealAdvisoryFees()
        {
            this._iRecord_ID = 0;
            this._iSPO_ID = 0;
            this._iServiceProvider_ID = 0;
            this._iInvestmentPolicy_ID = 0;
            this._fltAmountFrom = 0;
            this._fltAmountTo = 0;
            this._fltFeesAmount = 0;
            this._sFeesCurr = "";
            this._fltYperReturn = 0;
            this._sVariable1 = "";
            this._fltVariable2 = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProviderDealAdvisoryFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSPO_ID = Convert.ToInt32(drList["SPO_ID"]);
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._iInvestmentPolicy_ID = Convert.ToInt32(drList["InvestmentPolicy_ID"]);
                    this._fltAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._fltAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._fltFeesAmount = Convert.ToSingle(drList["FeesAmount"]);
                    this._sFeesCurr = drList["FeesCurr"] + "";
                    this._fltYperReturn = Convert.ToSingle(drList["YperReturn"]);
                    this._sVariable1 = drList["Variable1"] + "";
                    this._fltVariable2 = Convert.ToSingle(drList["Variable2"]);
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

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetFees()
        {
            try
            {
                _dtList = new DataTable("ServiceProviderDealAdvisoryFees_List");
                dtCol = _dtList.Columns.Add("InvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("YperReturn", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Variable1", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Variable2", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPO_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetServiceProviderDealAdvisoryFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", this._iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@SPO_ID", this._iSPO_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["InvestmentPolicy_Title"] = drList["InvestmentPolicy_Title"];
                    dtRow["InvestmentPolicy_ID"] = drList["InvestmentPolicy_ID"];
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["FeesAmount"] = drList["FeesAmount"];
                    dtRow["FeesCurr"] = drList["FeesCurr"];
                    dtRow["YperReturn"] = drList["YperReturn"];
                    dtRow["Variable1"] = drList["Variable1"];
                    dtRow["Variable2"] = drList["Variable2"];
                    dtRow["ID"] = drList["ID"];
                    dtRow["SPO_ID"] = drList["SPO_ID"];
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
                using (cmd = new SqlCommand("InsertServiceProviderDealAdvisoryFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@InvestmentPolicy_ID", SqlDbType.Int).Value = _iInvestmentPolicy_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Float).Value = _fltFeesAmount;
                    cmd.Parameters.Add("@FeesCurr", SqlDbType.NVarChar, 6).Value = _sFeesCurr;
                    cmd.Parameters.Add("@YperReturn", SqlDbType.Float).Value = _fltYperReturn;
                    cmd.Parameters.Add("@Variable1", SqlDbType.NVarChar, 100).Value = _sVariable1;
                    cmd.Parameters.Add("@Variable2", SqlDbType.Float).Value = _fltVariable2;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("EditServiceProviderDealAdvisoryFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@InvestmentPolicy_ID", SqlDbType.Int).Value = _iInvestmentPolicy_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Float).Value = _fltFeesAmount;
                    cmd.Parameters.Add("@FeesCurr", SqlDbType.NVarChar, 6).Value = _sFeesCurr;
                    cmd.Parameters.Add("@YperReturn", SqlDbType.Float).Value = _fltYperReturn;
                    cmd.Parameters.Add("@Variable1", SqlDbType.NVarChar, 100).Value = _sVariable1;
                    cmd.Parameters.Add("@Variable2", SqlDbType.Float).Value = _fltVariable2;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServiceProviderDealAdvisoryFees";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int SPO_ID { get { return this._iSPO_ID; } set { this._iSPO_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int InvestmentPolicy_ID { get { return this._iInvestmentPolicy_ID; } set { this._iInvestmentPolicy_ID = value; } }
        public float AmountFrom { get { return this._fltAmountFrom; } set { this._fltAmountFrom = value; } }
        public float AmountTo { get { return this._fltAmountTo; } set { this._fltAmountTo = value; } }
        public float FeesAmount { get { return this._fltFeesAmount; } set { this._fltFeesAmount = value; } }
        public string FeesCurr { get { return this._sFeesCurr; } set { this._sFeesCurr = value; } }
        public float YperReturn { get { return this._fltYperReturn; } set { this._fltYperReturn = value; } }
        public string Variable1 { get { return this._sVariable1; } set { this._sVariable1 = value; } }
        public float Variable2 { get { return this._fltVariable2; } set { this._fltVariable2 = value; } }

        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}