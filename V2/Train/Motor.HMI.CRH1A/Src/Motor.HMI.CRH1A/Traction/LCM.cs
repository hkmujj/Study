using System.Drawing;
using Motor.HMI.CRH1A.Traction;

namespace Motor.HMI.CRH1A
{
    ////������
    public class LCM : TractionEquipMemtBase
    {

        public LCM(int x, int y, int weight, int height)
            : base(new Rectangle(x, y, weight, height))
        {
            Type = EquipType.Lcm;
        }

        public override void OnDraw(Graphics g)
        {
            switch (Status)
            {
                case EquipStatus.Active: //����״̬
                    g.FillRectangle(GreenBrush, OutLineOutLineRectangle);
                    g.DrawString("LCM", Strfont, WhiteBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y+8);
                    break;
                case EquipStatus.Well://���״̬
                    g.DrawRectangle(GreenPen, OutLineOutLineRectangle);
                    g.DrawString("LCM", Strfont, GreenBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y+8);
                    break;
                case EquipStatus.Fault://���ֹ���
                    g.DrawRectangle(RedPen, OutLineOutLineRectangle);
                    g.DrawString("LCM", Strfont, RedBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y+8);
                    break;
                case EquipStatus.CutOut://�ж�״̬
                    g.DrawRectangle(BluePen, OutLineOutLineRectangle);
                    g.DrawString("LCM", Strfont, BlueBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y+8);
                    break;
                case EquipStatus.Unknow://δ֪״̬
                    g.DrawRectangle(GrayPen, OutLineOutLineRectangle);
                    g.DrawString("LCM", Strfont, GrayBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y+8);
                    break;
            }

            base.OnDraw(g);
        }
       
    }
}