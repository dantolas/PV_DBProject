using System.Data.SqlClient;

namespace DB_Obchod_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
            consStringBuilder.UserID = "sa";
            consStringBuilder.Password = "student";
            consStringBuilder.InitialCatalog = "test";
            consStringBuilder.DataSource = "PC962";
            consStringBuilder.ConnectTimeout = 30;
            try
            {
                using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Pripojeno");

                    //Ulozeni dat do tabulky

                    string query = "create table Table (id int identity(1,1) primary key, text nvarchar(20) not null)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    //Načtení dat z tabulky:

                    query = "select * from Table";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                        }
                    }


                }





            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}