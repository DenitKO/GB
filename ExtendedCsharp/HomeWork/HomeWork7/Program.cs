using System; 

namespace HomeWork7
{
    /*
    Напишите 2 метода использующие рефлексию
    1 - сохраняет информацию о классе в строку
    2- позволяет восстановить класс из строки с информацией о классе
    В качестве примере класса используйте класс TestClass.

    class TestClass
    {
        public int I { get; set; }
        public string? S { get; set; }
        public decimal D { get; set; }
        public char[]? C { get; set; }

        public TestClass() { }
        private TestClass(int i)
        {
            this.I = i;
        }
        public TestClass(int i, string s, decimal d, char[] c):this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
        }
    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
        }
    }
}