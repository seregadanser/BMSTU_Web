using DB_course.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Repositories.DBRepository
{
    public class PlaceRepository : ISQLRepository<Place>
    {

        private WarehouseContext db;

        public IConnection DB { set { db = (WarehouseContext)value; } }

        public PlaceRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
        }
        public void Create(Place item)
        {
            try
            {
                db.Places.Add(item);
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void Delete(string id)
        {
            Place book = db.Places.Find(Convert.ToInt32(id));
            if (book == null) throw new Exception("person not Exists");
            db.Places.Remove(book);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Place> Get(string value)
        {
            var petList = new List<Place>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;

            return (from user in db.Places
                    where user.Id == petId
                    select user).ToList();
        }

        public IEnumerable<Place> GetList()
        {
            db.ChangeTracker.Clear();
            return db.Places.ToList();
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
            db.ChangeTracker.Clear();
        }

        public void Update(Place item)
        {
            throw new NotImplementedException();
        }
    }
}
