using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1D.Run
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Run : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        /// <summary>
        ///文字列表
        /// </summary>
        private List<Rectangle> _list;

        /// <summary>
        /// 矩形框
        /// </summary>
        private List<Rectangle> _rects;

        /// <summary>
        /// 点列表
        /// </summary>
        private List<Point> _points;

        /// <summary>
        /// 原边电流，原边电压，网压，本机的最大值
        /// </summary>
        private float[] _theRectangleF;

        /// <summary>
        /// 矩形框刻度变化的列表对象
        /// </summary>
        private List<NeedChangeLength> needChangeLengths;

        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;

        /// <summary>
        /// 绘制主界面
        /// </summary>
        private bool drawMainView = true;

        /// <summary>
        /// 
        /// </summary>
        private List<Region> _rectangle;

        private List<Rectangle> _faultRects = new List<Rectangle>();

        private List<Rectangle> _rowRcts = new List<Rectangle>();

        private Dictionary<int, string> _pointDic = new Dictionary<int, string>();

        private Dictionary<int, string> _promptDic = new Dictionary<int, string>();

        private ReadConfigcs read = new ReadConfigcs();

        public override string GetInfo()
        {
            return "运行";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            read.ReaFile("提示信息", _pointDic);

            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            DrawOn(dcGs);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            drawMainView = (nParaB == 1);
        }
        /// <summary>
        /// 增加速度，减少速度，按钮按下发送
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < _rectangle.Count(); index++)
            {
                if (_rectangle[index].IsVisible(nPoint))
                    break;


            }
            switch (index)
            {
                case 0:
                    append_postCmd(CmdType.SetBoolValue, 849, 1, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, 850, 1, 0);
                    break;
                default:
                    break;
            }


            return base.mouseDown(nPoint);
        }
        /// <summary>
        /// 增加速度，减少速度，按钮弹起不发送
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < _rectangle.Count(); index++)
            {
                if (_rectangle[index].IsVisible(nPoint))
                    break;

            }
            switch (index)
            {
                case 0:
                    append_postCmd(CmdType.SetBoolValue, 849, 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.SetBoolValue, 850, 0, 0);

                    break;
                default:
                    break;
            }

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 画图片
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImg(Graphics e)
        {
            //画主断断开且已经缓解，主断闭合，主断死锁，主断断开但没有缓解
            if (BoolList[_boolIds[0]])
            {
                e.DrawImage(_imgs[9], _rects[13]);
            }
            else if (BoolList[_boolIds[1]])
            {
                e.DrawImage(_imgs[10], _rects[13]);
            }
            else if (BoolList[_boolIds[2]])
            {
                e.DrawImage(_imgs[11], _rects[13]);
            }
            else if (BoolList[_boolIds[3]])
            {
                e.DrawImage(_imgs[8], _rects[13]);
            }

            //机车方向
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[4 + i]])
                {
                    e.DrawImage(_imgs[12 + i], _rects[11]);
                }
            }


            //受电功的状态显示
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[7 + i * 2]])
                {
                    e.DrawImage(_imgs[15 + i], _rects[12]);

                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[8 + i * 2]])
                {
                    e.DrawImage(_imgs[15 + i], _rects[21]);

                }
            }


            //停放制动
            if (BoolList[_boolIds[16]])
            {
                e.DrawImage(_imgs[22], _rects[24]);
            }
            else if (BoolList[_boolIds[17]])
            {
                e.DrawImage(_imgs[21], _rects[24]);
            }
            else if (BoolList[_boolIds[38]])
            {
                e.DrawImage(_imgs[42], _rects[24]);
            }
            //牵引运行，电制动运行
            if (BoolList[_boolIds[30]] || BoolList[_boolIds[31]])
            {
                e.DrawImage(_imgs[37], _rects[14]);
            }
            //电机未运行
            if (BoolList[_boolIds[40]])
            {
                e.DrawImage(_imgs[44], _rects[14]);
            }

            //警惕装置
            if (BoolList[_boolIds[18]])
            {
                e.DrawImage(_imgs[23], _rects[26]);
            }
            else if (BoolList[_boolIds[19]])
            {
                e.DrawImage(_imgs[24], _rects[26]);
            }

            //机车制动
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[20 + i]])
                {
                    e.DrawImage(_imgs[25 + i], _rects[23]);
                }

            }


            //紧急制动
            if (BoolList[_boolIds[23]])
            {
                e.DrawImage(_imgs[28], _rects[25]);
            }
            //空转、滑行，故障
            if (BoolList[_boolIds[29]])
            {
                e.DrawImage(_imgs[38], _rects[15]);
            }
            else if (BoolList[_boolIds[32]])
            {
                e.DrawImage(_imgs[39], _rects[15]);
            }
            else if (BoolList[_boolIds[37]])
            {
                e.DrawImage(_imgs[41], _rects[15]);
            }
            else if (BoolList[_boolIds[39]])
            {
                e.DrawImage(_imgs[43], _rects[15]);
            }



            //分相区，撒沙
            if (BoolList[_boolIds[24]])
                e.DrawImage(_imgs[30], _rects[16]);
            if (BoolList[_boolIds[25]])
                e.DrawImage(_imgs[29], _rects[20]);
            if (BoolList[_boolIds[26]])
                e.DrawImage(_imgs[31], _rects[17]);
            //+1Km，-1km
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(_imgs[6 + i], _rects[37 + i]);
            }
            //惩罚制动
            if (BoolList[_boolIds[33]])
            {
                e.DrawImage(_imgs[40], _rects[22]);
            }

        }

        /// <summary>
        /// 画文字
        /// </summary>
        private void DrawCharacters(Graphics e)
        {
            for (int i = 0; i < FormatStyle.str3.Count(); i++)
            {
                e.DrawString(FormatStyle.str3[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _list[36 + i], FormatStyle.MFormat);
            }
            //制动 级位显示
            if (BoolList[_boolIds[27]])
            {
                e.FillRectangle(FormatStyle.BlueBrush, _rects[5]);
                if (FloatList[_foolatIds[4]] > 0)
                {
                    e.DrawString(FloatList[_foolatIds[4]].ToString("0.0"), FormatStyle.Font12, FormatStyle.WhiteBrush,
                   _rects[5], FormatStyle.MFormat);
                }
                else
                {
                    e.DrawString("--", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[5], FormatStyle.MFormat);
                }


            }
            else if (BoolList[_boolIds[28]])
            {
                e.FillRectangle(FormatStyle.RedBrush, _rects[5]);
                if (FloatList[_foolatIds[4]] > 0)
                {
                    e.DrawString(FloatList[_foolatIds[4]].ToString("0.0"), FormatStyle.Font12, FormatStyle.WhiteBrush,
                   _rects[5], FormatStyle.MFormat);
                }
                else
                {
                    e.DrawString("--", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[5], FormatStyle.MFormat);
                }
            }
            else
            {
                e.DrawString("--", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[5], FormatStyle.MFormat);
            }
            //设定速度
            e.DrawString(FloatList[_foolatIds[5]] > 0 ? FloatList[_foolatIds[5]].ToString("0") : "--",
                FormatStyle.Font10,
                FormatStyle.WhiteBrush,
                _rects[6],
                FormatStyle.MFormat);

            //列供电  电压 、电流
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(Convert.ToInt32(FloatList[_foolatIds[6 + i]]).ToString(CultureInfo.InvariantCulture), FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[7 + i], FormatStyle.MFormat);
            }
            //写受电弓1，2，文字的位置
            for (int i = 0; i < 2; i++)
            {
                e.DrawString((i + 1).ToString(), FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[35 + i], FormatStyle.MFormat);
            }


        }
        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < _rects.Count - 12; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[i]);
            }

        }
        /// <summary>
        /// 填充故障，提示
        /// </summary>
        /// <param name="e">画图对象</param>
        private void Fillrect(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.RedBrush, _rects[3 + i]);
            }

        }
        /// <summary>
        /// 画故障内容
        /// </summary>
        /// <param name="e"></param>
        private void DrawWords(Graphics e)
        {

            try
            {

                for (int i = 0; i < Title.MsgInfList.CurrentMsgList.Count && i < 5; i++)
                {

                    e.DrawString(Title.MsgInfList.CurrentMsgList[i].MsgContent, FormatStyle.Font12, FormatStyle.WhiteBrush,
                        _faultRects[i]);

                }

                for (int i = 0; i < _pointDic.Count; i++)
                {
                    if (BoolList[_pointDic.Keys.Min() + i] && !_promptDic.ContainsKey(_pointDic.Keys.Min() + i))
                    {
                        _promptDic.Add(_pointDic.Keys.Min() + i, _pointDic[_pointDic.Keys.Min() + i]);
                    }
                    if (!BoolList[_pointDic.Keys.Min() + i])
                    {
                        _promptDic.Remove(_pointDic.Keys.Min() + i);
                    }
                }


                RowCount rc = new RowCount(_rowRcts, _promptDic, BoolList);

                rc.Count(e);




            }
            catch (System.Exception ex)
            {

            }

        }

        /// <summary>
        /// 填充矩形框
        /// </summary>
        /// <param name="e"></param>
        private void FillRect(Graphics e)
        {
            //填充原边电压
            float temp2;
            temp2 = FloatList[_foolatIds[0]];
            if (0 <= temp2 && temp2 <= 32)
            {

                needChangeLengths[0].DrawRectangle(e, ref temp2, ChangeColor(FloatList[_foolatIds[0]], 0));
            }
            if (temp2 > 32)//当超过最大值的时候只能为最大值
            {
                temp2 = 32;
                //FloatList[_foolatIds[0]] = 32;
                needChangeLengths[0].DrawRectangle(e, ref temp2, ChangeColor(temp2, 0));
            }

            //原边电流
            float temp;
            temp = FloatList[_foolatIds[1]];
            if (0 <= temp && temp <= 660)
            {
                needChangeLengths[1].DrawRectangle(e, ref temp, ChangeColor(FloatList[_foolatIds[1]], 1));
            }
            if (temp > 660)//当超过最大值的时候只能为最大值
            {
                temp = 660;
               // FloatList[_foolatIds[1]] = 660;
                needChangeLengths[1].DrawRectangle(e, ref temp, ChangeColor(temp, 1));
            }

            //画控制电压随着值变化而变化
            float temp1;
            temp1 = FloatList[_foolatIds[2]];
            if (0 <= temp1 && temp1 <= 120)
            {
                needChangeLengths[2].DrawRectangle(e, ref temp1, FormatStyle.YellowBrush);
            }
            if (temp1 > 120)//当超过最大值的时候只能为最大值
            {
                temp1 = 120;
              //  FloatList[_foolatIds[2]] = 120;
                needChangeLengths[2].DrawRectangle(e, ref temp1, FormatStyle.YellowBrush);
            }
            //本机牵引和制动的实际总值





        }
        /// <summary>
        /// 判断是牵引还是制动的实际总值
        /// </summary>
        /// <param name="e">画图对象</param>
        /// <param name="Index">传递给数字量的下标值</param>
        /// <param name="CType">判断是牵引还是制动</param>
        private void DrawChange(Graphics e, int Index, int CType)
        {
            SolidBrush brush = Decide(e, CType);
            float value = JudgeIndex(CType) < 108 ? JudgeIndex(CType) : 108;
            switch (CType)
            {
                case 0:
                    needChangeLengths[3].DrawRectangleType(e, ref value, brush);
                    break;
                case 1:
                    needChangeLengths[4].DrawRectangleType(e, ref value, brush);
                    break;
            }

        }

        private float JudgeIndex(int type1)
        {
            float fValue = type1 == 1 ? FloatList[_foolatIds[14]] : FloatList[_foolatIds[13]];
            return fValue;
            //  DragCalcuate _drag = new DragCalcuate(FloatList[_foolatIds[index]], FloatList[_foolatIds[index]],type1);
            //return _drag.Calucate();
        }


        /// <summary>
        /// 用来判断是牵引还是制动
        /// </summary>
        /// <param name="e">画图对象</param>
        /// <param name="type">判断是牵引还是制动的类型</param>
        /// <returns></returns>
        private SolidBrush Decide(Graphics e, int type)
        {
            if (type == 0 && BoolList[_boolIds[27]])
            {

                return FormatStyle.BlueBrush;

            }
            if (type == 1 && BoolList[_boolIds[28]])
            {

                return FormatStyle.RedBrush;

            }

            return FormatStyle.BlackBrush;
        }

        /// <summary>
        /// 画原边电流的警惕值的标志
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawLine(Graphics e)
        {
            e.DrawLine(FormatStyle.RedPen, 54, 207, 85, 207);//电压过低提示线
            e.DrawLine(FormatStyle.BluePen, 54, 194, 85, 194);//电压偏低提示线
            e.DrawLine(FormatStyle.YelloPen, 54, 159, 85, 159);//电压偏高
            e.DrawLine(FormatStyle.RedPen, 54, 77, 85, 77);//电压过高
            e.DrawLine(FormatStyle.YelloPen, 54, 87, 85, 87);//电压偏低提示线
        }

        /// <summary>
        /// 设定的值不同而颜色改变
        /// </summary>
        /// <param name="ft">变化的值</param>
        /// <param name="type">矩形框的种类</param>
        /// <returns></returns>
        private SolidBrush ChangeColor(float ft, int type)
        {
            switch (type)
            {
                case 0://原边电压
                    if (0 < ft && ft <= 17.5 || 31 < ft && ft <= 32)
                    {
                        return FormatStyle.RedBrush;
                    }
                    if (17.5 < ft && ft <= 19)
                    {
                        return FormatStyle.BlueBrush;
                    }
                    if (19 < ft && ft <= 22.5 || 30 < ft && ft <= 31)
                    {
                        return FormatStyle.YellowBrush;
                    }
                    if (22.5 < ft && ft <= 30)
                    {
                        return FormatStyle.GreenBrush;
                    }


                    break;
                case 1://原边电流
                    return ft <= 600 ? FormatStyle.YellowBrush : FormatStyle.BlueBrush;
            }

            return FormatStyle.BlackBrush;

        }


        private void DrawImag(Graphics g, int index, int locationIndex, int imgindx)
        {
            g.DrawImage(_imgs[imgindx], _rects[locationIndex].X, _rects[locationIndex].Y - (FloatList[_foolatIds[index]] * 2.824f<=304.992F?FloatList[_foolatIds[index]] * 2.824f:304.992F), _rects[locationIndex].Width,
                 _rects[locationIndex].Height);
        }
        /// <summary>
        /// 判断哪些矩形框和文字画在视图1上
        /// </summary>
        /// <param name="e"></param>
        private void Judge(Graphics e)
        {
            //DragCalcuate _drag = new DragCalcuate(FloatList[_foolatIds[index1]], FloatList[_foolatIds[index2]], type);
            #region //原边电流，原边电压，控制电压，本机。上面变动的数字

            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                {
                    e.DrawString(FloatList[_foolatIds[0 + i]].ToString("0.0"), FormatStyle.Font12, FormatStyle.WhiteBrush, _list[i], FormatStyle.MFormat);
                }

            }
            if (BoolList[_boolIds[27]])//本机牵引的实际值
            {

                e.DrawString(FloatList[_foolatIds[10]].ToString("0.0"), FormatStyle.Font12, FormatStyle.WhiteBrush, _list[3], FormatStyle.MFormat);
            }
            else if (BoolList[_boolIds[28]])//本机制动的实际值
            {

                e.DrawString(FloatList[_foolatIds[12]].ToString("0.0"), FormatStyle.Font12, FormatStyle.WhiteBrush, _list[3], FormatStyle.MFormat);
            }
            else
            {
                e.DrawString("0", FormatStyle.Font12, FormatStyle.WhiteBrush, _list[3], FormatStyle.MFormat);
            }
            e.DrawString(FloatList[_foolatIds[2]].ToString("0.0"), FormatStyle.Font12, FormatStyle.RedBrush, _list[2], FormatStyle.MFormat);

            #endregion            //网压，原边电流上面的值

            #region //视图1上的文字


            for (int i = 0; i < FormatStyle.str7.Count(); i++)
            {

                e.DrawString(FormatStyle.str7[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _list[i + 4], FormatStyle.RightFormat);
            }
            for (int i = 0; i < FormatStyle.str2.Count(); i++)
            {
                e.DrawString(FormatStyle.str2[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _list[i + 32], FormatStyle.MFormat);
            }
            #endregion

            #region 视图1，本机给定值图片移动的位置

            if (BoolList[_boolIds[27]])
            {
                DrawImag(e, 3, 34, FindImgClass.FindImgIndex(0, 3, 13, this, _foolatIds) == 2 ? 33 : 34);
            }
            else if (BoolList[_boolIds[28]])
            {
                DrawImag(e, 11, 34, FindImgClass.FindImgIndex(1, 11, 14, this, _foolatIds) == 4 ? 35 : 36);
            }
            else
            {
                e.DrawImage(_imgs[5], _rects[34]);
            }



            #endregion

            #region 画刻度值，填冲矩形，原边电流警惕值的标志
            e.DrawImage(_imgs[32], _rects[33]);//右下角牵引/制动的图片标志
            FillRect(e);
            DrawLine(e);
            //305f / 32刻度比例值
            Scale(e, _points[0], _points[1], 305f / 32, 1, 32);
            Scale(e, _points[2], _points[3], 305f / 30, 1, 30);
            Scale(e, _points[4], _points[5], 305f / 27, 1, 27);
            Scale(e, _points[6], _points[7], 305f / 32, 2, 32);
            for (int i = 0; i < 4; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[29 + i]);
            }
            #endregion
        }

        /// <summary>
        /// 定以刻度值的方法
        /// </summary>
        /// <param name="e">画图对象</param>
        /// <param name="p1">画直线第一个坐标</param>
        /// <param name="p2">画直线第二个坐标</param>
        /// <param name="hight">两个坐标间接高度</param>
        /// <param name="type">画刻度值的类型</param>
        /// <param name="Max">刻度值的最大值</param>
        private void Scale(Graphics e, Point p1, Point p2, float hight, int type, int Max)
        {
            if (type == 1)
            {
                for (int i = 0; i <= Max; i++)
                {
                    if (i % 5 == 0)
                    {
                        e.DrawLine(FormatStyle.WhitePen, p1.X - 4, p1.Y - hight * i, p2.X, p2.Y - hight * i);
                    }
                    else
                    {
                        e.DrawLine(FormatStyle.WhitePen, p1.X, p1.Y - hight * i, p2.X, p2.Y - hight * i);
                    }
                    e.DrawLine(FormatStyle.WhitePen, p2.X, p2.Y - hight * Max, p2.X, p2.Y);

                }

            }
            if (type == 2)
            {
                for (int i = 0; i <= Max; i++)
                {
                    if (i % 5 == 0 && i % 10 != 0)
                    {
                        e.DrawLine(FormatStyle.WhitePen, p1.X - 3, p1.Y - hight * i, p2.X, p2.Y - hight * i);
                    }
                    else if (i % 10 == 0)
                    {
                        e.DrawLine(FormatStyle.WhitePen, p1.X - 5, p1.Y - hight * i, p2.X, p2.Y - hight * i);
                    }
                    else
                    {
                        e.DrawLine(FormatStyle.WhitePen, p1.X, p1.Y - hight * i, p2.X, p2.Y - hight * i);
                    }
                    e.DrawLine(FormatStyle.WhitePen, p2.X, p2.Y - hight * 32, p2.X, p2.Y);

                }
            }

        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            if (drawMainView)
            {
                if (BoolList[_boolIds[27]])
                {
                    DrawChange(e, 10, 0);
                }
                else if (BoolList[_boolIds[28]])
                {
                    DrawChange(e, 12, 1);
                }
                Judge(e);
                Judge(e);

            }
            DrawRects(e);
            Fillrect(e);
            DrawImg(e);
            DrawCharacters(e);
            DrawWords(e);

        }

        /// <summary>
        ///数据初始化
        /// </summary>
        private void InitData()
        {
            #region//列表的初始化
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _points = new List<Point>();
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _list = new List<Rectangle>();
            _rectangle = new List<Region>();
            _theRectangleF = new float[5];
            _theRectangleF[0] = 32;
            _theRectangleF[1] = 660;
            _theRectangleF[2] = 120;
            _theRectangleF[3] = 108;
            _theRectangleF[4] = 108;
            needChangeLengths = new List<NeedChangeLength>();
            #endregion

            #region //从配置表取Img，Floatids，Boolids编号
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
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

            #region //坐标的赋值

            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(473 + i * 168, 36, 165, 119));
            }
            _rects.Add(new Rectangle(473, 157, 326, 350));
            //故障信息和提示信息矩形框
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(473, 220 + i * 140, 326, 27));
            }
            //级为位，和设定速度矩形框
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(480, 66 + i * 48, 96, 25));
            }
            //电压电流矩形框
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rects.Add(new Rectangle(559 + i * 133, 161 + j * 28, 62, 21));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _rects.Add(new Rectangle(3 + j * 51, 435 + i * 51, 50, 50));
                }
            }
            //网压，原边电流，控制电压，本机，矩形框
            for (int i = 0; i < 4; i++)
            {
                _rects.Add(new Rectangle(54 + i * 120, 69, 33, 305));

            }
            //牵引-制动图片文字
            _rects.Add(new Rectangle(396, 410, 67, 20));
            //移动图标的初始位置
            _rects.Add(new Rectangle(429, 368, 19, 13));
            //受电弓1，2，字的位置。
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(88, 464 + i * 50, 16, 20));
            }
            //+1Km，-1km,位置
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(745, 40 + i * 69, 52, 44));
            }
            //故障内容的位置
            _rects.Add(new Rectangle(474, 247, 325, 110));
            //提示的位置
            _rects.Add(new Rectangle(474, 388, 325, 119));
            //矩形框顶部的值
            for (int i = 0; i < 4; i++)
            {
                _list.Add(new Rectangle(48 + i * 120, 48, 55, 18));
            }
            //网压右边的刻度值文字的位置
            for (int i = 0; i < 7; i++)
            {
                _list.Add(new Rectangle(4, 84 + i * 47, 30, 21));
            }
            //原边电流右边的刻度值文字的位置
            for (int i = 0; i < 4; i++)
            {
                _list.Add(new Rectangle(118, 78 + i * 94, 35, 26));
            }
            //原边电压右边的刻度值文字的位置
            for (int i = 0; i < 7; i++)
            {
                _list.Add(new Rectangle(241, 53 + i * 51, 35, 26));
            }
            //本机右边的刻度值文字的位置
            for (int i = 0; i < 6; i++)
            {
                _list.Add(new Rectangle(363, 78 + i * 56, 35, 26));
            }
            //网压，原边电流，控制电压，本机的右边单位
            _list.Add(new Rectangle(1, 108, 40, 21));
            _list.Add(new Rectangle(118, 108, 40, 21));
            _list.Add(new Rectangle(243, 87, 40, 21));
            _list.Add(new Rectangle(360, 108, 40, 21));
            //网压，原边电流，控制电压，本机下面的文字
            for (int i = 0; i < 4; i++)
            {
                _list.Add(new Rectangle(27 + i * 120, 370, 81, 50));
            }
            //级位
            _list.Add(new Rectangle(496, 48, 61, 20));
            //设定速度
            _list.Add(new Rectangle(496, 97, 81, 20));
            //级，km/h
            for (int i = 0; i < 2; i++)
            {
                _list.Add(new Rectangle(586, 67 + i * 60, 45, 20));
            }
            //调整设定速度
            _list.Add(new Rectangle(652, 94, 106, 20));
            //列供1电压 V,列车2电压，V
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _list.Add(new Rectangle(460 + i * 127, 162 + j * 22, 108, 20));
                }
            }
            //电流，A
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _list.Add(new Rectangle(622 + i * 102, 162 + j * 22, 108, 20));
                }

            }
            //故障信息
            _list.Add(new Rectangle(473, 221, 326, 26));
            //提示信息
            _list.Add(new Rectangle(473, 365, 326, 26));
            //+1Km,_1km
            _list.Add(new Rectangle(678, 51, 40, 26));
            _list.Add(new Rectangle(678, 122, 40, 26));
            //网压画刻度的起点和终点
            _points.Add(new Point(41, 374));
            _points.Add(new Point(49, 374));
            //控制电压画刻度的起点和终点
            _points.Add(new Point(282, 374));
            _points.Add(new Point(290, 374));
            //本机画刻度的起点和终点
            _points.Add(new Point(401, 374));
            _points.Add(new Point(409, 374));
            //原边电流画刻度的起点和终点
            _points.Add(new Point(160, 374));
            _points.Add(new Point(168, 374));
            //原边电压，原边电流，本机，变动值初始化。
            for (int i = 0; i < 4; i++)
            {
                needChangeLengths.Add(new NeedChangeLength(_rects[29 + i], _theRectangleF[i], Rect_Rise_Direction.上, false));
            }
            needChangeLengths.Add(needChangeLengths[3]);
            //增加，减少按钮
            for (int i = 0; i < 2; i++)
            {
                _rectangle.Add(new Region(new Rectangle(745, 40 + i * 69, 52, 44)));
            }

            for (int i = 0; i < 5; i++)
            {
                _faultRects.Add(new Rectangle(474, 247 + i * 22, 325, 110));

                _rowRcts.Add(new Rectangle(474, 388 + i * 23, 325, 119));
            }

            #endregion



        }

    }
}


