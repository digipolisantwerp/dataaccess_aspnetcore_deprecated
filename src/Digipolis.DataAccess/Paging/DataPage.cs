using System;
using System.Collections.Generic;
using Digipolis.DataAccess.Entities;

namespace Digipolis.DataAccess.Paging
{
    public class DataPage<TEntity>
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalCount { get; set; }
    }
}
