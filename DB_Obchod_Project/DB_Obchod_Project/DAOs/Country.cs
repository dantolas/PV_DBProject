using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            string[] spacings = { "", "", ""};
            for (int i = 4 - this.Id.ToString().Length; i > 0; i--)
            {
                spacings[0] += " ";
            }
            for (int i = 25 - this.Name.ToString().Length; i > 0; i--)
            {
                spacings[1] += " ";
            }
            for (int i = 7 - this.Population.ToString().Length; i > 0; i--)
            {
                spacings[2] += " ";
            }
            return "Id:" + this.Id + spacings[0] +" | Name:" + this.Name + spacings[1] +" | Population:" + this.Population + spacings[2] +" | Size:" + Size;
        }



        #region <DAO Methods>
        public static void Delete(string connectionString, Country element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM country WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
            }
        }

        public static void Delete(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM country WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = id;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();

            }
        }

        public static IEnumerable<Country> GetAll(string connectionString)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM country", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetInt32(3)
                    );
                    yield return country;
                }
                reader.Close();
            }
        }

        public static Country? GetByID(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM country WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                Country? country = null;
                while (reader.Read())
                {
                    country = new Country(
                        id,
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)
                );

                }
                reader.Close();
                return country;
            }

        }

        public static void Save(string connectionString, Country element)
        {
            SqlCommand? command = null;

            if (element.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO country (name, population, size ) VALUES (@name, @population, @size)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@population", element.Population));
                    command.Parameters.Add(new SqlParameter("@size", element.Size));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    element.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE country SET name = @name, population = @population, size = @size " +
                    "WHERE id = @id", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@population", element.Population));
                    command.Parameters.Add(new SqlParameter("@size", element.Size));
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
