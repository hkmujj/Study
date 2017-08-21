using System.Drawing;
using Motor.HMI.CRH1A.Traction;

namespace Motor.HMI.CRH1A
{
    public class ACM : TractionEquipMemtBase
    {

        public ACM(int x, int y, int weight, int height)
            : base(new Rectangle(x, y, weight, height))
        {
            Type = EquipType.ACM;
        }


        public override void OnDraw(Graphics g)
        {
            switch (Status)
            {
                case EquipStatus.Active: //����״̬
                    g.FillRectangle(GreenBrush, OutLineOutLineRectangle);
                    g.DrawString("ACM", Strfont, WhiteBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 8);
                    break;
                case EquipStatus.Well://���״̬
                    g.DrawRectangle(GreenPen, OutLineOutLineRectangle);
                    g.DrawString("ACM", Strfont, GreenBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 8);
                    break;
                case EquipStatus.Fault://���ֹ���
                    g.DrawRectangle(RedPen, OutLineOutLineRectangle);
                    g.DrawString("ACM", Strfont, RedBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 8);
                    break;
                case EquipStatus.CutOut:
                    g.DrawRectangle(BluePen, OutLineOutLineRectangle);
                    g.DrawString("ACM", Strfont, BlueBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 8);
                    break;
                case EquipStatus.Unknow://δ֪״̬
                    g.DrawRectangle(GrayPen, OutLineOutLineRectangle);
                    g.DrawString("ACM", Strfont, GrayBrush, OutLineOutLineRectangle.X, OutLineOutLineRectangle.Y + 8);
                    break;
            }

            base.OnDraw(g);
        }
    }
}