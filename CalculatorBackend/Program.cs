namespace CalculatorBackend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var question = "36 / 6 * 3 + 2^2 - ( 3 + 5 )";

            //var questionShort = "36/6*3+2^2-(3+5*(5+10(56*4+10))+(6^4))";

            //var operatorTest = "5*2^2+5-6/2"; // - Passed

            //var validityTest = "3^^4++-6"; // - Passed

            var bracketsTest = "5 + ( 25 * ( 7 + ( 2^2 ) ) - ( 8 / 2 ) )"; // - Passed

            Calculator calculator = new();

            var result = calculator.Calculate(bracketsTest);
            Console.WriteLine(result);
        }
    }
}
