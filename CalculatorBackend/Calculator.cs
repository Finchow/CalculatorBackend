namespace CalculatorBackend
{
    internal class Calculator
    {

        private readonly char[] allowedChars = { '(', ')', '^', '/', '*', '+', '-', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' ' };

        private readonly string[] operators = { "^", "/", "*", "+", "-" };

        public string Calculate(string userInput)
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

        private bool ValidateUserInput(string userInput)
        {
            // make sure equation only consists of allowedChars
            foreach (char c in userInput)
            {
                if (!allowedChars.Contains(c))
                    return false;
            }
            return true;
        }

        private bool ValidateEquation(List<string> equation)
        {
            bool isValid = false;
            // make sure brackets are closed.
            var openBrackets = equation.Count(x => x.Equals("("));
            var closeBrackets = equation.Count(x => x.Equals(")"));

            if (openBrackets == closeBrackets)
                isValid = true;

            // check if operators are next to each other
            string previous = "";
            for (int i = 0; i < equation.Count(); i++)
            {
                if (previous == equation[i] && operators.Any(x => x == equation[i]))
                {
                    isValid = false;
                }
                previous = equation[i];
            }


            return isValid;
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
            equation.RemoveAll(x => x == " ");

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

        private string CalculateEquation(List<string> equation)
        {
            int bracketsCount = equation.Count(x => x.Equals("("));

            for (int i = 0; i < bracketsCount; i++)
            {
                int equationLength = equation.Count();
                // brackets - check for recursive brackets

                // find final open bracket
                var openBracketIndex = equation.FindLastIndex(x => x.Equals("("));
                int closeBracketIndex = 0;

                // find closest close bracket
                for (int j = openBracketIndex; j < equationLength; j++)
                {
                    if (equation[j] == ")")
                    {
                        closeBracketIndex = j;
                        break;
                    }
                }

                // select range
                var bracketEquation = equation.GetRange(openBracketIndex, closeBracketIndex - openBracketIndex);

                // remove brackets
                bracketEquation.RemoveAll(x => x.Equals("(") || x.Equals(")"));

                // calculate
                bracketEquation = PerformOperation(bracketEquation);

                // add answer into equation
                equation.Insert(openBracketIndex, bracketEquation.First());

                // remove calculated bracket from equation
                equation.RemoveRange(openBracketIndex + 1, closeBracketIndex - openBracketIndex + 1);

            }

            equation.RemoveAll(x => x.Equals("(") || x.Equals(")"));

            // find operators and related numbers and complete the calculation
            equation = PerformOperation(equation);

            // create readable answer
            string answer = "";
            foreach (string equationItem in equation)
                answer += " " + equationItem;

            return answer;
        }

        private List<string> PerformOperation(List<string> equation)
        {
            // detects all operators and replaces them with result of equation
            foreach (string operation in operators)
            {
                for (int i = 0; i < equation.Count(); i++)
                {
                    if (equation[i] == operation)
                    {
                        equation[i] = Arithmetic(equation[i], equation[i - 1], equation[i + 1]);
                        equation.RemoveAt(i + 1);
                        equation.RemoveAt(i - 1);
                    }
                }
            }

            return equation;
        }

        private static List<string> SeparateBrackets(List<string> equation)
        {
            // separates the first layer pair of brackets in a list
            List<string> brackets = [];
            var firstBracket = equation.IndexOf("(");
            var lastBracket = equation.LastIndexOf(")");

            return equation.GetRange(firstBracket, (lastBracket - firstBracket));
        }

        private static string Arithmetic(string operation, string num1, string num2)
        {
            var first = double.Parse(num1);
            var second = double.Parse(num2);

            double answer;

            switch (operation)
            {
                case "+":
                    answer = first + second;
                    break;
                case "-":
                    answer = first - second;
                    break;
                case "*":
                    answer = first * second;
                    break;
                case "/":
                    answer = first / second;
                    break;
                case "^":
                    answer = Math.Pow(first, second);
                    break;
                default:
                    answer = 0;
                    break;
            }
            return answer.ToString();
        }
    }
}
