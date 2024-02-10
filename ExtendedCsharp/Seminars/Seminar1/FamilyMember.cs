using System.Text;

namespace Seminar1
{
    enum Gender { Male, Female }
    class FamilyMember
    {
        public string? Name { get; set; }
        public Gender Gender { get; set;}

        public FamilyMember[]? Children { get; set; }
        public FamilyMember? Mother { get; set; }
        public FamilyMember? Father { get; set; }

        public FamilyMember() { }

        public FamilyMember(string name, Gender gender, FamilyMember mother, FamilyMember father, params FamilyMember[]? children)
        {
            this.Name = name;
            this.Gender = gender;
            this.Mother = mother;
            this.Father = father;
            this.Children = children!;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Print()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Children != null)
            {
                foreach (var child in this.Children)
                {
                    sb.Append($"{child} ");
                }
            }
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", this.Name,
                                                         this.Gender,
                                                         this.Mother,
                                                         this.Father,
                                                         sb);
        }

        public void AddFamilyMember(FamilyMember? mother, FamilyMember? father, params FamilyMember[]? children)
        {
            this.Mother = mother;
            this.Father = father;
            this.Children = children;
        }


        public void MothersInFamily()
        {
            #region toOldestMother
            FamilyMember adult = this;
            while (adult.Mother != null)
            {
                adult = adult.Mother;
            }
            #endregion


            #region PrintFromOldesWomenToYoungesGirl
            if (adult.IsFemale())
                adult.Print(); //print OldestWoman


            bool isFemale = true;
            while (isFemale)
            {
                isFemale = false;
                foreach (FamilyMember child in adult.Children)
                {
                    if (child.IsFemale())
                    {
                        adult = child;
                        isFemale = true;
                        child.Print();
                    }
                }
            }
            #endregion
        }

        public void PrintFamily()
        {
            FamilyMember secondMember = null;
            if (this.Children != null)
                secondMember = this.Gender == Gender.Male ? this.Children[0].Mother : this.Children[0].Father;
            if (secondMember != null)
                PrintFamily(this, secondMember);
            else
                PrintFamily(this);
        }

        private void PrintFamily(params FamilyMember[] familyMembers)
        {
            List<FamilyMember> children = new List<FamilyMember>();
            foreach (FamilyMember familyMember in familyMembers)
                Console.Write($"{familyMember} ");
            Console.WriteLine();
            foreach (FamilyMember familyMember in familyMembers) {
                    if (familyMember.Children != null) {
                        foreach (FamilyMember child in familyMember.Children) {
                            if (child.Children != null) {
                                foreach (FamilyMember child2 in child.Children) {
                                    FamilyMember? second = child2.Gender == Gender.Male ? child2.Mother : child2.Father;
                                    if (children.Contains(second!))
                                        children.Add(second!);
                                }
                            }
                            if (!children.Contains(child))
                                children.Add(child);
                        }
                    }
            }
            if (children.Count > 0)
                PrintFamily(children.ToArray());
        }
    }
    internal static class MyFamalyExtantion
    {
        internal static bool IsFemale(this FamilyMember f)
        {
            return (f.Gender == Gender.Female);
        }
    }
}
