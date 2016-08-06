using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEngine.Models;
using System.Web.Security;
using ImageResizer;

namespace MyEngine.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        UserContext db = new UserContext();
        MyСonverter myConv = new MyСonverter();

        public ActionResult Index()
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if(name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;
            
            int b;
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult General()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Messages()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult Liked()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public JsonResult LikedDeclaration()
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;

            IEnumerable<LikedDeclaration> liked = db.LikedDeclarations.Where(l => l.UserId == id);
            
            if(liked.Count() != 0)
            {
                IEnumerable<Declaration> declaration;

                declaration = db.Declarations.OrderByDescending(d => d.PublicDate)
                       .Where(d => d.DeclarationType == "parent");

                var newDeclaration = from dec in declaration
                                     join l in liked
                                     on dec.Id equals l.DeclarationId
                                     select new
                                     {
                                         dec.Id,
                                         dec.Title
                                     };

                return Json(newDeclaration, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);  
        }

        public JsonResult LikedDeclarationImages()
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;

            var liked = db.LikedDeclarations.Where(l => l.UserId == id);
            var images = db.Images.Where(i => i.ImageOrder == 1).OrderByDescending(i => i.Id);

            var newImages = from img in images
                            join d in liked
                            on img.DeclarationId equals d.DeclarationId
                            select new
                            {
                                img.Id,
                                img.ImagePath,
                                img.DeclarationId,
                                img.ImageType
                            };

            return Json(newImages, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteLikedDeclaration(int idDeclaration)
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;

            var liked = db.LikedDeclarations.Where(l => l.UserId == id)
                .FirstOrDefault(l => l.DeclarationId == idDeclaration);

            db.LikedDeclarations.Remove(liked);
            db.SaveChanges();
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Config()
        {
            if (this.Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public JsonResult LoadUserImages()
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;

            var filePath = db.ManagePhotoUsers.Where(i => i.UserId == id)
                .Select(i => new { ImagePath = i.ImagePath });

            return Json(filePath, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            int id = 0;
            string name = HttpContext.User.Identity.Name;
            if (name != "")
                id = db.Users.FirstOrDefault(u => u.Email == name).Id;
            string fileName = "";
            
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    DateTime current = DateTime.Now;
                    // получаем имя файла
                    fileName = System.IO.Path.GetFileName(upload.FileName);
                    fileName = fileName.Substring(fileName.LastIndexOf('.'));
                    fileName = id + "-" + current.ToString("dd/MM/yyyy_H:mm").Replace(":", "_")
                        .Replace("/", ".") + fileName;

                    var path = Server.MapPath("~/Files/PhotoUsers/");
                    upload.InputStream.Seek(0, System.IO.SeekOrigin.Begin);

                    ImageBuilder.Current.Build(
                        new ImageJob(
                            upload.InputStream,
                            path + fileName,
                            new Instructions("maxwidth=150&maxheight=150"),
                            false,
                            false));

                    ManagePhotoUser img = new ManagePhotoUser
                    {
                        ImagePath = fileName,
                        UserId = id
                    };
                    db.ManagePhotoUsers.Add(img);
                    db.SaveChanges();
                }
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
    }
}
