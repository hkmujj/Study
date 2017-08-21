using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 外部按键
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ExternalButtons : baseClass
    {
        private List<GDIButton> m_ButtomBtns;

        private ReceiveRequest m_ReceiveRequest;

        public override bool init(ref int nErrorObjectIndex)
        {
            var types = Enum.GetValues(typeof (ButtonType)).Cast<ButtonType>().ToArray();

            m_ButtomBtns = GetBtnRegions().Select((s, i) =>
                {
                    var btn = new GDIButton() { OutLineRectangle = s, Text =EnumUtil.GetDescription((ButtonType)i).FirstOrDefault(), TextColor = Color.OrangeRed , Tag = types[i]};
                    btn.ContentTextControl.TextFormat.Alignment = StringAlignment.Center;
                    btn.ButtonDownEvent = (sender, args) =>
                    {
                        var b = (GDIButton) sender;
                        m_ReceiveRequest.ClearDownStates();
                        ReceiveRequest.ButtonInfoDictionary[(ButtonType) b.Tag].State = MouseState.MouseDown;
                    };
                    btn.ButtonUpEvent = (sender, args) =>
                    {
                        var b = (GDIButton)sender;
                        ReceiveRequest.ButtonInfoDictionary[(ButtonType)b.Tag].State = MouseState.MouseUp;
                    };
                    return btn;
                }).ToList();

            m_ReceiveRequest = ObjectService.GetObject<ReceiveRequest>(ProjectName).First();

            return true;
        }

        public override void paint(Graphics g)
        {
            m_ButtomBtns.ForEach(e => e.OnDraw(g));
        }

        public override bool mouseDown(Point point)
        {
            return m_ButtomBtns.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_ButtomBtns.Any(a => a.OnMouseUp(point));
        }

        private IEnumerable<Rectangle> GetBtnRegions()
        {
            var y = Common.InitialPosY + 620;
            yield return new Rectangle(3, y, 81, 50);

            for (int i = 0; i < 10; i++)
            {
                yield return new Rectangle(90 + 71 * i, y, 63, 50);
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    yield return new Rectangle(900+i*70, 100+j*60, 63, 50);
                }
            }
          
        }
    }
}