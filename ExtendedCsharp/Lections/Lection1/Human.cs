namespace Lection1
{
    internal abstract class Human
    {
        protected Gender Gender { get; set; }
        protected string Name { get; private set; }
        protected int Age { get; private set; }
        private static int _index { get; set; }
        protected int Id { get; private set; }

        public Human()
        {
            Name = string.Empty;
            Age = 0;
            Id = _index;
            _index++;
        }

        public void EditName(string name)
        {
            this.Name = name;
        }

        public void EditAge(int age)
        {
            this.Age = age;
        }

        public void EditGender(Gender gender)
        {
            this.Gender = gender;
        }

        public abstract void Info();
        
    }

    internal class Man : Human
    {
        private bool _beard { get; set; } = true;
        public Man()
        {
            this.Gender = Gender.Male;
        }
        public override void Info()
        {
            Console.WriteLine($"ID: {this.Id}, Name: {this.Name}, Gender: {this.Gender}, Age: {this.Age}, Beard: {_beard}");
        }

        public void Shave()
        {
            Console.WriteLine("Бреемся!");
            _beard = false;
        }
    }

    internal class Woman : Human
    {
        private bool _makeup { get; set; } = false;

        public Woman()
        {
            this.Gender = Gender.Female;
        }
        public override void Info()
        {
            Console.WriteLine($"ID: {this.Id}, Name: {this.Name}, Gender: {this.Gender}, Age: {this.Age}, Makeup: {_makeup}");
        }

        public bool IsMakeup()
        {
            return _makeup;
        }

        public void MakeUp()
        {
            Console.WriteLine("Наносим макияж!");
            _makeup = true;
        }

        public void MakeDown()
        {
            Console.WriteLine("Снимам макияж!");
            _makeup = false;
        }
    }
}