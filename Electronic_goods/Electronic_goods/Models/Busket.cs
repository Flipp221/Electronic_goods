using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Models
{
    [Table("busket")]
    public class Busket
    {
        public Busket()
        {

        }

        public Busket(int quantity, int clientId, int tovarsId)
        {
            Quantity = quantity;
            ClientId = clientId;
            TovarsId = tovarsId;
        }

        [AutoIncrement, PrimaryKey, Column("_id")]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ClientId { get; set; }
        public int TovarsId { get; set; }
    }
}
