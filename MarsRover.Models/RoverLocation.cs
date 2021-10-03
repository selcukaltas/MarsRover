using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class RoverLocation
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public CompassDirection Direction { get; set; }
    }
}
