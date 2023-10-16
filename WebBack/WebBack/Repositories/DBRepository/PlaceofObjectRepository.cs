using DB_course.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Repositories.DBRepository
{
    public class PlaceofObjectRepository : ISQLRepository<PlaceofObject>
    {
        private WarehouseContext db;

        public IConnection DB { set { db = (WarehouseContext)value; } }

        public PlaceofObjectRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
        }


        public void Create(PlaceofObject item)
        {
            try
            {
                db.PlaceofObjects.Add(item);
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void Delete(string key)
        {
            PlaceofObject book = db.PlaceofObjects.Find(Convert.ToInt32(key))
                ?? throw new Exception("person not Exists");
            db.PlaceofObjects.Remove(book);
        }



        public IEnumerable<PlaceofObject> Get(string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlaceofObject> GetList()
        {
            db.ChangeTracker.Clear();
            return db.PlaceofObjects.ToList();
        }

        public void Update(PlaceofObject item)
        {
            throw new NotImplementedException();
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


        private bool disposed = false;



        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
