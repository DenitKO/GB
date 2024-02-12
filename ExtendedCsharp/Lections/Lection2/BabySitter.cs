namespace Lection2
{
    internal class BabySitter : IBabySitter
    {
        public void TakeCare()
        {
            Console.WriteLine("Сидит с детьми пока родители в кинотеатре");
        }

        public void Hello()
        {
            Console.WriteLine("Hi, I'm a babysiter");
        }
    }
}
