using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public override string ToString()
        {
            return "Id:"+this.Id+" | Name:"+this.Name+" | country_id:"+this.country_id;
        }

        #region<DAO Methods>
        public static void Delete(string connectionString, Manufacturer element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM manufacturer WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
            }
        }

        public static IEnumerable<Manufacturer> GetAll(string connectionString)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM manufacturer", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Manufacturer man = new Manufacturer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    );
                    yield return man;
                }
                reader.Close();
            }
        }

        public static Manufacturer? GetByID(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM manufacturer WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                Manufacturer man = null;
                while (reader.Read())
                {
                    man = new Manufacturer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    );
                

                }
                reader.Close();
                return man;
            }

        }

        public static void Save(string connectionString, Manufacturer element)
        {
            SqlCommand command = null;

            if (element.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO manufacturer (name, country_id) VALUES (@name, @country_id)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@country_id", element.country_id));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    element.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE orders SET name = @name, country_id = @country_id " +
                    "WHERE id = @id", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", element.Id));
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@country_id", element.country_id));
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
