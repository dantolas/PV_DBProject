SELECT
  TABLE_NAME
FROM
  INFORMATION_SCHEMA.TABLES;


create table country(
	id int primary key identity(1,1),
	name varchar(50) not null unique,
	population int not null check(population >= 1),
	size int not null check(size > 1)
);


insert into country (name, population, size) values ('Pakistan', 413128, 21);
insert into country (name, population, size) values ('Morocco', 257756, 70);
insert into country (name, population, size) values ('Croatia', 405241, 83);
insert into country (name, population, size) values ('Ireland', 133713, 48);
insert into country (name, population, size) values ('China', 888097, 80);
insert into country (name, population, size) values ('France', 733587, 70);
insert into country (name, population, size) values ('United Kingdom', 607598, 36);
insert into country (name, population, size) values ('Venezuela', 286127, 66);
insert into country (name, population, size) values ('Uzbekistan', 28835, 42);
insert into country (name, population, size) values ('Sweden', 914927, 99);
insert into country (name, population, size) values ('Russia', 720191, 42);
insert into country (name, population, size) values ('Guatemala', 994824, 30);
insert into country (name, population, size) values ('United States', 844661, 39);
insert into country (name, population, size) values ('Indonesia', 706976, 21);
insert into country (name, population, size) values ('Peru', 569766, 97);
insert into country (name, population, size) values ('Brazil', 586989, 23);
insert into country (name, population, size) values ('Czech Republic', 225867, 26);

select * from country;





create table manufacturer(
	id int primary key identity(1,1),
	_name varchar(50) not null,
	country_id int foreign key references country(id)
);

insert into manufacturer (_name, country_id) values ('Lindgren and Sons', 1);
insert into manufacturer (_name, country_id) values ('Morar-McDermott', 15);
insert into manufacturer (_name, country_id) values ('Kulas-Windler', 8);
insert into manufacturer (_name, country_id) values ('Reichert, Bergnaum and Pfannerstill', 12);
insert into manufacturer (_name, country_id) values ('Hartmann-Larson', 2);
insert into manufacturer (_name, country_id) values ('Wisoky, Kutch and Kuhn', 17);
insert into manufacturer (_name, country_id) values ('Rutherford-Spencer', 9);
insert into manufacturer (_name, country_id) values ('Pfeffer Group', 12);
insert into manufacturer (_name, country_id) values ('Larson, Schulist and Crist', 4);
insert into manufacturer (_name, country_id) values ('Turner LLC', 6);
insert into manufacturer (_name, country_id) values ('Bernier-Kuhic', 8);
insert into manufacturer (_name, country_id) values ('Green-McCullough', 6);
insert into manufacturer (_name, country_id) values ('Ledner, Schuster and Maggio', 6);
insert into manufacturer (_name, country_id) values ('Cormier, Mayer and Emard', 8);
insert into manufacturer (_name, country_id) values ('Hamill-Altenwerth', 12);
insert into manufacturer (_name, country_id) values ('Nicolas, White and Prohaska', 2);
insert into manufacturer (_name, country_id) values ('Crona-Conn', 6);
insert into manufacturer (_name, country_id) values ('Kulas Group', 16);
insert into manufacturer (_name, country_id) values ('Bergstrom-Bernhard', 16);
insert into manufacturer (_name, country_id) values ('Leannon Group', 17);
insert into manufacturer (_name, country_id) values ('Gerlach LLC', 17);
insert into manufacturer (_name, country_id) values ('Hackett, Hamill and Schuster', 5);
insert into manufacturer (_name, country_id) values ('Farrell, Luettgen and Hayes', 6);
insert into manufacturer (_name, country_id) values ('Ryan-Quitzon', 8);
insert into manufacturer (_name, country_id) values ('Botsford-Wiza', 11);

select * from manufacturer;



create table product(
	id int primary key identity(1,1),
	manu_id int foreign key references manufacturer(id),
	_name varchar(40) not null,
	_type varchar(50) not null,
	price float not null
);

