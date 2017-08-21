using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1D.控制设置
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlSeting : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        ///<summary>
        ///bool逻辑号
        /// </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;
        /// <summary>
        /// 视图19矩形框
        /// </summary>
        private List<Rectangle> _rects;

        /// <summary>
        /// 视图20矩形框
        /// </summary>
        private List<Rectangle> _list;
        /// <summary>
        /// 视图21的矩形框
        /// </summary>
        private List<Rectangle> _rectangles;
        /// <summary>
        /// 视图22的矩形框
        /// </summary>
        private List<Rectangle> _rectslists;
        /// <summary>
        /// 列表矩形框
        /// </summary>
        private List<Rectangle> _btnList;

        /// <summary>
        /// 值变动号
        /// </summary>
        public static int _rowid = 0;

        public static int RunModel_Normal = 1;
        public static int RunModel_Urgency = 0;

        public override string GetInfo()
        {
            return "控制设置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            InitData();
            return true;
        }

        //载重设置界面使用值
        public static Dictionary<int, int> DisplayDictionary = new Dictionary<int, int>();
        public string DeafultValue = "21";
        //联挂设置界面显示值
        public static int DisplayValue = 3;

        public override void paint(Graphics dcGs)
        {
            if (Title.CurentView != 19)
            {
                //Title.ContentDictionary.Clear();
                _rowid = 0;
            }
            JudgeView(dcGs, Title.CurentView);
            base.paint(dcGs);
        }

        #region 视图19的方法
        /// <summary>
        /// 画视图19的文字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawChacter(Graphics e)
        {
            //提示文字
            e.DrawString(FormatStyle.str19[0], FormatStyle.Font12, FormatStyle.GreenBrush, _rects[0], FormatStyle.MFormat);
            //列车重量文字
            e.DrawString(FormatStyle.str19[1], FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[21], FormatStyle.MFormat);
            //列车重量范围文字
            e.DrawString(FormatStyle.str19[2], FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[22], FormatStyle.MFormat);
            //第一个矩形框使用值文字
            e.DrawString(FormatStyle.str19[3], FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[23], FormatStyle.MFormat);
            //列车类型，客车=1
            e.DrawString(FormatStyle.str19[4], FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[24], FormatStyle.MFormat);
            //使用值
            e.DrawString(FormatStyle.str19[5], FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[25], FormatStyle.MFormat);
            //列车铀重1=21t
            e.DrawString(FormatStyle.str19[6], FormatStyle.Font14, FormatStyle.WhiteBrush, _rects[26], FormatStyle.MFormat);
            //使用值
            e.DrawString(FormatStyle.str19[7], FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[27], FormatStyle.MFormat);
            //单位值 t
            e.DrawString(FormatStyle.str19[8], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[28], FormatStyle.MFormat);
            e.DrawString(FormatStyle.str19[9], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[29], FormatStyle.MFormat);

            //画方框里的值
            foreach (var i in Title.ContentDictionary.Keys)
            {
                e.DrawString(Title.ContentDictionary[i].ToString(CultureInfo.InvariantCulture), FormatStyle.Font10, FormatStyle.WhiteBrush,
                    _btnList[i], FormatStyle.MFormat);
            }
            foreach (var i in DisplayDictionary.Keys)
            {
                e.DrawString(DisplayDictionary[i].ToString(CultureInfo.InvariantCulture), FormatStyle.Font10, FormatStyle.WhiteBrush,
                   _btnList[7 + i], FormatStyle.MFormat);
            }


        }
        /// <summary>
        /// 画图19的矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawRects(Graphics e)
        {
            //第一个大矩形框
            e.DrawRectangle(FormatStyle.WhitePen, _rects[1]);
            //使用之外面的矩形框
            e.DrawRectangle(FormatStyle.WhitePen, _rects[2]);
            //使用值里面的小矩形框
            for (int i = 0; i < 10; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen1, _rects[3 + i]);
            }
            //列车类型，列车铀重的矩形框里面的4个小矩形框
            for (int i = 0; i < 4; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen1, _rects[13 + i]);
            }
            //列车类型，列车铀重的矩形框
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[17 + i]);
            }
            //列车类型，列车铀重的矩形框里面的小矩形框
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[19 + i]);
            }


        }

        #endregion

        #region 视图20的方法
        private void DrawView20Chacter(Graphics e)
        {
            e.DrawString(FormatStyle.str20[0], FormatStyle.Font12, FormatStyle.GreenBrush, _list[0], FormatStyle.MFormat);
            e.DrawString(FormatStyle.str20[1], FormatStyle.Font14, FormatStyle.WhiteBrush, _list[6], FormatStyle.MFormat);
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(FormatStyle.str20[2 + i], FormatStyle.Font10, FormatStyle.WhiteBrush, _list[7 + i], FormatStyle.MFormat);
            }
            if (Title.Current != -1)
            {
                e.DrawString(Title.Current.ToString(CultureInfo.InvariantCulture), FormatStyle.Font10, FormatStyle.WhiteBrush, _list[5], FormatStyle.MFormat);
            }
            if (DisplayValue != -1)
            {
                e.DrawString("3", FormatStyle.Font10, FormatStyle.WhiteBrush, _list[4], FormatStyle.MFormat);
            }
        }

        private void DrawrView20Rects(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _list[1 + i]);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen1, _list[4 + i]);
            }

        }

        private void FillView20Rects(Graphics e)
        {
            e.FillRectangle(FormatStyle.BlueBrush, _list[5]);
        }

        #endregion

        #region 视图21的方法

        /// <summary>
        /// 画视图21的文字2
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawView21Chacter(Graphics e)
        {
            e.DrawString(FormatStyle.str21[0], FormatStyle.Font14, FormatStyle.WhiteBrush, _rectangles[0], FormatStyle.MFormat);
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(FormatStyle.str21[i + 1], FormatStyle.Font14, FormatStyle.WhiteBrush, _rectangles[i + 4], FormatStyle.MFormat);
            }

        }
        /// <summary>
        /// 画视图21的矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawView21Rects(Graphics e)
        {
            e.DrawRectangle(FormatStyle.WhitePen, _rectangles[1]);

        }

        private void DrawView21Img(Graphics e)
        {

            if (RunModel_Urgency==1)
            {
                for (int i = 0; i < 2; i++)
                {
                    e.DrawImage(_imgs[1 - i], _rectangles[2 + i]);
                }
                RunModel_Normal = 0;
                RunModel_Urgency = 1;
            }
            else if (RunModel_Normal==1)
            {
                for (int i = 0; i < 2; i++)
                {
                    e.DrawImage(_imgs[0 + i], _rectangles[2 + i]);
                }
                RunModel_Normal = 1;
                RunModel_Urgency = 0;
            }

            //else
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        e.DrawImage(_imgs[0 + i], _rectangles[2 + i]);
            //    }
            //}
        }

        #endregion

        #region 视图22的方法

        private void DrawView22Rects(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rectslists[i]);
            }

        }

        private void DrawView22Chacter(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(FormatStyle.str22[i], FormatStyle.Font14, FormatStyle.WhiteBrush, _rectslists[i + 3], FormatStyle.MFormat);
            }
            for (int i = 0; i < Title.ContentLists.Count; i++)
            {

                e.DrawString(Title.ContentLists[i].ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.WhiteBrush, _rectslists[i + 6], FormatStyle.MFormat);
            }
            e.DrawString(DeafultValue, FormatStyle.Font12, FormatStyle.WhiteBrush, _rectslists[1], FormatStyle.MFormat);
        }

        private void FillView22Rects(Graphics e)
        {
            e.FillRectangle(FormatStyle.BlueBrush, _rectslists[2]);
        }

        #endregion

        private void FillRects(Graphics e)
        {
            if (_rowid >= 0 && _rowid < 7)
            {
                e.FillRectangle(FormatStyle.BlueBrush, _btnList[_rowid]);
            }
        }

        public static string PointOut = "";
        /// <summary>
        /// 判断视图
        /// </summary>
        /// <param name="e">画图对象</param>
        /// <param name="view">视图</param>    
        private void JudgeView(Graphics e, int view)
        {
            switch (view)
            {
                case 19:
                    DrawRects(e);
                    FillRects(e);
                    DrawChacter(e);
                    drawPointOut(e);
                    break;
                case 20:
                    DrawrView20Rects(e);
                    FillView20Rects(e);
                    DrawView20Chacter(e);
                    drawPointOut(e);
                    break;
                case 21:
                    DrawView21Chacter(e);
                    DrawView21Rects(e);
                    DrawView21Img(e);
                    drawPointOut(e);
                    break;
                case 22:
                    FillView22Rects(e);
                    DrawView22Rects(e);
                    DrawView22Chacter(e);
                    break;
            }

        }

        private void drawPointOut(Graphics dcGs)
        {
            dcGs.DrawString(
                PointOut,
                FormatStyle.Font12,
                FormatStyle.GreenBrush,
                new RectangleF(270,482,180,30),
                new StringFormat(){ Alignment= StringAlignment.Center, LineAlignment= StringAlignment.Center}
                );
        }

        private void InitData()
        {
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _list = new List<Rectangle>();
            _rectangles = new List<Rectangle>();
            _rectslists = new List<Rectangle>();
            _btnList = new List<Rectangle>();

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

            #region 视图19列表的赋值

            //载重设置界面提示
            _rects.Add(new Rectangle(215, 155, 315, 46));
            //载重设置界面第一个矩形框
            _rects.Add(new Rectangle(69, 200, 568, 108));
            //使用值外部的矩形框
            _rects.Add(new Rectangle(317, 212, 290, 41));
            //使用值里面的10个小矩形框
            for (int i = 0; i < 5; i++)
            {
                _rects.Add(new Rectangle(412 + i * 28, 218, 19, 29));

            }
            for (int i = 0; i < 5; i++)
            {
                _rects.Add(new Rectangle(412 + i * 28, 266, 19, 29));
            }
            //列车类型，列车铀重变动的小矩形框
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rects.Add(new Rectangle(295 + i * 280, 345 + j * 50, 20, 32));
                }

            }
            //下面两个大的矩形框
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(69 + i * 288, 319, 286, 132));

            }
            //列车类型，列车铀重里的小矩形框
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(200 + i * 281, 341, 136, 40));
            }

            //列车重量文字位置的位置
            _rects.Add(new Rectangle(124, 226, 87, 26));
            //列车重量的范围
            _rects.Add(new Rectangle(124, 266, 87, 26));
            //使用值文字的位置
            _rects.Add(new Rectangle(331, 225, 48, 19));
            //列车类型，客车=1
            _rects.Add(new Rectangle(85, 332, 90, 57));
            //使用值
            _rects.Add(new Rectangle(214, 352, 50, 25));
            //列车铀重1=21t
            _rects.Add(new Rectangle(375, 329, 87, 53));
            //使用值
            _rects.Add(new Rectangle(470, 349, 118, 35));
            //t单位值的位置
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(546, 218 + i * 56, 17, 31));
            }
            for (int i = 0; i < 5; i++)
            {
                _btnList.Add(_rects[8 + i]);
            }

            _btnList.Add(_rects[14]);
            _btnList.Add(_rects[16]);
            for (int i = 0; i < 5; i++)
            {
                _btnList.Add(_rects[3 + i]);
            }
            _btnList.Add(_rects[13]);
            _btnList.Add(_rects[15]);




            #endregion

            #region 视图20列表的赋值
            //提示的文字
            _list.Add(new Rectangle(227, 132, 276, 31));
            //矩形框
            _list.Add(new Rectangle(88, 210, 554, 190));
            //矩形框里的小矩形框
            for (int i = 0; i < 2; i++)
            {
                _list.Add(new Rectangle(217, 288 + i * 58, 250, 36));

            }
            //矩形框里最小的矩形框
            for (int i = 0; i < 2; i++)
            {
                _list.Add(new Rectangle(381, 291 + i * 58, 21, 31));

            }
            //机车联挂速度范围
            _list.Add(new Rectangle(293, 216, 169, 56));
            //使用值
            _list.Add(new Rectangle(293, 294, 56, 22));
            //设置值
            _list.Add(new Rectangle(293, 351, 56, 22));
            //km/h
            for (int i = 0; i < 2; i++)
            {
                _list.Add(new Rectangle(410, 305 + i * 58, 56, 22));
            }


            #endregion

            #region 视图21列表的赋值

            //视图21的提示
            _rectangles.Add(new Rectangle(188, 124, 336, 64));

            //外面矩形框
            _rectangles.Add(new Rectangle(66, 220, 568, 119));
            //
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(201 + i * 209, 279, 41, 41));
            }
            //模式选中
            _rectangles.Add(new Rectangle(319, 242, 89, 34));
            //正常模式
            _rectangles.Add(new Rectangle(292, 289, 89, 34));
            //应急模式
            _rectangles.Add(new Rectangle(467, 289, 89, 34));
            #endregion

            #region 视图22列表的赋值
            //视图22外面的矩形框
            _rectslists.Add(new Rectangle(110, 92, 510, 310));
            //
            for (int i = 0; i < 2; i++)
            {
                _rectslists.Add(new Rectangle(365, 158 + i * 112, 109, 30));
            }
            //
            for (int i = 0; i < 2; i++)
            {
                _rectslists.Add(new Rectangle(260, 153 + i * 116, 71, 36));
            }
            //
            _rectslists.Add(new Rectangle(223, 328, 330, 39));
            for (int i = 0; i < 3; i++)
            {
                _rectslists.Add(new Rectangle(372 + i * 10, 280, 10, 28));
            }


            #endregion
        }

    }
}
