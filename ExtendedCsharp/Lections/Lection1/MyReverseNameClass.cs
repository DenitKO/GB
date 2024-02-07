using System;
namespace Lection1
{
    internal class MyReverseNameClass
    {
        static int _length { get; set; }
        public int Length { get; private set; }
        string name;

        public MyReverseNameClass()
        {
            Length = _length;
            _length++;
            name = string.Empty;
        }
        public MyReverseNameClass(string name)
        {
            Length = _length;
            _length++;
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
    }
}