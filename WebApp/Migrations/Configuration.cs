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
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 1, Content = "��������" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 2, Content = "�������" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 3, Content = "���" });
            db.PropertyTypes.AddOrUpdate(new PropertyType { Id = 4, Content = "�������" });

            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 1, Content = "���������" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 2, Content = "����������" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 3, Content = "���������" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 4, Content = "�������" });
            db.BlockOfFlatsTypes.AddOrUpdate(new BlockOfFlatsType { Id = 5, Content = "���������" });

            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 1, Content = "�����������" });
            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 2, Content = "����������" });
            db.BathroomTypes.AddOrUpdate(new BathroomType { Id = 3, Content = "�����������" });

            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 1, Content = "������������" });
            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 2, Content = "������" });
            db.BalconyTypes.AddOrUpdate(new BalconyType { Id = 3, Content = "�����������" });

            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 1, Content = "������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 2, Content = "������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 3, Content = "������, ���������� ��������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 4, Content = "������-�������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 5, Content = "�������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 6, Content = "�������������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 7, Content = "����������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 8, Content = "���������" });
            db.WallMaterials.AddOrUpdate(new WallMaterial { Id = 9, Content = "������" });

            db.Roles.AddOrUpdate(new Role { Id = 1, Content = "user" });
            db.Roles.AddOrUpdate(new Role { Id = 2, Content = "admin" });
        }
    }
}
