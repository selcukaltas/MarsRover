using MarsRover.Business.Exceptions;
using MarsRover.Business.Services.Abstract;
using MarsRover.Models.Enums;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Services.Concrete
{
    public class ControlDirectionService : IControlDirectionService
    {
        //private readonly ILogger<ControlDirectionService> _logger;
        //public ControlDirectionService(ILogger<ControlDirectionService> logger)
        //{
        //    _logger = logger;
        //}
        public List<ControlDirection> GetInstructions(string instruction)
        {
            List<ControlDirection> controlDirections = new List<ControlDirection>();
            if (!string.IsNullOrWhiteSpace(instruction))
            {
                char[] instructions = instruction.ToCharArray();
                foreach (char instruct in instructions)
                {
                    switch (instruct)
                    {
                        case (char)ControlDirection.R:
                            controlDirections.Add(ControlDirection.R);
                            break;
                        case (char)ControlDirection.L:
                            controlDirections.Add(ControlDirection.L);
                            break;
                        case (char)ControlDirection.M:
                            controlDirections.Add(ControlDirection.M);
                            break;
                        default:
                            var ex = new InvalidControlDirectionException();
                            Log.Error(ex.Message);
                            throw ex;
                    }
                }
            }
            else
            {
                Log.Error("Instructions empty.");
                throw new ArgumentNullException();
            }
            Log.Information("Instructions settled successfully.");
            return controlDirections;
        }
    }
}
