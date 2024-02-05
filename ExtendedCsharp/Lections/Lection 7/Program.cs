namespace Lection7
{
    class Program
    {
        public static void Main()
        {
            Reflection_ExtendedLection7();
        }
        public static void Reflection_ExtendedLection7()
        {
            var obj = new object();

            Type type1 = obj.GetType();

            Console.WriteLine($"Type type1 = new object().GetType(): type1 = {type1}");
            Console.WriteLine($"                     typeof(object):         {typeof(object)}");

            Type t1 = typeof(int?);
            Console.WriteLine($"              Type t1 = typeof(int?):     t1 = {t1}");
            Type t2 = typeof(Nullable<>);
            Console.WriteLine($"        Type t2 = typeof(Nullable<>):     t2 = {t2} открытый или незавершенный");
            Type t3 = typeof(Nullable<int>);
            Console.WriteLine($"     Type t3 = typeof(Nullable<int>):     t3 = {t3}");
            Console.WriteLine();
            Console.WriteLine("Для получения информации об открытом типе с несколькими параметрами, нужно дополнительно");
            Console.WriteLine("указывать запятые, например typeof(Tuple<,>) или typeof(Tuple<,,>)");
            Console.WriteLine($"{typeof(Tuple<,>)}, {typeof(Tuple<,,>)}");
            Console.WriteLine();
            Type typeOfArray = typeof(int[]);
            Console.WriteLine($"     Type typeOfArray = typeof(int[]):              typeOfArray = {typeOfArray}");
            Type typeOfInterface1 = typeof(IEnumerable);
            Console.WriteLine($"Type typeOfInterface1 = typeof(IEnumerable)):   typeOfInterface = {typeOfInterface1}");
            Type typeOfInterface2 = typeof(IEnumerable<>);
            Console.WriteLine($"Type typeOfInterface2 = typeof(IEnumerable<>)): typeOfInterface = {typeOfInterface2}");
            Console.WriteLine(typeof(Type));
            Console.WriteLine("typeof(object).GetType(): " + typeof(object).GetType());
            Console.WriteLine(" obj.GetType().GetType(): "+obj.GetType().GetType());
            Man aza = new Man();
            string myNameSpace = aza.GetType().Namespace;
            string myClassName = aza.GetType().Name;
            Console.WriteLine(myNameSpace);
            Console.WriteLine(myClassName);
        }
    }
}