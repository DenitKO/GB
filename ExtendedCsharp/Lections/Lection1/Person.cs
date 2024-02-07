using System;
namespace Lection1
{
    class Person
    {
        public readonly string Name;
        public readonly System.DateTime Birthday;
        public Person? Father = null;
        public Person? Mother = null;
        public Person[]? Children = null;

        protected virtual string HelloPhrase {get; set;} = "Привет, я человек!";

        public Person(string name, System.DateTime birthday)
        {
            Birthday = birthday;
            Name = name;
            if (birthday <= System.DateTime.Now)
                this.Birthday = birthday;
            else
            {
                System.Console.WriteLine($"Дата {birthday}, не верна! Присваиваем сегоднящнюю");
                this.Birthday = System.DateTime.Now;
            }
        }

        public Person(string name)
        {
            Name = name;
            Birthday = DateTime.Now;
        }

        public void Print()
        {
            System.Console.WriteLine($"Имя = {Name}, день рождения = {Birthday.ToString("dd.MM.yyyy")}");
        }

        public bool isAdult(int adultAge = 18)
        {
            var delta = System.DateTime.Now.Year - Birthday.Year;
            
            if (delta >= adultAge ||
               (delta == adultAge && System.DateTime.Now.DayOfYear <= Birthday.DayOfYear))
            {
                return true;
            }
            else
                return false;
        }

        public string FullAge()
        {
            return (System.DateTime.Now-Birthday).Divide(365.25).Days.ToString();
        }
        
        public void AddFamilyInfo(Person? father, Person? mother, params Person[]? children)
        {
            Father = father;
            Mother = mother;
            Children = children;
        }

        public void PrintFamilyInfo()
        {
            if (Father != null)
            {
                Console.Write("Отец: ");
                Father.Print();
            }
            if (Mother != null)
            {
                Console.Write("Мать: ");
                Mother.Print();
            }
            if (Children != null && Children.Length > 0)
            {
                Console.WriteLine("Дети: ");
                foreach (var child in Children)
                {
                    child.Print();
                }
            }
        }

        public bool GetChildren(out Person[]? children)
        {
            if (Children!= null && Children.Length != 0)
            {
                children = Children;
                return true;
            }
            else
            {
                children = null;
                return false;
            }
        }

        public virtual void SayHello()
        {
            System.Console.WriteLine("Привет, я - человек!");
        }

        public void SayHelloPhrase()
        {
            System.Console.WriteLine(this.HelloPhrase);
        }

        public static bool AreSiblings(Person p1, Person p2)
        {
            if (p1.Mother == null || p2.Mother == null) return false;
            if (p1.Father == null || p2.Father == null) return false;
            if (p1.Mother != p2.Mother && p1.Father != p2.Father) return false;
            return true;
        }
    }
}