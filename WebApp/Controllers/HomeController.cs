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
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        Models.WebContext db = new Models.WebContext();
        const int pageSize = 8;

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReturnPublications()
        {
            return Json(db.Publications.Where(m => m.IsApprovedByAdmin && m.IsActive), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnPublicationsCount()
        {
            return Json(db.Publications.Where(m => m.IsApprovedByAdmin && m.IsActive).Count(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Browse
        public ActionResult Browse(int? page)
        {
            var publications = db.Publications.Include(p => p.User)
                .Include(p => p.PropertyType)
                .Include(p => p.BlockOfFlatsType)
                .Include(p => p.BathroomType)
                .Include(p => p.BalconyType)
                .Include(p => p.WallMaterial);
            publications = publications.Where(m => m.IsApprovedByAdmin && m.IsActive && !m.IsDeleted);
            int pageNumber = page ?? 1;

            ViewBag.PropertyTypes = new SelectList(db.PropertyTypes.ToList(), "Id", "Content");

            return View(publications.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Browse(BrowseFilterModel model)
        {
            model.IsRent = !model.IsRent;
            var publications = db.Publications.Include(p => p.User)
                .Include(p => p.PropertyType)
                .Include(p => p.BlockOfFlatsType)
                .Include(p => p.BathroomType)
                .Include(p => p.BalconyType)
                .Include(p => p.WallMaterial);
            publications = publications.Where(m => m.IsApprovedByAdmin && m.IsActive);

            publications = ApplyFilters(model, publications);

            int pageNumber = 1;

            ViewBag.PropertyTypes = new SelectList(db.PropertyTypes.ToList(), "Id", "Content");

            return View(publications.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, pageSize));
        }

        private IQueryable<Publication> ApplyFilters(BrowseFilterModel model, IQueryable<Publication> publications)
        {
            publications = publications.Where(m => m.PropertyTypeId == model.PropertyType);
            switch (model.PropertyType)
            {
                case 1: return ApplyFilterForFlat(model, publications);
                case 2: return ApplyFilterForRoom(model, publications);
                case 3: return ApplyFilterForHouse(model, publications);
                case 4: return ApplyFilterForProperty(model, publications);
                default: return publications;
            }
        }
        private IQueryable<Publication> ApplyFilterForFlat(BrowseFilterModel model, IQueryable<Publication> publications)
        {
            if (model.Floor != null)
            {
                publications = publications.Where(m => m.Floor == model.Floor);
            }
            if (model.MinRoomAmount != null || model.MaxRoomAmount != null)
            {
                publications = publications.Where(m => m.RoomAmount >= (model.MinRoomAmount ?? 0));
                publications = publications.Where(m => m.RoomAmount <= (model.MaxRoomAmount ?? Int32.MaxValue));
            }
            publications = publications.Where(m => m.IsRent == model.IsRent);
            return publications;
        }
        private IQueryable<Publication> ApplyFilterForRoom(BrowseFilterModel model, IQueryable<Publication> publications)
        {
            if (model.Floor != null)
            {
                publications = publications.Where(m => m.Floor == model.Floor);
            }
            if (model.OfferingRoomAmount != null)
            {
                publications = publications.Where(m => m.OfferingRoomAmount == model.OfferingRoomAmount);
            }
            publications = publications.Where(m => m.IsPassageRoom == model.IsPassageRoom);
            publications = publications.Where(m => m.IsFurnitureExist == model.IsFurnitureExist);
            publications = publications.Where(m => m.IsOneDayRent == model.IsOneDayRent);
            return publications;
        }
        private IQueryable<Publication> ApplyFilterForHouse(BrowseFilterModel model, IQueryable<Publication> publications)
        {
            publications = publications.Where(m => m.IsRent == model.IsRent);
            if (model.MinRoomAmount != null || model.MaxRoomAmount != null)
            {
                publications = publications.Where(m => m.RoomAmount >= (model.MinRoomAmount ?? 0));
                publications = publications.Where(m => m.RoomAmount <= (model.MaxRoomAmount ?? Int32.MaxValue));
            }
            return publications;
        }
        private IQueryable<Publication> ApplyFilterForProperty(BrowseFilterModel model, IQueryable<Publication> publications)
        {
            if (model.MinPropertyArea != null || model.MaxPropertyArea != null)
            {
                publications = publications.Where(m => m.RoomAmount >= (model.MinPropertyArea ?? 0));
                publications = publications.Where(m => m.RoomAmount <= (model.MaxPropertyArea ?? Single.MaxValue));
            }
            return publications;
        }
        public ActionResult BrowseFilterFlat()
        {
            ViewBag.BlockOfFlatsTypes = new SelectList(db.BlockOfFlatsTypes.ToList(), "Id", "Content");
            ViewBag.BathroomTypes = new SelectList(db.BathroomTypes.ToList(), "Id", "Content");
            ViewBag.BalconyTypes = new SelectList(db.BalconyTypes.ToList(), "Id", "Content");

            return PartialView("BrowseFilterContent/FlatFilterProperties");
        }
        public ActionResult BrowseFilterRoom()
        {
            return PartialView("BrowseFilterContent/RoomFilterProperties");
        }
        public ActionResult BrowseFilterHouse()
        {
            ViewBag.WallMaterials = new SelectList(db.WallMaterials.ToList(), "Id", "Content");
            return PartialView("BrowseFilterContent/HouseFilterProperties");
        }
        public ActionResult BrowseFilterProperty()
        {
            return PartialView("BrowseFilterContent/PropertyFilterProperties");
        }
        public ActionResult BrowseFilterCost()
        {
            return PartialView("BrowseFilterContent/CostFilterProperties");
        }
        #endregion

        #region Publication
        public ActionResult Publication(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var publication = db.Publications.Include(m => m.User).FirstOrDefault(m => m.Id == id);
            if (publication == null)
                return HttpNotFound();
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    if (publication.UserId == user.Id || user.RoleId == 2)
                        return View("PublicationContent/Publication", publication);
                }
            }
            if (!publication.IsApprovedByAdmin || !publication.IsActive || publication.IsDeleted)
                return HttpNotFound();
            return View("PublicationContent/Publication", publication);
        }
        public ActionResult PublicationFlat(int id)
        {
            return PartialView("PublicationContent/PublicationFlat",
                db.Publications.Include(p => p.BalconyType)
                .Include(p => p.BlockOfFlatsType)
                .Include(p => p.BathroomType)
                .FirstOrDefault(p => p.Id == id));
        }
        public ActionResult PublicationRoom(int id)
        {
            return PartialView("PublicationContent/PublicationRoom", db.Publications.Find(id));
        }
        public ActionResult PublicationHouse(int id)
        {
            return PartialView("PublicationContent/PublicationHouse",
                db.Publications.Include(p => p.WallMaterial)
                .FirstOrDefault(p => p.Id == id));
        }
        public ActionResult PublicationProperty(int id)
        {
            return PartialView("PublicationContent/PublicationProperty", db.Publications.Find(id));
        }
        public ActionResult ImagesAmount(int publicationId)
        {
            int count;
            try
            {
                count = Directory.GetFiles(Server.MapPath("~/Images/Publication/" + publicationId + "/")).Count();
            }
            catch
            {
                count = 0;
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnImagePathAndNumber(int currentImage, int publicationId, bool isLeft)
        {
            int count;
            try
            {
                count = Directory.GetFiles(Server.MapPath("~/Images/Publication/" + publicationId + "/")).Count();
            }
            catch
            {
                count = 0;
            }
            object[] array;
            if (isLeft)
            {
                if (currentImage > 1)
                {
                    array = new object[] { "/Images/Publication/" + publicationId + "/" + (currentImage - 1) + ".jpg", currentImage - 1 };
                    return Json(array, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (currentImage < count)
                {
                    array = new object[] { "/Images/Publication/" + publicationId + "/" + (currentImage + 1) + ".jpg", currentImage + 1 };
                    return Json(array, JsonRequestBehavior.AllowGet);
                }
            }
            array = new object[] { "/Images/Publication/" + publicationId + "/" + currentImage + ".jpg", currentImage };
            return Json(array, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeAmountOfViews(int publicationId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault() != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            db.Publications.Where(m => m.Id == publicationId).FirstOrDefault().AmountOfPageViews += 1;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MyPublications
        public ActionResult MyPublications(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                var publications = db.Publications.Include(p => p.User)
                    .Include(p => p.PropertyType)
                    .Include(p => p.BlockOfFlatsType)
                    .Include(p => p.BathroomType)
                    .Include(p => p.BalconyType)
                    .Include(p => p.WallMaterial);
                publications = publications.Where(m => m.User.Email == User.Identity.Name && !m.IsDeleted);
                int pageNumber = page ?? 1;

                ViewBag.PropertyTypes = new SelectList(db.PropertyTypes.ToList(), "Id", "Content");

                return View("MyPublications/MyPublications", publications.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult ActivePublication(int? id, int? page)
        {
            if (id == null)
                return HttpNotFound();
            var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
            if (publication == null)
                return HttpNotFound();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            if (user.Id == publication.UserId || user.RoleId == 2)
            {
                if (publication.IsApprovedByAdmin)
                    publication.IsActive = !publication.IsActive;
                else
                    publication.IsActive = false;
                db.SaveChanges();
                return RedirectToAction("MyPublications", "Home", new { page });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult EditPublication(int? id, int? page)
        {
            if (id == null)
                return HttpNotFound();
            var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
            if (publication == null)
                return HttpNotFound();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            if (user.Id == publication.UserId || user.RoleId == 2)
            {
                return RedirectToAction("CreatePublication", "Home", new { id });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult DeletePublication(int? id, bool? isSelled, int? page)
        {
            if (id == null)
                return HttpNotFound();
            var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
            if (publication == null)
                return HttpNotFound();
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            if (user.Id == publication.UserId || user.RoleId == 2)
            {
                publication.IsDeleted = true;
                publication.IsActive = false;
                publication.IsSelled = isSelled ?? false;

                Task.Run(() => Directory.Delete(Server.MapPath("~/Images/Publication/" + publication.Id + "/"), true));
                db.SaveChanges();
                return RedirectToAction("MyPublications", "Home", new { page });
            }
            else
            {
                return HttpNotFound();
            }
        }
        #endregion

        #region CreatePublication
        public ActionResult CreatePublication(int? id)
        {
            ViewBag.PropertyTypes = new SelectList(db.PropertyTypes.ToList(), "Id", "Content");
            if (id == null)
            {
                return View("CreatePublicationContent/CreatePublication",
                    new CreatePublicationModel { UserId = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault().Id });
            }
            else
            {
                return View("CreatePublicationContent/CreatePublication",
                    new CreatePublicationModel(db.Publications.Where(m => m.Id == id).FirstOrDefault()));
            }
        }
        [HttpPost]
        public ActionResult CreatePublication(CreatePublicationModel publicationView)
        {
            Publication publication = new Publication(publicationView);
            if (db.Publications.Where(m => m.Id == publication.Id).FirstOrDefault() == null)
            {
                publication = db.Publications.Add(publication);
                db.SaveChanges();
            }
            else
            {
                publication.IsApprovedByAdmin = false;
                publication.IsActive = false;
                db.Entry(publication).State = EntityState.Modified;
                db.SaveChanges();
            }
            int count;
            try
            {
                count = Directory.EnumerateFiles(Server.MapPath("~/Images/Publication/" + publication.Id + "/")).Count() + 1;
            }
            catch
            {
                count = 1;
            }
            foreach (var file in publicationView.Files)
            {
                if (file != null)
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(Server.MapPath("~/Images/Publication/" + publication.Id + "/" + count + ".jpg"), ImageTransformation.Transform(file));
                    }
                    catch (IOException e)
                    {
                        Directory.CreateDirectory(Server.MapPath("~") + "/Images/Publication/" + publication.Id);
                        System.IO.File.WriteAllBytes(Server.MapPath("~/Images/Publication/" + publication.Id + "/" + count + ".jpg"), ImageTransformation.Transform(file));
                    }
                    count++;
                }
            }
            return RedirectToAction("MyPublications");
        }
        public ActionResult ContentForFlat(int? id)
        {
            ViewBag.BlockOfFlatsTypes = new SelectList(db.BlockOfFlatsTypes.ToList(), "Id", "Content");
            ViewBag.BathroomTypes = new SelectList(db.BathroomTypes.ToList(), "Id", "Content");
            ViewBag.BalconyTypes = new SelectList(db.BalconyTypes.ToList(), "Id", "Content");
            if (id != null)
            {
                var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
                if (publication != null)
                    return PartialView("CreatePublicationContent/ContentForFlat", new CreatePublicationModel(publication));
            }
            return PartialView("CreatePublicationContent/ContentForFlat");
        }
        public ActionResult ContentForRoom(int? id)
        {
            if (id != null)
            {
                var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
                if (publication != null)
                    return PartialView("CreatePublicationContent/ContentForRoom", new CreatePublicationModel(publication));
            }
            return PartialView("CreatePublicationContent/ContentForRoom");
        }
        public ActionResult ContentForHouse(int? id)
        {
            ViewBag.WallMaterials = new SelectList(db.WallMaterials.ToList(), "Id", "Content");
            if (id != null)
            {
                var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
                if (publication != null)
                    return PartialView("CreatePublicationContent/ContentForHouse", new CreatePublicationModel(publication));
            }
            return PartialView("CreatePublicationContent/ContentForHouse");
        }
        public ActionResult ContentForProperty(int? id)
        {
            ViewBag.BlockOfFlatsTypes = new SelectList(db.BlockOfFlatsTypes.ToList(), "Id", "Content");
            ViewBag.BathroomTypes = new SelectList(db.BathroomTypes.ToList(), "Id", "Content");
            ViewBag.BalconyTypes = new SelectList(db.BalconyTypes.ToList(), "Id", "Content");
            if (id != null)
            {
                var publication = db.Publications.Where(m => m.Id == id).FirstOrDefault();
                if (publication != null)
                    return PartialView("CreatePublicationContent/ContentForProperty", new CreatePublicationModel(publication));
            }
            return PartialView("CreatePublicationContent/ContentForProperty");
        }
        #endregion

    }
}