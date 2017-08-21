using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.Model;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 制动自检控制类
    /// </summary>
    [Export]
    public class BrakeAutoCheckController : ControllerBase<BrakeAutoCheckModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BrakeAutoCheckController()
        {
            AutoCheckEnd = new DelegateCommand(() =>
            {
                OutBoolKeys.OutBo制动自检界面终止.SendBoolData(true, true);
            }, () => ViewModel.IsAutoChecking);
            AutoCheckCommand = new DelegateCommand(() =>
            {
                OutBoolKeys.OutBo制动自检界面开始.SendBoolData(true, true);
                ViewModel.IsAutoChecking = true;
            }, () => !ViewModel.IsAutoChecking);
        }

        /// <summary>
        /// 自动自检命令
        /// </summary>
        public DelegateCommand AutoCheckCommand { get; private set; }

        /// <summary>
        /// 自动自检终止命令
        /// </summary>
        public DelegateCommand AutoCheckEnd { get; private set; }
    }
}