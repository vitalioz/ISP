using System;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class SendEmail_44
    {
        int k = 0, iII_ID = 0, iResult = 0, iCount = 0;
        int iContract_ID, iContract_Details_ID, iContract_Packages_ID, iClient_ID, iStockCompany_ID, iAdvisorID, iAuthorID, iAuthor_Status;
        string sTemp, sCode, sSubcode, sEmail_Recipient, sMobile_Recipient, sAdvisor, sAdvisorEMail, sAdvisorMobile, sProviderTitle, sInvestPolicy, sService, sIdeesTable, sTable, sMessage,
            sAdvisorEmail_Username, sAdvisorEmail_Password, sAuthorEMail, sAuthorMobile, sAuthorEmail_Username, sAuthorEmail_Password;
        string[] sProducts = {"", "Μετοχές", "Ομόλογα", "", "Διαπραγματεύσιμο Αμοιβαίο Κεφάλαιο", "", "Αμοιβαίο Κεφάλαιο" };
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        string sSubPath;
        string sAttachFiles;
        string sDocFilesPath = Global.DocFilesPath_Win;   // "C:\DMS"

        clsInvestIdees InvestIdees = new clsInvestIdees();
        clsInvestIdees_Customers InvestIdees_Customers = new clsInvestIdees_Customers();
        clsInvestIdees_Products InvestIdees_Products = new clsInvestIdees_Products();
        clsInvestIdees_Commands InvestIdees_Commands = new clsInvestIdees_Commands();
        clsContracts Contracts = new clsContracts();
        clsServerJobs ServerJobs = new clsServerJobs();
        public int Go(DataRow dtRow)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var recordID = JsonConvert.DeserializeObject<recordID>(dtRow["Parameters"].ToString());
                iII_ID = recordID.ii_id;

                InvestIdees_Customers = new clsInvestIdees_Customers();
                InvestIdees_Customers.II_ID = iII_ID;
                InvestIdees_Customers.GetList();

                //---
                sSubPath = InvestIdees_Customers.ClientName;             // = ContractTitle if ContractType = JOINT-, KEM-, KOINOS-     = Clients.Surname if client is Company   = Clients.Surname + ' ' + Clients.Firstname if Client is Person
                sCode = InvestIdees_Customers.Code;
                sSubcode = InvestIdees_Customers.Portfolio;
                sEmail_Recipient = InvestIdees_Customers.Email;
                sMobile_Recipient = InvestIdees_Customers.Mobile;
                iContract_ID = Convert.ToInt32(InvestIdees_Customers.Contract_ID);
                iContract_Details_ID = Global.IsNumeric(InvestIdees_Customers.Contract_Details_ID) ? Convert.ToInt32(InvestIdees_Customers.Contract_Details_ID) : 0;
                iContract_Packages_ID = Global.IsNumeric(InvestIdees_Customers.Contract_Packages_ID) ? Convert.ToInt32(InvestIdees_Customers.Contract_Packages_ID) : 0;
                iClient_ID = Convert.ToInt32(InvestIdees_Customers.Client_ID);
                iStockCompany_ID = Convert.ToInt32(InvestIdees_Customers.StockCompany_ID);
                iAdvisorID = Convert.ToInt32(InvestIdees_Customers.Advisor_ID);
                sAdvisor = InvestIdees_Customers.AdvisorName + "";
                sAdvisorEMail = InvestIdees_Customers.AdvisorEmail + "";
                sAdvisorEmail_Username = InvestIdees_Customers.AdvisorEmail_Username + "";
                sAdvisorEmail_Password = InvestIdees_Customers.AdvisorEmail_Password + "";
                sAdvisorMobile = InvestIdees_Customers.AdvisorMobile + "";
                sAuthorEMail = InvestIdees_Customers.AuthorEmail + "";
                sAuthorEmail_Username = InvestIdees_Customers.AuthorEmail_Username + "";
                sAuthorEmail_Password = InvestIdees_Customers.AuthorEmail_Password + "";
                sAuthorMobile = InvestIdees_Customers.AuthorMobile + "";
                iAuthorID = Convert.ToInt32(InvestIdees_Customers.Author_ID);
                iAuthor_Status = Convert.ToInt32(InvestIdees_Customers.Author_Status);

                //---
                Contracts = new clsContracts();
                Contracts.Record_ID = iContract_ID;
                Contracts.GetRecord();
                switch (Contracts.PackageType)
                {
                    case 2:
                        sProviderTitle = Contracts.AdvisoryServiceProvider_Title + "";
                        sInvestPolicy = Contracts.AdvisoryInvestmentPolicy_Title + "";
                        sService = "Advisory";
                        break;
                    case 5:
                        sProviderTitle = Contracts.DealAdvisoryServiceProvider_Title + "";
                        sInvestPolicy = Contracts.DealAdvisoryInvestmentPolicy_Title + "";
                        sService = "Dealing Advisory";
                        break;
                }

                sTemp = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
                           "<style>html, " +
                           "  body { height:100%; min-height:100%; margin:0; padding:0; overflow:hidden; font-family: 'Segoe UI';  font-size: 12px;}   " +
                           "  .main {width:900px;	margin:0px auto; padding:10px;}   " +
                           "  table {border-collapse: collapse;}" +
                           "  th, td {border: 1px solid orange; padding: 10px; text-align: left; } " +
                           "</style></head><body><br/><br/>" +
                           "<div style='height: 150px;'><img src='http://www.hellasfin.gr/wp-content/uploads/2013/08/logo.png' alt='' /></div><br/><br/>" +
                           "Όνομα πελάτη: " + InvestIdees_Customers.ClientName + "<br/>" +
                           "Κωδικός πελάτη/CIF: " + InvestIdees_Customers.Code + "<br/>" +
                           "Portfolio: " + InvestIdees_Customers.Portfolio + "<br/>" +
                           "Επενδυτικός Σύμβουλος: " + sAdvisor + "<br/>" +
                           "Τήλ.: " + sAdvisorMobile + "         EMail: " + sAdvisorEMail + "<br/>";
                if (iAuthorID != iAdvisorID)
                    sTemp = sTemp + "Αποστολέας: " + Global.UserName + "<br/>" + "Τήλ.: " + Global.UserMobile + "         EMail: " + Global.UserEMail + "<br/>";

                sTemp = sTemp + "Επενδυτική πολιτική: " + sInvestPolicy + "<br/>" + "Υπηρεσία: " + sService + "<br/><br/>";


                k = 0;
                sIdeesTable = "<table style='width: 900px; border:1px solid #bbb;border-collapse:collapse;'>" +
                         "<tr><th style='border:1px solid #bbb; width:80px;'>Πράξη</th><th style='border:1px solid #bbb; width: 180px;'>Τύπος προϊόντος</th>" +
                         "<th style ='border:1px solid #bbb; width:300px;'>Τίτλος</th><th style='border:1px solid #bbb; width:120px;'>ISIN</th>" +
                         "<th style='border:1px solid #bbb; width:60px;'>Νόμισμα</th><th style='border:1px solid #bbb;'>Ονομαστική αξία ή τεμάχια</th>" +
                         "<th style='border:1px solid #bbb; width:100px;'>Τιμή</th><th style='border:1px solid #bbb; width:100px;'>Διάρκεια συναλλαγής</th></tr>";

                InvestIdees_Products = new clsInvestIdees_Products();
                InvestIdees_Products.II_ID = iII_ID;
                InvestIdees_Products.GetList();                
                foreach (DataRow dtRow2 in InvestIdees_Products.List.Rows)
                {
                    k = k + 1;
                    sIdeesTable = sIdeesTable + "<tr><td style='border:1px solid #bbb;'>" + (Convert.ToInt32(dtRow2["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ") +
                                  "</td><td style='border:1px solid #bbb; align=left;'>" + sProducts[Convert.ToInt32(dtRow2["ShareType"])] +
                                  "</td><td style='border:1px solid #bbb; align=left;'>" + dtRow2["Title"] + "</td><td style='border:1px solid #bbb; align:left;'>" +
                                  dtRow2["ISIN"] + "<td style='border:1px solid #bbb; align=left;'>" + dtRow2["Curr"] + "</td>" +
                                  "<td style='border:1px solid #bbb; align=''rigth;'''>" + dtRow2["Quantity"] + "</td><td style='border:1px solid #bbb; align=''rigth;'''>" +
                                  dtRow2["Price"] + "</td><td style='border:1px solid #bbb; align=left;'>" + sConstant[Convert.ToInt32(dtRow2["Constant"])] + "</td></tr>";
                }
                sIdeesTable = sIdeesTable + "</table>";


                sTable = "";
                if (iAuthor_Status == 1)
                {
                    sTable = "Περαιτέρω στοιχεία της επενδυτικής πρότασης περιλαμβάνονται στα επισυναπτόμενα έγγραφα, τα οποία καλείστε να μελετήσετε προσεκτικά. </br></br> " +
                             "Σε περίπτωση που μελετήσατε την επενδυτική πρόταση και επιθυμείτε να προχωρήσετε στις προτεινόμενες συναλλαγές, πατήστε <strong>«Θέλω να πραγματοποιήσω συναλλαγή»</strong>. Θα μεταφερθείτε σε ασφαλές περιβάλλον όπου αποτυπώνονται οι προτεινόμενες συναλλαγές τις οποίες επιθυμείτε να πραγματοποιήσετε και αφού καταχωρήσετε τον κωδικό που θα σας σταλεί με SMS στο κινητό σας τηλέφωνο πατήστε <strong>«Αποστολή Εντολής»</strong>.<br/></br> " +
                             "Σε περίπτωση που μελετήσατε την επενδυτική πρόταση και δεν επιθυμείτε να προχωρήσετε στις προτεινόμενες συναλλαγές, πατήστε <strong>«Δεν θέλω να πραγματοποιήσω συναλλαγή»</strong>.<br/><br/>" +
                             "<table style='width: 400px; height: 24px; cellspacing:0px; cellpadding=0px'>" +
                                 "<tr style='height: 36px;' align='center'>" +
                                     "<td style='border-radius: 2px;' bgcolor='#0099FF'; width: 200px; text-align: center; >" +
                                         "<a href='https://hf2s.hellasfin.gr:4043/default.aspx?iip_id=" + iII_ID + "' style='text-decoration:none; color: black;'>Θέλω να πραγματοποιήσω συναλλαγή</a>" +
                                     "</td>" +
                                     "<td style='border-radius: 2px;' bgcolor='#ED2939'; width: 200px; text-align: center; >" +
                                         "<a href='https://hf2s.hellasfin.gr:4043/default.aspx?iip_id=" + iII_ID + "' style='text-decoration:none; color: black;'>Δεν θέλω να πραγματοποιήσω συναλλαγή</a>" +
                                     "</td>" +
                                 "</tr>" +
                             "</table><br/><br/>" +
                             "Εναλλακτικά, μπορείτε να επικοινωνήσετε τηλεφωνικά με το τμήμα συναλλαγών της εταιρίας και να δώσετε τις εντολές σας.<br/><br/>" +
                             "Σε περίπτωση που δώσετε εντολές συναλλαγών προς διαβίβαση/εκτέλεση στο τμήμα συναλλαγών της εταιρίας, οι οποίες αφορούν τα χρηματοπιστωτικά μέσα που περιλαμβάνονται στην επενδυτική πρόταση, αποδέχεστε ρητά ότι μελετήσατε και κατανοήσατε πλήρως τις περιεχόμενες πληροφορίες της επενδυτικής πρότασης οι οποίες αναγνωρίζονται από εσάς ως σαφείς, ακριβείς και μη παραπλανητικές.<br/><br/>" +
                             "Το τμήμα συναλλαγών της εταιρίας λειτουργεί Δευτέρα-Παρασκευή 09: 00-19:00. Σε περίπτωση που δώσετε εντολή να πραγματοποιηθούν συναλλαγές εκτός του ωραρίου λειτουργίας του τμήματος συναλλαγών, αυτές θα ληφθούν και διαβιβαστούν την επόμενη εργάσιμη ημέρα.";
                }

                sTemp = "Αγαπητέ πελάτη,<br/><br/>" +
                        "Στα πλαίσια της υπηρεσίας παροχής επενδυτικών συμβουλών, σας επισυνάπτουμε επενδυτική πρόταση την οποία καλείστε να μελετήσετε προσεκτικά προκειμένου" +
                        "να αποφασίσετε αν θα προχωρήσετε στις προτεινόμενες συναλλαγές επί των χρηματοπιστωτικών μέσων που αναφέρονται σε αυτήν.<br/><br/>" +
                        "Επιπλέον της επενδυτικής πρότασης, δύναται να επισυνάπτονται αρχεία με σχετικές επιπρόσθετες πληροφορίες επί της επενδυτικής πρότασης.<br/><br/>" +
                        "Παραμένουμε στη διάθεσή σας για οποιαδήποτε επιπρόσθετη επεξήγηση.<br/><br/>" +
                        "Συνοπτικά στοιχεία της επενδυτικής πρότασης παρατίθενται ακολούθως:<br/><br/>" +
                    sIdeesTable + "<br/><br/>" +
                    sTable + "<br/><br/>" +
                    "Με εκτίμηση<br/><br/>" +
                    sAdvisor + "<br/><br/>" +
                    "<strong>HellasFin</strong><br/>" +
                    "<strong>Global Wealth Management</strong><br/><br/>" +
                    "90, 26th Oktovriou Str. Office 507<br/>" +
                    "P.C.546 27, Thessaloniki, Greece<br/>" +
                    "T. +30 2310 517800<br/>" +
                    "F. +30 2310 515053<br/>" +
                    "E. " + sAdvisorEMail + "<br/>" +
                    "W.www.hellasfin.gr<br />" +
                    "</body></html>";


                sAttachFiles = "";
                k = 0;
                InvestIdees = new clsInvestIdees();
                InvestIdees.Record_ID = iII_ID;
                InvestIdees.GetRecord();
                iCount = InvestIdees.SendAttemptsCount;

                if (InvestIdees.ProposalPDFile != "")
                {
                    k = k + 1;
                    sAttachFiles = sAttachFiles + Global.DocFilesPath_Win + "/Customers/" + sSubPath.Replace(".", "_") + "/InvestProposals/" + iII_ID + "/" + InvestIdees.ProposalPDFile + ",";
                }
                if (InvestIdees.StatementFile != "")
                {
                    k = k + 1;
                    sAttachFiles = sAttachFiles + Global.DocFilesPath_Win + "/Customers/" + sSubPath.Replace(".", "_") + "/InvestProposals/" + iII_ID + "/" + InvestIdees.StatementFile;
                }

                SendEmail EMail = new SendEmail();
                EMail.EmailSender = sAuthorEMail;
                EMail.EmailFrom = sAuthorEMail;
                EMail.PasswordFrom = sAuthorEmail_Password;
                EMail.EmailRecipient = sEmail_Recipient + "";
                EMail.CC = InvestIdees.CC_Email + "";
                EMail.Attachments = sAttachFiles;
                EMail.Subject = "Επενδυτική Πρόταση HellasFin " + iII_ID + " (" + sSubPath + "/" + sProviderTitle + ") " + "";
                EMail.Body = sTemp + "";

                iResult = EMail.Go();
                sMessage = EMail.Message;

                if (iResult == 1)
                {
                    InvestIdees_Products = new clsInvestIdees_Products();
                    InvestIdees_Products.II_ID = iII_ID;
                    InvestIdees_Products.GetList();
                    foreach (DataRow dtRow2 in InvestIdees_Products.List.Rows)
                    {
                        InvestIdees_Commands = new clsInvestIdees_Commands();
                        InvestIdees_Commands.DateIns = DateTime.Now;
                        InvestIdees_Commands.II_ID = iII_ID;
                        InvestIdees_Commands.Contract_ID = iContract_ID;
                        InvestIdees_Commands.Contract_Details_ID = iContract_Details_ID;
                        InvestIdees_Commands.Contract_Packages_ID = iContract_Packages_ID;
                        InvestIdees_Commands.Client_ID = iClient_ID;
                        InvestIdees_Commands.Code = sCode;
                        InvestIdees_Commands.Portfolio = sSubcode;
                        InvestIdees_Commands.Aktion = Convert.ToInt32(dtRow2["Aktion"]);
                        InvestIdees_Commands.Share_ID = Convert.ToInt32(dtRow2["ShareCodes_ID"]);
                        InvestIdees_Commands.Product_ID = Convert.ToInt32(dtRow2["Product_ID"]);
                        InvestIdees_Commands.ProductCategory_ID = Convert.ToInt32(dtRow2["ProductCategories_ID"]);
                        InvestIdees_Commands.Quantity = dtRow2["Quantity"] + "";
                        InvestIdees_Commands.Amount = dtRow2["Amount"] + "";
                        InvestIdees_Commands.PriceType = Convert.ToInt32(dtRow2["Type"]);
                        InvestIdees_Commands.Price = dtRow2["Price"] + "";
                        InvestIdees_Commands.PriceUp = dtRow2["PriceUp"] + "";
                        InvestIdees_Commands.PriceDown = dtRow2["PriceDown"] + "";
                        InvestIdees_Commands.Curr = dtRow2["Curr"] + "";
                        InvestIdees_Commands.Constant = Convert.ToInt32(dtRow2["Constant"]);
                        InvestIdees_Commands.ConstantDate = dtRow2["ConstantDate"] + "";
                        InvestIdees_Commands.StockCompany_ID = iStockCompany_ID;
                        InvestIdees_Commands.StockExchange_ID = Convert.ToInt32(dtRow2["StockExchange_ID"]);
                        InvestIdees_Commands.ConfirmationStatus = 0;
                        InvestIdees_Commands.ConfirmationDate = Convert.ToDateTime("1900/01/01");
                        InvestIdees_Commands.Command_ID = 0;
                        InvestIdees_Commands.RecieveDate = Convert.ToDateTime("1900/01/01");
                        InvestIdees_Commands.RecieveMethod_ID = 0;
                        InvestIdees_Commands.RecieveVoicePath = "from frmSendEmail_44";
                        InvestIdees_Commands.Status = 1;
                        InvestIdees_Commands.InsertRecord();
                    }

                    InvestIdees = new clsInvestIdees();
                    InvestIdees.Record_ID = iII_ID;
                    InvestIdees.GetRecord();
                    InvestIdees.RecievedDate = DateTime.Now;
                    InvestIdees.SendAttemptsCount = iCount + 1;
                    InvestIdees.SendMessage = "";
                    InvestIdees.Status = 2;                                                    // 2 - sent 
                    InvestIdees.EditRecord();

                    ServerJobs = new clsServerJobs();
                    ServerJobs.JobType_ID = 42;                                                // 42 - send SMS
                    ServerJobs.Source_ID = 0;
                    ServerJobs.Parameters = "{'mobile': '" + sMobile_Recipient + "', 'message': 'EXETE EΠENΔYTIKH ΠPOTAΣH(" + iII_ID + ") AΠO THN HELLASFIN, ‎ΠAPAKAΛΩ ΔEITE TO EMAIL ΣAΣ H EΠIKOINΩNHΣTE MAΖI MAΣ ΣTO 2310-515100, 210-3387711. EYXAPIΣTΩ!'}";
                    ServerJobs.DateStart = DateTime.Now;
                    ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                    ServerJobs.PubKey = "";
                    ServerJobs.PrvKey = "";
                    ServerJobs.Attempt = 0;
                    ServerJobs.Status = 0;
                    ServerJobs.InsertRecord();
                }
                else
                {
                    InvestIdees = new clsInvestIdees();
                    InvestIdees.Record_ID = iII_ID;
                    InvestIdees.GetRecord();
                    InvestIdees.RecievedDate = Convert.ToDateTime("1900/01/01");
                    InvestIdees.SendAttemptsCount = iCount + 1;
                    InvestIdees.SendMessage = sMessage;
                    InvestIdees.EditRecord();
                }
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message;
                iResult = 0;
            }
            finally { }
            return iResult;
        }
        public class recordID
        {
            public int ii_id { get; set; }
        }
    }
}
