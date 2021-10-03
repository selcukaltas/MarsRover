using MarsRover.Business.Commands.Abstract;
using MarsRover.Business.Commands.Concrete;
using MarsRover.Business.Factory.Abstract;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Factory.Concrete
{
    public class DiscoveryFactory : IDiscoveryFactory
    {
        IDiscoveryCommand discoveryCommand = null;
        public IDiscoveryCommand ExecuteControlDirection(ControlDirection direction)
        {
            switch (direction)
            {
                case ControlDirection.R:
                    discoveryCommand = new TurnRightCommand();
                    break;
                case ControlDirection.L:
                    discoveryCommand = new TurnLeftCommand();
                    break;
                case ControlDirection.M:
                    discoveryCommand = new MoveForwardCommand();
                    break;
            }
            return discoveryCommand;
        }
    }
}
