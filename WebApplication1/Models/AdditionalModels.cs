using DB_course.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
}