insert into product (manu_id, _name, _type, price) values (14, 'Broom Handle', 'food', 30.5);
insert into product (manu_id, _name, _type, price) values (23, 'Syrup - Monin, Irish Cream', 'food', 24.7);
insert into product (manu_id, _name, _type, price) values (8, 'Puree - Raspberry', 'food', 23.0);
insert into product (manu_id, _name, _type, price) values (2, 'Vanilla Beans', 'food', 35.0);
insert into product (manu_id, _name, _type, price) values (17, 'Nantucket - 518ml', 'food', 12.8);
insert into product (manu_id, _name, _type, price) values (25, 'Pate - Liver', 'food', 33.8);
insert into product (manu_id, _name, _type, price) values (7, 'Bread - Multigrain Oval', 'food', 16.2);
insert into product (manu_id, _name, _type, price) values (17, 'Salt And Pepper Mix - White', 'food', 16.0);
insert into product (manu_id, _name, _type, price) values (5, 'Cookie Double Choco', 'food', 7.9);
insert into product (manu_id, _name, _type, price) values (20, 'Table Cloth 62x120 White', 'food', 16.1);
insert into product (manu_id, _name, _type, price) values (24, 'Energy Drink Red Bull', 'food', 48.9);
insert into product (manu_id, _name, _type, price) values (15, 'Tea - Herbal Orange Spice', 'food', 16.6);
insert into product (manu_id, _name, _type, price) values (19, 'Wine - White, Chardonnay', 'food', 28.7);
insert into product (manu_id, _name, _type, price) values (2, 'Sauce - Chili', 'food', 13.3);
insert into product (manu_id, _name, _type, price) values (25, 'Lamb - Shoulder, Boneless', 'food', 22.9);
insert into product (manu_id, _name, _type, price) values (22, 'Cheese - Swiss Sliced', 'food', 46.8);
insert into product (manu_id, _name, _type, price) values (21, 'Beans - Soya Bean', 'food', 29.6);
insert into product (manu_id, _name, _type, price) values (19, 'Plastic Wrap', 'food', 11.2);
insert into product (manu_id, _name, _type, price) values (15, 'Veal - Inside, Choice', 'food', 15.7);
insert into product (manu_id, _name, _type, price) values (9, 'Table Cloth 53x53 White', 'food', 15.6);
insert into product (manu_id, _name, _type, price) values (18, 'Halibut - Whole, Fresh', 'food', 21.8);
insert into product (manu_id, _name, _type, price) values (14, 'Juice - Apple, 341 Ml', 'food', 40.5);
insert into product (manu_id, _name, _type, price) values (24, 'Energy Drink - Franks Pineapple', 'food', 42.2);
insert into product (manu_id, _name, _type, price) values (5, 'Soup - Boston Clam Chowder', 'food', 10.9);
insert into product (manu_id, _name, _type, price) values (11, 'Kiwi', 'food', 48.8);
insert into product (manu_id, _name, _type, price) values (14, 'Fondant - Icing', 'food', 43.0);
insert into product (manu_id, _name, _type, price) values (14, 'Parasol Pick Stir Stick', 'food', 8.2);
insert into product (manu_id, _name, _type, price) values (19, 'Truffle Shells - Semi - Sweet', 'food', 9.3);
insert into product (manu_id, _name, _type, price) values (16, 'Flour - Chickpea', 'food', 39.6);
insert into product (manu_id, _name, _type, price) values (6, 'Veal - Bones', 'food', 6.6);
insert into product (manu_id, _name, _type, price) values (17, 'Peas - Frozen', 'food', 45.9);
insert into product (manu_id, _name, _type, price) values (10, 'Ice Cream - Turtles Stick Bar', 'food', 38.1);
insert into product (manu_id, _name, _type, price) values (21, 'Pasta - Cannelloni, Sheets, Fresh', 'food', 10.7);
insert into product (manu_id, _name, _type, price) values (8, 'Red Currant Jelly', 'food', 41.3);
insert into product (manu_id, _name, _type, price) values (21, 'Bread - Rolls, Rye', 'food', 39.4);
insert into product (manu_id, _name, _type, price) values (10, 'Stock - Veal, White', 'food', 14.1);
insert into product (manu_id, _name, _type, price) values (20, 'Tequila - Sauza Silver', 'food', 48.4);
insert into product (manu_id, _name, _type, price) values (19, 'Mussels - Cultivated', 'food', 5.4);
insert into product (manu_id, _name, _type, price) values (6, 'Beer - Alexander Kieths, Pale Ale', 'food', 5.9);
insert into product (manu_id, _name, _type, price) values (7, 'Spring Roll Veg Mini', 'food', 22.3);
insert into product (manu_id, _name, _type, price) values (12, 'Coke - Diet, 355 Ml', 'food', 48.3);
insert into product (manu_id, _name, _type, price) values (16, 'Soup - French Can Pea', 'food', 36.3);
insert into product (manu_id, _name, _type, price) values (14, 'Cocoa Butter', 'food', 23.9);
insert into product (manu_id, _name, _type, price) values (20, 'Container - Clear 32 Oz', 'food', 23.8);
insert into product (manu_id, _name, _type, price) values (12, 'Bandage - Fexible 1x3', 'food', 16.3);
insert into product (manu_id, _name, _type, price) values (24, 'Slt - Individual Portions', 'food', 41.0);
insert into product (manu_id, _name, _type, price) values (9, 'Sambuca - Ramazzotti', 'food', 42.7);
insert into product (manu_id, _name, _type, price) values (20, 'Bread - Sticks, Thin, Plain', 'food', 5.5);
insert into product (manu_id, _name, _type, price) values (14, 'Soup - Boston Clam Chowder', 'food', 34.9);
insert into product (manu_id, _name, _type, price) values (3, 'Huck Towels White', 'food', 32.0);
insert into product (manu_id, _name, _type, price) values (5, 'Yucca', 'food', 9.7);
insert into product (manu_id, _name, _type, price) values (14, 'Island Oasis - Strawberry', 'food', 20.3);
insert into product (manu_id, _name, _type, price) values (8, 'Tuna - Yellowfin', 'food', 22.9);
insert into product (manu_id, _name, _type, price) values (11, 'Creme De Menthe Green', 'food', 12.2);
insert into product (manu_id, _name, _type, price) values (22, 'Pail - 4l White, With Handle', 'food', 30.1);
insert into product (manu_id, _name, _type, price) values (20, 'Chinese Foods - Chicken', 'food', 44.6);
insert into product (manu_id, _name, _type, price) values (12, 'Venison - Striploin', 'food', 40.2);
insert into product (manu_id, _name, _type, price) values (6, 'Pork - Bacon, Sliced', 'food', 4.4);
insert into product (manu_id, _name, _type, price) values (4, 'Liners - Banana, Paper', 'food', 16.8);
insert into product (manu_id, _name, _type, price) values (13, 'The Pop Shoppe - Lime Rickey', 'food', 45.7);
insert into product (manu_id, _name, _type, price) values (18, 'Mortadella', 'food', 5.8);
insert into product (manu_id, _name, _type, price) values (5, 'Sour Puss Raspberry', 'food', 16.2);
insert into product (manu_id, _name, _type, price) values (23, 'Onions - Red', 'food', 12.7);
insert into product (manu_id, _name, _type, price) values (3, 'Pasta - Lasagne, Fresh', 'food', 18.6);
insert into product (manu_id, _name, _type, price) values (12, 'Cup - 6oz, Foam', 'food', 50.0);
insert into product (manu_id, _name, _type, price) values (7, 'Table Cloth 54x54 Colour', 'food', 42.9);
insert into product (manu_id, _name, _type, price) values (3, 'Bread - Ciabatta Buns', 'food', 43.3);
insert into product (manu_id, _name, _type, price) values (5, 'Nut - Pistachio, Shelled', 'food', 11.9);
insert into product (manu_id, _name, _type, price) values (19, 'Tea - Orange Pekoe', 'food', 45.1);
insert into product (manu_id, _name, _type, price) values (1, 'Wine - Fat Bastard Merlot', 'food', 29.2);
insert into product (manu_id, _name, _type, price) values (16, 'Chips - Potato Jalapeno', 'food', 20.5);
insert into product (manu_id, _name, _type, price) values (19, 'Wine - Cotes Du Rhone Parallele', 'food', 32.1);
insert into product (manu_id, _name, _type, price) values (11, 'Pineapple - Golden', 'food', 29.7);
insert into product (manu_id, _name, _type, price) values (16, 'Muffin Batt - Ban Dream Zero', 'food', 34.3);
insert into product (manu_id, _name, _type, price) values (3, 'Blueberries', 'food', 22.3);
insert into product (manu_id, _name, _type, price) values (5, 'Broccoli - Fresh', 'food', 8.1);
insert into product (manu_id, _name, _type, price) values (3, 'Wine - Ruffino Chianti Classico', 'food', 1.5);
insert into product (manu_id, _name, _type, price) values (14, 'Soup - Knorr, Ministrone', 'food', 14.8);
insert into product (manu_id, _name, _type, price) values (18, 'Wine - Beaujolais Villages', 'food', 18.3);
insert into product (manu_id, _name, _type, price) values (12, 'Lamb Tenderloin Nz Fr', 'food', 12.4);
insert into product (manu_id, _name, _type, price) values (22, 'Wine - Duboeuf Beaujolais', 'food', 31.7);
insert into product (manu_id, _name, _type, price) values (17, 'Sauce - Hp', 'food', 17.1);
insert into product (manu_id, _name, _type, price) values (6, 'Lettuce - Mini Greens, Whole', 'food', 47.4);
insert into product (manu_id, _name, _type, price) values (4, 'Cheese - Brie, Cups 125g', 'food', 22.8);
insert into product (manu_id, _name, _type, price) values (20, 'Bay Leaf Fresh', 'food', 28.5);
insert into product (manu_id, _name, _type, price) values (2, 'Bacardi Breezer - Tropical', 'food', 34.9);
insert into product (manu_id, _name, _type, price) values (21, 'Tomatoes - Roma', 'food', 27.8);
insert into product (manu_id, _name, _type, price) values (21, 'Pepper - Yellow Bell', 'food', 12.9);
insert into product (manu_id, _name, _type, price) values (12, 'Bag - Clear 7 Lb', 'food', 39.1);
insert into product (manu_id, _name, _type, price) values (21, 'Ecolab Crystal Fusion', 'food', 29.7);
insert into product (manu_id, _name, _type, price) values (20, 'Tomatoes Tear Drop Yellow', 'food', 5.3);
insert into product (manu_id, _name, _type, price) values (8, 'Peach - Fresh', 'food', 16.0);
insert into product (manu_id, _name, _type, price) values (22, 'Appetizer - Tarragon Chicken', 'food', 18.2);
insert into product (manu_id, _name, _type, price) values (7, 'Flour - Semolina', 'food', 48.1);
insert into product (manu_id, _name, _type, price) values (21, 'Cake Circle, Foil, Scallop', 'food', 49.8);
insert into product (manu_id, _name, _type, price) values (8, 'Shrimp - Black Tiger 6 - 8', 'food', 17.3);
insert into product (manu_id, _name, _type, price) values (25, 'Muffin - Zero Transfat', 'food', 24.4);
insert into product (manu_id, _name, _type, price) values (25, 'Okra', 'food', 39.4);
insert into product (manu_id, _name, _type, price) values (4, 'Island Oasis - Magarita Mix', 'food', 39.2);
insert into product (manu_id, _name, _type, price) values (10, 'Energy - Boo - Koo', 'food', 9.9);

