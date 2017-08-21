using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C3_AvailabilityIndicator:baseClass
    { 
        public override string GetInfo()
        {
            return "可用性指示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            Font f = new Font("Arial", 15, FontStyle.Bold);
            Brush[] brushs = new Brush[] { Brushes.LimeGreen, Brushes.Red, Brushes.Yellow };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[0+i]])
                {
                    //Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    //if (i == 1)
                    //    b = new SolidBrush(Color.Red);
                    dcGs.DrawString("ATP", f, brushs[i], new PointF(715, 80));
                    break;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[3+i]])
                {
                    //Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    //if (i == 1)
                    //    b = new SolidBrush(Color.Red);
                    dcGs.DrawString("ATO", f, brushs[i], new PointF(620, 80));
                    break;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[6+i]])
                {
                    //Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    //if (i == 1)
                    //    b = new SolidBrush(Color.Red);
                    dcGs.DrawString("IATP", f, brushs[i], new PointF(520, 80));
                    break;
                }
            }

            base.paint(dcGs);
        }
    }
}
