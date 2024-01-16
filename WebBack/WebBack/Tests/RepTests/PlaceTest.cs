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
    public class PlaceRepositoryTests
    {
        [AllureXunit]
        public void Create_AddsPlace()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PlaceRepository(mockWarehouseContext.Object);

            var place = new Place
            {
                Id = 1,
                NumberStay = 101,
                NumberLayer = 201,
                Size = 50
                // Add other properties as needed
            };

            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(new List<Place>()));

            // Act
            repository.Create(place);

            // Assert
            mockWarehouseContext.Verify(x => x.Places.Add(It.IsAny<Place>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Delete_RemovesPlace()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PlaceRepository(mockWarehouseContext.Object);

            var places = new List<Place>
            {
                new Place { Id = 1, NumberStay = 101, NumberLayer = 201, Size = 50 },
                new Place { Id = 2, NumberStay = 102, NumberLayer = 202, Size = 60 },
            };

            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(places));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.Places.Remove(It.IsAny<Place>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void GetList_ReturnsListOfPlaces()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();
            var repository = new PlaceRepository(mockWarehouseContext.Object);

            var places = new List<Place>
            {
                new Place { Id = 1, NumberStay = 101, NumberLayer = 201, Size = 50 },
                new Place { Id = 2, NumberStay = 102, NumberLayer = 202, Size = 60 },
            };

            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(places));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Place>>(result);
            Assert.Equal(places.Count, result.Count());
        }

        [AllureXunit]
        public void Get_ReturnsListOfPlacesMatchingCriteria()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PlaceRepository(mockWarehouseContext.Object);

            var places = new List<Place>
    {
        new Place { Id = 1, NumberStay = 101, NumberLayer = 201, Size = 50 },
        new Place { Id = 2, NumberStay = 102, NumberLayer = 202, Size = 60 },
        new Place { Id = 3, NumberStay = 103, NumberLayer = 203, Size = 70 },
    };

            mockWarehouseContext.Setup(x => x.Places).Returns(() => DbSetExtensions.ToDbSet(places));

            // Act
            var result = repository.Get("2");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Place>>(result);
            Assert.Single(result); // Assuming you expect one result for the provided criteria
            Assert.Equal(2, result.First().Id); // Assuming you expect the place with Id = 2
        }
    }
}