select * from product;


create table order_item(
id int primary key identity(1,1),
product_id int foreign key references product(id),
amount int not null,
price float not null,
order_id int foreign key references orders(id)
);


insert into order_item (product_id, amount, price, order_id) values (65, 1, 0, 63);
insert into order_item (product_id, amount, price, order_id) values (92, 15, 0, 67);
insert into order_item (product_id, amount, price, order_id) values (70, 16, 0, 59);
insert into order_item (product_id, amount, price, order_id) values (28, 17, 0, 70);
insert into order_item (product_id, amount, price, order_id) values (4, 14, 0, 66);
insert into order_item (product_id, amount, price, order_id) values (77, 6, 0, 78);
insert into order_item (product_id, amount, price, order_id) values (44, 16, 0, 71);
insert into order_item (product_id, amount, price, order_id) values (81, 14, 0, 59);
insert into order_item (product_id, amount, price, order_id) values (35, 2, 0, 64);
insert into order_item (product_id, amount, price, order_id) values (48, 16, 0, 69);
insert into order_item (product_id, amount, price, order_id) values (68, 3, 0, 60);
insert into order_item (product_id, amount, price, order_id) values (13, 19, 0, 51);
insert into order_item (product_id, amount, price, order_id) values (66, 19, 0, 69);
insert into order_item (product_id, amount, price, order_id) values (24, 4, 0, 49);
insert into order_item (product_id, amount, price, order_id) values (22, 10, 0, 61);
insert into order_item (product_id, amount, price, order_id) values (36, 10, 0, 75);
insert into order_item (product_id, amount, price, order_id) values (70, 12, 0, 47);
insert into order_item (product_id, amount, price, order_id) values (68, 7, 0, 80);
insert into order_item (product_id, amount, price, order_id) values (66, 4, 0, 75);
insert into order_item (product_id, amount, price, order_id) values (14, 13, 0, 59);
insert into order_item (product_id, amount, price, order_id) values (56, 13, 0, 59);
insert into order_item (product_id, amount, price, order_id) values (98, 14, 0, 65);
insert into order_item (product_id, amount, price, order_id) values (98, 4, 0, 67);
insert into order_item (product_id, amount, price, order_id) values (40, 20, 0, 46);
insert into order_item (product_id, amount, price, order_id) values (5, 1, 0, 60);
insert into order_item (product_id, amount, price, order_id) values (22, 8, 0, 52);
insert into order_item (product_id, amount, price, order_id) values (85, 11, 0, 57);
insert into order_item (product_id, amount, price, order_id) values (83, 11, 0, 60);
insert into order_item (product_id, amount, price, order_id) values (80, 17, 0, 73);
insert into order_item (product_id, amount, price, order_id) values (74, 2, 0, 48);
insert into order_item (product_id, amount, price, order_id) values (77, 4, 0, 42);
insert into order_item (product_id, amount, price, order_id) values (15, 7, 0, 41);
insert into order_item (product_id, amount, price, order_id) values (56, 5, 0, 69);
insert into order_item (product_id, amount, price, order_id) values (21, 14, 0, 41);
insert into order_item (product_id, amount, price, order_id) values (41, 18, 0, 77);
insert into order_item (product_id, amount, price, order_id) values (5, 7, 0, 75);
insert into order_item (product_id, amount, price, order_id) values (21, 10, 0, 47);
insert into order_item (product_id, amount, price, order_id) values (69, 5, 0, 60);
insert into order_item (product_id, amount, price, order_id) values (36, 18, 0, 56);
insert into order_item (product_id, amount, price, order_id) values (88, 5, 0, 48);
insert into order_item (product_id, amount, price, order_id) values (30, 4, 0, 42);
insert into order_item (product_id, amount, price, order_id) values (51, 3, 0, 78);
insert into order_item (product_id, amount, price, order_id) values (83, 17, 0, 61);
insert into order_item (product_id, amount, price, order_id) values (27, 1, 0, 71);
insert into order_item (product_id, amount, price, order_id) values (25, 19, 0, 75);
insert into order_item (product_id, amount, price, order_id) values (93, 15, 0, 69);
insert into order_item (product_id, amount, price, order_id) values (57, 18, 0, 42);
insert into order_item (product_id, amount, price, order_id) values (82, 16, 0, 79);
insert into order_item (product_id, amount, price, order_id) values (99, 20, 0, 64);
insert into order_item (product_id, amount, price, order_id) values (77, 3, 0, 56);
insert into order_item (product_id, amount, price, order_id) values (86, 2, 0, 48);
insert into order_item (product_id, amount, price, order_id) values (67, 19, 0, 52);
insert into order_item (product_id, amount, price, order_id) values (98, 2, 0, 63);
insert into order_item (product_id, amount, price, order_id) values (76, 13, 0, 68);
insert into order_item (product_id, amount, price, order_id) values (63, 7, 0, 43);
insert into order_item (product_id, amount, price, order_id) values (62, 10, 0, 49);
insert into order_item (product_id, amount, price, order_id) values (30, 17, 0, 78);
insert into order_item (product_id, amount, price, order_id) values (50, 7, 0, 53);
insert into order_item (product_id, amount, price, order_id) values (81, 3, 0, 58);
insert into order_item (product_id, amount, price, order_id) values (4, 4, 0, 74);
insert into order_item (product_id, amount, price, order_id) values (84, 12, 0, 53);
insert into order_item (product_id, amount, price, order_id) values (88, 15, 0, 56);
insert into order_item (product_id, amount, price, order_id) values (82, 20, 0, 58);
insert into order_item (product_id, amount, price, order_id) values (91, 6, 0, 62);
insert into order_item (product_id, amount, price, order_id) values (44, 5, 0, 60);
insert into order_item (product_id, amount, price, order_id) values (70, 7, 0, 75);
insert into order_item (product_id, amount, price, order_id) values (91, 4, 0, 56);
insert into order_item (product_id, amount, price, order_id) values (14, 6, 0, 61);
insert into order_item (product_id, amount, price, order_id) values (34, 14, 0, 63);
insert into order_item (product_id, amount, price, order_id) values (59, 3, 0, 76);
insert into order_item (product_id, amount, price, order_id) values (61, 8, 0, 79);
insert into order_item (product_id, amount, price, order_id) values (76, 8, 0, 76);
insert into order_item (product_id, amount, price, order_id) values (85, 15, 0, 59);
insert into order_item (product_id, amount, price, order_id) values (60, 8, 0, 63);
insert into order_item (product_id, amount, price, order_id) values (58, 18, 0, 58);
insert into order_item (product_id, amount, price, order_id) values (100, 14, 0, 67);
insert into order_item (product_id, amount, price, order_id) values (94, 6, 0, 55);
insert into order_item (product_id, amount, price, order_id) values (59, 16, 0, 51);
insert into order_item (product_id, amount, price, order_id) values (47, 16, 0, 78);
insert into order_item (product_id, amount, price, order_id) values (78, 2, 0, 64);
insert into order_item (product_id, amount, price, order_id) values (21, 4, 0, 61);
insert into order_item (product_id, amount, price, order_id) values (89, 4, 0, 78);
insert into order_item (product_id, amount, price, order_id) values (56, 8, 0, 70);
insert into order_item (product_id, amount, price, order_id) values (57, 8, 0, 68);
insert into order_item (product_id, amount, price, order_id) values (62, 6, 0, 64);
insert into order_item (product_id, amount, price, order_id) values (72, 14, 0, 45);
insert into order_item (product_id, amount, price, order_id) values (52, 15, 0, 67);
insert into order_item (product_id, amount, price, order_id) values (61, 12, 0, 58);
insert into order_item (product_id, amount, price, order_id) values (29, 15, 0, 69);
insert into order_item (product_id, amount, price, order_id) values (24, 19, 0, 68);
insert into order_item (product_id, amount, price, order_id) values (30, 3, 0, 72);
insert into order_item (product_id, amount, price, order_id) values (58, 3, 0, 58);
insert into order_item (product_id, amount, price, order_id) values (51, 3, 0, 53);
insert into order_item (product_id, amount, price, order_id) values (85, 18, 0, 74);
insert into order_item (product_id, amount, price, order_id) values (22, 18, 0, 68);

