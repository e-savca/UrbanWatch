using System.Linq.Expressions;

namespace UrbanWatchMVCWebApp.Services
{
    public static class Extensions
    {
        public static bool UseDatabase { get; set; }
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
        {
            if (UseDatabase)
            {
                return await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(source);
            }
            else
            {
                // in memory
                var result = source.ToList();
                return result;
            }
        }

        public static async Task<TSource?> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (UseDatabase)
            {
                return await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(source, predicate);
            }
            else
            {
                // in memory
                var result = source.ToList().FirstOrDefault(predicate.Compile());
                return result;

            }
        }
    }
}
