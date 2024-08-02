using Core.ViewModel;
using Databases.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Assessment.Controllers
{
    public class BaseController : Controller
    {
        private readonly DatabaseDbContext db = new DatabaseDbContext();
        public string  GV_UserID { get; set; }
        public UserInfoVM UserInfo { get; set; }
        List<Guid?> Roles = new List<Guid?>();

        public string GV_Lang
        {
            get; set;
        }
        public BaseController()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            GV_Lang = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

            if (User.Identity.IsAuthenticated)
            {
                if (GV_UserID == null)
                {
                    UserInfo = new UserInfoVM();
                    UserInfo.Lang = GV_Lang;
                    var userid = GetUserId(User);
                    var userlist = db.users.ToList();
                    GV_UserID = userid;
                    var user = db.users.Find(userid);

                    UserInfo.UserID   = Guid.Parse(GV_UserID);
                    UserInfo.UserName   =  user != null ? user.FullName : "";

                    if (user != null)
                    {
                        var obj = (from Urole in db.UserRoles
                                   join Role in db.ColumnRoles on Urole.RoleId equals Role.Id
                                   where Urole.UserId == user.Id
                                   select new
                                   {
                                       Urole.RoleId,
                                       Role.NameAr
                                   }).ToList();

                        foreach (var Role in obj)
                        {
                            Guid? ID = Guid.Parse(Role.RoleId);
                            Roles.Add(ID);
                        }

                        UserInfo.UserRoles = Roles;

                        var RoleID = db.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefault().RoleId;
                        UserInfo.RoleName = db.Roles.Find(RoleID).Name;
                        UserInfo.RoleID = Guid.Parse(RoleID);
                    }
                }
            }
        }
        private string GetUserId(ClaimsPrincipal user)
        {
            var userId = ((ClaimsIdentity)user.Identity).Claims.First().Value;
            return userId;
        }

    }
}
