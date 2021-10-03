using MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Services.Abstract
{
    public interface IControlDirectionService
    {
        List<ControlDirection> GetInstructions(string instruction);
    }
}