select * from order_item where order_id = 43;

select * from order_item;

update order_item 
set order_item.price = amount * product.price
from product inner join order_item on order_item.product_id = product.id;


create table orders(
	id int primary key identity(1,1),
	number int not null,
	total_price float not null
);


alter table orders 
	add constraint total_priceDF default 0 for total_price;

alter table orders 
	add _date date not null default('1.1.2023');

alter table orders 
	add paid bit not null default(1);

insert into orders (number) values (16844);
insert into orders (number) values (19219);
insert into orders (number) values (16664);
insert into orders (number) values (15152);
insert into orders (number) values (14957);
insert into orders (number) values (14295);
insert into orders (number) values (16403);
insert into orders (number) values (15310);
insert into orders (number) values (11714);
insert into orders (number) values (12112);
insert into orders (number) values (14991);
insert into orders (number) values (11959);
insert into orders (number) values (16050);
insert into orders (number) values (12519);
insert into orders (number) values (19223);
insert into orders (number) values (13160);
insert into orders (number) values (11915);
insert into orders (number) values (18037);
insert into orders (number) values (17436);
insert into orders (number) values (11745);
insert into orders (number) values (14556);
insert into orders (number) values (17923);
insert into orders (number) values (14824);
insert into orders (number) values (12736);
insert into orders (number) values (16575);
insert into orders (number) values (15615);
insert into orders (number) values (18135);
insert into orders (number) values (11405);
insert into orders (number) values (16561);
insert into orders (number) values (14208);
insert into orders (number) values (12521);
insert into orders (number) values (19236);
insert into orders (number) values (14496);
insert into orders (number) values (11071);
insert into orders (number) values (11556);
insert into orders (number) values (12410);
insert into orders (number) values (18801);
insert into orders (number) values (13959);
insert into orders (number) values (12838);
insert into orders (number) values (12914);

select * from orders order by id;
select sum(orders.total_price) as priceOfAllOrders from orders;


update orders 
set orders.total_price = subselect.total_price 
from (select sum(order_item.price) as total_price,orders.id from order_item inner join orders on orders.id = order_item.order_id group by orders.id) subselect
where subselect.id = orders.id;

update orders set orders.total_price = sum(order_item.price) from order_item;

delete from orders where id < 41;