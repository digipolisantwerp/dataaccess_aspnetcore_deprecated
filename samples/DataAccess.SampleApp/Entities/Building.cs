using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SampleApp.Entities
{
    public class Building : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Appartment> Appartments { get; set; }

    }
}
