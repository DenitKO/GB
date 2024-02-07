using System;
namespace Lection1
{
    class Man : Person
    {
        protected override string HelloPhrase {get; set;} = "Привет, я - мужчина!";

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
    }
}