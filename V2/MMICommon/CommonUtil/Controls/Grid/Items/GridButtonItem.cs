using System;
using System.Drawing;
using CommonUtil.Controls.Button;

namespace CommonUtil.Controls.Grid.Items
{
    /// <summary>
    /// GridButtonItem
    /// </summary>
    public class GridButtonItem : GridItemBaseT<GDIButton>
    {
         private Size m_ContentSize;

        /// <summary>
        /// 文本的实例 
        /// </summary>
        public GDIButton Button
        {
            set { m_InnerContrl = value; }
            get { return m_InnerContrl; }
        }

        /// <summary>
        /// construct
        /// </summary>
        public GridButtonItem()
        {
            ItemType = GridItemType.Text;
            Button = new GDIButton();
            OutLineChanged = OnOutLineChanged;

        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            if (ContentAutoSize)
            {
                Button.OutLineRectangle = new Rectangle(OutLineRectangle.Location, OutLineRectangle.Size);
            }

            var offset = GetInnerCtolLocationOffset();

            Button.Location = new Point((int)(Location.X + offset.X),
                    (int)(Location.Y + offset.Y));
            Button.Size = m_ContentSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool OnMouseDown(Point point)
        {
            return Button.OnMouseDown(point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool OnMouseUp(Point point)
        {
            return Button.OnMouseUp(point);
        }

        /// <summary>
        /// 内部元素的大小 
        /// </summary>
        public override Size ContentSize
        {
            get { return m_ContentSize; }
            set
            {
                m_ContentSize = value;
                var offset = GetInnerCtolLocationOffset();
                Button.Location = new Point((int)(Location.X + offset.X),
                        (int) (Location.Y + offset.Y));
                Button.Size = value;

            }
        }
    }
}
