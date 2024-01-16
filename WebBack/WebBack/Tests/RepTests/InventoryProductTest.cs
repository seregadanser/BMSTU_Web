using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using DB_course.Repositories.DBRepository;
using Microsoft.EntityFrameworkCore;
using WebBack.Tests.Additional;

namespace WebBack.Tests.RepTests
{
    public class InventoryProductRepositoryTests
    {
        [AllureXunit]
        public void Create_AddsInventoryProduct()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new InventoryProductRepository(mockWarehouseContext.Object);

            var IP = new List<InventoryProduct>();

            var inventoryProduct = new InventoryProduct
            {
                InventoryNumber = 1,
                ProductId = 101,
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(IP));

            // Act
            repository.Create(inventoryProduct);

            // Assert
            mockWarehouseContext.Verify(x => x.InventoryProducts.Add(It.IsAny<InventoryProduct>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Delete_RemovesInventoryProduct()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new InventoryProductRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
            {
                new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
                new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.InventoryProducts.Remove(It.IsAny<InventoryProduct>()), Times.Once);
           // mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void GetList_ReturnsInventoryProducts()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new InventoryProductRepository(mockWarehouseContext.Object);

            var inventoryProducts = new List<InventoryProduct>
            {
                new InventoryProduct { InventoryNumber = 1, ProductId = 101 },
                new InventoryProduct { InventoryNumber = 2, ProductId = 102 },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.InventoryProducts).Returns(() => DbSetExtensions.ToDbSet(inventoryProducts));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inventoryProducts.Count, result.Count());
            // Add more assertions based on your expected results
        }
    }
}
