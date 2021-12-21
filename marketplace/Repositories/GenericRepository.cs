using API.MappingConfiguration;
using AutoMapper;
using DelegateDecompiler;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace marketplace.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> AsNoTracking();
        IEnumerable<TEntity> All();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);

        #region "interfaces with DTO"

        IEnumerable<TDto> GetAll<TDto, TMapperProfile>() where TMapperProfile : Profile, new();
        TDto Get<TDto, TMapperProfile>(int id) where TMapperProfile : Profile, new();
        TEntity Add<TDto, TMapperProfile>(TDto entity) where TMapperProfile : Profile, new();
        TEntity Update<TDto, TMapperProfile>(TDto entidad) where TMapperProfile : Profile, new()
        {
            return Update(Mapeador.Map<TDto, TEntity, TMapperProfile>(entidad));
        }
        #endregion
    }

    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext context;
        protected DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> dbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = context.Set<TEntity>();
                }
                return _dbSet;
            }
            set
            {
                _dbSet = value;
            }
        }
        public GenericRepository(TContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all records as an IQueryable
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        public IQueryable<TEntity> GetQuery()
        {
            return dbSet;
        }

        /// <summary>
        /// Gets all records as an IQueryable and disables entity tracking
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        public IQueryable<TEntity> AsNoTracking()
        {
            return dbSet.AsNoTracking();
        }
        public IEnumerable<TEntity> All()
        {
            return GetQuery().ToList();
        }
        public TEntity Get(int id)
        {
            TEntity entity = dbSet.Find(id);
            return entity;
        }
        public TEntity Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public TEntity Update(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                context.SaveChanges();
            }
        }
        protected IQueryable<TEntity> QueriedListOnSet(Expression<Func<TEntity, bool>> filtro, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includes, ref IQueryable<TEntity> query)
        {
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (!String.IsNullOrEmpty(includes))
            {
                foreach (var include in includes.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            query = DecompileIfRequired(query);
            return query;
        }
        private static IQueryable<TEntity> DecompileIfRequired(IQueryable<TEntity> query)
        {
            var t = typeof(TEntity);
            var array = t.GetProperties();
            bool isComputed = false;
            int i = 0;
            while (isComputed == false && i < array.Length)
            {
                isComputed = Attribute.IsDefined(array[i++], typeof(DelegateDecompiler.ComputedAttribute));
            }
            if (isComputed)
            {
                query = query.Decompile();
            }
            return query;
        }

        #region "Métodos vía DTO"
        public IEnumerable<TDto> GetAll<TDto, TMapperProfile>() where TMapperProfile : Profile, new()
        {
            return Mapeador.Map<TEntity, TDto, TMapperProfile>(All());
        }
        public TDto Get<TDto, TMapperProfile>(int id) where TMapperProfile : Profile, new()
        {
            return Mapeador.Map<TEntity, TDto, TMapperProfile>(Get(id));
        }
        public TEntity Add<TDto, TMapperProfile>(TDto entity) where TMapperProfile : Profile, new()
        {
            return Add(Mapeador.Map<TDto, TEntity, TMapperProfile>(entity));
        }
        public TEntity Update<TDto, TMapperProfile>(TDto entity) where TMapperProfile : Profile, new()
        {
            return Update(Mapeador.Map<TDto, TEntity, TMapperProfile>(entity));
        }
        #endregion
    }
}
