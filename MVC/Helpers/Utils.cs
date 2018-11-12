using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;

namespace MVC.Helpers
{
    public static class Utils
    {
        public static Random random = new Random();

        public static List<UserModel> GetValueFromSessionOrSetIt (HttpContextBase currentContext)
        {
            List<UserModel> lstUser;
            if(currentContext.Session["myLstUser"] != null)
            {
                lstUser = (List <UserModel>) currentContext.Session["myLstUser"];
                return lstUser;
            }

            lstUser = new List<UserModel>();
            for (var i = 1000; i >= 0; i--)
            {
                lstUser.Add(new UserModel()
                {
                    Email = $"{GetRandomString(18)}@ephec.be",
                    Firstname = $"{GetRandomString(10)}",
                    Lastname = $"{GetRandomString(8)}",
                    Id = i
                });
            }

            currentContext.Session["myLstUser"] = lstUser;

            return lstUser;
        }

        internal static string GetRandomString(int length)
        {
            var value = "";
            for (var i = 0;i < length; i++)
            {
                var let = (char)('a' + random.Next(0, 26));
                value += let;
            }

            return value;
        }

        internal static string[][] GetUserDatatablesContent(List<UserModel> lstOverview, string editLink)
        {
            if (lstOverview == null || !lstOverview.Any())
            {
                return (from i in new List<UserModel>()
                        select new string[]
                        {
                        }).ToArray();
            }

            var resultToReturn = (from i in lstOverview
                                  select new string[]
                                  {
                    i.Id.ToString(),
                    i.Firstname,
                    i.Lastname,
                    $"<a href='mailto:{i.Email}'>{i.Email}</a>",
                    $"<div><a href='{editLink + "/" + i.Id}' class='btn btn-primary'>Edit</a></div>"
                                  }).ToArray();

            return resultToReturn;
        }
    }
}