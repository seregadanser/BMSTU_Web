using DB_course.Models.DBModels;
using DB_course.Models;
using DB_course.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Allure.Xunit.Attributes;

namespace DB_course.Tests
{
    public class AHRAdminModelTests
    {
        [AllureXunit]
        public void AddPerson_ValidPerson_CreatesPerson()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            AHRAdminModel adminModel = new HRAdminModel(mockUnitOfWork.Object);

            var person = new Person { Name = "John", SecondName = "Doe", Position = "Developer", DateOfBirthday = new DateTime(1985, 1, 1) };

            // Act
            Action act = () => adminModel.AddPerson(person);

            // Assert
            Assert.Throws<ValidationException>(act);


        }

        [AllureXunit]
        public void RemovePerson_ExistingId_DeletesPerson()
        {
            // Arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(a => a.PersonRepository).Returns(new Mock<IRepository<Person>>().Object);
            mockUnitOfWork.Setup(a => a.UsefulRepository).Returns(new Mock<IRepository<Useful>>().Object);
            AHRAdminModel adminModel = new HRAdminModel(mockUnitOfWork.Object);

            var existingId = "123";

            // Act
            adminModel.RemovePerson(existingId);

            // Assert
            mockUnitOfWork.Verify(x => x.PersonRepository.Delete(existingId), Times.Once);
            mockUnitOfWork.Verify(x => x.PersonRepository.Save(), Times.Once);
        }
        [AllureXunit]
        public void UpdatePerson_ValidIdAndPerson_PersonUpdated()
        {
            // Arrange
            var id = "1";
            var person = new Person
            {
                Login = "johndoe",
                Name = "John",
                SecondName = "Doe",
                Position = "Developer",
                Password = "password",
                DateOfBirthday = new DateTime(1990, 1, 1)
            };
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            unitOfWorkMock.SetupGet(uow => uow.PersonRepository).Returns(personRepositoryMock.Object);
            personRepositoryMock.Setup(r => r.Get(id)).Returns(new[] { person });
            var model = new HRAdminModel(unitOfWorkMock.Object);

            // Act
            model.UpdatePerson(id, new Person
            {
                Login = "janedoe",
                Name = "Jane",
                SecondName = "Doe",
                Position = "Manager",
                Password = "newpassword",
                DateOfBirthday = new DateTime(1991, 1, 1)
            });

            // Assert
            personRepositoryMock.Verify(r => r.Update(person), Times.Once);
            personRepositoryMock.Verify(r => r.Save(), Times.Once);
           // Assert.Equal("janedoe", person.Login);
            Assert.Equal("Jane", person.Name);
            Assert.Equal("Doe", person.SecondName);
            Assert.Equal("Manager", person.Position);
           // Assert.Equal("newpassword", person.Password);
            Assert.Equal(new DateTime(1991, 1, 1), person.DateOfBirthday);
        }


    }

}
