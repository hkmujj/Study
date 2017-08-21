using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP._200H.Subsys.Model;

namespace Motor.ATP._200H.Subsys.Controller
{
    public class DoMainController
    {

        public static readonly DoMainController Instance = new DoMainController();
        [Browsable(false)]
        public ATP200HDomainModel Model { get; private set; }
        public void Initalization(ATP200HDomainModel model)
        {
            Model = model;
            SwichAssist = new DelegateCommand(SwichAssistMethod);
        }

        private void SwichAssistMethod()
        {
            Model.SendInterface.AssistSwich(new SendModel<bool>(!Model.AssistDisplayInfo.Visible));
        }
        /// <summary>
        /// 切换辅屏命令
        /// </summary>
        public ICommand SwichAssist { get; private set; }
    }
}