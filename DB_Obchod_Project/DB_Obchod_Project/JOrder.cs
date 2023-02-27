using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project
{
    internal class JOrder
    {
        private int number;
        public int Number { get; set; }

        private DateTime date;
        public DateTime Date { get; set; }

        private bool paid;
        public bool Paid { get; set; }

        private List<Item> items;
        public List<Item> Items { get; set; }

        public JOrder() { }

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
            
            return "Number:"+this.number + " Date:" + this.date + " Paid:" + this.paid +" Items:"+ this.Items.Count; 
        }

    }
}
