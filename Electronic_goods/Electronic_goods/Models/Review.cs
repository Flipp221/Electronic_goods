using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("Review")]
    public class Review
    {
        public Review()
        {
        }

        public Review(string fIO, string path, string reviewText, int count)
        {
            FIO = fIO;
            Path = path;
            ReviewText = reviewText;
            Count = count;
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Path { get; set; }
        public string ReviewText { get; set; }
        public int Count { get; set; }

    }
}
