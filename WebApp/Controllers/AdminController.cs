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
using SelectPdf;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        WebAppContext db = new WebAppContext();
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
                    publications = publications.Where(m => !m.IsApprovedByAdmin && !m.IsDeleted);
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
        public ActionResult Statistics()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    if (user.RoleId == 2)
                    {
                        var publications = db.Publications.Include(m => m.User);
                        ViewBag.Users = db.Users;
                        ViewBag.UsersCount = db.Users.Count();
                        return View(publications);
                    }
                }
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Statistics(int? i)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc;
            try
            {
                doc = converter.ConvertHtmlString(ClearStatisticsAsString());
            }
            catch
            {
                return HttpNotFound();
            }
            byte[] pdf = doc.Save();
            doc.Close();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Statistics.pdf";
            return fileResult;
        }
        private string ClearStatisticsAsString()
        {
            var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            var publications = db.Publications.Include(m => m.User);
            ViewBag.Users = db.Users;
            ViewBag.UsersCount = db.Users.Count();
            return ViewToString.RenderViewToString(this.ControllerContext, "~/Views/Admin/ClearStatistics.cshtml", publications);
        }
    }
}