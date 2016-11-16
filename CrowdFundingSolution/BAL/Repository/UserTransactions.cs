using DAL;

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

        //public UserDTO ReadUserByName(string name)
        //{
        //    var db = new Entities();

        //    UserDTO result = new UserDTO();

        //    UserInfo s = (from us in db.UserInfo
        //                  where us.username == name
        //              select us).FirstOrDefault();

        //    result = new UserDTO
        //    {
        //        User_Id = s.user_id,
        //        Password = s.password,
        //        Firstname = s.firstname,
        //        Lastname = s.lastname,
        //        Username = s.username,
        //        Email_Primary = s.email_primary,
        //        Email_Secondary = s.email_secondary,
        //        Telephone = s.telephone,
        //        Mobile = s.mobile,
        //        About = s.about,
        //        Date_Of_Birth = s.date_of_birth,
        //        Points = s.points,
        //        Is_Active = s.is_active,
        //        Registration_Date = s.registration_date,
        //        Deletion_Date = s.deletion_date,
        //        Blocked_Date = s.blocked_date,
        //        Is_Admin = s.is_admin
        //    };

        //    return result;
        //}
    }
}
