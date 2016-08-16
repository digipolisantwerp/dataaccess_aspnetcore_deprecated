using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SampleApp.Entities
{
    public class Appartment: EntityBase
    {
        public int Number { get; set; }
        public int Floor { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
