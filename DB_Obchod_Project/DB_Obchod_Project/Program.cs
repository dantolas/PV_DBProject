using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DB_Obchod_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region <ConnectionString>
            Console.WriteLine("Working directory:"+Directory.GetCurrentDirectory()+ "\n---------------------------------------------------------------------------------------------------------------");
            
            string json = "";
            try 
            {
                 json = File.ReadAllText("config/conn_config.json");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: Database connection configuration file not found.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            
            
            SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(json);
            #region <Testing connection config file>
            try
            {
                consStringBuilder.UserID = jsonObject["userId"].ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: |userId| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);

            }
            try
            {
                consStringBuilder.UserID = jsonObject["password"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: |password| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);
            }
            try
            {
                consStringBuilder.UserID = jsonObject["initialCatalog"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: |initialCatalog| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);
            }
            try
            {
                consStringBuilder.UserID = jsonObject["dataSource"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: |dataSource| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);
            }
            try
            {
                consStringBuilder.UserID = jsonObject["connTimeout"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: |connTimeout| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);
            }
            try
            {
                consStringBuilder.UserID = jsonObject["persistSecurityInfo"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: |persistSecurityInfo| not set in connection configuration file. Please check that conn_config.json is properly set up.");
                Environment.Exit(0);
            }
            #endregion

            consStringBuilder.UserID = jsonObject["userId"].ToString();
            consStringBuilder.Password = jsonObject["password"].ToString();
            consStringBuilder.InitialCatalog = jsonObject["initialCatalog"].ToString();
            consStringBuilder.DataSource = jsonObject["dataSource"].ToString();
            consStringBuilder.ConnectTimeout = Convert.ToInt32(jsonObject["connTimeout"].ToString());
            consStringBuilder.PersistSecurityInfo= Convert.ToBoolean(jsonObject["persistSecurityInfo"].ToString());
            #endregion

            Console.WriteLine("Type help for available actions.");

            while (true)
            {
                Console.Write("->");

                string input = Console.ReadLine();
                if (input == null) continue;

                #region<Commands>
                switch (input.ToLower())
                {
                    #region<Deaulft-command unknown>
                    default:
                        Console.WriteLine("Unknown command. Please type help for available actions.");
                        break;
                    #endregion

                    #region <Help> 
                    case "help":
                        Console.WriteLine(
                            "showtables => Shows existing tables in db"+
                            "\n|\nimport => Import data from .json file to a table"
                            );
                        break;
                    #endregion

                    #region <Show tables>
                    case "showtables":
                        try
                        {
                            Console.WriteLine(getTables(consStringBuilder.ConnectionString));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    #endregion

                    #region<Import command>
                    case "import":
                        Console.Write("Enter table name\n->");
                        string table = Console.ReadLine();
                        if (table == null) continue;




                        Console.Write("Enter file path name\n->");
                        string path = Console.ReadLine();
                        if(path == null) continue;

                        try
                        {
                            json = File.ReadAllText(path);
                            Console.WriteLine(json);
                        }catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }



                        break;
                        #endregion

                }
                #endregion
            }


            try
            {
                using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected");

                    //Načtení dat z tabulky:

                    string query = "select * from country";
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
                Console.WriteLine("Exception");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        public static string getTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT\r\n  TABLE_NAME\r\nFROM\r\n  INFORMATION_SCHEMA.TABLES;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    string returnString = "";
                    while (reader.Read())
                    {
                        returnString += reader[0]+"\n";
                    }
                    return returnString;
                }

            }
        }
    }
}