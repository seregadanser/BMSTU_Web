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
    public class UsefulRepositoryTests
    {
        [Fact]
        public void Create_AddsUseful()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new UsefulRepository(mockWarehouseContext.Object);

            var useful = new Useful
            {
                InventoryId = 1,
                PersonId = "user123",
                DateOfStart = DateTime.Now
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(new List<Useful>()));

            // Act
            repository.Create(useful);

            // Assert
            mockWarehouseContext.Verify(x => x.Usefuls.Add(It.IsAny<Useful>()), Times.Once);
          //  mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_RemovesUseful()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new UsefulRepository(mockWarehouseContext.Object);

            var usefuls = new List<Useful>
            {
                new Useful { InventoryId = 1, PersonId = "user123", DateOfStart = DateTime.Now },
                new Useful { InventoryId = 2, PersonId = "user456", DateOfStart = DateTime.Now }
            };

            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.Usefuls.Remove(It.IsAny<Useful>()), Times.Once);
          //  mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetList_ReturnsListOfUsefuls()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();
            var repository = new UsefulRepository(mockWarehouseContext.Object);

            var usefuls = new List<Useful>
            {
                new Useful { InventoryId = 1, PersonId = "user123", DateOfStart = DateTime.Now },
                new Useful { InventoryId = 2, PersonId = "user456", DateOfStart = DateTime.Now }
            };

            mockWarehouseContext.Setup(x => x.Usefuls).Returns(() => DbSetExtensions.ToDbSet(usefuls));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Useful>>(result);
            Assert.Equal(usefuls.Count, result.Count());
        }
    }
}
