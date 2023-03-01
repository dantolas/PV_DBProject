using DB_Obchod_Project.table_objects;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;


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
            var jsonObject =System.Text.Json.JsonSerializer.Deserialize<JsonObject>(json);
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



                //
                //Defining local variables for all commands to avoid multiple declarations
                //
                Regex regexp = new Regex("^(\\d+)$");
                int id = 0;

                #region<Commands>
                switch (input.ToLower())
                {
                    #region<Default-command unknown>
                    default:
                        Console.WriteLine("Unknown command. Please type help for available actions.");
                        break;
                    #endregion

                    #region<Exit>
                    case "exit":
                        System.Environment.Exit(0);
                        
                        break;
                    #endregion

                    #region <Help> 
                    case "help":
                        Console.WriteLine(
                            "showtables => Shows existing tables in db"+
                            "\n|\nimport => Import data from .json file to a table"
                            +"\n|\nselect => Prints all data from table."
                            + "\n|\nselectId => Prints data row from table by id."
                            + "\n|\nnewOrder => Create a new order and save it to db."
                            + "\n|\ndeleteId => Delete from table by id."
                            + "\n|\nupdate => Update a column in a table."
                            + "\n|\nexit => Exits application."
                            
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

                    #region <Update command>
                    case "update":
                        Console.Write("Enter the table you wish to update.\n->");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;
                        if (!TableExists(consStringBuilder.ConnectionString, input)) { Console.WriteLine("Table does not exist."); continue; }
                        input = input.ToLower();

                        List<string> cols = new List<string>();

                        using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                        {
                            connection.Open();

                            string query = "SELECT COLUMN_NAME\r\nFROM INFORMATION_SCHEMA.COLUMNS\r\nWHERE TABLE_NAME = '"+input+"'";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                SqlDataReader reader = command.ExecuteReader();
                                
                                while (reader.Read())
                                {
                                    cols.Add(reader[0].ToString());
                                }
                            }

                        }

                        if (cols.Count <= 0) { Console.WriteLine("This table has no columns to update."); continue; }

                        cols.ForEach(col =>
                        {
                            Console.WriteLine(col);
                        });

                        string table = input;

                        Console.Write("Enter the column you wish to update\n->");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;
                        input = input.ToLower();
                        if (cols.Contains(input))
                        {
                            using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                            {
                                connection.Open();

                                string query = string.Format("SELECT DATA_TYPE\r\nFROM INFORMATION_SCHEMA.COLUMNS\r\nWHERE COLUMN_NAME = {0} AND TABLE_NAME = {1};", table,input);
                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString());
                                    }
                                }

                            }

                            while (true)
                            {
                                Console.WriteLine("Enter the new value");
                                string updateValue = Console.ReadLine();
                                if(updateValue == null) continue;

                                

                            }
                            
                            
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

                            jsonObject = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(json);
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
                        table = Console.ReadLine();
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
                        }catch(Exception e)
                        {
                            Console.WriteLine("Error: File not found. Please check that you entered the correct path.");

                            break;
                        }

                        switch (table.ToLower())
                        {
                            case "country":

                                List<Country> countryList = JsonSerializer.Deserialize<List<Country>>(json);

                                if (countryList == null) { Console.WriteLine("The import has failed. Please check that you entered the correct path and that the file is in correct Json format."); break; }
                                if (countryList.Count == 0) { Console.WriteLine("The import resulted in empty list."); break; }

                                foreach(Country c in countryList)
                                {
                                    Console.WriteLine(c);
                                }


                                Console.Write("Insert data into db?Y/N\n->");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;
                                if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                {
                                    try
                                    {
                                        countryList.ForEach(country =>
                                        {
                                            Country.Save(consStringBuilder.ConnectionString, country);
                                        });
                                        Console.WriteLine("Data inserted to db.");
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine("Insertion failed.");
                                    }
                                }

                                
                                break;

                            case "orders":

                                List<Order> orders = JsonSerializer.Deserialize<List<Order>>(json);

                                List<JOrder> jorders = JsonSerializer.Deserialize<List<JOrder>>(json);

                                if (orders == null) { Console.WriteLine("The import has failed. Please check that you entered the correct path and that the file is in correct Json format."); break; }
                                if (orders.Count == 0) { Console.WriteLine("The import resulted in empty list."); break; }

                                Console.WriteLine("Orders:");
                                orders.ForEach(order =>
                                {
                                    Console.WriteLine(order);
                                });

                                Console.WriteLine("Jorders:");
                                jorders.ForEach(jorder =>
                                {
                                    Console.WriteLine(jorder);
                                });




                                Console.Write("Insert data into db?Y/N\n->");

                                //input = Console.ReadLine();
                                //if (input == null) continue;
                                //if (input.ToLower().Equals("exit")) continue;
                                //if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                //{
                                //    try
                                //    {
                                //        countryList.ForEach(country =>
                                //        {
                                //            Country.Save(consStringBuilder.ConnectionString, country);
                                //        });
                                //        Console.WriteLine("Data inserted to db.");
                                //    }
                                //    catch (Exception e)
                                //    {
                                //        Console.WriteLine("Insertion failed.");
                                //    }
                                //}

                                break;
                        }



                        break;
                    #endregion

                    #region <Select Command>
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

                                Console.WriteLine("Error: Cannot select from "+input+" at the moment.");
                                break;

                            case "orders" :
                                try
                                {
                                    List<Order> orders = Order.GetAll(consStringBuilder.ConnectionString).ToList();
                                    foreach (Order order in orders)
                                    {
                                        Console.WriteLine(order);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: Could not connect to server.");
                                    Console.WriteLine(e.Message);
                                }
                                
                                break;

                            case "country":
                                try
                                {
                                    List<Country> countries = Country.GetAll(consStringBuilder.ConnectionString).ToList();
                                    foreach (Country country in countries)
                                    {
                                        Console.WriteLine(country);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: Could not connect to server.");
                                    Console.WriteLine(e.Message);
                                }
                                
                                break;


                            case "manufacturer":
                                try
                                {
                                    List<Manufacturer> manufacturers = Manufacturer.GetAll(consStringBuilder.ConnectionString).ToList();
                                    foreach (Manufacturer manufacturer in manufacturers)
                                    {
                                        Console.WriteLine(manufacturer);
                                    }
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine("Error: Could not connect to server.");
                                    Console.WriteLine(e.Message);
                                }
                                
                                break;

                            case "product":
                                try
                                {
                                    List<Product> products = Product.GetAll(consStringBuilder.ConnectionString).ToList();
                                    foreach (Product prod in products)
                                    {
                                        Console.WriteLine(prod);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: Could not connect to server.");
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(e.StackTrace);
                                }

                                break;

                            case "order_item":
                                try
                                {
                                    List<Order_item> ois= Order_item.GetAll(consStringBuilder.ConnectionString).ToList();
                                    foreach (Order_item oi in ois)
                                    {
                                        Console.WriteLine(oi);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error: Could not connect to server.");
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine(e.StackTrace);
                                }

                                break;

                        }

                        break;
                    #endregion

                    #region<SelectId Command>
                    case "selectid":

                        Console.WriteLine("Enter the table you wish to select from.");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;
                        if (!TableExists(consStringBuilder.ConnectionString, input)) { Console.WriteLine("Table does not exist."); continue; }
                        input = input.ToLower();

                        switch (input)
                        {

                            default:
                                
                                
                                Console.WriteLine("Cannot select from "+input+" at the moment.");
                                break;

                            case"orders":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                 regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input= Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if(Order.GetByID(consStringBuilder.ConnectionString,id) == null)
                                {
                                    Console.WriteLine("No order with that id.");
                                    break;
                                }

                                Console.WriteLine(Order.GetByID(consStringBuilder.ConnectionString,id));

                                break;


                            case "country":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                 regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Country.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No country with that id.");
                                    break;
                                }

                                Console.WriteLine(Country.GetByID(consStringBuilder.ConnectionString, id));

                                break;


                            case "order_item":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Order_item.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No order items with that id.");
                                    break;
                                }

                                Console.WriteLine(Order_item.GetByID(consStringBuilder.ConnectionString, id));

                                break;


                            case "product":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Product.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No products with that id.");
                                    break;
                                }

                                Console.WriteLine(Product.GetByID(consStringBuilder.ConnectionString, id));

                                break;

                            case "manufacturer":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Manufacturer.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No manufacturers with that id.");
                                    break;
                                }

                                Console.WriteLine(Manufacturer.GetByID(consStringBuilder.ConnectionString, id));

                                break;
                        }
                        break;
                    #endregion

                    #region<NewOrder Command>
                    case "neworder":

                        Order o = new Order();
                        List<Order_item> items = new List<Order_item>();

                        Console.WriteLine("**Phase:Creating order**");
                        Console.WriteLine("Please enter order number:");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;

                        while (!regexp.IsMatch(input))
                        {
                            Console.Write("Please enter a positive integer.\n->");
                            input = Console.ReadLine();
                            if (input == null) continue;
                            if (input.ToLower().Equals("exit")) continue;
                        }

                        o.Number = Convert.ToInt32(input) * 271;

                        Console.WriteLine("Add items to order? Y/N");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;
                        if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                        {
                            bool addingItems = true;
                            while (addingItems)
                            {
                                Order_item item = new Order_item();
                                Console.WriteLine("**Phase:Adding items**\n");

                                Console.WriteLine("Please select the product you wish to order. (By product id)");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) { addingItems = false; continue; };
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);
                                if (Product.GetByID(consStringBuilder.ConnectionString,id) == null) { Console.WriteLine("This product does not exist."); continue; }
                                Product currentProduct = Product.GetByID(consStringBuilder.ConnectionString, id);

                                Console.WriteLine("Please enter the amount you wish to order.");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) { addingItems = false; continue; };
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }


                                item.Product_id = currentProduct.Id;
                                item.Amount = Convert.ToInt32(input);
                                item.Price = item.Amount * currentProduct.Price;

                                items.Add(item);

                                Console.WriteLine("Would you like to order another item? Y/N");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (!input.ElementAt(0).ToString().ToLower().Equals("y")) addingItems = false; 

                            }

                            

                            Order.SaveWithItems(consStringBuilder.ConnectionString, o, items);
                            Console.WriteLine();

                        }


                        



                        break;
                    #endregion

                    #region <DeleteId command>
                    case "deleteid":

                        Console.WriteLine("Enter the table you wish to delete from.");
                        input = Console.ReadLine();
                        if (input == null) continue;
                        if (input.ToLower().Equals("exit")) continue;
                        if (!TableExists(consStringBuilder.ConnectionString, input)) { Console.WriteLine("Table does not exist."); continue; }
                        input = input.ToLower();

                        switch (input)
                        {

                            default:


                                Console.WriteLine("Cannot select from " + input + " at the moment.");
                                break;

                            case "orders":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Order.GetByID(consStringBuilder.ConnectionString,id) == null) { Console.WriteLine("No order with that id."); continue; }

                                Console.Write("Delete all associated order_items?Y/N\n->");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;
                                
                                //
                                //Deleting all order_items linked to order
                                //
                                if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                {
                                    Order_item.GetAllByOrder(consStringBuilder.ConnectionString, Order.GetByID(consStringBuilder.ConnectionString, id)).ToList().ForEach(item => {
                                        Order_item.Delete(consStringBuilder.ConnectionString, item); 
                                        });
                                }


                                Order.Delete(consStringBuilder.ConnectionString, id);

                                Console.WriteLine("Deleted Succesfully.");

                                break;


                            case "product":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Product.GetByID(consStringBuilder.ConnectionString, id) == null) { Console.WriteLine("No product with that id."); continue; }

                                Console.WriteLine("All order_items having this product associated with them will also be deleted. Procceed?Y/N\n->");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                //
                                //Deleting all order_items linked to order, and updating order price
                                //
                                if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                {
                                    Order_item.GetAllByProduct(consStringBuilder.ConnectionString, Product.GetByID(consStringBuilder.ConnectionString, id)).ToList().ForEach(item => {
                                        
                                        
                                        o = Order.GetByID(consStringBuilder.ConnectionString, item.Order_id);
                                        o.TotalPrice -= item.Price;
                                        Order.Save(consStringBuilder.ConnectionString, o);
                                        Order_item.Delete(consStringBuilder.ConnectionString, item);
                                    });
                                    
                                    Product.Delete(consStringBuilder.ConnectionString, id);
                                    Console.WriteLine("Deleted Succesfully.");
                                }

                                break;

                            
                            case "order_item":

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);
                                //
                                //Reducing price of order with this order_item associated to it
                                //
                                o = Order.GetByID(consStringBuilder.ConnectionString, Order_item.GetByID(consStringBuilder.ConnectionString, id).Order_id);
                                o.TotalPrice -= Order_item.GetByID(consStringBuilder.ConnectionString, id).Price;
                                Order.Save(consStringBuilder.ConnectionString,o);

                                Order_item.Delete(consStringBuilder.ConnectionString, id);

                                Console.WriteLine("Deleted Succesfully.");

                                break;


                            case "manufacturer":

                                
                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                regexp = new Regex("^(\\d+)$");
                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Manufacturer.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No manufacturer with that id.");
                                    break;
                                }

                                Console.WriteLine(Manufacturer.GetByID(consStringBuilder.ConnectionString, id));

                                Console.WriteLine("Delete from db?Y/N");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;
                                if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                {
                                    Manufacturer.Delete(consStringBuilder.ConnectionString,Manufacturer.GetByID(consStringBuilder.ConnectionString, id));
                                }
                                Console.WriteLine("Deleted succesfully");


                                    break;

                            case "country":
                                

                                Console.Write("Enter id\n->");
                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;

                                while (!regexp.IsMatch(input))
                                {
                                    Console.Write("Please enter a positive integer.\n->");
                                    input = Console.ReadLine();
                                    if (input == null) continue;
                                    if (input.ToLower().Equals("exit")) continue;
                                }

                                id = Convert.ToInt32(input);

                                if (Country.GetByID(consStringBuilder.ConnectionString, id) == null)
                                {
                                    Console.WriteLine("No country with that id.");
                                    break;
                                }

                                Console.WriteLine(Country.GetByID(consStringBuilder.ConnectionString, id));

                                Console.WriteLine("Delete from db?Y/N");

                                input = Console.ReadLine();
                                if (input == null) continue;
                                if (input.ToLower().Equals("exit")) continue;
                                if (input.ElementAt(0).ToString().ToLower().Equals("y"))
                                {
                                    Country.Delete(consStringBuilder.ConnectionString, Country.GetByID(consStringBuilder.ConnectionString, id));
                                }
                                Console.WriteLine("Deleted succesfully");

                                break;
                        }

                        break;
                        #endregion
                }
                #endregion
            }
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

        #region <Determines if input exists as table in db>
        public static bool TableExists(string connectionString,string tableName)
        {
            string[] tables = getTables(connectionString).Split("\n");
            return tables.Contains(tableName.ToLower());
        }
        #endregion
    }
}