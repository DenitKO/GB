namespace Lection2
{
    internal struct Metric : IComparable<Metric>
    {
        public int Month;
        public int Temperature;
        public int Days;
        public int CompareTo(Metric metric)
        {
            int res = this.Month.CompareTo(metric.Month);
            if (res != 0) 
            {
                return res;
            }
            else
            {
                return this.Temperature.CompareTo(metric.Temperature);
            }
        }

        public override string ToString()
        {
            return $"{{{Month}:{Temperature}:{Days}}}";
        }
    }
}
