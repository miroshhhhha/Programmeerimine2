using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Models
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            page = Math.Max(page, 1);
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        // Для совместимости с моим новым кодом
        public static Task<PagedResult<T>> ToPagedResult<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            return GetPagedAsync(query, page, pageSize);
        }
    }
}
