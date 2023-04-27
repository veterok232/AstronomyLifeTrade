using ApplicationCore.Interfaces.MappingValueResolvers;
using AutoMapper;

namespace ApplicationCore.Extensions;

public static class MemberConfigurationExpressionExtensions
{
    public static void MapWithCustomValueResolver<TSource, TDestination, TMember, TResolver>(
        this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
        TResolver resolver)
        where TResolver : IValueResolver<TSource, TDestination, TMember>
    {
        configurationExpression.MapFrom<ICustomValueResolver<TSource, TDestination, TMember, TResolver>>();
    }
}