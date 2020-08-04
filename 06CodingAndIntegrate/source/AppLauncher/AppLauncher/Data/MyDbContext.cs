using AppLauncher.Models;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLauncher.Data
{
    public class MyDbContext : DbContext
    {
        //定义属性，便于外部访问数据表
        public DbSet<Products> Products { get { return Set<Products>(); } }
        public DbSet<ProductsDetails> ProductsDetails { get { return Set<ProductsDetails>(); } }
        public DbSet<Users> Users { get { return Set<Users>(); } }

        public DbSet<Others> Others { get { return Set<Others>(); } }

        public MyDbContext()
            : base("dbConn") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MyDbContext>(modelBuilder);
            //Database.SetInitializer(sqliteConnectionInitializer);

            ModelConfiguration.Configure(modelBuilder);
            var init = new SqliteDropCreateDatabaseWhenModelChanges<MyDbContext>(modelBuilder);
            Database.SetInitializer(init);
        }
    }

    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureMeasureDataEntity(modelBuilder);
        }
        private static void ConfigureMeasureDataEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>();
            modelBuilder.Entity<ProductsDetails>();
            modelBuilder.Entity<Users>();
            modelBuilder.Entity<Others>();
        }
    }
}
