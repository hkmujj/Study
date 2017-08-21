using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class BreakController : ControllerBase<BreakModel>
    {
        [ImportingConstructor]
        public BreakController()
        {

        }

        public override void Initalize()
        {

        }

        public void Clear()
        {

        }

        /// <summary>
        /// 设置除冰制动可选状态
        /// </summary>
        public void SetButtonEnable()
        {
            DomainViewModel.Instacnce.Model.StateInterface.BtnB1.IsEnable = true;
        }
        
        /// <summary>
        /// 点击除冰制动按钮发送数据
        /// </summary>
        public void RemoveIceBreakSendData()
        {
            OutBoolKey.除冰制动.SendBoolData(true,true);
        }
    }
}