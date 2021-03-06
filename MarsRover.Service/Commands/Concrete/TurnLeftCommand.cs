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
    public class TurnLeftCommand : IDiscoveryCommand
    {
        public void Discover(RoverLocation location)
        {
            switch (location.Direction)
            {
                case CompassDirection.N:
                    location.Direction = CompassDirection.W;
                    break;
                case CompassDirection.W:
                    location.Direction = CompassDirection.S;
                    break;
                case CompassDirection.S:
                    location.Direction = CompassDirection.E;
                    break;
                case CompassDirection.E:
                    location.Direction = CompassDirection.N;
                    break;
            
            }

        }
    }
}
