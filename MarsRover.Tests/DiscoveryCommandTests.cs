using FluentAssertions;
using MarsRover.Business.Commands.Abstract;
using MarsRover.Business.Commands.Concrete;
using MarsRover.Business.Factory.Abstract;
using MarsRover.Business.Services.Abstract;
using MarsRover.Business.Services.Concrete;
using MarsRover.Models;
using MarsRover.Models.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Tests
{
    public class DiscoveryCommandTests
    {
        private IRoverService _roverService;
        private IPlateauService _plateauService;
        private IDiscoveryCommand _discoveryCommandMock;
        private IDiscoveryFactory _discoveryFactoryMock;

        public DiscoveryCommandTests()
        {
            _plateauService = Mock.Of<PlateauService>();
            _discoveryCommandMock = Mock.Of<IDiscoveryCommand>();
            _discoveryFactoryMock = Mock.Of<IDiscoveryFactory>();
        }
        [Theory]
        [InlineData(5, 5, 1, 2, CompassDirection.W, CompassDirection.N)]
        [InlineData(2, 2, 0, 0, CompassDirection.N, CompassDirection.E)]
        [InlineData(8, 7, 8, 7, CompassDirection.E, CompassDirection.S)]
        [InlineData(4, 5, 3, 5, CompassDirection.S, CompassDirection.W)]
        public void RotateRightCommand_ValidPlateauValidRoverAndValidInstructions_ShouldBeEqualGivenResult(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection direction, CompassDirection result)
        {

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);
            _roverService = new RoverService(_discoveryFactoryMock);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };

            Rover rover = _roverService.Initalize(plateau, roverLocation);

            _discoveryCommandMock = new TurnRightCommand();

            _discoveryCommandMock.Discover(_roverService.GetCurrentRover().RoverLocation);

            _roverService.GetCurrentRover().RoverLocation.Direction.Should().Be(result);
        }
        [Theory]
        [InlineData(5, 5, 1, 2, CompassDirection.W, CompassDirection.S)]
        [InlineData(2, 2, 0, 0, CompassDirection.N, CompassDirection.W)]
        [InlineData(8, 7, 8, 7, CompassDirection.E, CompassDirection.N)]
        [InlineData(4, 5, 3, 5, CompassDirection.S, CompassDirection.E)]
        public void RotateLeftCommand_ValidPlateauValidRoverAndValidInstructions_ShouldBeEqualGivenResult(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection direction, CompassDirection result)
        {

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);
            _roverService = new RoverService(_discoveryFactoryMock);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };

            Rover rover = _roverService.Initalize(plateau, roverLocation);

            _discoveryCommandMock = new TurnLeftCommand();

            _discoveryCommandMock.Discover(_roverService.GetCurrentRover().RoverLocation);

            _roverService.GetCurrentRover().RoverLocation.Direction.Should().Be(result);
        }

    }
}
