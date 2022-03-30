using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Databes
{
    public class MySQLDB
    {
        public bool GirisYap(string ad, string sifre, out int id)
        {
            string mysqlBaglantisi = "SERVER=localhost;DATABASE=carusers;UID=root;PWD=1234";
            
                using (var baglan = new MySqlConnection(mysqlBaglantisi))
                {
                    try
                    {
                        baglan.Open();
                        MySqlDataReader oku;
                        MySqlCommand komut = new MySqlCommand("SELECT user_id FROM users WHERE user_name ='" + ad + "' AND user_password ='" + sifre + "'", baglan);
                        oku = komut.ExecuteReader();
                        if (oku.Read())
                        {
                            id = oku.GetInt32(0);
                            return true;
                        }
                        else
                        {
                            id = 0;
                            return false;
                        }

                    }
                    catch (Exception hata)
                    {
                        id = 0;
                        return false;
                        throw;
                    }
                }
            }
        public List<int> CarId()
        {
            string mysqlBaglantisi = "SERVER=localhost;DATABASE=carusers;UID=root;PWD=1234";
            List<int> cars = new List<int>();
            using (var baglan = new MySqlConnection(mysqlBaglantisi))
            {
                try
                {
                    baglan.Open();
                    MySqlDataReader oku;

                    MySqlCommand komut = new MySqlCommand("SELECT  car_users.car_id FROM users, car_users where car_users.user_id ="+Kullanici.kullanici_id+"  and users.user_id="+Kullanici.kullanici_id+" ", baglan);
                    oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        cars.Add(oku.GetInt32(0));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            return cars;
        }
        public void GirisCikisKayit()
        {
            string mysqlBaglantisi = "SERVER=localhost;DATABASE=carusers;UID=root;PWD=1234";
            using (var baglan = new MySqlConnection(mysqlBaglantisi))
            {
                try
                {
                    baglan.Open();

                    MySqlCommand komut = new MySqlCommand("insert into time_users (user_id,login_time,logout_time) values (@p1,@p2,@p3)", baglan);
                    komut.Parameters.AddWithValue("@p1", Kullanici.kullanici_id);

                    komut.Parameters.AddWithValue("@p2", Kullanici.kullanici_giris);
                    komut.Parameters.AddWithValue("@p3", Kullanici.kullanici_cikis);
                    komut.ExecuteNonQuery();
                }

                catch (Exception hata)
                {
                    throw;
                }
            }
        }


    }
    }
