using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Constant;
using Motor.HMI.CRH380D.Models.Model;
using Motor.HMI.CRH380D.View.Layout;
using Motor.HMI.CRH380D.ViewModel;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Motor.HMI.CRH380D.Controller
{
    [Export]
    public class LoginController : ControllerBase<LoginInfoModel>
    {
        [ImportingConstructor]
        public LoginController()
        {
            LoginCommand = new DelegateCommand<string>(OnLogin);

            LoginEnterCommand = new DelegateCommand(OnLoginEnter);

            LoginClearCommand = new DelegateCommand(OnLoginClear);
        }

        public override void Initalize()
        {
            ViewModel.PassWord = string.Empty;
        }

        public void Clear()
        {
            ViewModel.PassWord = string.Empty;
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="obj"></param>
        private void OnLogin(string obj)
        {
            if (obj.Contains("Del") && ViewModel.PassWord.Length > 0)
            {
                ViewModel.PassWord = ViewModel.PassWord.Substring(0, ViewModel.PassWord.Length - 1);
            }
            else
            {
                ViewModel.PassWord += obj;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void OnLoginEnter()
        {
            foreach (var value in ViewModel.AllLoginInfo)
            {
                if (ViewModel.PassWord == value.PassWord)
                {
                    DomainViewModel.Instacnce.Controller.NavigateToView(RegionNames.DomainShellContent, typeof(ShellContentStyle1Layout).FullName);
                    DomainViewModel.Instacnce.Controller.NavigateToRoot();
                }
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        private void OnLoginClear()
        {
            ViewModel.PassWord = string.Empty;
        }

        public ICommand LoginCommand { private set; get; }

        public ICommand LoginEnterCommand { private set; get; }

        public ICommand LoginClearCommand { private set; get; }
    }
}