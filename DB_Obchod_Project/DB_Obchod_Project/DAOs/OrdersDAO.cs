using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.DAOs
{
    internal class OrdersDAO
    {
        public static void Delete(string connectionString, Order element)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM orders WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
            }
        }

        public static IEnumerable<Order> GetAll(string connectionString)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM orders", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        float.Parse(reader[2].ToString())
                    );
                    yield return order;
                }
                reader.Close();
            }
        }

        public static Order? GetByID(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM orders WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                Order order = null;
                while (reader.Read())
                {
                        order = new Order(
                        Convert.ToInt32(reader[0].ToString()),
                        reader.GetInt32(1),
                        reader.GetFloat(2)
                    );
                    
                }
                reader.Close();
                return order;
            }
            
        }

        public static void Save(string connectionString, Order element)
        {
            SqlCommand command = null;

            if (element.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO orders (number, total_price) VALUES (@number, @total_price)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@number", element.Number));
                    command.Parameters.Add(new SqlParameter("@total_price", element.TotalPrice));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    element.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE orders SET number = @number, total_price = @total_price " +
                    "WHERE id = @id", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", element.Id));
                    command.Parameters.Add(new SqlParameter("@number", element.Number));
                    command.Parameters.Add(new SqlParameter("@total_price", element.TotalPrice));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
