using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.ConfigModel;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class LoginInfoModel : ModelBase
    {
        private string m_PassWord;
        private ObservableCollection<LoginInfo> m_AllLoginInfo;

        [ImportingConstructor]
        LoginInfoModel(LoginController loginController)
        {
            m_AllLoginInfo = new ObservableCollection<LoginInfo>(GlobalParam.Instance.LoginInfos.OrderBy(o => o.id));

            LoginController = loginController;
            LoginController.ViewModel = this;
            LoginController.Initalize();
        }
        
        /// <summary>
        /// 控制
        /// </summary>
        public LoginController LoginController { get; set; }

        /// <summary>
        /// 所有制动试验提示文本
        /// </summary>
        public ObservableCollection<LoginInfo> AllLoginInfo
        {
            get { return m_AllLoginInfo; }
            set
            {
                if (value == m_AllLoginInfo)
                {
                    return;
                }
                m_AllLoginInfo = value;
                RaisePropertyChanged(() => AllLoginInfo);
            }
        }

        /// <summary>
        /// 输入密码
        /// </summary>
        public string PassWord
        {
            get { return m_PassWord; }
            set
            {
                if (value == m_PassWord)
                {
                    return;
                }
                m_PassWord = value;
                RaisePropertyChanged(() => PassWord);
            }
        }
    }
}
