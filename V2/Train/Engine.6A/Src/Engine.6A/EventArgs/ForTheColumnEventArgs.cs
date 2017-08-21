namespace Engine._6A.EventArgs
{
    public class ForTheColumnEventArgs
    {
        public ForTheColumnEventArgs(string trainName)
        {
            TrainName = trainName;
        }
        public string TrainName { get; set; }
    }
}