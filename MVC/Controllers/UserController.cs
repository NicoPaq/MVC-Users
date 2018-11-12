using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVC.Models;
using MVC.Helpers;
using MVC.Grids;

namespace MVC.Controllers
{
    public class UserController : Controller
    {

        private static int idCreate = 4;
        
        [HttpGet]
        public ActionResult Overview()
        {
            //var lstUser = Utils.GetValueFromSessionOrSetIt(HttpContext);
            //return View(lstUser);
            return View(new List<UserModel>());

        }

        public JsonResult GetOverview(GridDefaultSettings grid)
        {
            try
            {
                var lst = Utils.GetValueFromSessionOrSetIt(HttpContext).ToList();
                if (!string.IsNullOrEmpty(grid.Search))
                    lst = lst.FindAll(x => x.Firstname.Contains(grid.Search) || x.Lastname.Contains(grid.Search) || x.Email.Contains(grid.Search)).ToList();
                lst = Order(grid.SortColumn, grid.SortOrder, lst).ToList();

                var totalRecords = lst.Count;
                var totalPages = (int)Math.Ceiling(totalRecords / (float)grid.Length);
                lst = lst.Skip(grid.Start).Take(grid.Length).ToList();
                var jsonData = new
                {
                    draw = grid.Draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    start = grid.Start,
                    length = totalPages,
                    data = Utils.GetUserDatatablesContent(lst, Url.Action("Edit", "User"))
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
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

        internal List<UserModel> Order(int column, string order, List<UserModel> lst)
        {
            switch (column)
            {
                case 0:
                    return order != "desc"
                        ? lst.OrderBy(x => x.Id).ToList()
                        : lst.OrderByDescending(x => x.Id).ToList();
                case 1:
                    return order != "desc"
                        ? lst.OrderBy(x => x.Firstname).ToList()
                        : lst.OrderByDescending(x => x.Firstname).ToList();
                case 2:
                    return order != "desc"
                        ? lst.OrderBy(x => x.Lastname).ToList()
                        : lst.OrderByDescending(x => x.Lastname).ToList();
                case 3:
                    return order != "desc"
                        ? lst.OrderBy(x => x.Email).ToList()
                        : lst.OrderByDescending(x => x.Email).ToList();
                default:
                    return lst;
            }
        }

    }

}