using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V53_LongDistanceCutOff : baseClass
    {
        public static int CurrentRow = 0;
        public static int[] CutOffList = new Int32[16];
        private List<Image> _imgs = new List<Image>();

        public override string GetInfo()
        {
            return "远程切除界面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            drawFrame(dcGs);

            base.paint(dcGs);
        }

        private void drawFrame(Graphics dcGs)
        {
            String[] strs =
            {
                "TCU1", "TCU1_1轴电机", "TCU1_2轴电机", "TCU1_3轴电机", "TCU1_辅逆变", "TCU1_1重四象限", "TCU1_2重四象限", "TCU1_3重四象限",
                "TCU2", "TCU2_4轴电机", "TCU2_5轴电机", "TCU2_6轴电机", "TCU2_辅逆变", "TCU2_1重四象限", "TCU2_2重四象限", "TCU2_3重四象限"
            };

            for (int i = 0; i < 2; i++)
            {
                dcGs.FillRectangle(Brushes.White, new Rectangle(408,97+212*i,83,192));
            }

            dcGs.DrawString(
                "机车编号", FormatStyle.Font14, Brushes.White, new Rectangle(231, 97 - 24 - 15, 177, 28),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                );
            dcGs.DrawString(
                "0256", FormatStyle.Font14, Brushes.White, new Rectangle(408 + 1, 97 -24 - 15, 83 - 2, 22),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );

            for (int i = 0; i < 16; i++)
            {
                if(CurrentRow==i)
                    dcGs.DrawString(
                        strs[i], FormatStyle.Font14, Brushes.Yellow, new Rectangle(231, 97 + (i % 8) * 24 + (i / 8) * 212, 177, 28),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                        );
                else
                    dcGs.DrawString(
                        strs[i], FormatStyle.Font14, Brushes.White, new Rectangle(231, 97 + (i % 8) * 24 + (i / 8) * 212, 177, 28),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                        );

                if (CutOffList[i] == 0) //投入
                {
                    dcGs.FillRectangle(Brushes.LawnGreen, new Rectangle(408 + 1, 97 + 1+(i/8)*212 + 24*(i%8), 83 - 2, 22));

                    dcGs.DrawString(
                        "投入", FormatStyle.Font14, FormatStyle.BlackBrush, new Rectangle(408 + 1, 97 + 1 + (i / 8) * 212 + 24 * (i % 8)+2, 83 - 2, 22),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
                else
                {
                    dcGs.FillRectangle(Brushes.Red, new Rectangle(408 + 1, 97 + 1 + (i / 8) * 212 + 24 * (i % 8), 83 - 2, 22));

                    dcGs.DrawString(
                        "切除", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(408 + 1, 97 + 1 + (i / 8) * 212 + 24 * (i % 8)+2, 83 - 2, 22),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                }
            }
        }
    }
}
