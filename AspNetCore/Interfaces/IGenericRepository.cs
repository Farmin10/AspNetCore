using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity:class,new ()
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        List<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
