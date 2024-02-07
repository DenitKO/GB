using System;
namespace Lection1
{
    class Woman : Person
    {
        protected override string HelloPhrase {get; set;} = "Привет, я - женщина!";

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
            System.Console.WriteLine("Удаляет макияж");
            this.HasMakeup = false;
        }

        public override void SayHello()
        {
            System.Console.WriteLine("Привет, я - женщина!");
        }

        public void SayHelloLikeParent()
        {
            base.SayHello();
        }
    }
}