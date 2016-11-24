using System.Linq;
using DAL;
using System.Threading.Tasks;
using BAL.DTO;
using System;
using System.Data.Entity;

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

        public AspNetUsersDTO ReadUserByName(string email)
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                AspNetUsersDTO result = new AspNetUsersDTO();

                var s = (from us in db.AspNetUsers
                               where us.Email == email
                               select us).FirstOrDefault();

                result = new AspNetUsersDTO
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    UserName = s.UserName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber
                };
                return result;
            }
            catch(Exception ex)
            {
                return new AspNetUsersDTO();
            }
        }
        public async Task<TransactionResult> SaveUserTransaction(AspNetUsersDTO userInfoDTO, string email)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {
                    var s = await (from us in db.AspNetUsers
                             where us.Email == email
                             select us).FirstOrDefaultAsync();

                    s.FirstName = userInfoDTO.FirstName;
                    s.LastName = userInfoDTO.LastName;
                    s.UserName = userInfoDTO.UserName;
                    s.Email = userInfoDTO.Email;
                    s.PhoneNumber = userInfoDTO.PhoneNumber;

                    //AspNetUsersDTO updatedUser = new AspNetUsersDTO
                    //{
                    //    FirstName = s.FirstName,
                    //    LastName = s.LastName,
                    //    UserName = s.UserName,
                    //    Email = s.Email,
                    //    PhoneNumber = "6977777777"
                    //};
                    await db.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }
    }
}
