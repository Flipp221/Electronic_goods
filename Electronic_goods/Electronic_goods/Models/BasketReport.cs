using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("basketrepor")]
    public class BasketReport
    {
        public BasketReport()
        {

        }
        public BasketReport(string name, string count, DateTime date, string price, string fIO)
        {
            Name = name;
            Count = count;
            Date = date;
            Price = price;
            FIO = fIO;
        }

        [AutoIncrement, PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
        public string FIO { get; set; }
    }
}
