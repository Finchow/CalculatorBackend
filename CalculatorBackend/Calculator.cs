using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorBackend
{
    internal class Calculator
    {
        public static string Calculate(string userInput)
        {

            if (ValidateUserInput(userInput))
            {
                var equation = CreateEquation(userInput);
            }
            else
            {
                return "Invalid input";
            }


            return "test";
        }

        private static bool ValidateUserInput(string userInput)
        {
            return true;
        }

        private static bool ValidateEquation(string equation)
        {
            return true;
        }

        private static string[] CreateEquation(string rawEquation)
        {
            var 
        }

    }
}
