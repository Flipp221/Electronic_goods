﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("tovars")]
    public class Tovars
    {
        public Tovars()
        {
        }

        public Tovars(string name, string description, string price, string color, string type, string material, string imagePath, string count, int tovarsid)
        {
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            Type = type;
            Material = material;
            ImagePath = imagePath;
            Count = count;
            TovarsId = tovarsid;
        }

        [AutoIncrement, PrimaryKey, Column("_id")]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Material { get; set; }
        public string ImagePath { get; set; }
        public string Count { get; set; }
        public int TovarsId { get; set; }
    }
}
