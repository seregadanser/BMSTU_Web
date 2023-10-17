using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DB_course;
using DB_course.Models;
using DB_course.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "hradmin")]
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private AHRAdminModel model;
        IConnection _connection;
        Dictionary<string, IConnection> userConnections;
        private int a = 0;
        public PersonsController(Dictionary<string, IConnection> userConnections) 
        {
            this.userConnections = userConnections;
        }
        [HttpGet]
        public Pagination<DB_course.Models.DBModels.PersonNoPassword> GetPersons()
        {
            //string[] p = {"p1", "p2" };
            string bbb = User.Identity.Name;
            string userName = Request.Cookies["UserNameCookie"];
            //_connection = ConnectionBuilder.CreateMSSQLconnection(config);
            model = new HRAdminModel(new UnitOfWork(new SQLRepositoryAbstractFabric(userConnections[userName])));

            Pagination<DB_course.Models.DBModels.PersonNoPassword> p =new Pagination<DB_course.Models.DBModels.PersonNoPassword>();
            p.page = 1;
            p.total = 1;
            DB_course.Models.DBModels.PersonNoPassword[] pe = { new DB_course.Models.DBModels.PersonNoPassword { Name = "aa", SecondName = "bb" } };
            p.results = pe.ToList();
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


    [Authorize(Roles = "worker")]
    [Route("[controller]")]
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
