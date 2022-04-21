using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GenericRepo
{
    public interface ICFGenericRepo<TEntity, TKey> where TEntity : class
    {
        Task<bool> Add(TEntity entity);
        void Update(TEntity entity);
        void Edit(params TEntity[] entity);
        void Delete(TKey key);
        void DeleteData(TKey key);
        void DeleteList(List<TEntity> entities);
        void AddList(List<TEntity> entities);
        Task<TEntity> GetById(TKey id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetFirstOrDefault();
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null);
        void AddSync(TEntity entity);
        TEntity GetByIdSync(TKey id);
        TEntity FirstOrDefaultSync(Expression<Func<TEntity, bool>> expression);
        TEntity GetFirstOrDefaultSync();
        TEntity GetLastOrDefaultSync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null);
        List<TEntity> GetAllSync();
        List<TEntity> GetSync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null);
        Task<bool> Any(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyData();
        void UpdateSync(TEntity entity);
        bool AnySys(Expression<Func<TEntity, bool>> expression = null);
        void RemoveAllTableData();

        void AddWithOutOtherField(TEntity entity);
        void DbDispose();
    }
}
