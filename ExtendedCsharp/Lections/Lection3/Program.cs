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
            // ListInsert();
            // LinkedListExample();
            // LinkedListAddAfter();
            // QueueExample();
            // QueueExample2();
            StackExample();

            static void StackExample()
            {
                static bool ValidParentheses(string s)
                {
                    Stack<char> stack = new Stack<char>();
                    foreach (var c in s)
                    {
                        if (c == '[') stack.Push(']');
                        if (c == '(') stack.Push(')');
                        if (c == '{') stack.Push('}');

                        if ("])}".Contains(c))
                        {
                            if (stack.Count == 0)
                                return false;
                            if (stack.Pop() != c)
                                return false;
                        }
                    }
                    return stack.Count == 0;
                }

                Console.WriteLine(ValidParentheses("(([]{}))()")); // True
                Console.WriteLine(ValidParentheses("()(({())))")); // False
            }

            static void QueueExample2()
            {
                static IEnumerable<int> DataSource()
                {
                    for (int i = 0; i < 30; i++)
                        yield return i;
                }

                var q = new Queue<int>();

                foreach (var el in DataSource())
                {
                    q.Enqueue(el);
                    if (q.Count > 5)
                        Console.Write(q.Dequeue() + " "); // 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24
                }
            }

            static void QueueExample()
            {
                static bool Process(int i)
                {
                    return false;
                }
                static bool ALternativeProcess(int i)
                {
                    return true;
                }
                var q = new Queue<int>();

                q.Enqueue(1);
                q.Enqueue(2);
                q.Enqueue(3);
                q.Enqueue(4);
                q.Enqueue(5);

                var element = q.Peek();

                if (Process(element))
                {
                    q.Dequeue();
                }
                else
                {
                    ALternativeProcess(q.Dequeue());
                }
            }

            static void LinkedListAddAfter()
            {
                LinkedList<int> list1 = new LinkedList<int>(new int[] { 1, 4, 5,});

                LinkedListNode<int> node = list1.First!;

                list1.AddAfter(node, 3);
                list1.AddAfter(node, new LinkedListNode<int>(2));

                foreach (var n in list1)
                {
                    System.Console.Write($"{n} "); // 1 2 3 4 5 
                }
            }

            static void LinkedListExample()
            {
                LinkedList<int> list1 = new LinkedList<int>(new int[] { 1, 2, 3, 4, 5,});

                System.Console.WriteLine($"В списке {list1.Count} элементов"); // В списке 5 элементов

                LinkedListNode<int> node = list1.First!;

                while (node != null)
                {
                    System.Console.Write(node.Value + " "); // 1 2 3 4 5
                    node = node.Next!;
                }

                System.Console.WriteLine();

                node = list1.Last!;

                while (node != null)
                {
                    System.Console.Write(node.Value + " "); // 5 4 3 2 1
                    node = node.Previous!;
                }
            }

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