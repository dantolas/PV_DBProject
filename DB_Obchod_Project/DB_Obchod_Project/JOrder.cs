using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project
{
    internal class JOrder
    {
        public int Id { get; set; }

        private int number;
        public int Number { get; set; }

        private DateTime date;
        public DateTime Date { get; set; }

        private bool paid;
        public bool Paid { get; set; }


        public JOrder() { }

        public JOrder(int id, int number, DateTime date, bool paid)
        {
            Id = id;
            Number = number;
            this.Date = date;
            this.Paid = paid;

        }

        public JOrder(int number, float totalPrice, DateTime date, bool paid)
        {
            this.Number = number;
            this.Date = date;
            this.Paid = paid;
        }

        public class Item
        {
            private int product_id;
            public int Product_id { get; set; }

            private int amount;
            public int Amount { get; set; }

            public override string ToString()
            {
                return "product: "+product_id +" " + "amount: "+amount;
            }
        }

        public override string ToString()
        {
            string itemsIn = " ";
            
            return "Number:"+this.number + " Date:" + this.date + " Paid:" + this.paid +" Items:"; 
        }

    }
}
