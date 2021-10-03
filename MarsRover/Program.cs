using MarsRover.Business.Commands.Abstract;
using MarsRover.Business.Commands.Concrete;
using MarsRover.Business.Exceptions;
using MarsRover.Business.Factory.Abstract;
using MarsRover.Business.Factory.Concrete;
using MarsRover.Business.Helper;
using MarsRover.Business.Services.Abstract;
using MarsRover.Business.Services.Concrete;
using MarsRover.Models;
using MarsRover.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using static MarsRover.Business.Constants.Constants;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<IDiscoveryFactory, DiscoveryFactory>()
            .AddSingleton<IDiscoveryCommand, TurnRightCommand>()
            .AddSingleton<IDiscoveryCommand, TurnLeftCommand>()
            .AddSingleton<IDiscoveryCommand, MoveForwardCommand>()
            .AddSingleton<IPlateauService, PlateauService>()
            .AddSingleton<IRoverService, RoverService>()
            .AddSingleton<IControlDirectionService, ControlDirectionService>()
            .BuildServiceProvider();

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo
            .Console()
            .CreateLogger();
            Plateau plateau = null;
            RoverLocation roverLocation = null;
            RoverLocation lastLocation = null;
            string loop = "Y";

            IPlateauService plateauService = serviceProvider.GetService<IPlateauService>();
            IRoverService roverService = serviceProvider.GetService<IRoverService>();
            IControlDirectionService instructionService = serviceProvider.GetService<IControlDirectionService>();
            plateauCordinate:
            Console.Write(Messages.PutPlateauCoordinate);
            var plateauCoordinate = Console.ReadLine();
            if (CheckInputs.CheckPlateauInput(plateauCoordinate))
            {
                var plateauCoordinates = plateauCoordinate.Split(" ");

                plateau = plateauService.Create(Int32.Parse(plateauCoordinates[0]), Int32.Parse(plateauCoordinates[1]));
            }
            else
            {
                Console.WriteLine(Messages.InputError);
                goto plateauCordinate;
            }
            List<Rover> rovers = new List<Rover>();
            while (loop.ToLower() == "Y".ToLower())
            {
                try
                {
                    Console.Write(Messages.PutRoverCoordinate);
                    var roverCoordinate = Console.ReadLine();
                    if (CheckInputs.CheckRoverInput(roverCoordinate))
                    {
                        var roverCoordinates = roverCoordinate.Split(" ");

                        roverLocation = new RoverLocation() { CoordinateX = Convert.ToInt32(roverCoordinates[0]), CoordinateY = Convert.ToInt32(roverCoordinates[1]), Direction = (CompassDirection)Enum.Parse(typeof(CompassDirection), roverCoordinates[2].ToString()) };
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(Messages.InputError);
                        Console.WriteLine();
                        continue;
                    }

                    Rover rover = roverService.Initalize(plateau, roverLocation);
                    Console.Write(Messages.GetInstruction);
                    var instructions = Console.ReadLine();
                    List<ControlDirection> controlDirectionServices = instructionService.GetInstructions(instructions);
                    foreach (var item in controlDirectionServices)
                    {

                        lastLocation = roverService.DiscoverPlateau(item);
                    }
                    Console.WriteLine($"ROVER LAST LOCATION: {lastLocation.CoordinateX} {lastLocation.CoordinateY} {lastLocation.Direction}");
                    rovers.Add(rover);
                    Console.WriteLine($"Count of rover on the plateau : {rovers.Count} , if you want keep adding new rover press 'Y' or for escape press enter");
                    loop = Console.ReadLine();
                }
                catch (ValidateLocationException)
                {
                    continue;
                }
                catch (ValidatePlateauException)
                {
                    continue;
                }
                catch (InvalidControlDirectionException)
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

            }

        }
    }
}
