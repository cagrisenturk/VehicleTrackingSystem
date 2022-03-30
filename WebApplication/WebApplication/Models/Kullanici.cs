using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public static class Kullanici
    {
        public static int kullanici_id { get; set; }
        public static string kullanici_adi { get; set; }
        public static string kullanici_sifre { get; set; }
        public static DateTime kullanici_giris { get; set; }
        public static DateTime kullanici_cikis { get; set; }

    }
}