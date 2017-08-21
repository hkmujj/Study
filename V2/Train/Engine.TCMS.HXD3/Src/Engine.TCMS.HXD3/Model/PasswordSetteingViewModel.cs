using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model
{
    [Export(typeof(PasswordSetteingViewModel))]
    public class PasswordSetteingViewModel : NotificationObject
    {
        private string m_Password;

        [ImportingConstructor]
        public PasswordSetteingViewModel(PasswordSettingController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
        }

        public PasswordSettingController Controller { get; private set; }
        public TCMSViewModel Parent { get; set; }

        public string Password
        {
            get { return m_Password; }
            set
            {
                if (value == m_Password)
                {
                    return;
                }
                m_Password = value;
                RaisePropertyChanged(() => Password);
            }
        }
    }
}