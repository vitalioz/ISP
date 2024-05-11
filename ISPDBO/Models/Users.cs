using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class Users
    {
        public int ID { get; set; }                                            // WebUser.ID
        [Required]
        public int Client_ID { get; set; }                                     // Client_ID
        [Required]
        public int Category { get; set; }
        [Required]
        public string Category_Title { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Fathername { get; set; }
        [Required]
        public string ADT { get; set; }
        [Required]
        public string Passport { get; set; }
        [Required]
        public string AFM { get; set; }
        [Required]
        public string AMKA { get; set; }
        [Required]
        public string Tel{ get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string LogAxion { get; set; }
        [Required]
        public string Merida { get; set; }
        [Required]
        public int Brunch_ID { get; set; }
        [Required]
        public string Brunch_Title { get; set; }
        [Required]
        public int Spec_ID { get; set; }
        [Required]
        public string Spec_Title { get; set; }
        [Required]
        public string DoB { get; set; }
        [Required]
        public int FamilyStatus { get; set; }
        [Required]
        public string Family_Title { get; set; }
        [Required]
        public string Folder { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
