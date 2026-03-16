using System;
using System.Collections.Generic;
using System.Text;

namespace PrimumCore.Entities
{
    public class PageResult<TEntity>
    {
        public required IEnumerable<TEntity> Items { get; set; }
        public required int TotalItemsCount { get; set; }
        public required int TotalPages { get; set; }
        public required int CurrentPage { get; set; }
    }
}
