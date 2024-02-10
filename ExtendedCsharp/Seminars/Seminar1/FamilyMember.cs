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

        public FamilyMember(string name, Gender gender, FamilyMember? mother, FamilyMember? father, params FamilyMember[]? children)
        {
            this.Name = name;
            this.Gender = gender;
            this.Mother = mother;
            this.Father = father;
            this.Children = children!;
        }

        public override string ToString()
        {
            return this.Name!;
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
            FamilyMember secondParent = null!;
            if (this.Children != null)
                secondParent = this.SecondParentFromAnotherParent()!;
            if (secondParent != null)
                PrintFamily(this, secondParent);
            else
                PrintFamily(this);
        }

        private void PrintFamily(params FamilyMember[] grandFathersAndMothers)
        {
            List<FamilyMember> children = new List<FamilyMember>();
            foreach (FamilyMember familyMember in grandFathersAndMothers)
                Console.Write($"{familyMember} ");
            Console.WriteLine();
            foreach (FamilyMember fathersAndMothers in grandFathersAndMothers) {
                    if (fathersAndMothers.Children != null) {
                        foreach (FamilyMember child in fathersAndMothers.Children) {
                            if (child.Children != null) {
                                foreach (FamilyMember childOfChildren in child.Children) {
                                    FamilyMember secondParent = childOfChildren.SecondParentFromChild()!;
                                    if (children.Contains(secondParent!))
                                        children.Add(secondParent!);
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

        public void SpouseAndSiblings()
        {
            FamilyMember? spouse = null!;
            if (this.Children != null)
                spouse = this.SecondParentFromAnotherParent()!;
            if(spouse != null)
            {
                string firthSpouse = this.Gender == Gender.Male ? "Муж: " : "Жена: ";
                string secondSpouse = spouse.Gender == Gender.Male ? "Муж: " : "Жена: ";
                Console.WriteLine($"{firthSpouse}{this}, {secondSpouse}{spouse}");
            }


            List<FamilyMember>? siblings = new List<FamilyMember>();
            if (this.Mother != null && this.Mother.Children != null)
            {
                foreach (FamilyMember child in this.Mother.Children)
                {
                    if(child != this)
                        siblings!.Add(child);
                }
            }
            if (this.Father != null && this.Father.Children != null)
            {
                foreach (FamilyMember child in this.Father.Children)
                {
                    if (child != this && !siblings.Contains(child))
                        siblings.Add(child);
                }
            }
            foreach (FamilyMember sibling in siblings.ToArray())
            {
                string stringSibling = sibling.Gender == Gender.Male ? "Брат: " : "Сестра: ";
                Console.Write($"{stringSibling}{sibling} ");
            }
        }
    }
    internal static class MyFamalyExtantion
    {
        internal static bool IsFemale(this FamilyMember f)
        {
            return (f.Gender == Gender.Female);
        }

        internal static FamilyMember? SecondParentFromChild(this FamilyMember f)
        {
            return f.Gender == Gender.Male ? f.Mother : f.Father;
        }

        internal static FamilyMember? SecondParentFromAnotherParent(this FamilyMember f)
        {
            return f.Gender == Gender.Male ? f.Children[0].Mother : f.Children[0].Father;
        }
    }
}
