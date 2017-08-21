using CBTC.DataAdapter.Model;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Send;

namespace CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class SendInterfaceCASCO : SendInterfaceBase
    {
        public SendInterfaceCASCO(SignalDataOut dataOut) : base(dataOut)
        {
        }

        /// <summary>
        /// 发送后备模式是否强制
        /// </summary>
        /// <param name="bm">true 强制 false 非强制</param>
        /// <returns>true 发送成功  false  发送失败</returns>
        public override bool InputBM(SendModel<bool> bm)
        {
            //bm.Content   true  强制  false 非强制
            // DataOut.CBTC.Request.RequestView(ViewNames.Menu);
            //DataOut.CBTC.Request.RequestView(ViewNames.BM);
            //object objTest = new NewType();
            //NewType newValue = objTest as NewType;

            //SignalDataOut tempdataout = new SignalDataOut();
            //SignalDataOutCASCO dataout = tempdataout as SignalDataOutCASCO;

           // SignalDataOutCASCO dataout = (SignalDataOutCASCO) DataOut;
           SignalDataOutCASCO dataout = DataOut as SignalDataOutCASCO;
            if (bm.Content)
            {
                dataout.BMACK_Confirm = true;
            }
            else
            {
                dataout.BMUNACK_Confirm = true;
            }
            return true;
            //return base.InputBM(bm);
        }
    }
}