using System; 

namespace HomeWork3
{
    /*
    Доработайте приложение поиска пути в лабиринте, но на этот раз вам нужно 
    определить сколько всего выходов имеется в трёхмерном лабиринте:

    int[,,] labirynth = new int[5,5,5];

    Сигнатура метода:

    static int HasExit(int startI, int startJ, int[,,] l)
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

            // You can fill in the maze manually
            // Labirint labirint = new Labirint(arr);

            // You can fill in the maze randomly only seting fils
            // Labirint labirint = new Labirint(7,7);

            Labirint labirint = new Labirint(5,5,5);


            //labirint.FindExitsRecursivelyFromXY();
            //labirint.Print2DLabirint();

            labirint.FindExitsRecursivelyFromXYZ();
            labirint.Print3DLabirint();

            labirint.PrintExits();
        }
    }

    class Labirint
    {
        private int[,] _labirint2D { get; set; }
        private int[,,] _labirint3D { get; set; }
        public int _exits {  get; private set; } = 0;

        private int _width {  get; set; }
        private int _length { get; set; }
        private int _height { get; set; }

        public Labirint(int[,] labirint)
        {
            _labirint2D = labirint;
            _length = labirint.GetLength(0);
            _width = labirint.GetLength(1);
        }
        public Labirint(int length, int width)
        {
            _length = length;
            _width = width;
            Fill2DLabirint(50);
            _labirint2D[3, 3] = 0;
        }

        public Labirint(int length, int width, int height)
        {
            _length = length;
            _width = width;
            _height = height;
            Fill3DLabirint(200);
            _labirint3D[2, 2, 2] = 0;
        }

        public void PrintExits()
        {
            Console.WriteLine($"Количество выходов: {_exits}.");
        }


        public void Print2DLabirint()
        {
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write($"{_labirint2D[i,j]} ");
                }
                Console.WriteLine();
            }
        }

        public void Print3DLabirint()
        {
            for (int i = 0; i < _length; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    for (global::System.Int32 k = 0; k < _height; k++)
                    {
                        Console.Write($"{_labirint3D[i, j, k]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Рекурсивно находит выходы из лабиринта и записывает их.
        /// </summary>
        /// <param name="x">Стартовая позиция по X</param>
        /// <param name="y">Стартовая позиция по Y</param>
        public void FindExitsRecursivelyFromXY(int x = 3, int y = 3)
        {
            if (!IsMove2D(x, y))
                return;
            _labirint2D[x, y] = 2;
            FindExitsRecursivelyFromXY(x + 1, y);
            FindExitsRecursivelyFromXY(x - 1, y);
            FindExitsRecursivelyFromXY(x, y + 1);
            FindExitsRecursivelyFromXY(x, y - 1);
        }
        private bool IsMove2D(int x, int y)
        {
            if (x < 0 || x >= _length)
                return false;
            if (y < 0 || y >= _width)
                return false;
            _exits += IsExit(x, _length, _labirint2D[x, y]) + IsExit(y, _width, _labirint2D[x, y]);
            return _labirint2D[x, y] == 0;
        }
        /// <summary>
        /// Рекурсивно находит выходы из лабиринта и записывает их.
        /// </summary>
        /// <param name="x">Стартовая позиция по X</param>
        /// <param name="y">Стартовая позиция по Y</param>
        /// <param name="z">Стартовая позиция по Y</param> 
        public void FindExitsRecursivelyFromXYZ(int x = 2, int y = 2, int z = 2)
        {
            if (!IsMove3D(x, y, z))
                return;
            _labirint3D[x, y, z] = 2;
            FindExitsRecursivelyFromXYZ(x + 1, y, z);
            FindExitsRecursivelyFromXYZ(x - 1, y, z);
            FindExitsRecursivelyFromXYZ(x, y + 1, z);
            FindExitsRecursivelyFromXYZ(x, y - 1, z);
            FindExitsRecursivelyFromXYZ(x, y, z + 1);
            FindExitsRecursivelyFromXYZ(x, y, z - 1);
        }

        private bool IsMove3D(int x, int y, int z)
        {
            if (x < 0 || x >= _length)
                return false;
            if (y < 0 || y >= _width)
                return false;
            if (z < 0 || z >= _height)
                return false;
            _exits += IsExit(x, _length, _labirint3D[x, y, z]) + IsExit(y, _width, _labirint3D[x, y, z] + IsExit(z, _width, _labirint3D[x, y, z]));
            return _labirint3D[x, y, z] == 0;
        }


        private int IsExit(int coord, int border, int segment)
        {
            if ((coord == 0 || coord == border - 1) && segment == 0)
                return 1;
            return 0;
        }

        private void Fill2DLabirint(int count)
        {
            _labirint2D = new int[_length, _width];
            Random rnd = new Random();
            while (count>0) 
            {
                _labirint2D[rnd.Next(_length), rnd.Next(_width)] = 1;
                count--;
            }
        }

        private void Fill3DLabirint(int count)
        {
            _labirint3D = new int[_length, _width, _height];
            Random rnd = new Random();
            while (count > 0)
            {
                _labirint3D[rnd.Next(_length), rnd.Next(_width), rnd.Next(_height)] = 1;
                count--;
            }
        }
    }
}