create database warehouse
go

use warehouse
go

create schema warehouse2
go

create table warehouse2.Products 
(
Id int primary key, 
[Name] varchar(30), 
[value] int not null,
 dateCome date,
 dateProduction date
)
go

create table warehouse2.InventoryProduct
(
inventory_number int primary key, 
product_Id int REFERENCES  warehouse2.Products (Id)
)
go

create table warehouse2.Place
(
Id int primary key, 
number_stay int not null,
number_layer int not null, 
size int not null
)
go

create table warehouse2.PlaceofObject
(
Id int primary key, 
PlaceId int REFERENCES  warehouse2.Place (Id),
InventoryId int REFERENCES  warehouse2.InventoryProduct (inventory_number)
)
go

create table dbo.Persons
(
[Login] varchar(50) primary key,
[Name] varchar(50), 
SecondName varchar(50),
Position varchar(50),
DateOfBirthday date,
[password] varchar(50) unique,
number_of_come int
)
go

create table warehouse2.Useful
(
InventoryId int REFERENCES  warehouse2.InventoryProduct (inventory_number) primary key,
PersonId varchar(50) REFERENCES  dbo.Persons ([Login]),
DateOfStart date
)
go

create table warehouse2.History
(
Id int primary key,
InventoryId int REFERENCES  warehouse2.InventoryProduct (inventory_number),
PersonId varchar(50) REFERENCES  dbo.Persons ([Login]),
DateOfStart date,
DateOfEnd date
)
go


