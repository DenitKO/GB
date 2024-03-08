using System;
using System.Text;

namespace Lection4
{
    /* Урок 4. Коллекции (часть 2)
    */
    class Program
    {
        static void Main(string[] args)
        {
            // FindCountOfWordsInText();
            // FindCountOfSimbolsInTextDictionary();
            // FindCountOfSimbolsInTextArray();
            // HashSetExceptWith();
            // HashSetIntersectWith();
            // HashSetIsProperSubsetOf();
            // HashSetIsProperSupersetOf();
            // HashSetIsSubsetOf();
            // HashSetIsSupersetOf();
            // HashSetOverlaps();
            // HashSetRemove();
            // HashSetRemoveWhere();
            HashSetSymmetricExceptWith();
        }


        static void HashSetSymmetricExceptWith()
        {

            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var set2 = new HashSet<int>(new int[]{ 1, 2, 3, 10, 11, 12, 13, 14, 15 });

            set1.SymmetricExceptWith(set2);
            
            foreach (var i in set1)
            {
                System.Console.Write(i + " "); // 12 11 10 4 5 6 7 8 9 0 13 14 15
            }
        }
        static void HashSetRemoveWhere()
        {
            static bool IsEven(int i)
            {
                return i % 2 == 0;
            }

            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
            var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

            System.Console.WriteLine(set1.RemoveWhere(IsEven)); // 5
            
            foreach (var i in set1)
            {
                System.Console.Write(i + " "); // 1 3 5 7 9
            }
        }

        static void HashSetRemove()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
            var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

            System.Console.WriteLine(set1.Remove(1)); // True
            System.Console.WriteLine(set1.Remove(1)); // False
        }

        static void HashSetOverlaps()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var set2 = new HashSet<int>(new int[]{ 20, 30, 40, 1, 3, 4 });
            var set3 = new HashSet<int>(new int[]{ 20, 30, 40 });

            System.Console.WriteLine(set1.Overlaps(set2)); // True
            System.Console.WriteLine(set1.Overlaps(set3)); // False
        }

        static void HashSetIsSupersetOf()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

            System.Console.WriteLine(set1.IsSupersetOf(set2)); // True
            System.Console.WriteLine(set1.IsSupersetOf(set4)); // True
        }

        static void HashSetIsSubsetOf()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

            System.Console.WriteLine(set2.IsSubsetOf(set1)); // True
            System.Console.WriteLine(set3.IsSubsetOf(set4)); // True
        }
        static void HashSetIsProperSupersetOf()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set4 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

            System.Console.WriteLine(set1.IsProperSupersetOf(set2)); // True
            System.Console.WriteLine(set3.IsProperSupersetOf(set4)); // False
        }

        static void HashSetIsProperSubsetOf()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });
            var set3 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0 });

            System.Console.WriteLine(set2.IsProperSubsetOf(set1)); // True
            System.Console.WriteLine(set2.IsProperSubsetOf(set3)); // False
        }

        static void HashSetIntersectWith()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ -1, 5, 6, 7, 8, 9, 0, 10});

            set1.IntersectWith(set2);

            foreach (var s in set1)
                Console.Write(s+" "); // 5 6 7 8 9 0
        }

        static void HashSetExceptWith()
        {
            var set1 = new HashSet<int>(new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});
            var set2 = new HashSet<int>(new int[]{ 5, 6, 7, 8, 9, 0});

            set1.ExceptWith(set2);

            foreach (var s in set1)
                Console.Write(s+" "); // 1 2 3 4
        }

        static void FindCountOfSimbolsInTextArray()
        {
            var s = "To use or not to use Dictionary";

            int[] count = new int[26];

            foreach (var myChar in s)
            {
                if (!Char.IsAsciiLetter(myChar))
                    continue;

                var lowerChar = Char.ToLower(myChar);

                int pos = ((byte)lowerChar) - ((byte)'a');

                count[pos]++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (count[i] > 0)
                    Console.WriteLine($"{(char)(i+(byte)'a')}={count[i]}");
            }
        }

        static void FindCountOfSimbolsInTextDictionary()
        {
            var s = "To use or not to use Dictionary";

            Dictionary<char, int> count = new Dictionary<char, int>();

            foreach (var myChar in s)
            {
                if (!Char.IsAsciiLetter(myChar))
                    continue;
                var lowerChar = Char.ToLower(myChar);
                if(count.ContainsKey(lowerChar))
                    count[lowerChar]++;
                else 
                    count[lowerChar] = 1;
            }

            foreach (var pair in count)
            {
                Console.WriteLine($"{pair.Key} = {pair.Value}");
            }
        }

        static void FindCountOfWordsInText()
        {
            var s = "Текст с повторяющимися повторяющимися повторяющимися словами. Выведи количество повторов вместе со словами";

            StringBuilder sb = new StringBuilder();
            Dictionary<string, int> count = new Dictionary<string, int>();

            foreach (var myChar in s)
            {
                if (" ,.-".Contains(myChar))
                {
                    if (sb.Length > 0)
                    {
                        if (count.ContainsKey(sb.ToString()))
                        {
                            count[sb.ToString()]++;
                        }
                        else
                            count[sb.ToString()] = 1;
                        sb.Clear();
                    }
                }
                else
                    sb.Append(myChar);
            }
            /*
            * вторая проверка после окончания цикла нужна
            * так как в конце предложения может не оказаться проблема
            * или знака препинания и условие в цикле не сработает
            */
            if (count.ContainsKey(sb.ToString()))
            {
                count[sb.ToString()]++;
            }
            else
                count[sb.ToString()] = 1;

            foreach (var pair in count)
            {
                Console.WriteLine($"Слово '{pair.Key}' повторяется {pair.Value} раз");
            }
        }
    }
}