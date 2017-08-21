using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HD_HXD2_TMS.V2机车信息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V2_TrainInfo : baseClass
    {
        private List<Image> _images = new List<Image>();

        public override string GetInfo()
        {
            return "机车信息试图-机车信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_images[0], new Rectangle(1, 118, 798, 208));
            bool isBreaking = BoolList[UIObj.InBoolList[28]];
            
            //单独A车
            if (BoolList[UIObj.InBoolList[0]]) //在A端
            {
                //机车编号
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                    new Font("宋体", 11),
                    Brushes.White,
                    new RectangleF(303-5, 117 + 42, 70+10, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                //受电弓
                for (int i = 0; i < 4; i++)
                {
                    if (BoolList[UIObj.InBoolList[3] + i])
                    {
                        dcGs.DrawImage(_images[5 + i], new Rectangle(209, 118 + 34, 38, 38));
                    }
                }
                //司机室占用
                if (BoolList[UIObj.InBoolList[5]])
                {
                    dcGs.DrawImage(_images[9], new Rectangle(163, 118 + 83, 29, 18));
                }
                //高压隔离开关
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[6]+i])
                    {
                        dcGs.DrawImage(_images[10+i], new Rectangle(299, 118 + 74, 31, 14));
                    }
                }
                //主断
                for (int i = 0; i < 5; i++)
                {
                    if (BoolList[UIObj.InBoolList[8] + i])
                    {
                        dcGs.DrawImage(_images[12 + i], new Rectangle(341, 118 + 68, 39, 38));
                    }
                }
                //转向架制动
                for (int i = 0; i < 2; i++)//2个转向架
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (BoolList[UIObj.InBoolList[10+i] + j*4])
                        {
                            dcGs.DrawImage(_images[17 + j], new Rectangle(231+56*i, 118 + 97, 54, 26));
                        }
                    }
                }
                //停放制动
                for (int i = 0; i < 3; i++)
                {
                    if (BoolList[UIObj.InBoolList[14] + i])
                    {
                        dcGs.DrawImage(_images[20 + i], new Rectangle(341, 118 + 110, 37, 27));
                    }
                }
                //电机
                for (int i = 0; i < 2; i++)//2对电机
                {
                    for (int j = 0; j < 2; j++)//2个电机
                    {
                        for (int k = 0; k < 2; k++)//2个状态
                        {
                            if (BoolList[UIObj.InBoolList[16 + i*2+j] + k * 8])
                            {
                                dcGs.DrawImage(_images[23 + k], new Rectangle(182 + 88 * i+34*j, 118 + 128, 29, 28));
                            }
                        }
                    }
                }
                //恒力矩
                Brush[] brushes = {Brushes.Red, Brushes.LimeGreen};
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[24] + i])
                    {
                        dcGs.FillRectangle(brushes[i], new Rectangle(186, 118 + 170 + 10, 75, 30));
                        dcGs.DrawString(
                            "恒力矩",
                            new Font("宋体", 13),
                            Brushes.Black,
                            new RectangleF(186, 118 + 170 + 10+2, 75, 30),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
                //准恒速
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[26] + i])
                    {
                        dcGs.FillRectangle(brushes[i], new Rectangle(276, 118 + 170 + 10, 75, 30));
                        dcGs.DrawString(
                            "准恒速",
                            new Font("宋体", 13),
                            Brushes.Black,
                            new RectangleF(276, 118 + 170 + 10 + 2, 75, 30),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
                //牵引制动
                dcGs.DrawString(
                    "kN",
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(102, 118 + 131, 26, 26),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
                String valueA = FloatList[UIObj.InFloatList[isBreaking ? 3 : 2]].ToString("0");
                if (isBreaking) valueA = "-" + valueA;
                dcGs.DrawString(
                    valueA,
                    new Font("宋体", 13),
                    Brushes.Yellow,
                    new RectangleF(43, 118 + 131, 58, 24),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                    );

                if (!BoolList[UIObj.InBoolList[1]])//右侧为B端
                {
                    dcGs.DrawImage(_images[1], new Rectangle(1, 118, 798, 208));
                    //机车编号
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                        new Font("宋体", 11),
                        Brushes.White,
                        new RectangleF(416-5, 117 + 42, 70+10, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                    //受电弓
                    for (int i = 0; i < 4; i++)
                    {
                        if (BoolList[UIObj.InBoolList[4] + i])
                        {
                            dcGs.DrawImage(_images[5 + i], new Rectangle(544, 118 + 34, 38, 38));
                        }
                    }
                    //司机室占用
                    if (BoolList[UIObj.InBoolList[5] + 1])
                    {
                        dcGs.DrawImage(_images[9], new Rectangle(597, 118 + 82, 29, 18));
                    }
                    //高压隔离开关
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[7] + i])
                        {
                            dcGs.DrawImage(_images[10 + i], new Rectangle(460, 118 + 74, 31, 14));
                        }
                    }
                    //主断
                    for (int i = 0; i < 5; i++)
                    {
                        if (BoolList[UIObj.InBoolList[9] + i])
                        {
                            dcGs.DrawImage(_images[12 + i], new Rectangle(409, 118 + 68, 39, 38));
                        }
                    }
                    //转向架制动
                    for (int i = 0; i < 2; i++)//2个转向架
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (BoolList[UIObj.InBoolList[12 + i] + j * 4])
                            {
                                dcGs.DrawImage(_images[17 + j], new Rectangle(504 - 56 * i, 118 + 97, 54, 26));
                            }
                        }
                    }
                    //停放制动
                    for (int i = 0; i < 3; i++)
                    {
                        if (BoolList[UIObj.InBoolList[15] + i])
                        {
                            dcGs.DrawImage(_images[20 + i], new Rectangle(410, 118 + 110, 37, 27));
                        }
                    }
                    //电机
                    for (int i = 0; i < 2; i++)//2对电机
                    {
                        for (int j = 0; j < 2; j++)//2个电机
                        {
                            for (int k = 0; k < 2; k++)//2个状态
                            {
                                if (BoolList[UIObj.InBoolList[20 + i * 2 + j] + k * 8])
                                {
                                    dcGs.DrawImage(_images[23 + k], new Rectangle(581 - 88 * i - 34 * j, 118 + 128, 29, 28));
                                }
                            }
                        }
                    }
                    //恒力矩
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[25] + i])
                        {
                            dcGs.FillRectangle(brushes[i], new Rectangle(530, 118 + 170 + 10, 75, 30));
                            dcGs.DrawString(
                                "恒力矩",
                                new Font("宋体", 13),
                                Brushes.Black,
                                new RectangleF(530, 118 + 170 + 10 + 2, 75, 30),
                                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                                );
                        }
                    }
                    //准恒速
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[27] + i])
                        {
                            dcGs.FillRectangle(brushes[i], new Rectangle(440, 118 + 170 + 10, 75, 30));
                            dcGs.DrawString(
                                "准恒速",
                                new Font("宋体", 13),
                                Brushes.Black,
                                new RectangleF(440, 118 + 170 + 10 + 2, 75, 30),
                                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                                );
                        }
                    }
                    //牵引制动
                    dcGs.DrawString(
                        "kN",
                        new Font("宋体", 11),
                        Brushes.Yellow,
                        new RectangleF(748, 118 + 131, 26, 26),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                        );
                    String valueB = FloatList[UIObj.InFloatList[isBreaking ? 5 : 4]].ToString("0");
                    if (isBreaking) valueB = "-" + valueB;
                    dcGs.DrawString(
                        valueB,
                        new Font("宋体", 13),
                        Brushes.Yellow,
                        new RectangleF(688, 118 + 131, 58, 24),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                        );
                }
            }
            else if (BoolList[UIObj.InBoolList[0] + 1]) //在B端
            {
                //机车编号
                dcGs.DrawString(
                    "HXD2" + FloatList[UIObj.InFloatList[1]] + "B",
                    new Font("宋体", 11),
                    Brushes.White,
                    new RectangleF(303-5, 117 + 42, 70+10, 20),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                //受电弓
                for (int i = 0; i < 4; i++)
                {
                    if (BoolList[UIObj.InBoolList[4] + i])
                    {
                        dcGs.DrawImage(_images[5 + i], new Rectangle(209, 118 + 34, 38, 38));
                    }
                }
                //司机室占用
                if (BoolList[UIObj.InBoolList[5] + 1])
                {
                    dcGs.DrawImage(_images[9], new Rectangle(163, 118 + 83, 29, 18));
                }
                //高压隔离开关
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[7] + i])
                    {
                        dcGs.DrawImage(_images[10 + i], new Rectangle(299, 118 + 74, 31, 14));
                    }
                }
                //主断
                for (int i = 0; i < 5; i++)
                {
                    if (BoolList[UIObj.InBoolList[9] + i])
                    {
                        dcGs.DrawImage(_images[12 + i], new Rectangle(341, 118 + 68, 39, 38));
                    }
                }
                //转向架制动
                for (int i = 0; i < 2; i++)//2个转向架
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (BoolList[UIObj.InBoolList[12 + i] + j * 4])
                        {
                            dcGs.DrawImage(_images[17 + j], new Rectangle(231 + 56 * i, 118 + 97, 54, 26));
                        }
                    }
                }
                //停放制动
                for (int i = 0; i < 3; i++)
                {
                    if (BoolList[UIObj.InBoolList[15] + i])
                    {
                        dcGs.DrawImage(_images[20 + i], new Rectangle(243, 118 + 110, 37, 27));
                    }
                }
                //电机
                for (int i = 0; i < 2; i++)//2对电机
                {
                    for (int j = 0; j < 2; j++)//2个电机
                    {
                        for (int k = 0; k < 2; k++)//2个状态
                        {
                            if (BoolList[UIObj.InBoolList[20 + i * 2 + j] + k * 8])
                            {
                                dcGs.DrawImage(_images[23 + k], new Rectangle(182 + 88 * i + 34 * j, 118 + 128, 29, 28));
                            }
                        }
                    }
                }
                //恒力矩
                Brush[] brushes = { Brushes.Red, Brushes.LimeGreen };
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[25] + i])
                    {
                        dcGs.FillRectangle(brushes[i], new Rectangle(186, 118 + 170 + 10, 75, 30));
                        dcGs.DrawString(
                            "恒力矩",
                            new Font("宋体", 13),
                            Brushes.Black,
                            new RectangleF(186, 118 + 170 + 10 + 2, 75, 30),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
                //准恒速
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[UIObj.InBoolList[27] + i])
                    {
                        dcGs.FillRectangle(brushes[i], new Rectangle(276, 118 + 170 + 10, 75, 30));
                        dcGs.DrawString(
                            "准恒速",
                            new Font("宋体", 13),
                            Brushes.Black,
                            new RectangleF(276, 118 + 170 + 10 + 2, 75, 30),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
                //牵引制动
                dcGs.DrawString(
                    "kN",
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(102, 118 + 131, 26, 26),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                    );
                String valueA = FloatList[UIObj.InFloatList[isBreaking ? 5 : 4]].ToString("0");
                if (isBreaking) valueA = "-" + valueA;
                dcGs.DrawString(
                    valueA,
                    new Font("宋体", 13),
                    Brushes.Yellow,
                    new RectangleF(43, 118 + 131, 58, 24),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                    );

                if (!BoolList[UIObj.InBoolList[1] + 1])
                {
                    dcGs.DrawImage(_images[1], new Rectangle(1, 118, 798, 208));
                    //机车编号
                    dcGs.DrawString(
                        "HXD2" + FloatList[UIObj.InFloatList[0]] + "A",
                        new Font("宋体", 11),
                        Brushes.White,
                        new RectangleF(416-5, 117 + 42, 70+10, 20),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
                    //受电弓
                    for (int i = 0; i < 4; i++)
                    {
                        if (BoolList[UIObj.InBoolList[3] + i])
                        {
                            dcGs.DrawImage(_images[5 + i], new Rectangle(544, 118 + 34, 38, 38));
                        }
                    }
                    //司机室占用
                    if (BoolList[UIObj.InBoolList[5]])
                    {
                        dcGs.DrawImage(_images[9], new Rectangle(597, 118 + 82, 29, 18));
                    }
                    //高压隔离开关
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[6] + i])
                        {
                            dcGs.DrawImage(_images[10 + i], new Rectangle(460, 118 + 74, 31, 14));
                        }
                    }
                    //主断
                    for (int i = 0; i < 5; i++)
                    {
                        if (BoolList[UIObj.InBoolList[8] + i])
                        {
                            dcGs.DrawImage(_images[12 + i], new Rectangle(409, 118 + 68, 39, 38));
                        }
                    }
                    //转向架制动
                    for (int i = 0; i < 2; i++)//2个转向架
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (BoolList[UIObj.InBoolList[10 + i] + j * 4])
                            {
                                dcGs.DrawImage(_images[17 + j], new Rectangle(504 - 56 * i, 118 + 97, 54, 26));
                            }
                        }
                    }
                    //停放制动
                    for (int i = 0; i < 3; i++)
                    {
                        if (BoolList[UIObj.InBoolList[14] + i])
                        {
                            dcGs.DrawImage(_images[20 + i], new Rectangle(410, 118 + 110, 37, 27));
                        }
                    }
                    //电机
                    for (int i = 0; i < 2; i++)//2对电机
                    {
                        for (int j = 0; j < 2; j++)//2个电机
                        {
                            for (int k = 0; k < 2; k++)//2个状态
                            {
                                if (BoolList[UIObj.InBoolList[16 + i * 2 + j] + k * 8])
                                {
                                    dcGs.DrawImage(_images[23 + k], new Rectangle(581 - 88 * i - 34 * j, 118 + 128, 29, 28));
                                }
                            }
                        }
                    }
                    //恒力矩
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[24] + i])
                        {
                            dcGs.FillRectangle(brushes[i], new Rectangle(530, 118 + 170 + 10, 75, 30));
                            dcGs.DrawString(
                                "恒力矩",
                                new Font("宋体", 13),
                                Brushes.Black,
                                new RectangleF(530, 118 + 170 + 10 + 2, 75, 30),
                                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                                );
                        }
                    }
                    //准恒速
                    for (int i = 0; i < 2; i++)
                    {
                        if (BoolList[UIObj.InBoolList[26] + i])
                        {
                            dcGs.FillRectangle(brushes[i], new Rectangle(440, 118 + 170 + 10, 75, 30));
                            dcGs.DrawString(
                                "准恒速",
                                new Font("宋体", 13),
                                Brushes.Black,
                                new RectangleF(440, 118 + 170 + 2 + 10, 75, 30),
                                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                                );
                        }
                    }
                    //牵引制动
                    dcGs.DrawString(
                        "kN",
                        new Font("宋体", 11),
                        Brushes.Yellow,
                        new RectangleF(748, 118 + 131, 26, 26),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                        );
                    String valueB = FloatList[UIObj.InFloatList[isBreaking ? 3 : 2]].ToString("0");
                    if (isBreaking) valueB = "-" + valueB;
                    dcGs.DrawString(
                        valueB,
                        new Font("宋体", 13),
                        Brushes.Yellow,
                        new RectangleF(688, 118 + 131, 58, 24),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far }
                        );
                }
            }

            #region 机车方向
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.DrawImage(_images[2+i], new Rectangle(52, 118+25, 38, 38));
                }
            }
            #endregion

            base.paint(dcGs);
        }
    }
}
