using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Other.Detail;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignAccumulativeData
    {
        public static readonly DesignAccumulativeData Instance = new DesignAccumulativeData();

        public DesignAccumulativeData()
        {
            Items = new ReadOnlyCollection<AccumulativeDataItem>(new List<AccumulativeDataItem>()
            {
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent1 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent2 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent3 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent4 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent5 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent6 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent7 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent8 ,"}),
                new AccumulativeDataItem(new AccumulativeBrakeTimeConfig() {ShowContent = "ShowContent9 ,"}),
            });
        }

        public ReadOnlyCollection<AccumulativeDataItem> Items { private set; get; }
    }
}