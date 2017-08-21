using System;
using System.Collections.Generic;
using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Models;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.CarInfo
{
    internal abstract class CarInfoPageBase : IInnerControl
    {

        // ReSharper disable once InconsistentNaming
        protected readonly CarInfo m_CarInfo;

        /// <summary>
        /// 切换界面的事件
        /// </summary>
        public EventHandler ChangePageEvent;

        protected StringFormat InnerTextFormat
        {
            set { m_GridLineInitialize.InnerTextFormat = value; }
            get
            {
                return m_GridLineInitialize.InnerTextFormat;
            }
        }

        private readonly GridLineInitialize m_GridLineInitialize;

        protected CarInfoPageBase(CarInfo carInfo)
        {
            m_CarInfo = carInfo;
            m_GridLineInitialize = new GridLineInitialize() { ViewClass = m_CarInfo };
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public virtual void Init()
        {
        }

        public abstract void OnDraw(Graphics g);
        public void OnPaint(Graphics g)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="point"/>
        public abstract bool OnMouseDown(Point point);


        public bool OnMouseUp(Point point)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }

        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。
        /// </summary>
        public object Tag { get; set; }

        protected List<GDIRectText> CreateTitles(GridLine gl, GridConfig glConfig, int textWidth = 90)
        {
            return m_GridLineInitialize.CreateTitles(gl, glConfig, textWidth);
        }

        protected void InitInnerContrl(GridLine gl,GridColumnConfig columnConfig,GridItemBase item)
        {
            m_GridLineInitialize.InitInnerContrl(gl, columnConfig, item);
        }
    }
}
