using MarsRover.Models;
using MarsRover.Models.Enums;
using MarsRover.Business.Commands.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Commands.Concrete
{
    public class MoveForwardCommand : IDiscoveryCommand
    {
        public void Discover(RoverLocation location)
        {
            switch (location.Direction)
            {
                case CompassDirection.N:
                    location.CoordinateY += 1;
                    break;
                case CompassDirection.S:
                    location.CoordinateY -= 1;
                    break;
                case CompassDirection.E:
                    location.CoordinateX += 1;
                    break;
                case CompassDirection.W:
                    location.CoordinateX -= 1;
                    break;
            }
        }
    }
}
