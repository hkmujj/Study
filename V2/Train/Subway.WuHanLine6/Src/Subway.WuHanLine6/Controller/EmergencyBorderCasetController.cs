using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 紧急广播控制
    /// </summary>
    [Export]
    public class EmergencyBorderCasetController : ControllerBase<EmergencyBordercastModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EmergencyBorderCasetController()
        {
            SendBorderCast = new DelegateCommand<string>(SendBorderCastAction);
        }

        private void SendBorderCastAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            obj.SendBoolData(true);
            ViewModel.Units.Where(w => !w.IsChecked).ForEach(f => f.SendValueLogic.SendBoolData(false));
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitData()
        {
            ViewModel.Units.ForEach(f => f.IsChecked = false);
            ViewModel.Units.ForEach(f => f.SendValueLogic.SendBoolData(false));
        }
        /// <summary>
        /// 发送数据命令
        /// </summary>
        public ICommand SendBorderCast { get; private set; }
    }
}