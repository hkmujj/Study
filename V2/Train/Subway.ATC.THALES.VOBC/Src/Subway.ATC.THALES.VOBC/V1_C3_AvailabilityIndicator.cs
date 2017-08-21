using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
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

            
        }
    }
}
