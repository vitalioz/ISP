using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class ClientsDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public int AddRecord(Clients client)
        {
            int iID = 0;
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertClient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@SurnameEng", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@FirstnameEng", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@SurnameFather", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@SurnameMother", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@FirstnameMother", SqlDbType.NVarChar, 1000).Value = "";
                cmd.Parameters.Add("@SurnameSizigo", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@FirstnameSizigo", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@Division", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Is_InfluenceCenter", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Is_Introducer", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Is_RepresentPerson", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Brunch_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Spec_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@CompanyTitle", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = "1900/01/01";
                cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 50).Value = "";
                cmd.Parameters.Add("@Citizen_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = "";
                cmd.Parameters.Add("@FamilyStatus", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Guardian_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = -1;                                // -1 - ypopsifios
                cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 20).Value = "";
                cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 20).Value = "";
                cmd.Parameters.Add("@Police", SqlDbType.NVarChar, 50).Value = "";
                cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = "";
                cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@AFM2", SqlDbType.NVarChar, 20).Value = "";
                cmd.Parameters.Add("@DOY2", SqlDbType.NVarChar, 40).Value = "";
                cmd.Parameters.Add("@AMKA", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 20).Value = "";
                cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@SendSMS", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = "";
                cmd.Parameters.Add("@ConnectionMethod", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@LogSxedio_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Users_List", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = 0;
                cmd.Parameters.Add("@SpecialCategory", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Merida", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@LogAxion", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = "";
                cmd.Parameters.Add("@RM_ID", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@RM_Step", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@BO_Step", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Conne", SqlDbType.NVarChar, 100).Value = "";
                cmd.Parameters.Add("@SumAxion", SqlDbType.Float).Value = 0;
                cmd.Parameters.Add("@SumAkiniton", SqlDbType.Float).Value = 0;
                cmd.Parameters.Add("@Risk", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DependentPersons", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.ExecuteNonQuery();                

                iID = Convert.ToInt32(cmd.Parameters["@ID"].Value);
                con.Close();
            }
            return iID;
        }
    }
}
