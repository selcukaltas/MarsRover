using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Services.Abstract
{
    public interface IPlateauService
    {
        Plateau Create(int maxcoordinatex, int maxcoordinatey);
        bool Validate(Plateau plateau);
    }
}
