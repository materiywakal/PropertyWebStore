using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApp.ViewModels;

namespace WebApp.Models
{
    public class WebContex : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<BlockOfFlatsType> BlockOfFlatsTypes { get; set; }
        public DbSet<BathroomType> BathroomTypes { get; set; }
        public DbSet<BalconyType> BalconyTypes { get; set; }
        public DbSet<WallMaterial> WallMaterials { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
    #region main
    public class Publication
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public bool IsRent { get; set; }
        public int Floor { get; set; }
        public int RoomAmount { get; set; }
        public int OfferingRoomAmount { get; set; }
        public bool IsPassageRoom { get; set; }
        public bool IsFurnitureExist { get; set; }
        public float TotalArea { get; set; }
        public float LivingArea { get; set; }
        public float KitchenArea { get; set; }
        public float PropertyArea { get; set; }

        public int BlockOfFlatsTypeId { get; set; }
        public BlockOfFlatsType BlockOfFlatsType { get; set; }

        public int BathroomTypeId { get; set; }
        public BathroomType BathroomType { get; set; }

        public int BalconyTypeId { get; set; }
        public BalconyType BalconyType { get; set; }

        public int YearOfConstruction { get; set; }

        public int WallMaterialId { get; set; }
        public WallMaterial WallMaterial { get; set; }

        public string Description { get; set; }
        public DateTime PostTime { get; set; }
        public float Cost { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public bool IsOneDayRent { get; set; }
        public bool CanExchange { get; set; }
        public bool IsSelled { get; set; }
        public bool IsApprovedByAdmin { get; set; } 
        public bool IsActive { get; set; }
        public int AmountOfPageViews { get; set; }

        public Publication()
        {

        }

        public Publication(CreatePublicationModel model)
        { 
            Id = model.Id;
            PropertyTypeId = model.PropertyTypeId;
            if (model.BalconyTypeId == 0)
            {
                BalconyTypeId = 1;
            }
            else
            {
                BalconyTypeId = model.BalconyTypeId;
            }

            if (model.BathroomTypeId == 0)
            {
                BathroomTypeId = 1;
            }
            else
            {
                BathroomTypeId = model.BathroomTypeId;
            }

            if (model.BlockOfFlatsTypeId == 0)
            {
                BlockOfFlatsTypeId = 1;
            }
            else
            {
                BlockOfFlatsTypeId = model.BathroomTypeId;
            }

            if (model.WallMaterialId == 0)
            {
                WallMaterialId = 1;
            }
            else
            {
                WallMaterialId = model.WallMaterialId;
            }

            if (model.UserId == 0)
            {
                UserId = 1;
            }
            else
            {
                UserId = model.UserId;
            }
            IsRent = model.IsRent;
            IsPassageRoom = model.IsPassageRoom;
            IsOneDayRent = model.IsOneDayRent;
            IsFurnitureExist = model.IsFurnitureExist;
            Floor = model.Floor;
            Address = model.Address;
            Coordinates = model.Coordinates;
            Cost = model.Cost;
            Description = model.Description;
            TotalArea = model.TotalArea;
            LivingArea = model.LivingArea;
            KitchenArea = model.KitchenArea;
            PropertyArea = model.PropertyArea;
            YearOfConstruction = model.YearOfConstruction;
            CanExchange = model.CanExchange;
            RoomAmount = model.RoomAmount;
            OfferingRoomAmount = model.OfferingRoomAmount;
            PostTime = DateTime.Now;
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Publication> Publications { get; set; }
        public User()
        {
            Publications = new List<Publication>();
        }
    }
    public class MainForm
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
    public class Role : MainForm { }
    public class PropertyType : MainForm { }
    public class BlockOfFlatsType : MainForm { }
    public class BathroomType : MainForm { }
    public class BalconyType : MainForm { }
    public class WallMaterial : MainForm { }

    #endregion

    #region chat
    public class Chat
    {
        public int Id { get; set; }
        public int User1Id { get; set; }
        public User User1 { get; set; }
        public int User2Id { get; set; }
        public User User2 { get; set; }
        public ICollection<Message> Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime Date { get; set; }
        public string HoursAndMinutes { get { return Date.ToString("HH:mm"); } }
        public string Text { get; set; }
    }
    #endregion
}