using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignWhellRModel
    {
        public static readonly DesignWhellRModel Instance = new DesignWhellRModel();

        public ObservableCollection<WhellRItem> Items
        {
            get
            {
                return new ObservableCollection<WhellRItem>(new List<WhellRItem>()
                {
                    new WhellRItem("第1轴轮径", 1200, 1150, 1250) {IsSetting = true},
                    new WhellRItem("第2轴轮径", 1200, 1150, 1250),
                    new WhellRItem("第3轴轮径", 1200, 1150, 1250),
                    new WhellRItem("第4轴轮径", 1200, 1150, 1250),
                    new WhellRItem("第5轴轮径", 1200, 1150, 1250),
                    new WhellRItem("第6轴轮径", 1200, 1150, 1250),
                });
            }
        }
    }
}