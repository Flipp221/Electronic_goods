using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("delivery")]
    class Delivery
    {
        public Delivery()
        {

        }

        public Delivery(string deliveryDate, string expectedDeliveryDate, int order)
        {
            DeliveryDate = deliveryDate;
            ExpectedDeliveryDate = expectedDeliveryDate;
            Order = order;
        }

        [AutoIncrement, PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string DeliveryDate { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        [Unique]
        public int Order { get; set; }

    }
}
