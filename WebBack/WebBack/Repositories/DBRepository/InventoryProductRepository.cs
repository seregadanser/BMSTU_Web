using DB_course.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Repositories.DBRepository
{
    public class InventoryProductRepository : ISQLRepository<InventoryProduct>
    {
        private WarehouseContext db;

        public IConnection DB { set { db = (WarehouseContext)value; } }

        public InventoryProductRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
       
        }

        public void Create(InventoryProduct item)
        {
            try
            {
                db.InventoryProducts.Add(item);
            }
            catch (Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void Delete(string key)
        {
            InventoryProduct book = db.InventoryProducts.Find(Convert.ToInt32(key));
            if (book == null)
                throw new Exception("Inventory not Exists");
            db.InventoryProducts.Remove(book);
        }



        public IEnumerable<InventoryProduct> Get(string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryProduct> GetList()
        {
            db.ChangeTracker.Clear();
            return db.InventoryProducts.ToList();
        }



        public void Update(InventoryProduct item)
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
