using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Helper
{
    public static class CheckInputs
    {
        public static bool CheckPlateauInput(string pleateuCoordinate)
        {
         
            if (pleateuCoordinate.Length == 3 && pleateuCoordinate.Contains(" "))
            {
                var splitInput = pleateuCoordinate.Split(" ");
                foreach (var item in splitInput)
                {
                    if (item.Length > 1 || string.IsNullOrWhiteSpace(item) || splitInput.Length != 2)
                    {
                        return false;
                    }
                }
                if (pleateuCoordinate.Length == 3 && splitInput.All(x => char.IsDigit(x.ToCharArray()[0])))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public static bool CheckRoverInput(string roverCoordinate)
        {

            if (!string.IsNullOrWhiteSpace(roverCoordinate))
            {
                var splitInput = roverCoordinate.Split(" ");

                foreach (var item in splitInput)
                {
                    if (item.Length>1||string.IsNullOrWhiteSpace(item)||splitInput.Length!=3)
                    {
                        return false;
                    }
                }
                if (char.IsDigit(char.Parse(splitInput[0])) && char.IsDigit(char.Parse(splitInput[1])) && char.IsLetter(char.Parse(splitInput[2])))
                {
                    return true;
                }

            }
            return false;
        }
    }
}
