use warehouse
go

create role Worker
grant select, insert, delete on warehouse2.Useful to Worker
grant select on warehouse2.Products to Worker
grant select on warehouse2.InventoryProduct to Worker
grant update on dbo.Persons to Worker

create role HRAdmin
grant select, insert, update, delete on dbo.Persons to HRAdmin 
grant select on warehouse2.Useful to HRAdmin
GRANT ALTER ANY ROLE TO HRAdmin;

create role Warehouseman
grant select, delete on warehouse2.Useful to Warehouseman
grant select on warehouse2.InventoryProduct to Warehouseman
grant select on warehouse2.Place to Warehouseman
grant select on warehouse2.PlaceofObject to Warehouseman
grant select on dbo.Persons to Warehouseman

create role WarehouseAdmin
grant select, insert, delete on warehouse2.Products to WarehouseAdmin
grant select, insert, delete on warehouse2.InventoryProduct to WarehouseAdmin
grant select, insert, delete on warehouse2.PlaceofObject to WarehouseAdmin
grant select, insert, delete on warehouse2.Place to WarehouseAdmin	
grant select on warehouse2.Useful to WarehouseAdmin
grant update on warehouse2.Place to WarehouseAdmin

create role NotLogin
grant select on  dbo.Persons to  NotLogin

use master 
go 
create login notautorise with password = '111111'
use warehouse
go

create user notautorise for login notautorise
alter role NotLogin add member notautorise

