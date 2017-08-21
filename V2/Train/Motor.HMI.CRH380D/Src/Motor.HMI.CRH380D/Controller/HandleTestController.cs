using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Extension;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.Resources.Keys;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class HandleTestController : ControllerBase<HandleTestModel>
    {
        [ImportingConstructor]
        public HandleTestController()
        {

        }

        public override void Initalize()
        {
            
        }

        public void Clear()
        {

        }

        /// <summary>
        /// 更新提示文本
        /// </summary>
        /// <param name="Num"></param>
        public void UpdateHandleTestInfoDisplay(int Num)
        {
            foreach (var value in ViewModel.AllHandleTestInfo)
            {
                if (value.Index == Num)
                {
                    ViewModel.HandleTestInfoDisplay = value.Info;
                    if (string.IsNullOrEmpty(value.Info))
                    {
                        ViewModel.IsEnable = false;
                    }
                    else
                    {
                        ViewModel.IsEnable = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// 点击激活试验按钮发送数据
        /// </summary>
        public void ActivateTest()
        {
            OutBoolKey.激活试验.SendBoolData(true, true);
        }
    }
}