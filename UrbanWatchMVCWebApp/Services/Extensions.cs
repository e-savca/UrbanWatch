using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UrbanWatchMVCWebApp.Services
{
    public static class Extensions
    {
        public static bool UseDatabase { get; set; }
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
        {

            if (UseDatabase)
            {
                return await source.ToListAsync();
            }
            else
            {
                return source.ToList();
            }
        }

        public static async Task<TSource?> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (UseDatabase)
            {
                return await source.FirstOrDefaultAsync(predicate);
            }
            else
            {
                // in memory
                return source.ToList().FirstOrDefault(predicate.Compile());

            }
        }
    }
}
