using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using DB_course.Repositories.DBRepository;
using Microsoft.EntityFrameworkCore;
using WebBack.Tests.Additional;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace WebBack.Tests.RepTests
{
    public class ProductRepositoryTests
    {
        [AllureXunit]
        public void Create_AddsProduct()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new ProductRepository(mockWarehouseContext.Object);

            var product = new Product
            {
                Id = 1,
                Name = "Product1",
                Value = 50,
                DateCome = DateTime.Now,
                DateProduction = DateTime.Now
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(new List<Product>()));

            // Act
            repository.Create(product);

            // Assert
            mockWarehouseContext.Verify(x => x.Products.Add(It.IsAny<Product>()), Times.Once);
           // mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Delete_RemovesProduct()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new ProductRepository(mockWarehouseContext.Object);

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Value = 50, DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 2, Name = "Product2", Value = 60, DateCome = DateTime.Now, DateProduction = DateTime.Now },
            };

            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.Products.Remove(It.IsAny<Product>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Get_ReturnsListOfProductsMatchingCriteria()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new ProductRepository(mockWarehouseContext.Object);

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Value = 50, DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 2, Name = "Product2", Value = 60, DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 3, Name = "Product3", Value = 70, DateCome = DateTime.Now, DateProduction = DateTime.Now },
            };

            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));

            // Act
            var result = repository.Get("2");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Product>>(result);
            Assert.Single(result); // Assuming you expect one result for the provided criteria
            Assert.Equal(2, result.First().Id); // Assuming you expect the product with Id = 2
        }

        [AllureXunit]
        public void GetList_ReturnsListOfProducts()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();
            var repository = new ProductRepository(mockWarehouseContext.Object);

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Value = 50, DateCome = DateTime.Now, DateProduction = DateTime.Now },
                new Product { Id = 2, Name = "Product2", Value = 60, DateCome = DateTime.Now, DateProduction = DateTime.Now },
            };

            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(products));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Product>>(result);
            Assert.Equal(products.Count, result.Count());
        }

        [AllureXunit]
        public void Update_UpdatesProduct()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();
            var repository = new ProductRepository(mockWarehouseContext.Object);

            var product = new Product
            {
                Id = 1,
                Name = "UpdatedProduct",
                Value = 75,
                DateCome = DateTime.Now,
                DateProduction = DateTime.Now
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.Products).Returns(() => DbSetExtensions.ToDbSet(new List<Product> { product }));

            // Act
            repository.Update(product);

            // Assert
            mockWarehouseContext.Verify(x => x.Entry(It.IsAny<Product>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
