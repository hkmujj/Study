using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Maintenace : HMIBase
    {
        private  FjButtonEx[] m_BtnArr;

        private void BtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            //append_postCmd(CmdType.ChangePage, btnSender.BtnIndex, 0, 0);
            ChangedPage((IranViewIndex) btnSender.BtnIndex);
        }

        public override string GetInfo()
        {
            return "Maintenace";
        }

        protected override bool Initalize()
        {
            m_BtnArr = new[]
                     {
                         new FjButtonEx(29, "Acc./Ret. test", new Rectangle(180, 230, 97, 62)),
                         new FjButtonEx(31, "Master ctrl test", new Rectangle(350, 230, 97, 62)),
                         new FjButtonEx(32, "Fill/Leak test", new Rectangle(520, 230, 97, 62))
                     };
            foreach (var btn in m_BtnArr)
            {
                btn.MouseDown += BtnMouseDown;
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var btn in m_BtnArr)
            {
                btn.OnDraw(g);
            }
        }

        public override bool mouseDown(Point point)
        {
            foreach (var btn in m_BtnArr)
            {
                if (btn.IsVisible(point))
                {
                    //btn.Status = BtnStatus.Active;
                    btn.OnMouseDown(point);
                    break;
                }
            }
            return base.mouseDown(point);
        }
    }
}