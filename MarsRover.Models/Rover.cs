using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class Rover
    {
        public Rover()
        {
            RoverLocation = new RoverLocation();
        }
        public Guid Id { get; set; }
        public RoverLocation RoverLocation { get; set; }
    }
}
