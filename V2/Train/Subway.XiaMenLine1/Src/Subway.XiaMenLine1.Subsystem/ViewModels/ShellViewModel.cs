using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.Controller;
using Subway.XiaMenLine1.Subsystem.Extension;
using Subway.XiaMenLine1.Subsystem.Model;

namespace Subway.XiaMenLine1.Subsystem.ViewModels
{
    [Export]
    public class ShellViewModel : NotificationObject, INavigationAware, IUpdateStatusProvider, IDisposable, IDataListener
    {
        private string m_Btn8CommandPara;
        private string m_Btn9CommandPara;
        private bool m_IsEfeectKey;

        [ImportingConstructor]
        public ShellViewModel(MMIModel model, ShellController controller, TractionLockViewModel tractionLockViewModel)
        {
            Model = model;
            model.MMIBackCommand = controller.MainRuningNaviageteCommand;
            model.Dataserver = SubsysParams.Instance.SubsystemInitParam.CommunicationDataService;
            model.Init();
            Controller = controller;
            TractionLockViewModel = tractionLockViewModel;
            controller.ViewModel = this;
            controller.UpdateTitleName(ViewNames.MainRunningDoorPage);
        }
        public MMIModel Model { private set; get; }

        public ShellController Controller { private set; get; }

        public TractionLockViewModel TractionLockViewModel { private set; get; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Controller.UpdateTitleName(navigationContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }


        public void UpdateState(object sender, CommunicationDataChangedArgs e)
        {
            Model.ChangeStatus(sender, e);
            e.ChangedBools.UpdateIfContains(InBoolKeys.Inb电钥匙断开, b => { IsEfeectKey = b; });
        }

        public void Dispose()
        {
            Controller.Dispose();
            Model.Dispose();
            TractionLockViewModel.Dispose();
        }

        public string Btn8CommandPara
        {
            get { return m_Btn8CommandPara; }
            set
            {
                if (value == m_Btn8CommandPara)
                    return;

                m_Btn8CommandPara = value;
                RaisePropertyChanged(() => Btn8CommandPara);
            }
        }

        public string Btn9CommandPara
        {
            get { return m_Btn9CommandPara; }
            set
            {
                if (value == m_Btn9CommandPara)
                    return;

                m_Btn9CommandPara = value;
                RaisePropertyChanged(() => Btn9CommandPara);
            }
        }

        public bool IsEfeectKey
        {
            get { return m_IsEfeectKey; }
            set
            {
                if (value == m_IsEfeectKey)
                    return;

                m_IsEfeectKey = value;
                RaisePropertyChanged(() => IsEfeectKey);
            }
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            UpdateState(sender, dataChangedArgs);
        }
    }
}