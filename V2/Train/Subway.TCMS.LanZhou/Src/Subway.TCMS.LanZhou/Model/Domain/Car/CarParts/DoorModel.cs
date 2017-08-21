using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class DoorItem : CarItem<DoorState, DoorConfig>
    {
        public DoorItem(int doorIndex,DoorConfig itemConfig, int carIndex)
            : base(itemConfig, carIndex)
        {
            DoorIndex = doorIndex;
        }

        public int DoorIndex { get; private set; }
    }

    public class DoorModel : CarPartBase
    {
        public DoorModel(ItemLocationStyle itemLocationStyle)
        {
            ItemLocationStyle = itemLocationStyle;
        }

        private ObservableCollection<DoorItem> m_DoorItems;

        public IEnumerable<DoorItem> UpDoors { get; set; }

        public IEnumerable<DoorItem> DownDoors { get; set; }

        public ItemLocationStyle ItemLocationStyle { get; private set; }

        public ObservableCollection<DoorItem> DoorItems
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
                        UpDoors = value.Where(w => w.DoorIndex % 2 != 0); 
                        DownDoors = value.Where(w => w.DoorIndex % 2 == 0);
                        break;
                    case ItemLocationStyle.RightStyle:
                        UpDoors = value.Where(w => w.DoorIndex % 2 == 0).Reverse();
                        DownDoors = value.Where(w => w.DoorIndex % 2 != 0).Reverse();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                RaisePropertyChanged(() => DoorItems);
                RaisePropertyChanged(() => UpDoors);
                RaisePropertyChanged(() => DownDoors);
            }
        }
    }
}