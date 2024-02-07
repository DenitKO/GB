namespace Lection1
{
    internal class StringUtils
    {
        static int lenght;
        public int index { get; private set; }
        string name;

        public StringUtils()
        {
            index = lenght;
            lenght++;
            name = string.Empty;
        }
        public StringUtils(string name)
        {
            index = lenght;
            lenght++;
            this.name = name;
        }

        public void Print(string msg)
        {
            if (msg == string.Empty || msg == null)
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