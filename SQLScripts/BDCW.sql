

















delete warehouse2.Products
delete warehouse2.InventoryProduct
delete warehouse2.PlaceofObject
delete warehouse2.Place
delete warehouse2.Useful
delete dbo.Persons where  dbo.Persons.Login = 'hradminn' 

insert into warehouse2.Products values
(1, 'microscop', 2, null, null),
(2, 'LUPA', 1, null, null)


insert into warehouse2.InventoryProduct values
( 1, 1),
( 3, 1),
( 23, 2)

insert into warehouse2.Place values
(1, 1, 1, 50),
(2, 2, 3, 50),
(3, 2, 4, 50),
(4, 2, 5, 50),
(5, 2, 6, 50)

insert into warehouse2.PlaceofObject values
(5,2,5),
(2,1,3),  
(3,3,4),
(4,1,4)





insert into warehouse2.Useful values
( 1, 'f', null)
go



select * from warehouse2.Products
select * from warehouse2.InventoryProduct
select * from warehouse2.Place
select * from warehouse2.PlaceofObject
select * from dbo.Persons
select * from warehouse2.Useful

update warehouse2.Products
set warehouse2.Products.value = 1

go


--���������
select warehouse2.InventoryProduct.inventory_number, [name], dateCome, dateProduction
from warehouse2.InventoryProduct join warehouse2.Products on warehouse2.Products.Id = warehouse2.InventoryProduct.product_Id 
where warehouse2.InventoryProduct.inventory_number not in (select warehouse2.Useful.InventoryId from warehouse2.Useful)

--������� �������� Inventory �� Id ���������, ��� ���� ��������� ����� ������ ��������� � ������� Useful �� Id ������������ 
--Id ���������� ����� ��������������� ��� �����. ������������� �� ��������� ������� �� Inventory, Product

select * from (select warehouse2.InventoryProduct.inventory_number as IvN, [name], dateCome, dateProduction
from warehouse2.InventoryProduct join warehouse2.Products on
warehouse2.Products.Id = warehouse2.InventoryProduct.product_Id ) as K join warehouse2.Useful on
warehouse2.Useful.InventoryId = K.IvN where PersonId like 'f'


--���������
select  PE.[Name], PE.SecondName, PE.[Login], I.inventory_number, K.number_stay, K.number_layer, U.DateOfStart
from warehouse2.Useful as U 
join dbo.Persons as PE on PE.[Login] like U.PersonId 
join 
	(select PO.InventoryId, P.number_stay, P.number_layer from warehouse2.PlaceofObject as PO 
	 join warehouse2.Place as P on PO.PlaceId = P.Id) 
as K on K.InventoryId = U.InventoryId 
join warehouse2.InventoryProduct as I on I.inventory_number = U.InventoryId


--Useful ������� ������� �� Id ���������. ��������� ����� ������ ������� �� Useful �� Id ��� ���� ������������� �� 
--��������� �������� �� Useful, Person, Inventory, Place

--����� ������

