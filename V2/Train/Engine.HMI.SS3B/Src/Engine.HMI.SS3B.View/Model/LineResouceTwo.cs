namespace Engine.HMI.SS3B.View.Model
{
    public class LineResouceTwo : LineResouceOne
    {
        public override double GetScal(double currentValue)
        {
            return (double) currentValue/(double) MaxValue;
        }
    }
}