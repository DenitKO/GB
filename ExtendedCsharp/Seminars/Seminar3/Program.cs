using System; 

namespace Seminar3
{
    /*

    */
    class Program
    {
        static void Main()
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

        private int _width {  get; set; }
        private int _length { get; set; }

        public Labirint(int[,] labirint)
        {
            _labirint = labirint;
            _length = labirint.GetLength(0);
            _width = labirint.GetLength(1);
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
            if (!IsMove(x, y))
                return;
            _labirint[x, y] = 2;
            FindExitsRecursivelyFromXY(x, y+1);
            FindExitsRecursivelyFromXY(x+1, y);
            FindExitsRecursivelyFromXY(x, y - 1);
            FindExitsRecursivelyFromXY(x-1, y);
        }

        public void FindExitByDenis(int x, int y)
        {
            if (_labirint[x, y] == 0)
                _exits++;
        }

        public void PrintLabirint()
        {
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write($"{_labirint[i,j]} ");
                }
                Console.WriteLine();
            }
        }

        private bool IsMove(int x, int y)
        {
            if (x < 0 || x >= _length)
                return false;
            if (y < 0 || y >= _width)
                return false;
            _exits += IsExit(x, _length, _labirint[x, y]) + IsExit(y, _width, _labirint[x, y]);
            return _labirint[x, y] == 0;
        }

        private int IsExit(int coord, int border, int segment)
        {
            if ((coord == 0 || coord == border - 1) && segment == 0)
                return 1;
            return 0;
        }
    }
}