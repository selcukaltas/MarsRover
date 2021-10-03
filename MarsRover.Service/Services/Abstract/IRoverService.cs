using MarsRover.Models;
using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Services.Abstract
{
    public interface IRoverService
    {
        Rover Initalize(Plateau pleateu, RoverLocation roverLocation);
        bool ValidateLocation(Plateau plateau, RoverLocation roverLocation);
        Rover GetCurrentRover();
        RoverLocation DiscoverPlateau(ControlDirection direction);
    }
}
