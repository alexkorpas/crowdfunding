using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTO
{
    public class UserDTO
    {
        public int User_Id { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email_Primary { get; set; }
        public string Email_Secondary { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public int Photo_Main_Id { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public int Points { get; set; }
        public bool Is_Active { get; set; }
        public DateTime? Registration_Date { get; set; }
        public DateTime? Deletion_Date { get; set; }
        public DateTime? Blocked_Date { get; set; }
        public bool Is_Admin { get; set; }
        public int Address_Id_Main { get; set; }
    }
}
