using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Models.Units;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class TractionViewModel : ViewModelBase
    {
        private TractionState m_Car5State;
        private TractionState m_Car4State;
        private TractionState m_Car3State;
        private TractionState m_Car2State;

        /// <summary>
        /// 
        /// </summary>
        public TractionViewModel()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<BoolDataChangedEvent>().Subscribe((DataChangedAction), ThreadOption.UIThread);

        }

        /// <summary>
        /// 
        /// </summary>
        public TractionState Car2State
        {
            get { return m_Car2State; }
            set
            {
                if (value == m_Car2State)
                {
                    return;
                }
                m_Car2State = value;
                RaisePropertyChanged(() => Car2State);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TractionState Car3State
        {
            get { return m_Car3State; }
            set
            {
                if (value == m_Car3State)
                {
                    return;
                }
                m_Car3State = value;
                RaisePropertyChanged(() => Car3State);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TractionState Car4State
        {
            get { return m_Car4State; }
            set
            {
                if (value == m_Car4State)
                {
                    return;
                }
                m_Car4State = value;
                RaisePropertyChanged(() => Car4State);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TractionState Car5State
        {
            get { return m_Car5State; }
            set
            {
                if (value == m_Car5State)
                {
                    return;
                }
                m_Car5State = value;
                RaisePropertyChanged(() => Car5State);
            }
        }

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            Car2State = TractionState.Off;
            Car3State = TractionState.Off;
            Car4State = TractionState.Off;
            Car5State = TractionState.Off;
        }

        private void DataChangedAction(DataChangedEventArgs<bool> dataChangedEventArgs)
        {
            var tmp = new Dictionary<string, Action>();
            tmp.Add(InBoolKeys.车2牵引切除, () => Car2State = TractionState.Cut);
            tmp.Add(InBoolKeys.车2牵引激活, () => Car2State = TractionState.Active);
            tmp.Add(InBoolKeys.车2牵引故障, () => Car2State = TractionState.Fault);
            tmp.Add(InBoolKeys.车2牵引断开, () => Car2State = TractionState.Off);
            tmp.Add(InBoolKeys.车3牵引切除, () => Car3State = TractionState.Cut);
            tmp.Add(InBoolKeys.车3牵引激活, () => Car3State = TractionState.Active);
            tmp.Add(InBoolKeys.车3牵引故障, () => Car3State = TractionState.Fault);
            tmp.Add(InBoolKeys.车3牵引断开, () => Car3State = TractionState.Off);
            tmp.Add(InBoolKeys.车4牵引切除, () => Car4State = TractionState.Cut);
            tmp.Add(InBoolKeys.车4牵引激活, () => Car4State = TractionState.Active);
            tmp.Add(InBoolKeys.车4牵引故障, () => Car4State = TractionState.Fault);
            tmp.Add(InBoolKeys.车4牵引断开, () => Car4State = TractionState.Off);
            tmp.Add(InBoolKeys.车5牵引切除, () => Car5State = TractionState.Cut);
            tmp.Add(InBoolKeys.车5牵引激活, () => Car5State = TractionState.Active);
            tmp.Add(InBoolKeys.车5牵引故障, () => Car5State = TractionState.Fault);
            tmp.Add(InBoolKeys.车5牵引断开, () => Car5State = TractionState.Off);
            dataChangedEventArgs.Data.UpdateIfContainWhenAllTrue(tmp, Start);
        }
    }

}