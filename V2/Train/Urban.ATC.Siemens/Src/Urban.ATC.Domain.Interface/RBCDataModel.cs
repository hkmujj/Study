using System.Diagnostics;

namespace Motor.ATP.Domain.Interface
{
    [DebuggerDisplay("RbcId={RbcId}, TelNumber={TelNumber}")]
    public class RBCDataModel
    {
        [DebuggerStepThrough]
        public RBCDataModel(string rbcId, string telNumber)
        {
            RbcId = rbcId;
            TelNumber = telNumber;
        }

        public string RbcId { private set; get; }

        public string TelNumber { private set; get; }
    }
}