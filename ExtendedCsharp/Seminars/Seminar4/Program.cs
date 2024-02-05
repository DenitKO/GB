using System; 

namespace Seminar4
{
    /*

    */
    class Program
    {
        static void Main(string[] args)
        {
            FromMinSortDictionary();
        }

        public static void FromMinSortDictionary()
        {
            int[] nums = new int[10];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = new Random().Next(6);
                Console.Write($"{nums[i]}");
            }
            Console.WriteLine();

            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (int i in nums)
            {
                if (map.ContainsKey(i))
                    map[i]++;
                else
                    map[i] = 0;
            }

            Dictionary<int, int> ordered = map.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in ordered.Keys)
            {
                Console.WriteLine($"{item}: {map[item]}");
            }
        }
    }
}