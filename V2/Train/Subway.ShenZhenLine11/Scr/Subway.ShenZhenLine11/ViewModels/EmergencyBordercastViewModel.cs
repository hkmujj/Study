using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Controller;
using Subway.ShenZhenLine11.Info;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(EmergencyBordercastViewModel))]
    public class EmergencyBordercastViewModel : SubViewModelBase
    {
        [ImportingConstructor]
        public EmergencyBordercastViewModel(EmergencyBordercastController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            Manger.CurrentChanged += M_Manger_CurrentChanged;
            Manger.CurrentPageChanged += Manger_CurrentPageChanged;
            Manger.Load(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);

        }

        private void Manger_CurrentPageChanged(int arg1, int arg2)
        {
            CurrentInfo=new ObservableCollection<EmergencyBordercastInfo>(Manger.Display);
        }

        private void M_Manger_CurrentChanged(IList<EmergencyBordercastInfo> arg1, IList<EmergencyBordercastInfo> arg2)
        {
            CurrentInfo = new ObservableCollection<EmergencyBordercastInfo>(arg2);
        }

        public EmergencyBordercastController Controller { get; private set; }


        private IEnumerable<EmergencyBordercastInfo> m_CurrentInfo;

        public IEnumerable<EmergencyBordercastInfo> CurrentInfo
        {
            get { return m_CurrentInfo; }
            set
            {
                if (Equals(value, m_CurrentInfo))
                {
                    return;
                }
                m_CurrentInfo = value;
                RaisePropertyChanged(() => CurrentInfo);
            }
        }
        public readonly EnmergencyBordercastManager Manger = new EnmergencyBordercastManager();
        public override void Clear()
        {
            base.Clear();

            Manger.Reset();
        }
    }
}