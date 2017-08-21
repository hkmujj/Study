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
    public class V1_C2_Door : baseClass
    {
        private String[] doorStates_Train;
        private String[] doorStates_Station;
        //private String[] doorOpenModes_Train;
        private String[] doorStates;
        private String[] trainStates;

        public override string GetInfo()
        {
            return "门状态";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            this.doorStates_Train = new String[4] { "X", "X", "O", "O" };
            this.doorStates_Station = new String[4] { "", "X", "O", "" };
            //this.doorOpenModes_Train = new String[2] { "AUTOMATIC", "   MANUAL" };
            this.doorStates = new String[2] { "自动", "手动" };
            this.trainStates = new String[3] { "牵引", "制动", "惰性" };

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    Font f = new Font("Arial", 30, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    if (i == 2 || i == 3)
                        b = new SolidBrush(Color.Red);
                    dcGs.DrawString(
                        this.doorStates_Train[i],
                        f, 
                        b,
                        new RectangleF(506, 268, 142, 47),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Font f = new Font("Arial", 30, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    if (i == 2 || i == 3)
                        b = new SolidBrush(Color.Red);
                    dcGs.DrawString(
                        this.doorStates_Station[i], 
                        f, 
                        b,
                        new RectangleF(651, 268, 142, 47),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                    break;
                }
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[2] + i])
            //    {
            //        Font f = new Font("Arial", 14, FontStyle.Bold);
            //        Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
            //        dcGs.DrawString(this.doorOpenModes_Train[i], f, b, new PointF(515, 284));
            //        break;
            //    }
            //}

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    Font f = new Font("Arial", 14, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    dcGs.DrawString(
                        this.doorStates[i], 
                        f, 
                        b, 
                        new RectangleF(504,161,141,35),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                    break;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[4] + i])
                {
                    Font f = new Font("Arial", 14, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    dcGs.DrawString(
                        this.trainStates[i],
                        f,
                        b,
                        new RectangleF(649, 161, 141, 35),
                        new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        );
                    break;
                }
            }

            if (BoolList[UIObj.InBoolList[5]])
            {
                dcGs.DrawString(
                    "使能",
                    new Font("Arial", 14, FontStyle.Bold),
                    new SolidBrush(Color.FromArgb(0, 255, 0)),
                    new RectangleF(506, 239, 142, 35),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }

            if (BoolList[UIObj.InBoolList[6]])
            {
                dcGs.DrawString(
                    "使能",
                    new Font("Arial", 14, FontStyle.Bold),
                    new SolidBrush(Color.FromArgb(0, 255, 0)),
                    new RectangleF(651, 239, 142, 35),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
    }
}
