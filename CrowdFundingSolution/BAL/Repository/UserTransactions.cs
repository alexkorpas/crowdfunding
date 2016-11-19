using DAL;
using System.Threading.Tasks;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public UserInfoDTO ReadUserById(int id)
        {
            var db = new backup_CrowdFundingViva1Entities();
            UserInfoDTO result = new UserInfoDTO();
            UserInfo s = db.UserInfo.Find(id);
            result = new UserInfoDTO
            {
                 Id = s.Id,
                 //Password = s.password,
                 //Firstname = s.firstname,
                 //Lastname = s.lastname,
                 //Username = s.username,
                 //Email_Primary = s.email_primary,
                 //Email_Secondary = s.email_secondary,
                 //Telephone = s.telephone,
                 //Mobile = s.mobile,
                 //About = s.about,
                 //Date_Of_Birth = s.date_of_birth,
                 //Points = s.points,
                 //Is_Active = s.is_active,
                 //Registration_Date = s.registration_date,
                 //Deletion_Date = s.deletion_date,
                 //Blocked_Date = s.blocked_date,
                 //Is_Admin = s.is_admin
            };

            return result;
        }

        public async Task<UserInfoDTO> ReadUserByName(string name)
        {
            var db = new backup_CrowdFundingViva1Entities();

            UserInfoDTO result = new UserInfoDTO();

            //var s = await (from us in db.AspNetUsers
            //              where us.FirstName == name
            //              select us).FirstOrDefault();

            //result = new UserInfoDTO
            //{
            //    User_Id = s.user_id,
            //    Password = s.password,
            //    Firstname = s.firstname,
            //    Lastname = s.lastname,
            //    Username = s.username,
            //    Email_Primary = s.email_primary,
            //    Email_Secondary = s.email_secondary,
            //    Telephone = s.telephone,
            //    Mobile = s.mobile,
            //    About = s.about,
            //    Date_Of_Birth = s.date_of_birth,
            //    Points = s.points,
            //    Is_Active = s.is_active,
            //    Registration_Date = s.registration_date,
            //    Deletion_Date = s.deletion_date,
            //    Blocked_Date = s.blocked_date,
            //    Is_Admin = s.is_admin
            //};

            return result;
        }
    }
}
