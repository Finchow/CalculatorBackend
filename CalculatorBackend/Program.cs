namespace CalculatorBackend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var question = "36 / 6 * 3 + 2^2 - ( 3 + 5 )";
            var questionShort = "36/6*3+2^2-(3+5*(5+10))";
            var result = Calculator.Calculate(questionShort);
            Console.WriteLine(result);
        }
    }
}
