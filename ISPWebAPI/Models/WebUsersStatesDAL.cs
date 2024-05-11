using System;
using System.Data;
using System.Data.SqlClient;
using Core;

namespace ISPWebAPI.Models
{
    public class WebUsersStatesDAL
    {
        int _iRecord_ID = 0, _iWU_ID = 0, _iStatus = 0, _iResult = 0;
        SqlConnection conn;

        //--- Save the record into WebUsersStates table --------------------------   
        public Interface SaveRecord(string connectionString, Interface oInterface)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);
 
            clsWebUsersStates WebUsersStates = new clsWebUsersStates();
            WebUsersStates.WU_ID = oInterface.WU_ID;
            WebUsersStates.Status = oInterface.Status;
            WebUsersStates.GetList();
            if (WebUsersStates.List.Rows.Count == 0)
            {
                WebUsersStates.Status = oInterface.Status;
                WebUsersStates.Email = oInterface.EMail;
                WebUsersStates.Mobile = oInterface.Mobile_phone;
                _iRecord_ID = WebUsersStates.InsertRecord();
            }
            else
            {
                foreach (DataRow dtRow in WebUsersStates.List.Rows)
                {
                    WebUsersStates.Record_ID = Convert.ToInt16((dtRow["ID"]));
                    WebUsersStates.Status = oInterface.Status;
                    WebUsersStates.Email = oInterface.EMail;
                    WebUsersStates.Mobile = oInterface.Mobile_phone;
                    _iRecord_ID = WebUsersStates.EditRecord();
                }
            }

            if (_iRecord_ID > 0) oInterface.Result = 1;
            else oInterface.Result = 0;

            return oInterface;
        }

        //--- GET WebUsersStates list with e-mail & password ----------------------------------
        public WebUsersStates GetData(string connectionString, int iWU_ID)
        {
            WebUsersStates objWebUsersStates = new WebUsersStates();
            Global.connStr = connectionString;

            clsWebUsersStates oWebUsersStates = new clsWebUsersStates();
            oWebUsersStates.WU_ID = iWU_ID;
            oWebUsersStates.GetList();
            foreach (DataRow dtRow in oWebUsersStates.List.Rows)
            {           
                objWebUsersStates.ID = Convert.ToInt16(dtRow["ID"]);
                objWebUsersStates.WU_ID = Convert.ToInt16(dtRow["WU_ID"]);
                objWebUsersStates.Status = Convert.ToInt16(dtRow["Status"]);
                objWebUsersStates.Email = dtRow["Email"] + "";
                objWebUsersStates.Mobile = dtRow["Mobile"] + "";
            }

            return objWebUsersStates;
        }

        //--- GET WebUsersStates list with e-mail & password ----------------------------------
        public int GetStatus(string connectionString, int iWU_ID)
        {
            _iStatus = 0;
            Global.connStr = connectionString;

            clsWebUsersStates WebUsersStates = new clsWebUsersStates();
            WebUsersStates.WU_ID = iWU_ID;
            WebUsersStates.GetList();
            foreach (DataRow dtRow in WebUsersStates.List.Rows)
                _iStatus = Convert.ToInt16(dtRow["Status"]);

            return _iStatus;
        }
        //--- ADD the record into WebUsersStates table --------------------------   
        public int InsertRecord(string connectionString, int iWU_ID, int iStatus)
        {
            _iResult = 0;

            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsersStates WebUsersStates = new clsWebUsersStates();
            WebUsersStates.WU_ID = iWU_ID;
            WebUsersStates.Status = iStatus;
            _iWU_ID = WebUsersStates.InsertRecord();

            return _iResult;
        }
        //--- UPDATE Password of the record into WebUsersStates table ---------------------------------------
        public void UpdateRecord(string connectionString, int iWU_ID, int iStatus)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsersStates WebUsersStates = new clsWebUsersStates();
            WebUsersStates.Record_ID = iWU_ID;
            WebUsersStates.GetRecord();
            WebUsersStates.Status = iStatus;
            WebUsersStates.EditRecord();
        }

        //--- DELETE the record from WebUsersStates   table ---------------------------------
        public void DeleteRecord(string connectionString, int iWU_ID)
        {
            Global.connStr = connectionString;
            clsWebUsersStates WebUsersStates = new clsWebUsersStates();
            WebUsersStates.Record_ID = iWU_ID;
            WebUsersStates.DeleteRecord();
        }
    }
}
