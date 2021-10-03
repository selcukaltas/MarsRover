using FluentAssertions;
using MarsRover.Business.Exceptions;
using MarsRover.Business.Services.Abstract;
using MarsRover.Business.Services.Concrete;
using MarsRover.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRover.Tests
{
    public class PlateauTests
    {
        private IPlateauService _plateauService;

        public PlateauTests()
        {
            _plateauService = Mock.Of<PlateauService>();
        }

        [Theory]
        [InlineData(null)]
        public void Validate_IsNull_ThrowsValidatePlateauException(Plateau plateau)
        {
            Action act = () => _plateauService.Validate(plateau);

            act.Should().Throw<ValidatePlateauException>();

        }
        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        public void Validate_NotGreaterThanZero_ThrowsValidatePlateauException(int maxCoordinateX, int maxCoordinateY)
        {

            Plateau plateau = new Plateau();
            plateau.MaxCoordinateX = maxCoordinateX;
            plateau.MaxCoordinateY = maxCoordinateY;

            Action act = () => _plateauService.Validate(plateau);

            act.Should().Throw<ValidatePlateauException>();
        }
        [Theory]
        [InlineData(100, 100)]
        [InlineData(10, 100)]
        [InlineData(100, 10)]
        public void Validate_GreaterThanZero_ShouldReturnTrue(int maxCoordinateX, int maxCoordinateY)
        {

            Plateau plateau = new Plateau();
            plateau.MaxCoordinateX = maxCoordinateX;
            plateau.MaxCoordinateY = maxCoordinateY;

            bool result = _plateauService.Validate(plateau);

            result.Should().BeTrue();
        }
        [Theory]
        [InlineData(5, 5)]
        [InlineData(2, 7)]
        [InlineData(7, 2)]
        [InlineData(100, 100)]
        public void Create_PositiveWidthPositiveHeight_ShouldReturnSameValues(int maxCoordinateX, int maxCoordinateY)
        {

            Plateau plateau = _plateauService.Create(maxCoordinateX, maxCoordinateY);

            plateau.MaxCoordinateX.Should().Equals(maxCoordinateX);
            plateau.MaxCoordinateY.Should().Equals(maxCoordinateY);
        }
    }
}
