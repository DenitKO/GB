using System;

namespace Seminar1
{
    class Program
    {
        static void Main(string[] args)
        {
            FamilyMember grandGrandDad = new FamilyMember("Прадедушка", Gender.Male, null, null, null);
            FamilyMember granGranMa = new FamilyMember("Прабабушка", Gender.Female, null, null, null);
            FamilyMember granMa = new FamilyMember("Бабушка", Gender.Female, granGranMa, grandGrandDad, null);
            FamilyMember granDad = new FamilyMember("Дедушка", Gender.Male, null, null, null);
            FamilyMember father = new FamilyMember("Папа", Gender.Male, null, null, null);
            FamilyMember mother1 = new FamilyMember("Мама1", Gender.Female, granMa, granDad, null);
            FamilyMember mother2 = new FamilyMember("Мама2", Gender.Female, granMa, granDad, null);
            FamilyMember dother1 = new FamilyMember("Дочка1", Gender.Female, mother1, father, null);
            FamilyMember dother2 = new FamilyMember("Дочка2", Gender.Female, mother2, null, null);
            FamilyMember dother3 = new FamilyMember("Дочка3", Gender.Female, mother2, null, null);
            granGranMa.AddFamilyMember(null, null, granMa);
            grandGrandDad.AddFamilyMember(null, null, granMa);
            granMa.AddFamilyMember(granGranMa, grandGrandDad, mother1, mother2);
            granDad.AddFamilyMember(null, null, mother1, mother2);
            father.AddFamilyMember(null, null, dother1);
            mother1.AddFamilyMember(granMa, granDad, dother1);
            mother2.AddFamilyMember(granMa, granDad, dother2, dother3);
            mother1.SpouseAndSiblings();
        }
    }
}