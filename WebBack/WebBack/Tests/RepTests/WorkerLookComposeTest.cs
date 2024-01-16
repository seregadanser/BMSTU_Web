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
    public class WorkerLookComposeRepositoryTests
    {
        [AllureXunit]
        public void Get_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new WorkerLookComposeRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
            {
                new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
                new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
                // Add more sample data as needed
            };

            var products = new List<Product>
            {
                new Product { Id = 101, Name = "Product1", DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 102, Name = "Product2", DateCome = DateTime.Now, DateProduction = DateTime.Now },
                // Add more sample data as needed
            };

            var usefuls = new List<Useful>
            {
                new Useful { InventoryId = 1 },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));
            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));
            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));

            // Act
            var result = repository.Get("Product1");

            // Assert
            Assert.NotNull(result);
            // Add more assertions based on your expected results
        }

        [AllureXunit]
        public void GetList_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new WorkerLookComposeRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
            {
                new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
                new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
                // Add more sample data as needed
            };

            var products = new List<Product>
            {
                new Product { Id = 101, Name = "Product1", DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 102, Name = "Product2", DateCome = DateTime.Now, DateProduction = DateTime.Now },
                // Add more sample data as needed
            };

            var usefuls = new List<Useful>
            {
                new Useful { InventoryId = 1 },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));
            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));
            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            // Add more assertions based on your expected results
        }
    }

}
