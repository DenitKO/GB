using System; 

namespace Lection3
{
    /* Урок 3. Коллекции (часть 1)
    */

    class Program
    {
        
        static void Main(string[] args)
        {
            // ListExists();
            // ListFind();
            // ListFindAll();
            // ListFindIndex();
            // ListFindLastIndex();
            ListInsert();

            static void ListInsert()
            {
                List<int> list1 = new List<int>() { 1,2,3,4,5,6,7,8,9,10 };

                list1.Insert(1,2);

                list1.ForEach(Console.Write); // 122345678910
            }

            static void ListFindLastIndex()
            {
                static bool IsEven(int i)
                {
                    return i % 2 == 0;
                }

                List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                var index = list1.FindLastIndex(IsEven);

                System.Console.Write(index + " ");
                while (index > 0)
                {
                    index--;
                    index = list1.FindLastIndex(index, IsEven);

                    if (index >= 0)
                    {
                        System.Console.Write(index + " "); // 9 7 5 3 1 
                    }
                }
            }

            static void ListFindIndex()
            {
                static bool IsEven(int i)
                {
                    return i % 2 == 0;
                }

                List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                int start = 0;

                start = list1.FindIndex(IsEven);
                System.Console.Write(start + " ");
                start++;

                while (start < list1.Count && start!=-1)
                {
                    start = list1.FindIndex(++start, IsEven);
                    System.Console.Write(start + " "); // 1 3 5 7 9 

                    start++;
                }
            }

            static void ListFindAll()
            {
                static bool IsEven(int i)
                {
                    return i % 2 == 0;
                }

                List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                List<int> list2 = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17 };

                var list1Filtered = list1.FindAll(IsEven);
                var list2Filtered = list2.FindAll(IsEven);

                System.Console.Write("List 1 filterd: ");
                foreach (var i in list1Filtered)
                {
                    System.Console.Write($"{i} "); // List 1 filterd: 2 4 6 8 10 
                }

                System.Console.Write("\nList 2 filterd: ");
                foreach (var i in list2Filtered)
                {
                    System.Console.Write($"{i} "); // List 2 filterd:
                }
            }

            static void ListFind()
            {
                static bool IsCapital(string s)
                {
                    foreach (var c in s)
                    {
                        if (char.IsAscii(c)&& !char.IsUpper(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }

                List<string> list1 = new List<string>() { "abc", "ABC", "bcd"};
                List<string> list2 = new List<string>() { "abc", "bcd", "def"};

                System.Console.WriteLine(list1.Find(IsCapital)); // ABC
                System.Console.WriteLine(list2.Find(IsCapital)); // nothing
            }

            static void ListExists()
            {
                static bool IsEven(int i)
                {
                    return i % 2 == 0;
                }

                List<int> list1 = new List<int>() { 2 };
                List<int> list2 = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17 };

                Console.WriteLine(list1.Exists(IsEven)); // true
                Console.WriteLine(list2.Exists(IsEven)); // false
            }
        }
    }
}