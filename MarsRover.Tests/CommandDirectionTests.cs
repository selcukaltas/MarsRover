using FluentAssertions;
using MarsRover.Business.Services.Abstract;
using MarsRover.Business.Services.Concrete;
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
    public class ControlDirectionTests
    {
        private IControlDirectionService _controlDirectionService;
        public ControlDirectionTests()
        {
            _controlDirectionService = Mock.Of<ControlDirectionService>();
        }
        [Theory]
        [InlineData(null)]
        public void GetInstructions_IsNull_ThrowsValidatePlateauException(string instruction) 
        {
            Action act = () => _controlDirectionService.GetInstructions(instruction);

            act.Should().Throw<ArgumentNullException>();
        }
        [Theory]
        [InlineData("LMLMLMLMM")]
        [InlineData("MMRMMRMRRM")]
        public void GetInstructions_IsNotNullAndValidInstruction_ShouldReturnInstructionList_CountShouldEqualWithInstructionLength(string instruction)
        {

            List<ControlDirection> controlDirections = _controlDirectionService.GetInstructions(instruction);

            controlDirections.Count.Should().Equals(instruction.Length);
        }
    }
}
