using Digipolis.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.SampleApp.Entities
{
    public class Room : EntityBase
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int NumberOfDoors { get; set; }
    }
}
