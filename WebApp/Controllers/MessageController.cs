using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;

namespace WebApp.Controllers
{
    public class MessageController : Controller
    {
        WebContex db = new WebContex();

        public ActionResult Chat(int? id, int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || db.Users.Find(id) == null || db.Users.Find(id).Email == User.Identity.Name)
                {
                    var chats = db.Chats.Include(m => m.User1)
                        .Include(m => m.User2)
                        .Where(m => m.User1.Email == User.Identity.Name || m.User2.Email == User.Identity.Name)
                        .ToList();
                    int pageSize = 10;
                    int pageNumber = page ?? 1;

                    if (chats.Count == 0)
                        return View("NoChatsYet");
                    else
                        return View("AllChats", chats.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    Chat chat;
                    List<Message> message;
                    if (db.Chats.Where(m => m.User1.Email == User.Identity.Name && m.User2Id == id).FirstOrDefault() != null)
                    {
                        chat = db.Chats.Where(m => m.User1.Email == User.Identity.Name && m.User2Id == id)
                            .FirstOrDefault();
                        message = db.Messages.Include(m=>m.User)
                            .Where(m => m.ChatId == chat.Id)
                            .OrderBy(m=>m.Date).ToList();
                    }
                    else if (db.Chats.Where(m => m.User2.Email == User.Identity.Name && m.User1Id == id).FirstOrDefault() != null)
                    {
                        chat = db.Chats.Include(m => m.Messages)
                            .Where(m => m.User2.Email == User.Identity.Name && m.User1Id == id)
                            .FirstOrDefault();
                        message = db.Messages.Include(m => m.User)
                            .Where(m => m.ChatId == chat.Id)
                            .OrderBy(m => m.Date).ToList();
                    }
                    else
                    {
                        db.Chats.Add(new Chat { User1Id = db.Users.FirstOrDefault(m => m.Email == User.Identity.Name).Id, User2Id = (int)id });
                        db.SaveChanges();
                        chat = db.Chats.Include(m => m.Messages)
                            .Where(m => m.User1.Email == User.Identity.Name && m.User2Id == id)
                            .FirstOrDefault();
                        message = db.Messages.Include(m => m.User)
                            .Where(m => m.ChatId == chat.Id)
                            .OrderBy(m => m.Date).ToList();
                    }
                    ViewBag.User1Id = chat.User1Id;
                    ViewBag.User2Id = chat.User2Id;
                    return View(message);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}