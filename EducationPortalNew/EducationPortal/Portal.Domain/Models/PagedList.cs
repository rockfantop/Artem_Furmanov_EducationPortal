using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Models
{
    public class PagedList<TEntity>
        where TEntity : class
    {
        public int PageCount { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        public long TotalItemCount { get; }

        public IEnumerable<TEntity> Items { get; }

        public PagedList(int pageNumber, int pageSize, long totalItemCount, IEnumerable<TEntity> items)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.PageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            this.TotalItemCount = totalItemCount;
            this.Items = items;
        }
    }
}
