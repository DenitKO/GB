namespace Lection2
{
    internal interface IFamily
    {
        int Count { get; }
        Person this[int index] { get; }
    }
}