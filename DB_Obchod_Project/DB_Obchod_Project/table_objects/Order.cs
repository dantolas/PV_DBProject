using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project
{
    internal class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public float TotalPrice { get; set; }

        public Order(int id, int number, float totalPrice)
        {
            Id = id;
            Number = number;
            TotalPrice = totalPrice;
        }

        public Order(int number, float totalPrice)
        {
            this.Number = number;
            this.TotalPrice= totalPrice;
        }

        public override string ToString()
        {
            return "ID:"+this.Id+" | Number:"+this.Number+" | TotalPrice:"+this.TotalPrice;
        }
    }
}
