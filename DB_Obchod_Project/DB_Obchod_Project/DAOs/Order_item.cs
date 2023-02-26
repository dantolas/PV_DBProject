﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.table_objects
{
    internal class Order_item
    {
        public int Id { get; set; }

        public int Product_id { get; set; }

        public int Amount { get; set; }

        public float Price { get; set; }

        public int Order_id { get; set; }

        public Order_item() { }
        public Order_item(int id, int product_id, int amount, float price, int order_id)
        {
            this.Amount = amount;
            this.Id = id;
            this.Product_id = product_id;
            this.Order_id = order_id;
            this.Price= price;
        }

        public Order_item(int product_id, int amount, float price, int order_id)
        {
            this.Amount = amount;
            this.Product_id = product_id;
            this.Order_id = order_id;
            this.Price = price;
        }

        public override string ToString()
        {
            return "ID:" + this.Id + " | product_id:" + this.Product_id + " | amount:" + this.Amount + " | price:" + this.Price + " | order_id:" + this.Order_id;
        }

        #region<DAO Methods>
        public static void Delete(string connectionString, Order_item element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM order_item WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
            }
        }

        public static IEnumerable<Order_item> GetAll(string connectionString)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM order_item", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order_item oi = new Order_item(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        float.Parse(reader[3].ToString()),
                        reader.GetInt32(4)
                    );
                    yield return oi;
                }
                reader.Close();
            }
        }

        public static Order_item? GetByID(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM order_item WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                Order_item oi= null;
                while (reader.Read())
                {
                    oi = new Order_item(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        float.Parse(reader[3].ToString()),
                        reader.GetInt32(4)
                    );


                }
                reader.Close();
                return oi;
            }

        }

        public static void Save(string connectionString, Order_item element)
        {
            SqlCommand command = null;

            if (element.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO order_item (product_id, amount, price, order_id) VALUES (@product_id, @amount, @price, @order_id)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@product_id", element.Product_id));
                    command.Parameters.Add(new SqlParameter("@amount", element.Amount));
                    command.Parameters.Add(new SqlParameter("@price", element.Price));
                    command.Parameters.Add(new SqlParameter("@order_id", element.Order_id));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    element.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE product SET product_id = @product_id, amount = @manu_id, type = @type, price = @price" +
                    "WHERE id = @id", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", element.Id));
                    command.Parameters.Add(new SqlParameter("@product_id", element.Product_id));
                    command.Parameters.Add(new SqlParameter("@amount", element.Amount));
                    command.Parameters.Add(new SqlParameter("@price", element.Price));
                    command.Parameters.Add(new SqlParameter("@order_id", element.Order_id));
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
