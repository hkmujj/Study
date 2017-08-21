using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Huaping2D : ATPBase
    {
        int FC_X = FrameButton2D.FrameChange_X;
        int FC_Y = FrameButton2D.FrameChange_Y;

        public override void paint(Graphics g)
        {
            GetValue();
            OnDraw(g);
        }
        public void GetValue()
        {
            //if (!BoolList[270])//花屏
            if (!BoolList[UIObj.InBoolList[0]])//花屏
            {
                Background2D.bfirstshuju = false;
                StrDrivData2D.breal = false;
                DMIMainMenu2D.Popuptxt = " ";
                Background2D.bfirstcheci = false;
                Background2D.bfirstsijihao = false;
                append_postCmd(CmdType.ChangePage, 101, 0, 0);
            }
        }

        public override bool Initalize()
        {
            if (UIObj.ParaList.Count < 0)
            {
                return false;
            }
            return true;
        }

        public override string GetInfo()
        {
            return "花屏";
        }

        public void OnDraw(Graphics g)
        {
            g.DrawImage(Images[0], 0, 0, Images[0].Width, Images[0].Height);
        }
    }
}