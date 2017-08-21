using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;using CommonUtil.Util.Extension;

namespace CRH2MMI.LightTrans.UnitView
{
    [DebuggerDisplay("CarName={CarName} Type={GetType()}")]
    abstract class UnitViewBase : CommonInnerControlBase
    {
        /// <summary>
        /// 列车号, 从0 开始
        /// </summary>
        public int CarNo { set; get; }

        public int CarName{ set { CarNo = value - 1; }get { return CarNo + 1; }}

        // ReSharper disable once InconsistentNaming
        protected List<CommonInnerControlBase> m_ConstInfo;

        // ReSharper disable once InconsistentNaming
        protected List<Line> m_Lines;

        // ReSharper disable once UnassignedReadonlyField.Compiler
        protected readonly Trans Trans;

        /// <summary>
        /// 线的距离
        /// </summary>
        protected const int DefaultLineInterval = 8;


        protected UnitViewBase(Trans trans)
        {
            Trans = trans;
            
            m_ConstInfo = new List<CommonInnerControlBase>();

            m_Lines = new List<Line>();

            OutLineChanged += OnOutLineChanged;

        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var args = (OutLineChangedEventArgs<Rectangle>)eventArgs;
            LogMgr.Debug(string.Format(" Ignore size changed of class {0}. set default size {1}", this.GetType().Name, args.OldRectangle.Size));
            m_OutLineRectangle = new Rectangle(args.NewRectangle.Location, args.OldRectangle.Size);
            //m_OutLineRectangle.Size = 
            m_ConstInfo.ForEach(e => e.Location = LocationChangeMatrix.TransformPoint(e.Location));
            m_Lines.ForEach(e =>
                            {
                                e.StartPoint = LocationChangeMatrix.TransformPoint(e.StartPoint);
                                e.EndPoint = LocationChangeMatrix.TransformPoint(e.EndPoint);
                            });
        }

        public override void OnDraw(Graphics g)
        {
            m_ConstInfo.ForEach(e => e.OnDraw(g));

            m_Lines.ForEach(e => e.OnPaint(g));
        }

        protected void GetLinePen(Line line, List<string> inBoolNames)
        {
            Trans.Resource.GetLinePen(line, inBoolNames);
        }

        /// <summary>
        /// 获得连接线的点
        /// </summary>
        /// <returns></returns>
        public abstract List<Point> GetConnectionPoints();
    }
}
