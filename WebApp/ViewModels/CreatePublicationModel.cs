﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreatePublicationModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public CreatePublicationModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public CreatePublicationModel(Publication publication)
        {
            Id = publication.Id;
            UserId = publication.UserId;
            PropertyTypeId = publication.PropertyTypeId;
            IsRent = publication.IsRent;
            Floor = publication.Floor;
            RoomAmount = publication.RoomAmount;
            OfferingRoomAmount = publication.OfferingRoomAmount;
            IsPassageRoom = publication.IsPassageRoom;
            IsFurnitureExist = publication.IsFurnitureExist;
            TotalArea = publication.TotalArea;
            LivingArea = publication.LivingArea;
            KitchenArea = publication.KitchenArea;
            PropertyArea = publication.PropertyArea;
            BlockOfFlatsTypeId = publication.BlockOfFlatsTypeId;
            BathroomTypeId = publication.BathroomTypeId;
            BalconyTypeId = publication.BathroomTypeId;
            YearOfConstruction = publication.YearOfConstruction;
            WallMaterialId = publication.WallMaterialId;
            Description = publication.Description;
            PostTime = publication.PostTime;
            Cost = publication.Cost;
            Address = publication.Address;
            Coordinates = publication.Coordinates;
            IsOneDayRent = publication.IsOneDayRent;
            CanExchange = publication.CanExchange;
            IsSubwayNear = publication.IsSubwayNear;
        }
        public List<HttpPostedFileBase> Files { get; set; }

        public bool IsRent { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public int RoomAmount { get; set; }
        [Required]
        public int OfferingRoomAmount { get; set; }
        public bool IsPassageRoom { get; set; }
        public bool IsFurnitureExist { get; set; }
        [Required]
        public float TotalArea { get; set; }
        [Required]
        public float LivingArea { get; set; }
        [Required]
        public float KitchenArea { get; set; }
        [Required]
        public float PropertyArea { get; set; }
        public bool IsSubwayNear { get; set; }

        public int BlockOfFlatsTypeId { get; set; }
        public BlockOfFlatsType BlockOfFlatsType { get; set; }

        public int BathroomTypeId { get; set; }
        public BathroomType BathroomType { get; set; }

        public int BalconyTypeId { get; set; }
        public BalconyType BalconyType { get; set; }

        public int YearOfConstruction { get; set; }

        public int WallMaterialId { get; set; }
        public WallMaterial WallMaterial { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime PostTime { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public bool IsOneDayRent { get; set; }
        public bool CanExchange { get; set; }
    }
}