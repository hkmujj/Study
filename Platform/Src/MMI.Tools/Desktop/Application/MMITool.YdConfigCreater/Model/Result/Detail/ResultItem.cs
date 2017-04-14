using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMITool.Addin.YdConfigCreater.Events;
using MMITool.Addin.YdConfigCreater.Model.Constant;

namespace MMITool.Addin.YdConfigCreater.Model.Result.Detail
{
    [DebuggerDisplay("ModuleType={ModuleType}")]
    public class ResultItem : NotificationObject
    {
        private string m_Content;

        public ResultItem(ModuleType moduleType, string fileFullName)
        {
            ModuleType = moduleType;
            FileFullName = fileFullName;
            CopyFileCommand = new DelegateCommand(OnCopyFile, CanCopyFile);
        }

        private bool CanCopyFile()
        {
            return !string.IsNullOrEmpty(Content);
        }

        private void OnCopyFile()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<CopyResultItemFileEvent>()
                .Publish(new CopyResultItemFileEvent.Args(this));
        }

        public ModuleType ModuleType { get; private set; }

        public string FileFullName { get; private set; }

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (value == m_Content)
                {
                    return;
                }

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        public ICommand CopyFileCommand { get; private set; }
    }
}