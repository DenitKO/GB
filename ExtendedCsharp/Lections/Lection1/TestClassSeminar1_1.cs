namespace Lection1
{
    internal class TestClassSeminar1_1
    {
        static int lenght;
        public int index { get; private set; }
        string name;
        bool b;

        public TestClassSeminar1_1()
        {
            index = lenght;
            lenght++;
            name = string.Empty;
            b = false;
        }
        public TestClassSeminar1_1(string name)
        {
            index = lenght;
            lenght++;
            this.name = name;
            b = false;
        }

        public void Print(string msg)
        {
            if (msg == "" || msg == null)
                return;
            Console.WriteLine(msg);
        }

        public string GetReverseName()
        {
            char[] chars = name.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static int Length()
        {
            return lenght;
        }
    }
}