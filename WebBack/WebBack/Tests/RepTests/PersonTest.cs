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
    public class PersonRepositoryTests
    {
        [AllureXunit]
        public void Create_AddsPerson()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PersonRepository(mockWarehouseContext.Object);
            var persons = new List<Person>();
            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));
            var person = new Person
            {
                Id = 1,
                Login = "john.doe",
                Name = "John",
                SecondName = "Doe",
                Position = "Developer",
                DateOfBirthday = new DateTime(1990, 1, 1),
                Password = "password123",
                NumberOfCome = 5
                // Add other properties as needed
            };

            // Act
            repository.Create(person);

            // Assert
            mockWarehouseContext.Verify(x => x.Persons.Add(It.IsAny<Person>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Delete_RemovesPerson()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PersonRepository(mockWarehouseContext.Object);

            var persons = new List<Person>
            {
                new Person { Id = 1, Login = "john.doe" },
                new Person { Id = 2, Login = "jane.smith" },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));

            // Act
            repository.Delete("1");

            // Assert
            mockWarehouseContext.Verify(x => x.Persons.Remove(It.IsAny<Person>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [AllureXunit]
        public void Get_ReturnsPersons()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PersonRepository(mockWarehouseContext.Object);

            var persons = new List<Person>
            {
                new Person { Id = 1, Login = "john.doe" },
                new Person { Id = 2, Login = "jane.smith" },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));

            // Act
            var result = repository.Get("john.doe");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            // Add more assertions based on your expected results
        }

        [AllureXunit]
        public void GetList_ReturnsPersons()
        {
            // Arrange
            var mockWarehouseContext = new Mock<WarehouseContext>();
            var repository = new PersonRepository(mockWarehouseContext.Object);

            var persons = new List<Person>
            {
                new Person { Id = 1, Login = "john.doe" },
                new Person { Id = 2, Login = "jane.smith" },
                // Add more sample data as needed
            };

            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));

            // Act
            var result = repository.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(persons.Count, result.Count());
            // Add more assertions based on your expected results
        }

        [AllureXunit]
        public void Update_UpdatesPerson()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mockWarehouseContext = fixture.Freeze<Mock<WarehouseContext>>();//new Mock<WarehouseContext>();
            var repository = new PersonRepository(mockWarehouseContext.Object);

            var person = new Person
            {
                Id = 1,
                Login = "john.doe",
                Name = "John",
                SecondName = "Doe",
                Position = "Developer",
                DateOfBirthday = new DateTime(1990, 1, 1),
                Password = "password123",
                NumberOfCome = 5
                // Add other properties as needed
            };

            var persons = new List<Person>
            {
                person
            };

            mockWarehouseContext.Setup(x => x.Persons).Returns(() => DbSetExtensions.ToDbSet(persons));

            // Act
            repository.Update(person);

            // Assert
            mockWarehouseContext.Verify(x => x.Entry(It.IsAny<Person>()), Times.Once);
            //mockWarehouseContext.Verify(x => x.SaveChanges(), Times.Once);
        }

    }
}
