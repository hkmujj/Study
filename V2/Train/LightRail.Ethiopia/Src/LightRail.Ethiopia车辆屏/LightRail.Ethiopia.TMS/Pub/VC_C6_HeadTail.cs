using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.Pub
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C6_HeadTail : baseClass
    {
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-****";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                dcGs.FillRectangle(
                    new SolidBrush(Color.FromArgb(54, 54, 54)),
                    new Rectangle(182,241,416,125)
                    );
                dcGs.DrawString(
                    "Head tail switch conflict!",
                    new Font("Verdana", 24),
                    Brushes.Yellow,
                    new RectangleF(182-5, 241, 416+10, 125),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
    }
}
