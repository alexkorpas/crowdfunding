using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTO
{
    public class UserAddressDTO
    {
        public int Address_Id { get; set; }
        public int User_Id { get; set; }
        public string Friendly_Name { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Area { get; set; }
        public string Country { get; set; }
        public int Postal_Code { get; set; }
        public int Telephone { get; set; }
    }
}
