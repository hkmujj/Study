using System;
using System.Drawing;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Roundness;
using CommonUtil.Util.Extension;

namespace MMICommon.Controls.Grid
{
    /// <summary>
    /// 
    /// </summary>
    public class GridRoundnessItem : GridItemBaseT<GDIRoundness>
    {
        private Size m_ContentSize;

        /// <summary>
        /// 
        /// </summary>
        public GDIRoundness Roundness
        {
            private set { m_InnerContrl = value; }
            get { return m_InnerContrl; }
        }


        /// <summary>
        /// 
        /// </summary>
        public GridRoundnessItem()
        {
            ItemType = GridItemType.Roudness;
            Roundness = new GDIRoundness();
            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            if (ContentAutoSize)
            {
                var min = Math.Min(Size.Width, Size.Height);
                Roundness.R = min / 2;
            }
            Roundness.Center = OutLineRectangle.GetCenterPoint();

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
                Roundness.Center = OutLineRectangle.GetCenterPoint();
                var min = Math.Min(value.Width, value.Height);
                Roundness.R = min / 2;
            }
        }
    }
}
