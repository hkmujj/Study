using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using MMI.Facility.Interface;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    internal abstract class LightViewBase : CommonInnerControlBase, ILightingView
    {
        protected List<CommonInnerControlBase> ConstInfoCollection;

        protected List<SelectableLightViewItemDecorator> LightViewItemCollection;

        protected ISelectStrategy SelectStrategy;

        public event Action<ILightStateItem> LightingStateChanged;

        public event Action<ILightingView> CurrentSelectedLightingUnitChanged;

        protected baseClass BaseClass;

        public ILightStateItem CurrentSelectedLightingUnit { get; private set; }

        public ReadOnlyCollection<ILightStateItem> AllLightStateItemCollection
        {
            get { return LightViewItemCollection.Cast<ILightStateItem>().ToList().AsReadOnly(); }
        }

        protected LightViewBase(baseClass baseClass)
        {
            BaseClass = baseClass;

            LightViewItemCollection = new List<SelectableLightViewItemDecorator>();

            const int x = 570;
            const int y = 40;

            var regionSize = new Size(207, 100);

            var itemSize = new Size(20, 20);

            var txtSize = new Size(100, 20);

            var interval = 10;

            var regionTextInterval = 20;

            ConstInfoCollection = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    OutLinePen = Pens.White,
                    OutLineRectangle = new Rectangle(x, y, regionSize.Width, regionSize.Height),
                    NeedDarwOutline = true
                }
            };
            for (int i = 0; i < 3; i++)
            {
                var location = new Point(x + interval, y + interval*(i + 1) + itemSize.Height*i);

                ConstInfoCollection.Add(new LightViewItem
                {
                    OutLineRectangle = new Rectangle(location, itemSize),
                    State = (LightState) i
                });

                location.Offset(itemSize.Width + regionTextInterval, 0);

                ConstInfoCollection.Add(new GDIRectText
                {
                    Text = EnumUtil.GetDescription((LightState) i).FirstOrDefault(),
                    TextColor = Color.White,
                    OutLineRectangle = new Rectangle(location, txtSize)
                });
            }

            InitalizeGrid(y + regionSize.Height);

        }


        private void InitalizeGrid(int y)
        {
            var x = 40;
            var length0 = 260;
            var length = 433;
            var y1 = y + 120;
            var y2 = y1 + 100;
            var x1 = 185;
            var line1 = new Line(new Point(x, y), new Point(x + length0, y)) {Color = Color.White, Tag = 1};
            var line2 = new Line(new Point(x, y1), new Point(x + length, y1)) {Color = Color.White, Tag = 2};
            var line3 = new Line(new Point(x, y2), new Point(x + length, y2)) {Color = Color.White, Tag = 3};
            var line4 = new Line(new Point(x1, y - 20), new Point(x1, y2 + 100)) {Color = Color.White, Tag = 4};
            var line5 = new Line(new Point(line2.EndPoint.X, line2.EndPoint.Y - 20),
                new Point(line2.EndPoint.X, line4.EndPoint.Y)) {Color = Color.White, Tag = 5};
            var line6 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + 290, line2.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 6
            };
            var line7 = new Line(line3.EndPoint, new Point(line3.EndPoint.X + 290, line3.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 7
            };

            ConstInfoCollection.Add(line1);
            ConstInfoCollection.Add(line2);
            ConstInfoCollection.Add(line3);
            ConstInfoCollection.Add(line4);
            ConstInfoCollection.Add(line5);
            ConstInfoCollection.Add(line6);
            ConstInfoCollection.Add(line7);


        }

        public void ChangeLightingState(LightingUnit unit, LightState state)
        {

        }

        public void Select(Direction direction)
        {
            SelectStrategy.Select(direction);
            if (CurrentSelectedLightingUnit != SelectStrategy.CurrentSelected)
            {
                CurrentSelectedLightingUnit = (SelectableLightViewItemDecorator) SelectStrategy.CurrentSelected;
                OnCurrentSelectedLightingUnitChanged();
            }
        }


        public override void OnDraw(Graphics g)
        {
            ConstInfoCollection.ForEach(e => e.OnPaint(g));

            LightViewItemCollection.ForEach(e => e.OnPaint(g));
        }

        protected virtual void OnCurrentSelectedLightingUnitChanged()
        {
            var handler = CurrentSelectedLightingUnitChanged;
            if (handler != null)
            {
                handler(this);
            }
        }

        protected virtual void OnLightingStateChanged(ILightStateItem obj)
        {
            var handler = LightingStateChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected void RefreshItemView(LightViewItem item)
        {
            var indexName = item.Tag as string[];

            if (indexName == null || indexName.Length < 2)
            {
                AppLog.Debug("Can not RefreshItemView, the indexName.Legth must >= 2");
                return;
            }

            item.State = LightState.Light0;

            if (BaseClass.GetInBoolValue(indexName[1]))
            {
                item.State = LightState.Light1;
            }
            else if (BaseClass.GetInBoolValue(indexName[0]))
            {
                item.State = LightState.Light1P3;
            }
        }
    }
}