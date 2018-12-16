namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebContext db)
        {
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 1, Content = "квартира" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 2, Content = "комната" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 3, Content = "дом" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 4, Content = "участок" });

            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 1, Content = "панельный" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 2, Content = "монолитный" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 3, Content = "кирпичный" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 4, Content = "блочный" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 5, Content = "каркасный" });

            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 1, Content = "совмещенный" });
            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 2, Content = "раздельный" });
            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 3, Content = "отсутствует" });

            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 1, Content = "присутствует" });
            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 2, Content = "лоджия" });
            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 3, Content = "отсутствует" });

            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 1, Content = "кирпич" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 2, Content = "дерево" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 3, Content = "дерево, обложенное кирпичом" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 4, Content = "сборно-щитовой" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 5, Content = "блочный" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 6, Content = "керамзитбетон" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 7, Content = "шлакобетон" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 8, Content = "панельный" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 9, Content = "другое" });

            db.Roles.AddOrUpdate(new Role { Id = 1, Content = "user" });
            db.Roles.AddOrUpdate(new Role { Id = 2, Content = "admin" });
        }
    }
}
