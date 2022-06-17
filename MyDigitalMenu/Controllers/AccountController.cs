using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using MyDigitalMenu.Identity;
using MyDigitalMenu.Models;
using MyDigitalMenu.Models.UserIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyDigitalMenu.Controllers
{
    public class AccountController : Controller
    {
        private IdentityDataContext dbI = new IdentityDataContext();
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> userManager;


        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            //parola koşulları kontrol ediliyor.
            userManager.PasswordValidator = new CustomPasswordValidator()
            {
                //parola bir sayısal değer içermek zorunda
                RequireDigit = true,
                //parola minimum 7 karakter olamk zorunda
                RequiredLength = 7,
                //küçük harf içermeli
                RequireLowercase = true,
                //büyük harf içrtmeli
                RequireUppercase = true,
                //Alfanumerik değer içermeli
                RequireNonLetterOrDigit = false,
            };
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {//bir emaili bir kişi kullanabilir.
                RequireUniqueEmail = false,
                //alfa numerik karakter içermesin
                AllowOnlyAlphanumericUserNames = false,

            };
        }


        // GET: Account
        public ActionResult Index()
        {

            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Login()
        {

            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Login(Models.UserIdentity.Models.LoginModel model)
        {
            //string count;
            var user =/* CustomLogin(model);*/
                        userManager.Find(model.Username, model.Password);
            //  var _users = db.Users.ToList();
            //// var user = _users.Where(x=>x.UserName==model.Username && x.Password==model.Password);
            //  var user = db.Users.Find(model.Username, model.Password);
            try
            {
                if (ModelState.IsValid)
                {//eğer kullanıcı bulunamadıysa burada hata verir.
                 //eğer mysql sunucusu açık değilse burada hata veririr.
                    if (user == null)
                    {
                        //ModelState.AddModelError("UserErr", "Yanlış kullanıcı adı veya parola!");
                        TempData["Control"] = "Yanlış kurum kodu veya parola!";
                        return View(model);
                    }

                }

                #region Session add                            
                //var _users = db.Users.ToList();
                Session["Id"] = user.Id.ToString();
                Session["UserName"] = user.UserName;
                //Session["CompanyName"] = user.CompanyName;
                Session["CompanyName"] = user.CompanyName;
                //Session["Role"] = user.Role;
                return RedirectToAction("TableList", "Masa");
                #endregion
            }
            catch
            {
                return View();
            }
        }




        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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
        public ActionResult Register()
        {
            //ViewBag.KayıtHata1 = "";
            //if (TempData["Control"] != null)
            //{
            //    if (TempData["Control"].ToString() == "1")
            //    {
            //        ViewBag.KayıtHata1 = "Kayıtlı Hiç Kullanıcınız Yok. Lütfen Kullanıcı Ekleyiniz! ";
            //    }
            //}
            //{
            //    List<string> SchoolTypeList = scm.GetList().Select(x => x.Category).ToList();
            //    //List<string> SchoolTypeList = db.SchoolsCategorys.Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //if (Session["Role"].ToString() == "District")
            //{
            //    List<string> SchoolTypeList = scm.GetList().Where(x => x.Category != "İL" && x.Category != "İLÇE" && x.Category != "BAKANLIK").Select(x => x.Category).ToList();
            //    //List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category != "İL" && x.Category != "İLÇE" && x.Category != "BAKANLIK").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else if (Session["Role"].ToString() == "Province")
            //{
            //    List<string> SchoolTypeList = scm.GetList().Where(x => x.Category == "İLÇE").Select(x => x.Category).ToList();
            //    //List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category == "İLÇE").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else if (Session["Role"].ToString() == "Ministry")
            //{
            //    List<string> SchoolTypeList = scm.GetList().Where(x => x.Category == "İL").Select(x => x.Category).ToList();
            //    //List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category == "İL").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else
            //{
            //    List<string> SchoolTypeList = scm.GetList().Select(x => x.Category).ToList();
            //    //List<string> SchoolTypeList = db.SchoolsCategorys.Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            return View();
        }

        //Bu kısım ilçe milli eğitimler için yazıldı
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize] //tüm üyeler ile ilgili işlemler burada yapıldı.
        // [SessionExpire]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {


            //var ManagerId = Session["ManagerId"].ToString();
            //// var ManagerId = 0.ToString();
            //var districtCode = Session["DistrictCode"].ToString();
            ////var districtCode = 0.ToString();
            //var provinceCode = Session["ProvinceCode"].ToString();
            ////var provinceCode = 0.ToString();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Adress = model.Adress;
                user.CompanyName = model.CompanyName;
                user.PhoneNumber = model.Tel;
                user.CompanyName = model.CompanyName;
                //
                //user.SpecialEducation = model.SpecialEducation;

                //if (Session["Role"].ToString() == "District")
                //{
                //    // districtCode = ManagerId;
                //    user.ProvinceCode = provinceCode;
                //    user.DistrictCode = districtCode;
                //}
                //else if (Session["Role"].ToString() == "Province")
                //{
                //    // provinceCode = ManagerId;
                //    user.ProvinceCode = provinceCode;
                //    user.DistrictCode = 0.ToString();
                //}
                //else if (Session["Role"].ToString() == "Ministry")
                //{
                //    user.ProvinceCode = 0.ToString();
                //    user.DistrictCode = 0.ToString();
                //}
                //user.InstitutionCode = model.UserName;
                //user.InstitutionName = model.InstitutionName;
                var result = userManager.Create(user, model.Password);


                if (result.Succeeded)
                {
                    //userManager.AddToRole(user.Id, "User");

                    ViewBag.KayıtHata1 = "";
                    TempData["Control"] = "3";
                    ViewBag.KayıtHata1 = "Kullanıcınız başarı ile eklenmiştir.";


                    return RedirectToAction("Register", "Account");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error == "Passwords must be at least 7 characters.")
                        {
                            ModelState.AddModelError("", "Şifre en az 7 karakter olmalıdır.");
                            ViewBag.KayıtHata1 = "";
                            TempData["Control"] = "2";
                            ViewBag.KayıtHata1 = "Şifre en az 7 karakter olmalıdır.";
                        }
                        else if (error == "Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z').")
                        {
                            ModelState.AddModelError("", "Şifre en az bir tane küçük harf içermeli.");
                            ViewBag.KayıtHata1 = "";
                            TempData["Control"] = "2";
                            ViewBag.KayıtHata1 = "Şifre en az bir tane küçük harf içermeli.";

                        }
                        else if (error == "Passwords must have at least one uppercase ('A'-'Z').")
                        {
                            ModelState.AddModelError("", "Şifre en az bir tane küçük harf içermeli.");
                            ViewBag.KayıtHata1 = "";
                            TempData["Control"] = "2";
                            ViewBag.KayıtHata1 = "Şifre en az bir tane büyük harf içermeli.";

                        }
                        else if (error == "Passwords must have at least one lowercase ('a'-'z').")
                        {
                            ModelState.AddModelError("", "Şifre en az bir tane küçük harf içermeli.");
                            ViewBag.KayıtHata1 = "";
                            TempData["Control"] = "2";
                            ViewBag.KayıtHata1 = "Şifre en az bir tane küçük harf içermeli.";
                        }
                        else
                        {//Oluşturmaya çalıştığınız kurum kodu ile bir kullanıcı sistemde kayıtlı.
                            TempData["Control"] = "1";

                        }

                        return RedirectToAction("Register", "Account");


                    }
                    //return RedirectToAction("Index", "Admin");
                }

            }


            //if (Session["Role"].ToString() == "District")
            //{
            //    List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category != "İL" && x.Category != "İLÇE" && x.Category != "BAKANLIK").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else if (Session["Role"].ToString() == "Province")
            //{
            //    List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category == "İLÇE").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else if (Session["Role"].ToString() == "Ministry")
            //{
            //    List<string> SchoolTypeList = db.SchoolsCategorys.Where(x => x.Category == "İL").Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;
            //}
            //else
            //{
            //    List<string> SchoolTypeList = db.SchoolsCategorys.Select(x => x.Category).ToList();
            //    ViewBag.SchoolTypeListViewBag = SchoolTypeList;

            //}


            return View(model);


        }
        
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Id"] =null;
            Session["UserName"] = null;
            Session["CompanyName"] = null;
            return RedirectToAction("Login","Account");
        }
    }
}

