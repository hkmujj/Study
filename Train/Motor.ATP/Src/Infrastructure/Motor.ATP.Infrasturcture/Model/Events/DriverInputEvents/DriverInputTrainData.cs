using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents
{
    public class DriverInputTrainData
    {
        [DebuggerStepThrough]
        public DriverInputTrainData(ATPType atpType,string[] inputtedTrainData)
        {
            InputtedTrainData = inputtedTrainData;
            ATPType = atpType;
        }

        public ATPType ATPType { get; private set; }

        public string[] InputtedTrainData { private set; get; }
    }
}