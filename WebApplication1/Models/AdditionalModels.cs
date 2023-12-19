using DB_course.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DB_course.Models.DBModels;
using DB_course.Models.CompositModels;
//using WebApplication1.Controllers;

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
        public string? DateOfBirthday { get; set; }
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

        public string? DateCome { get; set; }

        public string? DateProduction { get; set; }

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
                NumberOfCome = personNoPassword.NumberOfCome,
                Password = "qwerty"
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
                DateOfBirthday = DateTime.ParseExact(personNoId.DateOfBirthday, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Password = personNoId.Password ?? "ds",
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
               // DateOfBirthday = DateTime.ParseExact(person.DateOfBirthday, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
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
                DateCome = DateTime.ParseExact(adminComposeShort.DateCome, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
                DateProduction = DateTime.ParseExact(adminComposeShort.DateProduction, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture),
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
                DateCome = adminCompose.DateCome.ToString(),
                DateProduction = adminCompose.DateProduction.ToString(),
                InventoryNumber = adminCompose.InventoryNumber,
                PlaceId = adminCompose.PlaceId
            };
        }
    }
}
