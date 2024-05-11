using System;
using System.Xml;
using System.IO;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Reports
{
    public partial class frmTRS : Form
    {
        int i, j, k, m, iOld_ID, iLateRecords;
        string  sCurrentBulk = "", sParentBulk = "", sTemp = "";
        string a1, a3, a4, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18;  
        string b1, b2, b3, b4, b5, b6, b7, b8, b9, s1, s2, s3, s4, s5, s6, s7, s8 = "", s9 = "";
        string c01, c2, c3, c4, c5, c6, c25, c26, c27, c28, c29, c30, c31, c34, c36, c37, c57, c58, c59, c60, c61, c65;
        Boolean bFilter;
        CellRange rng;
        clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();

        public frmTRS()
        {
            InitializeComponent();
        }

        private void frmTRS_Load(object sender, EventArgs e)
        {
            k = 0;
            iOld_ID = 0;
            sCurrentBulk = "";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = "AA";
            rng = fgList.GetCellRange(2, 0, 2, 0);
            rng.Data = "";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = "Report Status";
            rng = fgList.GetCellRange(2, 1, 2, 1);
            rng.Data = "RTS22-1";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Transaction Reference Number";
            rng = fgList.GetCellRange(2, 2, 2, 2);
            rng.Data = "RTS22-2";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Trading ID indicator";
            rng = fgList.GetCellRange(2, 3, 2, 3);
            rng.Data = "ATHEX-1";

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = "Trading venue transaction identification code";
            rng = fgList.GetCellRange(2, 4, 2, 4);
            rng.Data = "RTS22-3";

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = "Executing entity identification code";
            rng = fgList.GetCellRange(2, 5, 2, 5);
            rng.Data = "RTS22-4";

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = "Investment Firm covered by Directive 2014/65/EU";
            rng = fgList.GetCellRange(2, 6, 2, 6);
            rng.Data = "RTS22-5";

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = "Submitting entity identification code ";
            rng = fgList.GetCellRange(2, 7, 2, 7);
            rng.Data = "RTS22-6";

            rng = fgList.GetCellRange(0, 8, 0, 18);
            rng.Data = "Buyer";

            fgList[1, 8] = "Identification type";
            fgList[2, 8] = "ATHEX-3";
            fgList[1, 9] = "Identification code";
            fgList[2, 9] = "RTS22-7";
            fgList[1, 10] = "Country of the branch";
            fgList[2, 10] = "RTS22-8";
            fgList[1, 11] = "First name(s) ";
            fgList[2, 11] = "RTS22-9";
            fgList[1, 12] = "Surname(s) ";
            fgList[2, 12] = "RTS22-10";
            fgList[1, 13] = "Date of birth";
            fgList[2, 13] = "RTS22-11";
            fgList[1, 14] = "Decision maker type";
            fgList[2, 14] = "ATHEX-4";
            fgList[1, 15] = "Decision maker code";
            fgList[2, 15] = "RTS22-12";
            fgList[1, 16] = "Decision maker - First Name(s)";
            fgList[2, 16] = "RTS22-13";
            fgList[1, 17] = "Decision maker – Surname(s)";
            fgList[2, 17] = "RTS22-14";
            fgList[1, 18] = "Decision maker - Date of birth";
            fgList[2, 18] = "RTS22-15";

            rng = fgList.GetCellRange(0, 19, 0, 29);
            rng.Data = "Seller";

            fgList[1, 19] = "Identification type";
            fgList[2, 19] = "ATHEX-5";
            fgList[1, 20] = "Identification code";
            fgList[2, 20] = "RTS22-16";
            fgList[1, 21] = "Country of the branch";
            fgList[1, 22] = "First name(s) ";
            fgList[1, 23] = "Surname(s) ";
            fgList[1, 24] = "Date of birth";
            fgList[1, 25] = "Decision maker type";
            fgList[1, 26] = "Decision maker code";
            fgList[1, 27] = "Decision maker - First Name(s)";
            fgList[1, 28] = "Decision maker – Surname(s)";
            fgList[1, 29] = "Decision maker - Date of birth";

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = "Transmission of order indicator";

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = "Transmitting firm identification code for the buyer";

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = "Transmitting firm identification code for the seller";

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "Trading date time";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = "Side";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Trading capacity";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "Quantity notaion";

            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = "Quantity";

            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = "Quantity currency";

            fgList.Cols[39].AllowMerging = true;
            rng = fgList.GetCellRange(0, 39, 1, 39);
            rng.Data = "Derivative notional increase/decrease";

            fgList.Cols[40].AllowMerging = true;
            rng = fgList.GetCellRange(0, 40, 1, 40);
            rng.Data = "Price notaion";

            fgList.Cols[41].AllowMerging = true;
            rng = fgList.GetCellRange(0, 41, 1, 41);
            rng.Data = "Price";

            fgList.Cols[42].AllowMerging = true;
            rng = fgList.GetCellRange(0, 42, 1, 42);
            rng.Data = "Price currency";

            fgList.Cols[43].AllowMerging = true;
            rng = fgList.GetCellRange(0, 43, 1, 43);
            rng.Data = "Net amount";

            fgList.Cols[44].AllowMerging = true;
            rng = fgList.GetCellRange(0, 44, 1, 44);
            rng.Data = "Venue";

            fgList.Cols[45].AllowMerging = true;
            rng = fgList.GetCellRange(0, 45, 1, 45);
            rng.Data = "Country of the branch membership";

            fgList.Cols[46].AllowMerging = true;
            rng = fgList.GetCellRange(0, 46, 1, 46);
            rng.Data = "Up-front payment";

            fgList.Cols[47].AllowMerging = true;
            rng = fgList.GetCellRange(0, 47, 1, 47);
            rng.Data = "Up-front payment currency";

            fgList.Cols[48].AllowMerging = true;
            rng = fgList.GetCellRange(0, 48, 1, 48);
            rng.Data = "Complex trade component id";

            fgList.Cols[49].AllowMerging = true;
            rng = fgList.GetCellRange(0, 49, 1, 49);
            rng.Data = "Instrument ID type";

            fgList.Cols[50].AllowMerging = true;
            rng = fgList.GetCellRange(0, 50, 1, 50);
            rng.Data = "Instrument identification code";

            fgList.Cols[51].AllowMerging = true;
            rng = fgList.GetCellRange(0, 51, 1, 51);
            rng.Data = "Instrument full name";

            fgList.Cols[52].AllowMerging = true;
            rng = fgList.GetCellRange(0, 52, 1, 52);
            rng.Data = "Instrument classification";

            fgList.Cols[53].AllowMerging = true;
            rng = fgList.GetCellRange(0, 53, 1, 53);
            rng.Data = "Notional currency 1";

            fgList.Cols[54].AllowMerging = true;
            rng = fgList.GetCellRange(0, 54, 1, 54);
            rng.Data = "Notional currency 2";

            fgList.Cols[55].AllowMerging = true;
            rng = fgList.GetCellRange(0, 55, 1, 55);
            rng.Data = "Price multiplier";

            fgList.Cols[56].AllowMerging = true;
            rng = fgList.GetCellRange(0, 56, 1, 56);
            rng.Data = "Underlying instrument code";

            fgList.Cols[57].AllowMerging = true;
            rng = fgList.GetCellRange(0, 57, 1, 57);
            rng.Data = "Underlying index name";

            fgList.Cols[58].AllowMerging = true;
            rng = fgList.GetCellRange(0, 58, 1, 58);
            rng.Data = "Term of the underlying index";

            fgList.Cols[59].AllowMerging = true;
            rng = fgList.GetCellRange(0, 59, 1, 59);
            rng.Data = "Option type";

            fgList.Cols[60].AllowMerging = true;
            rng = fgList.GetCellRange(0, 60, 1, 60);
            rng.Data = "Strike price notation";

            fgList.Cols[61].AllowMerging = true;
            rng = fgList.GetCellRange(0, 61, 1, 61);
            rng.Data = "Strike price";

            fgList.Cols[62].AllowMerging = true;
            rng = fgList.GetCellRange(0, 62, 1, 62);
            rng.Data = "Strike price currency";

            fgList.Cols[63].AllowMerging = true;
            rng = fgList.GetCellRange(0, 63, 1, 63);
            rng.Data = "Option exercise style ";

            fgList.Cols[64].AllowMerging = true;
            rng = fgList.GetCellRange(0, 64, 1, 64);
            rng.Data = "Maturity date";

            fgList.Cols[65].AllowMerging = true;
            rng = fgList.GetCellRange(0, 65, 1, 65);
            rng.Data = "Expiry date";

            fgList.Cols[66].AllowMerging = true;
            rng = fgList.GetCellRange(0, 66, 1, 66);
            rng.Data = "Delivery type";

            fgList.Cols[67].AllowMerging = true;
            rng = fgList.GetCellRange(0, 67, 1, 67);
            rng.Data = "Investment decision within firm Type";

            fgList.Cols[68].AllowMerging = true;
            rng = fgList.GetCellRange(0, 68, 1, 68);
            rng.Data = "Investment decision within firm";

            fgList.Cols[69].AllowMerging = true;
            rng = fgList.GetCellRange(0, 69, 1, 69);
            rng.Data = "Investment decision Maker Country";

            fgList.Cols[70].AllowMerging = true;
            rng = fgList.GetCellRange(0, 70, 1, 70);
            rng.Data = "Execution within firm type";

            fgList.Cols[71].AllowMerging = true;
            rng = fgList.GetCellRange(0, 71, 1, 71);
            rng.Data = "Execution within firm";

            fgList.Cols[72].AllowMerging = true;
            rng = fgList.GetCellRange(0, 72, 1, 72);
            rng.Data = "Executor Country";

            fgList.Cols[73].AllowMerging = true;
            rng = fgList.GetCellRange(0, 73, 1, 73);
            rng.Data = "Waiver indicator ";

            fgList.Cols[74].AllowMerging = true;
            rng = fgList.GetCellRange(0, 74, 1, 74);
            rng.Data = "Short selling indicator";

            fgList.Cols[75].AllowMerging = true;
            rng = fgList.GetCellRange(0, 75, 1, 75);
            rng.Data = "OTC post-trade indicator";

            fgList.Cols[76].AllowMerging = true;
            rng = fgList.GetCellRange(0, 76, 1, 76);
            rng.Data = "Commodity derivative indicator";

            fgList.Cols[77].AllowMerging = true;
            rng = fgList.GetCellRange(0, 77, 1, 77);
            rng.Data = "Securities financing transaction indicator";

            fgList.Cols[78].AllowMerging = true;
            rng = fgList.GetCellRange(0, 78, 1, 78);
            rng.Data = "Business Unit";

            fgList.Cols[79].AllowMerging = true;
            rng = fgList.GetCellRange(0, 79, 1, 79);
            rng.Data = "Free text 1";

            fgList.Cols[80].AllowMerging = true;
            rng = fgList.GetCellRange(0, 80, 1, 80);
            rng.Data = "Free text 2";

            fgList.Cols[81].AllowMerging = true;
            rng = fgList.GetCellRange(0, 81, 1, 81);
            rng.Data = "Routing Instructions";

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            dExecution.Value = DateTime.Now.AddDays(-1);
        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 120;
            fgList.Width = this.Width - 32;
        }
        private void dExecution_ValueChanged(object sender, EventArgs e)
        {
            klsOrderSecurity = new clsOrdersSecurity();
            klsOrderSecurity.CommandType_ID = 1;
            klsOrderSecurity.DateFrom = dExecution.Value;
            klsOrderSecurity.DateTo = dExecution.Value;
            klsOrderSecurity.ServiceProvider_ID = 0;
            klsOrderSecurity.Sent = 0;
            klsOrderSecurity.Actions = 1;
            klsOrderSecurity.User1_ID = 0;
            klsOrderSecurity.User4_ID = 0;
            klsOrderSecurity.Division_ID = 0;
            klsOrderSecurity.Code = "";
            klsOrderSecurity.GetTRSList();

            k = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 3;

            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            foreach (DataRow dtRow in klsOrderSecurity.List.Rows) {

                //if (Convert.ToInt32(dtRow["ID"]) == 504213)
                //    i = i;

                bFilter = false;
                sTemp = dtRow["BulkCommand"]+"";

                sParentBulk = "<0>";
                sCurrentBulk = dtRow["BulkCommand"] + "";
                m = sTemp.IndexOf("/");
                if (m > 0)
                {
                    sParentBulk = sTemp.Substring(0, m);
                    sCurrentBulk = sTemp.Substring(m + 2);
                }

                if ((Convert.ToDateTime(dtRow["ExecuteDate"]).Date != Convert.ToDateTime("01/01/1900").Date) &&  
                   (Convert.ToInt32(dtRow["CommandType_ID"]) > 1 || dtRow["BulkCommand"]+"" == "") && (m < 0 || sParentBulk == "<0>")) 
                   bFilter = true;
                

                if (iOld_ID == Convert.ToInt32(dtRow["ID"])) bFilter = false;
                else  iOld_ID = Convert.ToInt32(dtRow["ID"]);

                if (bFilter) {
                    EmptyRow();
                    c01 = "NEWT";
                    c2 = dtRow["ID"] + "";
                    c3 = dtRow["StockExchanges_MIC"] + "";
                    c4 = Global.LEI;
                    c5 = "1";
                    c6 = "549300GSRN07MNENPL97";                                   // ATHEX LEI  
                    c28 = Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyyMMddhhmmssffffff");
                    c30 = "AOTC";
                    if (dtRow["StockExchanges_MIC"] + "" != "OTC") c36 = "XOFF";
                    else c36 = "XOFF";

                    if (Convert.ToInt32(dtRow["Product_ID"]) == 2) {
                        a9 = "2";
                        a10 = "PERC";
                        c31 = dtRow["Currency"] + "";
                        c34 = "";
                    }
                    else {
                        a9 = "1";
                        a10 = "MONE";
                        c31 = "";
                        c34 = (dtRow["Currency"] + "").ToUpper();
                    }

                    a1 = "1";
                    a11 = dtRow["Share_ISIN"] + "" != "" ? "ISIN" : "OTHR";
                    a13 = "1";
                    a14 = "5";

                    if (Convert.ToInt32(dtRow["Aktion"] + "") == 1)                                       // 1 - AGORA
                    {
                        a8 = "B";
                        switch (Convert.ToInt32(dtRow["CommandType_ID"]))
                        {
                            case 1:                                                                                          // 1 - Simple Order
                                if (Convert.ToInt32(dtRow["ContractTipos"]) != 1) {                                          // ContractTipos = 0 - atomikos or 2 - etairikos  
                                    switch (Convert.ToInt32(dtRow["ClientType"]))
                                    {
                                        case 1:
                                            b2 = dtRow["CountryTax_Code"] + "";
                                            b3 = DefineFirtsname(dtRow["ClientFirstnameEng"]+"");
                                            b4 = dtRow["ClientSurnameEng"] + "";
                                            b5 = dtRow["ClientDoB"] + "";
                                            b1 = Define_Concat(b2, b5, b3, b4);
                                            a3 = "6";
                                            if (Convert.ToInt32(dtRow["Service_ID"]) == 3) {
                                                b7 = dtRow["DiaxFirstname"] + "";
                                                b8 = dtRow["DiaxSurname"] + "";
                                                b9 = dtRow["DiaxDoB"] + "";
                                                b6 = Define_Concat("GR", b9, b7, b8);
                                                a4 = b8.Length == 0 ? "" : "2";
                                                c57 = "GR19800101AGGELSIOPI";
                                                c58 = "GR";
                                            }
                                            break;
                                        case 2:
                                            b1 = dtRow["ClientLEI"] + "";
                                            a3 = "1";
                                            if (Convert.ToInt32(dtRow["Service_ID"]) == 3) {
                                                b7 = dtRow["DiaxFirstname"] + "";
                                                b8 = dtRow["DiaxSurname"] + "";
                                                b9 = dtRow["DiaxDoB"] + "";
                                                b6 = Define_Concat("GR", b9, b7, b8);
                                                a4 = b8.Length == 0 ? "" : "2";

                                                c57 = "GR19800101AGGELSIOPI";
                                                c58 = "GR";
                                            }
                                            break;
                                    }
                                }
                                else {                                                                           // ContractTipos = 1 - koinos  (JOINT/KEM/KOINOS)
                                    a3 = "4";

                                    b1 = "INTC";
                                    iLateRecords = 1;                                                            // 1 - LateRecords   - JOINT/KEM/KOINOS case

                                    if (Convert.ToInt32(dtRow["Service_ID"]) == 3) {
                                        b7 = dtRow["DiaxFirstname"] + "";
                                        b8 = dtRow["DiaxSurname"] + "";
                                        b9 = dtRow["DiaxDoB"] + "";
                                        b6 = Define_Concat("GR", b9, b7, b8);
                                        a4 = b8.Length == 0 ? "" : "2";
                                    }
                                }

                                s1 = dtRow["ServiceProvider_LEI"] + "";
                                a6 = "1";
                                c25 = "0";
                                break;
                            case 2:
                            case 3:
                            case 4:                                                                           // 2-HF2S, 3-Bulk, 4-DPM order   -> GROUP ORDER
                                b1 = "INTC";
                                a3 = "4";

                                s1 = dtRow["ServiceProvider_LEI"] + "";
                                a6 = "1";
                                c25 = "1";

                                iLateRecords = 2;                                                             // 2 - LateRecords   - GROUP ORDER case
                                break;
                        }

                        if (c25 == "0") {
                            c26 = Global.LEI;
                            c27 = Global.LEI;
                        }
                    }
                    else {                                                                  // <> 1 - POLISI
                        a8 = "S";
                        b1 = dtRow["ServiceProvider_LEI"] + "";
                        a3 = b1.Length > 0 ? "1" : "0";
                        b2 = "GR";
                        b3 = "";
                        switch (Convert.ToInt32(dtRow["CommandType_ID"])) {
                            case 1:                                                                       // 1 - Simple Order
                                if (Convert.ToInt32(dtRow["ContractTipos"]) != 1)
                                {                          // ContractTipos = 0 - atomikos or 2 - etairikos  
                                    switch (Convert.ToInt32(dtRow["ClientType"]))
                                    {
                                        case 1:
                                            s2 = dtRow["CountryTax_Code"] + "";
                                            s3 = DefineFirtsname(dtRow["ClientFirstnameEng"] + "");
                                            s4 = dtRow["ClientSurnameEng"] + "";
                                            s5 = dtRow["ClientDoB"] + "";
                                            s1 = Define_Concat(s2, s5, s3, s4);
                                            a6 = "6";
                                            if (Convert.ToInt32(dtRow["Service_ID"]) == 3)
                                            {
                                                s6 = Global.LEI;

                                                s7 = dtRow["DiaxFirstname"] + "";
                                                s8 = dtRow["DiaxSurname"] + "";
                                                s9 = dtRow["DiaxDoB"] + "";
                                                s6 = Define_Concat("GR", s9, s7, s8);
                                                a7 = s8.Length == 0 ? "" : "2";
                                                c57 = s6 + "";
                                                c58 = "GR";
                                            }
                                            break;
                                        case 2:
                                            s1 = dtRow["ClientLEI"] + "";
                                            a6 = "1";
                                            if (Convert.ToInt32(dtRow["Service_ID"]) == 3)
                                            {
                                                s6 = Global.LEI;
                                                s7 = dtRow["DiaxFirstname"] + "";
                                                s8 = dtRow["DiaxSurname"] + "";
                                                s9 = dtRow["DiaxDoB"] + "";
                                                s6 = Define_Concat("GR", s9, s7, s8);
                                                a7 = s8.Length == 0 ? "" : "2";
                                                c57 = s6;
                                                c58 = "GR";
                                            }
                                            break;
                                    }
                                }
                                else
                                {                                                                          // ContractTipos = 1 - koinos  (JOINT/KEM/KOINOS)
                                    a6 = "4";
                                    s1 = "INTC";
                                    iLateRecords = 1;                                                            // 1 - LateRecords   - JOINT/KEM/KOINOS case
                                }

                                a7 = s8.Length == 0 ? "" : "2";
                                c25 = "0";
                                break;
                            case 2:
                            case 3:
                            case 4:                                                                     // 2-HF2S, 3-Bulk, 4-DPM order   -> GROUP ORDER
                                s1 = "INTC";
                                a6 = "4";

                                b1 = dtRow["ServiceProvider_LEI"] + "";
                                a3 = "1";
                                c25 = "1";
                                iLateRecords = 2;                                                             // 2 - LateRecords   - GROUP ORDER case
                                break;
                        }
                        if (c25 == "0") {
                            c26 = Global.LEI;
                            c27 = Global.LEI;
                        }                   
                    }
                    c29 = "AOTC";          //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    c57 = Define_Concat(dtRow["UserCountryCode"]+"", dtRow["UserDoB"] + "", dtRow["UserFirstname"] + "", dtRow["UserSurname"] + "");
                    c59 = Define_Concat(dtRow["UserCountryCode"] + "", dtRow["UserDoB"] + "", dtRow["UserFirstname"] + "", dtRow["UserSurname"] + "");
                    c60 = dtRow["UserCountryCode"] + "";

                    if (c29 == "MTCH" || c29 == "AOTC") c57 = "";
                    else c58 = "GR";

                    if (c36 == "XOFF") {
                        c37 = "";     // was "GR"
                        c61 = "";
                    }
                    c65 = "0";
                    if (c57 == "") a13 = "";

                    if (c3.Length > 0) a1 = Define_A1(dtRow["StockExchanges_MIC"] + "");
                    else {
                        //a1 = "";
                        //c3 = "XOFF";
                    }
                    if (c25 == "0") {
                        if (a1 != "1" && a1 != "2")
                            c3 = "";
                        c61 = "";
                    }


                    if (c4 == b1 || c4 == s1) {
                        c25 = "1";
                        c26 = "";
                        c27 = "";
                        c29 = "DEAL";
                        a13 = "1";
                        c57 = Define_Concat(dtRow["UserCountryCode"] + "", dtRow["UserDoB"] + "", dtRow["UserFirstname"] + "", dtRow["UserSurname"] + "");
                        c58 = "GR";
                        c60 = "GR";
                    }

                    k = k + 1;
                    fgList.AddItem(k + "\t" + c01 + "\t" + c2 + "\t" + a1 + "\t" + c3 + "\t" + c4 + "\t" + c5 + "\t" + c6 + "\t" +
                               a3 + "\t" + b1 + "\t" + b2 + "\t" + b3 + "\t" + b4 + "\t" + b5 + "\t" + a4 + "\t" + b6 + "\t" + b7 + "\t" + b8 + "\t" + b9 + "\t" +
                               a6 + "\t" + s1 + "\t" + s2 + "\t" + s3 + "\t" + s4 + "\t" + s5 + "\t" + a7 + "\t" + s6 + "\t" + s7 + "\t" + s8 + "\t" + s9 + "\t" +
                               c25 + "\t" + c26 + "\t" + c27 + "\t" + c28 + "\t" + a8 + "\t" + c29 + "\t" +
                               a9 + "\t" + dtRow["RealQuantity"] + "\t" + c31 + "\t" + "" + "\t" + a10 + "\t" + dtRow["RealPrice"] + "\t" + c34 + "\t" +
                               dtRow["RealAmount"] + "\t" + c36 + "\t" + c37 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + a11 + "\t" + dtRow["Share_ISIN"] + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + a12 + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + a13 + "\t" + c57 + "\t" + c58 + "\t" + a14 + "\t" + c59 + "\t" +
                               c60 + "\t" + c61 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + c65 + "\t" + a15 + "\t" + a16 + "\t" + a17 + "\t" + a18);

                    switch(iLateRecords) { 
                            case 1:
                                 JoinRecords(Convert.ToInt32(dtRow["Aktion"]), Convert.ToInt32(dtRow["ID"]));
                        break;
                            case 2:
                                 SecondLevel(Convert.ToInt32(dtRow["ID"]), sCurrentBulk, Convert.ToInt32(dtRow["CommandType_ID"]), Convert.ToInt32(dtRow["Aktion"]) == 1 ?  s1 : b1);
                        break;
                    }
                } 
            }        
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            fgList.Redraw = true;
        }
        private void JoinRecords(int iAktion, int iID)
        {
            DataView dtView;

            dtView = klsOrderSecurity.List.Copy().DefaultView;
            dtView.RowFilter = "ID = " + iID;
            foreach (DataRowView dtViewRow in dtView) {
                EmptyRow();

                if (iAktion == 1) {                                                                  // 1 - AGORA
                    switch (Convert.ToInt32(dtViewRow["ClientType"])) {
                        case 1:
                            b2 = dtViewRow["CountryTax_Code"] + "";
                            b3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                            b4 = dtViewRow["ClientSurnameEng"] + "";
                            b5 = dtViewRow["ClientDoB"] + "";
                            b1 = Define_Concat(b2, b5, b3, b4);
                            a3 = "6";
                            break;
                        case 2:
                            b1 = dtViewRow["ClientLEI"] + "";
                            a3 = "1";
                            break;
                    }

                    s1 = dtViewRow["ServiceProvider_LEI"] + "";
                    a6 = "1";

                    //b1 = "";
                    //a3 = "";
                    s1 = "";
                    a6 = "";
                }
                else
                {
                    switch (Convert.ToInt32(dtViewRow["ClientType"]))
                    {
                        case 1:
                            s2 = dtViewRow["CountryTax_Code"] + "";
                            s3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                            s4 = dtViewRow["ClientSurnameEng"] + "";
                            s5 = dtViewRow["ClientDoB"] + "";
                            s1 = Define_Concat(s2, s5, s3, s4);
                            a6 = "6";
                            break;
                        case 2:
                            s1 = dtViewRow["ClientLEI"] + "";
                            a6 = "1";
                            break;
                    }
                    //b1 = dtViewRow["ServiceProvider_LEI"] + "";
                    //a3 = "1";
                    b1 = "";
                    a3 = "";
                    //s1 = "";
                    //a6 = "";
                }

                k = k + 1;
                fgList.AddItem(k + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               a3 + "\t" + b1 + "\t" + b2 + "\t" + b3 + "\t" + b4 + "\t" + b5 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               a6 + "\t" + s1 + "\t" + s2 + "\t" + s3 + "\t" + s4 + "\t" + s5 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "");
            }            
        }
        private void SecondLevel(int iID, string sBulkCommand, int iCommandType_ID, string sStockCompanyLEI)
        {
            DataView dtView;
            int iLocOld_ID, iLocLateRecords;
            string sBulkCommand2;

            //if (sBulkCommand == "<7612>")
            //    i = i;
            iLocOld_ID = 0;
            iLocLateRecords = 0;
            dtView = klsOrderSecurity.List.Copy().DefaultView;
            dtView.RowFilter = "ID <> " + iID + " AND BulkCommand LIKE '%" + sBulkCommand + "%'";
            foreach (DataRowView dtViewRow in dtView)
            {
                if (iLocOld_ID != Convert.ToInt32(dtViewRow["ID"]))
                {
                    iLocOld_ID = Convert.ToInt32(dtViewRow["ID"]);

                    sBulkCommand2 = "";
                    sTemp = dtViewRow["BulkCommand"] + "";
                    if (sTemp.IndexOf("/") >= 0)
                    {
                        i = sTemp.IndexOf("/");
                        sBulkCommand2 = sTemp.Substring(i + 2);
                    }

                    EmptyRow();
                    c01 = "NEWT";
                    c2 = dtViewRow["ID"] + "";
                    c3 = dtViewRow["StockExchanges_MIC"] + "";
                    c4 = Global.LEI;
                    c5 = "1";
                    c6 = "549300GSRN07MNENPL97";                                                     // ATHEX LEI  
                    c28 = Convert.ToDateTime(dtViewRow["ExecuteDate"]).ToString("yyyyMMddhhmmssffffff");
                    c30 = "AOTC";
                    if (dtViewRow["StockExchanges_MIC"] + "" != "OTC") c36 = "XOFF";
                    else c36 = "XOFF";

                    if (Convert.ToInt32(dtViewRow["Product_ID"]) == 2)
                    {
                        a9 = "2";
                        a10 = "PERC";
                        c31 = (dtViewRow["Currency"] + "").ToUpper();
                        c34 = "";
                    }
                    else
                    {
                        a9 = "1";
                        a10 = "MONE";
                        c31 = "";
                        c34 = (dtViewRow["Currency"] + "").ToUpper();
                    }

                    a1 = "1";
                    a11 = (dtViewRow["Share_ISIN"] + "") != "" ? "ISIN" : "OTHR";
                    a13 = "1";
                    a14 = "5";

                    //if (Convert.ToInt32(dtViewRow["ID"]) == 340684)
                    //    i = i;

                    if (Convert.ToInt32(dtViewRow["Aktion"]) == 1)
                    {                                                                           // 1 - AGORA
                        a8 = "B";
                        switch (Convert.ToInt32(dtViewRow["CommandType_ID"]))
                        {
                            case 1:                                                                              // 1 - Simple Order
                                if (Convert.ToInt32(dtViewRow["ContractTipos"]) != 1) {                          // ContractTipos = 0 - atomikos or 2 - etairikos  
                                    switch (Convert.ToInt32(dtViewRow["ClientType"]))
                                    {
                                        case 1:
                                            b2 = dtViewRow["CountryTax_Code"] + "";
                                            b3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                                            b4 = dtViewRow["ClientSurnameEng"] + "";
                                            b5 = dtViewRow["ClientDoB"] + "";
                                            b1 = Define_Concat(b2, b5, b3, b4);
                                            a3 = "6";
                                            if (Convert.ToInt32(dtViewRow["Service_ID"]) == 3)
                                            {
                                                //b7 = dtViewRow["DiaxFirstname"]
                                                //b8 = dtViewRow["DiaxSurname"]
                                                //b9 = dtViewRow["DiaxDoB"]
                                                //b6 = Define_Concat("GR", b9, b7, b8)
                                                //'a4 = IIf(b8.Length = 0, "", "2"]
                                                //c57 = "GR19800101AGGELSIOPI"
                                                c58 = "GR";
                                            }
                                            break;
                                        case 2:
                                            b1 = dtViewRow["ClientLEI"] + "";
                                            a3 = "1";
                                            if (Convert.ToInt32(dtViewRow["Service_ID"]) == 3)
                                            {
                                                //b7 = dtViewRow["DiaxFirstname"]
                                                //b8 = dtViewRow["DiaxSurname"]
                                                //b9 = dtViewRow["DiaxDoB"]
                                                //b6 = Define_Concat("GR", b9, b7, b8)
                                                //a4 = IIf(b8.Length = 0, "", "2"]

                                                c57 = "GR19800101AGGELSIOPI";
                                                c58 = "GR";
                                            }
                                            break;
                                    }
                                }
                                else                                                                           // ContractTipos = 1 - koinos  (JOINT/KEM/KOINOS)
                                {
                                    a3 = "4";
                                    b1 = "INTC";
                                    iLocLateRecords = 3;                                                       // 1 - LateRecords   - JOINT/KEM/KOINOS 

                                    if (Convert.ToInt32(dtViewRow["Service_ID"]) == 3)
                                    {
                                        //b7 = dtViewRow["DiaxFirstname"]
                                        //b8 = dtViewRow["DiaxSurname"]
                                        //b9 = dtViewRow["DiaxDoB"]
                                        //b6 = Define_Concat("GR", b9, b7, b8)
                                        //a4 = IIf(b8.Length = 0, "", "2"]
                                    }
                                }

                                s1 = sStockCompanyLEI;
                                a6 = "1";
                                c25 = "0";
                                break;
                            case 2:
                            case 3:
                            case 4:                                                                             // 2-HF2S, 3-Bulk, 4-DPM order   -> GROUP ORDER
                                b1 = "INTC";
                                a3 = "4";

                                s1 = dtViewRow["ServiceProvider_LEI"] + "";
                                a6 = "1";
                                c25 = "1";

                                iLocLateRecords = 3;                                                            // 2 - LateRecords   - GROUP ORDER case    
                                break;
                        }

                        if (c25 == "0") {
                            c26 = Global.LEI;
                            c27 = Global.LEI;
                        }
                    }
                    else                                                             // <> 1 - POLISI
                    {
                        a8 = "S";
                        a3 = "1";
                        b1 = sStockCompanyLEI;
                        a3 = b1.Length > 0 ? "1" : "0";
                        b2 = "GR";
                        b3 = "";
                        switch (Convert.ToInt32(dtViewRow["CommandType_ID"])) {
                            case 1:                                                                              //  1 - Simple Order
                                if (Convert.ToInt32(dtViewRow["ContractTipos"]) != 1)
                                {                                                                                // ContractTipos = 0 - atomikos or 2 - etairikos  
                                    switch (Convert.ToInt32(dtViewRow["ClientType"]))
                                    {
                                        case 1:
                                            s2 = dtViewRow["CountryTax_Code"] + "";
                                            s3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                                            s4 = dtViewRow["ClientSurnameEng"] + "";
                                            s5 = dtViewRow["ClientDoB"] + "";
                                            s1 = Define_Concat(s2, s5, s3, s4);
                                            a6 = "6";
                                            if (Convert.ToInt32(dtViewRow["Service_ID"]) == 3)
                                            {
                                                //s6 = curLEI;
                                                //s7 = dtViewRow["DiaxFirstname"];
                                                //s8 = dtViewRow["DiaxSurname"];
                                                //s9 = dtViewRow["DiaxDoB"];
                                                //s6 = Define_Concat("GR", s9, s7, s8);
                                                //a7 = IIf(s8.Length = 0, "", "2"];
                                                c57 = s6;
                                                c58 = "GR";
                                            }
                                            break;
                                        case 2:
                                            s1 = dtViewRow["ClientLEI"] + "";
                                            a6 = "1";
                                            if (Convert.ToInt32(dtViewRow["Service_ID"]) == 3)
                                            {
                                                //s6 = curLEI;
                                                //s7 = dtViewRow["DiaxFirstname"];
                                                // s8 = dtViewRow["DiaxSurname"];
                                                //s9 = dtViewRow["DiaxDoB"];
                                                //s6 = Define_Concat("GR", s9, s7, s8);
                                                //a7 = IIf(s8.Length = 0, "", "2"];
                                                c57 = s6;
                                                c58 = "GR";
                                            }
                                            break;
                                    }
                                }
                                else
                                {                                                                              // ContractTipos = 1 - koinos  (JOINT/KEM/KOINOS)
                                    a6 = "4";
                                    s1 = "INTC";
                                    iLocLateRecords = 3;                                                       // 1 - LocLateRecords   - JOINT/KEM/KOINOS case
                                }

                                a7 = s8.Length == 0 ? "" : "2";
                                c25 = "0";
                                break;
                            case 2:
                            case 3:
                            case 4:                                                                            // 2-HF2S, 3-Bulk, 4-DPM order   -> GROUP ORDER
                                s1 = "INTC";
                                a6 = "4";
                                c25 = "1";
                                iLocLateRecords = 3;                                                          // 2 - LateRecords   - GROUP ORDER case
                                break;
                        }
                        if (c25 == "0") {
                            c26 = Global.LEI;
                            c27 = Global.LEI;
                        }
                    }

                    c29 = "AOTC";                                                                               //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    c57 = Define_Concat(dtViewRow["UserCountryCode"] + "", dtViewRow["UserDoB"] + "", dtViewRow["UserFirstname"] + "", dtViewRow["UserSurname"] + "");
                    c59 = Define_Concat(dtViewRow["UserCountryCode"] + "", dtViewRow["UserDoB"] + "", dtViewRow["UserFirstname"] + "", dtViewRow["UserSurname"] + "");
                    c60 = dtViewRow["UserCountryCode"] + "";

                    if (c29 == "MTCH" || c29 == "AOTC") c57 = "";
                    else c58 = "GR";

                    if (c36 == "XOFF") {
                        c37 = "";                        // was c37 = "GR"
                        c61 = "";
                    }
                    c65 = "0";
                    if (c57 == "") a13 = "";

                    if (c3.Length > 0) a1 = Define_A1(dtViewRow["StockExchanges_MIC"] + "");
                    else
                    {
                        a1 = "";
                        c3 = "XOFF";
                    }
                    if (c25 == "0") {
                        if (a1 != "1" && a1 != "2") c3 = "";
                        c61 = "";
                    }

                    if (c4 == b1 || c4 == s1)
                    {
                        c25 = "1";
                        c26 = "";
                        c27 = "";
                        c29 = "DEAL";
                        a13 = "1";
                        c57 = Define_Concat(dtViewRow["UserCountryCode"] + "", dtViewRow["UserDoB"] + "", dtViewRow["UserFirstname"] + "", dtViewRow["UserSurname"] + "");
                        c58 = "GR";
                        c60 = "GR";
                    }

                    if (iLocLateRecords == 3)
                    {
                        if (sBulkCommand2 == "") JoinRecords(Convert.ToInt32(dtViewRow["Aktion"]), Convert.ToInt32(dtViewRow["ID"]));
                        else ThirdLevel(Convert.ToInt32(dtViewRow["Aktion"]), sBulkCommand2);
                    }
                    else
                    {
                        k = k + 1;
                        fgList.AddItem(k + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               a3 + "\t" + b1 + "\t" + b2 + "\t" + b3 + "\t" + b4 + "\t" + b5 + "\t" + a4 + "\t" + b6 + "\t" + b7 + "\t" + b8 + "\t" + b9 + "\t" +
                               a6 + "\t" + s1 + "\t" + s2 + "\t" + s3 + "\t" + s4 + "\t" + s5 + "\t" + a7 + "\t" + s6 + "\t" + s7 + "\t" + s8 + "\t" + s9 + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                               "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "");
                    }
                }
            }
        }
        private void ThirdLevel(int iAktion, string sBulkCommand)
        {
            DataView dtView;

            dtView = klsOrderSecurity.List.Copy().DefaultView;
            dtView.RowFilter = "BulkCommand = '" + sBulkCommand + "'";
            foreach (DataRowView dtViewRow in dtView)
            {
                EmptyRow();

                if (iAktion == 1)
                {                                              // 1 - AGORA
                    switch (Convert.ToInt32(dtViewRow["ClientType"]))
                    {
                        case 1:
                            b2 = dtViewRow["CountryTax_Code"] + "";
                            b3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                            b4 = dtViewRow["ClientSurnameEng"] + "";
                            b5 = dtViewRow["ClientDoB"] + "";
                            b1 = Define_Concat(b2, b5, b3, b4);
                            a3 = "6";
                            break;
                        case 2:
                            b1 = dtViewRow["ClientLEI"] + "";
                            a3 = "1";
                            break;
                    }

                    s1 = dtViewRow["ServiceProvider_LEI"] + "";
                    a6 = "1";

                    //b1 = "";
                    //a3 = "";
                    s1 = "";
                    a6 = "";
                }
                else
                {
                    switch (Convert.ToInt32(dtViewRow["ClientType"]))
                    {
                        case 1:
                            s2 = dtViewRow["CountryTax_Code"] + "";
                            s3 = DefineFirtsname(dtViewRow["ClientFirstnameEng"] + "");
                            s4 = dtViewRow["ClientSurnameEng"] + "";
                            s5 = dtViewRow["ClientDoB"] + "";
                            s1 = Define_Concat(s2, s5, s3, s4);
                            a6 = "6";
                            break;
                        case 2:
                            s1 = dtViewRow["ClientLEI"] + "";
                            a6 = "1";
                            break;
                    }
                    b1 = dtViewRow["ServiceProvider_LEI"] + "";
                    a3 = "1";

                    b1 = "";
                    a3 = "";
                    //s1 = "";
                    //a6 = "";
                }

                k = k + 1;
                fgList.AddItem(k + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           a3 + "\t" + b1 + "\t" + b2 + "\t" + b3 + "\t" + b4 + "\t" + b5 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           a6 + "\t" + s1 + "\t" + s2 + "\t" + s3 + "\t" + s4 + "\t" + s5 + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                           "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "");
            }        
        }
        private void EmptyRow()
        {
            iLateRecords = 0;

            b1 = "";
            b2 = "";
            b3 = "";
            b4 = "";
            b5 = "";
            b6 = "";
            b7 = "";
            b8 = "";
            b9 = "";
            s1 = "";
            s2 = "";
            s3 = "";
            s4 = "";
            s5 = "";
            s6 = "";
            s7 = "";
            s8 = "";
            s9 = "";
            c01 = "";
            c2 = "";
            c3 = "";
            c4 = "";
            c5 = "";
            c6 = "";
            c25 = "";
            c26 = "";
            c27 = "";
            c28 = "";
            c29 = "";
            c30 = "";
            c31 = "";
            c34 = "";
            c36 = "";
            c37 = "";
            c57 = "";
            c58 = "";
            c59 = "";
            c60 = "";
            c61 = "";
            c65 = "";
            a1 = "";
            a3 = "";
            a4 = "";
            a6 = "";
            a7 = "";
            a8 = "";
            a9 = "";
            a10 = "";
            a11 = "";
            a12 = "";
            a13 = "";
            a14 = "";
            a15 = "";
            a16 = "";
            a17 = "";
            a18 = "";
        }
   
        private string Define_A1(string sSE_Code)
        {
            string sI;
            sI = "";

            if (sSE_Code.Length > 0) {
                switch (sSE_Code)
                {
                    case "XATH":
                        sI = "1";
                        break;
                    case "OTC":
                        sI = "3";
                        c3 = "";
                        break;
                    default:
                        sI = "2";
                        break;
                }
            }

            sI = "3";
            c3 = "";

            return sI;
        }
        private string Define_Concat(string sCountry_Code, string sDoB, string sFirstname, string sSurname)
        {
            string sConcat;
            sConcat = sCountry_Code + sDoB + (sFirstname + "#####").Substring(0, 5) + (sSurname + "#####").Substring(0, 5);

            return sConcat.Trim();
        }
        private string DefineFirtsname(string sFisrtname)
        {
            string sTemp;
            int i = 0;
            sTemp = sFisrtname;
            i = sFisrtname.IndexOf("_");
            if (i > 0) sTemp = sFisrtname.Substring(0, i);

            return sTemp;
        }
        private void tsbExcel_Search_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "TRS";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                if (fgList.Rows[i].Visible) {

                    for (this.j = 0; this.j <= 63; this.j++) {
                        if (j >= 12 && j <= 17) {
                            sTemp = fgList[i, j] + "";
                            if (Global.IsNumeric(sTemp)) EXL.Cells[i + 1, j + 1].Value = Convert.ToDouble(sTemp);
                            else  EXL.Cells[i + 1, j + 1].Value = fgList[i, j];
                        }
                        else EXL.Cells[i + 1, j + 1].Value = fgList[i, j];
                    }
                    m = m + 1;
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }
        private void tsbCSV_Click(object sender, EventArgs e)
        {
            String sCSVFilePath = Application.StartupPath + "/Temp/TRS_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";

            if (fgList.Row > 0) {

                using (var w = new StreamWriter(sCSVFilePath))
                {
                    for (i = 3; i <= fgList.Rows.Count - 1; i++)
                    {
                        sTemp = "";
                        for (j = 1; j <= 80; j++)
                            sTemp = sTemp + fgList[i, j] + ";";

                        sTemp = sTemp + fgList[i, 81];
                        w.WriteLine(sTemp);
                        w.Flush();
                    }
                }       
            }
        }
    }
}
