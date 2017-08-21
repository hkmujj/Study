using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export]
    public class TractionLockViewModel : SubViewModelBase
    {
        public TractionLockViewModel()
        {
            var tmp =
                ExcelParser.Parser<TractionLockUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            AllTractionLock = new ObservableCollection<TractionLockUnit>(tmp);
            BeVigilant = AllTractionLock.FirstOrDefault(w => w.Type == 5);
            BCUFault = AllTractionLock.FirstOrDefault(w => w.Type == 6);
            TractionBrake = AllTractionLock.FirstOrDefault(w => w.Type == 7);
            B09 = AllTractionLock.FirstOrDefault(w => w.Type == 8);
            TwoEnd = AllTractionLock.FirstOrDefault(w => w.Type == 9);
            Speeding = AllTractionLock.FirstOrDefault(w => w.Type == 10);
            Dirction = AllTractionLock.FirstOrDefault(w => w.Type == 11);
            DCU = AllTractionLock.FirstOrDefault(w => w.Type == 12);
            HaveBrake = AllTractionLock.FirstOrDefault(w => w.Type == 13);
        }
        private ObservableCollection<TractionLockUnit> m_AllTractionLock;
        private TractionLockUnit m_HaveBrake;
        private TractionLockUnit m_DCU;
        private TractionLockUnit m_BeVigilant;

        public ObservableCollection<TractionLockUnit> AllTractionLock
        {
            get { return m_AllTractionLock; }
            set
            {
                if (Equals(value, m_AllTractionLock))
                {
                    return;
                }
                m_AllTractionLock = value;
                RaisePropertyChanged(() => AllTractionLock);
            }
        }
        /// <summary>
        /// 左侧门未关好
        /// </summary>
        public IEnumerable<TractionLockUnit> LeftDoor
        {
            get { return AllTractionLock.Where(w => w.Type == 1).OrderBy(o => o.Index); }
        }
        /// <summary>
        /// 右侧门未关好
        /// </summary>
        public IEnumerable<TractionLockUnit> RightDoor
        {
            get { return AllTractionLock.Where(w => w.Type == 2).OrderBy(o => o.Index); }
        }
        /// <summary>
        /// 停放制动未缓解
        /// </summary>
        public IEnumerable<TractionLockUnit> ParkingBrake
        {
            get { return AllTractionLock.Where(w => w.Type == 3).OrderBy(o => o.Index); }
        }
        /// <summary>
        /// 紧急停车蘑菇按下
        /// </summary>
        public IEnumerable<TractionLockUnit> EmergencyStop
        {
            get { return AllTractionLock.Where(w => w.Type == 4).OrderBy(o => o.Index); }
        }

        /// <summary>
        /// 警惕按钮
        /// </summary>
        public TractionLockUnit BeVigilant
        {
            get { return m_BeVigilant; }
            set
            {
                if (Equals(value, m_BeVigilant))
                {
                    return;
                }
                m_BeVigilant = value;
                RaisePropertyChanged(() => BeVigilant);
            }
        }

        /// <summary>
        /// 3个以上BCU严重故障
        /// </summary>
        public TractionLockUnit BCUFault
        {
            get; set;
        }
        /// <summary>
        /// 牵引制动同时存在
        /// </summary>
        public TractionLockUnit TractionBrake
        {
            get; set;
        }
        /// <summary>
        /// 切除5个以上B09
        /// </summary>
        public TractionLockUnit B09
        {
            get; set;
        }
        /// <summary>
        /// 两端同时占有
        /// </summary>
        public TractionLockUnit TwoEnd { get; set; }

        /// <summary>
        /// 超速
        /// </summary>
        public TractionLockUnit Speeding
        {
            get; set;
        }
        /// <summary>
        /// 方向矛盾
        /// </summary>
        public TractionLockUnit Dirction
        {
            get; set;
        }

        /// <summary>
        /// 4个DCU严重故障
        /// </summary>
        public TractionLockUnit DCU
        {
            get { return m_DCU; }
            set
            {
                if (Equals(value, m_DCU))
                {
                    return;
                }
                m_DCU = value;
                RaisePropertyChanged(() => DCU);
            }
        }

        /// <summary>
        /// 有制动未缓解
        /// </summary>
        public TractionLockUnit HaveBrake
        {
            get { return m_HaveBrake; }
            set
            {
                if (Equals(value, m_HaveBrake))
                {
                    return;
                }
                m_HaveBrake = value;
                RaisePropertyChanged(() => HaveBrake);
            }
        }

        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var unit in AllTractionLock.Where(w => !string.IsNullOrEmpty(w.LogicName)))
            {
                var index = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[unit.LogicName];
                if (args.NewValue.ContainsKey(index))
                {
                    unit.IsLock = args.NewValue[index];
                }
            }
        }
    }
}