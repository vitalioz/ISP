using System;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    public class SendEmail_46
    {
        int iResult = 0;
        string sSubject, sBody;
        string sEMailSender = Global.NonReplay_Sender;        
        string sEMailFrom = Global.NonReplay_Username;               
        string sPasswordFrom = Global.NonReplay_Password;     
        string sTemp = "";
        string sRequest_ClientName = "";
        string sRequest_Type_Title = "";
        string sRequest_Num = "";
        string sDate_Issued = "";
        string sDate_Closed = "";
        string sWarning = "";

        public int Go(DataRow dtRow)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var emailData = JsonConvert.DeserializeObject<emailData>(dtRow["Parameters"].ToString());

                sTemp = (emailData.request_id + "").Trim();
                if (Global.IsNumeric(sTemp)) {
                    clsClientsRequests ClientsRequests = new clsClientsRequests();
                    ClientsRequests.Record_ID = Convert.ToInt32(sTemp);
                    ClientsRequests.GetRecord();
                    sRequest_ClientName = ClientsRequests.ClientName;
                    sRequest_Type_Title = ClientsRequests.RequestType_Title + "";
                    sRequest_Num = sTemp;
                    sDate_Issued = ClientsRequests.DateIns.ToString("dd/MM/yyyy");
                    sDate_Closed = ClientsRequests.DateClose.ToString("dd/MM/yyyy");
                    sWarning = ClientsRequests.Warning;
                }
                
                switch (emailData.request_action)
                {
                    case 1:
                        sSubject = "Ενημέρωση αιτήματος";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Το αίτημα σας (" + sRequest_Type_Title + ") με αριθμό " + sRequest_Num +
                                " καταχωρήθηκε επιτυχώς στις " + sDate_Issued + ".<br/>" +
                                "Το αίτημά σας είναι υπό επεξεργασία. Θα ενημερωθείτε με email σύντομα για την ολοκλήρωσή του. <br/><br/><br/> Με εκτίμηση,";
                        break;
                    case 2:
                        sSubject = "Ολοκλήρωση αιτήματος";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Το αίτημα σας (" + sRequest_Type_Title + ") με αριθμό " + sRequest_Num +
                                " επεξεργάστηκε και ολοκληρώθηκε επιτυχώς στις " + sDate_Closed + ".<br/><br/><br/> Με εκτίμηση,";
                        break;
                    case 3:
                        sSubject = "Απόρριψη αιτήματος";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Το αίτημα σας (" + sRequest_Type_Title + ") με αριθμό " + sRequest_Num +
                                " επεξεργάστηκε και απορρίφθηκε στις " + sDate_Closed + ".<br/>" +
                                "Αιτία απόρριψης: " + sWarning + ".<br/><br/> Προσπαθήστε ξανά σύμφωνα με τις οδηγίες μας. <br/><br/><br/> Με εκτίμηση,";
                        break;
                    case 4:
                        sSubject = "Δημιουργία συνθηματικού";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχετε αλλάξει το συνθηματικό σας για την  πρόσβαση στην εφαρμογή της HellaFin.<br/>" +
                                "<p style='color: red;'>Αν δεν κάνατε εσείς την αλλαγή του συνθηματικού, επικοινωνήστε άμεσα μαζί μας.</p><br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 5:
                        sSubject = "Επιτυχημένη εγγραφή - Αποδοχή όρων";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Σας ενημερώνουμε ότι έχετε εγγραφεί επιτυχώς στην εφαρμογή της HellaFin.<br/>" +
                                "<p style='color: red;'>Αν δεν κάνατε εσείς την εγγραφή, επικοινωνήστε άμεσα μαζί μας.</p><br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 6:
                        sSubject = "Αποστολή πρόσκλησης σύνδεσης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχετε στείλει πρόσκληση σύνδεσης στον <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 7:
                        sSubject = "Λάβατε πρόσκληση σύνδεσης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχετε λάβει πρόσκληση σύνδεσης από τον <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<p>Συνδεθείτε στην εφαρμογή της HellasFin και αποδεχτείτε ή απορρίψτε την πρόσκληση.</p><br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 8:
                        sSubject = "Αποσύνδεση προσώπου";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Σας ενημερώνουμε ότι αποσυνδεθήκατε με τον χρήστη <strong>" + emailData.person_name + "</strong>.<br/>" +
                               "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 9:
                        sSubject = "Αποσύνδεση προσώπου";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Σας ενημερώνουμε ότι αποσυνδέσατε τον χρήστη <strong>" + emailData.person_name + "</strong>.<br/>" +
                               "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 10:
                        sSubject = "Απορρίψατε πρόσκληση σύνδεσης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε απορρίψατε την πρόσκληση σύνδεσης του <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 11:
                        sSubject = "Απόρριψη πρόσκλησης σύνδεσης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε η πρόσκληση σύνδεσης απορρίφθηκε από τον <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 12:
                        sSubject = "Επιτυχημένη σύνδεση προσώπου";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έγινε σύνδεση με τον <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 13:
                        sSubject = "Αποδοχή πρόσκλησης σύνδεσης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έγινε σύνδεση με <strong>" + emailData.person_name + "</strong> με ΑΦΜ " + emailData.afm + ".<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 14:
                        sSubject = "Λήξη πρόσκλησης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχει λήξει η αναμονή των 30 ημερών της πρόσκλησης σύνδεσης, που αποστείλατε στον <strong>" + emailData.person_name + "</strong>. <br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 15:
                        sSubject = "Λήξη πρόσκλησης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχει λήξει η αναμονή των 30 ημερών της πρόσκλησης σύνδεσης, που λάβατε από τον <strong>" + emailData.person_name + "</strong>. <br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 16:
                        sSubject = "Ακύρωση πρόσκλησης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι έχετε ακυρώσει της πρόσκληση σύνδεσης για τον χρήστη <strong>" + emailData.person_name + "</strong> .<br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 17:
                        sSubject = "Ακύρωση πρόσκλησης";
                        sBody = "Αγαπητέ πελάτη, <br/><br/> Σας ενημερώνουμε ότι ακυρώθηκε η πρόσκληση σύνδεσης από τον χρήστη <strong>" + emailData.person_name + "</strong>. <br/>" +
                                "<br/><br/>Σας ευχαριστούμε,";
                        break;
                    case 18:
                        sSubject = "Ακύρωση αιτήματος";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Σας ενημερώνουμε ότι ακυρώσατε το " + sRequest_Type_Title + ".<br/>" +
                                "<br/><br/><br/> Σας ευχαριστούμε,";
                        break;
                    case 19:
                        sSubject = "Ακύρωση αιτήματος";
                        sBody = "Αγαπητέ πελάτη, <br/><br/>Σας ενημερώνουμε ότι ο χρήστης <strong>" + emailData.person_name + "</strong> ακύρωσε το " + sRequest_Type_Title + ".<br/>" +
                                "<br/><br/><br/> Σας ευχαριστούμε,";
                        break;
                    case 22:
                        sSubject = "Αποτυχία ταυτοποίησης";                                 // ypopsifios
                        sBody = "Σας ενημερώνουμε ότι διαγραφήκατε ως χρήστης της εφαρμογής της HellasFin. Αν θέλετε, μπορείτε να προσπαθήσετε ξανά.";
                        break;
                    case 23:
                        sSubject = "Υπενθύμιση ταυτοποίησης μέσω εγγράφων";
                        sBody = "Ο χρόνος αναμονής ταυτοποίησης με εγγράφων πρόκειται να λήξει.<br/>" +
                                "Αν χρειάζεστε βοήθεια, καλέστε στα τηλέφωνα επικοινωνίας της εταιρείας για να σας βοηθήσουμε.<br/>" +
                                "Διαφορετικά θα διαγραφείτε ως χρήστης της εφαρμογής.";
                        break;
                    case 24:
                        sSubject = "Έληξε ο χρόνος αναμονής ταυτοποίησης";                  // client
                        sBody = "Έληξε ο χρόνος αναμονής ταυτοποίησης.<br/>" +
                                "Σας ενημερώνουμε ότι κλειδώσαμε την πρόσβαση στην εφαρμογή μας για λόγους ασφαλείας.<br/>" +
                                "Παρακαλούμε επικοινωνήστε μαζί μας το συντομότερο. ";
                        break;
                    case 25:
                        sSubject = "Απορρίφθηκε η ταυτοποίηση μέσω βίντεο κλήσης";
                        sBody = "Η βίντεο κλήση που πραγματοποιήσατε απορρίφθηκε και θα πρέπει να επαναληφθεί. <br/>" +
                                "Αιτία απόρριψης : " + emailData.notes + ".<br/>" +
                                "Θα σας καλέσουμε για να προγραμματίσουμε νέα βίντεο κλήση";
                        break;
                    case 27:
                        sSubject = "Κωδικός μίας χρήσης";
                        sBody = emailData.otp + ": Κωδικός μίας χρήσης για την επαλήθευση του email σας. ";
                        break;
                    //====================================================
                    case 201:
                        sSubject = "Έχετε ένα νέο αίτημα";
                        sBody = "Σας έχει αποσταλεί ένα νέο αίτημα από τον/την " + emailData.person_name + ". Προχωρήστε στην επεξεργασία του.";
                        break;
                    case 202:
                        sSubject = "Ολοκλήρωση αιτήματος";
                        sBody = "Το αίτημα του/της " + sRequest_ClientName + "  (" + sRequest_Type_Title + ") με αριθμό " + sRequest_Num +
                                " επεξεργάστηκε και ολοκληρώθηκε επιτυχώς στις " + sDate_Closed + ".<br/><br/><br/> Με εκτίμηση,";
                        break;
                    case 203:
                        sSubject = "Απόρριψη αιτήματος";
                        sBody = "Το αίτημα του/της " + sRequest_ClientName + "  (" + sRequest_Type_Title + ") με αριθμό " + sRequest_Num +
                                " επεξεργάστηκε και απορρίφθηκε στις " + sDate_Closed + ".<br/>" +
                                "Αιτία απόρριψης: " + sWarning + ".<br/><br/>";
                        break;
                    case 204:   // <- 21
                        sSubject = "Εγγραφή νέου χρήστη στην εφαρμογή";
                        sBody = "Σας ενημερώνουμε ότι εγγράφτηκε στην εφαρμογή της HellasFin ο χρήστης " + emailData.person_name + " με το ΑΦΜ " + emailData.afm;
                        break;
                    case 205:
                        sSubject = "Απορρίφθηκε η ταυτοποίηση μέσω βίντεο κλήσης";
                        sBody = "Σας ενημερώνουμε ότι απορρίφθηκε η ταυτοποίηση μέσω βίντεο κλήσης του χρήστη " + emailData.person_name + " με το ΑΦΜ " + emailData.afm + "<br/>" +
                                "Αιτία απόρριψης : " + emailData.notes + ".<br/>" +
                                "Θα πρέπει να προγραμματιστεί νέα βίντεο κλήση";
                        break;
                }
                
                SendEmail EMail = new SendEmail();
                EMail.EmailSender = sEMailSender;
                EMail.EmailFrom = sEMailFrom;
                EMail.PasswordFrom = sPasswordFrom;
                EMail.EmailRecipient = emailData.recipient_email + "";
                EMail.CC = "";
                if ((emailData.att + "").Trim() != "") sTemp = Global.DMSTransferPoint + "/" + emailData.att;
                else sTemp = "";
                EMail.Attachments = sTemp;
                EMail.Subject = sSubject + "";                
                EMail.Body = sBody + "";                

                iResult = EMail.Go();
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message;
                iResult = 0;
            }
            finally { }
            return iResult;
        }
        public class emailData
        {
            public string otp { get; set; }
            public string recipient_email { get; set; }
            public int request_action { get; set; }
            public int request_id { get; set; }
            public string att { get; set; }
            public string person_name { get; set; }
            public string afm { get; set; }
            public string notes { get; set; }
        }
    }
}
