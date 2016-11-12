using BAL.DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public UserDTO ReadUserById(int id)
        {
            var db = new CrowdFundingViva1Entities();
            UserDTO result = new UserDTO();
            user s = db.user.Find(id);
            result = new UserDTO
            {
                 User_Id = s.user_id,
                 Password = s.password,
                 Firstname = s.firstname,
                 Lastname = s.lastname,
                 Username = s.username,
                 Email_Primary = s.email_primary,
                 Email_Secondary = s.email_secondary,
                 Telephone = s.telephone,
                 Mobile = s.mobile,
                 About = s.about,
                 Date_Of_Birth = s.date_of_birth,
                 Points = s.points,
                 Is_Active = s.is_active,
                 Registration_Date = s.registration_date,
                 Deletion_Date = s.deletion_date,
                 Blocked_Date = s.blocked_date,
                 Is_Admin = s.is_admin
            };

            return result;
        }

        public UserDTO ReadUserByName(string name)
        {
            var db = new CrowdFundingViva1Entities();

            UserDTO result = new UserDTO();

            user s = (from us in db.user
                      where us.username == name
                      select us).FirstOrDefault();

            result = new UserDTO
            {
                User_Id = s.user_id,
                Password = s.password,
                Firstname = s.firstname,
                Lastname = s.lastname,
                Username = s.username,
                Email_Primary = s.email_primary,
                Email_Secondary = s.email_secondary,
                Telephone = s.telephone,
                Mobile = s.mobile,
                About = s.about,
                Date_Of_Birth = s.date_of_birth,
                Points = s.points,
                Is_Active = s.is_active,
                Registration_Date = s.registration_date,
                Deletion_Date = s.deletion_date,
                Blocked_Date = s.blocked_date,
                Is_Admin = s.is_admin
            };

            return result;
        }
    }
}
