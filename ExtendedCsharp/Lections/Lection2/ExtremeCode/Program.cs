namespace ExtremeCode
{
    class Program
    {
        abstract class Animal{}
        class Cat : Animal{}


        abstract class Engine{}
        class V8Engine : Engine {}
        interface ICar<out T> where T : Engine
        {
            T Engine{ get; }
            T GetEngine();

            // void SetEngine(T engine); //Error
            // T EngineProp { set; } //Error
        }
        class Lada : ICar<V8Engine>
        {
            public V8Engine Engine { get; }
            public V8Engine GetEngine()
            {
                return new V8Engine();
            }
        }


        public static void Main(string[] args)
        {
            List<Cat> list = new List<Cat>();
            IEnumerable<Cat> cats = list;
            IEnumerable<Animal> animal = cats;
            // Почему List<T> не ковариантен, а инвариантен?
            // Потому что это актуально для обобщенных интерфейсов (Еще на самом деле для делегатов)

            Lada lada = new Lada();
            ICar<V8Engine> vEightCar = lada;
            ICar<Engine> someCar = lada; // тут была бы ошибка "cannot implicitly" если бы не <out T>
        }
    }
}