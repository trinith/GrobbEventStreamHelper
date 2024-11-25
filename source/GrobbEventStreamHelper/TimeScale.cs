namespace GrobbEventStreamHelper
{
    public class TimeScale
    {
        public double Value { get; set; } = 1.0;

        public TimeScale() { }
        public TimeScale(double value)
        {
            this.Value = value;
        }
    }
}
