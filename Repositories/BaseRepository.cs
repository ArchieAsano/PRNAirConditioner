using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository <T> where T : class
    {
        private readonly AirConditionerShop2024DbContext _db;
        internal DbSet<T> dbSet;
        public BaseRepository(AirConditionerShop2024DbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();  

        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
            _db.SaveChanges();
        }
        public void Remove (T Entity)
        {
            dbSet.Remove(Entity);
            _db.SaveChanges();
        }
    }
}
