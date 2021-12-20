using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.MappingConfiguration
{
    public class Mapeador
    {
        public static MapperConfiguration getMapper<TMapperProfile>() where TMapperProfile : Profile, new()
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile<TMapperProfile>(); });
        }
        public static MapperConfiguration getMapper<TSource, TDest>(Profile mapperProfile = null)
        {
            if (mapperProfile == null)
            {
                return new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDest>(); });
            }
            return new MapperConfiguration(cfg => { cfg.AddProfile(mapperProfile); });
        }
        public static IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> origen, Profile mapperProfile = null)
        {
            return getMapper<TSource, TDest>(mapperProfile).CreateMapper().Map<IEnumerable<TDest>>(origen);
        }
        public static IEnumerable<TDest> Map<TSource, TDest, TMapperProfile>(IEnumerable<TSource> origen) where TMapperProfile : Profile, new()
        {
            return getMapper<TMapperProfile>().CreateMapper().Map<IEnumerable<TDest>>(origen);
        }
        public static TDest Map<TSource, TDest>(TSource origen, Profile mapperProfile = null)
        {
            return getMapper<TSource, TDest>(mapperProfile).CreateMapper().Map<TDest>(origen);
        }
        public static TDest Map<TSource, TDest, TMapperProfile>(TSource origen) where TMapperProfile : Profile, new()
        {
            return getMapper<TMapperProfile>().CreateMapper().Map<TDest>(origen);
        }
        public static IEnumerable<TDest> Map<TDest>(IEnumerable<object> origen, Profile mapperProfile, Type OriginType) where TDest : class
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(mapperProfile); }).CreateMapper().Map(origen, OriginType, typeof(TDest)) as IEnumerable<TDest>;
        }
        public static TDest Map<TDest>(object origen, Profile mapperProfile, Type OriginType) where TDest : class
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(mapperProfile); }).CreateMapper().Map(origen, OriginType, typeof(TDest)) as TDest;
        }
        public static IEnumerable<object> Map(IEnumerable<object> origen, Profile mapperProfile, Type OriginType, Type DestType)
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(mapperProfile); }).CreateMapper().Map(origen, OriginType, DestType) as IEnumerable<object>;
        }
        public static object Map(object origen, Profile mapperProfile, Type OriginType, Type DestType)
        {
            return new MapperConfiguration(cfg => { cfg.AddProfile(mapperProfile); }).CreateMapper().Map(origen, OriginType, DestType);
        }
    }
}
