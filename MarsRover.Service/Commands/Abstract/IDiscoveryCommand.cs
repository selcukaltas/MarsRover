using MarsRover.Models;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Commands.Abstract
{
    public interface IDiscoveryCommand
    {
        void Discover(RoverLocation rover);
    }
}
