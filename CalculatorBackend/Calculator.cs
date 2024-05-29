namespace CalculatorBackend
{
    internal class Calculator
    {
        public static string Calculate(string userInput)
        {
            string answer;

            if (ValidateUserInput(userInput))
            {
                var equation = CreateEquation(userInput);

                if (ValidateEquation(equation))
                {
                    answer = CalculateEquation(equation);
                }
                else return "Invalid equation";
            }
            else
            {
                return "Invalid input";
            }


            return answer;
        }

        private static bool ValidateUserInput(string userInput)
        {
            char[] allowedChars = { '(', ')', '^', '/', '*', '+', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };

            // make sure equation only consists of allowedChars
            foreach (char c in userInput)
            {
                if (!allowedChars.Contains(c))
                    return false;
            }
            return true;
        }

        private static bool ValidateEquation(List<string> equation)
        {
            // make sure brackets are closed.
            var openBrackets = equation.Count(x => x.Equals("("));
            var closeBrackets = equation.Count(x => x.Equals(")"));

            if (openBrackets == closeBrackets)
                return true;

            return false;
        }

        private static List<string> CreateEquation(string rawEquation)
        {
            List<string> equation = new();

            // split equation into array
            foreach (char c in rawEquation)
            {
                equation.Add($"{c}");
            }

            // remove whitespace
            equation.RemoveAll(x => equation.Contains(" "));

            // recombine numbers
            string previous = " ";
            string combination;
            for (int i = 0; i < equation.Count(); i++)
            {
                if (decimal.TryParse(equation[i], out _) && decimal.TryParse(previous, out _))
                {
                    // combine strings
                    combination = previous + equation[i];

                    // remove previous entry
                    equation.RemoveAt(i--);

                    // change current item to combination
                    equation[i] = combination;
                }

                previous = equation[i];

            }

            return equation;
        }

        private static string CalculateEquation(List<string> equation)
        {
            //Brackets - Check for recursive brackets
            List<List<string>> bracketEquations = new();

            var totalBrackets = equation.Count(x => x.Equals("("));
            if (totalBrackets > 0)
            {
                bracketEquations.Add(SeparateBrackets(equation));

                for (int i = 1; i < totalBrackets; i++)
                    bracketEquations.Add(SeparateBrackets(bracketEquations[i - 1]));
            }

            string answer = "";
            foreach (string equationItem in equation)
                answer += " " + equationItem;

            return answer;
        }

        private static List<string> SeparateBrackets(List<string> equation)
        {
            // Separates the first layer pair of brackets in a list
            List<string> brackets = [];
            var firstBracket = equation.IndexOf("(");
            var lastBracket = equation.LastIndexOf(")");

            return equation.GetRange(firstBracket + 1, (lastBracket - firstBracket - 1));
        }

        private static decimal Arithmetic(string operation, decimal num1, decimal num2)
        {
            switch (operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "x":
                    return num1 * num2;
                case "/":
                    return num1 / num2;
                case "^":
                    return Exponentiate(num1, num2);
                default:
                    return 0;

            }
        }

        private static decimal Exponentiate(decimal baseNum, decimal power)
        {
            for (int i = 0; i < power; i++)
            {
                baseNum += baseNum * baseNum;
            }

            return baseNum;
        }

    }
}
