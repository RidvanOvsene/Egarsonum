using MyDigitalMenu.Identity;
using MyDigitalMenu.Models;
using MyDigitalMenu.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MyDigitalMenu.ViewModel;

namespace MyDigitalMenu.Controllers
{
    [AllowAnonymous]
    // [SessionExpire]
    public class CategoryController : Controller
    {
        private DataContext db = new DataContext();
        private IdentityDataContext dbI = new IdentityDataContext();
        // GET: Category
        public ActionResult Index(string id, string TableId)
        {
            var Foods = db.Foods.Where(x => x.RestorantId == id).Select(x => x.CategoryId).Distinct().ToList();
            var CategoryList = new List<Category>();
            var MainModel = new MainModel();
            var category = db.Categories.Where(x => x.RestorantId == id).OrderBy(x => x.CategorySortOrder).ToList();
            foreach (var item in Foods)
            {
                var Data = category.FirstOrDefault(x => x.CategoryId == item);
                if (Data != null)
                {
                    CategoryList.Add(Data);
                }
            }
            int CultureId = Session["CultureId"] == null ? 1 : Convert.ToInt32(Session["CultureId"]);

            var Translate = (from a in db.Translates
                             join c in db.TranslateDetails on a.Id equals c.TranslateId into lg
                             from x in lg.DefaultIfEmpty()
                             select new Language
                             {
                                 Code = a.Code,
                                 Text = x.Text,
                                 Culture = x.Culture
                             }).Where(x => x.Culture == CultureId).ToList();


            ViewBag.CultureId = CultureId.ToString();
            ViewBag.TableId = TableId;
            ViewBag.RestaurantId = id;
            MainModel.Category = CategoryList;
            MainModel.Language = Translate;
            return View(MainModel);
        }

        public ActionResult List()
        {

            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var category = db.Categories.Where(x => x.RestorantId == RestorantId.ToString()).Where(x => x.CategoryName != string.Empty).OrderBy(x => x.CategorySortOrder).ToList();
            ViewBag.RestaurantId = RestorantId.ToString();
            return View(category);
        }
        // GET: Category/Details/5
        [SessionExpire]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        [SessionExpire]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            var category = new Category();
            category = null;
            if (id != null)
            {
                category = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            }
            return View(category);
        }

        // POST: Category/Create
        [HttpPost]
        [SessionExpire]
        public ActionResult Create(Category category, FormCollection form, HttpPostedFileBase file)
        {
            try
            {
                string _userId = Session["Id"].ToString();
                var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
                Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
                //string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                //if (file != null && file.ContentLength > 0 && file.ContentType == "image/jpeg")
                //{
                //    var Category = new Category();
                //    {
                //        category.CategoryDateAded = DateTime.Now;
                //        category.ImagePath = form["ImagePath"];
                //        category.CategoryDateModified = DateTime.Now;
                //        category.UserId = Session["Id"].ToString();
                //        category.RestorantId = RestorantId.ToString();
                //    };

                //    var path = Path.Combine(Server.MapPath("~/FoodImages"), Path.GetFileName(uniqueFileName));
                //    WebImage img = new WebImage(file.InputStream);
                //    string picExt = Path.GetExtension(file.FileName);
                //    if (picExt == ".jpg")
                //    {
                //        picExt = ".png";
                //    }
                //    if (img.Width > 100)
                //    {
                //        img.Resize(100, 100);
                //    }
                //    img.Save(path, null, false);
                //}
                var Category = new Category();
                {
                    category.CategoryDateAded = DateTime.Now;
                    //category.ImagePath = form["ImagePath"];
                    category.CategoryDateModified = DateTime.Now;
                    category.UserId = Session["Id"].ToString();
                    category.RestorantId = RestorantId.ToString();
                    category.Code = category.CategoryName;
                };
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("List");

            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Category/Edit/5
        [SessionExpire]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        [SessionExpire]
        public ActionResult Edit(int Id, string CategoryName, int CategorySortOrder, FormCollection form, HttpPostedFileBase file)
        {
            try
            {

                var Category = db.Categories.FirstOrDefault(x => x.CategoryId == Id);
                if (Category != null)
                {

                    using (var context = new DataContext())
                    {
                        var _Category = context.Categories.FirstOrDefault(x => x.CategoryId == Id);
                        _Category.CategoryName = CategoryName;
                        _Category.CategorySortOrder = CategorySortOrder;
                        //string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        //if (file != null && file.ContentLength > 0 && file.ContentType == "image/jpeg")
                        //{
                        //    var path = Path.Combine(Server.MapPath("~/FoodImages"), Path.GetFileName(uniqueFileName));
                        //    WebImage img = new WebImage(file.InputStream);
                        //    string picExt = Path.GetExtension(file.FileName);
                        //    if (picExt == ".jpg")
                        //    {
                        //        picExt = ".png";
                        //    }
                        //    if (img.Width > 100)
                        //    {
                        //        img.Resize(100, 100);
                        //    }
                        //    img.Save(path, null, false);
                        //    Category.FoodDateAded = DateTime.Now;
                        //    Category.FoodDateModified = DateTime.Now;
                        //    Category.ImagePath = uniqueFileName;

                        //    _Category.ImagePath = uniqueFileName;
                        //}
                        Category.UserId = Session["Id"].ToString();
                        
                        var CategoryData = db.Categories.Add(Category);
                        context.SaveChanges();

                    }

                }
                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("List");
            }
        }
        [SessionExpire]
        public ActionResult CategoryDelete(int? id)
        {
            if (id != null)
            {
                string _userId = Session["Id"].ToString();
                var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
                Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

                var Categories = db.Categories.Where(m => m.CategoryId == id && m.RestorantId == RestorantId.ToString()).FirstOrDefault();
                if (Categories != null)
                {
                    db.Categories.Remove(Categories);
                    var test = db.SaveChanges();
                }

                var food = db.Foods.Where(m => m.CategoryId == id).ToList();
                foreach (var item in food)
                {
                    var FoodItems = db.FoodItems.Where(x => x.FoodId == item.FoodId).ToList();

                    foreach (var Fitem in FoodItems)
                    {
                        db.FoodItems.Remove(Fitem);
                        db.SaveChanges();
                    }
                    db.Foods.Remove(item);
                    db.SaveChanges();
                }


            }
            return RedirectToAction("List");
        }

        public ActionResult ChangeLanguage(int CultureId, string id, string TableId)
        {
            Session["CultureId"] = CultureId;
            return RedirectToAction("Index", new { id = id, TableId = TableId });
        }

    }
}
