// DocuSign References
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSign.eSign.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace DocuSign.ISP
{
    class Program
    {
        static string templateName = "Portfolio_1";
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Enter your DocuSign credentials
            var credentials = new DocusignCredentials
            {
                Username = "vitaly@otenet.gr",
                Password = "Kv*26101959",
                IntegratorKey = "b6866e8a-a367-4e51-b994-f7f9489e7314"
            };

            // specify the document (file) we want signed
            string SignTest1File = @"C:\DMS\Portfolio_1.pdf";

            // Enter recipient (signer) name and email address
            string recipientName = "Vitaly Kougioumtzidis";
            string recipientEmail = "vito2610@gmail.com";

            string recipientName2 = "Vito Kou";
            string recipientEmail2 = "v.kougioumtzidis@hellasfin.gr";

            // instantiate api client with appropriate environment (for production change to www.docusign.net/restapi)
            string basePath = "https://demo.docusign.net/restapi";

            // instantiate a new api client
            var apiClient = new ApiClient(basePath);

            // set client in global config so we don't need to pass it to each API object
            Configuration.Default.ApiClient = apiClient;

            string authHeader = JsonConvert.SerializeObject(credentials);
            //DocusignCredentials cred = JsonConvert.DeserializeObject<DocusignCredentials>(authHeader);
            Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

            // we will retrieve this from the login() results
            string accountId = null;

            // the authentication api uses the apiClient (and X-DocuSign-Authentication header) that are set in Configuration object
            var authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            // user might be a member of multiple accounts
            accountId = loginInfo.LoginAccounts[0].AccountId;

            //Console.WriteLine("LoginInformation: {0}", loginInfo.ToJson());

            // Read a file from disk to use as a document
            byte[] fileBytes = File.ReadAllBytes(SignTest1File);

            var envDef = new EnvelopeDefinition();
            envDef.EmailSubject = "Παρακαλώ υπογράψτε το έγγραφο";

            // Add a document to the envelope
            var doc = new Document();
            doc.DocumentBase64 = Convert.ToBase64String(fileBytes);
            doc.Name = "Σύμβαση.pdf";
            doc.DocumentId = "1";

            envDef.Documents = new List<Document>();
            envDef.Documents.Add(doc);

            // Add a recipient to sign the documeent
            var signer = new Signer();
            signer.Name = recipientName;
            signer.Email = recipientEmail;
            signer.RecipientId = "1";
            signer.RoutingOrder = "1";

            var signer2 = new Signer();
            signer2.Name = recipientName2;
            signer2.Email = recipientEmail2;
            signer2.RecipientId = "2";
            signer.RoutingOrder = "2";

            // Create a |SignHere| tab somewhere on the document for the recipient to sign
            signer.Tabs = new Tabs();
            signer.Tabs.SignHereTabs = new List<SignHere>();
            var signHere = new SignHere();
            signHere.DocumentId = "1";
            signHere.PageNumber = "11";
            signHere.RecipientId = "1";
            signHere.XPosition = "346";
            signHere.YPosition = "420";
            signer.Tabs.SignHereTabs.Add(signHere);

            signer2.Tabs = new Tabs();
            signer2.Tabs.SignHereTabs = new List<SignHere>();
            var signHere2 = new SignHere();
            signHere2.DocumentId = "1";
            signHere2.PageNumber = "11";
            signHere2.RecipientId = "2";
            signHere2.XPosition = "346";
            signHere2.YPosition = "444";
            signer2.Tabs.SignHereTabs.Add(signHere2);

            envDef.Recipients = new Recipients();
            envDef.Recipients.Signers = new List<Signer>();
            envDef.Recipients.Signers.Add(signer);
            envDef.Recipients.Signers.Add(signer2);

            // set envelope status to "sent" to immediately send the signature request
            envDef.Status = "sent";

            // create template
            // Step 1. List templates to see if ours exists already

            var accessToken = "eyJ0eXAiOiJNVCIsImFsZyI6IlJTMjU2Iiwia2lkIjoiNjgxODVmZjEtNGU1MS00Y2U5LWFmMWMtNjg5ODEyMjAzMzE3In0.AQoAAAABAAUABwAAFGkYq1vYSAgAAFSMJu5b2EgCAOnlSv3kedNOownxwLlrN4gVAAEAAAAYAAEAAAAFAAAADQAkAAAAYjY4NjZlOGEtYTM2Ny00ZTUxLWI5OTQtZjdmOTQ4OWU3MzE0IgAkAAAAYjY4NjZlOGEtYTM2Ny00ZTUxLWI5OTQtZjdmOTQ4OWU3MzE0MAAAEgWPqVvYSDcAumt1S8Nm6kO2Ac_1KVAo6A.ENXXPDmLyLZVTZpVi3zngtobkiyWFaP47hkdAxARIUtC1ur8m63Bm1KHloFW8a4b90yf1YivmB-ntWPHcLiTjtC1AdGYfc2-ZUpNqCe_wDBEbU-jMrIplY-BAbHppKUW7qjkQW-PYEYyFAvaAPUI17LfoN65y0RVHTiuwVr75ss7UnRpe_AVYRhdfPsS7aEKIvzRNax7CtbsfbhBGe8BmLcOVV9Kw1q8lDM09BdlAHrCAt5aLyPDl8X39iHUNv0tMZVVSGGVDw1VspOOwExVDp7mFpo4Ig_KnHqEoSpjYgwllUVuHLGd0AYpodabfhwgCNtutWri41ray8xYOG7v7A";
            //var basePath = RequestItemsService.Session.BasePath + "/restapi";
            //var accountId = RequestItemsService.Session.AccountId;

            var config = new Configuration(new ApiClient(basePath));
            config.AddDefaultHeader("Authorization", "Bearer " + accessToken);
            TemplatesApi templatesApi = new TemplatesApi(config);
            //TemplatesApi.ListTemplatesOptions options = new TemplatesApi.ListTemplatesOptions();
            var options = "";
            //options.searchText = "Portfolio_1";
            //EnvelopeTemplateResults results = templatesApi.ListTemplates(accountId, options);

            string templateId;
            string resultsTemplateName;
            bool createdNewTemplate;

            EnvelopeTemplate templateReqObject = MakeTemplate(templateName);

            TemplateSummary template = templatesApi.CreateTemplate(accountId, templateReqObject);

            // Retrieve the new template Name / TemplateId
            //EnvelopeTemplateResults templateResults = templatesApi.ListTemplates(accountId, options);
            //templateId = templateResults.EnvelopeTemplates[0].TemplateId;
            //resultsTemplateName = templateResults.EnvelopeTemplates[0].Name;
            createdNewTemplate = true;


            // Use the EnvelopesApi to send the signature request!
            var envelopesApi = new EnvelopesApi();
            EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountId, envDef);

            // print the JSON response
            Console.WriteLine("EnvelopeSummary:\n{0}", JsonConvert.SerializeObject(envelopeSummary));
            Console.Read();
        }
        static private EnvelopeTemplate MakeTemplate(string resultsTemplateName)
        {
            // Data for this method
            // resultsTemplateName


            // document 1 (pdf) has tag /sn1/
            //
            // The template has two recipient roles.
            // recipient 1 - signer
            // recipient 2 - cc
            // The template will be sent first to the signer.
            // After it is signed, a copy is sent to the cc person.
            // read file from a local directory
            // The reads could raise an exception if the file is not available!
            // add the documents
            Document doc = new Document();
            string docB64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Portfolio_1.pdf"));
            doc.DocumentBase64 = docB64;
            doc.Name = "Portfolio_1.pdf"; // can be different from actual file name
            doc.FileExtension = "pdf";
            doc.DocumentId = "1";

            // create a signer recipient to sign the document, identified by name and email
            // We're setting the parameters via the object creation
            Signer signer1 = new Signer();
            signer1.RoleName = "signer";
            signer1.RecipientId = "1";
            signer1.RoutingOrder = "1";

            Signer signer2 = new Signer();
            signer2.RoleName = "signer";
            signer2.RecipientId = "2";
            signer2.RoutingOrder = "2";
            // routingOrder (lower means earlier) determines the order of deliveries
            // to the recipients. Parallel routing order is supported by using the
            // same integer as the order for two or more recipients.

            // Create fields using absolute positioning:
            SignHere signHere_1_1 = new SignHere();
            signHere_1_1.DocumentId = "1";
            signHere_1_1.PageNumber = "11";
            signHere_1_1.XPosition = "346";
            signHere_1_1.YPosition = "420";

            SignHere signHere_2_1 = new SignHere();
            signHere_2_1.DocumentId = "1";
            signHere_2_1.PageNumber = "11";
            signHere_2_1.XPosition = "346";
            signHere_2_1.YPosition = "444";

            /*
            // The SDK can't create a number tab at this time. Bug DCM-2732
            // Until it is fixed, use a text tab instead.
            //   , number = docusign.Number.constructFromObject({
            //         documentId: "1", pageNumber: "1", xPosition: "163", yPosition: "260",
            //         font: "helvetica", fontSize: "size14", tabLabel: "numbersOnly",
            //         height: "23", width: "84", required: "false"})
            Text textInsteadOfNumber = new Text();
            textInsteadOfNumber.DocumentId = "1";
            textInsteadOfNumber.PageNumber = "1";
            textInsteadOfNumber.XPosition = "153";
            textInsteadOfNumber.YPosition = "260";
            textInsteadOfNumber.Font = "helvetica";
            textInsteadOfNumber.FontSize = "size14";
            textInsteadOfNumber.TabLabel = "numbersOnly";
            textInsteadOfNumber.Height = "23";
            textInsteadOfNumber.Width = "84";
            textInsteadOfNumber.Required = "false";

            Text text = new Text();
            text.DocumentId = "1";
            text.PageNumber = "1";
            text.XPosition = "153";
            text.YPosition = "230";
            text.Font = "helvetica";
            text.FontSize = "size14";
            text.TabLabel = "text";
            text.Height = "23";
            text.Width = "84";
            text.Required = "false";
            */

            // Tabs are set per recipient / signer
            Tabs signer1Tabs = new Tabs();
            signer1Tabs.SignHereTabs = new List<SignHere> { signHere_1_1 };
            signer1.Tabs = signer1Tabs;


            Tabs signer2Tabs = new Tabs();
            signer2Tabs.SignHereTabs = new List<SignHere> { signHere_2_1 };
            signer2.Tabs = signer2Tabs;

            // Add the recipients to the env object
            Recipients recipients = new Recipients();
            recipients.Signers = new List<Signer> { signer1, signer2 };

            // create the overall template definition
            EnvelopeTemplate template = new EnvelopeTemplate();
            // The order in the docs array determines the order in the env
            //template.Description = "HF RTO contract template";
            //template.Name = resultsTemplateName;
            template.Documents = new List<Document> { doc };
            template.EmailSubject = "Παρακαλώ υπογράψτε την σύμβαση";
            template.Recipients = recipients;
            template.Status = "created";


            return template;
        }
    }
}
