using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D.Engine_Config
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Engine_Config : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> BoolIds;
        //<summary>
        //float逻辑号
        // </summary>
        private List<int> FoolatIds;

        /// <summary>
        /// 图片的坐标
        /// </summary>
        private List<Rectangle> _rectangles;


        public override string GetInfo()
        {
            return "机车配置";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }


        public override void paint(Graphics dcGs)
        {
            DrawImg(dcGs);
            DrawRects(dcGs);
            base.paint(dcGs);

        }
        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawRects(Graphics e)
        {
            if (BoolList[BoolIds[23]])
            {
                for (int i = 0; i < 14; i++)
                {
                    e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i + 6]);
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i + 23]);
                }
            }

        }
        /// <summary>
        /// 画图片
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImg(Graphics e)
        {
            if (Title.CurentView == 2 && BoolList[BoolIds[23]])//当视图为2，选中机车配置的按键，则画双车
            {
                e.DrawImage(_imgs[0], _rectangles[0]); //画车体的图片
                e.DrawImage(_imgs[0], _rectangles[1]);
            }
            //受电弓的状态
            else
            {

                if (BoolList[BoolIds[4]] && BoolList[BoolIds[28]] && !BoolList[BoolIds[30]])
                {
                    e.DrawImage(_imgs[18], _rectangles[20]);
                }

                else if (BoolList[BoolIds[30]] && BoolList[BoolIds[5]] && !BoolList[BoolIds[28]])
                {
                    e.DrawImage(_imgs[19], _rectangles[20]);
                }
                else if (BoolList[BoolIds[4]] && BoolList[BoolIds[30]] && BoolList[BoolIds[5]] && BoolList[BoolIds[28]])
                {
                    e.DrawImage(_imgs[20], _rectangles[20]);
                }

                else if (BoolList[BoolIds[28]] && BoolList[BoolIds[30]] && !BoolList[BoolIds[4]] && !BoolList[BoolIds[5]])
                {
                    e.DrawImage(_imgs[22], _rectangles[20]);
                }
                else if (BoolList[BoolIds[30]] && !BoolList[BoolIds[28]])
                {
                    e.DrawImage(_imgs[23], _rectangles[20]);
                }
                else if (BoolList[BoolIds[28]] && !BoolList[BoolIds[30]])
                {
                    e.DrawImage(_imgs[24], _rectangles[20]);
                }

                else if (BoolList[BoolIds[28]] && BoolList[BoolIds[30]] && BoolList[BoolIds[5]])
                {
                    e.DrawImage(_imgs[25], _rectangles[20]);
                }
                else if (BoolList[BoolIds[4]] && BoolList[BoolIds[30]] && BoolList[BoolIds[28]])
                {
                    e.DrawImage(_imgs[26], _rectangles[20]);
                }
                else
                {
                    e.DrawImage(_imgs[21], _rectangles[20]);
                }
                e.DrawImage(_imgs[27], _rectangles[46]);
                e.DrawImage(_imgs[28], _rectangles[47]);


            }
            //司机室被占用的状态
            if (BoolList[BoolIds[26]])
            {
                e.DrawImage(_imgs[29], _rectangles[48]);
            }
            if (BoolList[BoolIds[27]])
            {
                e.DrawImage(_imgs[30], _rectangles[49]);
            }

            //主断状态
            if (BoolList[BoolIds[0]])
            {
                e.DrawImage(_imgs[2], _rectangles[30]);
            }

            else if (BoolList[BoolIds[1]])
            {
                e.DrawImage(_imgs[3], _rectangles[30]);
            }
            else if (BoolList[BoolIds[2]])
            {
                e.DrawImage(_imgs[4], _rectangles[30]);
            }
            else if (BoolList[BoolIds[3]])
            {
                e.DrawImage(_imgs[1], _rectangles[30]);
            }


            //电机的状态
            if (BoolList[BoolIds[13]])
            {
                e.DrawImage(_imgs[9], _rectangles[33]);
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[33]);
            }
            //else if (BoolList[BoolIds[36]])
            //{
            //    e.DrawImage(_imgs[31], _rectangles[33]);
            //    e.DrawRectangle(FormatStyle.WhitePen, _rectangles[33]);
            //}
            else
            {
                e.DrawImage(_imgs[8], _rectangles[33]);
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[33]);
            }
            if (BoolList[BoolIds[14]])
            {
                e.DrawImage(_imgs[9], _rectangles[34]);
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[34]);
            }
            else
            {
                e.DrawImage(_imgs[8], _rectangles[34]);
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[34]);
            }
            //机车制动
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[BoolIds[33 + i]])
                {
                    e.DrawImage(_imgs[15 + i], _rectangles[35]);
                    e.DrawImage(_imgs[15 + i], _rectangles[36]);
                }


            }

            //紧急制动

            if (BoolList[BoolIds[15]])
            {
                e.DrawImage(_imgs[10], _rectangles[38]);
            }
            //停放制动
            if (BoolList[BoolIds[16]])
            {
                e.DrawImage(_imgs[12], _rectangles[37]);
            }
            if (BoolList[BoolIds[17]])
            {
                e.DrawImage(_imgs[11], _rectangles[37]);
            }
            //本机投入，本机重联
            if (BoolList[BoolIds[18]])
            {
                e.DrawString("BCU\n重联", FormatStyle.Font14, FormatStyle.WhiteBrush, _rectangles[39], FormatStyle.MFormat);
            }
            else
            {

                e.DrawString("BCU\n本机投入", FormatStyle.Font14, FormatStyle.WhiteBrush, _rectangles[39], FormatStyle.MFormat);

            }
            ////门的占用情况
            //e.DrawImage(BoolList[BoolIds[37]] ? _imgs[14] : _imgs[13], _rectangles[40]);
            //e.DrawImage(BoolList[BoolIds[38]] ? _imgs[14] : _imgs[13], _rectangles[41]);


            //>>>>>>>>>>>>>>Start：门占用改为6个门，18个状态<<<<<<<<<<<<<<<<<<<<<<<<<<<
            e.DrawImage(_imgs[31], _rectangles[40].X, _rectangles[40].Y, _rectangles[40].Width + 1, _rectangles[40].Height);
            e.DrawImage(_imgs[31], _rectangles[41].X, _rectangles[41].Y, _rectangles[41].Width + 1, _rectangles[41].Height);
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[BoolIds[39+6*(i/3) + i%3]])
                {
                    e.DrawImage(_imgs[32 + i / 3], _rectangles[40].X + 1 + 38 * (i % 3), _rectangles[40].Y + 1, 36, 8);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (BoolList[BoolIds[42 + 6 * (i / 3) + i % 3]])
                {
                    e.DrawImage(_imgs[32 + i / 3], _rectangles[41].X + 1 + 38 * (i % 3), _rectangles[41].Y + 1, 36, 8);
                }
            }
            //>>>>>>>>>>>>>>End：门占用改为6个门，12个状态<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            #region //对象初始化
            BoolIds = new List<int>();
            FoolatIds = new List<int>();
            _rectangles = new List<Rectangle>();
            _imgs = new List<Image>();
            #endregion
            #region //对象配置表里取东西
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }

            #endregion
            #region//坐标的赋值
            //双车，机车配置火车图片位置          
            _rectangles.Add(new Rectangle(69, 99, 302, 79));
            _rectangles.Add(new Rectangle(403, 99, 302, 79));
            //车1，司机室占有状态位置
            _rectangles.Add(new Rectangle(104, 179, 98, 14));
            _rectangles.Add(new Rectangle(239, 179, 98, 14));
            //车2，司机室占有状态位置
            _rectangles.Add(new Rectangle(426, 179, 98, 14));
            _rectangles.Add(new Rectangle(562, 179, 98, 14));
            //车1，电机状态，停放制动状态
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(108 + j * 135, 193 + i * 55, 85, 55));
                }
            }
            //车2，电状态，停放制动状态
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(429 + j * 135, 193 + i * 55, 85, 55));
                }
            }
            //车1，停放制动，紧急制动状态 CCBII投入

            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(172, 309 + i * 53, 86, 53));

            }

            //车2，停放制动，紧急制动状态 CCBII投入

            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(494, 309 + i * 53, 86, 53));

            }
            //单车，火车图片的位置
            _rectangles.Add(new Rectangle(153, 72, 483, 90));
            //司机室占有状态位置
            _rectangles.Add(new Rectangle(252, 169, 98, 7));
            _rectangles.Add(new Rectangle(422, 169, 98, 7));
            //电机状态，停放制动状态
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(252 + j * 170, 175 + i * 62, 114, 62));
                }
            }
            //停放制动，紧急制动状态 CCBII投入

            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(339, 309 + i * 62, 114, 62));

            }
            //阻断状态位置
            _rectangles.Add(new Rectangle(370, 97, 51, 48));
            //受电功的状态

            _rectangles.Add(new Rectangle(267, 80, 37, 16));
            _rectangles.Add(new Rectangle(480, 80, 37, 16));


            //电制动运行
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(282 + i * 170, 181, 53, 45));
            }
            //机车制动
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(282 + i * 170, 246, 53, 45));

                //_rectangles.Add(new Rectangle(0, 0, 53, 45));
                //_rectangles.Add(new Rectangle(53, 54, 53*2, 45*2));
            }
            //停放制动
            _rectangles.Add(new Rectangle(339, 309, 114, 62));
            //紧急制动
            _rectangles.Add(new Rectangle(339, 371, 114, 62));
            //BCU本机投入BCU重联，
            _rectangles.Add(new Rectangle(360, 445, 78, 42));
            //门的占用情况
            _rectangles.Add(new Rectangle(251, 162, 114, 10));
            _rectangles.Add(new Rectangle(420, 162, 114, 10));
            //车顶隔离开关关
            _rectangles.Add(new Rectangle(320, 74, 37, 22));
            _rectangles.Add(new Rectangle(426, 74, 37, 22));
            //车顶隔离开关开
            _rectangles.Add(new Rectangle(324, 75, 37, 22));
            _rectangles.Add(new Rectangle(426, 74, 37, 22));
            //司机室1，司机室2
            _rectangles.Add(new Rectangle(222, 103, 26, 33));
            _rectangles.Add(new Rectangle(549, 103, 26, 33));
            //司机室1，司机室2被占用
            _rectangles.Add(new Rectangle(190, 101, 60, 37));
            _rectangles.Add(new Rectangle(547, 104, 60, 37));
            #endregion

        }
    }
}
