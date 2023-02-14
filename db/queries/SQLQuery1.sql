create database pv_project_dbObchod;
use pv_project_dbObchod;

create table country(
	id int primary key identity(1,1),
	name varchar(50) not null unique,
	population int not null check(population >= 1),
	size int not null check(size > 1)
);


create table country