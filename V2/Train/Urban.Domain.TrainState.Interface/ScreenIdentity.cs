namespace Urban.Domain.TrainState.Interface
{
    public class ScreenIdentity
    {
        public string ProjectName { set; get; }

        public override string ToString()
        {
            return ProjectName;
        }

        public override int GetHashCode()
        {
            return ProjectName.GetHashCode();
        }
    }
}
