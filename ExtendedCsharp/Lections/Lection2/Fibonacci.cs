namespace Lection2
{
    internal class Fibonacci
    {
        public int Value { get; private set; } = 1;
        private int _valuePrev = 0;

        public static Fibonacci operator ++(Fibonacci f)
        {
            var temp = f.Value; 
            f.Value = f.Value + f._valuePrev;
            f._valuePrev = temp;

            return f;
        }

        public static Fibonacci operator +(Fibonacci f, int count)
        {
            for (int i = 0; i < count; i++)
            {
                f++;
            }
            return f;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}