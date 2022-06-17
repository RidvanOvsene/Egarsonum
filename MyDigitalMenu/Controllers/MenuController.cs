using MyDigitalMenu.Identity;
using MyDigitalMenu.Models;
using MyDigitalMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDigitalMenu.Controllers
{
    public class MenuController : Controller
    {
        private DataContext db = new DataContext();
        private IdentityDataContext dbI = new IdentityDataContext();
        private object result;

        public ActionResult Categories()
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var category = db.Categories.Where(x => x.RestorantId == RestorantId.ToString()).ToList();
            return View(category);
        }

        public ActionResult Foods(int id, string RestaurantId, int TableId)
        {
            Guid _RestorantId = new Guid(RestaurantId);
            var Result = new List<FoodViewModel>();
            var MainResult = new FoodViewMainModel();
            ViewBag.Category = db.Categories.Find(id);

            var _foods = db.Foods.Where(x => x.CategoryId == id && x.RestorantId == _RestorantId.ToString()).ToList();
            var FoodItems = db.FoodItems.Where(x => x.RestonrantId == _RestorantId.ToString()).ToList();
            foreach (var item in _foods)
            {
                var ResultItem = new FoodViewModel();
                ResultItem.CategoryId = item.CategoryId;
                ResultItem.FoodId = item.FoodId;
                ResultItem.FoodDescription = item.FoodDescription;
                ResultItem.FoodImageUrl = item.FoodImageUrl;
                ResultItem.FoodName = item.FoodName;
                ResultItem.FoodPrice = item.FoodPrice;
                ResultItem.Code = item.Code;
                ResultItem.FoodItems = FoodItems.Where(x => x.FoodId == item.FoodId).ToList(); ;
                Result.Add(ResultItem);
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
            var DinnerTable = db.DinnerTable.Where(x => x.RestorantId == _RestorantId.ToString() && x.State == true).ToList();
            var Restorants = db.Restaurants.FirstOrDefault(x => x.Id == _RestorantId);
            ViewBag.DinnerTable = DinnerTable;
            ViewBag.TotalOrder = db.Orders.Where(x => x.TableId == TableId && x.FoodCount > 0 && x.State != -1 && x.State != 4).ToList();
            ViewBag.TableId = TableId;
            ViewBag.RestaurantId = RestaurantId;
            MainResult.FoodViewModel = Result;
            MainResult.Language = Translate;
            ViewBag.Diller = Translate;
            ViewBag.IsOrder = Restorants.IsOrder;
            return View(MainResult);
        }
        public ActionResult AddOrder(int? OrderId, int TableId, string RestaurantId)
        {
            var _foods = db.Foods.FirstOrDefault(x => x.FoodId == OrderId && x.RestorantId == RestaurantId);
            int CategoryId = _foods != null ? _foods.CategoryId : 0;

            if (_foods != null)
            {
                var Orders = new Order();
                Orders.FoodId = _foods.FoodId;
                Orders.FoodName = _foods.FoodName;
                Orders.CategoryId = _foods.CategoryId;
                Orders.CreatedDate = DateTime.Now;
                Orders.UpdataDate = DateTime.Now;
                Orders.RestorantId = RestaurantId;
                Orders.TableId = TableId;
                Orders.FoodCount = 1;
                Orders.Price = _foods.FoodPrice;
                //Sate=1 ise aktif sipariş 2 ise işleme alındı 0 ise kapatatıldı
                Orders.State = 1;
                db.Orders.Add(Orders);
                db.SaveChanges();
            }
            return RedirectToAction("Foods", new { id = CategoryId, RestaurantId = RestaurantId, TableId = TableId.ToString() });
        }
        public ActionResult OrderDelete(int? id, int CategoryId, string RestaurantId)
        {
            var Order = db.Orders.FirstOrDefault(x => x.OrderId == id);
            int TableId = Order != null ? Order.TableId : 0;
            if (id != null)
            {
                var order = db.Orders.Where(m => m.OrderId == id).FirstOrDefault();
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Foods", new { id = CategoryId, RestaurantId = RestaurantId.ToString(), TableId = TableId });
        }

        public JsonResult Orderdecrease(int? id, int CategoryId, string RestaurantId)
        {
            var Order = db.Orders.FirstOrDefault(x => x.OrderId == id);
            int TableId = Order != null ? Order.TableId : 0;
            int FoodCount = 0;
            bool Control = false;
            if (id != null)
            {
                using (var context = new DataContext())
                {
                    var entity = context.Orders.Where(m => m.OrderId == id && m.FoodCount > 0).FirstOrDefault();
                    if (entity != null)
                    {
                        FoodCount = entity.FoodCount - 1;
                        entity.FoodCount = entity.FoodCount - 1;
                        context.SaveChanges();
                        Control = true;
                    }
                }
            }
            var result = new { FoodCount = FoodCount, Control = Control };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Orderincrease(int? id, int CategoryId, string RestaurantId)
        {

            var Order = db.Orders.FirstOrDefault(x => x.OrderId == id);
            int TableId = Order != null ? Order.TableId : 0;
            int FoodCount = 0;
            bool Control = false;
            if (id != null)
            {
                using (var context = new DataContext())
                {
                    var entity = context.Orders.FirstOrDefault(x => x.OrderId == id);
                    if (entity != null)
                    {
                        FoodCount = entity.FoodCount + 1;
                        entity.FoodCount = entity.FoodCount + 1;
                        context.SaveChanges();
                        Control = true;
                    }
                }
            }
            
            var result = new { FoodCount = FoodCount, Control = Control };
            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Foods", new { id = CategoryId, RestaurantId = RestaurantId.ToString(), TableId = TableId });
        }
        public ActionResult ToOrder(int? id, int CategoryId, string RestaurantId)
        {

            if (id != null)
            {
                var entity = db.Orders.Where(x => x.TableId == id && x.State == 1);
                foreach (var item in entity)
                {
                    using (var context = new DataContext())
                    {
                        var OrderItem = context.Orders.FirstOrDefault(x => x.OrderId == item.OrderId);
                        OrderItem.State = 2;
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Foods", new { id = CategoryId, RestaurantId = RestaurantId.ToString(), TableId = id });
        }
        public ActionResult EmtyBasket(int? id, int CategoryId, string RestaurantId)
        {
            if (id != null)
            {

                var entity = db.Orders.Where(x => x.TableId == id && x.State == 1);
                foreach (var item in entity)
                {
                    using (var context = new DataContext())
                    {
                        var OrderItem = context.Orders.FirstOrDefault(x => x.OrderId == item.OrderId);
                        OrderItem.State = -1;
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Foods", new { id = CategoryId, RestaurantId = RestaurantId.ToString(), TableId = id });
        }
    }
}