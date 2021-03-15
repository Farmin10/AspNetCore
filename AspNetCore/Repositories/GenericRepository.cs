using AspNetCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Repositories
{
    public class GenericRepository<TEntity> where TEntity:class,new()
    {
        public void Add(TEntity entity)
        {
            using (var contex = new AspContext())
            {
                contex.Set<TEntity>().Add(entity);
                contex.SaveChanges();
            }
        }
        public void Update(TEntity entity)
        {
            using (var contex = new AspContext())
            {
                contex.Set<TEntity>().Update(entity);
                contex.SaveChanges();
            }
        }
        public void Delete(TEntity entity)
        {
            using (var contex = new AspContext())
            {
                contex.Set<TEntity>().Remove(entity);
                contex.SaveChanges();
            }
        }
        public List<TEntity> GetAll()
        {
            using (var contex = new AspContext())
            {
                return contex.Set<TEntity>().ToList();

            }
        }
        public TEntity GetById(int id)
        {
            using (var contex = new AspContext())
            {
                return contex.Set<TEntity>().Find(id);
            }
        }
    }
}
