using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
        protected user LoggedUser = null;

        public BaseController()
        {
            //var identity = User.Identity as ClaimsIdentity;
            //LoggedUser = i2s.iMaintSuite.Service.Helpers.DALUserHelper.findUserByName(identity.Name, _UoW);
            //vsimenglite.LoggedUserDetails.LoggedUser = LoggedUser;
            //globalCriteria = null;
            ////  LoginResult logResult = LoginHelper.FindUserForLogin(false, "Admin", "i2s", false, _UoW);
            ////   LoggedUser = logResult.LoggedUserObject;
            //transactions = new i2s.iMaintSuite.Service.Repository.Transactions(LoggedUser);
        }
    }
}