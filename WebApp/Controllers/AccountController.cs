using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using WebApp.Models;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Net.Mail;
using System.Data.Entity;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        Models.WebContext db = new Models.WebContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user =  db.Users.FirstOrDefault(m => m.Email == model.Email);
                if (user != null && Crypto.VerifyHashedPassword(user.Password, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверные данные пользователя");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            User user = db.Users.FirstOrDefault(m => m.Email == model.Email);
            if (user == null)
            {
                db.Users.Add(new User { Name = model.Name, Email = model.Email, Password = Crypto.HashPassword(model.Password), RoleId = 1 });
                db.SaveChanges();
                user = db.Users.Where(m => m.Email == model.Email).FirstOrDefault();
                if (user != null)
                    if (Crypto.VerifyHashedPassword(user.Password, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        //MailAddress from = new MailAddress("somemail@gmail.com", "Регистрация на сайте");
                        //MailAddress to = new MailAddress(user.Email);
                        //MailMessage m = new MailMessage(from, to);
                        //m.Subject = "Подтверждение почты";
                        //m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                        //                "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                        //    Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Mail = user.Email }, Request.Url.Scheme));
                        //m.IsBodyHtml = true;
                        //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        //smtp.Credentials = new System.Net.NetworkCredential("somemail@gmail.com", "pass");
                        //smtp.Send(m);  
                        //return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                        return RedirectToAction("Index", "Home");
                    }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь уже существует");
            }
            return View(model);
        }

        //public string Confirm(string Email)
        //{
        //    return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
        //            "инструкции по завершению регистрации";
        //}

        //public ActionResult ConfirmEmail(string Token, string Email)
        //{
        //    WebContex db = new WebContex();
        //    User user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
        //    if (user != null)
        //    {
        //        if (user.Email == Email)
        //        {
        //            user.ConfirmedEmail = true;
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Confirm", "Account", new { mail = user.Email });
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Confirm", "Account", new { Email = "" });
        //    }
        //}

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Account()
        {
            return View(db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Account(User user)
        {
            var u = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            u.Name = user.Name ?? u.Name;
            u.Phone = user.Phone ?? u.Phone;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Statistics()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                if (user!=null)
                {
                    var publications = db.Publications.Include(m => m.User).Where(m => m.User.Email == User.Identity.Name);
                    var chats = db.Chats.Include(m=>m.User1).Include(m=>m.User2).Where(m => m.User1Id == user.Id || m.User2Id == user.Id);
                    ViewBag.Chats = chats.Include(m=>m.Messages);
                    ViewBag.ChatsCount = chats.Count();
                    ViewBag.UserId = user.Id;
                    return View(publications);
                }
            }
            return HttpNotFound();
        }
        public ActionResult IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnUsername()
        {
            if (User.Identity.IsAuthenticated)
                try
                {
                    string name = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault().Name;
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("UserNotFound", JsonRequestBehavior.AllowGet);
                }
            else
                return Json("Профиль", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnUserId()
        {
            if (User.Identity.IsAuthenticated)
                    try
                    {
                        return Json(db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault().Id, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json("UserNotFound", JsonRequestBehavior.AllowGet);
                    }
            else
                return Json("Guest", JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsAdmin()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    if (user.RoleId == 2)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}