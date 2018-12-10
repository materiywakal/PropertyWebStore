using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class BrowseFilterModel
    {
        public int PropertyType { get; set; }
        public bool IsRent { get; set; }
        public int? Floor { get; set; }
        public int? MinRoomAmount { get; set; }
        public int? MaxRoomAmount { get; set; }
        public int? OfferingRoomAmount { get; set; }
        public bool IsPassageRoom { get; set; }
        public bool IsFurnitureExist { get; set; }
        public bool IsOneDayRent { get; set; }
        public int? MinCost { get; set; }
        public int? MaxCost { get; set; }
        public int? MinPropertyArea { get; set; }
        public int? MaxPropertyArea { get; set; }
    }
}