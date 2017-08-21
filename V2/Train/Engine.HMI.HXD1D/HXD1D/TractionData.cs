using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Run;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class TractionData : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _images;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;
        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;

        /// <summary>
        /// 图片的坐标
        /// </summary>

        private List<Rectangle> _rectangles;
        /// <summary>
        /// 字的点坐标
        /// </summary>
        private List<Rectangle> _rects;
        /// <summary>
        /// 填充矩形的列表
        /// </summary>
        private List<NeedChangeLength> _changeLengths;

        public override string GetInfo()
        {
            return "牵引数据";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            DrawOn(dcGs);
        }

        /// <summary>
        /// 填充矩形框
        /// </summary>
        /// <param name="e"></param>
        private void FillRect(Graphics e)
        {
            if (BoolList[_boolIds[0]])
            {
                //转向架1-1 1-2 1-3牵引实际总值
                for (int i = 0; i < 3; i++)
                {
                   // DrawChange(e, i + 3, i, 0, i);
                    DrawRectangleChange(e,i+24,0,i);

                }
                //转向架2-1 2-2 2-3牵引实际总值
                for (int i = 0; i < 3; i++)
                {
                   // DrawChange(e, i + 15, 12 + i, 0, i + 3);
                    DrawRectangleChange(e, i + 27, 0, i+3);
                }

            }
            if (BoolList[_boolIds[1]])
            { //转向架1-1 1-2 1-3制动实际总值
                for (int i = 0; i < 3; i++)
                {
                    //DrawChange(e, i + 9, 6 + i, 1, i);
                    DrawRectangleChange(e, i + 30, 1, i);
                }

                //转向架2-1 2-2 2-3制动实际总值
                for (int i = 0; i < 3; i++)
                {
                    //DrawChange(e, 21 + i, 18 + i, 1, i + 3);
                    DrawRectangleChange(e, i + 33, 1, i+3);
                }
            }

        }

        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRects(Graphics e)
        {   //画一架矩形框
            for (int i = 1; i < 4; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i]);
            }
            //画二架矩形框
            for (int i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i + 4]);
            }

        }

        /// <summary>
        /// 画图片
        /// </summary>
        /// <param name="e"></param>
        private void DrawImg(Graphics e)
        {
            e.DrawImage(_images[0], _rectangles[7]);
            e.DrawImage(_images[0], _rectangles[8]);

            if (BoolList[_boolIds[0]])
            {  // 转向架1-1 1-2 1-3牵引给定总值
                for (int i = 0; i < 3; i++)
                {
                    DrawImag(e, 0 + i, i, FindImgClass.FindImgIndex(0, 0 + i, 24 + i, this, _foolatIds));
                }
                // 转向架2-1 2-2 2-3牵引给定总值
                for (int i = 0; i < 3; i++)
                {
                    DrawImag(e, 12 + i, i + 3, FindImgClass.FindImgIndex(0, 12 + i, 27 + i, this, _foolatIds));
                }

            }
            else if (BoolList[_boolIds[1]])
            { // 转向架1-1 1-2 1-3制动给定总值
                for (int i = 0; i < 3; i++)
                {
                    DrawImag(e, 6 + i, i, FindImgClass.FindImgIndex(1, 6 + i, 30 + i, this, _foolatIds));
                }

                // 转向架2-1 2-2 2-3制动给定总值
                for (int i = 0; i < 3; i++)
                {
                    DrawImag(e, 18 + i, 3 + i, FindImgClass.FindImgIndex(1, 18 + i, 33 + i, this, _foolatIds));
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    e.DrawImage(_images[1], _rectangles[9 + i]);
                }

            }

        }
        /// <summary>
        /// 画文字和数字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawChracter(Graphics e)
        {
            #region //画牵引实际值的显示数字
            if (BoolList[_boolIds[0]])
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[_foolatIds[3 + i]].ToString("0.0"), FormatStyle.Font10B, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
                }
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[_foolatIds[15 + i]].ToString("0.0"), FormatStyle.Font10B, FormatStyle.WhiteBrush, _rects[i + 3], FormatStyle.MFormat);
                }
            }

            #endregion
            #region//画制动实际值的显示数字

            else if (BoolList[_boolIds[1]])
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[_foolatIds[9 + i]].ToString("0.0"), FormatStyle.Font10B, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
                }
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[_foolatIds[21 + i]].ToString("0.0"), FormatStyle.Font10B, FormatStyle.WhiteBrush, _rects[i + 3], FormatStyle.MFormat);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    e.DrawString(0.ToString("0.0"), FormatStyle.Font10B, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
                }
            }

            #endregion
            #region//画转向架的数字
            for (int i = 1; i < 7; i++)
            {
                e.DrawString(i.ToString(CultureInfo.InvariantCulture), FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[i + 5], FormatStyle.MFormat);
            }
            e.DrawString("转向架1", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[12], FormatStyle.MFormat);
            e.DrawString("转向架2", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[13], FormatStyle.MFormat);


            #endregion

        }
        /// <summary>
        /// 画图标 3.03=矩形框高度除以刻度最大值
        /// </summary>
        /// <param name="g">画图对象</param>
        /// <param name="valueIndex">需要画的值的索引</param>
        /// <param name="locationIndex">需要画的位置</param>
        /// <param name="IconIndex">图片索引</param>
        private void DrawImag(Graphics g, int valueIndex, int locationIndex, int IconIndex)
        {
            g.DrawImage(_images[IconIndex], _rectangles[locationIndex + 9].X, _rectangles[locationIndex + 9].Y - FloatList[_foolatIds[valueIndex]] * 3.03f, _rectangles[locationIndex + 9].Width,
                  _rectangles[locationIndex + 9].Height);
        }
        /// <summary>
        /// 填充变化的矩形框
        /// </summary>
        /// <param name="e">绘图对象</param>
        /// <param name="index">实际百分比的值的索引</param>
        /// <param name="Ctype">类型</param>
        /// <param name="recIndex">绘制区域索引</param>
        private void DrawRectangleChange(Graphics e,int index,int Ctype,int recIndex)
        {
            float value = Judge(index,Ctype,108);
            _changeLengths[recIndex].DrawRectangle(e, ref value, Decide(Ctype));
        }
        /// <summary>
        /// 填充的百分比
        /// </summary>
        /// <param name="index">填充百分比索引</param>
        /// <param name="type">类型</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        private float Judge(int index, int type, float maxValue)
        {
            float fValue = 0f;
            switch (type)
            {
                case 0:
                    fValue = FloatList[_foolatIds[index]] > maxValue ? maxValue : FloatList[_foolatIds[index]];
                    break;
                case 1:
                    fValue = FloatList[_foolatIds[index]] > maxValue ? maxValue : FloatList[_foolatIds[index]];
                    break;
            }
            return fValue;
        }
        /// <summary>
        /// 牵引和制动不同填充颜色的不同
        /// </summary>
        /// <param name="type">判断是牵引还是制动</param>
        /// <returns></returns>
        private SolidBrush Decide(int type)//type用来判断是牵引还是制动
        {
            if (type == 0)
            {
                return FormatStyle.BlueBrush;
            }
            return type == 1 ? FormatStyle.RedBrush : FormatStyle.BlackBrush;
        }
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawOn(Graphics e)
        {
            DrawRects(e);
            DrawChracter(e);
            FillRect(e);
            DrawImg(e);

        }
        /// <summary>
        /// 数据初始话
        /// </summary>
        private void InitData()
        {
            #region 对象的初始画
            _images = new List<Image>();
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _rectangles = new List<Rectangle>();
            _rects = new List<Rectangle>();
            _changeLengths = new List<NeedChangeLength>();
            #endregion
            #region 从配置表里取出数据
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _images.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    _boolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    _foolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            #endregion
            #region //列表赋值
            _rectangles.Add(new Rectangle(10, 40, 441, 363));
            //一架矩形框位置
            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(58 + i * 45, 63, 40, 327));
            }
            //二架矩形框的位置
            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(290 + i * 45, 63, 40, 327));
            }
            //架1，架2，刻度的位置
            _rectangles.Add(new Rectangle(7, 63, 49, 335));
            _rectangles.Add(new Rectangle(239, 63, 49, 335));
            //架1图标的位置
            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(81 + i * 45, 384, 18, 13));
            }

            //架2的图标位置
            for (int i = 0; i < 3; i++)
            {
                _rectangles.Add(new Rectangle(313 + i * 45, 384, 18, 13));
            }
            //矩形框上面的字位置
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(63 + i * 45, 40, 40, 30));
            }
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(297 + i * 45, 40, 40, 30));
            }
            //矩形框下面的文字位置
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(68 + i * 45, 391, 40, 30));
            }
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(302 + i * 45, 391, 40, 30));
            }
            //转向架1的文字位置
            _rects.Add(new Rectangle(25, 399, 40, 30));
            //转向架1的文字位置
            _rects.Add(new Rectangle(264, 399, 40, 30));
            for (int i = 0; i < 3; i++)
            {
                _changeLengths.Add(new NeedChangeLength(new Rectangle(59 + i * 46, 65, 38, 326), 108, Rect_Rise_Direction.上, false));
            }
            for (int i = 0; i < 3; i++)
            {
                _changeLengths.Add(new NeedChangeLength(new Rectangle(291 + i * 46, 65, 38, 326), 108, Rect_Rise_Direction.上, false));
            }

            #endregion

        }


    }
}
