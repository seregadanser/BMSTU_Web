﻿using DB_course.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Repositories.DBRepository
{
    public class PersonRepository : ISQLRepository<Person>
    {
        private WarehouseContext db;

        public IConnection DB { set {db = (WarehouseContext)value; } }

        public PersonRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
        }

        public void Create(Person item)
        {
            try
            {
                db.Persons.Add(item);
            }
            catch(Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
        }

        public void Delete(string key)
        {
            Person book = db.Persons.Find(key);
            if (book == null) throw new Exception("person not Exists");
                db.Persons.Remove(book);
        }


        public IEnumerable<Person> Get(string value)
        {
            var petList = new List<Person>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;

            return (from user in db.Persons
                    where EF.Functions.Like(user.Name!, value) || EF.Functions.Like(user.Login!, value)
                    select user).ToList();

        }

        public IEnumerable<Person> GetList()
        {
            return db.Persons.ToList();
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
            db.ChangeTracker.Clear();
        }

        public void Update(Person item)
        {
            db.Entry(item).State = EntityState.Modified;
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

    public class PersonNoPasswordRepository : ISQLRepository<PersonNoPassword>
    {
        private WarehouseContext db;

        public IConnection DB { set { db = (WarehouseContext)value; } }

        public PersonNoPasswordRepository(IConnection db)
        {
            this.db = (WarehouseContext)db;
        }

        public void Create(PersonNoPassword item)
        {
           
        }

        public void Delete(string key)
        {

        }


        public IEnumerable<PersonNoPassword> Get(string value)
        {
            var petList = new List<Person>();
            int petId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string petName = value;

            return (from I in db.Persons
                    where EF.Functions.Like(I.Name!, value) || EF.Functions.Like(I.Login!, value)
                    select new PersonNoPassword
                    {
                        Login = I.Login,
                        Name = I.Name,
                        SecondName = I.SecondName,
                        Position = I.Position,
                        DateOfBirthday = I.DateOfBirthday,
                        NumberOfCome = I.NumberOfCome
                    }).ToList();
        }

        public IEnumerable<PersonNoPassword> GetList()
        {
            var P = from I in db.Persons
                    select new PersonNoPassword
                    {
                        Login = I.Login,
                        Name = I.Name,
                        SecondName = I.SecondName,
                        Position = I.Position,
                        DateOfBirthday = I.DateOfBirthday,
                        NumberOfCome = I.NumberOfCome
                    };
            return P.ToList();
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                db.ChangeTracker.Clear();
                throw ex;
            }
            db.ChangeTracker.Clear();
        }

        public void Update(PersonNoPassword item)
        {

        }

        private bool disposed = false;



        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
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
