using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class WebUsersDevicesDAL
    {
        SqlCommand cmd;
        string _sConnectionString = Global.ConnectionString;

        public WebUsersDevices GetRecord(WebUsersDevices oWebUsersDevices)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetWebUsersDevices", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OS", oWebUsersDevices.OS);
                cmd.Parameters.AddWithValue("@Video", oWebUsersDevices.Video);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    oWebUsersDevices.ID = Convert.ToInt32(rdr["ID"]);
                    oWebUsersDevices.Status = Convert.ToInt32(rdr["Status"]);
                }
                con.Close();
            }
            return oWebUsersDevices;
        }
        public void AddWebUsersDevices(WebUsersDevices WebUsersDevices)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();
                cmd = new SqlCommand("InsertWebUsersDevices", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@WU_ID", WebUsersDevices.WU_ID);
                cmd.Parameters.AddWithValue("@Manufacturer", WebUsersDevices.Manufacturer + "");
                cmd.Parameters.AddWithValue("@Brand", WebUsersDevices.Brand + "");
                cmd.Parameters.AddWithValue("@Model", WebUsersDevices.Model + "");
                cmd.Parameters.AddWithValue("@Board", WebUsersDevices.Board + "");
                cmd.Parameters.AddWithValue("@Hardware", WebUsersDevices.Hardware + "");
                cmd.Parameters.AddWithValue("@Android", WebUsersDevices.Android + "");
                cmd.Parameters.AddWithValue("@ScreenResolution", WebUsersDevices.ScreenResolution + "");
                cmd.Parameters.AddWithValue("@ScreenDensity", WebUsersDevices.ScreenDensity + "");
                cmd.Parameters.AddWithValue("@Host", WebUsersDevices.Host + "");
                cmd.Parameters.AddWithValue("@Version", WebUsersDevices.Version + "");
                cmd.Parameters.AddWithValue("@API_level", WebUsersDevices.API_level + "");
                cmd.Parameters.AddWithValue("@Build_ID", WebUsersDevices.Build_ID + "");
                cmd.Parameters.AddWithValue("@Build_Time", WebUsersDevices.Build_Time + "");
                cmd.Parameters.AddWithValue("@Fingerprint", WebUsersDevices.Fingerprint + "");
                cmd.Parameters.AddWithValue("@PhoneType", WebUsersDevices.PhoneType + "");
                cmd.Parameters.AddWithValue("@NetworkCountryISO", WebUsersDevices.NetworkCountryISO + "");
                cmd.Parameters.AddWithValue("@NetworkOperatorName", WebUsersDevices.NetworkOperatorName + "");
                cmd.Parameters.AddWithValue("@DeviceId", WebUsersDevices.DeviceId + "");
                cmd.Parameters.AddWithValue("@DeviceSoftwareVersion", WebUsersDevices.DeviceSoftwareVersion + "");
                cmd.Parameters.AddWithValue("@SimCountryIso", WebUsersDevices.SimCountryIso + "");
                cmd.Parameters.AddWithValue("@SimOperatorName", WebUsersDevices.SimOperatorName + "");
                cmd.Parameters.AddWithValue("@SimSerialNumber", WebUsersDevices.SimSerialNumber + "");
                cmd.Parameters.AddWithValue("@Imei", WebUsersDevices.Imei + "");
                cmd.Parameters.AddWithValue("@Meid", WebUsersDevices.Meid + "");
                cmd.Parameters.AddWithValue("@MmsUAProfUrl", WebUsersDevices.MmsUAProfUrl + "");
                cmd.Parameters.AddWithValue("@MmsUserAgent", WebUsersDevices.MmsUserAgent + "");
                cmd.Parameters.AddWithValue("@SubscriberId", WebUsersDevices.SubscriberId + "");
                cmd.Parameters.AddWithValue("@TypeAllocationCode", WebUsersDevices.TypeAllocationCode + "");
                cmd.Parameters.AddWithValue("@OS", WebUsersDevices.OS + "");
                cmd.Parameters.AddWithValue("@Video", WebUsersDevices.Video + "");
                cmd.Parameters.AddWithValue("@Status", 1);

                cmd.ExecuteNonQuery();
                WebUsersDevices.ID = Convert.ToInt32(outParam.Value);
                WebUsersDevices.Status = 1;
               con.Close();
            }
        }
    }
}
