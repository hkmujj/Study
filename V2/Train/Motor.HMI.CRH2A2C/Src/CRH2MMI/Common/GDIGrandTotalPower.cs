using System.Drawing;
using CommonUtil.Controls;

namespace CRH2MMI.Common
{
    public class GDIGrandTotalPower : CommonInnerControlBase
    {
        /// <summary>
        /// 多少行
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// 多少列
        /// </summary>
        public int ColNum { get; set; }
        /// <summary>
        /// 线条
        /// </summary>
        public Pen LinePen { get; set; }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);
        }
    }
}