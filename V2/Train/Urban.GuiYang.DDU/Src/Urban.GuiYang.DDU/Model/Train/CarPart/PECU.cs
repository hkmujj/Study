using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{

    public class PECUItem : CarItem<PECUState, CarPECUConfig>
    {
        
        public PECUItem(int carIndex, CarPECUConfig indexString) : base(carIndex, indexString)
        {
        }

        
    }

    public class PECU : CarPartBase
    {
        private ObservableCollection<PECUItem> m_PECUItems;

        public PECU(ItemLocationStyle itemLocationStyle)
        {
            ItemLocationStyle = itemLocationStyle;
        }


        public ItemLocationStyle ItemLocationStyle { get; private set; }

        public ObservableCollection<PECUItem> PECUItems
        {
            get { return m_PECUItems; }
            set
            {
                if (Equals(value, m_PECUItems))
                {
                    return;
                }

                m_PECUItems = value;
                switch (ItemLocationStyle)
                {
                    case ItemLocationStyle.LeftStyle:
                        UpItems = value.Where(w => w.CarIndex % 2 == 0);
                        DownItems = value.Where(w => w.CarIndex % 2 != 0);
                        break;
                    case ItemLocationStyle.RightStyle:
                        UpItems = value.Where(w => w.CarIndex % 2 == 0).Reverse();
                        DownItems = value.Where(w => w.CarIndex % 2 != 0).Reverse();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                RaisePropertyChanged(() => PECUItems);
                RaisePropertyChanged(() => UpItems);
                RaisePropertyChanged(() => DownItems);
            }
        }

        public IEnumerable<PECUItem> UpItems { get; private set; }
        public IEnumerable<PECUItem> DownItems { get; private set; }
    }
}