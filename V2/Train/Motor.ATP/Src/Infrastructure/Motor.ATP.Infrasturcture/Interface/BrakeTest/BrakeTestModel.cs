namespace Motor.ATP.Infrasturcture.Interface.BrakeTest
{
    public class BrakeTestModel
    {
    }

    public class BrakeTestModel<T> : BrakeTestModel
    {
        public BrakeTestModel(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}