using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Constant;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.View.Contents;
using Motor.HMI.CRH380D.View.Layout;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class SettingController : ControllerBase<SettingModel>
    {
        [ImportingConstructor]
        public SettingController()
        {
            SetLightCommand = new DelegateCommand<string>(OnSetLight);
            LoginOutCommand = new DelegateCommand(LoginOut);
        }

        public override void Initalize()
        {
            ViewModel.Light = 100;
        }

        public void Clear()
        {
            ViewModel.Light = 100;
        }

        /// <summary>
        /// 亮度调节处理
        /// </summary>
        /// <param name="strName"></param>
        private void OnSetLight(string strName)
        {
            switch (strName)
            {
                case "+":
                    ViewModel.Light = Math.Min(ViewModel.Light + 10, 100);
                    break;
                case "-":
                    ViewModel.Light = Math.Max(ViewModel.Light - 10, 0);
                    break;
            }
        }

        /// <summary>
        /// 注销处理
        /// </summary>
        private void LoginOut()
        {
            DomainViewModel.Instacnce.Controller.NavigateToView(RegionNames.DomainShellContent, typeof(LoginView).FullName);
            DomainViewModel.Instacnce.Model.LoginInfoModel.PassWord = string.Empty;
        }

        /// <summary>
        /// 亮度调节命令
        /// </summary>
        public ICommand SetLightCommand { get; set; }

        /// <summary>
        /// 注销
        /// </summary>
        public ICommand LoginOutCommand { get; set; }
    }
}