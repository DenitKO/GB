using System;
namespace Lection2
{
    class Man : Person
    {
        protected override string HelloPhrase => "Привет, я - мужчина!";

        public Man(string name) : base(name){}

        public Man(string name, DateTime birthday) : base(name, birthday){}

        public bool HasBeard { get; private set; } = true;

        public void Shave()
        {
            System.Console.WriteLine("Бреется");
            this.HasBeard = false;
        }

        public override void SayHello()
        {
            System.Console.WriteLine("Привет, я - мужчина!");
        }

        public void SayHelloLikeParent()
        {
            base.SayHello();
        }

        protected override void TakeCareImplementation()
        {
            Console.WriteLine("Проверяет уроки и потом идет с детьми на прогулку.");
        }
    }
}