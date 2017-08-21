using System;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{

    internal class CloseCouplerHatchesView1 : CloseCouplerHatchesViewBase, ICouplerHatchesStateItem
    {
        public CloseCouplerHatchesView1(CRH3C380BBase baseClass)
            : base(baseClass)
        {
            var point3 =
                ((TrainGroupView) ConstInfoCollection.Find(f => f is TrainGroupView && (int) f.Tag == 1))
                    .LeftEdgeLocation;
            var point4 =
                ((TrainGroupView) ConstInfoCollection.Find(f => f is TrainGroupView && (int) f.Tag == 1))
                    .RightEdgeLocation;

            var itemSize = new Size(25, 25);
            var inflat = new Size(10, 10);
            CloseCouplerHatchesViewItemCollection.Add(
                new SelectableCloseCouplerHatchesViewItemDecorator(new CouplerHatchesViewItem(CouplerHatchesUnit.All)
                {
                    RefreshAction =
                        o => RefreshItemView((CouplerHatchesViewItem) o),
                    OutLineRectangle =
                        new Rectangle(point3.X - itemSize.Width/2,
                            point3.Y,
                            itemSize.Width,
                            itemSize.Height),
                    Tag =
                        new[]
                        {
                            InBoolKeys.Inb动车组1前车钩状态第0位,
                            InBoolKeys.Inb动车组1前车钩状态第1位,
                            InBoolKeys.Inb动车组1前车钩状态第2位
                        },

                },
                    inflat));
            ConstInfoCollection.Add(new CouplerHatchesViewItem
            {
                OutLineRectangle =
                    new Rectangle(point3.X - itemSize.Width/2, point3.Y, itemSize.Width, itemSize.Height)
            });
            CloseCouplerHatchesViewItemCollection.Add(
                new SelectableCloseCouplerHatchesViewItemDecorator(new CouplerHatchesViewItem(CouplerHatchesUnit.All)
                {
                    RefreshAction =
                        o => RefreshItemView((CouplerHatchesViewItem) o),
                    OutLineRectangle =
                        new Rectangle(point4.X - itemSize.Width/2,
                            point4.Y,
                            itemSize.Width,
                            itemSize.Height),
                    Tag =
                        new[]
                        {
                            InBoolKeys.Inb动车组1后车钩状态第0位,
                            InBoolKeys.Inb动车组1后车钩状态第1位,
                            InBoolKeys.Inb动车组1后车钩状态第2位
                        },

                },
                    inflat));
            ConstInfoCollection.Add(new CouplerHatchesViewItem
            {
                OutLineRectangle =
                    new Rectangle(point4.X - itemSize.Width/2, point4.Y, itemSize.Width, itemSize.Height)
            });
            baseClass.UpdateUiObject(CommunicationDataType.InBool,
                CloseCouplerHatchesViewItemCollection.Select(s => s.CloseCouplerHatchesViewItemDecorater.Tag)
                    .Cast<string[]>()
                    .SelectMany(s => s)
                    .Distinct());
        }


        public CouplerHatchesUnit Unit
        {
            get { throw new NotImplementedException(); }
        }

        public CouplerHatchesState State
        {
            get { throw new NotImplementedException(); }
        }
    }
}

