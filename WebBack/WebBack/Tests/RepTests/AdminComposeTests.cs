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
    public class AdminComposeRepositoryTests
    {
        [Fact]
        public void Get_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new AdminComposeRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
        {
            new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
            new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
      
        };

            var placeOfObjects = new List<PlaceofObject>
        {
            new PlaceofObject { InventoryId = 1, PlaceId = 1, Id = 201 },
            new PlaceofObject { InventoryId = 1, PlaceId = 2, Id = 202 },

        };

            var products = new List<Product>
        {
            new Product { Id = 101, Name = "Product1", DateCome = DateTime.Now, DateProduction = DateTime.Now, Value = 5 },
            new Product { Id = 102, Name = "Product2", DateCome = DateTime.Now, DateProduction = DateTime.Now, Value = 10 },
     
        };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));
            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeOfObjects));
            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));

            //mockDb.Setup(x => x).Returns(mockWarehouseContext.Object);

            // Act
            var result = repository.Get("1");

            // Assert

            Assert.NotNull(result);
      
        }

        [Fact]
        public void GetList_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new AdminComposeRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
        {
            new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
            new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
            // Add more sample data as needed
        };

            var placeOfObjects = new List<PlaceofObject>
        {
            
            new PlaceofObject { InventoryId = 2, PlaceId = 1, Id = 202 },
            // Add more sample data as needed
        };

            var products = new List<Product>
        {
            new Product { Id = 101, Name = "Product1", DateCome = DateTime.Now, DateProduction = DateTime.Now, Value = 5 },
            new Product { Id = 102, Name = "Product2", DateCome = DateTime.Now, DateProduction = DateTime.Now, Value = 10 },
            // Add more sample data as needed
        };


            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));
            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeOfObjects));
            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));


            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
        }
    }

    // Extension method to convert a list to a DbSet for Moq
    public static class DbSetExtensions
    {
        public static DbSet<T> ToDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return dbSet.Object;
        }
    }

}
