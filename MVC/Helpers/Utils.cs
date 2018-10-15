using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;

namespace MVC.Helpers
{
    public static class Utils
    {
        public static List<UserModel> GetValueFromSessionOrSetIt (HttpContextBase currentContext)
        {
            List<UserModel> lstUser;
            if(currentContext.Session["myLstUser"] != null)
            {
                lstUser = (List <UserModel>) currentContext.Session["myLstUser"];
            }
            else
            {
                lstUser = new List<UserModel>();
                lstUser.Add(new UserModel() { Email = "test@test.be", Firstname = "test", Lastname = "test", Id = 1 });
                lstUser.Add(new UserModel() { Email = "test2@test2.be", Firstname = "test2", Lastname = "test2", Id = 2 });
                lstUser.Add(new UserModel() { Email = "test3@test3.be", Firstname = "test3", Lastname = "test3", Id = 3 });
                currentContext.Session["myLstUser"] = lstUser;
            }
            return lstUser;
        }
    }
}