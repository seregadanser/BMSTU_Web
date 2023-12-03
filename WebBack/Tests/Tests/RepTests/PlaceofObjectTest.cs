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
    public class PlaceofObjectRepositoryTests
    {
        [Fact]
        public void Create_AddsPlaceofObject()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PlaceofObjectRepository(mockWarehouseContext.Object);

            var placeofObject = new PlaceofObject
            {
                Id = 1,
                PlaceId = 101,
                InventoryId = 201
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(new List<PlaceofObject>()));

            // Act
            repository.Create(placeofObject);

            // Assert
            mockWarehouseContext.Verify(x => x.PlaceofObjects.Add(It.IsAny<PlaceofObject>()), Times.Once);
          //  mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_RemovesPlaceofObject()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PlaceofObjectRepository(mockWarehouseContext.Object);

            var placeofObjects = new List<PlaceofObject>
            {
                new PlaceofObject { Id = 1, PlaceId = 101, InventoryId = 201 },
                new PlaceofObject { Id = 2, PlaceId = 102, InventoryId = 202 },
            };

            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeofObjects));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.PlaceofObjects.Remove(It.IsAny<PlaceofObject>()), Times.Once);
           // mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetList_ReturnsListOfPlaceofObjects()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();
            var repository = new PlaceofObjectRepository(mockWarehouseContext.Object);

            var placeofObjects = new List<PlaceofObject>
    {
        new PlaceofObject { Id = 1, PlaceId = 101, InventoryId = 201 },
        new PlaceofObject { Id = 2, PlaceId = 102, InventoryId = 202 },
    };

            mockWarehouseContext.Setup(x => x.PlaceofObjects).Returns(() => DbSetExtensions.ToDbSet(placeofObjects));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PlaceofObject>>(result);
            Assert.Equal(placeofObjects.Count, result.Count());
        }
    }
}
