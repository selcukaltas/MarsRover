using MarsRover.Business.Exceptions;
using MarsRover.Business.Services.Abstract;
using MarsRover.Models;
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
    public class PlateauService : IPlateauService
    {
        private  Plateau _plateau;
     
        public Plateau Create(int maxCoordinateX, int maxCoordinateY)
        {
            
            Plateau plateau = new Plateau() { MaxCoordinateX = maxCoordinateX, MaxCoordinateY = maxCoordinateY };
            var validatePlateau = Validate(plateau);
            if (validatePlateau)
            {
                Log.Information(Messages.PlateauCreated);
                _plateau = plateau;
            }
            return _plateau;
        }

        public bool Validate(Plateau plateau)
        {
            if (plateau != null && plateau.MaxCoordinateX > 0 && plateau.MaxCoordinateY > 0)
            {
                Log.Information($"Plateau has valid size ({plateau.MaxCoordinateX}-{plateau.MaxCoordinateY}).");
                return true;
            }
            else
            {
                ValidatePlateauException ex = new ValidatePlateauException();
                Log.Error(ex.Message);
                throw ex;
            }
        }
    }
}
