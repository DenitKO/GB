using System; 

namespace Seminar3
{
    /*

    */
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[,]
            {
                {1, 1, 1, 0, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 0, 1 },
                {0, 0, 0, 0, 1, 0, 0 },
                {1, 1, 0, 0, 1, 1, 1 },
                {1, 1, 1, 0, 1, 1, 1 },
                {1, 1, 1, 0, 1, 1, 1 }
            };

            Labirint labirint = new Labirint(arr);

            labirint.FindExitsRecursivelyFromXY(3,3);
            labirint.PrintExits();
            labirint.PrintLabirint();
        }
    }
    class Labirint
    {
        private int[,] _labirint { get; set; }
        public int _exits {  get; private set; } = 0;

        public Labirint(int[,] labirint)
        {
            _labirint = labirint;
        }

        public void PrintExits()
        {
            Console.WriteLine($"Количество выходов: {_exits}.");
        }

        /// <summary>
        /// Рекурсивно находит выходы из лабиринта и записывает их.
        /// </summary>
        /// <param name="x">Стартовая позиция по X</param>
        /// <param name="y">Стартовая позиция по Y</param>
        public void FindExitsRecursivelyFromXY(int x, int y)
        {
            
            if (!isMove(x, y))
                return;
            _labirint[x, y] = 2;
            FindExitsRecursivelyFromXY(x, y+1);
            FindExitsRecursivelyFromXY(x+1, y);
            FindExitsRecursivelyFromXY(x, y - 1);
            FindExitsRecursivelyFromXY(x-1, y);
        }

        public void PrintLabirint()
        {
            for (int i = 0; i < _labirint.GetLength(0); i++)
            {
                for (int j = 0; j < _labirint.GetLength(1); j++)
                {
                    Console.Write($"{_labirint[i,j]} ");
                }
                Console.WriteLine();
            }
        }

        private bool isMove(int x, int y)
        {
            if ((x == _labirint.GetLength(0) - 1 || y == _labirint.GetLength(1) - 1 || x == 0 || y == 0) && (_labirint[x,y]==0))
                _exits++;
            if (x < 0 || x >= _labirint.GetLength(0))
                return false;
            if (y < 0 || y >= _labirint.GetLength(1))
                return false;
            return _labirint[x, y] == 0;
        }
    }
}