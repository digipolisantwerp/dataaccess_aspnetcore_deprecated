using System;
using System.Collections.Generic;
using Toolbox.DataAccess.Entities;

namespace Toolbox.DataAccess.Paging
{
    public class DataPage<TEntity> where TEntity : EntityBase
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalCount { get; set; }
    }
}
