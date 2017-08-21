using System.ComponentModel.Composition;
using System.Threading;
using System.Windows.Input;
using Engine._6A.Interface.ViewModel.SystemSeting;
using Microsoft.Practices.Prism.Commands;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IWorkModelViewModel))]
    public class WorkModelViewModel : ViewModelBase, IWorkModelViewModel
    {
        private readonly Timer m_Timer;
        private string m_WorkText;

        public WorkModelViewModel()
        {
            WorkChanged = new DelegateCommand<string>(WorkChangedMethod);
            m_Timer = new Timer((sate) => { WorkText = ""; }, null, int.MaxValue, int.MaxValue);
        }

        private void WorkChangedMethod(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            if (obj.Equals("Normal"))
            {
                WorkText = "设置成功后屏幕上方模式信息消失";
            }
            else if (obj.Equals("Test"))
            {
                WorkText = "设置成功后屏幕上方显示[试验模式]";
            }
            else if (obj.Equals("Train"))
            {
                WorkText = "设置成功后屏幕上方显示[调车模式]";
            }
            m_Timer.Change(5000, int.MaxValue);
        }

        public string WorkText
        {
            get { return m_WorkText; }
            set
            {
                if (value == m_WorkText)
                {
                    return;
                }
                m_WorkText = value;
                RaisePropertyChanged(() => WorkText);
            }
        }

        public ICommand WorkChanged { get; private set; }
    }
}