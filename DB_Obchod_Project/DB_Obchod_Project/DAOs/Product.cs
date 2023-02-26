using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Obchod_Project.table_objects
{
    internal class Product
    {

        public int Id { get; set; }

        public int Manu_id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public float Price { get; set; }

        public Product(int id, int manu_id, string name, string type, float price)
        {
            Id = id;
            Manu_id = manu_id;
            Name = name;
            Type = type;
            Price = price;
        }

        public Product(int manu_id, string name, string type, float price) 
        {
            Manu_id = manu_id;
            Name = name;
            Type = type;
            Price = price;
        }

        public override string ToString()
        {
            string spacing = "";
            for(int i = 5 - this.Manu_id.ToString().Length; i > 0; i--)
            {
                spacing += " ";
            }
            return "Id:"+this.Id+" | Manu_Id:"+this.Manu_id+spacing+" | Name:"+this.Name+" | Type:"+this.Type + " | Price:"+this.Price;
        }



        #region<DAO Methods>
        public static void Delete(string connectionString, Product element)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM product WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = element.Id;
                element.Id = 0;
            }
        }

        public static IEnumerable<Product> GetAll(string connectionString)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM product", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product prod = new Product(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        float.Parse(reader[4].ToString())
                    );
                    yield return prod;
                }
                reader.Close();
            }
        }

        public static Product? GetByID(string connectionString, int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM product WHERE id = @id", new SqlConnection(connectionString)))
            {
                command.Connection.Open();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                Product prod = null;
                while (reader.Read())
                {
                    prod = new Product(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        float.Parse(reader[4].ToString())
                    );


                }
                reader.Close();
                return prod;
            }

        }

        public static void Save(string connectionString, Product element)
        {
            SqlCommand command = null;

            if (element.Id < 1)
            {
                using (command = new SqlCommand("INSERT INTO product (manu_id, name, type, price) VALUES (@manu_id, @name, @type, @price)", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@manu_id", element.Manu_id));
                    command.Parameters.Add(new SqlParameter("@type", element.Type));
                    command.Parameters.Add(new SqlParameter("@price", element.Price));
                    command.ExecuteNonQuery();
                    //zjistim id posledniho vlozeneho zaznamu
                    command.CommandText = "Select @@Identity";
                    element.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            else
            {
                using (command = new SqlCommand("UPDATE product SET name = @name, manu_id = @manu_id, type = @type, price = @price" +
                    "WHERE id = @id", new SqlConnection(connectionString)))
                {
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", element.Id));
                    command.Parameters.Add(new SqlParameter("@name", element.Name));
                    command.Parameters.Add(new SqlParameter("@manu_id", element.Manu_id));
                    command.Parameters.Add(new SqlParameter("@type", element.Type));
                    command.Parameters.Add(new SqlParameter("@price", element.Price));
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }
}
