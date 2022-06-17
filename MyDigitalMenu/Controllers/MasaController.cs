using Microsoft.Owin.Security.Cookies;
using MyDigitalMenu.Identity;
using MyDigitalMenu.Models;
using MyDigitalMenu.Session;
using MyDigitalMenu.ViewModel;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyDigitalMenu.Controllers
{
    [AllowAnonymous]
    [SessionExpire]
    public class MasaController : Controller
    {
        private DataContext db = new DataContext();
        private IdentityDataContext dbI = new IdentityDataContext();
        private object httpContex;

        // GET: Section
        public ActionResult Section(int? id)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var Section = new Section();

            if (id != null)
            {
                Section = db.Sections.Where(x => x.RestaurantId == RestorantId.ToString() && x.Id == id).FirstOrDefault();
                return View(Section);

            }
            //Section = db.Sections.Where(x => x.RestaurantId == RestorantId.ToString() && x.Id == id).FirstOrDefault();
            return View();
            //return View(Section);
        }
        public ActionResult CreateSection(string Adi, Section section)
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            var Data = new Section();
            Data.Name = Adi;
            Data.CreatedDate = DateTime.Now;
            Data.ModifiedDate = DateTime.Now;
            Data.Code = Adi;
            Data.UserId = Session["Id"].ToString();
            Data.RestaurantId = RestorantId.ToString();
            db.Sections.Add(Data);
            //db.Sections.Add(section);
            db.SaveChanges();
            return RedirectToAction("Section");
        }

        public ActionResult SectionList()
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            //var OrderStatus = GetTableStatus(RestorantId.ToString());
            var Sections = new List<Section>();
            Sections = db.Sections.Where(x => x.RestaurantId == RestorantId.ToString()).ToList();
            return View(Sections);

        }
        public ActionResult SectionDelete(int? id)
        {
            if (id != null)
            {
                var _Sections = db.Sections.Where(m => m.Id == id).FirstOrDefault();
                if (_Sections != null)
                {
                    db.Sections.Remove(_Sections);
                    var test = db.SaveChanges();
                }
                //Bölgedeki masaların silinmesi için yazıldı.
                var _table = db.DinnerTable.Where(m => m.SectionId == id).ToList();
                foreach (var item in _table)
                {
                    var _Tables = db.DinnerTable.Where(x => x.SectionId == id).ToList();

                    foreach (var table in _Tables)
                    {
                        db.DinnerTable.Remove(table);
                        db.SaveChanges();
                    }
                    //db.Foods.Remove(item);
                    //db.SaveChanges();
                }


            }
            return RedirectToAction("SectionList");
        }

        // GET: Masa
        public ActionResult Index()
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var Masalar = db.DinnerTable.Where(x => x.RestorantId == RestorantId.ToString()).ToList();
            // iki tablonun birleştirilmesi işlemi yapıldı
            var list = db.Sections;
            var result = (from a in list.ToList()
                          join b in Masalar on a.Id equals b.SectionId
                          select new DinnerTableViewModel()
                          {
                              TableName = b.TableName,
                              TableSortOrder = b.TableSortOrder,
                              SectionName = a.Name,
                              TableId=b.TableId,
                          }).ToList();


            //ViewBag.GetData = new SelectList(db.Sections.Where(x => x.RestaurantId == RestorantId.ToString()).ToList(), "SectionId", "SectionName");
            ViewBag.GetData = new SelectList(db.Sections.Where(x => x.RestaurantId == RestorantId.ToString()).ToList(), "Id", "Name");
            return View(result.ToList());
            //return View(Masalar);
        }
        public ActionResult TableList()
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            var OrderStatus = GetTableStatus(RestorantId.ToString());      
            var _sections = db.Sections.Where(x => x.RestaurantId == RestorantId.ToString()).ToList();

            var result = (from a in OrderStatus
                          join b in _sections on a.SectionId equals b.Id
                          select new TableStatusViewModel()
                          {
                              TableName = a.TableName,
                              SectionName = b.Name,
                              TableId = a.TableId,
                          }).ToList();


            return View(result);
        }
        // Masa kaydetme için yazıldı
        public ActionResult Create(int Sira, string Adi, int Id)
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            var Data = new DinnerTable();
            Data.TableName = Adi;
            Data.TableSortOrder = Sira;
            Data.SectionId = Id;
            Data.State = true;
            Data.UserId = Session["Id"].ToString();
            Data.RestorantId = RestorantId.ToString();
            db.DinnerTable.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var Masa = db.DinnerTable.FirstOrDefault(x => x.RestorantId == RestorantId.ToString() && x.TableId == Id);
            ViewBag.GetData = new SelectList(db.Sections.Where(x => x.RestaurantId == RestorantId.ToString()).ToList(), "Id", "Name");
            return View(Masa);
        }
        [HttpPost]
        public ActionResult Edit(int TableId, int Sira, string Adi, int Id)
        {
            var UserId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == UserId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            var Masa = db.DinnerTable.FirstOrDefault(x => x.TableId == TableId);
            if (Masa != null)
            {
                using (var context = new DataContext())
                {
                    var _Masa = context.DinnerTable.FirstOrDefault(x => x.TableId == TableId);
                    _Masa.TableName = Adi;
                    _Masa.TableSortOrder = Sira;
                    _Masa.SectionId = Id;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult MasaDelete(int? id)
        {
            if (id != null)
            {
                string _userId = Session["Id"].ToString();
                var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
                Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

                var Masa = db.DinnerTable.Where(m => m.TableId == id && m.RestorantId == RestorantId.ToString()).FirstOrDefault();
                if (Masa != null)
                {
                    db.DinnerTable.Remove(Masa);
                    var test = db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult TableOrder(int id)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            ViewBag.TotalOrder = db.Orders.Where(x => x.RestorantId == RestorantId.ToString() && x.TableId == id && x.FoodCount > 0 && (x.State == 1 || x.State == 2 || x.State == 3)).ToList();
            ViewBag.MasaTasi = db.DinnerTable.Where(x => x.RestorantId == RestorantId.ToString()).ToList();
            //ViewBag.Masa = db.DinnerTable.ToList();
            return View();
        }

        public ActionResult MasaTasi(int Id)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            ViewBag.MasaTasi = db.DinnerTable.Where(x => x.RestorantId == RestorantId.ToString()).ToList();
            ViewBag.BeforeTableId = Id.ToString();

            return View();

        }
        [HttpPost]
        public ActionResult MasaTasi(string BeforeTableId, int TableId)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;

            if (!string.IsNullOrEmpty(BeforeTableId) && !string.IsNullOrEmpty(TableId.ToString()))
            {
                int BeforeTableIntId = Convert.ToInt32(BeforeTableId);
                //Eski siparişlerin taşımnası durumu kontrol edilmelidir
                var TotalOrder = db.Orders.Where(x => x.RestorantId == RestorantId.ToString() && x.TableId == BeforeTableIntId && x.FoodCount > 0 && (x.State == 1 || x.State == 2 || x.State == 3)).ToList();
                foreach (var item in TotalOrder)
                {
                    using (var context = new DataContext())
                    {
                        var _orders = context.Orders.FirstOrDefault(x => x.OrderId == item.OrderId);
                        _orders.TableId = TableId;
                        db.Orders.Add(_orders);
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult TableQr(int id)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            // gelen link bu şekilde yakalandı
            string domainName = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string Link = domainName + "/Category/Index?id=" + RestorantId + "&TableId=" + id;
            //string Link = "http://kartepesaklivadi.egarsonum.net/Category/Index?id=" + RestorantId + "&TableId=" + id;
            //  string Link = "https://localhost:44307/Category/Index?id=" + RestorantId + "&TableId=" + id;
            string QrCode = CreateQrCode(Link, "");
            ViewBag.Qr = QrCode;
            var _Table = db.DinnerTable.FirstOrDefault(x => x.TableId == id);
            ViewBag.TableName = _Table.TableName;
            ViewBag.CompanyName = user.CompanyName;
            var _SectionTest = db.Sections.ToList();

            var _Section = db.Sections.FirstOrDefault(x => x.Id == _Table.SectionId).Name;
            ViewBag.SectionName = _Section;
            return View();
        }
        public string CreateQrCode(string ImportData, string Path)
        {
            string Response = "";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ImportData, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap qrCodeImage = qrCode.GetGraphic(10, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(Path));
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(5, Color.Black, Color.White, null);
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            Response = "data:image/png;base64," + Convert.ToBase64String(byteImage); // Get Base64
            return Response;
        }
        public List<TableStatusViewModel> GetTableStatus(string RestorantId)
        {
            var DinnerTables = db.DinnerTable.Where(x => x.RestorantId == RestorantId).ToList();
            var Result = new List<TableStatusViewModel>();
            var Order = db.Orders.Where(x => x.RestorantId == RestorantId && x.State != 0).ToList();
            foreach (var item in DinnerTables)
            {
                var OrderItem = Order.OrderByDescending(x => x.CreatedDate).FirstOrDefault(x => x.TableId == item.TableId);
                var ResultItem = new TableStatusViewModel();
                ResultItem.SectionId=item.SectionId;
                ResultItem.TableId = item.TableId;
                ResultItem.TableName = item.TableName;
                ResultItem.TableSortOrder = item.TableSortOrder;
                ResultItem.OrderId = OrderItem != null ? OrderItem.OrderId : 0;
                ResultItem.OrderStatus = OrderItem != null ? OrderItem.State : 0;
                Result.Add(ResultItem);
            }
            return Result.OrderBy(x => x.TableSortOrder).ToList();
        }
        public ActionResult GettingReady(int? id)
        {
            if (id != null)
            {

                var entity = db.Orders.Where(x => x.TableId == id && x.State == 2);
                foreach (var item in entity)
                {
                    using (var context = new DataContext())
                    {
                        var OrderItem = context.Orders.FirstOrDefault(x => x.OrderId == item.OrderId);
                        OrderItem.State = 3;
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("TableOrder", new { id = id });
        }

        public ActionResult Paid(int TableId, string PaymentType, double TotalPrice)
        {
            string _userId = Session["Id"].ToString();
            var user = dbI.Users.FirstOrDefault(x => x.Id == _userId);
            Guid RestorantId = user != null ? (user.RestorantId != string.Empty ? new Guid(user.RestorantId) : Guid.Empty) : Guid.Empty;
            using (var context = new DataContext())
            {
                var OrderPaids = new OrderPaid();
                OrderPaids.UserId = _userId;
                OrderPaids.PaymentType = PaymentType;
                OrderPaids.CreatedDate = DateTime.Now;
                OrderPaids.UpdataDate = DateTime.Now;
                OrderPaids.RestorantId = RestorantId.ToString();
                OrderPaids.TableId = TableId;
                OrderPaids.TotalPrice = TotalPrice;
                context.OrderPaids.Add(OrderPaids);
                context.SaveChanges();
                context.Dispose();
            }

            var entity = db.Orders.Where(x => x.TableId == TableId && x.State == 3);
            foreach (var item in entity)
            {
                using (var context = new DataContext())
                {
                    var OrderItem = context.Orders.FirstOrDefault(x => x.OrderId == item.OrderId);
                    OrderItem.State = 4;
                    context.SaveChanges();
                    context.Dispose();
                }
            }

            return RedirectToAction("TableList");
        }
        public ActionResult EditSection(int id)
        {
            var section = db.Sections.FirstOrDefault(x => x.Id == id);
            return View(section);
        }
        [HttpPost]
        public ActionResult EditSection(int id, string Name)
        {
            try
            {
                var Section = db.Sections.FirstOrDefault(x => x.Id == id);
                if (Section != null)
                {
                    using (var context = new DataContext())
                    {
                        var _Section = context.Sections.FirstOrDefault(x => x.Id == id);
                        _Section.Name = Name;
                        db.Sections.Add(_Section);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("SectionList");
            }
            catch
            {
                return RedirectToAction("SectionList");
            }
        }
    }
}