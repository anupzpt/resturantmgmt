using DMS.DAL.DatabaseContext;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace DMS.DAL.Repositories.GenericRepo
{
    public class GenericRepo<TEntity, TKey> : IGenericRepo<TEntity, TKey> where TEntity : class
    {
        protected IdentityEntities _db;
        protected DbSet<TEntity> _dbSet;
        protected IQueryable<TEntity> _Query;
        protected bool HasCreateDates = true;

        public GenericRepo()
        {
            _db = new IdentityEntities();
            _dbSet = _db.Set<TEntity>();
            _Query = _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetList()
        {
            _Query = _dbSet.AsQueryable();
            return _Query;
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            if (HasCreateDates) { entity = UpdateCreateDates(entity); }
            try {
                _dbSet.Add(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return true;
        }


        public virtual void AddList(List<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }
        public virtual async Task<bool> AnyData() => await _dbSet.AnyAsync();

        public virtual void Delete(TKey key)
        {
            //_dbSet.Attach(entity);
            //_dbSet.Remove(entity);
            dynamic data = GetById(key);
            data.IsDelete = true;
            _db.SaveChanges();
        }

        public virtual void DeleteData(TKey key)
        {
            TEntity entity = GetByIdSync(key);
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public virtual void DeleteList(List<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                // Delete(entity);
            }
        }

        public virtual void Edit(params TEntity[] entity)
        {
            foreach (TEntity item in entity)
            {
                if (HasCreateDates) { entity = UpdateDates(entity); }
                _db.Entry(item).State = EntityState.Modified;
            }
            _db.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null)
        {
            IQueryable<TEntity> query = _Query;
            if (filter != null)
                query = query.Where(filter);
            if (order != null)
                query = query.OrderBy(order);
            return await query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _Query.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(TKey id)
        {
            var data = await _dbSet.FindAsync(id);
            return data;
        }

        public virtual void Update(TEntity entity)
        {
            entity = UpdateDates(entity);
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        private dynamic UpdateDates(dynamic data)
        {
            //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
            data.UpdatedNepaliDate = SystemInfo.NepaliDate;
            data.UpdatedEnglishDate = SystemInfo.EnglishDate;
            //data.UpdatedBy = systemSession.UserId;
            return data;
        }

        private dynamic UpdateCreateDates(dynamic data)
        {
            //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
            if (data.Id == null || data.Id == "")
            {
                data.Id = Guid.NewGuid().ToString();
            }
            data.CreatedNepaliDate = SystemInfo.NepaliDate;
            data.CreatedEnglishDate = SystemInfo.EnglishDate;
            data.UpdatedNepaliDate = SystemInfo.NepaliDate;
            data.UpdatedEnglishDate = SystemInfo.NepaliDate;
            //data.UpdatedBy = systemSession.UserId;
            return data;
        }

        public virtual bool DBIsActive()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetFirstOrDefault()
        {
            var data = await _Query.FirstOrDefaultAsync();
            return data;
        }


        public virtual TEntity GetLastOrDefaultSync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null)
        {
            IQueryable<TEntity> query = _Query;
            if (filter != null)
                query = query.Where(filter);
            if (order != null)
                query = query.OrderByDescending(order);
            var data = query.Take(1);
            return (TEntity)data;
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> expression = null)
        {
            var query = _Query;
            if (expression != null)
            {
                return await _dbSet.Where(expression).AnyAsync();
            }
            return await _dbSet.AnyAsync();
        }

        public virtual void AddSync(TEntity entity)
        {
            //  entity = UpdateCreateDates(entity);
            _dbSet.Add(entity);
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {

                foreach (var valEx in dbex.EntityValidationErrors)
                {
                    foreach (var valEx2 in valEx.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", valEx2.PropertyName, valEx2.ErrorMessage);
                    }
                }
            }

        }

        public virtual List<TEntity> GetSync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TKey>> order = null)
        {
            IQueryable<TEntity> query = _Query;
            if (filter != null)
                query = query.Where(filter);
            if (order != null)
                query = query.OrderBy(order);
            return query.ToList();
        }

        public virtual List<TEntity> GetAllSync()
        {
            return _Query.ToList();
        }

        public virtual TEntity GetByIdSync(TKey id)
        {
            var data = _dbSet.Find(id);
            return data;
        }


        public virtual TEntity FirstOrDefaultSync(Expression<Func<TEntity, bool>> expression)
        {
            var data = _Query.FirstOrDefault(expression);
            return data;
        }

        public virtual TEntity GetFirstOrDefaultSync()
        {
            var data = _dbSet.FirstOrDefault();
            return data;
        }

        public virtual void UpdateSync(TEntity entity)
        {
            // entity = UpdateDates(entity);
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public virtual bool AnySys(Expression<Func<TEntity, bool>> expression = null)
        {
            var query = _dbSet;
            if (expression != null)
            {
                return _dbSet.Where(expression).Any();
            }
            return _dbSet.Any();
        }

        public virtual void RemoveAllTableData()
        {
            var getAll = _dbSet.ToList();
            _dbSet.RemoveRange(getAll);
            _db.SaveChanges();
        }

        public virtual void AddWithOutOtherField(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public virtual void DbDispose()
        {
            //_db.Dispose();
        }
    }
}