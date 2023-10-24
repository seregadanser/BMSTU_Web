
use warehouse
go

drop trigger add_login_and_role



CREATE TRIGGER add_login_and_role
ON dbo.Persons
AFTER INSERT
AS
BEGIN
  DECLARE @login_name NVARCHAR(100);
  declare @ppp NVARCHAR(100);
  DECLARE @password NVARCHAR(100);
  DECLARE @passwordCON NVARCHAR(100);
  DECLARE @role_name NVARCHAR(100);
  
  
  SELECT @login_name = inserted.[Login], @ppp = inserted.[password], @role_name = inserted.Position
  FROM inserted;

  SELECT @passwordCON = value FROM STRING_SPLIT(@ppp, ',') 
  set @password =  REPLACE(@ppp, ','+@passwordCON, '') 
  print(@password)
  print(@passwordCON)
  update dbo.Persons set [password] = @password where [Login] = @login_name

DECLARE @sql NVARCHAR(MAX) = N'CREATE LOGIN ' + QUOTENAME(@login_name) + N' WITH PASSWORD = ' + QUOTENAME(@passwordCON, '''') + N';';
EXEC sp_executesql @sql;
DECLARE @sql1 NVARCHAR(MAX) = N'create user ' + QUOTENAME(@login_name) + N' for login ' + QUOTENAME(@login_name);
EXEC sp_executesql @sql1;
DECLARE @sql2 NVARCHAR(MAX) = case @role_name
when 'hradmin' then
   N'ALTER ROLE HRAdmin ADD MEMBER ' +  QUOTENAME(@login_name)
when 'admin' then
	N'ALTER ROLE WarehouseAdmin ADD MEMBER ' +  QUOTENAME(@login_name)
when 'worker' then
	N'ALTER ROLE Worker ADD MEMBER ' +  QUOTENAME(@login_name)
when 'warehouseman' then
	N'ALTER ROLE Warehouseman ADD MEMBER ' +  QUOTENAME(@login_name)
end
EXEC sp_executesql @sql2;
END;
go

CREATE TRIGGER delete_login_and_role
ON dbo.Persons
AFTER DELETE
AS
BEGIN
  DECLARE @login_name NVARCHAR(100);
  DECLARE @role_name NVARCHAR(100);
  
  -- Get the login name and role name from the deleted row
  SELECT @login_name = deleted.Login, @role_name = deleted.Position
  FROM deleted;

    
DECLARE @sql2 NVARCHAR(MAX) = case @role_name
when 'hradmin' then
   N'ALTER ROLE HRAdmin drop MEMBER ' +  QUOTENAME(@login_name)
when 'admin' then
	N'ALTER ROLE WarehouseAdmin drop MEMBER ' +  QUOTENAME(@login_name)
when 'worker' then
	N'ALTER ROLE Worker drop MEMBER ' +  QUOTENAME(@login_name)
when 'warehouseman' then
	N'ALTER ROLE Warehouseman drop MEMBER ' +  QUOTENAME(@login_name)
end
 EXEC sp_executesql @sql2;
 DECLARE @sql1 NVARCHAR(MAX) = N'drop user ' + QUOTENAME(@login_name);
EXEC sp_executesql @sql1;
  DECLARE @sql NVARCHAR(MAX) = N'drop LOGIN ' + QUOTENAME(@login_name);
EXEC sp_executesql @sql;

END;
go


CREATE TRIGGER delete_inventory
ON warehouse2.InventoryProduct
instead of DELETE
AS
BEGIN
  DECLARE @number int;
  DECLARE @prod_id int;
  
  SELECT @number = deleted.inventory_number, @prod_id = deleted.product_Id
  FROM deleted;

  delete warehouse2.PlaceofObject where warehouse2.PlaceofObject.InventoryId = @number
  delete warehouse2.InventoryProduct where warehouse2.InventoryProduct.inventory_number = @number
  update warehouse2.Products 
  set   warehouse2.Products.value = warehouse2.Products.value-1
  where warehouse2.Products.Id = @prod_id 

  declare @value int 
  select @value = warehouse2.Products.value from warehouse2.Products where warehouse2.Products.Id = @prod_id

  if @value = 0
	delete warehouse2.Products where warehouse2.Products.Id = @prod_id

END;
go


--enable TRIGGER delete_login_and_role
--ON dbo.Persons


--delete dbo.Persons where Login = 'hradminn'
--select * from dbo.Persons