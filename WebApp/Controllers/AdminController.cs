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
        WebContex db = new WebContex();
        const int pageSize = 8;
        public ActionResult UnconfirmedPublications(int? page)
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

            return View(publications.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}