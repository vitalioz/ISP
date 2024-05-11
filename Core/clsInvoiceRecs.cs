using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvoiceRecs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iIT_ID;
        private int _iGood_Type;
        private string _sGood_Code;
        private string _sGood_Title;
        private string _sGood_MM;
        private float _sgPrice;
        private float _sgPosotita;
        private float _sgAxiaMikti;
        private float _sgEkptosiPercent;
        private float _sgEkptosiAxia;
        private float _sgAxiaKathari;
        private float _sgFPAPercent;
        private float _sgFPAAxia;
        private float _sgAxiaTeliki;

        private DataTable _dtList;

        public clsInvoiceRecs()
        {
            this._iRecord_ID = 0;
            this._iIT_ID = 0;
            this._iGood_Type = 0;
            this._sGood_Code = "";
            this._sGood_Title = "";
            this._sGood_MM = "";
            this._sgPrice = 0;
            this._sgPosotita = 0;
            this._sgAxiaMikti = 0;
            this._sgEkptosiPercent = 0;
            this._sgEkptosiAxia = 0;
            this._sgAxiaKathari = 0;
            this._sgFPAPercent = 0;
            this._sgFPAAxia = 0;
            this._sgAxiaTeliki = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Invoice_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iIT_ID = Convert.ToInt32(drList["IT_ID"]);
                    this._iGood_Type = Convert.ToInt32(drList["Good_Type"]);
                    this._sGood_Code = drList["Good_Code"] + "";
                    this._sGood_Title = drList["Good_Title"] + "";
                    this._sGood_MM = drList["Good_MM"] + "";
                    this._sgPrice = Convert.ToSingle(drList["Price"]);
                    this._sgPosotita = Convert.ToSingle(drList["Posotita"]);
                    this._sgAxiaMikti = Convert.ToSingle(drList["AxiaMikti"]);
                    this._sgEkptosiPercent = Convert.ToSingle(drList["EkptosiPercent"]);
                    this._sgEkptosiAxia = Convert.ToSingle(drList["EkptosiAxia"]);
                    this._sgAxiaKathari = Convert.ToSingle(drList["AxiaKathari"]);
                    this._sgFPAPercent = Convert.ToSingle(drList["FPAPercent"]);
                    this._sgFPAAxia = Convert.ToSingle(drList["FPAAxia"]);
                    this._sgAxiaTeliki = Convert.ToSingle(drList["AxiaTeliki"]);       
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
                _dtList = new DataTable("InvoiceRecs_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Good_Code", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Arithmos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Good_Title", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Posotita", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetInvoiceRecs_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IT_ID", _iIT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Good_Code"] = drList["Good_Code"];
                    dtRow["Arithmos"] = drList["Arithmos"];
                    dtRow["Good_Title"] = drList["Good_Title"];
                    dtRow["Posotita"] = drList["Posotita"];
                    dtRow["FileName"] = drList["FileName"];
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
                using (SqlCommand cmd = new SqlCommand("InsertInvoice_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IT_ID", SqlDbType.Int).Value = _iIT_ID;
                    cmd.Parameters.Add("@Good_Type", SqlDbType.Int).Value = _iGood_Type;
                    cmd.Parameters.Add("@Good_Code", SqlDbType.NVarChar, 50).Value = _sGood_Code;
                    cmd.Parameters.Add("@Good_Title", SqlDbType.NVarChar, 100).Value = _sGood_Title;
                    cmd.Parameters.Add("@Good_MM", SqlDbType.NVarChar, 20).Value = _sGood_MM;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = _sgPrice;
                    cmd.Parameters.Add("@Posotita", SqlDbType.Float).Value = _sgPosotita;
                    cmd.Parameters.Add("@AxiaMikti", SqlDbType.Float).Value = _sgAxiaMikti;
                    cmd.Parameters.Add("@EkptosiPercent", SqlDbType.Float).Value = _sgEkptosiPercent;
                    cmd.Parameters.Add("@EkptosiAxia", SqlDbType.Float).Value = _sgEkptosiAxia;
                    cmd.Parameters.Add("@AxiaKathari", SqlDbType.Float).Value = _sgAxiaKathari;
                    cmd.Parameters.Add("@FPAPercent", SqlDbType.Float).Value = _sgFPAPercent;
                    cmd.Parameters.Add("@FPAAxia", SqlDbType.Float).Value = _sgFPAAxia;
                    cmd.Parameters.Add("@AxiaTeliki", SqlDbType.Float).Value = _sgAxiaTeliki;

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
                using (SqlCommand cmd = new SqlCommand("EditInvoice_Rec", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IT_ID", SqlDbType.Int).Value = _iIT_ID;
                    cmd.Parameters.Add("@Good_Type", SqlDbType.Int).Value = _iGood_Type;
                    cmd.Parameters.Add("@Good_Code", SqlDbType.NVarChar, 50).Value = _sGood_Code;
                    cmd.Parameters.Add("@Good_Title", SqlDbType.NVarChar, 100).Value = _sGood_Title;
                    cmd.Parameters.Add("@Good_MM", SqlDbType.NVarChar, 20).Value = _sGood_MM;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = _sgPrice;
                    cmd.Parameters.Add("@Posotita", SqlDbType.Float).Value = _sgPosotita;
                    cmd.Parameters.Add("@AxiaMikti", SqlDbType.Float).Value = _sgAxiaMikti;
                    cmd.Parameters.Add("@EkptosiPercent", SqlDbType.Float).Value = _sgEkptosiPercent;
                    cmd.Parameters.Add("@EkptosiAxia", SqlDbType.Float).Value = _sgEkptosiAxia;
                    cmd.Parameters.Add("@AxiaKathari", SqlDbType.Float).Value = _sgAxiaKathari;
                    cmd.Parameters.Add("@FPAPercent", SqlDbType.Float).Value = _sgFPAPercent;
                    cmd.Parameters.Add("@FPAAxia", SqlDbType.Float).Value = _sgFPAAxia;
                    cmd.Parameters.Add("@AxiaTeliki", SqlDbType.Float).Value = _sgAxiaTeliki;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Invoice_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int IT_ID { get { return this._iIT_ID; } set { this._iIT_ID = value; } }
        public int Good_Type { get { return this._iGood_Type; } set { this._iGood_Type = value; } }
        public string Good_Code { get { return this._sGood_Code; } set { this._sGood_Code = value; } }
        public string Good_Title { get { return this._sGood_Title; } set { this._sGood_Title = value; } }
        public string Good_MM { get { return this._sGood_MM; } set { this._sGood_MM = value; } }
        public float Price { get { return this._sgPrice; } set { this._sgPrice = value; } }
        public float Posotita { get { return this._sgPosotita; } set { this._sgPosotita = value; } }
        public float AxiaMikti { get { return this._sgAxiaMikti; } set { this._sgAxiaMikti = value; } }
        public float EkptosiPercent { get { return this._sgEkptosiPercent; } set { this._sgEkptosiPercent = value; } }
        public float EkptosiAxia { get { return this._sgEkptosiAxia; } set { this._sgEkptosiAxia = value; } }
        public float AxiaKathari { get { return this._sgAxiaKathari; } set { this._sgAxiaKathari = value; } }
        public float FPAPercent { get { return this._sgFPAPercent; } set { this._sgFPAPercent = value; } }
        public float FPAAxia { get { return this._sgFPAAxia; } set { this._sgFPAAxia = value; } }
        public float AxiaTeliki { get { return this._sgAxiaTeliki; } set { this._sgAxiaTeliki = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}