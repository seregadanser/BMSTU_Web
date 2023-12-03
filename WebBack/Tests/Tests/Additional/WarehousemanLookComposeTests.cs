using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.Additional
{
    public class WarehousemanLookComposeBuilder
    {
        private readonly WarehousemanLookCompose _warehousemanLookCompose;

        public WarehousemanLookComposeBuilder()
        {
            _warehousemanLookCompose = new WarehousemanLookCompose();
        }

        public WarehousemanLookComposeBuilder WithName(string? name)
        {
            _warehousemanLookCompose.Name = name;
            return this;
        }

        public WarehousemanLookComposeBuilder WithSecondName(string? secondName)
        {
            _warehousemanLookCompose.SecondName = secondName;
            return this;
        }

        public WarehousemanLookComposeBuilder WithLogin(string? login)
        {
            _warehousemanLookCompose.Login = login;
            return this;
        }

        public WarehousemanLookComposeBuilder WithInventoryNumber(int? inventoryNumber)
        {
            _warehousemanLookCompose.InventoryNumber = inventoryNumber;
            return this;
        }

        public WarehousemanLookComposeBuilder WithNumberStay(int? numberStay)
        {
            _warehousemanLookCompose.NumberStay = numberStay;
            return this;
        }

        public WarehousemanLookComposeBuilder WithNumberLayer(string? numberLayer)
        {
            _warehousemanLookCompose.NumberLayer = numberLayer;
            return this;
        }

        public WarehousemanLookComposeBuilder WithDateOfStart(DateTime? dateOfStart)
        {
            _warehousemanLookCompose.DateOfStart = dateOfStart;
            return this;
        }

        public WarehousemanLookCompose Build()
        {
            return _warehousemanLookCompose;
        }
    }

    public static class WarehousemanLookComposeMother
    {
        public static WarehousemanLookCompose CreateValidWarehousemanLookCompose()
        {
            return new WarehousemanLookComposeBuilder()
                .WithName("John")
                .WithSecondName("Doe")
                .WithLogin("john.doe")
                .WithInventoryNumber(123)
                .WithNumberStay(10)
                .WithNumberLayer("A")
                .WithDateOfStart(new DateTime(2020, 1, 1))
                .Build();
        }
    }

}
