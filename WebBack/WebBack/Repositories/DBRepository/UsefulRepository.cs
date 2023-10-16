using DB_course.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Repositories.DBRepository
{
    public class UsefulRepository : ISQLRepository<Useful>
    {

        private WarehouseContext db;

        public IConnection DB { set { db = (WarehouseContext)value; } }

        public UsefulRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
        }

        public void Create(Useful item)
        {
            try
            {
                db.Usefuls.Add(item);
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void Delete(string key)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(key);
            }
            catch { throw new Exception("unvalid key"); }
            Useful book = db.Usefuls.Find(id);
            if (book != null)
                db.Usefuls.Remove(book);
        }


        public IEnumerable<Useful> Get(string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Useful> GetList()
        {
            return db.Usefuls.ToList();
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

        public void Update(Useful item)
        {
            throw new NotImplementedException();
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
