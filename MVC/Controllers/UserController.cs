using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVC.Models;
using MVC.Helpers;

namespace MVC.Controllers
{
    public class UserController : Controller
    {

        private static int idCreate = 4;
        
        [HttpGet]
        public ActionResult Overview()
        {
            var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);
            return View(lstUser);
        }

        [HttpGet]
        public ActionResult Create(){
            UserModel user = new UserModel();
            // After creation we must check status of user.
            if (TempData["status"] != null)
            {
                // Now we can set the Status for ViewBag
                //ViewBag.Status = TempData["status"].ToString();
                // Then we remove it.
                TempData.Remove("status");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            TempData["status"] = null;
            if (ModelState.IsValid)
            {
                var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);
                user.Id = idCreate;
                lstUser.Add(user);
                idCreate++;
                HttpContext.Session["myLstUser"] = lstUser;
                // Redirection forbidden to use ViewBag => Use TempData
                TempData["status"] = "Success";
                // Redirect to Overview
                //return RedirectToAction("Overview");
            }
            return View(user);
         
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);

            if (lstUser.Exists(x => x.Id == Id))
            {
                var userToEdit = lstUser.Find(x => x.Id == Id);
                return View(userToEdit);

            }
            return View(User);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel user)
        {
            var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);
            var oldUser = lstUser.FirstOrDefault(n => n.Id == user.Id);

            if (ModelState.IsValid)
            {
                if (oldUser != null)
                {
                    oldUser.Firstname = user.Firstname;
                    oldUser.Lastname = user.Lastname;
                    oldUser.Email = user.Email;
                    return RedirectToAction("Overview");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);

            if (lstUser.Exists(x => x.Id == Id))
            {
                var userDetails = lstUser.Find(x => x.Id == Id);
                return View(userDetails);
            }
            return RedirectToAction("Overview");
        }

        [HttpGet]
        public ActionResult Delete(UserModel user)
        {
            var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);
            var removeUser = lstUser.FirstOrDefault(x => x.Id == user.Id);

            if (removeUser != null)
            {
                lstUser.Remove(removeUser);
                return RedirectToAction("Overview");
            }
            return View(user);
        }

    }

}