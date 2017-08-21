using System;
using System.Drawing;
using CommonUtil.Util;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;


namespace CRH2MMI.Common.View
{
    public class SetButton : CommonInnerControlBase
    {
        /// <summary>
        /// 设置
        /// </summary>
        private readonly CRH2Button m_SetBtn;

        /// <summary>
        /// 设置按键被点击的事件
        /// </summary>
        public EventHandler SetBtnClick;

        /// <summary>
        /// 重设响应事件
        /// </summary>
        /// <param name="setBtnClick"></param>
        public void ResetClickEvent(EventHandler setBtnClick)
        {
            SetBtnClick = setBtnClick;
        }

        public static readonly Rectangle DefaultOutlineRectangle = new Rectangle(630, 526, 150, 45);


        public SetButton()
        {
            //设定
            m_SetBtn = new CRH2Button
            {
                OutLineRectangle = new Rectangle(DefaultOutlineRectangle.Location, DefaultOutlineRectangle.Size),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = OnSetButtonDown,
                TextColor = Color.White,
                Text = "设   定",
            };

            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var mat = LocationChangeMatrix.Clone();
            mat.Multiply(SizeChangeMatrix);
            m_SetBtn.OutLineRectangle = new Rectangle(mat.TransformPoint(m_SetBtn.Location), new Size(SizeChangeMatrix.TransformPoint(m_SetBtn.Size.Width, m_SetBtn.Size.Height)));
        }


        private void OnSetButtonDown(object sender, EventArgs e)
        {
            HandleUtil.OnHandle(SetBtnClick, this, null);
        }

        public override void OnDraw(Graphics g)
        {
            m_SetBtn.OnDraw(g);
        }


        public override bool OnMouseDown(Point point)
        {
            return m_SetBtn.OnMouseDown(point);
        }
    }
}
