using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// RBC 数据模型。
    /// </summary>
    [DebuggerDisplay("RbcId={RbcId}, TelNumber={TelNumber}")]
    public class RBCDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rbcId"></param>
        /// <param name="telNumber"></param>
        /// <param name="dataType"></param>
        /// <param name="inputtedFrom"></param>
        [DebuggerStepThrough]
        public RBCDataModel(string rbcId, string telNumber,
            RBCDataType dataType = RBCDataType.RBCID | RBCDataType.TelNumber,
            DataInputtedFrom inputtedFrom = DataInputtedFrom.MainNodeRequst)
        {
            RbcId = rbcId;
            TelNumber = telNumber;
            InputtedFrom = inputtedFrom;
            ValidDataType = dataType;
        }

        /// <summary>
        /// rbc id
        /// </summary>
        public string RbcId { private set; get; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelNumber { private set; get; }

        /// <summary>
        /// 有用的RBC数据类型  
        /// </summary>
        public RBCDataType ValidDataType { get; private set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public DataInputtedFrom InputtedFrom { get; private set; }
    }
}