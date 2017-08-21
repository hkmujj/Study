namespace CRH2MMI.Common.View.Train
{
    interface ICarStateProxy
    {
        CarState UserSetState { set; get; }
    }

    class CarStateProxy : ICarStateProxy
    {
        public CarState UserSetState { get; set; }
    }
}