using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestmentCommetties_AssetAllocationRecs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iIC_AA_ID;
        private string _sTitle;
        private float _fltMainValue;
        private float _fltMinValue;
        private float _fltMaxValue;

        private DataTable _dtList;

        public clsInvestmentCommetties_AssetAllocationRecs()
        {
            this._iRecord_ID = 0;
            this._iIC_AA_ID = 0;
            this._sTitle = "";
            this._fltMainValue = 0;
            this._fltMinValue = 0;
            this._fltMaxValue = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvestmentCommetties_AssetAllocation_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iIC_AA_ID = Convert.ToInt32(drList["IC_AA_ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._fltMainValue = Convert.ToSingle(drList["MainValue"]);
                    this._fltMinValue = Convert.ToSingle(drList["MinValue"]);
                    this._fltMaxValue = Convert.ToSingle(drList["MaxValue"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }  
        public void GetList()
        {
            int i = 0;
            _dtList = new DataTable();
            _dtList.Columns.Add("AA", typeof(int));
            _dtList.Columns.Add("IC_AA_ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(int));
            _dtList.Columns.Add("MainValue", typeof(float));
            _dtList.Columns.Add("MinValue", typeof(float));
            _dtList.Columns.Add("MaxValue", typeof(float));
            _dtList.Columns.Add("ID", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvestmentCommetties_AssetAllocation_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "IC_AA_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iIC_AA_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["IC_AA_ID"] = drList["IC_AA_ID"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["MainValue"] = drList["MainValue"];
                    dtRow["MinValue"] = drList["MinValue"];
                    dtRow["MaxValue"] = drList["MaxValue"];
                    dtRow["ID"] = drList["ID"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertInvestmentCommetties_AssetAllocation_Recs", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IC_AA_ID", SqlDbType.Int).Value = _iIC_AA_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@MainValue", SqlDbType.Float).Value = _fltMainValue;
                    cmd.Parameters.Add("@MinValue", SqlDbType.Float).Value = _fltMinValue;
                    cmd.Parameters.Add("@MaxValue", SqlDbType.Float).Value = _fltMaxValue;
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
                using (SqlCommand cmd = new SqlCommand("EditInvestmentCommetties_AssetAllocation_Recs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@IC_AA_ID", SqlDbType.Int).Value = _iIC_AA_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@MainValue", SqlDbType.Float).Value = _fltMainValue;
                    cmd.Parameters.Add("@MinValue", SqlDbType.Float).Value = _fltMinValue;
                    cmd.Parameters.Add("@MaxValue", SqlDbType.Float).Value = _fltMaxValue;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvestmentCommetties_AssetAllocation_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int IC_AA_ID { get { return _iIC_AA_ID; } set { _iIC_AA_ID = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public float MainValue { get { return _fltMainValue; } set { _fltMainValue = value; } }
        public float MinValue { get { return _fltMinValue; } set { _fltMinValue = value; } }
        public float MaxValue { get { return _fltMaxValue; } set { _fltMaxValue = value; } }      
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
