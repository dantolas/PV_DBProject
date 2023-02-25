using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.table_objects
{
    internal class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int Size { get; set; }


        public Country(int id, string name, int population, int size) 
        { 
            this.Id= id;
            this.Name = name;
            this.Population = population;
            this.Size = size;
        }

        public Country(string name, int population, int size)
        {
            this.Name = name;
            this.Population = population;
            this.Size = size;
        }

        public override string ToString()
        {
            return "Id:"+this.Id+" | Name:" + this.Name + " | Population:" + this.Population + " | Size:" + Size;
        }

    }
}
