using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using DB_course.Repositories.CompositRepository;
using Microsoft.EntityFrameworkCore;
using WebBack.Tests.Additional;

namespace WebBack.Tests.RepTests
{
    public class WarehousemanLookComposeRepositoryTests
    {
        [Fact]
        public void Get_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new WarehousemanLookComposeRepository(mockWarehouseContext.Object);

            var placeOfObjects = new List<PlaceofObject>
        {
            new PlaceofObject { InventoryId = 1, PlaceId = 1 },
            new PlaceofObject { InventoryId = 2, PlaceId = 2 },
        };

            var places = new List<Place>
        {
            new Place { Id = 1, NumberStay = 3, NumberLayer = 1 },
            new Place { Id = 2, NumberStay = 2, NumberLayer = 2 },
        };

            var usefuls = new List<Useful>
        {
            new Useful { InventoryId = 1, PersonId = "user1", DateOfStart = DateTime.Now },
            new Useful { InventoryId = 2, PersonId = "user2", DateOfStart = DateTime.Now },
        };

            var persons = new List<Person>
        {
            new Person { Login = "user1", Name = "John", SecondName = "Doe" },
            new Person { Login = "user2", Name = "Jane", SecondName = "Doe" },
        };

            var inventoryProducts = new List<InventoryProduct>
        {
            new InventoryProduct { InventoryNumber = 1 },
            new InventoryProduct { InventoryNumber = 2 },
        };

            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeOfObjects));
            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(places));
            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));
            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));
            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));

            // Act
            var result = repository.Get("user1");

            // Assert
            Assert.NotNull(result);
            // Add more specific assertions based on your expected results and business logic
        }

        [Fact]
        public void GetList_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new WarehousemanLookComposeRepository(mockWarehouseContext.Object);

            var placeOfObjects = new List<PlaceofObject>
        {
            new PlaceofObject { InventoryId = 1, PlaceId = 1 },
            new PlaceofObject { InventoryId = 2, PlaceId = 2 },
        };

            var places = new List<Place>
        {
            new Place { Id = 1, NumberStay = 3, NumberLayer = 1 },
            new Place { Id = 2, NumberStay = 2, NumberLayer = 2 },
        };

            var usefuls = new List<Useful>
        {
            new Useful { InventoryId = 1, PersonId = "user1", DateOfStart = DateTime.Now },
            new Useful { InventoryId = 2, PersonId = "user2", DateOfStart = DateTime.Now },
        };

            var persons = new List<Person>
        {
            new Person { Login = "user1", Name = "John", SecondName = "Doe" },
            new Person { Login = "user2", Name = "Jane", SecondName = "Doe" },
        };

            var inventoryProducts = new List<InventoryProduct>
        {
            new InventoryProduct { InventoryNumber = 1 },
            new InventoryProduct { InventoryNumber = 2 },
        };

            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeOfObjects));
            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(places));
            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));
            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));
            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            // Add more assertions based on your expected results
        }
    }
}
