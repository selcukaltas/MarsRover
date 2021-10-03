using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Constants
{
    public static class Constants
    {
        public static class Messages
        {
            public const string LocationErrorMessage = "Rover location not valid on the plateau with given parameters. Check the coordinates.";
            public const string RoverInitialized = "Rover initialized successfully.";
            public const string PlateauCreated = "Plateau created successfully.";
            public const string PlateauError = "Plateau can't created with given parameters";
            public const string InvalidInstruction = "Invalid Instruction";
            public const string InputError = "One of the inputs are wrong";
            public const string PutRoverCoordinate = "Please put rover coordinates and head: ";
            public const string PutPlateauCoordinate = "Please put plateau coordinates: ";
            public const string GetInstruction = "Please give the instructions: ";
        }

    }
}
