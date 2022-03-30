using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Databes
{
    public class MongoDataB
    {
       
        public void CsvKaydet()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("CarLocation");
            var collection = database.GetCollection<Arac>("Car");
            FileStream stream = new FileStream(@"C:\Users\csent\Desktop\proje\WebApplication\WebApplication\allCars.csv", FileMode.Open, FileAccess.Read);
            StreamReader reader = null;

            reader = new StreamReader(stream);
            List<string[]> data = new List<string[]>();
            string line = "";
            int sayac = 0;
            TimeSpan eklenecekgün = new TimeSpan(0, 0, 0, 0, 0);
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                var values = line.Split(',');
                if (sayac == 0)
                {
                    eklenecekgün = DateTime.Now - Convert.ToDateTime(values[0]);
                }
                Arac arac = new Arac();
                arac.konumX = Convert.ToDouble(values[1].Replace('.', ','));
                arac.konumY = Convert.ToDouble(values[2].Replace('.', ','));
                arac.aracID = Convert.ToInt32(values[3]);
                arac.Datetime = Convert.ToDateTime(values[0]).AddHours(3) + eklenecekgün;
                collection.InsertOne(arac);
                sayac++;
            }
        }
    }
}