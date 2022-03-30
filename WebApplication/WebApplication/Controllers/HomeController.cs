using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Databes;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
       
        public ActionResult Index()
        {

            var database = mongoClient.GetDatabase("CarLocation");
            var collection = database.GetCollection<Arac>("Car");
            MongoDataB mongoDB = new MongoDataB();
            var kontrol = collection.Find<Arac>(a => true).ToList();
            if (kontrol.Count==0)
            {
                mongoDB.CsvKaydet();
            }
            DateTime time=DateTime.Now.AddMinutes(150);
            MySQLDB mySQLDB = new MySQLDB();
            var kulanniciAracId = mySQLDB.CarId();
            var sorgu = collection.Find<Arac>(a=> a.Datetime >= time && kulanniciAracId.Contains(a.aracID) ).ToList();
            return View(sorgu);
        }
        [HttpPost]
        public ActionResult Index(string carid)
        {
            var database = mongoClient.GetDatabase("CarLocation");
            var collection = database.GetCollection<Arac>("Car");
            MySQLDB mySQLDB = new MySQLDB();
            var kulanniciAracId = mySQLDB.CarId();
            List<Arac> sorgu = new List<Arac>();
            var tarihBaslangic =Convert.ToDateTime(Request.Form["Tarih1"]).AddHours(3);
            var tarihBitis = Convert.ToDateTime(Request.Form["Tarih2"]).AddHours(3);
            if (Request.Form["arac_id"]=="")
            {
                 sorgu = collection.Find<Arac>(a => a.Datetime >= tarihBaslangic && a.Datetime <= tarihBitis && kulanniciAracId.Contains(a.aracID)).ToList();
            }
            else
            {
                var aracId = Convert.ToInt32(Request.Form["arac_id"]);
                if (kulanniciAracId.Contains(aracId))
                {
                    sorgu = collection.Find<Arac>(a => a.Datetime >= tarihBaslangic && a.Datetime <= tarihBitis && a.aracID == aracId).ToList();
                }
            }
            return View(sorgu);
        }
        public RedirectResult LogOut()
        {
            Kullanici.kullanici_cikis = DateTime.Now;
            MySQLDB mySQLDB = new MySQLDB();
            mySQLDB.GirisCikisKayit();
            return Redirect("/Login");
        }
    }
}