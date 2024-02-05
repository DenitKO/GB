using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Lection1
{
    class Program
    {
        static void Main(string[] args)
        {
            UpCastDownCastSubstitution();

            // ExtendetCSharpLection1();

            // TypeConvertsion();

            #region Start TestClass1Seminar1_1
            // TestClassSeminar1_1 testClass = new("Денис");
            // TestClassSeminar1_1 testClass2 = new("Оля");
            // TestClassSeminar1_1 testClass3 = new("Абырвалг");
            // Console.WriteLine(testClass.GetReverseName());
            // Console.WriteLine(testClass2.GetReverseName());
            // Console.WriteLine(testClass3.GetReverseName());
            // Console.WriteLine(testClass3.index);
            // testClass.Print("It's work!");
            // Console.WriteLine();
            // var text = MeasurePerformance(10, () => testClass.GetReverseName());
            #endregion

            #region SecondSeminarDZ and MeasurePerformance
            // SecondSeminarDZ();
            // var text = MeasurePerformance(10, () => SecondSeminarDZ());
            #endregion
        }


        public static void UpCastDownCastSubstitution()
        {
            Console.WriteLine("1. A a = new A();");
            Console.WriteLine("----------");
            A a = new A();
            a.Foo();
            a.Print();
            Console.WriteLine();

            Console.WriteLine("2. B b = new B();");
            Console.WriteLine("----------");
            B b = new B();
            b.Foo();
            b.Print();
            b.Print2();
            Console.WriteLine();

            Console.WriteLine("3. UpCast from B to A");
            Console.WriteLine("A a2 = new B()");
            Console.WriteLine("----------");
            A a2 = new B();
            a2.Foo();
            a2.Print();
            Console.WriteLine("Это называют 'замещение метода'");
            Console.WriteLine("т.е. после обкаста вызывается реализация базового метода");
            Console.WriteLine("кончено если он так же называется, и у него паблик модификатор");
            Console.WriteLine();

            Console.WriteLine("4. DownCast from B(A) to B");
            Console.WriteLine("B b2 = a2 as B;");
            Console.WriteLine("----------");
            B b2 = a2 as B;
            if (b2 != null)
            {
                b2.Foo();
                b2.Print();
                b2.Print2();
            }
            Console.WriteLine();

            Console.WriteLine("5. UpCast from B to A");
            Console.WriteLine("A a3 = b as A;");
            Console.WriteLine("----------");
            A a3 = b as A;
            if (a3 != null)
            {
                a3.Foo();
                a3.Print();
            }
            Console.WriteLine();
            // B b2 = new A(); НЕЛЬЗЯ
        }

        public static void ExtendetCSharpLection1()
        {
            Human[] humans = { new Man(), new Woman(), new Man() };
            foreach (var human in humans)
            {
                Woman w = human as Woman;
                w?.MakeUp();

                if (human is Woman woman)
                {
                    if (!woman.IsMakeup())
                        woman.MakeUp();
                    else woman.MakeDown();
                }

                human.Info();
            }
        }

        public static void TypeConvertsion()
        {
            /*
            * Inheritance [ɪnˈherɪt(ə)ns] - наследование
            * 
            * Type Conversion - приведение типов
            * 
            * приведение типов и наследование
            * 
            * использование операторов as и is
            * 
            */
            object obj = new MyPoint { X = 3, Y = 5 };
            Console.WriteLine(obj.GetType());
            Console.WriteLine();
            /*
             * obj.Print(); не работает, потому что сделан DownCast. И тип object ничего не знает о MyPoint
             * MyPoint myPoint1 = (MyPoint)obj; // А так сработает.
             * myPoint1.Print();
             * Это UpCast. Мы выполнили явное приведение типов
             * UpCast возможен только полсе DownCast-а
             * но явнове приведение типа таким образом (SomeType)someVar
             * вывалится в исключение InvalidCastExeption если будет передан не тот тип
             * Поэтому придумали as, is
             */

            
            // если один объект не соответвует другому то оператор as выведет null


            Console.WriteLine("construction - obj as MyPoint");
            Console.WriteLine("if (myPoint1 != null) {}");

            MyPoint myPoint1 = obj as MyPoint;

            if (myPoint1 != null)
            {
                myPoint1.Print();
            }

            // и РАНЬШЕ, где то до 7 версии смысла использовать следующую
            // конструкция небыло

            Console.WriteLine();
            Console.WriteLine("construction - obj is MyPoint");

            if (obj is MyPoint) // Если obj принадлежит классу MyPoint то true
            {
                MyPoint myPoint2 = (MyPoint)obj;
                myPoint2.Print();
            }

            // Но сейчас предпочтительней использовать is

            Console.WriteLine();
            Console.WriteLine("construction - obj is MyPoint point");


            if (obj is MyPoint point) 
            {
                point.Print();
            }

            //MyPoint obj2 = new MyPoint { Y = 3, X = 4 };
            //Console.WriteLine(obj2.GetType());
            //obj2.Print();
        }

        public static void SecondSeminarDZ()
        {
            ArrayList temp = new();
            DateTime dateTime = DateTime.Now;
            int[,] a = { { 7, 3, 2 }, { 4, 9, 6 }, { 1, 8, 5 } };
            foreach (int i in a)
                temp.Add(i);
            temp.Sort();

            int[,] result = new int[a.GetLength(0), a.GetLength(1)];
            int count = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    result[i, j] = (int)temp[count];
                    Console.Write(result[i, j]);
                    count++;
                }
                Console.WriteLine();
            }
        }

        private static double MeasurePerformance(int iterations, Action action)
        {
            // First myDelegat always too large, I woder why?
            action();
            double elapsedTicks = 0;
            double elapsedMS = 0;
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                stopwatch.Start();
                action();
                stopwatch.Stop();

                elapsedTicks += stopwatch.ElapsedTicks;
                elapsedMS += stopwatch.Elapsed.TotalMilliseconds;

                Console.WriteLine($"Iteration {i + 1}; Ticks: {stopwatch.ElapsedTicks}; ms: {stopwatch.Elapsed.TotalNanoseconds}.");
                stopwatch.Reset();
            }
            Console.WriteLine($"Iterations: {iterations}; TotalTiks: {elapsedTicks}; TotalMilliseconds:{elapsedMS}.");
            elapsedTicks /= iterations;
            elapsedMS /= iterations;
            Console.WriteLine($"AVG_Ticks: {elapsedTicks}; AVG_MS: {elapsedMS}.");
            return elapsedTicks;
        }

    }

    class A
    {
        public virtual void Foo() => Console.WriteLine("Type: " + this.GetType().Name + ". A virtual Foo()");
        public void Print() => Console.WriteLine("Type: " + this.GetType().Name + ". A.Print()");
    }

    class B : A
    {
        public override void Foo() => Console.WriteLine("Type: " + this.GetType().Name + ". B override Foo()");
        public new void Print() => Console.WriteLine("Type: " + this.GetType().Name + ". B new Print()");

        public void Print2() => Console.WriteLine("Type: " + this.GetType().Name + ". B Print2()");
    }

}