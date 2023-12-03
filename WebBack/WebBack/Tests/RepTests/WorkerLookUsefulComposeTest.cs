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
    public class WorkerLookUsefulComposeRepositoryTests
    {
        [Fact]
        public void Get_ReturnsExpectedResult()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new WorkerLookUsefulComposeRepository(mockWarehouseContext.Object);

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
                new Useful { PersonId = "user1", InventoryId = 1, DateOfStart = DateTime.Now },
                new Useful { PersonId = "user2", InventoryId = 2, DateOfStart = DateTime.Now },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));
            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));
            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));

            // Act
            var result = repository.Get("user1");

            // Assert
            Assert.NotNull(result);
            // Add more assertions based on your expected results
        }
    }
}
