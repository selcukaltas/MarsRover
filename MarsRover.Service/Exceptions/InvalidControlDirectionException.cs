using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsRover.Business.Constants.Constants;

namespace MarsRover.Business.Exceptions
{
    public class InvalidControlDirectionException : Exception
    {
        public InvalidControlDirectionException() : base(Messages.InvalidInstruction)
        {

        }
    }
}
