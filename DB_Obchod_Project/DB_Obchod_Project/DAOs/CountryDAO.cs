using DB_Obchod_Project.table_objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.DAOs
{
    internal class CountryDAO
    {
        public static void Delete(string connectionString, Country element)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM country WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
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
                    Country country= new Country(
                        
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

                Country country= null;
                while (reader.Read())
                {
                    country = new Country(
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
            SqlCommand command = null;

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

    }
}
