using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    //DbContext işlemleri yapabilmek için DbContext Sınıfından miras alması gerekir.
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class DataContext : DbContext
    {

        //eklenen base kodu istenen veritabanını oluşturmaya yardımcı olur.
        // base kısmına var olan bir connection string ismini yazmalmalı
        public DataContext() : base("dataConnection")
        {
            //Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodItems> FoodItems { get; set; }
        public DbSet<DinnerTable> DinnerTable { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPaid> OrderPaids { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<TranslateDetail> TranslateDetails { get; set; }
        public DbSet<Section> Sections { get; set; }  
        

    }
}