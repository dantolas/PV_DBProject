using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.table_objects
{
    internal class Manufacturer
    {

        public string Name { get; set; }

        public int Id { get; set; }

        public int country_id { get; set; }

        public Manufacturer(int id, string name, int country_id)
        {
            Name = name;
            Id = id;
            this.country_id = country_id;
        }

        public Manufacturer(string name, int country_id) {
            this.Name = name;
            this.country_id = country_id;
        }
    }
}
