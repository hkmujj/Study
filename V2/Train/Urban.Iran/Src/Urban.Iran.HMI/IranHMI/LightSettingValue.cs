using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LightSettingValue : HMIBase
    {
        protected override bool Initalize()
        {

            return true;
        }

        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g"/>
        public override void paint(Graphics g)
        {
            
            var gdibrush = new SolidBrush(Color.FromArgb(100 - Settings.LightValue, 0, 0, 0));
            g.FillRectangle(gdibrush, 0, 0, 800, 600);
        }
    }
}