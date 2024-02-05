using System;

namespace Seminar5
{
    class CalculatorArgs : EventArgs
    {
        public int answer = 0;
    }
    class Calculator
    {
        public event EventHandler<CalculatorArgs> Result;
        public int result { get; private set; } = 0;
        private void Calculation()
        {
            Result.Invoke(this, new CalculatorArgs { answer = result } );      
        }

        public void Add(int value)
        {
            result += value;
            Calculation();
        }

        public void Sub(int value)
        {
            result -= value;
            Calculation();
        }

        public void Mult(int value)
        {
            result *= value;
            Calculation();
        }

        public void Div(int value)
        {
            result /= value;
            Calculation();
        }
    }
}