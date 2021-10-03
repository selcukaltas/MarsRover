using MarsRover.Business.Exceptions;
using MarsRover.Business.Factory.Abstract;
using MarsRover.Business.Services.Abstract;
using MarsRover.Models;
using MarsRover.Models.Enums;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsRover.Business.Constants.Constants;

namespace MarsRover.Business.Services.Concrete
{
    public class RoverService : IRoverService
    {
        private Rover CurrentRover { get; set; }
        private readonly IDiscoveryFactory _discoveryFactory;
        public RoverService(IDiscoveryFactory discoveryFactory)
        {
            _discoveryFactory = discoveryFactory;
        }
        public Rover Initalize(Plateau plateau, RoverLocation roverLocation)
        {
            Rover rover = new Rover();
            var validateLocations = ValidateLocation(plateau, roverLocation);
            if (validateLocations)
            {
                rover.Id = Guid.NewGuid();
                rover.RoverLocation.CoordinateX = roverLocation.CoordinateX;
                rover.RoverLocation.CoordinateY = roverLocation.CoordinateY;
                rover.RoverLocation.Direction = roverLocation.Direction;
                CurrentRover = rover;
                Log.Information(Messages.RoverInitialized);
            }
            return rover;
        }

        public bool ValidateLocation(Plateau plateau, RoverLocation roverLocation)
        {
            bool isValidDirection = IsValidDirection(roverLocation.Direction);
            bool isValidXCoordinate = IsValidXCoordinate(plateau.MaxCoordinateX, roverLocation.CoordinateX);
            bool isValidYCoordinate = IsValidYCoordinate(plateau.MaxCoordinateY, roverLocation.CoordinateY);
            if (isValidDirection && isValidXCoordinate && isValidYCoordinate)
            {
                Log.Information($"Given location ({roverLocation.CoordinateX}-{roverLocation.CoordinateY}-{roverLocation.Direction}) for the rover is valid on the given plateau ({plateau.MaxCoordinateX}-{plateau.MaxCoordinateY}).");
                return true;
            }
            else
            {
                ValidateLocationException ex = new ValidateLocationException();
                Log.Error(ex.Message);
                throw ex;

            }
        }

        private bool IsValidYCoordinate(int maxCoordinateY, int coordinateY)
        {
            return maxCoordinateY >= 0 && coordinateY <= maxCoordinateY && coordinateY >= 0;
        }

        private bool IsValidXCoordinate(int maxCoordinateX, int coordinateX)
        {
            return maxCoordinateX >= 0 && coordinateX <= maxCoordinateX && coordinateX >= 0;
        }
        public Rover GetCurrentRover()
        {
            Rover model = new Rover();

            if (CurrentRover != null)
            {
                model = CurrentRover;
            }

            return model;
        }
        private bool IsValidDirection(CompassDirection direction)
        {
            if (direction == CompassDirection.E || direction == CompassDirection.N || direction == CompassDirection.W || direction == CompassDirection.S)
            {
                return true;
            }
            return false;
        }
        public RoverLocation DiscoverPlateau(ControlDirection direction)
        {
            _discoveryFactory.ExecuteControlDirection(direction).Discover(CurrentRover.RoverLocation);
            if (CurrentRover.RoverLocation.CoordinateX < 0 || CurrentRover.RoverLocation.CoordinateY < 0)
            {
                Log.Information($"Rover location is ({CurrentRover.RoverLocation.CoordinateX} {CurrentRover.RoverLocation.CoordinateY} {CurrentRover.RoverLocation.Direction}) Danger Rover moved out of the plateau");
                return new RoverLocation() { CoordinateX = CurrentRover.RoverLocation.CoordinateX, CoordinateY = CurrentRover.RoverLocation.CoordinateY,Direction=CurrentRover.RoverLocation.Direction };
            }
            Log.Information($"Rover location is ({CurrentRover.RoverLocation.CoordinateX} {CurrentRover.RoverLocation.CoordinateY} {CurrentRover.RoverLocation.Direction})");
            return new RoverLocation() { CoordinateX = CurrentRover.RoverLocation.CoordinateX, CoordinateY = CurrentRover.RoverLocation.CoordinateY,Direction=CurrentRover.RoverLocation.Direction };

        }
    }

}
