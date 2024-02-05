using System; 

namespace Lection5
{
    /*

    */
    class Program
    {
        delegate int MyDelegat();
        delegate void MyStringDelegat(string s);
        static void Main(string[] args)
        {
            Lection5_1();
        }

        static void Lection5_1()
        {
            MyDelegat? myDelegat = SayHello;
            myDelegat += SayBye;
            myDelegat += SayBye;
            Console.WriteLine($"В делегате вот столько методов: {myDelegat.GetInvocationList().Length}");
            Console.WriteLine();

            foreach (MyDelegat item in myDelegat.GetInvocationList())
            {
                Console.WriteLine(item.GetType().Name);
                
                Console.WriteLine(item());
            }

            Console.WriteLine();

            myDelegat();
            Console.WriteLine();
            Console.WriteLine(myDelegat());
            Console.WriteLine("----------------------------");
            MyStringDelegat myDelegat2 = MyName;
            myDelegat2 -= MyName;
            myDelegat2?.Invoke("Denis");
            Console.WriteLine("----------------------------");


            static int SayHello()
            {
                Console.WriteLine("Привет!");
                return 0;
            }

            static int SayBye()
            {
                Console.WriteLine("Пока");
                return 1;
            }

            static void MyName(string name)
            {
                Console.WriteLine($"my name is {name}");
            }
        }
    }
}
        
        