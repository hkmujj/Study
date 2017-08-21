using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Motor.TCMS.CRH400BF.Config;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class DoorStateItem : CarItem<DoorState, CarDoorConfig>
    {
        public DoorStateItem(int carIndex, CarDoorConfig indexString) : base(carIndex, indexString)
        {
        }

    }

    //public class DoorLockStateItem : CarItem<DoorLockState, CarDoorLockStateConfig>
    //{
    //    public DoorLockStateItem(int carIndex, CarDoorLockStateConfig lockStateConfig) : base(carIndex, lockStateConfig)
    //    {
    //    }
    //}

    public class Door : CarPartBase
    {
        //private ObservableCollection<DoorLockStateItem> m_LockStates;
        private ObservableCollection<DoorStateItem> m_DoorItems;

        private bool m_Surchage2;
        private bool m_Surchage1;

        public Door(ItemLocationStyle itemLocationStyle)
        {
            ItemLocationStyle = itemLocationStyle;
        }

        public IEnumerable<DoorStateItem> UpItems { get; private set; }
        public IEnumerable<DoorStateItem> DownItems { get; private set; }

        //public DoorLockStateItem UpDoorLockStateItem { get; private set; }

        //public DoorLockStateItem DownDoorLockStateItem { get; private set; }

        public ObservableCollection<DoorStateItem> DoorItems
        {
            get { return m_DoorItems; }
            set
            {
                if (Equals(value, m_DoorItems))
                {
                    return;
                }

                m_DoorItems = value;
                switch (ItemLocationStyle)
                {
                    case ItemLocationStyle.LeftStyle:
                        UpItems = value.Where(w => w.CarIndex % 2 == 0);
                        DownItems = value.Where(w => w.CarIndex % 2 != 0);
                        break;
                    case ItemLocationStyle.RightStyle:
                        DownItems = value.Where(w => w.CarIndex % 2 == 0).Reverse();
                        UpItems = value.Where(w => w.CarIndex % 2 != 0).Reverse();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                RaisePropertyChanged(() => DoorItems);
                RaisePropertyChanged(() => UpItems);
                RaisePropertyChanged(() => DownItems);
            }
        }

        /// <summary>
        /// 超员报警1
        /// </summary>
        public bool Surchage1
        {
            get { return m_Surchage1; }
            set
            {
                if (value == m_Surchage1)
                    return;

                m_Surchage1 = value;
                RaisePropertyChanged(() => Surchage1);
            }
        }

        /// <summary>
        /// 超员报警2
        /// </summary>
        public bool Surchage2
        {
            get { return m_Surchage2; }
            set
            {
                if (value == m_Surchage2)
                    return;

                m_Surchage2 = value;
                RaisePropertyChanged(() => Surchage2);
            }
        }

        public ItemLocationStyle ItemLocationStyle { get; private set; }

        //public ObservableCollection<DoorLockStateItem> LockStates
        //{
        //    get { return m_LockStates; }
        //    set
        //    {
        //        if (value == m_LockStates)
        //        {
        //            return;
        //        }

        //        m_LockStates = value;
        //        switch (ItemLocationStyle)
        //        {
        //            case ItemLocationStyle.LeftStyle:
        //                UpDoorLockStateItem = LockStates[0];
        //                DownDoorLockStateItem = LockStates[1];
        //                break;
        //            case ItemLocationStyle.RightStyle:
        //                UpDoorLockStateItem = LockStates[1];
        //                DownDoorLockStateItem = LockStates[0];
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }


        //        RaisePropertyChanged(() => LockStates);
        //        RaisePropertyChanged(() => UpDoorLockStateItem);
        //        RaisePropertyChanged(() => DownDoorLockStateItem);
        //    }
        //}
    }
}
