using AutoMapper;
using marketplace.MappingConfiguration;

namespace marketplace.Repositories.Interfaces
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
            return Update(CustomMapper.Map<TDto, TEntity, TMapperProfile>(entidad));
        }
        #endregion
    }

}
