using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Databes;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        static int sayac=0;
        // GET: Login
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpGet]
        public ActionResult Redirect()
        {

            return View();
        }
        [HttpPost]
        public RedirectResult Redirect(string adi,string sifree)
        {
            MySQLDB mySql = new MySQLDB();
            
            string kullaniciAdi = Request.Form["kullanıcıAdi"];
            string sifre = Request.Form["sifre"];
            if (mySql.GirisYap(kullaniciAdi,sifre,out int id))
            {
                Kullanici.kullanici_id = id;
                Kullanici.kullanici_adi = kullaniciAdi;
                Kullanici.kullanici_sifre = sifre;
                Kullanici.kullanici_giris = DateTime.Now;
                sayac = 0;
                return Redirect("/Home/Index");
            
            }
            else if(sayac==2)
            {
                    TempData.Clear();
                    TempData["mesaj"] = "3 defa başarısız giriş yaptınız bekleyiniz";
            }
            else
            {
                sayac++;
                TempData.Clear();
                TempData["mesaj"] = "Yanlış şifre veya kullanıcı adı";
            }

            return Redirect("/");
        }

    }
}