using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Traction
{
    /// <summary>
    /// 牵引设备, LCM ,ACM , MCM 的基类
    /// </summary>
    public abstract class TractionEquipMemtBase : IInnerControl
    {

        //画笔
        protected Pen GreenPen = new Pen(Color.FromArgb(0, 128, 0), 2);
        protected Pen GrayPen = new Pen(Color.FromArgb(135, 135, 135), 2);
        protected Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);//红色画笔
        protected Pen BluePen = new Pen(Color.FromArgb(0, 255, 255), 2);//蓝色画笔

        //画刷
        protected SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        protected SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
        protected SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        protected SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));
        protected SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(225, 225, 225));

        /// <summary>
        /// 选中的
        /// </summary>
        protected Pen SelectedPend = new Pen(Color.Black, 2);

        protected Font Strfont = new Font("Arial", 11);

        private readonly Rectangle m_OutLineRectangle;
        /// <summary>
        /// 轮廓的大小
        /// </summary>
        public Rectangle OutLineOutLineRectangle { get { return m_OutLineRectangle; }}

        /// <summary>
        /// 火车号
        /// </summary>
        public int TrainNumber { set; get; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected { set; get; }

        public EquipType Type { protected set; get; }

        /// <summary>
        /// 设备状态 
        /// </summary>
        public EquipStatus Status { set; get; }

        protected TractionEquipMemtBase(Rectangle rect)
        {
            m_OutLineRectangle = rect;
            IsSelected = false;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        public virtual bool OnMouseDown(Point point)
        {
            if (OutLineOutLineRectangle.Contains(point))
            {
                IsSelected = true;
                return true;
            }
            return false;
        }

        public virtual bool OnMouseUp(Point point)
        {
            throw new NotImplementedException();
        }

        public virtual void OnDraw(Graphics g)
        {
            if (IsSelected)
            {
                g.DrawRectangle(SelectedPend, OutLineOutLineRectangle);
            }
        }

        public void OnPaint(Graphics g)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }
        public object Tag { get; set; }
    }

    /// <summary>
    /// 设备状态 
    /// </summary>
    public enum EquipStatus
    {
        Active,
        Well,
        Fault,
        CutOut,
        Unknow
    };

    public enum EquipType
    {
        ACM = 0,
        Lcm,
        Mcm1,
        Mcm2,
    }
}
