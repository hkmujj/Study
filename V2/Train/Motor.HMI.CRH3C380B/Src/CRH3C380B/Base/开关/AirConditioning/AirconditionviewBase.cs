using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal abstract class AirconditionviewBase : CommonInnerControlBase, IAirconditionView
    {
        protected List<CommonInnerControlBase> ConstInfoCollection;

        protected List<SelectableAirconditionViewItemDecorator> AirconditionViewItemCollection;

        protected ISelectStrategy SelectStrategy;

        public event Action<IAirconditionStateItem> AirconditionStateChanged;

        public event Action<IAirconditionView> CurrentSelectedAirconditionUnitChanged;

        protected CRH3C380BBase BaseClass;

        protected AirconditionviewBase(CRH3C380BBase baseClass)
        {
            BaseClass = baseClass;

            AirconditionViewItemCollection = new List<SelectableAirconditionViewItemDecorator>();

            const int x = 570;
            const int y = 40;

            var regionSize = new Size(207, 130);

            var itemSize = new Size(20, 20);

            var txtSize = new Size(130, 20);

            const int interval = 10;

            const int regionTextInterval = 20;
            ConstInfoCollection = new List<CommonInnerControlBase>
            {
                new GDIRectText
                {
                    OutLinePen = Pens.White,
                    OutLineRectangle = new Rectangle(x, y, regionSize.Width, regionSize.Height),
                    NeedDarwOutline = true
                }
            };
            for (int i = 0; i < 4; i++)
            {
                var location = new Point(x + interval, y + interval*(i + 1) + itemSize.Height*i);

                ConstInfoCollection.Add(new AirconditionViewItem
                {
                    OutLineRectangle = new Rectangle(location, itemSize),
                    State = (AirConditionState) i
                });

                location.Offset(itemSize.Width + regionTextInterval, 0);

                ConstInfoCollection.Add(new GDIRectText
                {
                    Text = EnumUtil.GetDescription((AirConditionState) i).FirstOrDefault(),
                    TextColor = Color.White,
                    OutLineRectangle = new Rectangle(location, txtSize)
                });

            }

            InitalizeGrid(y + 80);

        }

        public IAirconditionStateItem CurrentSelectedAirconditionUnit { get; private set; }


        private void InitalizeGrid(int y)
        {
            const int x = 30;
            const int length0 = 260;
            const int length = 416;
            const int length2 = 293;
            const int x1 = 155;
            var y1 = y + 100;
            var y2 = y1 + 60;
            var y3 = y2 + 60;

            var line1 = new Line(new Point(x, y), new Point(x + length0, y)) {Color = Color.White, Tag = 1};
            var line2 = new Line(new Point(x, y1), new Point(x + length, y1)) {Color = Color.White, Tag = 2};
            var line3 = new Line(new Point(x, y2), new Point(x + length, y2)) {Color = Color.White, Tag = 3};
            var line4 = new Line(new Point(x, y3), new Point(x + length, y3)) {Color = Color.White, Tag = 4};
            var line5 = new Line(new Point(x, y3 + 6), new Point(x + length, y3 + 6)) {Color = Color.White, Tag = 5};
            var line6 = new Line(new Point(x1, y - 20), new Point(x1, y2 + 200)) {Color = Color.White, Tag = 6};

            var line7 = new Line(line2.EndPoint, new Point(line2.EndPoint.X + length2, line2.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 7
            };
            var line8 = new Line(line3.EndPoint, new Point(line3.EndPoint.X + length2, line3.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 8
            };
            var line9 = new Line(line4.EndPoint, new Point(line4.EndPoint.X + length2, line4.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 9
            };
            var line10 = new Line(line5.EndPoint, new Point(line5.EndPoint.X + length2, line5.EndPoint.Y))
            {
                Color = Color.White,
                Tag = 10
            };

            var line11 = new Line(new Point(line2.EndPoint.X, line2.EndPoint.Y - 20),
                new Point(line2.EndPoint.X, line6.EndPoint.Y)) {Color = Color.White, Tag = 11};


            ConstInfoCollection.Add(line1);
            ConstInfoCollection.Add(line2);
            ConstInfoCollection.Add(line3);
            ConstInfoCollection.Add(line4);
            ConstInfoCollection.Add(line5);
            ConstInfoCollection.Add(line6);
            ConstInfoCollection.Add(line7);
            ConstInfoCollection.Add(line8);
            ConstInfoCollection.Add(line9);
            ConstInfoCollection.Add(line10);
            ConstInfoCollection.Add(line11);

        }

        public void ChangeAirconditionState(AirconditionLevelUnit unit, AirConditionState state)
        {

        }

        public void Select(Direction direction)
        {
            SelectStrategy.Select(direction);
            if (CurrentSelectedAirconditionUnit != SelectStrategy.CurrentSelected)
            {
                CurrentSelectedAirconditionUnit =
                    (SelectableAirconditionViewItemDecorator) SelectStrategy.CurrentSelected;
                OnCurrentSelectedLightingUnitChanged();
            }
        }

        public override void OnDraw(Graphics g)
        {
            ConstInfoCollection.ForEach(e => e.OnDraw(g));

            AirconditionViewItemCollection.ForEach(e => e.OnPaint(g));
        }

        protected virtual void OnCurrentSelectedLightingUnitChanged()
        {
            var handler = CurrentSelectedAirconditionUnitChanged;
            if (handler != null)
            {
                handler(this);
            }
        }

        protected virtual void OnLightingStateChanged(IAirconditionStateItem obj)
        {
            var handler = AirconditionStateChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

        #region Delete

        //private void DrawEmergaceClose(AirConditionState state)
        //{
        //    if (IsEmergaceClose || IsEmpty)
        //    {
        //        //获取第二条Line
        //        var line2 = (Line)m_ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 2);
        //        //获取第三条Line
        //        var line3 = (Line)m_ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 3);
        //        var line6 = (Line)m_ConstInfoCollection.Find(f => f is Line && (int)f.Tag == 6);
        //        var size1 = new Size(20, 20);
        //        var inflat = new Size(10, 10);
        //        var crose2 = new Point(line6.StartPoint.X, line2.StartPoint.Y);
        //        var crose3 = new Point(line6.StartPoint.X, line3.StartPoint.Y);
        //        crose3.Offset(inflat.Width, inflat.Height);
        //        crose2.Offset(inflat.Width, inflat.Height);
        //        m_ConstInfoCollection.Add(new AirconditionViewItem { OutLineRectangle = new Rectangle(crose2, size1), State = state });

        //        for (int i = 0; i < 8; i++)
        //        {
        //            m_ConstInfoCollection.Add(new AirconditionViewItem { OutLineRectangle = new Rectangle(crose3, size1), State = state });
        //            crose3.Offset(size1.Width + inflat.Width * 2, 0);
        //        }
        //    }
        //}

        #endregion

        protected void RefreshItemView(AirconditionViewItem item)
        {
            var indexName = item.Tag as string[];
            if (indexName == null || indexName.Length < 1)
            {
                AppLog.Debug("Can not RefreshItemView, the indexName.Legth must >= 1");
                return;
            }

            item.State = AirConditionState.Default;
            if (indexName.Length == 1)
            {
                if (BaseClass.GetInBoolValue(indexName[0]))
                {
                    item.State = AirConditionState.EmergaceClose;
                }
            }
            else if (indexName.Length == 3 && indexName[0].Equals("空调紧急关"))
            {
                DrawAiecondition(item, indexName);
            }
            else if (indexName.Length == 3)
            {
                DrawAirconditionTemperature(item, indexName);
            }
        }

        /// <summary>
        /// 单车空调和本动车组空调相关逻辑
        /// </summary>
        /// <param name="item">AirconditionViewItem类</param>
        /// <param name="indexName">AirconditionViewItem的tag值</param>
        private void DrawAiecondition(AirconditionViewItem item, string[] indexName)
        {
            if (BaseClass.GetInBoolValue(indexName[2]))
            {
                item.State = AirConditionState.Open;
            }
            else if (BaseClass.GetInBoolValue(indexName[1]))
            {
                item.State = AirConditionState.Open;
            }
            else if (BaseClass.GetInBoolValue(indexName[0]))
            {
                item.State = AirConditionState.EmergaceClose;
            }
        }

        /// <summary>
        /// 温度相关逻辑
        /// </summary>
        /// <param name="item">AirconditionViewItem类</param>
        /// <param name="indexName">AirconditionViewItem的tag值</param>
        private void DrawAirconditionTemperature(AirconditionViewItem item, string[] indexName)
        {
            var location = BaseClass.DMITitle.MarshallMode ? new Point(454, 380) : new Point(165, 380);
            var itemSize = new Size(20, 20);
            bool[] blList = new bool[3];
            blList[0] = BaseClass.GetInBoolValue(indexName[0]);
            blList[1] = BaseClass.GetInBoolValue(indexName[1]);
            blList[2] = BaseClass.GetInBoolValue(indexName[2]);
            BitArray br = new BitArray(blList);
            int[] intValue = new int[1];
            br.CopyTo(intValue, 0);
            GetLocation(indexName, ref location);
            if (intValue[0] == 0)
            {
                item.OutLineRectangle = new Rectangle(0, 0, 0, 0);
            }
            else
            {
                item.OutLineRectangle = new Rectangle(location, itemSize);
                for (int i = 1; i < intValue[0]; i++)
                {
                    item.OutLineRectangle = new Rectangle(item.OutLineRectangle.X, item.OutLineRectangle.Y + 10,
                        item.OutLineRectangle.Width, item.OutLineRectangle.Height);
                }

                item.State = AirConditionState.SetTemperature;
            }
        }

        /// <summary>
        /// 温度相关图像的位移
        /// </summary>
        /// <param name="indexName">AirconditionViewItem的tag值</param>
        /// <param name="location">图像的起始坐标点</param>
        private static void GetLocation(IList<string> indexName, ref Point location)
        {
            for (int i = 2; i <= 8; i++)
            {
                if (!indexName[0].Equals(string.Format("空调-温度--1{0}--0", i)))
                {
                    continue;
                }

                location.Offset(36*(i - 1), 0);
                return;
            }
            for (int i = 2; i <= 8; i++)
            {
                if (!indexName[0].Equals(string.Format("空调-温度--2{0}--0", i)))
                {
                    continue;
                }

                location.Offset(36*(i - 1), 0);
                return;
            }
        }

        public ReadOnlyCollection<IAirconditionStateItem> AllAirconditionStateItemCollections
        {
            get { return AirconditionViewItemCollection.Cast<IAirconditionStateItem>().ToList().AsReadOnly(); }
        }
    }
}