using System.Drawing;
using CommonUtil.Controls;

namespace Motor.ATP._300T.共用
{
    public sealed class ButtonBar : CommonInnerControlBase
    {

        private bool m_BtnPress;

        /// <summary>
        /// 按钮按下
        /// </summary>
        public bool BtnPress
        {
            get { return m_BtnPress; }
            set
            {
                m_BtnPress = !IsEnable && value;
            }
        }

        public string Content { get; set; }

        /// <summary>
        /// 按钮没有锁定时底色
        /// </summary>
        public SolidBrush EnableBkBrush { get; set; }

        //按钮被锁定时底色，默认值SolidBrushsItems.DarkGrayBrush
        public SolidBrush UnEnableBkBrush { get; set; }

        //按钮内容范围框
        private readonly RectangleF m_BtnRectF;

        /// <summary>
        /// 是否需要画边线框
        /// </summary>
        public bool IsOutlineVisible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btnRectF">按钮大小</param>
        /// <param name="btnContentStr">按钮显示文字</param>
        /// <param name="outlineVisible">是否需要画边线框</param>
        public ButtonBar(RectangleF btnRectF, string btnContentStr, bool outlineVisible)
        {
            IsEnable = true;

            UnEnableBkBrush = SolidBrushsItems.DarkGrayBrush;

            EnableBkBrush = SolidBrushsItems.BtnAbledBrush;

            m_BtnRectF = btnRectF;

            Content = string.IsNullOrEmpty(btnContentStr) ? "" : btnContentStr;

            IsOutlineVisible = outlineVisible;
        }

        public override void OnDraw(Graphics g)
        {
            DrawBtn(g);
        }

        /// <summary>
        /// 绘制按钮
        /// </summary>
        /// <param name="gs"></param>
        private void DrawBtn(Graphics gs)
        {


            gs.FillRectangle(IsEnable && !string.IsNullOrEmpty(Content)
                ? EnableBkBrush
                : UnEnableBkBrush, m_BtnRectF);

            {
                gs.DrawString(Content, FontsItems.Font16YouB,
                    SolidBrushsItems.WhiteBrush, m_BtnRectF,
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            if (IsOutlineVisible)
            {
                gs.DrawRectangle(PenItems.WhitePen2, Rectangle.Round(m_BtnRectF));
            }
        }
    }
}