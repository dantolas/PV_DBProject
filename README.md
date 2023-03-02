DB PROJEKT made by Kuta Samuel C3b
----------------------------------
| Quick Intro |
  This is a console application working with an industrial-like database to manage orders of different products from different manufacturers.
  You can check the DB scheme or creation query in DB_Project\db.
  Application isn't finished, however it would seem I ran out of time so...


| Launching the application |
  Navigate to DB_Project\DB_Obchod_Project\DB_Obchod_Project\bin\Debug\net6.0
  and run the .exe file.
  You should see ->
  ![image](https://user-images.githubusercontent.com/19873145/222544872-ee596484-0fb9-44fc-b27b-9e27910d2109.png)


| Configuration |
  You can configure the DATABASE CONNECTION STRING and SUPPORTED IMPORT TABLES in the net6.0/config folder.
  For configurations to work properly please remember to keep the names same, just change the values.
  
| Functionality | 
  Application responds to a variety of commands. Type help when you turn on the application to see the commands available.
  With this application, you can read or delete specific data from the database (almost made the update work too...), and place new orders into the database.
  You can also import data into database through this application, doing so by passing files written in the JSON format.
  
  | Quick Commands Explanations | 
    showtables => Lists all tables in database.
   
    import => Import data from a .json file.
    
    select => Prints all data from a table.
    
    selectId => Prints a specific data row by record ID.
    
    
    
    
    
    
