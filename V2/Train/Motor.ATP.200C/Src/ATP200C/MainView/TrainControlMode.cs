namespace ATP200C.MainView
{
    /// <summary>
    /// 列车控制模式
    /// </summary>
    public static class TrainControlMode
    {
        static TrainControlMode()
        {
            CurrentTrainControl = ControlModeName.Other;
        }

        public static ControlModeName CurrentTrainControl { get; set; }
    }
}