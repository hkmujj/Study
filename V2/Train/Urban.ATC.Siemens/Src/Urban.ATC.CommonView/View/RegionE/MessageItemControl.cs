using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Interface;
using Motor.ATP.CommonView.Model;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class MessageItemControl : UserControl
    {
        private IMessageItem m_MessageItem;
        private IMessageItemContentShowStrategy m_ItemContentShowStrategy;
        private Brush m_ContentBrush;
        private Pen m_ConfirmingRectangePen;

        /// <summary>
        /// 内容画刷
        /// </summary>
        public Brush ContentBrush
        {
            set { m_ContentBrush = value; Invalidate(); }
            get { return m_ContentBrush; }
        }

        /// <summary>
        /// 确认框的画刷
        /// </summary>
        public Pen ConfirmingRectangePen
        {
            set
            {
                m_ConfirmingRectangePen = value;
                if (MessageItem != null && MessageItem.Style != MessageStyle.Ordinary)
                {
                    Invalidate();
                }
            }
            get { return m_ConfirmingRectangePen; }
        }

        public IMessageItem MessageItem
        {
            set
            {
                if (MessageItem != null)
                {
                    MessageItem.PropertyChanged -= MessageItemOnPropertyChanged;
                }
                m_MessageItem = value;
                if (MessageItem != null)
                {
                    MessageItem.PropertyChanged += MessageItemOnPropertyChanged;
                }
                Invalidate();
            }
            get { return m_MessageItem; }
        }

        private void MessageItemOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == PropertySupport.ExtractPropertyName<IMessageItem, MessageStyle>(f => f.Style))
            {
                Invalidate();
            }
        }

        public IMessageItemContentShowStrategy ItemContentShowStrategy
        {
            set { m_ItemContentShowStrategy = value; Invalidate(); }
            get { return m_ItemContentShowStrategy; }
        }

        public MessageItemControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            ItemContentShowStrategy = new MessageItemContentShowSingleLineStrategy();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (MessageItem == null)
            {
                base.OnPaint(e);
                return;
            }

            if (ItemContentShowStrategy != null)
            {
                ItemContentShowStrategy.Paint(this, e.Graphics);
            }

            if (ConfirmingRectangePen != null && MessageItem.Style != MessageStyle.Ordinary)
            {
                e.Graphics.DrawRectangle(ConfirmingRectangePen, this.GetActureRectangle());
            }

            base.OnPaint(e);
        }
    }
}
