using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class MessageListControl : UserControl
    {
        private readonly List<MessageItemControl> m_MessageItemControlBuffer;

        private static readonly Action<MessageItemControl, int, int, IMessageItem> UpdateMessageItemAction =
            UpdateMessageItem;

        public ReadOnlyCollection<MessageItemControl> MessageItemControlBuffer
        {
            get { return m_MessageItemControlBuffer.AsReadOnly(); }
        }

        private readonly List<MessageItemControl> m_UsingMessageItemControls;
        private int m_MaxMessageItemCount;
        private IMessage m_Message;

        private int m_ItemHeight;
        private IATP m_ATP;
        private bool m_ItemsVisible;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pen BackLinePen { set; get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int MaxMessageItemCount
        {
            set
            {
                Contract.Requires(value > 0);

                m_MaxMessageItemCount = value;
                InitalizeMessageItems();
            }
            get { return m_MaxMessageItemCount; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IATP ATP
        {
            set
            {
                m_ATP = value;
                Message = m_ATP.Message;
                Invalidate();
            }
            get { return m_ATP; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        private IMessage Message
        {
            set
            {
                if (m_Message != value)
                {
                    if (m_Message != null)
                    {
                        m_Message.MessageCollection.CollectionChanged -= MessageCollectionOnCollectionChanged;
                        m_Message.PropertyChanged -= MessageOnPropertyChanged;
                    }
                    m_Message = value;
                    m_UsingMessageItemControls.ForEach(e => e.InvokeUpdateVisibleIfNeed(false));
                    m_UsingMessageItemControls.Clear();
                    if (m_Message != null)
                    {
                        m_Message.MessageCollection.CollectionChanged += MessageCollectionOnCollectionChanged;
                        m_Message.PropertyChanged += MessageOnPropertyChanged;
                    }
                    UpdateMessageItems();
                    Invalidate();
                }
            }
            get { return m_Message; }
        }

        public bool ItemsVisible
        {
            set
            {
                m_ItemsVisible = value;
                m_MessageItemControlBuffer.ForEach(e => e.Visible = ItemsVisible);
                Invalidate();
            }
            get { return m_ItemsVisible; }
        }

        public MessageListControl()
        {
            m_MessageItemControlBuffer = new List<MessageItemControl>(MaxMessageItemCount);
            m_UsingMessageItemControls = new List<MessageItemControl>(MaxMessageItemCount);
            InitializeComponent();
            MaxMessageItemCount = 5;
            SizeChanged += OnSizeChanged;
            BackColorChanged += (sender, args) => m_MessageItemControlBuffer.ForEach(e => e.BackColor = BackColor);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// 正在显示的消息个数
        /// </summary>
        public int GetShowingMessageCount()
        {
            if (m_UsingMessageItemControls.Sum(s => s.ItemContentShowStrategy.LineCount) <= MaxMessageItemCount)
            {
                return m_UsingMessageItemControls.Count;
            }
            var count = 0;
            var sum = 0;
            foreach (var itemControl in m_UsingMessageItemControls)
            {
                sum += itemControl.ItemContentShowStrategy.LineCount;
                if (sum <= MaxMessageItemCount)
                {
                    ++count;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            m_ItemHeight = Height / MaxMessageItemCount;
            if (m_MessageItemControlBuffer != null)
            {
                m_MessageItemControlBuffer.ForEach(e => e.Size = new Size(Width, m_ItemHeight));
            }
            UpdateMessageItemsRegion();
        }

        private void UpdateMessageItemsRegion()
        {
            SuspendLayout();
            var top = 0;
            foreach (var itemControl in m_UsingMessageItemControls)
            {
                if (top >= Height)
                {
                    break;
                }
                itemControl.Height = m_ItemHeight;
                itemControl.Top = top;
                top += itemControl.ItemContentShowStrategy.LineCount * m_ItemHeight;
            }
            ResumeLayout();
        }

        private void InitalizeMessageItems()
        {
            Contract.Requires(m_MessageItemControlBuffer != null);
            Contract.Requires(MaxMessageItemCount > 0);

            m_ItemHeight = Height / MaxMessageItemCount;

            m_MessageItemControlBuffer.ForEach(e =>
            {
                Controls.Remove(e);
                e.Dispose();
            });
            m_MessageItemControlBuffer.Clear();
            m_MessageItemControlBuffer.AddRange(
                Enumerable.Range(0, MaxMessageItemCount).Select(s => new MessageItemControl()
                {
                    Visible = false,
                    Size = new Size(Width, m_ItemHeight),
                    BackColor = BackColor,
                    Padding = new Padding(0, 1, 1, 1),
                    Margin = new Padding(0),
                }));

            m_MessageItemControlBuffer.ForEach(e => Controls.Add(e));
        }

        private void MessageOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateMessageItems();
        }

        private void MessageCollectionOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            UpdateMessageItems();
        }

        private void UpdateMessageItems()
        {
            m_UsingMessageItemControls.ForEach(e =>
            {
                e.InvokeUpdateVisibleIfNeed(false);
                e.MessageItem = null;
                e.ClearOpuaqueLayer(ATP.Other);
            });
            m_UsingMessageItemControls.Clear();

            if (Message == null || Message.MessageCollection == null)
            {
                return;
            }

            var top = 0;
            foreach (var item in Message.MessageCollection.Reverse().Skip(Message.CurrentFirstIndex).Take(MaxMessageItemCount))
            {
                if (top >= Height)
                {
                    break;
                }
                var control = m_MessageItemControlBuffer.First(f => f.MessageItem == null);
                
                control.InvokeIfNeed(UpdateMessageItemAction, control, top, m_ItemHeight, item);

                top += control.ItemContentShowStrategy.LineCount * m_ItemHeight;
                control.AddOpuaqueLayer(ATP.Other);
                m_UsingMessageItemControls.Add(control);
            }

            m_Message.ShowingCount = GetShowingMessageCount();
        }

        private static void UpdateMessageItem(MessageItemControl control, int top, int height,IMessageItem item)
        {
            control.Top = top;
            control.Height = height;
            control.Visible = true;
            control.MessageItem = item;
            control.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
            if (BackLinePen != null)
            {
                var top = 0;
                for (int i = 0; i < MaxMessageItemCount - 1; i++)
                {
                    e.Graphics.DrawRectangle(BackLinePen, 0, top, Width, m_ItemHeight);
                    top += m_ItemHeight;
                }
                // 消除误差
                e.Graphics.DrawRectangle(BackLinePen, 0, 0, Width, Height);
            }
        }
    }
}
