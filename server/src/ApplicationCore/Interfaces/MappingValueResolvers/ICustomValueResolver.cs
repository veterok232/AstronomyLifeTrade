using AutoMapper;

namespace ApplicationCore.Interfaces.MappingValueResolvers;

public interface ICustomValueResolver<in TSource, in TDestination, TDestMember, TResolver>
    : IValueResolver<TSource, TDestination, TDestMember>
    where TResolver : IValueResolver<TSource, TDestination, TDestMember>
{
}