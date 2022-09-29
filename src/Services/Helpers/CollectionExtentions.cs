using Domain.Configurations;
using System.Linq;

namespace Services.Helpers
{
    public static class CollectionExtentions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params) =>
            @params.PageIndex > 0 && @params.PageSize >= 0
                ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : source;
    }
}
