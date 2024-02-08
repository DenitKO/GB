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

        public void Print()
        {
            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", this.Name,
                                                         this.Gender, 
                                                         this.Mother,
                                                         this.Father,
                                                         this.Children);
        }

    }
}