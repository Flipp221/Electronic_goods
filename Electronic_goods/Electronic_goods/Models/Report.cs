using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("report")]
    public class Report
    {
        public Report()
        {

        }
        public Report(string name, string count, string fIO)
        {
            Name = name;
            Count = count;
            FIO = fIO;
        }

        [AutoIncrement, PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public string FIO { get; set; }
    }
}
