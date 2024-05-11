namespace ISPWebAPI.Models
{
    public class ClientsContracts
    {
        public int ID { get; set; }
        public int Client_ID { get; set; }
        public int Contract_ID { get; set; }
        public int IsMaster { get; set; }
        public int IsOrder { get; set; }
        public string ClientName { get; set; }
        public string ContractTitle { get; set; }
        public string Code { get; set; }
        public string Portfolio { get; set; }
    }
}
