using AutoMapper;
using VistaClaim.Domain.Entities._Base;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Common.Mappings
{
    public interface IMap<T>
    {
        void Mapping(Profile profile);
    }

    public abstract class Map<T> : IMap<T>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }

        //public class TestConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
        //{
        //    public TDestination Convert(TSource source, TDestination destination, ResolutionContext context)
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //}
    }
}
