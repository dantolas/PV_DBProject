using DB_Obchod_Project.table_objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public DateTime Date { get; set; }

        public bool Paid { get; set; }


        public Order() { }

        public Order(int id, int number, float totalPrice, DateTime date, bool paid)
        {
            Id = id;
            Number = number;
            TotalPrice = totalPrice;
            this.Date = date;
            this.Paid = paid;

        }

        public Order(int number, float totalPrice, DateTime date, bool paid)
        {
            this.Number = number;
            this.TotalPrice= totalPrice;
            this.Date = date;
            this.Paid = paid;
        }

        public override string ToString()
        {
            return "ID:"+this.Id+" | Number:"+this.Number+" | TotalPrice:"+this.TotalPrice+" | Date of creation:"+this.Date+" | Paid:"+this.Paid;
        }


        #region<DAO Methods>
        public static void Delete(string connectionString, Order element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM orders WHERE id = @id", new SqlConnection(connectionString)))
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

        public static void Delete(string connectionString,int id)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM orders WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = id;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
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
                        float.Parse(reader[2].ToString()),
                        reader.GetDateTime(3),
                        reader.GetBoolean(4)

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
                    reader.GetInt32(0),
                        reader.GetInt32(1),
                        float.Parse(reader[2].ToString()),
                        reader.GetDateTime(3),
                        reader.GetBoolean(4)
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
                using (command = new SqlCommand("INSERT INTO orders (number, total_price,_date,paid) VALUES (@number, @total_price, @_date,@paid)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@number", element.Number));
                    command.Parameters.Add(new SqlParameter("@total_price", element.TotalPrice));
                    command.Parameters.Add(new SqlParameter("@_date", element.Date));
                    command.Parameters.Add(new SqlParameter("@paid", element.Paid));
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


        public static void SaveWithItems(string connectionString, Order element, List<Order_item> items)
        {
            element.Date = DateTime.Now;
            element.Paid = false;
            element.TotalPrice = 0;
            items.ForEach(item =>
            {
                element.TotalPrice += item.Price;
            });

            using (SqlCommand  command = new SqlCommand("INSERT INTO orders (number, total_price,_date,paid) VALUES (@number, @total_price, @_date,@paid)", new SqlConnection(connectionString)))
            {
                command.Connection.Open();
                command.Parameters.Add(new SqlParameter("@number", element.Number));
                command.Parameters.Add(new SqlParameter("@total_price", element.TotalPrice));
                command.Parameters.Add(new SqlParameter("@_date", element.Date));
                command.Parameters.Add(new SqlParameter("@paid", element.Paid));
                command.ExecuteNonQuery();
                //zjistim id posledniho vlozeneho zaznamu
                command.CommandText = "Select @@Identity";
                element.Id = Convert.ToInt32(command.ExecuteScalar());

                items.ForEach(item =>
                {
                    item.Order_id = element.Id;

                    Order_item.Save(connectionString,item);
                });


            }

        }


        #endregion

    }
}
