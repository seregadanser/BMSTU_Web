using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DB_course;
using DB_course.Models;
using DB_course.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private AHRAdminModel model;
        IConnection _connection;
        private int a = 0;
        public PersonsController(IConfigurationRoot config) 
        {
            _connection = ConnectionBuilder.CreateMSSQLconnection(config);
            model = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(_connection)));
        }
        [HttpGet]
        public Pagination<DB_course.Models.DBModels.PersonNoPassword> GetPersons()
        {
            //string[] p = {"p1", "p2" };
            Pagination<DB_course.Models.DBModels.PersonNoPassword> p;
            p.
            DB_course.Models.DBModels.PersonNoPassword[] p = { new DB_course.Models.DBModels.PersonNoPassword { Name = "aa", SecondName = "bb" } };
            
            return p;
        }

        [HttpGet("{id}")]
        public DB_course.Models.DBModels.PersonNoPassword GetPerson(int id)
        {
            //string[] p = {"p1", "p2" };
            DB_course.Models.DBModels.PersonNoPassword[] p = { new DB_course.Models.DBModels.PersonNoPassword { Name = "aa", SecondName = "bb" } };
            return p[id];
        }
    }


    [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    [ApiController]
    public class AController : ControllerBase
    {
        private AHRAdminModel model;
        IConnection _connection;
        private int a = 0;
        public AController(IConfigurationRoot config)
        {
            _connection = ConnectionBuilder.CreateMSSQLconnection(config);
            model = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(_connection)));
        }
        [HttpGet]
        public int GetPersons()             {
            //string[] p = {"p1", "p2" };
            //DB_course.Models.DBModels.PersonNoPassword[] p = { new DB_course.Models.DBModels.PersonNoPassword { Name = "aa", SecondName = "bb" } };
            return 0;
        }
    }
}
