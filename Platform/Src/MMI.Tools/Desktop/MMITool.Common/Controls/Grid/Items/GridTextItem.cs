using System;
using System.Drawing;

namespace MMITool.Common.Controls.Grid.Items
{
    /// <summary>
    /// 文本
    /// </summary>
    public class GridTextItem : GridItemBaseT<GDIRectText>
    {
        private Size m_ContentSize;

        /// <summary>
        /// 文本的实例 
        /// </summary>
        public GDIRectText RectText
        {
            private set { m_InnerContrl = value; }
            get { return m_InnerContrl; }
        }

        /// <summary>
        /// construct
        /// </summary>
        public GridTextItem()
        {
            ItemType = GridItemType.Text;
            RectText = new GDIRectText() { NeedDarwOutline = true };
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            if (ContentAutoSize)
            {
                RectText.OutLineRectangle = new Rectangle(OutLineRectangle.Location, OutLineRectangle.Size);
            }

            var offset = GetInnerCtolLocationOffset();

            RectText.Location = new Point((int)(Location.X + offset.X),
                    (int)(Location.Y + offset.Y));
            RectText.Size = m_ContentSize;
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
                RectText.Location = new Point((int) (Location.X + offset.X),
                        (int) (Location.Y + offset.Y));
                RectText.Size = value;

            }
        }
    }

}
