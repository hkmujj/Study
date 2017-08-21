using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace SS4B_TMS.WirelessInterface
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainDecodingView : baseClass
    {
        private List<SS4BButton> m_Button;
        private Timer timer;

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Button = new List<SS4BButton>();
            var btn = new SS4BButton
            {
                Text = "确认\r\n\"E\"",
                BackIamge = ImageResource.back,
                OutRec = new Rectangle(185, 300, 100, 100),
                MoseUpImage = ImageResource.back,
                MoseDownImage = ImageResource.back,
                TextBrush = Brushs.WhiteBrush,
                TextFormat = FontInfo.SfCc,
                TextFont = new Font("宋体", 20f),
            };
            timer = new Timer((state) =>
             {
                 append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB解除编组标志), 0, 0);
                 append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
             });
            btn.MouseDown += () =>
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB解除编组标志), 1, 0);
                timer.Change(500, int.MaxValue);
            };
            var btn1 = new SS4BButton
            {
                Text = "取消\r\n\"C\"",
                BackIamge = ImageResource.back,
                OutRec = new Rectangle(465, 300, 100, 100),
                MoseUpImage = ImageResource.back,
                MoseDownImage = ImageResource.back,
                TextBrush = Brushs.WhiteBrush,
                TextFormat = FontInfo.SfCc,
                TextFont = new Font("宋体", 20f),
            };
            btn1.MouseDown += () =>
            {
                append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
            };
            m_Button.Add(btn);
            m_Button.Add(btn1);

            return true;
        }

        private void ReciveButtonInfo()
        {
            if (GetInBoolValue(InBoolKeys.InB确认按下状态MMIE确认键按下状态))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OutB解除编组标志), 1, 0);
                timer.Change(500, int.MaxValue);
            }
            else if (GetInBoolValue(InBoolKeys.InB设定按下状态))
            {
                append_postCmd(CmdType.ChangePage, (int)ViewState.WirelessInterface, 0, 0);
            }
        }

        public override bool mouseUp(Point point)
        {
            m_Button.ForEach(f => f.OnMouseUp(point));
            return true;
        }

        public override bool mouseDown(Point point)
        {
            m_Button.ForEach(f => f.OnMouseDown(point));
            return true;
        }

        public override void paint(Graphics g)
        {
            ReciveButtonInfo();
            m_Button.ForEach(f => f.OnDraw(g));
            base.paint(g);
        }
    }
}