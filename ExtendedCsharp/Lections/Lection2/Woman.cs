using System;
namespace Lection2
{
    class Woman : Person, IBabySitter
    {
        protected override string HelloPhrase => "Привет, я - женщина!";

        public Woman(string name) : base(name){}

        public Woman(string name, DateTime birthday) : base(name, birthday){}

        public bool HasMakeup { get; private set; } = false;
        public void PutMakeup()
        {
            System.Console.WriteLine("Наносит макияж");
            this.HasMakeup = true;
        }

        public void RemoveMakeup()
        {
            Console.WriteLine("Удаляет макияж");
            this.HasMakeup = false;
        }

        public override void SayHello()
        {
            Console.WriteLine("Привет, я - женщина!");
        }

        public void SayHelloLikeParent()
        {
            base.SayHello();
        }

        protected override void TakeCareImplementation()
        {
            Console.WriteLine("Кормит ужином, а затем укладывает спать.");
        }

        void IBabySitter.TakeCare()
        {
            if(Children!=null)
                Console.WriteLine("Сидит с детьми пока родители на работе!");
        }
    }
}