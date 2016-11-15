using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }
        public int PhotoFK { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public bool IsAdmin { get; set; }
        public int Address { get; set; }
    }
}
