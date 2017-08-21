using System;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignPortData : NotificationObject
    {
        public static readonly DesignPortData Instance = new DesignPortData();
        private PortDataStyle m_DataStyle;

        private DesignPortData()
        {
            var rd = new Random();
            PortDataItems = new Lazy<ReadOnlyCollection<PortDataItem>>(() => Enumerable.Range(7, 13).Select(s => new PortDataItem(s)
            {
                D0 = rd.Next(0, 255),
                D1 = rd.Next(0, 255),
                D2 = rd.Next(0, 255),
                D3 = rd.Next(0, 255),
                D4 = rd.Next(0, 255),
                D5 = rd.Next(0, 255),
                D6 = rd.Next(0, 255),
                D7 = rd.Next(0, 255),
                D8 = rd.Next(0, 255),
                D9 = rd.Next(0, 255),
                D10 = rd.Next(0, 255),
                D11 = rd.Next(0, 255),
                D12 = rd.Next(0, 255),
                D13 = rd.Next(0, 255),
                D14 = rd.Next(0, 255),
                D15 = rd.Next(0, 255),
            }).ToList().AsReadOnly());
        }

        public Lazy<ReadOnlyCollection<PortDataItem>> PortDataItems { get; private set; }

        public PortDataStyle DataStyle
        {
            set
            {
                if (value == m_DataStyle)
                {
                    return;
                }

                m_DataStyle = value;
                RaisePropertyChanged(() => DataStyle);
            }
            get { return m_DataStyle; }
        }
    }
}