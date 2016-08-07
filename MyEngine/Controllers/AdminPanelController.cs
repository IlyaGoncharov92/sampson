using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MyEngine.Models;
using System.Data.Entity;
using ImageResizer;

namespace MyEngine.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class AdminPanelController : Controller
    {
        UserContext db = new UserContext();

        public ActionResult IndexAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAllUsers()
        {
            var us = db.Users.OrderByDescending(u => u.CreationDate).Include(p => p.Role);
            return View(us);
        }

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
                return HttpNotFound();

            User user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return HttpNotFound();

            User user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound();
            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("ViewAllUsers");
        }

        [HttpGet]
        public ActionResult ViewUser(int? id)
        {
            if (id == null)
                return HttpNotFound();

            User user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound();

            user.Declarations = db.Declarations.Where(m => m.UserId == user.Id).Include(r => r.User);

            return View(user);
        }

        public ActionResult ViewAllDeclarations(int sort = 1)
        {
            IQueryable<Declaration> declaration = db.Declarations;

            if (sort == 0)
            {
                declaration = db.Declarations.OrderByDescending(d => d.PublicDate)
                .Where(d => d.DeclarationType == "parent")
                .Include(u => u.User);
            }

            if(sort == 1)
            {
                declaration = db.Declarations.OrderByDescending(d => d.Rating)
                .Where(d => d.DeclarationType == "parent")
                .Include(u => u.User);
            }
            

            ViewBag.Images = db.Images.Where(i => i.ImageOrder == 1).Where(i => i.ImageType == 0);

            return View(declaration);
        }

        [HttpPost]
        public ActionResult AddRate()
        {
            int declarationId = int.Parse(Request.Form["id"]);
            int rate = int.Parse(Request.Form["rate"]);

            var relateds = db.RelatedDeclarations.Where(r => r.IdParent == declarationId);

            foreach (var related in relateds)
            {
                Declaration declaration = db.Declarations.Find(related.IdChild);

                declaration.Rating = rate;
                db.SaveChanges();
            }
            return RedirectToAction("ViewAllDeclarations");
        }

        public ActionResult NewDeclaration(int sort = 0, string searh = null)
        {
            IQueryable<Declaration> declaration = db.Declarations;

            if (searh == null)
            {
                if (sort == 0)
                {
                    declaration = db.Declarations.OrderByDescending(d => d.PublicDate).Include(u => u.User);
                }

                if (sort == 1)
                {
                    declaration = db.Declarations.OrderByDescending(d => d.Rating).Include(u => u.User);
                }
            }
            else
            {
                declaration = db.Declarations.Where(d => d.Description == searh).Include(u => u.User);
            }

            @ViewBag.img = db.Images.Where(i => i.ImageOrder == 0).Where(i => i.ImageType == 0);
            @ViewBag.related = db.RelatedDeclarations;

            return View(declaration);
        }

        public ActionResult AddDeclaration(string type, int parentId = 0)
        {
            if(parentId == 0)
            {
                var sectionIndex = db.Sections.FirstOrDefault().Id;
                SelectList sections = new SelectList(db.Sections, "Id", "Name", sectionIndex);
                ViewBag.Sections = sections;

                SelectList categories = new SelectList(db.Categories.Where(c => c.SectionId == sectionIndex), "Id", "Name");
                ViewBag.Categories = categories;
            }
            else
            {
                var declaration = db.Declarations.Find(parentId);

                var categoryIndex = db.Categories.FirstOrDefault(c => c.Id == declaration.CategoryId);

                SelectList sections = new SelectList(db.Sections, "Id", "Name", categoryIndex.SectionId);
                ViewBag.Sections = sections;

                SelectList categories = new SelectList(db.Categories, "Id", "Name", declaration.CategoryId);
                ViewBag.Categories = categories;

                ViewBag.selectBlock = "block";
            }
            
            ViewBag.declarationType = type;
            ViewBag.parentId = parentId;

            return View();
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Categories.Where(c => c.SectionId == id).ToList());
        }

        [HttpPost]
        public ActionResult AddDeclaration(Declaration declaration, IEnumerable<HttpPostedFileBase> uploads)
        {
            var selectedIndex = db.Sections.FirstOrDefault().Id;
            SelectList sections = new SelectList(db.Sections, "Id", "Name", selectedIndex);
            ViewBag.Sections = sections;

            SelectList categories = new SelectList(db.Categories.Where(c => c.SectionId == selectedIndex), "Id", "Name");
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                User user = db.Users.Where(e => e.Email == HttpContext.User.Identity.Name).FirstOrDefault();
                declaration.UserId = user.Id;

                declaration.PublicDate = DateTime.Now;

                db.Declarations.Add(declaration);
                db.SaveChanges();

                int i = 0;
                foreach (var file in uploads)
                {
                    if (file != null)
                    {
                        DateTime current = DateTime.Now;
                        string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        var path = Server.MapPath("~/Files/");
                        string pathDate = current.ToString("dd/MM/yyyy_H:mm:ss").Replace(":", "_")
                                .Replace("/", ".") + "-" + i;

                        for (int j = 0; j < 2; j++)
                        {
                            string pathDateNew = null;
                            switch (j)
                            {
                                case 0:
                                    pathDateNew = pathDate + "-" + j + ext;
                                    file.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                    ImageBuilder.Current.Build(
                                        new ImageJob(
                                            file.InputStream,
                                            path + pathDateNew,
                                            new Instructions("maxwidth=750&maxheight=1000"),
                                            false,
                                            false));
                                    Image image = new Image
                                    {
                                        ImagePath = pathDateNew,
                                        DeclarationId = declaration.Id,
                                        ImageOrder = j,
                                        ImageType = i
                                    };
                                    db.Images.Add(image);
                                    db.SaveChanges();
                                    break;
                                case 1:
                                    pathDateNew = pathDate + "-" + j + ext;
                                    file.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                                    ImageBuilder.Current.Build(
                                        new ImageJob(
                                            file.InputStream,
                                            path + pathDateNew,
                                            new Instructions("maxwidth=225&maxheight=300"),
                                            false,
                                            false));
                                    Image image1 = new Image
                                    {
                                        ImagePath = pathDateNew,
                                        DeclarationId = declaration.Id,
                                        ImageOrder = j,
                                        ImageType = i
                                    };
                                    db.Images.Add(image1);
                                    db.SaveChanges();
                                    break;
                            }
                        }
                        i++;
                    }
                }

                int parentId = int.Parse(Request.Form["parentId"]);

                if (parentId == 0)
                    parentId = declaration.Id;

                RelatedDeclaration related = new RelatedDeclaration
                {
                    IdParent = parentId,
                    IdChild = declaration.Id
                };
                db.RelatedDeclarations.Add(related);
                db.SaveChanges();

                return RedirectToAction("NewDeclaration");
            }
            return View();
        }

        public ActionResult DeleteDeclaration(int? id, string type)
        {
            if (id == null)
                return HttpNotFound();

            if (type == "child")
            {
                Declaration declaration = db.Declarations.Find(id);
                db.Declarations.Remove(declaration);

                RelatedDeclaration related = db.RelatedDeclarations.FirstOrDefault(r => r.IdChild == id);
                db.RelatedDeclarations.Remove(related);

                var img = db.Images.Where(i => i.DeclarationId == id);
                foreach (var i in img)
                {
                    string fullPath = Request.MapPath("~/Files/" + i.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    db.Images.Remove(i);
                }
                db.SaveChanges();
            }
            if (type == "parent")
            {
                var relateds = db.RelatedDeclarations.Where(r => r.IdParent == id);

                foreach (var related in relateds)
                {
                    Declaration dec = db.Declarations.Find(related.IdChild);

                    var img = db.Images.Where(i => i.DeclarationId == dec.Id);
                    foreach (var i in img)
                    {
                        string fullPath = Request.MapPath("~/Files/" + i.ImagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        db.Images.Remove(i);
                    }
                        
                    db.Declarations.Remove(dec);
                }

                foreach (var r in relateds)
                    db.RelatedDeclarations.Remove(r);

                db.SaveChanges();
            }
            if (type == "all")
            {
                var img = db.Images;
                foreach (var i in img)
                {
                    string fullPath = Request.MapPath("~/Files/" + i.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    db.Images.Remove(i);
                }

                db.Images.RemoveRange(db.Images);
                db.RelatedDeclarations.RemoveRange(db.RelatedDeclarations);
                db.Declarations.RemoveRange(db.Declarations);
                db.LikedDeclarations.RemoveRange(db.LikedDeclarations);
                db.SaveChanges();
            }

            return RedirectToAction("NewDeclaration");
        }

        public ActionResult NewGeneral(int? id)
        {
            RelatedDeclaration relatedId = db.RelatedDeclarations.FirstOrDefault(r => r.IdChild == id);

            int idOldParent = relatedId.IdParent;

            IEnumerable<RelatedDeclaration> relateds = db.RelatedDeclarations
                .Where(r => r.IdParent == relatedId.IdParent)
                .AsEnumerable()
                .Select(r =>
                {
                    r.IdParent = relatedId.IdChild;
                    return r;
                });

            foreach (RelatedDeclaration rel in relateds)
                db.Entry(rel).State = EntityState.Modified;

            var decNewParent = db.Declarations.Find(id);
            decNewParent.DeclarationType = "parent";

            var decOldParent = db.Declarations.Find(idOldParent);
            decOldParent.DeclarationType = "child";

            db.SaveChanges();

            return RedirectToAction("NewDeclaration");
        }
    }
}

