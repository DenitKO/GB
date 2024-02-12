namespace Lection2
{
    internal interface IParent
    {
        public bool GetChildren(out Person[] children);
        public void TakeCare();
    }
}
