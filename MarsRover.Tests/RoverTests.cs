using FluentAssertions;
using MarsRover.Business.Exceptions;
using MarsRover.Business.Factory.Abstract;
using MarsRover.Business.Factory.Concrete;
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
    public class RoverTests
    {
        private IRoverService _roverService;
        private IPlateauService _plateauService;
        private IDiscoveryFactory _discoveryFactory;
        public RoverTests()
        {
            _discoveryFactory = Mock.Of<DiscoveryFactory>();
            _plateauService = Mock.Of<PlateauService>();
            _roverService = new Mock<RoverService>(_discoveryFactory).Object;
        }
        [Theory]
        [InlineData(5, 5, 1, 2, CompassDirection.W)]
        [InlineData(2, 2, 0, 0, CompassDirection.N)]
        [InlineData(8, 7, 8, 7, CompassDirection.E)]
        [InlineData(4, 5, 3, 5, CompassDirection.S)]
        public void Initialize_ValidPlateauAndValidPositionWithValidDirection_ShouldReturnGivenLocation(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection direction)
        {
            _plateauService = new PlateauService();
            _roverService = new RoverService(_discoveryFactory);

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };

            Rover rover = _roverService.Initalize(plateau, roverLocation);

            rover.RoverLocation.Direction.Should().Equals(roverLocation.Direction);
            rover.RoverLocation.CoordinateY.Should().Equals(roverLocation.CoordinateY);
            rover.RoverLocation.CoordinateX.Should().Equals(roverLocation.CoordinateX);
        }
        [Theory]
        [InlineData(5, 5, 6, -1, CompassDirection.W)]
        [InlineData(2, 2, 4, 4, CompassDirection.N)]
        [InlineData(8, 7, 4, 15, CompassDirection.E)]
        [InlineData(4, 5, -2, 5, CompassDirection.S)]
        public void ValidateLocation_ValidPlateauAndOutsidePositionWithValidDirection_ThrowsValidateLocationException(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection direction)
        {

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = direction,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };

            Assert.Throws<ValidateLocationException>(() => _roverService.ValidateLocation(plateau, roverLocation));
        }
        [Theory]
        [InlineData(5, 5, 2, 2, CompassDirection.W, ControlDirection.L)]
        public void DiscoverPlateau_TurnLeftDirection_ShouldReturnGivenLocation(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection compass, ControlDirection command)
        {

            _plateauService = new PlateauService();
            _roverService = new RoverService(_discoveryFactory);

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = compass,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };
            Rover rover = _roverService.Initalize(plateau, roverLocation);

            RoverLocation lastLocation= _roverService.DiscoverPlateau(command);

            lastLocation.CoordinateX.Should().Be(2);
            lastLocation.CoordinateY.Should().Be(2);
            lastLocation.Direction.Should().Be(CompassDirection.S);
        }
        [Theory]
        [InlineData(5, 5, 2, 2, CompassDirection.W, ControlDirection.R)]
        public void DiscoverPlateau_TurnRightDirection_ShouldReturnGivenLocation(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection compass, ControlDirection command)
        {

            _plateauService = new PlateauService();
            _roverService = new RoverService(_discoveryFactory);

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = compass,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };
            Rover rover = _roverService.Initalize(plateau, roverLocation);

            RoverLocation lastLocation = _roverService.DiscoverPlateau(command);

            lastLocation.CoordinateX.Should().Be(2);
            lastLocation.CoordinateY.Should().Be(2);
            lastLocation.Direction.Should().Be(CompassDirection.N);
        }
        [Theory]
        [InlineData(5, 5, 2, 2, CompassDirection.W, ControlDirection.M)]
        public void DiscoverPlateau_MoveForwardDirection_ShouldReturnGivenLocation(int maxCoordinateX, int maxCoordinateY, int xPosition, int yPosition, CompassDirection compass, ControlDirection command)
        {

            _plateauService = new PlateauService();
            _roverService = new RoverService(_discoveryFactory);

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);

            RoverLocation roverLocation = new RoverLocation()
            {
                Direction = compass,
                CoordinateX = xPosition,
                CoordinateY = yPosition
            };
            Rover rover = _roverService.Initalize(plateau, roverLocation);

            RoverLocation lastLocation = _roverService.DiscoverPlateau(command);

            lastLocation.CoordinateX.Should().Be(1);
            lastLocation.CoordinateY.Should().Be(2);
            lastLocation.Direction.Should().Be(CompassDirection.W);
        }
    }

}
