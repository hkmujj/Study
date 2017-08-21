using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Resources.Keys;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class TractionAndBreakController : ControllerBase<TractionAndBreakModel>
    {
        [ImportingConstructor]
        public TractionAndBreakController()
        {

        }

        public override void Initalize()
        {
            
        }

        public void Clear()
        {

        }
        
        /// <summary>
        /// 点击切除电制动按钮发送数据
        /// </summary>
        public void CutOffElectricBreak()
        {
            OutBoolKey.切除电制动.SendBoolData(true, true);
        }
    }
}