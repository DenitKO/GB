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
        static void Main(string[] args)
        {

            // 33:30
            #region Indexators

            IndexatorBitsExample();

            // IndexatorExample();

            #endregion
            

            #region Intarface
            // MyInterfaceExample();

            // IComparableExample();
            #endregion
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