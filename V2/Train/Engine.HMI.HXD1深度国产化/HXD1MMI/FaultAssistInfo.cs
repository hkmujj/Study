using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 错误的辅助信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultAssistInfo : baseClass
    {
        private FaultAssistInfoRect _faultAssistInfoRect;

        private readonly Rectangle _backGround = new Rectangle(10, 70, 780, 470);

        public static Fault CurrentFault;

        private int _selectIndex =2;

        private GDIRectText m_InfoText;

        public override string GetInfo()
        {
            return "处理提示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _faultAssistInfoRect = new FaultAssistInfoRect(new Rectangle(10, 40, 780, 30), "", "", "");
            m_InfoText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(10, 75, 777, 466),
                BackColorVisible = false,
                DrawFont = Common._16Font,
                TextBrush = Common.WhiteBrush,
                TextFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near },
                RefreshAction = o =>
            {
                var txt = (GDIRectText) o;
                if (CurrentFault != null)
                {
                    switch (_selectIndex)
                    {
                        case 0:
                            txt.Text = CurrentFault.HelpinfoV_Equal_0;
                            break;
                        case 1:
                            txt.Text = CurrentFault.Helpinfo_V_Big_0;
                            break;
                        case 2:
                            txt.Text = CurrentFault.HelpInfo;
                            break;
                    }
                }
            }};
            return true;
        }

        private void GetValue()
        {
            if (BoolList[800])
            {
                _selectIndex = 0;
            }

            if (BoolList[801])
            {
                _selectIndex = 1;
            }

            if (BoolList[802])
            {
                _selectIndex = 2;
            }

            if (BoolList[809])
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }

            if (CurrentFault != null)
            {
                _faultAssistInfoRect._type.Text = CurrentFault.Level.ToString();
                _faultAssistInfoRect._describe.Text = CurrentFault.FaultText;

                switch (_selectIndex)
                {
                    case 0:
                        {
                            _faultAssistInfoRect._v.Text = "V=0";
                        }
                        break;
                    case 1:
                        {
                            _faultAssistInfoRect._v.Text = "V>0";
                        }
                        break;
                    case 2:
                        {
                            _faultAssistInfoRect._v.Text = "信息";
                        }
                        break;
                }
            }
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(Common.BlueBrush, _backGround);

            GetValue();

            _faultAssistInfoRect.OnDraw(g);

            m_InfoText.OnPaint(g);
        }
    }
}
