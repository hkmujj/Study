using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.YdConfigCreater.Model
{
    [Export]
    public class ShellModel: NotificationObject
    {
        private string m_FooterText;

        public ICommand ClearFooterCommand { get; set; }

        public string FooterText
        {
            get { return m_FooterText; }
            set
            {
                if (value == m_FooterText)
                {
                    return;
                }

                m_FooterText = value;
                RaisePropertyChanged(() => FooterText);
            }
        }
    }
}