using MyDigitalMenu.Identity;
using MyDigitalMenu.Models;
using MyDigitalMenu.Session;
using MyDigitalMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyDigitalMenu.Controllers
{
    [AllowAnonymous]
    [SessionExpire]
    public class FoodController : Controller
    {
        private DataContext db = new DataContext();
        private IdentityDataContext dbI = new IdentityDataContext();
        // GET: Food
        public ActionResult Index()
        {
            string _userId = Session["Id"].ToString();
            var Result = new List<FoodViewModel>();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            var Restorant = db.Restaurants.FirstOrDefault(x => x.Id == RestorantId);
            var _foods = db.Foods.Where(x => x.UserId == _userId).ToList();
            var FoodItems = db.FoodItems.Where(x => x.UserId == _userId).ToList();
            foreach (var item in _foods)
            {
                string ItemTex = "";
                var ResultItem = new FoodViewModel();
                ResultItem.CategoryId = item.CategoryId;
                ResultItem.FoodId = item.FoodId;
                ResultItem.FoodDescription = item.FoodDescription;
                ResultItem.FoodImageUrl = item.FoodImageUrl;
                ResultItem.FoodName = item.FoodName;
                ResultItem.FoodPrice = item.FoodPrice;
                var _FoodItems = FoodItems.Where(x => x.FoodId == item.FoodId).ToList();
                foreach (var FoodItem in _FoodItems)
                {
                    ItemTex = ItemTex + FoodItem.Text + ", ";
                }
                ResultItem.FoodItemText = ItemTex;
                Result.Add(ResultItem);
            }
            return View(Result);
        }

        // GET: Food/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Food/Create
        public ActionResult Create(int? Id)
        {
            string _userId = Session["Id"].ToString();
            var Result = new List<FoodViewModel>();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            var Food = new Food();
            Food = null;
            ViewBag.GetData = new SelectList(db.Categories.Where(x => x.RestorantId == RestorantId.ToString()).ToList(), "CategoryId", "CategoryName");
            ViewBag.category = db.Categories.Where(x => x.RestorantId == RestorantId.ToString()).ToList();
            if (Id != null)
            {
                Food = db.Foods.FirstOrDefault(x => x.FoodId == Id);
            }
            ViewBag.Foods = Food;
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        //public ActionResult Create([Bind(Include = "FoodId,FoodName,FoodPrice,FoodImageUrl,FoodSortOrder,FoodIngredients1,FoodIngredients2,FoodIngredients3,FoodDescription,CategoryName,CategoryId")] Food food, HttpPostedFileBase file)
        public ActionResult Create(FormCollection form, HttpPostedFileBase file)
        {
            try
            {
                string _userId = Session["Id"].ToString();
                var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
                Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
           
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                if (file != null && file.ContentLength > 0 && file.ContentType == "image/jpeg")
                {

                    var Food = new Food
                    {
                        FoodName = form["FoodName"],
                        Code = form["FoodName"],
                        FoodPrice = double.Parse(form["FoodPrice"]),
                        FoodImageUrl = form["FoodImageUrl"],
                        FoodSortOrder = int.Parse(form["FoodSortOrder"]),
                        FoodDescription = form["FoodDescription"],
                        CategoryId = int.Parse(form["CategoryId"]),
                        RestorantId = RestorantId.ToString(),
                    };

                    int member = int.Parse(form["member"]);
                    var path = Path.Combine(Server.MapPath("~/FoodImages"), Path.GetFileName(uniqueFileName));
                    WebImage img = new WebImage(file.InputStream);
                    string picExt = Path.GetExtension(file.FileName);
                    if (picExt == ".jpg")
                    {
                        picExt = ".png";
                    }
                    if (img.Width > 100)
                    {
                        img.Resize(100, 100);
                    }
                    img.Save(path, null, false);
                    Food.FoodDateAded = DateTime.Now;
                    Food.FoodDateModified = DateTime.Now;
                    Food.FoodImageUrl = uniqueFileName;
                    Food.UserId = Session["Id"].ToString();
                    var FoodData = db.Foods.Add(Food);
                    db.SaveChanges();
                    for (int i = 0; i < member; i++)
                    {
                        //var FoodItems = new List<FoodItems>();
                        var FoodItem = new FoodItems();
                        FoodItem.CreatedDate = DateTime.Now;
                        FoodItem.ModifiedDate = DateTime.Now;
                        FoodItem.FoodId = FoodData.FoodId;
                        FoodItem.Text = form["member" + i];
                        FoodItem.UserId = FoodData.UserId;
                        FoodItem.RestonrantId = FoodData.RestorantId;
                        db.FoodItems.Add(FoodItem);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Food/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, string FoodName, double FoodPrice, int FoodSortOrder, string FoodDescription, FormCollection form, HttpPostedFileBase file)
        {
            try
            {

                var Food = db.Foods.FirstOrDefault(x => x.FoodId == Id);
                if (Food != null)
                {

                    using (var context = new DataContext())
                    {
                        var _Food = context.Foods.FirstOrDefault(x => x.FoodId == Id);
                        _Food.FoodName = FoodName;
                        _Food.FoodSortOrder = FoodSortOrder;
                        _Food.FoodPrice = FoodPrice;
                        _Food.FoodDescription = FoodDescription;

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        if (file != null && file.ContentLength > 0 && file.ContentType == "image/jpeg")
                        {
                           
                            var path = Path.Combine(Server.MapPath("~/FoodImages"), Path.GetFileName(uniqueFileName));
                            WebImage img = new WebImage(file.InputStream);
                            string picExt = Path.GetExtension(file.FileName);
                            if (picExt == ".jpg")
                            {
                                picExt = ".png";
                            }
                            if (img.Width > 100)
                            {
                                img.Resize(100, 100);
                            }
                            img.Save(path, null, false);
                            Food.FoodDateAded = DateTime.Now;
                            Food.FoodDateModified = DateTime.Now;
                            Food.FoodImageUrl = uniqueFileName;
                            Food.UserId = Session["Id"].ToString();
                            var FoodData = db.Foods.Add(Food);
                        }
                        _Food.FoodImageUrl = uniqueFileName;
                        context.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch

            {
                return View();
            }
            return View();
        }

        // GET: Food/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var food = db.Foods.Where(m => m.FoodId == id).FirstOrDefault();
                if (food != null)
                {
                    db.Foods.Remove(food);
                    var test = db.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }

        // POST: Food/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult addBasket(int id)
        {
            return View();
        }


    }
}


