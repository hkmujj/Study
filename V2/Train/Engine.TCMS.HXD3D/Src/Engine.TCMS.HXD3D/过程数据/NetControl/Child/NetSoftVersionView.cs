using System.Drawing;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.Resource;

namespace Engine.TCMS.HXD3D.��������.NetControl.Child
{
    public class NetSoftVersionView : NetControlChildView
    {
        private RectangleImage m_Content;

        private HXD3DBlueButton m_ReturnBtn;

        public NetSoftVersionView(ProcessNetControl parent) : base(parent, NetControlChildType.SoftVersion)
        {
        }

        /// <summary>��ʼ��</summary>
        public override void Init()
        {
            m_Content = new RectangleImage
            {
                Image = Images.NetSoftVersion,
                OutLineRectangle = new Rectangle(70, 178, 600, 300)
            };

            m_ReturnBtn = new HXD3DBlueButton
            {
                Text = "����",
                OutLineRectangle = new Rectangle(700,510,80,40),
            };
            m_ReturnBtn.ButtonClickEvent +=
                (sender, args) => Parent.RequestNavigateTo(NetControlChildType.NetControlRoot);
        }

        /// <summary>��ͼ</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_Content.OnDraw(g);

            m_ReturnBtn.OnDraw(g);
        }

        /// <summary>��갴�º���</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            m_ReturnBtn.OnMouseUp(point);

            return true;
        }

        /// <summary>��갴��</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            return m_ReturnBtn.OnMouseDown(point);
        }
    }
}