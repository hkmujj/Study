using System;
using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.Menu
{
    internal abstract class MenuBase : IInnerControl
    {

        // ReSharper disable once InconsistentNaming
        protected CRH2menu m_CRH2Menu;
        private Image m_BkImage;

        protected MenuBase(CRH2menu crh2Menu)
        {
            m_CRH2Menu = crh2Menu;
        }

        public void Refresh()
        {
            
        }

        public virtual void SwitchFromOthers()
        {
            
        }

        public virtual void Init()
        {
            m_BkImage = MenuResource.Instance.GetBkImage(this);
        }

        public virtual bool OnMouseDown(Point point)
        {
            return false;
        }

        public virtual bool OnMouseUp(Point point)
        {
            return false;
        }

        public virtual void OnDraw(Graphics g)
        {
            g.DrawImage(m_BkImage, 0, 0, m_BkImage.Width, m_BkImage.Height);
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

        /// <summary>
        /// 获取或设置包含有关控件的数据的对象。
        /// </summary>
        public object Tag { get; set; }
    }
}
