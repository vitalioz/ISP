using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using Core;
using Tulpep.NotificationWindow;

namespace Tools
{
    public partial class frmSystemServices : Form
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlConnection conn1 = new SqlConnection(Global.connStr);
        SqlConnection conn2 = new SqlConnection(Global.connStr2);
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlCommand cmd2;
        SqlDataReader drList = null;
        private DataTable dtList;
        DataRow dtRow;
        DataRow[] foundRows;
        int iRightsLevel;
        string sExtra;
        public frmSystemServices()
        {
            InitializeComponent();
        }

        private void frmSystemServices_Load(object sender, EventArgs e)
        {
            dAktionDate.Value = DateTime.Now;
            txtYear.Text = DateTime.Now.Year.ToString();

            ucDC.DateFrom = Convert.ToDateTime("01/01/2021");
            ucDC.DateTo = DateTime.Now;
        }

        private void btnRestordeCommandsRec_Click(object sender, EventArgs e)
        {
            string sSQL = "";

            try
            {
                conn.Open();
                conn2.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", txtCommandsID.Text));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    sSQL = "UPDATE Commands SET " +
                           "BulkCommand='" + drList["BulkCommand"] + "', " +
                           "BusinessType_ID=" + drList["BusinessType_ID"] + ", " +
                           "CommandType_ID=" + drList["CommandType_ID"] + ", " +
                           "Client_ID=" + drList["Client_ID"] + ", " +
                           "Company_ID=" + drList["Company_ID"] + ", " +
                           "StockCompany_ID=" + drList["StockCompany_ID"] + ", " +
                            "Executor_ID=" + drList["Executor_ID"] + ", " +
                            "StockExchange_ID=" + drList["StockExchange_ID"] + ", " +
                            "CustodyProvider_ID=" + drList["CustodyProvider_ID"] + ", " +
                            "Depository_ID=" + drList["Depository_ID"] + ", " +
                            "II_ID=" + drList["II_ID"] + ", " +
                            "Parent_ID=" + drList["Parent_ID"] + ", " +
                            "ClientPackage_ID=" + drList["ClientPackage_ID"] + ", " +
                            "Contract_Details_ID=" + drList["Contract_Details_ID"] + ", " +
                            "Contract_Packages_ID=" + drList["Contract_Packages_ID"] + ", " +
                            "Code='" + drList["Code"] + "', " +
                            "ProfitCenter='" + drList["ProfitCenter"] + "', " +
                            "Aktion=" + drList["Aktion"] + ", " +
                            "AktionDate='" + Convert.ToDateTime(drList["AktionDate"]).ToString("yyyy/MM/dd") + "', " +
                            "Share_ID=" + drList["Share_ID"] + ", " +
                            "Product_ID=" + drList["Product_ID"] + ", " +
                            "ProductCategory_ID=" + drList["ProductCategory_ID"] + ", " +
                            "Type=" + drList["Type"] + ", " +
                            "Price='" + Convert.ToDecimal(drList["Price"]).ToString().Replace(",", ".") + "', " +
                            "Quantity='" + Convert.ToDecimal(drList["Quantity"]).ToString().Replace(",", ".") + "', " +
                            "Amount='" + Convert.ToDecimal(drList["Amount"]).ToString().Replace(",", ".") + "', " +
                            "Curr='" + drList["Curr"] + "', " +
                            "Constant =" + drList["Constant"] + ", " +
                            "ConstantDate = '" + drList["ConstantDate"] + "', "  +
                            "ConstantContinue =" + drList["ConstantContinue"] + ", " +
                            "RecieveDate = '" + Convert.ToDateTime(drList["RecieveDate"]).ToString("yyyy/MM/dd") + "', " +
                            "RecieveMethod_ID =" + drList["RecieveMethod_ID"] + ", " +
                            "SendDate = '" + drList["SendDate"] + "', " +
                            "SendCheck =" + drList["SendCheck"] + ", " +
                            "ExecuteDate = '" + Convert.ToDateTime(drList["ExecuteDate"]).ToString("yyyy/MM/dd") + "', " +
                            "RealPrice =" + Convert.ToDecimal(drList["RealPrice"]).ToString().Replace(",", ".") + ", " +
                            "RealQuantity =" + Convert.ToDecimal(drList["RealQuantity"]).ToString().Replace(",", ".") + ", " +
                            "RealAmount =" + Convert.ToDecimal(drList["RealAmount"]).ToString().Replace(",", ".") + ", " +
                            "FeesDiff =" + Convert.ToSingle(drList["FeesDiff"]).ToString().Replace(",", ".") + ", " +
                            "FeesMarket =" + Convert.ToSingle(drList["FeesMarket"]).ToString().Replace(",", ".") + ", " +
                            "AccruedInterest =" + Convert.ToSingle(drList["AccruedInterest"]).ToString().Replace(",", ".") + ", " +
                            "CurrRate =" + Convert.ToSingle(drList["CurrRate"]).ToString().Replace(",", ".") + ", " +
                            "Notes = '" + drList["Notes"] + "' " +
                            " WHERE ID=" + drList["ID"];

                    using (cmd2 = new SqlCommand("sp_Query", conn2))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@SQL", SqlDbType.NVarChar, 1000).Value = sSQL;
                        cmd2.ExecuteNonQuery();
                    }


                    sSQL = "UPDATE Commands SET " +
                          "ValueDate = '" + drList["ValueDate"] + "', " +
                          "ProviderCommandNumber = '" + drList["ProviderCommandNumber"] + "', " +
                          "InformationMethod_ID =" + drList["InformationMethod_ID"] + ", " +
                          "OfficialInformingDate = '" + drList["OfficialInformingDate"] + "', " +
                          "User_ID =" + drList["User_ID"] + ", " +
                          "DateIns = '" + Convert.ToDateTime(drList["DateIns"]).ToString("yyyy/MM/dd") + "', " +
                          "Status =" + drList["Status"] + ", " +
                          "SettlementDate = '" + Convert.ToDateTime(drList["SettlementDate"]).ToString("yyyy/MM/dd") + "', " +
                          "FeesPercent =" + Convert.ToSingle(drList["FeesPercent"]).ToString().Replace(",", ".") + ", " +
                          "FeesAmount =" + Convert.ToSingle(drList["FeesAmount"]).ToString().Replace(",", ".") + ", " +
                          "FeesDiscountPercent =" + Convert.ToSingle(drList["FeesDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                          "FeesDiscountAmount =" + Convert.ToSingle(drList["FeesDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                          "FinishFeesPercent =" + Convert.ToSingle(drList["FinishFeesPercent"]).ToString().Replace(",", ".") + ", " +
                          "FinishFeesAmount =" + Convert.ToSingle(drList["FinishFeesAmount"]).ToString().Replace(",", ".") + ", " +
                          "FeesRate =" + Convert.ToSingle(drList["FeesRate"]).ToString().Replace(",", ".") + ", " +
                          "FeesAmountEUR =" + Convert.ToSingle(drList["FeesAmountEUR"]).ToString().Replace(",", ".") + ", " +
                          "MinFeesCurr = '" + drList["MinFeesCurr"] + "', " +
                          "MinFeesAmount =" + Convert.ToSingle(drList["MinFeesAmount"]).ToString().Replace(",", ".") + ", " +
                          "MinFeesDiscountPercent =" + Convert.ToSingle(drList["MinFeesDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                          "MinFeesDiscountAmount =" + Convert.ToSingle(drList["MinFeesDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                          "FinishMinFeesAmount =" + Convert.ToSingle(drList["FinishMinFeesAmount"]).ToString().Replace(",", ".") + ", " +
                          "TicketFeeCurr = '" + drList["TicketFeeCurr"] + "', " +
                          "TicketFee =" + Convert.ToSingle(drList["TicketFee"]).ToString().Replace(",", ".") + ", " +
                          "TicketFeeDiscountPercent =" + Convert.ToSingle(drList["TicketFeeDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                          "TicketFeeDiscountAmount =" + Convert.ToSingle(drList["TicketFeeDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                          "FinishTicketFee =" + Convert.ToSingle(drList["FinishTicketFee"]).ToString().Replace(",", ".") + ", " +
                          "FeesCalc =" + Convert.ToSingle(drList["FeesCalc"]).ToString().Replace(",", ".") + ", " +
                          "ProviderFees =" + Convert.ToSingle(drList["ProviderFees"]).ToString().Replace(",", ".") + " " +
                          " WHERE ID=" + drList["ID"];


                    using (cmd2 = new SqlCommand("sp_Query", conn2))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@SQL", SqlDbType.NVarChar, 1000).Value = sSQL;
                        cmd2.ExecuteNonQuery();
                    }


                    sSQL = "UPDATE Commands SET " + 
                            "RTO_FeesPercent =" + Convert.ToSingle(drList["RTO_FeesPercent"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesAmount =" + Convert.ToSingle(drList["RTO_FeesAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesDiscountPercent =" + Convert.ToSingle(drList["RTO_FeesDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesDiscountAmount =" + Convert.ToSingle(drList["RTO_FeesDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FinishFeesPercent =" + Convert.ToSingle(drList["RTO_FinishFeesPercent"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FinishFeesAmount =" + Convert.ToSingle(drList["RTO_FinishFeesAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesRate =" + Convert.ToSingle(drList["RTO_FeesRate"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesAmountEUR =" + Convert.ToSingle(drList["RTO_FeesAmountEUR"]).ToString().Replace(",", ".") + ", " +
                            "RTO_MinFeesCurr = '" + drList["RTO_MinFeesCurr"] + "', " +
                            "RTO_MinFeesAmount =" + Convert.ToSingle(drList["RTO_MinFeesAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_MinFeesDiscountPercent =" + Convert.ToSingle(drList["RTO_MinFeesDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                            "RTO_MinFeesDiscountAmount =" + Convert.ToSingle(drList["RTO_MinFeesDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FinishMinFeesAmount =" + Convert.ToSingle(drList["RTO_FinishMinFeesAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_TicketFeeCurr = '" + drList["RTO_TicketFeeCurr"] + "', " +
                            "RTO_TicketFee =" + Convert.ToSingle(drList["RTO_TicketFee"]).ToString().Replace(",", ".") + ", " +
                            "RTO_TicketFeeDiscountPercent =" + Convert.ToSingle(drList["RTO_TicketFeeDiscountPercent"]).ToString().Replace(",", ".") + ", " +
                            "RTO_TicketFeeDiscountAmount =" + Convert.ToSingle(drList["RTO_TicketFeeDiscountAmount"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FinishTicketFee =" + Convert.ToSingle(drList["RTO_FinishTicketFee"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesProVAT =" + Convert.ToSingle(drList["RTO_FeesProVAT"]).ToString().Replace(",", ".") + ", " +
                            "RTO_FeesVAT =" + Convert.ToSingle(drList["RTO_FeesVAT"]).ToString().Replace(",", ".") + ", " +
                            "RTO_CompanyFees =" + Convert.ToSingle(drList["RTO_CompanyFees"]).ToString().Replace(",", ".") + ", " +
                            "RTO_InvoiceTitle_ID =" + drList["RTO_InvoiceTitle_ID"] + ", " +
                            "FeesMisc =" + Convert.ToSingle(drList["FeesMisc"]).ToString().Replace(",", ".") + ", " +
                            "FeesNotes = '" + drList["FeesNotes"] + "', " +
                            "FeesCalcMode =" + drList["FeesCalcMode"] + ", " +
                            "CompanyFeesPercent =" + Convert.ToSingle(drList["CompanyFeesPercent"]).ToString().Replace(",", ".") + ", " +
                            "Pinakidio =" + drList["Pinakidio"] + ", " +
                            "LastCheckFile = '" + drList["LastCheckFile"] + "', " +
                            "Prov_BasicFee =" + Convert.ToSingle(drList["Prov_BasicFee"]).ToString().Replace(",", ".") + ", " +
                            "Prov_TicketFee =" + Convert.ToSingle(drList["Prov_TicketFee"]).ToString().Replace(",", ".") + ", " +
                            "Prov_FeesPercent =" + Convert.ToSingle(drList["Prov_FeesPercent"]).ToString().Replace(",", ".") + ", " +
                            "Prov_BasicFeeAmount =" + Convert.ToSingle(drList["Prov_BasicFeeAmount"]).ToString().Replace(",", ".") + ", " +
                            "Prov_TicketFeeAmount =" + Convert.ToSingle(drList["Prov_TicketFeeAmount"]).ToString().Replace(",", ".") + ", " +
                            "Log_Status =" + drList["Log_Status"] + ", " +
                            "Log_ProblemType =" + drList["Log_ProblemType"] + ", " +
                            "Log_Notes = '" + drList["Log_Notes"] + "', " +
                            "Log_DateIns = '" + Convert.ToDateTime(drList["Log_DateIns"]).ToString("yyyy/MM/dd") + "'  " +
                            " WHERE ID=" + drList["ID"];

                    using (cmd2 = new SqlCommand("sp_Query", conn2))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@SQL", SqlDbType.NVarChar, 1000).Value = sSQL;
                        cmd2.ExecuteNonQuery();
                    }
 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn2.Close(); }

        }
        private void btnBackUp_Click(object sender, EventArgs e)
        {
            // read connectionstring from config file
            //var connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            // read backup folder from config file ("C:/temp/")
            var backupFolder = "C:/TraderBackups/"; // ConfigurationManager.AppSettings["BackupFolder"];

            var sqlConStrBuilder = new SqlConnectionStringBuilder(Global.connStr);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd hh_mm"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }
            }
        }
        private void btnCurrRates_Click(object sender, EventArgs e)
        {
            string sCode = "";
            float fltRate = 0;

            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, AktionDate, Curr, CurrRate, FeesRate FROM Commands WHERE (CurrRate = 0 OR FeesRate = 0) ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text; 
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ((drList["Curr"] + "") != "EUR")
                    {

                        sCode = "EUR" + drList["Curr"] + "=";

                        foundRows = dtList.Select("DateIns = '" + Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy") + "' AND Code = '" + sCode + "'");
                        if (foundRows.Length > 0) fltRate = (float)foundRows[0]["Close"];
                        else
                        {
                            ProductsCodes = new clsProductsCodes();
                            ProductsCodes.DateIns = Convert.ToDateTime(drList["AktionDate"]).Date;
                            ProductsCodes.Code = sCode;
                            ProductsCodes.GetPrice_Code();
                            fltRate = ProductsCodes.LastClosePrice;

                            dtList.Rows.Add(Convert.ToDateTime(drList["AktionDate"]).Date, "EUR" + drList["Curr"] + "=", ProductsCodes.LastClosePrice);
                        }
                    }
                    else fltRate = 1;

                    cmd1 = new SqlCommand("UPDATE Commands SET CurrRate = " + fltRate.ToString().Replace(",", ".") + ", FeesRate = " + fltRate.ToString().Replace(",", ".") +
                                          " WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
                drList.Close();


                cmd = new SqlCommand("SELECT ID, AktionDate, CurrFrom, FeesRate, RTO_FeesCurrRate FROM CommandsFX WHERE (FeesRate = 0 OR RTO_FeesCurrRate = 0) ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ((drList["CurrFrom"] + "") != "EUR")
                    {

                        sCode = "EUR" + drList["CurrFrom"] + "=";

                        foundRows = dtList.Select("DateIns = '" + Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy") + "' AND Code = '" + sCode + "'");
                        if (foundRows.Length > 0) fltRate = (float)foundRows[0]["Close"];
                        else
                        {
                            ProductsCodes = new clsProductsCodes();
                            ProductsCodes.DateIns = Convert.ToDateTime(drList["AktionDate"]).Date;
                            ProductsCodes.Code = sCode;
                            ProductsCodes.GetPrice_Code();
                            fltRate = ProductsCodes.LastClosePrice;

                            dtList.Rows.Add(Convert.ToDateTime(drList["AktionDate"]).Date, "EUR" + drList["CurrFrom"] + "=", ProductsCodes.LastClosePrice);
                        }
                    }
                    else fltRate = 1;

                    cmd1 = new SqlCommand("UPDATE CommandsFX SET FeesRate = " + fltRate.ToString().Replace(",", ".") + ", RTO_FeesCurrRate = " + fltRate.ToString().Replace(",", ".") +
                                          " WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
                drList.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }   
        }

        private void btnRecievedDate_Click(object sender, EventArgs e)
        {

            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT dbo.Commands.ID, dbo.Commands.AktionDate, dbo.Commands.Parent_ID, Commands_1.RecieveDate AS RealRecieveDate  " +
                                                "FROM dbo.Commands INNER JOIN dbo.Commands AS Commands_1 ON dbo.Commands.Parent_ID = Commands_1.ID " +
                                                "WHERE CONVERT(varchar(10), CONVERT(datetime, Commands.AktionDate, 120), 120) = CONVERT(varchar(10), CONVERT(datetime, '" + dAktionDate.Value.ToString("yyyy/MM/dd") + "' , 120), 120)  ORDER BY ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    cmd1 = new SqlCommand("UPDATE Commands SET RecieveDate = '" + Convert.ToDateTime(drList["RealRecieveDate"]).ToString("yyyy/MM/dd hh:mm:ss")  + "' WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }

        private void btnConvertSendDate_Click(object sender, EventArgs e)
        {
            DateTime dTemp;
            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, SendDate FROM dbo.Commands ORDER BY ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (CheckDate(drList["SendDate"] + "")) dTemp = Convert.ToDateTime(drList["SendDate"]);
                    else
                    {
                        //MessageBox.Show("ID = " + drList["ID"] + "  SendDate =" + drList["SendDate"]);
                        dTemp = Convert.ToDateTime("1900/01/01");
                    }

                    cmd1 = new SqlCommand("UPDATE Commands SET SentDate = '" + dTemp.ToString("yyyy/MM/dd hh:mm:ss") + "' WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }


        private bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sTemp;
            SqlConnection conn = new SqlConnection(@"server=10.0.0.15\MSSQLSERVER2012;uid=sa;password=Sql1$Tr@d3rHF;database=TraderExternalData");
            SqlConnection conn1 = new SqlConnection(@"server=10.0.0.15\MSSQLSERVER2012;uid=sa;password=Sql1$Tr@d3rHF;database=Trader");

            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, ShareCodes_ID, ClosePrice FROM ReutersPrices_Recs WHERE ClosePriceDate = '11/12/2020' ORDER BY ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    sTemp = drList["ClosePrice"] + "";
                    if (sTemp != "'NULL'")
                    {
                        cmd1 = new SqlCommand("UPDATE ShareCodes SET LastClosePrice = " + sTemp.Replace(",", ".") + " WHERE ID = " + drList["ShareCodes_ID"], conn1);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sBulkCommand = "", sTemp = "";
            decimal decRate = 0;

            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM SharePrices WHERE ShareType = 3 AND  YEAR(DateIns) = " + txtYear.Text + " ORDER BY SharePrices.ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = dtList.NewRow();
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Close"] = drList["Close"];
                    dtList.Rows.Add(dtRow);
                }
                drList.Close();


                cmd = new SqlCommand("SELECT * FROM dbo.CommandsFX WHERE YEAR(dbo.CommandsFX.AktionDate) = " + txtYear.Text + " AND RTO_FeesCurrRate = 0 ORDER BY dbo.CommandsFX.ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (drList["CurrFrom"] + "" == "EUR") decRate = 1;
                    else {
                        decRate = 0;
                        foundRows = dtList.Select("DateIns = '" + Convert.ToDateTime(drList["AktionDate"]).ToString("yyyy/MM/dd") + "' AND Code = 'EUR" + drList["CurrFrom"] + "='");
                        if (foundRows.Length > 0) decRate = Convert.ToDecimal(foundRows[0]["Close"]);
                    }

                    sTemp = "UPDATE CommandsFX SET RTO_FeesCurrRate = " + decRate.ToString().Replace(",", ".") + " WHERE ID = " + drList["ID"];
                    cmd1 = new SqlCommand(sTemp, conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime dLastDate;

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                 dLastDate = DateTime.Now.AddDays(-1);

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM SharePrices ORDER BY DateIns Desc", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dLastDate = Convert.ToDateTime(drList["DateIns"]);
                }
                drList.Close();
                conn.Close();

                conn.Open();
                cmd = new SqlCommand("SELECT Share_ID, [Close] FROM SharePrices WHERE DateIns = '" + dLastDate.ToString("yyyy/MM/dd") + "'", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    ProductsCodes = new clsProductsCodes();
                    ProductsCodes.Record_ID = Convert.ToInt32(drList["Share_ID"]);
                    ProductsCodes.LastClosePrice = Convert.ToSingle(drList["Close"]);
                    ProductsCodes.EditRecord_LastClosePrice();
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sContractTitle, sFilePath, sFileName, sFileMask, sTemp;
            int i = 0;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();

                cmd = new SqlCommand("SELECT dbo.Invoice_Titles.ID, dbo.Invoice_Titles.FileName, dbo.Contracts.ContractTitle, dbo.Contracts.Tipos AS ContractTipos, " +
                                     "dbo.Clients.Surname, dbo.Clients.Firstname, dbo.Clients.Tipos AS ClientTipos " +
                                     "FROM dbo.Contracts INNER JOIN dbo.Commands ON dbo.Contracts.ID = dbo.Commands.ClientPackage_ID INNER JOIN " +
                                     "dbo.Invoice_Titles ON dbo.Commands.RTO_InvoiceTitle_ID = dbo.Invoice_Titles.ID INNER JOIN dbo.Clients ON dbo.Commands.Client_ID = dbo.Clients.ID " +
                                     "WHERE YEAR(dbo.Invoice_Titles.DateIns) = " + txtYear.Text + " ORDER BY dbo.Invoice_Titles.ID DESC", conn);

                //cmd = new SqlCommand("SELECT   dbo.Invoice_Titles.ID, dbo.Invoice_Titles.FileName, dbo.Contracts.ContractTitle, dbo.ManagmentFees_Recs.ID AS MFR_ID FROM dbo.Contracts INNER JOIN " +
                //                     "dbo.ManagmentFees_Recs ON dbo.Contracts.ID = dbo.ManagmentFees_Recs.Contract_ID INNER JOIN " +
                //                     "dbo.Invoice_Titles ON dbo.ManagmentFees_Recs.Invoice_ID = dbo.Invoice_Titles.ID WHERE YEAR(dbo.Invoice_Titles.DateIns) = " + txtYear.Text +
                //                                " ORDER BY dbo.Invoice_Titles.ID DESC", conn);

                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["ContractTipos"]) == 1) sContractTitle = drList["ContractTitle"] + "";
                    else
                    {
                        if (Convert.ToInt32(drList["ClientTipos"]) == 1) sContractTitle = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                        else sContractTitle = drList["Surname"] + "";
                    }

                    sFileName = "C:/DMS/Customers/" + sContractTitle.Replace(".", "_") + "/Invoices/" + drList["FileName"];
                    if (!File.Exists(sFileName))
                    {
                        //cmd1 = new SqlCommand("UPDATE ManagmentFees_Recs SET Invoice_File = '" + drList["FileName"] + "' WHERE ID = " + drList["MFR_ID"], conn1);
                        //cmd1.CommandType = CommandType.Text;
                        //cmd1.ExecuteNonQuery();


                        sFilePath = "C:/DMS/Customers/" + sContractTitle.Replace(".", "_") + "/Invoices";
                        sFileMask = Path.GetFileNameWithoutExtension(drList["FileName"] + "") + "*" + Path.GetExtension(drList["FileName"] + "");

                        sTemp = "";
                        var docFiles = new DirectoryInfo(sFilePath).GetFiles(sFileMask);
                        foreach (FileInfo file in docFiles)
                        {
                            sTemp = file.Name;
                        }

                        if (sTemp == "")
                        {
                            i = i + 1;
                            fgList.AddItem(sFileName + "\t" + "Not Found");
                        }
                        else
                        {
                            cmd1 = new SqlCommand("UPDATE Invoice_Titles SET FileName = '" + sTemp + "' WHERE ID = " + drList["ID"], conn1);
                            cmd1.CommandType = CommandType.Text;
                            cmd1.ExecuteNonQuery();

                            fgList.AddItem(sTemp + "\t" + "Updated FileName for Invoice_Titles.ID = " + drList["ID"]);
                        }
                    }
                    //else fgList.AddItem(sFileName + "\t" + "File Found");
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); conn1.Close(); }

            fgList.Redraw = true;
            MessageBox.Show("Finish. Not Found = " + i);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string sContractTitle, sFileName;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();

                cmd = new SqlCommand("SELECT dbo.Invoice_Titles.ID, dbo.Invoice_Titles.FileName, dbo.Contracts.ContractTitle, dbo.Contracts.Tipos AS ContractType, " +
                                     "       dbo.Clients.Tipos AS ClientType, dbo.Clients.Surname, dbo.Clients.Firstname " +
                                     "FROM   dbo.Invoice_Titles INNER JOIN dbo.InvoicesRTO_Details ON dbo.Invoice_Titles.ID = dbo.InvoicesRTO_Details.Dtl_InvoiceTitles_ID INNER JOIN" +
                                     "       dbo.Commands ON dbo.InvoicesRTO_Details.Dtl_Command_ID = dbo.Commands.ID INNER JOIN " +
                                     "       dbo.Contracts ON dbo.Commands.ClientPackage_ID = dbo.Contracts.ID LEFT OUTER JOIN " +
                                     "       dbo.Clients ON dbo.Invoice_Titles.Client_ID = dbo.Clients.ID WHERE YEAR(dbo.Invoice_Titles.DateIns) = " + txtYear.Text +
                                                " ORDER BY dbo.Invoice_Titles.ID DESC", conn);

                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["ContractType"]) == 1) sContractTitle = drList["ContractTitle"] + "";
                    else if (Convert.ToInt32(drList["ClientType"]) == 2)  sContractTitle = (drList["Surname"]+"").Trim() ;
                    else sContractTitle = (drList["Surname"] + " " + drList["Firstname"]).Trim();


                    sFileName = "C:/DMS/Customers/" + sContractTitle.Replace(".", "_") + "/Invoices/" + drList["FileName"];
                    if (!File.Exists(sFileName))
                    {
                        fgList.AddItem(sFileName + "\t" + "File Not Found");
                    }
                    //else fgList.AddItem(sFileName + "\t" + "File Found");
                }
                drList.Close();


                cmd = new SqlCommand("SELECT dbo.Invoice_Titles.ID, dbo.Invoice_Titles.FileName, dbo.Contracts.ContractTitle, dbo.Contracts.Tipos AS ContractType, " +
                                     "dbo.Clients.Tipos AS ClientType, dbo.Clients.Surname, dbo.Clients.Firstname " +
                                     "FROM dbo.Clients INNER JOIN dbo.Invoice_Titles ON dbo.Clients.ID = dbo.Invoice_Titles.Client_ID RIGHT OUTER JOIN " +
                                     "dbo.AdminFees_Titles INNER JOIN dbo.AdminFees_Recs ON dbo.AdminFees_Titles.ID = dbo.AdminFees_Recs.AT_ID ON " +
                                     "dbo.Invoice_Titles.ID = dbo.AdminFees_Recs.Invoice_ID LEFT OUTER JOIN dbo.Contracts ON dbo.AdminFees_Recs.Contract_ID = dbo.Contracts.ID  " +
                                     " WHERE YEAR(dbo.Invoice_Titles.DateIns) = " + txtYear.Text + " ORDER BY dbo.Invoice_Titles.ID DESC", conn);

                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["ContractType"]) == 1) sContractTitle = drList["ContractTitle"] + "";
                    else if (Convert.ToInt32(drList["ClientType"]) == 2) sContractTitle = (drList["Surname"] + "").Trim();
                    else sContractTitle = (drList["Surname"] + " " + drList["Firstname"]).Trim();

                    sFileName = "C:/DMS/Customers/" + sContractTitle.Replace(".", "_") + "/Invoices/" + drList["FileName"];
                    if (!File.Exists(sFileName))
                    {
                        fgList.AddItem(sFileName + "\t" + "File Not Found");
                    }
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); conn1.Close(); }

            fgList.Redraw = true;
            MessageBox.Show("Finish");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int j = 0;
            string sClientName, sContractTitle, sFileName;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();

                cmd = new SqlCommand("SELECT dbo.ClientsDocFiles.ID, dbo.ClientsDocFiles.Client_ID, dbo.ClientsDocFiles.PreContract_ID, dbo.ClientsDocFiles.Contract_ID, " +
                                     "dbo.ClientsDocFiles.DocTypes, dbo.ClientsDocFiles.DMS_Files_ID, dbo.DMS_Files.FileName, dbo.Clients.Tipos, dbo.Clients.Surname, dbo.Clients.Firstname, " +
                                     "dbo.Contracts.ContractTitle, dbo.Contracts.Code, dbo.ClientsDocFiles.OldFile FROM dbo.ClientsDocFiles LEFT OUTER JOIN dbo.Clients ON " +
                                     " dbo.ClientsDocFiles.Client_ID = dbo.Clients.ID LEFT OUTER JOIN dbo.DMS_Files ON dbo.ClientsDocFiles.DMS_Files_ID = dbo.DMS_Files.ID " +
                                     "LEFT OUTER JOIN dbo.Contracts ON dbo.ClientsDocFiles.Contract_ID = dbo.Contracts.ID WHERE dbo.ClientsDocFiles.Client_ID <> 0 " + 
                                     " ORDER BY dbo.ClientsDocFiles.ID DESC", conn);

                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["Contract_ID"]) == 0)
                    {
                        if (Convert.ToInt32(drList["Tipos"]) == 1) sClientName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                        else sClientName = (drList["Surname"]+"").Trim();

                        if (sClientName.Length > 0)
                        {
                            if (Convert.ToInt32(drList["OldFile"]) == 1) sClientName = sClientName + "/OldDocs";

                            sFileName = "C:/DMS/Customers/" + sClientName.Replace(".", "_") + "/" + drList["FileName"];
                            if (!File.Exists(sFileName))
                            {
                                j = j + 1;
                                fgList.AddItem(j + "\t" + sFileName + "\t" + drList["ID"] + "\t" + "File Not Found");
                            }
                        }
                        else
                        {
                            j = j + 1;
                            fgList.AddItem(j + "\t" + sClientName + "\t" + drList["ID"] + "\t" + "Wrong Client Name");
                        }
                    }
                    else
                    {
                        sContractTitle = drList["ContractTitle"] + "/" + drList["Code"];
                        if (Convert.ToInt32(drList["OldFile"]) == 1) sContractTitle = sContractTitle + "/OldDocs";
                        sFileName = "C:/DMS/Customers/" + sContractTitle.Replace(".", "_")  + "/" + drList["FileName"];
                        if (!File.Exists(sFileName))
                        {
                            j = j + 1;
                            fgList.AddItem(j + "\t" + sFileName + "\t" + drList["ID"] + "\t" + "File Not Found");
                        }
                    }

                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); conn1.Close(); }


            fgList.Redraw = true;
            MessageBox.Show("Finish");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            float sgRTO_FeesRate = 0, sgRTO_FeesAmountEUR = 0, sgRTO_FeesProVAT = 0, sgRTO_FeesVAT = 0, sgRTO_CompanyFees = 0;
            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {
                

                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Commands WHERE AktionDate > '2021/01/01' ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    sgRTO_FeesRate = Convert.ToSingle(drList["CurrRate"]);
                    if (sgRTO_FeesRate != 0)
                       sgRTO_FeesAmountEUR = Convert.ToSingle(drList["RTO_FinishFeesAmount"]) / sgRTO_FeesRate;
                    else
                       sgRTO_FeesAmountEUR = 0;


                    //lblRTO_FeesAmountEUR.Text = klsOrder.RTO_FeesAmountEUR.ToString("0.##");

                    if (sgRTO_FeesAmountEUR >= Convert.ToSingle(drList["RTO_FinishMinFeesAmount"]))
                        sgRTO_FeesProVAT = sgRTO_FeesAmountEUR + Convert.ToSingle(drList["RTO_FinishTicketFee"]);
                    else
                        sgRTO_FeesProVAT = Convert.ToSingle(drList["RTO_FinishMinFeesAmount"]) + Convert.ToSingle(drList["RTO_FinishTicketFee"]);

                    sgRTO_FeesVAT = 0;
                    sgRTO_CompanyFees = sgRTO_FeesProVAT + sgRTO_FeesVAT;

                    //lblRTO_FeesProVAT.Text = klsOrder.RTO_FeesProVAT.ToString("0.##");
                    //lblRTO_FeesVAT.Text = klsOrder.RTO_FeesVAT.ToString("0.##");
                    //lblRTO_CompanyFees.Text = klsOrder.RTO_CompanyFees.ToString("0.##");

                    cmd1 = new SqlCommand("UPDATE Commands SET RTO_FeesAmountEUR = " + sgRTO_FeesAmountEUR.ToString().Replace(",", ".") +
                                                            ", RTO_FeesProVAT = " + sgRTO_FeesProVAT.ToString().Replace(",", ".") +
                                                            ", RTO_FeesVAT = 0" + 
                                                            ", RTO_CompanyFees = " + sgRTO_CompanyFees.ToString().Replace(",", ".") +
                                                            " WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal sgRTO_FeesAmount, sgInvestAmount, sgRTO_FeesPercent, sgRTO_FinishFeesAmount, sgRTO_FinishFeesPercent, sgRTO_FeesAmountEUR, sgRTO_TicketFeesDiscountAmount, sgRTO_FinishTicketFeesAmount,
                    sgRTO_FeesRate = 0, sgRTO_FeesProVAT = 0, sgRTO_FeesVAT = 0, sgRTO_CompanyFees = 0, sgRTO_MinFeesDiscountAmount, sgRTO_FinishMinFeesAmount, sgRTO_TicketFeesAmount;
            dtList = new DataTable();
            dtList.Columns.Add("DateIns", typeof(DateTime));
            dtList.Columns.Add("Code", typeof(string));
            dtList.Columns.Add("Close", typeof(float));

            clsProductsCodes ProductsCodes = new clsProductsCodes();

            SqlDataReader drList = null;
            try
            {


                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Commands WHERE AktionDate > '2021/01/01' ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {

                    sgInvestAmount = Convert.ToDecimal(drList["RealAmount"]) + Convert.ToDecimal(drList["AccruedInterest"]);                    
                    sgRTO_FeesRate = Convert.ToDecimal(drList["CurrRate"]);

                    if (sgInvestAmount != 0 && sgRTO_FeesRate != 0 && Convert.ToDecimal(drList["RTO_FeesPercent"]) != 0) { 
                        sgRTO_FeesAmount = sgInvestAmount * Convert.ToDecimal(drList["RTO_FeesPercent"]) / 100;
                        sgRTO_FinishFeesAmount = sgRTO_FeesAmount - Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                        sgRTO_FinishFeesPercent = Convert.ToDecimal(drList["RTO_FeesPercent"]) * Convert.ToDecimal(drList["RTO_FinishFeesAmount"]) / sgRTO_FeesAmount;
                        sgRTO_FeesAmountEUR = sgRTO_FinishFeesAmount / sgRTO_FeesRate;

                        sgRTO_MinFeesDiscountAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]) * Convert.ToDecimal(drList["RTO_MinFeesDiscountPercent"]) / 100;
                        sgRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]) - sgRTO_MinFeesDiscountAmount;

                        sgRTO_TicketFeesDiscountAmount = Convert.ToDecimal(drList["RTO_TicketFee"]) * Convert.ToDecimal(drList["RTO_TicketFeeDiscountPercent"]) / 100;
                        sgRTO_FinishTicketFeesAmount = Convert.ToDecimal(drList["RTO_TicketFee"]) - sgRTO_TicketFeesDiscountAmount;

                        if (sgRTO_FeesAmountEUR > sgRTO_FinishMinFeesAmount)
                            sgRTO_FeesProVAT = sgRTO_FeesAmountEUR + sgRTO_FinishTicketFeesAmount;
                        else
                            sgRTO_FeesProVAT = sgRTO_FinishMinFeesAmount + sgRTO_FinishTicketFeesAmount;

                        sgRTO_CompanyFees = sgRTO_FeesProVAT + sgRTO_FeesVAT;

                        cmd1 = new SqlCommand("UPDATE Commands SET RTO_FeesAmount = " + (sgRTO_FeesAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_FinishFeesAmount = " + (sgRTO_FinishFeesAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_FinishFeesPercent = " + (sgRTO_FinishFeesPercent.ToString().Replace(",", ".")) +
                                                                ", RTO_FeesAmountEUR = " + (sgRTO_FeesAmountEUR.ToString().Replace(",", ".")) +
                                                                ", RTO_MinFeesDiscountAmount = " + (sgRTO_MinFeesDiscountAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_FinishMinFeesAmount = " + (sgRTO_FinishMinFeesAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_TicketFeeDiscountAmount = " + (sgRTO_TicketFeesDiscountAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_FinishTicketFee = " + (sgRTO_FinishTicketFeesAmount.ToString().Replace(",", ".")) +
                                                                ", RTO_FeesProVAT = " + (sgRTO_FeesProVAT.ToString().Replace(",", ".")) +
                                                                ", RTO_FeesVAT = 0" +
                                                                ", RTO_CompanyFees = " + (sgRTO_CompanyFees.ToString().Replace(",", ".")) +
                                                                " WHERE ID = " + drList["ID"], conn1);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }

        private void picDocFilesPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDocFilesPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sSource, sTarget, sFileName;
            for (i=1; i <= fgList.Rows.Count - 1; i++)
            {
                sFileName = fgList[i, 0] + "";
                if (sFileName.Length > 0)
                {
                    sFileName = Path.GetFileName(sFileName);
                    sSource = txtDocFilesPath.Text + "/" + sFileName;
                    if (File.Exists(sSource)) { 
                       sTarget = fgList[i, 0] + "";
                       sTarget = sTarget.Replace(@"C:/DMS", txtDMSFolder.Text);
                       File.Copy(sSource, sTarget);
                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            int i = 0;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            var loopTo = fgList.Rows.Count - 2;
            for (i = 1; i <= loopTo; i++)
            {
                EXL.Cells[i + 1, 1].Value = fgList[i, 0];
                EXL.Cells[i + 1, 2].Value = fgList[i, 1];
                EXL.Cells[i + 1, 3].Value = fgList[i, 2];
                EXL.Cells[i + 1, 4].Value = fgList[i, 3];
                EXL.Cells[i + 1, 5].Value = fgList[i, 4];
                EXL.Cells[i + 1, 6].Value = fgList[i, 5];
                EXL.Cells[i + 1, 7].Value = fgList[i, 6];
                EXL.Cells[i + 1, 8].Value = fgList[i, 7];
                EXL.Cells[i + 1, 9].Value = fgList[i, 8];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;

            this.Cursor = Cursors.Default;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int iOldContract_ID = -999;

            SqlDataReader drList = null;
            try
            {

                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT dbo.Contracts.ID AS Contract_ID, dbo.Contracts.Status, dbo.Contracts.DateStart, dbo.Contracts.DateFinish, " +
                                                "dbo.Contracts_Details_Packages.ID, dbo.Contracts_Details_Packages.DateFrom, dbo.Contracts_Details_Packages.DateTo " +
                                                "FROM dbo.Contracts LEFT OUTER JOIN dbo.Contracts_Details_Packages ON dbo.Contracts.ID = dbo.Contracts_Details_Packages.Contract_ID " +
                                                " WHERE dbo.Contracts.ID IS NOT NULL " +
                                                "ORDER BY Contract_ID DESC, dbo.Contracts.Code DESC, dbo.Contracts.Portfolio DESC, dbo.Contracts_Details_Packages.DateFrom DESC, " +
                                                "dbo.Contracts_Details_Packages.ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ((drList["Contract_ID"] + "") != "")
                    {
                        if (iOldContract_ID != Convert.ToInt32(drList["Contract_ID"]))
                        {
                            iOldContract_ID = Convert.ToInt32(drList["Contract_ID"]);

                            if (Convert.ToDateTime(drList["DateTo"]).Date != Convert.ToDateTime("2070/12/31").Date)
                            {
                                cmd1 = new SqlCommand("UPDATE dbo.Contracts_Details_Packages SET DateTo = '2070/12/31' WHERE ID = " + drList["ID"], conn1);
                                cmd1.CommandType = CommandType.Text;
                                cmd1.ExecuteNonQuery();
                            }

                        }
                    }
                }
                drList.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
                conn1.Close();
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }

        private void button11_Click(object sender, EventArgs e)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Popup();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string sTemp;
            SqlConnection conn = new SqlConnection(@"server=10.0.0.14\MSSQLSERVER2019;uid=vs_trader_user;password=mnye2mKOQSg9Mu5rALGN!;database=TraderTest");
            SqlConnection conn1 = new SqlConnection(@"server=10.0.0.14\MSSQLSERVER2019;uid=vs_trader_user;password=mnye2mKOQSg9Mu5rALGN!;database=Trader");

            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ShareTitles", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    cmd1 = new SqlCommand("UPDATE ShareTitles SET ISIN = '" + drList["ISIN"] + "', Title = '" + drList["Title"] + 
                                          "' , StandardTitle = '" + drList["StandardTitle"] + "' WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  " + drList["ID"], Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); conn1.Close(); }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sTemp = "";
            fgList.Rows.Count = 1;
            txtDocFilesPath.Text = "C:/AdminFees";


            fgList.Redraw = false;
            var docFiles = new DirectoryInfo(txtDocFilesPath.Text).GetFiles("*.pdf");
            foreach (FileInfo file in docFiles)
                {
                    fgList.AddItem(file.Name);
                }
                fgList.Redraw = true;


                SqlDataReader drList = null;
                try
                {
                    conn.Open();
                    conn1.Open();
                    SqlCommand cmd = new SqlCommand("SELECT  dbo.Invoice_Titles.ID, dbo.Invoice_Titles.FileName FROM dbo.AdminFees_Recs INNER JOIN dbo.Invoice_Titles ON " +
                                                    "dbo.AdminFees_Recs.Invoice_ID = dbo.Invoice_Titles.ID", conn);
                    cmd.CommandType = CommandType.Text;
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {

                        for (i=1; i <= fgList.Rows.Count-1; i++)
                        {
                            sTemp = fgList[i, 0]+"";
                            if (sTemp.IndexOf(Path.GetFileNameWithoutExtension(drList["FileName"] + "")) >= 0)
                            {
                                cmd1 = new SqlCommand("UPDATE dbo.Invoice_Titles SET FileName = '" + sTemp + "' WHERE ID = " + drList["ID"], conn1);
                                cmd1.CommandType = CommandType.Text;
                                cmd1.ExecuteNonQuery();

                                break;
                            }
                        }


                    }
                    drList.Close();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { conn.Close(); conn1.Close(); }
  
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sTemp = "";
            DateTime dTemp;

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, SentDate, SendDate FROM CommandsLL", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dTemp = Convert.ToDateTime(drList["SendDate"]);
                    cmd1 = new SqlCommand("UPDATE CommandsLL SET SentDate = '" + dTemp.ToString("yyyy/MM/dd") + "' WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
                drList.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }
    }
}
