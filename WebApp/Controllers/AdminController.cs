using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using WebApp.Models;
using WebApp.ViewModels;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using WebApp.Util;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        Models.WebContext db = new Models.WebContext();
        const int pageSize = 8;
        public ActionResult UnconfirmedPublications(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (db.Users.Where(m => m.Email == User.Identity.Name && m.RoleId == 2).FirstOrDefault() != null)
                {
                    var publications = db.Publications.Include(p => p.User)
                        .Include(p => p.PropertyType)
                        .Include(p => p.BlockOfFlatsType)
                        .Include(p => p.BathroomType)
                        .Include(p => p.BalconyType)
                        .Include(p => p.WallMaterial);
                    publications = publications.Where(m => !m.IsApprovedByAdmin);
                    int pageNumber = page ?? 1;

                    ViewBag.PropertyTypes = new SelectList(db.PropertyTypes.ToList(), "Id", "Content");

                    return View(publications.ToList().ToPagedList(pageNumber, pageSize));
                }
            }
            return HttpNotFound();
        }
        public ActionResult Confirm(int? id, bool? value)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (db.Users.Where(m => m.Email == User.Identity.Name && m.RoleId == 2).FirstOrDefault() != null)
                {
                    if (id == null || value == null)
                        return HttpNotFound();
                    var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
                    if (publication != null)
                    {
                        if (value == true)
                        {
                            publication.IsApprovedByAdmin = true;
                            publication.IsActive = true;
                            db.Entry(publication).State = EntityState.Modified;

                        }
                        else
                        {
                            db.Publications.Remove(publication);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("UnconfirmedPublications");
                }
            }
            return HttpNotFound();
        }
    }
}