using System; 

namespace Seminar5
{
    /*

    */
    class Program
    {
        static void Main(string[] args)
        {
            Calculator();
        }
        public static void Calculator()
        {
            Calculator calculator = new Calculator();
            calculator.Result += Calculator_Result;

            calculator.Add(10);
            calculator.Add(20);
            calculator.Div(3);

            static void Calculator_Result(object? sender, CalculatorArgs e)
            {
                Console.WriteLine($"result: {e.answer}");
            }
        }
    }
}