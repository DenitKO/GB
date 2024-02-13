namespace Lection2
{
    /*
        Урок 2. Интерфейсы и обощения
    */
    class Program
    {
        static void TakeCare(params IParent[] parents)
        {
            foreach (var p in parents)
            {
                p.TakeCare();
            }
        }
        static void TakeCare(params IBabySitter[] sisters)
        {
            foreach (var s in sisters)
            {
                s.TakeCare();
            }
        }

        record Record (int a){
            public int b { get; set; } = 0;
            public int c { get; } = 0;
            public int d { get; private set; } = 0;
        }

        record Record2 (int a)
        {
            public int b { get; set; } = 0;
        }

        record class ReferenceRecord(int a)
        {
            public int b = 0;
        }
        record class ValueRecord(int a)
        {
            public int b = 0;
        }

        class SimpleTuple<T1, T2>
        {
            public T1 Item1 { get; init; }
            public T2 Item2 { get; init; }

            public SimpleTuple(T1 item1, T2 item2)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }

            public override string ToString()
            {
                return $"{{{Item1}, {Item2}}}";
            }
        }

        class Utility<T> where T:struct
        {
            public static void Swap(ref T v1, ref T v2)
            {
                T temp = v1;
                v1 = v2;
                v2 = temp;
            }
        }

        class Utility2
        {
            public static void Swap<T>(ref T v1, ref T v2)
            {
                T temp = v1;
                v1 = v2;
                v2 = temp;
            }
        }

        interface ICovariant<out T>
        {
            T GetDefault();
        }

        class SomeClass<T> : ICovariant<T>
        {
            public T GetDefault()
            {
                return default(T);
            }
        }

        interface IContrvariant<in T>
        {
            void DoSomething(T arg);
        }

        class SomeClass2<T> : IContrvariant<T>
        {
            public void DoSomething(T arg)
            {              
            }
        }

        interface IInvariant<T>
        {
            T GetDefault();
            void DoSomething(T arg);
        }

        class SomeClass3<T> : IInvariant<T>
        {
            public void DoSomething(T arg)
            {              
            }

            public T GetDefault()
            {
                return default(T);
            }
        }

        static void Main(string[] args)
        {
            #region Generic интерфейсы коварианты контрварианты инварианты
            // InvariantExample();

            // ContrvariantExample();

            // CovariantExample();
            #endregion

            // GenericInterfaceExample();

            // GenericLimitationMethodExample();

            // GenericLimitationClassExample();

            // SimpleTupleExample();

            // Record2Example();

            // RecordExample();

            // OverridedOperatorEx();

            // 33:30
            #region Indexators

            // IndexatorBitsExample();

            // IndexatorExample();

            #endregion
            

            #region Intarface
            // MyInterfaceExample();

            // IComparableExample();
            #endregion

            static void InvariantExample()
            {
                IContrvariant<object> obj2 = new SomeClass3<object>();
                // IContrvariant<string> str2 = obj2; // так нельзя

                IContrvariant<string> str1 = new SomeClass3<string>();
                // IContrvariant<object> obj1 = str1; // так нельзя
            }

            static void ContrvariantExample()
            {
                IContrvariant<object> obj2 = new SomeClass2<object>();
                IContrvariant<string> str2 = obj2;

                IContrvariant<string> str1 = new SomeClass2<string>();
                // IContrvariant<object> obj1 = str1; // так нельзя
            }

            static void CovariantExample()
            {
                ICovariant<string> str1 = new SomeClass<string>();
                ICovariant<object> obj1 = str1;

                ICovariant<object> obj2 = new SomeClass<string>();
                // ICovariant<string> str2 = obj2; // так нельзя
            }

            static void GenericInterfaceExample()
            {
                Metric[] tempratures = new Metric[]
                {
                    new Metric{ Month = 1, Temperature = -1, Days = 10 },
                    new Metric{ Month = 8, Temperature = 22, Days = 1 },
                    new Metric{ Month = 1, Temperature = -10, Days = 2 },
                    new Metric{ Month = 2, Temperature = -1, Days = 3 },
                    new Metric{ Month = 5, Temperature = 10, Days = 4 },
                    new Metric{ Month = 1, Temperature = -2, Days = 5 },
                    new Metric{ Month = 2, Temperature = -30, Days = 1 },
                    new Metric{ Month = 1, Temperature = 2, Days = 3 },
                };

                Array.Sort(tempratures);

                foreach (var t in tempratures)
                {
                    Console.Write(t + " ");
                }
            }

            static void GenericLimitationMethodExample()
            {
                int a = 10, b = 20;
                char c1 = 'B', c2 = 'B';
                string s1 = "ABC", s2 = "BCD";

                Utility2.Swap<int>(ref a, ref b);

                Console.WriteLine($"a={a}, b = {b}"); // a=20, b = 10

                Utility2.Swap(ref c1, ref c2);

                Console.WriteLine($"c1={c1}, c2 = {c2}"); // c1=B, c2 = B

                Utility2.Swap(ref s1, ref s2);

                System.Console.WriteLine($"c1={s1}, s2 = {s2}"); // c1=BCD, s2 = ABC
            }

            static void GenericLimitationClassExample()
            {
                int a = 10, b = 20;
                char c1 = 'B', c2 = 'B';

                Utility<int>.Swap(ref a, ref b);

                Console.WriteLine($"a={a}, b = {b}"); // a=20, b = 10

                Utility<char>.Swap(ref c1, ref c2);

                Console.WriteLine($"c1={c1}, c2 = {c2}"); // c1=B, c2 = B
            }

            static void SimpleTupleExample()
            {
                SimpleTuple<int, string> simpleTuple1 = new SimpleTuple<int, string>(10, "ABC");
                SimpleTuple<string, string> simpleTuple2 = new SimpleTuple<string, string>("ABC", "ABC");
                SimpleTuple<object, SimpleTuple<int, string>> simpleTuple3 = new SimpleTuple<object, SimpleTuple<int, string>>("ABC" , simpleTuple1);

                Console.WriteLine(simpleTuple1);
                Console.WriteLine(simpleTuple2);
                Console.WriteLine(simpleTuple3);
            }

            static void Record2Example()
            {
                var record1 = new Record2(10);
                var record2 = new Record2(10);

                System.Console.WriteLine(record1 == record2); // True

                record1.b = 20;

                System.Console.WriteLine(record1 == record2); // False

                System.Console.WriteLine(record1);
            }

            static void RecordExample()
            {
                var record = new Record(10);

                var record1 = new ReferenceRecord(10);
                var record2 = record1;

                record2.b = 20;
                System.Console.WriteLine(record1.b); // 20


                var record3 = new ValueRecord(10);
                var record4 = record1;

                record4.b = 20;
                System.Console.WriteLine(record3.b); // 0

                // record.a = 20; // так нельзя, ошибка!
            }

            static void OverridedOperatorEx()
            {
                var a = new Fibonacci();

                //1 1 2 3 5 8 13 21
                for (int i = 1; i <= 8; i++)
                {
                    Console.Write($"{a} ");
                    a++;
                }

                Console.WriteLine();

                var b = new Fibonacci();

                b = b + 7;

                Console.WriteLine(b);
            }

            static void IndexatorBitsExample()
            {
                var b = new Bits(0);

                for (int i = 0; i <= 7; i++)
                {
                    if(i%2 == 0)
                        b[i] = true;
                }

                System.Console.WriteLine(b.Value.ToString());

                for (int i = 7; i >= 0; i--)
                {
                    System.Console.Write(b[i] + " ");
                }
            }

            static void IndexatorExample()
            {
                var grandMa = new Woman("Анна", DateTime.Parse("01.01.1970"));
                var grandPa = new Man("Карен", DateTime.Parse("01.01.1971"));

                var parent = new Woman("Инна", DateTime.Parse("01.01.1990"));
                var kid1 = new Woman("Регина", DateTime.Parse("01.01.2020"));

                parent.AddFamilyInfo(grandPa, grandMa, kid1);

                for (int i = 0; i < parent.Count; i++) 
                {
                    parent[i].Print();
                }
                Console.WriteLine(parent.Count);
            }

            static void IComparableExample()
            {
                Person[] people = new Person[] {
                new Woman("Анна", DateTime.Parse("01.01.1990")),
                new Woman("Мария", DateTime.Parse("01.01.1999")),
                new Woman("Екатерина", DateTime.Parse("01.01.1980")),
                new Man("Петр", DateTime.Parse("01.01.1997")),
                new Man("Федор", DateTime.Parse("01.01.2001")),
                new Man("Руслан", DateTime.Parse("01.01.1983"))
            };

                Array.Sort(people);

                foreach (Person p in people)
                {
                    p.Print();
                }

            }

            static void MyInterfaceExample()
            {
                var woman = new Woman("Анна", DateTime.Parse("01.01.1990"));
                var man = new Man("Руслан", DateTime.Parse("01.01.1983"));
                var kid = new Man("Федор", DateTime.Parse("01.01.2021"));

                man.AddFamilyInfo(null, null, kid);
                woman.AddFamilyInfo(null, null, kid);

                TakeCare(man, woman);

                IBabySitter babySiter1 = woman;
                IParent mom = woman;
                IParent dad = man;

                Console.WriteLine();
                mom.TakeCare();
                babySiter1.TakeCare();

                IBabySitter babySitter2 = new BabySitter();
                BabySitter babySitter3 = new BabySitter();

                Console.WriteLine();
                TakeCare(babySiter1, babySitter2, babySitter3);
                babySitter3.Hello();
            }
        }
    }
}