namespace ATP200C.MainView
{
    /// <summary>
    /// �г�����ģʽ
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