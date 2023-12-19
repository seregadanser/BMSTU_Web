using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBack.Tests.DataBuilder
{
    public class PersonBuilder
    {
        private readonly Person _person;

        public PersonBuilder()
        {
            _person = new Person();
        }

        public PersonBuilder WithId(int id)
        {
            _person.Id = id;
            return this;
        }

        public PersonBuilder WithLogin(string login)
        {
            _person.Login = login;
            return this;
        }

        public PersonBuilder WithName(string name)
        {
            _person.Name = name;
            return this;
        }

        public PersonBuilder WithSecondName(string secondName)
        {
            _person.SecondName = secondName;
            return this;
        }

        public PersonBuilder WithPosition(string position)
        {
            _person.Position = position;
            return this;
        }

        public PersonBuilder WithDateOfBirthday(DateTime? dateOfBirthday)
        {
            _person.DateOfBirthday = dateOfBirthday;
            return this;
        }

        public PersonBuilder WithPassword(string password)
        {
            _person.Password = password;
            return this;
        }

        public PersonBuilder WithNumberOfCome(int? numberOfCome)
        {
            _person.NumberOfCome = numberOfCome;
            return this;
        }

        public Person Build()
        {
            return _person;
        }
    }

    public static class PersonMother
    {
        public static Person CreateValidPerson()
        {
            return new PersonBuilder()
                .WithId(1)
                .WithLogin("john.doe")
                .WithName("John")
                .WithSecondName("Doe")
                .WithPosition("Developer")
                .WithDateOfBirthday(new DateTime(1990, 1, 1))
                .WithPassword("password123")
                .WithNumberOfCome(5)
                .Build();
        }
    }
}
