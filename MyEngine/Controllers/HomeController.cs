using System.Collections.Generic;
using System.Linq;
using MyEngine.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace MyEngine.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        UserContext db = new UserContext();

        public ActionResult Index()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public JsonResult LoadCarouselIndex()
        {
            var declarations = db.Declarations.OrderByDescending(d => d.Id)
                   .Where(d => d.DeclarationType == "parent")
                   .Select(d => new
                   {
                       d.Id,
                       d.Title,
                       d.Coast
                   }).Take(8);

            var images = db.Images.Where(i => i.ImageOrder == 1).Where(i => i.ImageType == 0);

            var pictures = from img in images
                           join dec in declarations
                           on img.DeclarationId equals dec.Id
                           select new
                           {
                               dec.Id,
                               dec.Title,
                               dec.Coast,
                               img.ImagePath
                           };

            pictures = pictures.OrderByDescending(p => p.Id);

            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnderMenu()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Catalog()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult PopUpDialog()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public JsonResult LoadUnderMenu(string sectionId)
        {
            var cat = db.Categories.OrderBy(s => s.Id)
                .Where(s => s.Section.IdTitle == sectionId);

            return new JsonResult { Data = cat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult LoadCatalog(string sectionId, string categoryId)
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;

            IEnumerable<Declaration> declaration;
            IEnumerable<LikedDeclaration> liked = db.LikedDeclarations.Where(l => l.UserId == id).ToList();

            if (categoryId == "undefined")
                declaration = db.Declarations.OrderByDescending(d => d.Rating)
                   .Include(d => d.Category.Section)
                   .Where(d => d.Category.Section.IdTitle == sectionId)
                   .Where(d => d.DeclarationType == "parent").ToList();
            else
                declaration = db.Declarations.OrderByDescending(d => d.Rating)
                   .Include(d => d.Category.Section)
                   .Where(d => d.Category.Section.IdTitle == sectionId)
                   .Where(d => d.Category.IdTitle == categoryId)
                   .Where(d => d.DeclarationType == "parent").ToList();

            var newDeclaration = from d in declaration
                                 join l in liked
                                 on d equals l.Declaration into gj
                                 from sub in gj.DefaultIfEmpty()
                                 select new
                                 {
                                     d.Id,
                                     d.Title,
                                     Liked = (sub == null ? "false" : "true")
                                 };

            return Json(newDeclaration, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadCatalogImages(string sectionId, string categoryId)
        {
            var images = db.Images.Where(i => i.ImageOrder == 1).OrderByDescending(i => i.Id);
             
            IEnumerable<Declaration> declaration;

            if (categoryId == "undefined")
                declaration = db.Declarations.OrderByDescending(d => d.PublicDate)
                   .Include(d => d.Category.Section)
                   .Where(d => d.Category.Section.IdTitle == sectionId)
                   .Where(d => d.DeclarationType == "parent");
            else
                declaration = db.Declarations.OrderByDescending(d => d.PublicDate)
                   .Include(d => d.Category.Section)
                   .Where(d => d.Category.IdTitle == categoryId)
                   .Where(d => d.DeclarationType == "parent");

            //new{} - создает анонимный тип данных pictures
            var pictures = from img in images
                           join dec in declaration
                           on img.DeclarationId equals dec.Id
                           select new
                           {
                               Id = img.Id,
                               ImagePath = img.ImagePath,
                               DeclarationId = img.DeclarationId,
                               ImageType = img.ImageType
                           };

            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Declaration()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public JsonResult LoadDeclration(int id)
        {
            var decCheck = db.Declarations.Find(id);
            if (decCheck.DeclarationType == "child")
                return Json(null, JsonRequestBehavior.AllowGet);

            var related = db.RelatedDeclarations.Where(r => r.IdParent == id);

            var decl = db.Declarations;

            var declaration = from dec in decl
                              join rel in related
                              on dec.Id equals rel.IdChild
                              select new
                              {
                                  dec.Id,
                                  dec.Title,
                                  dec.Description,
                                  dec.UserId,
                                  dec.PublicDate,
                                  dec.Name,
                                  dec.Coast,
                                  dec.CategoryId,
                                  dec.Article,
                                  dec.Color,
                                  Consist = dec.Сonsist,
                                  dec.Size,
                                  dec.ExtraDescription,
                                  dec.DeclarationType
                              };

            return new JsonResult { Data = declaration, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult LoadDeclrationImages(int id)
        {
            var images = db.Images.Where(i => i.ImageOrder == 0).OrderByDescending(i => i.Id);

            var relatedParent = db.RelatedDeclarations.FirstOrDefault(r => r.IdParent == id);

            if (relatedParent == null)         
                    id = db.RelatedDeclarations.FirstOrDefault(r => r.IdChild == id).IdParent;

            var related = db.RelatedDeclarations.Where(r => r.IdParent == id);

            var pictures = from img in images
                           join rel in related
                           on img.DeclarationId equals rel.IdChild
                           select new
                           {
                               img.Id,
                               img.ImagePath,
                               img.DeclarationId,
                               img.ImageType,
                               Type = rel.IdChild == id ? "parent" : "child"
                           };

            return new JsonResult { Data = pictures, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult LoadImagePop(int id)
        {
            var images = db.Images.Where(i => i.DeclarationId == id).Where(i => i.ImageOrder == 0);

            return new JsonResult { Data = images, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult LoadCarouselData(int declarationId)
        {
            var decl = db.Declarations.FirstOrDefault(e => e.Id == declarationId).CategoryId;
            var category = db.Categories.FirstOrDefault(s => s.Id == decl).SectionId;

            var declarations = db.Declarations.OrderByDescending(d => d.Rating)
                   .Include(d => d.Category.Section)
                   .Where(d => d.Category.Section.Id == category)
                   .Where(d => d.DeclarationType == "parent")
                   .Select(d => new
                   {
                       d.Id,
                       d.Title,
                       d.Coast
                   }).Take(7);

            var images = db.Images.Where(i => i.ImageOrder == 1).Where(i => i.ImageType == 0)
                .OrderBy(i => i.Id);

            var pictures = from img in images
                           join dec in declarations
                           on img.DeclarationId equals dec.Id
                           select new
                           {
                               dec.Id,
                               dec.Title,
                               dec.Coast,
                               img.ImagePath
                           };

            return Json(pictures, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LickedClick(int idDeclaration, bool declarationLiked)
        {
            if (Request.IsAuthenticated)
            {
                Declaration dec;
                int id = 0;
                string name = HttpContext.User.Identity.Name;
                if (name != "")
                    id = db.Users.FirstOrDefault(u => u.Email == name).Id;

                int k = 0;
                if (declarationLiked == true)
                {
                    var liked = db.LikedDeclarations
                        .FirstOrDefault(l => l.DeclarationId == idDeclaration);

                    if (liked != null)
                    {
                        db.LikedDeclarations.Remove(liked);
                        db.SaveChanges();
                    }

                }
                else
                {
                    var likedCheck = db.LikedDeclarations
                        .FirstOrDefault(l => l.DeclarationId == idDeclaration);

                    if (likedCheck == null)
                    {
                        LikedDeclaration liked = new LikedDeclaration
                        {
                            UserId = id,
                            DeclarationId = idDeclaration
                        };
                        db.LikedDeclarations.Add(liked);
                        db.SaveChanges();
                    }
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}