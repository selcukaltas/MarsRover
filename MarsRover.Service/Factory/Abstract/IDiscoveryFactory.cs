using MarsRover.Business.Commands.Abstract;
using MarsRover.Models;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Factory.Abstract
{
    public interface IDiscoveryFactory
    {
        IDiscoveryCommand ExecuteControlDirection(ControlDirection direction);
    }
}
