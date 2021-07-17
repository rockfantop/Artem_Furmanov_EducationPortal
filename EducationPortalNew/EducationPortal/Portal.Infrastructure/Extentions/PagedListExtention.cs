using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Extentions
{
    public static class PagedListExtention
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            where T : class
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException($"pageNumber = {pageNumber}. PageNumber cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"pageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            var subset = new List<T>();
            long totalCount = 0;
            if (query != null)
            {
                totalCount = await query.LongCountAsync();
                if (totalCount > 0)
                {
                    query = pageNumber == 1
                                ? query.Take(pageSize)
                                : query.Skip(((pageNumber - 1) * pageSize)).Take(pageSize);

                    subset = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
                }
            }

            return new PagedList<T>(pageNumber, pageSize, totalCount, subset);
        }
    }
}
