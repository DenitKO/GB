namespace Lection2
{
    abstract class Person : IComparable, IParent
    {
        public readonly string Name = String.Empty;
        public readonly DateTime Birthday;
        public Person? Father = null;
        public Person? Mother = null;
        public Person[]? Children = null;

        protected abstract string HelloPhrase { get; }

        public Person(string name, DateTime birthday)
        {
            this.Name = name;

            if (birthday <= DateTime.Now)
                this.Birthday = birthday;
            else
            {
                Console.WriteLine($"Дата {birthday}, не верна! Присваиваем сегоднящнюю");
                this.Birthday = DateTime.Now;
            }
        }

        public Person(string name)
        {
            Name = name;
            Birthday = DateTime.Now;
        }

        #region Indexator
        private Person[] _family;
        public int Count { get { return 1 + (_family?.Length ?? 0); } }
        // public int Count => 1 + (_family?.Length ?? 0);

        public Person this[int index]
        {
            get
            {
                if (index <= 0) return this;
                if (_family is null)
                    return null;
                if (_family.Length >= index)
                    return _family[index - 1];
                return null;
                // блок null нужен на всякий случай, что бы он возвращял
                // все возможные ветвления, по идее он не будет использован
            }
        }
        #endregion
        public void AddFamilyInfo(Person? father, Person? mother, params Person[]? children)
        {
            Father = father;
            Mother = mother;
            Children = children;

            int familyCount = 0;

            familyCount += father == null ? 0 : 1;
            familyCount += mother == null ? 0 : 1;
            familyCount += children.Length;

            if (familyCount > 0)
                _family = new Person[familyCount];

            int counter = 0;

            if (father != null)
            {
                _family[counter] = father;
                counter++;
            }
            if (mother != null)
            {
                _family[counter] = mother;
                counter++;
            }
            if (children != null)
            {
                foreach (var child in children)
                {
                    _family[counter] = child;
                    counter++;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"Имя = {Name}, день рождения = {Birthday.ToString("dd.MM.yyyy")}");
        }

        public bool isAdult(int adultAge = 18)
        {
            var delta = DateTime.Now.Year - Birthday.Year;
            
            if (delta >= adultAge || (delta == adultAge && System.DateTime.Now.DayOfYear <= Birthday.DayOfYear))
            {
                return true;
            }
            else
                return false;
        }

        public string FullAge()
        {
            return (DateTime.Now-Birthday).Divide(365.25).Days.ToString();
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
                foreach (Person child in Children)
                {
                    child.Print();
                }
            }
        }

        public bool GetChildren(out Person[] children)
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
            Console.WriteLine("Привет, я - человек!");
        }

        public void SayHelloPhrase()
        {
            Console.WriteLine(this.HelloPhrase);
        }

        public static bool AreSiblings(Person p1, Person p2)
        {
            if (p1.Mother == null || p2.Mother == null) return false;
            if (p1.Father == null || p2.Father == null) return false;
            if (p1.Mother != p2.Mother && p1.Father != p2.Father) return false;
            return true;
        }

        public int CompareTo(object? obj)
        {
            if(obj == null) 
                return -1;
            return this.Birthday.CompareTo((obj as Person)!.Birthday);
        }

        protected abstract void TakeCareImplementation();

        public void TakeCare()
        {
            if (Children != null)
                TakeCareImplementation();
        }
    }
}