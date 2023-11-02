using DB_course.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DB_course.Models.DBModels;
using DB_course.Models.CompositModels;

namespace WebApplication1.Models
{
    public partial class PersonNoPassword
    {
        public int Id { get; set; }  
        public string Login { get; set; } = null!;
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Position { get; set; }
        public DateTime? DateOfBirthday { get; set; }
        public int? NumberOfCome { get; set; }
    }

    public partial class PersonNoId
    {
        public string Login { get; set; } = null!;
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Position { get; set; }
        public DateTime? DateOfBirthday { get; set; }
        public int? NumberOfCome { get; set; }
        public string? Password { get; set; }
    }

    public partial class PlaceNoId
    {
        public int? NumberStay { get; set; }

        public int? NumberLayer { get; set; }

        public int? Size { get; set; }
    }

    public class AdminComposeShort              //IPrPlo
    {
        public int? ProductId { get; set; }

        public string? Name { get; set; }

        public DateTime? DateCome { get; set; }

        public DateTime? DateProduction { get; set; }

        public int? InventoryNumber { get; set; }

        public string? PlaceId { get; set; }
    }


    public static class PersonConverter
    {
        public static Person ConvertFromPersonNoPassword(PersonNoPassword personNoPassword)
        {
            return new Person
            {
                Id = personNoPassword.Id,
                Login = personNoPassword.Login,
                Name = personNoPassword.Name,
                SecondName = personNoPassword.SecondName,
                Position = personNoPassword.Position,
                DateOfBirthday = personNoPassword.DateOfBirthday,
                NumberOfCome = personNoPassword.NumberOfCome
            };
        }

        public static Person ConvertFromPersonNoId(PersonNoId personNoId)
        {
            return new Person
            {
                Login = personNoId.Login,
                Name = personNoId.Name,
                SecondName = personNoId.SecondName,
                Position = personNoId.Position,
                DateOfBirthday = personNoId.DateOfBirthday,
                Password = personNoId.Password,
                NumberOfCome = personNoId.NumberOfCome
            };
        }

        public static PersonNoPassword ConvertToPersonNoPassword(Person person)
        {
            return new PersonNoPassword
            {
                Id = person.Id,
                Login = person.Login,
                Name = person.Name,
                SecondName = person.SecondName,
                Position = person.Position,
                DateOfBirthday = person.DateOfBirthday,
                NumberOfCome = person.NumberOfCome
            };
        }

        public static PersonNoId ConvertToPersonNoId(Person person)
        {
            return new PersonNoId
            {
                Login = person.Login,
                Name = person.Name,
                SecondName = person.SecondName,
                Position = person.Position,
                DateOfBirthday = person.DateOfBirthday,
                Password = person.Password,
                NumberOfCome = person.NumberOfCome
            };
        }
    }

    public static class PlaceConverter
    {
        public static Place ConvertFromPlaceNoId(PlaceNoId placeNoId)
        {
            return new Place
            {
                NumberStay = placeNoId.NumberStay,
                NumberLayer = placeNoId.NumberLayer,
                Size = placeNoId.Size,
                Id = 0
            };
        }

        public static Place ConvertFromPlaceNoId(PlaceNoId placeNoId, int id)
        {
            return new Place
            {
                NumberStay = placeNoId.NumberStay,
                NumberLayer = placeNoId.NumberLayer,
                Size = placeNoId.Size,
                Id = id
            };
        }

        public static PlaceNoId ConvertToPlaceNoId(Place place)
        {
            return new PlaceNoId
            {
                NumberStay = place.NumberStay,
                NumberLayer = place.NumberLayer,
                Size = place.Size
            };
        }
    }


    public static class AdminComposeConverter
    {
        public static AdminCompose ConvertFromAdminComposeShort(AdminComposeShort adminComposeShort)
        {
            return new AdminCompose
            {
                ProductId = adminComposeShort.ProductId,
                Name = adminComposeShort.Name,
                DateCome = adminComposeShort.DateCome,
                DateProduction = adminComposeShort.DateProduction,
                InventoryNumber = adminComposeShort.InventoryNumber,
                PlaceId = adminComposeShort.PlaceId

            };
        }

        public static AdminComposeShort ConvertToAdminComposeShort(AdminCompose adminCompose)
        {
            return new AdminComposeShort
            {
                ProductId = adminCompose.ProductId,
                Name = adminCompose.Name,
                DateCome = adminCompose.DateCome,
                DateProduction = adminCompose.DateProduction,
                InventoryNumber = adminCompose.InventoryNumber,
                PlaceId = adminCompose.PlaceId
            };
        }
    }
}
