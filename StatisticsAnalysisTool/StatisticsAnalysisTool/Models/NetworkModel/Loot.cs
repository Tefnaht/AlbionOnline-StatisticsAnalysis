﻿using Newtonsoft.Json;
using System;

namespace StatisticsAnalysisTool.Models.NetworkModel
{
    public class Loot
    {
        public Loot()
        {
            UtcPickupTime = DateTime.UtcNow;
        }

        public long ObjectId { get; set; }
        public int ItemId { get; set; }
        [JsonIgnore] public Item Item { get; set; }
        public DateTime UtcPickupTime { get; }
        public int Quantity { get; set; }
        public string BodyName { get; set; }
        public string LooterName { get; set; }
        public bool IsTrash { get; set; }
    }
}