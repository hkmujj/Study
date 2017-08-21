using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 烟火界面控制类
    /// </summary>
    [Export]
    public class SmokeContrller : ControllerBase<SmokeModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SmokeContrller()
        {
            SendSmokeCut = new DelegateCommand<string>(SendSmokeCutAction);
        }

        private void SendSmokeCutAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            obj.SendBoolData(true, true);
        }

        /// <summary>
        /// 烟火复位命令
        /// </summary>
        public ICommand SendSmokeCut { get; private set; }
    }
}