namespace ISPWebAPI.Models
{
    public class WebUsersStates
    {
        //----- WebUsersStates data -----------------------
        public int ID { get; set; }
        public int WU_ID { get; set; } = 0;
        public int Status { get; set; } = 0;
        public string Email { get; set; } = "";
        public string Mobile { get; set; } = "";
    }
}
