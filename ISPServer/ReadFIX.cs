using System;
using System.Data;
using System.Collections.Generic;
using Core;

namespace ISPServer
{
    public struct ClientOrder
    {
        public int Command_ID;
        public int CommandExecution_ID;
        public DateTime CurrentTimestamp;
        public string SecondOrdID;
        public decimal Price;
        public decimal Quantity;
        public decimal Percent;
        public decimal RealPrice;
        public decimal RealQuantity;
        public int StockExchange_ID;
    }
    internal class ReadFIX
    {
        int j, iCommand_ID, iStockExchange_ID, iBulcCommand_ID, iBulcCommand2_ID;
        decimal decPrice, decQuantity, decAmount, decKoef, sumQuantity;
        string sTemp, sFIX_DB;
        DataTable dtServiceProviders, dtStockExchanges;
        DateTime dTemp;
        DataRow[] foundRows;
        ClientOrder rOrders;
        List<ClientOrder> coOrders = new List<ClientOrder>();
        List<ExecCommandClient> Commands_ID = new List<ExecCommandClient>();

        clsOrdersSecurity Orders = new clsOrdersSecurity();
        clsOrdersSecurity Orders2 = new clsOrdersSecurity();
        clsOrdersSecurity Orders3 = new clsOrdersSecurity();
        clsOrders_Executions Orders_Executions = new clsOrders_Executions();
        clsCommandsExecutionsDetails CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
        public int Go(int iProvider_ID, DateTime dAktionDate)
        {
            //--- define ServiceProviders list --------------------------------------------
            clsServiceProviders ServiceProviders = new clsServiceProviders();
            ServiceProviders.GetList();
            dtServiceProviders = ServiceProviders.List.Copy();

            sFIX_DB = "";
            foundRows = dtServiceProviders.Select("ID = " + iProvider_ID);
            if (foundRows.Length > 0) sFIX_DB = foundRows[0]["FIX_DB"] + "";            

            //--- define StockExchanges list ----------------------------------------------
            clsStockExchanges StockExchanges = new clsStockExchanges();
            StockExchanges.GetList();
            dtStockExchanges = StockExchanges.List.Copy();

            //--- check ExecutionReports tables new records -------------------------------
            Global.connFIXStr = Global.FIX_DB_Server_Path + "database=" + sFIX_DB;
            clsExecutionReports_Control ExecutionReports_Control = new clsExecutionReports_Control();
            clsExecutionReports ExecutionReports = new clsExecutionReports();
            clsExecutionReports ExecutionReports_2 = new clsExecutionReports();

            ExecutionReports.GetUncheckedList(DateTime.Now.AddDays(-4));
            foreach (DataRow dtRow in ExecutionReports.List.Rows)
            {
                if (Global.IsNumeric(dtRow["ClOrdID"]))
                {
                    iCommand_ID = Convert.ToInt32((dtRow["ClOrdID"]+"").Replace("C", ""));

                    Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> Start  ISP.Commands.ID = " + dtRow["ClOrdID"] + "   OrdStatus = " + dtRow["OrdStatus"]);

                    switch (dtRow["OrdStatus"].ToString())
                    {
                        case "A":
                            Orders = new clsOrdersSecurity();
                            Orders.Record_ID = iCommand_ID;
                            Orders.GetRecord();
                            Orders.FIX_A = 1;
                            Orders.FIX_RecievedDate = Convert.ToDateTime(dtRow["CurrentTimestamp"]).ToLocalTime();
                            Orders.EditRecord();
                            break;
                        case "C":
                        case "4":
                        case "8":
                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> C_4_8    Point 1");

                            Orders = new clsOrdersSecurity();
                            Orders.Record_ID = iCommand_ID;
                            Orders.GetRecord();

                            //--- iBulcCommand2_ID - define childrens orders BulkCommand -------------------------------
                            iBulcCommand2_ID = 0;
                            sTemp = Orders.BulkCommand;
                            if (sTemp.Length > 0)
                            {
                                string[] tokens = sTemp.Replace("<", "").Replace(">", "").Split('/');
                                if (tokens.Length > 0)
                                {
                                    iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                                    if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                                }
                            }

                            //--- cancel order with ID = iCommand_ID ---------------------------------------------------
                            Orders.Notes = "FIX:" + dtRow["Text"] + "/" + Orders.Notes;
                            Orders.Status = -1;
                            Orders.EditRecord();

                            if (iBulcCommand2_ID != 0)
                            {
                                //--- deblock all children orders with bulkCommand = iBulcCommand2_ID -----------------
                                Orders2 = new clsOrdersSecurity();
                                Orders2.AktionDate = Orders.AktionDate;
                                Orders2.BulkCommand = iBulcCommand2_ID + "";
                                Orders2.GetList_BulkCommand();
                                foreach (DataRow dtRow2 in Orders2.List.Rows)
                                {
                                    Orders3 = new clsOrdersSecurity();
                                    Orders3.Record_ID = Convert.ToInt32(dtRow2["ID"]);
                                    Orders3.GetRecord();
                                    Orders3.BulkCommand = "";
                                    Orders3.SentDate = Convert.ToDateTime("1900/01/01");
                                    Orders3.EditRecord();
                                }
                            }

                            dTemp = DateTime.Now;

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> C_4_8    Point 2    Orders.AktionDate.Date = " + Orders.AktionDate.Date + "    dTemp.Date = " + dTemp.Date);

                            if (Orders.AktionDate.Date < dTemp.Date)
                            {
                                Orders2 = new clsOrdersSecurity();
                                Orders2.CommandType_ID = Orders.CommandType_ID;
                                Orders2.DateFrom = Orders.AktionDate;
                                Orders2.DateTo = dTemp;
                                Orders2.ServiceProvider_ID = Orders.ServiceProvider_ID;
                                Orders2.Code = Orders.Code;
                                Orders2.Share_ID = Orders.Share_ID;
                                Orders2.GetList();

                                Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> C_4_8    Point 3");


                                foreach (DataRow dtRow1 in Orders2.List.Rows)
                                    if ((dtRow1["BulkCommand"]+"") == Orders.BulkCommand && Convert.ToInt32(dtRow1["Aktion"]) == Orders.Aktion && 
                                        Convert.ToDateTime(dtRow1["AktionDate"]).Date == dTemp.Date)
                                    {
                                        Orders3 = new clsOrdersSecurity();
                                        Orders3.Record_ID = Convert.ToInt32(dtRow1["ID"]);
                                        Orders3.GetRecord();
                                        Orders3.Notes = "FIX:" + dtRow1["Text"] + "/" + Orders.Notes;
                                        Orders3.Status = -1;
                                        Orders3.EditRecord();
                                    }
                            }

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> C_4_8    Point 4");

                            break;
                        case "0":
                            break;
                        case "1":
                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 1    Point 1");

                            //--- insert Commands_Executions record --------------------
                            Orders_Executions = new clsOrders_Executions();
                            Orders_Executions.Command_ID = iCommand_ID;
                            Orders_Executions.DateExecution = Convert.ToDateTime(dtRow["CurrentTimestamp"]);
                            Orders_Executions.StockExchange_MIC = dtRow["LastMkt"] + "";
                            Orders_Executions.ProviderCommandNumber = dtRow["SecondOrdID"] + "";
                            Orders_Executions.RealPrice = Convert.ToDecimal((dtRow["LastPx"]+"").Replace(".", ","));
                            Orders_Executions.RealQuantity = Convert.ToDecimal((dtRow["LastQty"] + "").Replace(".", ","));
                            Orders_Executions.RealAmount = Orders_Executions.RealPrice * Orders_Executions.RealQuantity;
                            Orders_Executions.AccruedInterest = 0;
                            Orders_Executions.InsertRecord();
                            break;
                        case "2":
                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 1");

                            Orders = new clsOrdersSecurity();
                            Orders.Record_ID = iCommand_ID;
                            Orders.GetRecord();
                            if (Orders.Product_ID == 2) decKoef = 100.0M;                                       // 2 - Omologo
                            else decKoef = 1.0M;
                            decPrice = 0;
                            decQuantity = 0;
                            decAmount = 0;
                            ExecutionReports_2.ClOrdID = dtRow["ClOrdID"]+"";
                            ExecutionReports_2.GetList();
                            foreach(DataRow dtRow1 in ExecutionReports_2.List.Rows)
                            {
                                if (dtRow1["OrdStatus"].ToString() == "1" || dtRow1["OrdStatus"].ToString() == "2")
                                {
                                    decQuantity = decQuantity + Convert.ToDecimal(dtRow1["LastQty"]);
                                    decAmount = decAmount + (Convert.ToDecimal((dtRow1["LastPx"] + "").Replace(".", ",")) * Convert.ToDecimal((dtRow1["LastQty"]+"").Replace(".", ",")) );
                                }
                            }
                            if (decQuantity != 0) 
                                decPrice = decAmount / decQuantity;

                            sTemp = dtRow["TransactTime"] + "   ";
                            dTemp = Convert.ToDateTime(sTemp.Substring(0, 4) + "/" + sTemp.Substring(4, 2) + "/" + sTemp.Substring(6, 2) + " " + sTemp.Substring(9)).ToLocalTime();

                            Orders = new clsOrdersSecurity();
                            Orders.Record_ID = iCommand_ID;
                            Orders.GetRecord();

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 2    Orders.AktionDate.Date = " + Orders.AktionDate.Date + "    dTemp.Date = " + dTemp.Date);

                            if  (Orders.AktionDate.Date < dTemp.Date)   // it means that execution order has GrandFather's records, so find that
                            {
                                Orders2 = new clsOrdersSecurity();
                                Orders2.Record_ID = Orders.Record_ID;
                                Orders2.FirstOrderDate = dTemp;
                                Orders2.GetStartRecord();

                                Orders.Record_ID = Orders2.Record_ID;
                                Orders.GetRecord();

                                Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 3    Orders2.GetStartRecord = " + Orders2.Record_ID);
                            }
                            sumQuantity = Orders.Quantity;

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 4");

                            //--- edit Commands Table record ------------------------------------
                            Orders.ExecuteDate = dTemp;
                            Orders.RealPrice = decPrice;
                            Orders.RealQuantity = decQuantity;
                            Orders.RealAmount = decPrice * decQuantity;

                            sTemp = (dtRow["Text"] + "").Trim();
                            if (sTemp.Length > 0) sTemp = "FIX: " + sTemp + " // " + Orders.Notes;
                            Orders.EditRecord();

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 5");

                            //--- insert Commands_Executions record --------------------
                            Orders_Executions = new clsOrders_Executions();
                            Orders_Executions.Command_ID = iCommand_ID;
                            Orders_Executions.DateExecution = dTemp;
                            Orders_Executions.StockExchange_MIC = dtRow["LastMkt"] + "";
                            Orders_Executions.ProviderCommandNumber = dtRow["SecondOrdID"] + "";
                            Orders_Executions.RealPrice = decPrice;
                            Orders_Executions.RealQuantity = decQuantity;
                            Orders_Executions.RealAmount = decPrice * decQuantity;
                            Orders_Executions.AccruedInterest = 0;
                            Orders_Executions.InsertRecord();

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 6");

                            //--- define StockExchange_ID ---------------------------------------------
                            iStockExchange_ID = 0;
                            foundRows = dtStockExchanges.Select("Code = '" + dtRow["LastMkt"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            //--- insert Commands_ExecutionsDetails records --------------------
                            coOrders = new List<ClientOrder>();

                            Orders2 = new clsOrdersSecurity();
                            Orders2.AktionDate = Orders.AktionDate;
                            Orders2.BulkCommand = Orders.BulkCommand.Replace("<", "").Replace(">", "");
                            Orders2.GetList_BulkCommand();
                            foreach (DataRow dtRow1 in Orders2.List.Rows)
                            {
                                switch (Convert.ToInt32(dtRow1["CommandType_ID"]))
                                {
                                    case 1:
                                        Global.SyncExec_SingleOrder(iCommand_ID, Convert.ToInt32(dtRow1["ID"]), Orders.RealPrice, Orders.RealQuantity, false);

                                        AddCommandIntoList(Convert.ToInt32(dtRow1["ID"]), Convert.ToInt32(dtRow["ID"]), Convert.ToDateTime(dtRow["CurrentTimestamp"]), 
                                                           dtRow["SecondOrdID"] + "", Convert.ToDecimal((dtRow1["Price"] + "").Replace(".", ",")), 
                                                           Convert.ToDecimal((dtRow1["Quantity"] + "").Replace(".", ",")), decPrice, iStockExchange_ID);
                                        break;
                                    case 3:
                                        break;
                                    case 4:
                                        iBulcCommand2_ID = 0;
                                        Orders3 = new clsOrdersSecurity();
                                        Orders3.Record_ID = Convert.ToInt32(dtRow1["ID"]);
                                        Orders3.GetRecord();
                                        sTemp = Orders3.BulkCommand;

                                        if (sTemp.Length > 0)
                                        {
                                            string[] tokens = sTemp.Replace("<", "").Replace(">", "").Split('/');
                                            if (tokens.Length > 0)
                                            {
                                                iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                                                if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                                            }
                                        }

                                        if (iBulcCommand2_ID != 0)
                                        {
                                            Orders3 = new clsOrdersSecurity();
                                            Orders3.AktionDate = Orders.AktionDate;
                                            Orders3.BulkCommand = iBulcCommand2_ID + "";
                                            Orders3.GetList_BulkCommand();
                                            foreach (DataRow dtRow2 in Orders3.List.Rows)
                                            {
                                                AddCommandIntoList(Convert.ToInt32(dtRow2["ID"]), Convert.ToInt32(dtRow["ID"]), 
                                                                   Convert.ToDateTime(dtRow["CurrentTimestamp"]), dtRow["SecondOrdID"] + "",
                                                                   Convert.ToDecimal((dtRow2["Price"] + "").Replace(".", ",")), 
                                                                   Convert.ToDecimal((dtRow2["Quantity"] + "").Replace(".", ",")),
                                                                   decPrice, iStockExchange_ID);
                                            }
                                        }

                                        Global.SyncExec_DPM(iCommand_ID, Convert.ToInt32(dtRow1["ID"]), Convert.ToDecimal(dtRow1["RealPrice"]), Convert.ToDecimal(dtRow1["RealQuantity"]));
                                        if (Convert.ToSingle(dtRow1["AllocationPercent"]) < 100)
                                            Global.SyncDPM_SingleOrder(Convert.ToInt32(dtRow1["ID"]), Convert.ToDecimal(dtRow1["RealPrice"]), Convert.ToDecimal(dtRow1["RealQuantity"]));

                                        break;
                                }
                            }

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 7");

                            //--- calculate each client's Percentage --------------------------------------------
                            for (j = 0; j <= coOrders.Count - 1; j++)
                            {
                                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                                CommandsExecutionsDetails.Command_ID = coOrders[j].Command_ID;
                                CommandsExecutionsDetails.DeleteRecord_Command_ID();

                                rOrders = coOrders[j];
                                rOrders.Percent = rOrders.Quantity / sumQuantity;
                                rOrders.RealQuantity = rOrders.Percent * decQuantity;
                                coOrders[j] = rOrders;

                                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                                CommandsExecutionsDetails.Command_ID = coOrders[j].Command_ID;
                                CommandsExecutionsDetails.CommandExecution_ID = coOrders[j].CommandExecution_ID;
                                CommandsExecutionsDetails.CurrentTimestamp = coOrders[j].CurrentTimestamp;
                                CommandsExecutionsDetails.SecondOrdID = coOrders[j].SecondOrdID;
                                CommandsExecutionsDetails.StockExchange_ID = coOrders[j].StockExchange_ID;
                                CommandsExecutionsDetails.StockCompany_ID = iProvider_ID;
                                CommandsExecutionsDetails.Price = coOrders[j].RealPrice;
                                CommandsExecutionsDetails.Quantity = coOrders[j].RealQuantity;
                                CommandsExecutionsDetails.InsertRecord();
                            }

                            Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.FIX -> 2    Point 8");
                            break;
                        case "3":
                            break;
                        case "6":
                            break;
                    }

                    //--- insert record into ExecutionReports_Control table - it means that current ExecutionReports record was read ---------
                    ExecutionReports_Control = new clsExecutionReports_Control();
                    ExecutionReports_Control.EX_Id = Convert.ToInt32(dtRow["Id"]);
                    ExecutionReports_Control.EX_CurrentTimestamp = Convert.ToDateTime(dtRow["CurrentTimestamp"]);
                    ExecutionReports_Control.EX_ClOrdID = dtRow["ClOrdID"] + "";
                    ExecutionReports_Control.Status = 1;
                    ExecutionReports_Control.InsertRecord();
                }
            }

            return 0;
        }
        private void AddCommandIntoList(int iCommand_ID, int iCommandExecution_ID, DateTime dCurrentTimestamp, string sSecondOrdID, 
                                        decimal decPrice, decimal decQuantity, decimal decRealPrice, int iStockExchange_ID)
        {
            coOrders.Insert(coOrders.Count, new ClientOrder
            {
                Command_ID = iCommand_ID,
                CommandExecution_ID = iCommandExecution_ID,
                CurrentTimestamp = dCurrentTimestamp,
                SecondOrdID = sSecondOrdID,
                Price = decPrice,
                Quantity = decQuantity,
                Percent = 0,
                RealPrice = decRealPrice,
                RealQuantity = 0,
                StockExchange_ID = iStockExchange_ID
            });
        }
    }
}
