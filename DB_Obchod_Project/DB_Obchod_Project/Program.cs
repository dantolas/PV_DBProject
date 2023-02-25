using DB_Obchod_Project.DAOs;
using DB_Obchod_Project.table_objects;
using System.Data;
using System.Data.Common;
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
                            +"\n|\nselect => Prints data from table."
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
                        
                        //
                        //Exists to store currently supported tables
                        //
                        string[] supportedTables = Array.Empty<string>();
                        #region<Reading config files supported tables>
                        try
                        {
                            json = File.ReadAllText("config/import_config.json");

                            jsonObject = JsonSerializer.Deserialize<JsonObject>(json);
                            try
                            {   
                                //
                                //Exists to cut out a usable string from the JSONNode containing the tables
                                //
                                string cutout = jsonObject["supportedTables"].ToString().Replace("[", "").Replace("]", "").Replace("\"", "");
                                
                                //
                                //Effective white space replacement
                                //
                                cutout = new string(cutout.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                                supportedTables = cutout.Split(',');
                                
                                //
                                //Weird bug when JSONArray was empty it still stored the empty value, this fixes it
                                //
                                if (supportedTables[0].Equals("")) supportedTables = Array.Empty<string>();

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: import_config.json file missing or not set up properly. Please check that it is present in config folder and set up properly.");
                            continue;
                        }
                        #endregion
                        
                        if(supportedTables.Length <= 0) 
                        {
                            Console.WriteLine("No tables currently supported.");
                            break;
                        }
                        
                        Console.WriteLine("Supported tables:");
                        foreach(string s in supportedTables)
                        {
                            Console.Write(s+" ");
                        }

                        Console.Write("\nEnter table name\n->");
                        string table = Console.ReadLine();
                        if (table == null) continue;
                        if (table.ToLower().Equals("exit")) continue;


                        string[] tables = getTables(consStringBuilder.ConnectionString).Split("\n");
                        if (!TableExists(consStringBuilder.ConnectionString,table)) { Console.WriteLine("Table does not exist.");  continue; }

                        if (!supportedTables.Contains(table.ToLower())) { Console.WriteLine("Table not currently supported"); continue;}

                        Console.Write("Enter file path name\n->");
                        string path = Console.ReadLine();
                        if(path == null) continue;
                        if (path.ToLower().Equals("exit")) continue;

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

                    case "select":

                        Console.WriteLine("Enter the table you wish to select from.");
                        input= Console.ReadLine();
                        if(input == null) continue;
                        if(input.ToLower().Equals("exit")) continue;
                        if (!TableExists(consStringBuilder.ConnectionString, input)) { Console.WriteLine("Table does not exist."); continue; }
                        input = input.ToLower();
                        switch (input)
                        {
                            default:

                                Console.WriteLine("Error: Something went wrong.");
                                break;

                            case "orders" :
                                List<Order> orders = OrdersDAO.GetAll(consStringBuilder.ConnectionString).ToList();
                                foreach (Order order in orders)
                                {
                                    Console.WriteLine(order);
                                }
                                break;

                            case "country":
                                List<Country> countries = CountryDAO.GetAll(consStringBuilder.ConnectionString).ToList();
                                foreach(Country country in countries)
                                {
                                    Console.WriteLine(country);
                                }
                                break;

                        }

                        break;

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

        #region <Returns existing tables in db>
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
        #endregion

        public static bool TableExists(string connectionString,string tableName)
        {
            string[] tables = getTables(connectionString).Split("\n");
            return tables.Contains(tableName.ToLower());
        }
    }
}