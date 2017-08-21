using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Infrastructure.ViewModel;

namespace MMITool.Platform.ViewModel
{
    [Export(typeof(IMainViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainViewModel : NotificationObject, IMainViewModel
    {
        private int m_WindHeight;
        private string m_WindTitle;
        private int m_WindWidth;
        private ICommand m_HelpCommand;

        public ICommand HelpCommand
        {
            get { return m_HelpCommand; }
            set
            {
                if (Equals(value, m_HelpCommand))
                {
                    return;
                }

                m_HelpCommand = value;
                RaisePropertyChanged(() => HelpCommand);
            }
        }

        public string WindTitle
        {
            get { return m_WindTitle; }
            set
            {
                if (value == m_WindTitle)
                {
                    return;
                }
                m_WindTitle = value;
                RaisePropertyChanged(() => WindTitle);
            }
        }

        public int WindWidth
        {
            get { return m_WindWidth; }
            set
            {
                if (value == m_WindWidth)
                {
                    return;
                }
                m_WindWidth = value;
                RaisePropertyChanged(() => WindWidth);
            }
        }

        public int WindHeight
        {
            get { return m_WindHeight; }
            set
            {
                if (value == m_WindHeight)
                {
                    return;
                }
                m_WindHeight = value;
                RaisePropertyChanged(() => WindHeight);
            }
        }

        public MainViewModel()
        {
            WindWidth = 800;
            WindHeight = 600;
            WindTitle = "Title";
        }
    }
}