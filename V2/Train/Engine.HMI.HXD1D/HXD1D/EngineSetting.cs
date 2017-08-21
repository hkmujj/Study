using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class EngineSetting : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;

        /// <summary>
        /// 视图23矩形框列表
        /// </summary>
        private List<Rectangle> _rects;

        /// <summary>
        /// 视图24矩形框列表
        /// </summary>
        private List<Rectangle> _rectlists;
        //是否设置过分相
        public static bool IsSetFenX;
        public static PartMutuallyType PartMutually;

        public static PartMutuallyType_ PartMutually1 = PartMutuallyType_.None;
        public static PartMutuallyType_ PartMutually2 = PartMutuallyType_.None;
        #region 重载的方法
        public override string GetInfo()
        {
            return "机车设置";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            Judge(Title.CurentView, dcGs);
            base.paint(dcGs);

        }
        public enum PartMutuallyType_
        {
            None,
            Open,
            Close
        }
        public enum PartMutuallyType
        {
            Open,
            Close
        }
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2 && !IsSetFenX)
            {
                IsSetFenX = true;
                PartMutually = PartMutuallyType.Open;
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        #endregion

        #region 视图23的方法

        /// <summary>
        /// 画视图23的文字
        /// </summary>
        /// <param name="e">画图对象</param>

        private void DrawView23Chacter(Graphics e)
        {
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(FormatStyle.str23[1 + i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i + 1],
                    FormatStyle.MFormat);
            }
            //提示的文字
            e.DrawString(FormatStyle.str23[0], FormatStyle.Font14, FormatStyle.GreenBrush, _rects[0],
                   FormatStyle.MFormat);
            //使用值
            e.DrawString(FormatStyle.str23[5], FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[6],
                FormatStyle.MFormat);
            e.DrawString("170m", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[5], FormatStyle.MFormat);

            if (PartMutually == PartMutuallyType.Open)
            {
                e.DrawString("170m", FormatStyle.Font10, FormatStyle.WhiteBrush, _rects[9], FormatStyle.MFormat);
            }



        }

        private void DrawView23Rects(Graphics e)
        {
            //两个大的矩形框
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[i + 7]);
            }
            //第二个矩形框里的小矩形框
            e.DrawRectangle(FormatStyle.WhitePen, _rects[9]);
        }



        private void DrawViewImgs(Graphics e)
        {
            if (PartMutually == PartMutuallyType.Close)
            {
                e.DrawImage(_imgs[1], _rects[10]);
                e.DrawImage(_imgs[0], _rects[11]);
                e.DrawImage(_imgs[1], _rects[12]);
            }
            else
            {

                e.DrawImage(_imgs[0], _rects[10]);
                e.DrawImage(_imgs[1], _rects[11]);
                e.DrawImage(_imgs[0], _rects[12]);
            }

        }
        #endregion

        public static string PointOut = "";
        private void drawPointOut(Graphics dcGs)
        {
            dcGs.DrawString(
                PointOut,
                FormatStyle.Font12,
                FormatStyle.GreenBrush,
                new RectangleF(270, 482, 180, 30),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        #region
        private void DrawView24Imgs(Graphics e)
        {
            e.DrawImage(_imgs[2], _rectlists[0]);
            e.DrawImage(_imgs[2], _rectlists[1]);
            if (BoolList[UIObj.InBoolList[0]])
            {
                PartMutually1 = PartMutuallyType_.Close;
                e.DrawImage(_imgs[3], _rectlists[4]);
            }
            else if (BoolList[UIObj.InBoolList[1]])
            {
                PartMutually1 = PartMutuallyType_.Open;
                e.DrawImage(_imgs[4], _rectlists[4]);
            }
            if (BoolList[UIObj.InBoolList[2]])
            {
                PartMutually2 = PartMutuallyType_.Close;
                e.DrawImage(_imgs[5], _rectlists[5]);
            }
            else if (BoolList[UIObj.InBoolList[3]])
            {
                PartMutually2 = PartMutuallyType_.Open;
                e.DrawImage(_imgs[6], _rectlists[5]);
            }



        }

        private void DrawView24Chacter(Graphics e)
        {

            e.DrawString("隔离开关1", FormatStyle.Font14, FormatStyle.WhiteBrush, _rectlists[2], FormatStyle.MFormat);
            e.DrawString("隔离开关2", FormatStyle.Font14, FormatStyle.WhiteBrush, _rectlists[3], FormatStyle.MFormat);
        }

        #endregion



        private void Judge(int View, Graphics e)
        {
            switch (View)
            {
                case 23:
                    DrawView23Chacter(e);
                    DrawView23Rects(e);
                    DrawViewImgs(e);
                    drawPointOut(e);
                    break;
                case 24:
                    DrawView24Imgs(e);
                    DrawView24Chacter(e);
                    break;
                case 25:
                    break;
            }
        }

        private void InitData()
        {
            _imgs = new List<Image>();
            _foolatIds = new List<int>();
            _rects = new List<Rectangle>();
            _boolIds = new List<int>();
            _rectlists = new List<Rectangle>();

            #region 从配置表里取出数据
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

            #region 视图23的坐标初始化

            //视图23提示
            _rects.Add(new Rectangle(200, 125, 311, 42));
            //制动过分分相设置
            _rects.Add(new Rectangle(132, 239, 142, 30));
            //投入
            _rects.Add(new Rectangle(379, 239, 48, 30));
            //切入
            _rects.Add(new Rectangle(529, 239, 48, 30));
            //过分相G1-G2的距离
            _rects.Add(new Rectangle(106, 309, 190, 30));
            //170m
            _rects.Add(new Rectangle(375, 316, 58, 40));
            //当前使用值
            _rects.Add(new Rectangle(341, 350, 92, 26));
            //两个大的矩形框
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(80, 199 + i * 100, 565, 90));
            }
            //第二个矩形框里的小矩形框
            _rects.Add(new Rectangle(430, 347, 72, 30));
            //投入，切入图片的位置
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(338 + i * 148, 228, 41, 41));
            }
            //G1-G2的距离图片
            _rects.Add(new Rectangle(338, 308, 41, 41));
            #endregion

            #region 视图24的坐标的初始化
            //图片框的位置
            for (int i = 0; i < 2; i++)
            {
                _rectlists.Add(new Rectangle(303 + i * 126, 260, 77, 77));
            }
            //隔离开关1-隔离开关2
            for (int i = 0; i < 2; i++)
            {
                _rectlists.Add(new Rectangle(170 + i * 366, 283, 101, 35));
            }
            //图片的位置
            for (int i = 0; i < 2; i++)
            {
                _rectlists.Add(new Rectangle(307 + i * 126, 265, 67, 67));
            }


            #endregion


        }

    }
}
