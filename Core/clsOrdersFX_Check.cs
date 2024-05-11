﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersFX_Check
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iCommandFX_ID;
        private DateTime _dDateIns;
        private int _iUser_ID;
        private int _iStatus;
        private int _iProblemType_ID;
        private string _sNotes;
        private string _sFileName;
        private string _sReversalRequestDate;
        private DataTable _dtList;
        public clsOrdersFX_Check()
        {
            this._iRecord_ID = 0;
            this._iCommandFX_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iUser_ID = 0;
            this._iStatus = 0;
            this._iProblemType_ID = 0;
            this._sNotes = "";
            this._sFileName = "";
            this._sReversalRequestDate = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "CommandsFX_Check"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCommandFX_ID = Convert.ToInt32(drList["CommandFX_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iStatus = Convert.ToInt32(drList["User_ID"]);
                    this._iProblemType_ID = Convert.ToInt32(drList["ProblemType_ID"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sFileName = drList["FileName"] + "";
                    this._sReversalRequestDate = drList["ReversalRequestDate"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("CommandFX_ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("ProblemType_ID", typeof(int));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("ReversalRequestDate", typeof(string));
            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));
            _dtList.Columns.Add("ProblemType_Title", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_Check", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iCommandFX_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["CommandFX_ID"], drList["DateIns"], drList["User_ID"], drList["Status"], drList["ProblemType_ID"],
                                     drList["Notes"], drList["FileName"], drList["ReversalRequestDate"], drList["Surname"], drList["Firstname"], drList["ProblemType_Title"]);
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
                using (SqlCommand cmd = new SqlCommand("sp_InsertCommandsFX_Check", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CommandFX_ID", SqlDbType.Int).Value = _iCommandFX_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@ProblemType_ID", SqlDbType.Int).Value = _iProblemType_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@ReversalRequestDate", SqlDbType.NVarChar, 20).Value = _sReversalRequestDate;

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
                using (SqlCommand cmd = new SqlCommand("sp_EditCommandsFX_Check", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CommandFX_ID", SqlDbType.Int).Value = _iCommandFX_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@ProblemType_ID", SqlDbType.Int).Value = _iProblemType_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@ReversalRequestDate", SqlDbType.NVarChar, 20).Value = _sReversalRequestDate;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CommandsFX_Check";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int CommandFX_ID { get { return _iCommandFX_ID; } set { _iCommandFX_ID = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public int ProblemType_ID { get { return _iProblemType_ID; } set { _iProblemType_ID = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public string ReversalRequestDate { get { return _sReversalRequestDate; } set { _sReversalRequestDate = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
