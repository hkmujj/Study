using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMI.Tester.BatchDataSender.Model;
using MMI.Tester.BatchDataSender.ViewModel;

namespace MMI.Tester.BatchDataSender.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestDataController : ControllerBase<TestDataViewModel>
    {
        protected ICommunicationDataWriteService WriteService
        {
            get
            {
                return BatchSenderParam.Instance.Param.SubsystemParam.CommunicationDataService.WritableReadService;
            }
        }

        private Dictionary<int, bool> m_BoolBuffer = new Dictionary<int, bool>();

        private Dictionary<int, float> m_FloatBuffer = new Dictionary<int, float>();

        public DelegateCommand<CommandParameter> SelectChangedCommand { private set; get; }

        public DelegateCommand SendDataCommand { private set; get; }

        public TestDataController()
        {
            SendDataCommand = new DelegateCommand(OnSendData, CanSendData);
            SelectChangedCommand = new DelegateCommand<CommandParameter>(OnSelectChanged);
        }

        private void OnSelectChanged(CommandParameter commandParameter)
        {
            var args = (CurrentColumnChangedEventArgs) commandParameter.EventArgs;

            if (args.NewColumn != null)
            {
                ViewModel.Model.CurrentColumnIndex = args.NewColumn.VisibleIndex;
            }
            else
            {
                ViewModel.Model.CurrentColumnIndex = -1;
            }
        }

        private bool CanSendData()
        {
            return BatchSenderParam.Instance.Param.SubsystemParam.DataPackage.Config.SystemConfig.IsDebugModel && ViewModel != null &&
                   ViewModel.Model.CurrentColumnIndex >= 2;
        }

        private void OnSendData()
        {
            switch (ViewModel.Model.SendDataType)
            {
                case SendDataType.InBool:
                    m_BoolBuffer.Clear();
                    for (int i = 0; i < ViewModel.Model.DataTable.Rows.Count; i++)
                    {
                        var row =ViewModel.Model.DataTable.Rows[i];
                        m_BoolBuffer.Add(ViewModel.Model.IndexTable[i], !Convert.IsDBNull(row[ViewModel.Model.CurrentColumnIndex]) && row[ViewModel.Model.CurrentColumnIndex].ToString() == "1");
                    }
                    WriteService.ChangeBools(m_BoolBuffer, true);
                    break;
                case SendDataType.InFloat:
                    m_FloatBuffer.Clear();
                    for (int i = 0; i < ViewModel.Model.DataTable.Rows.Count; i++)
                    {
                        var row = ViewModel.Model.DataTable.Rows[i];
                        m_FloatBuffer.Add(ViewModel.Model.IndexTable[i],
                            Convert.IsDBNull(row[ViewModel.Model.CurrentColumnIndex])
                                ? 0
                                : Convert.ToSingle(row[ViewModel.Model.CurrentColumnIndex]));
                       
                    }
                    WriteService.ChangeFloats(m_FloatBuffer, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}