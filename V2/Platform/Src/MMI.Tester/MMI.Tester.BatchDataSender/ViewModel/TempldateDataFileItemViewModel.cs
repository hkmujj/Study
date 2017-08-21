using System.IO;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Tester.BatchDataSender.Model;

namespace MMI.Tester.BatchDataSender.ViewModel
{
    public class TempldateDataFileItemViewModel : NotificationObject
    {
        private bool m_FileHasError;

        private string m_File;

        public DelegateCommand SelectFileCommand { get; private set; }

        public DelegateCommand ApplyCommand { get; private set; }

        public SendDataType DataType { get; private set; }


        private IEventAggregator m_EventAggregator;

        public string File
        {
            set
            {
                if (value == m_File)
                {
                    return;
                }
                m_File = value;
                FileHasError = false;
                RaisePropertyChanged(() => File);
            }
            get { return m_File; }
        }

        public bool FileHasError
        {
            set
            {
                if (value == m_FileHasError)
                {
                    return;
                }
                m_FileHasError = value;
                ApplyCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => FileHasError);
            }
            get { return m_FileHasError; }
        }

        public TempldateDataFileItemViewModel(SendDataType dataType)
        {
            DataType = dataType;
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            ApplyCommand = new DelegateCommand(OnApply, CanApply);

            SelectFileCommand = new DelegateCommand(OnSelectFile, CanSelectFile);

            FileHasError = true;
        }

        private void OnSelectFile()
        {
            var dir = BatchSenderParam.Instance.Param.SubsystemParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory;
            if (System.IO.File.Exists(File))
            {
                dir = Path.GetDirectoryName(File);
            }

            var form = new OpenFileDialog() { Filter = "测试数据文件|*.xls;*.csv", InitialDirectory = dir };
            var rlt = form.ShowDialog(BatchSenderParam.Instance.Param.ParentForm);
            if (rlt == DialogResult.OK)
            {
                File = form.FileName;
            }
        }

        private bool CanSelectFile()
        {
            return true;
        }

        private void OnApply()
        {
            m_EventAggregator.GetEvent<CompositePresentationEvent<TempldateDataFileItemViewModel>>().Publish(this);
        }

        private bool CanApply()
        {
            return !FileHasError;
        }
    }
}