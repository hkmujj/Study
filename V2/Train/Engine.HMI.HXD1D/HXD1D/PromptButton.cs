using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.运行条件;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class PromptButton : baseClass
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
        /// 矩形框列表
        /// </summary>
        private List<Rectangle> _rects;

        /// <summary>
        /// 点的列表
        /// </summary>
        private List<Point> _points;


        public override string GetInfo()
        {
            return "提示按钮";
        }
        public override void paint(Graphics dcGs)
        {
            JudgeView(Title.CurentView, dcGs);
            base.paint(dcGs);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        private void DrawPoints(Graphics e, int type)
        {
            //第一个框
            if (type == 1)
            {
                e.DrawLine(FormatStyle.WhitePen1, _points[0], _points[1]);
                e.DrawLine(FormatStyle.WhitePen1, _points[1], _points[2]);
            }
            //第二个框
            e.DrawLine(FormatStyle.WhitePen1, _points[3], _points[4]);
            e.DrawLine(FormatStyle.WhitePen1, _points[4], _points[5]);
            //第三个框
            e.DrawLine(FormatStyle.WhitePen1, _points[6], _points[7]);
            e.DrawLine(FormatStyle.WhitePen1, _points[7], _points[8]);
            //第四个框
            e.DrawLine(FormatStyle.WhitePen1, _points[9], _points[10]);
            e.DrawLine(FormatStyle.WhitePen1, _points[10], _points[11]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="k"></param>
        private void Fillrects(Graphics e, int k)
        {
            for (int i = k; i < 4; i++)
            {
                e.FillRectangle(FormatStyle.GreyBrush, _rects[i]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="k"></param>
        private void DrawChacter(Graphics e, int k)
        {
            for (int i = k; i < 4; i++)
            {
                e.DrawString(FormatStyle.str27[i], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
            }
        }

        private void DrawDown(Graphics e)
        {
            //上一页
            e.DrawLine(FormatStyle.WhitePen1, _points[18], _points[19]);
            e.DrawLine(FormatStyle.WhitePen1, _points[19], _points[20]);
            e.FillRectangle(FormatStyle.GreyBrush, _rects[6]);
            e.DrawString("上一页", FormatStyle.Font12B, FormatStyle.WhiteBrush, _rects[6], FormatStyle.MFormat);
        }

        private void DrawUp(Graphics e)
        {
            //下一页
            e.DrawLine(FormatStyle.WhitePen1, _points[12], _points[13]);
            e.DrawLine(FormatStyle.WhitePen1, _points[13], _points[14]);
            e.FillRectangle(FormatStyle.GreyBrush, _rects[4]);
            e.DrawString("下一页", FormatStyle.Font12B, FormatStyle.WhiteBrush, _rects[4], FormatStyle.MFormat);

        }

        /// <summary>
        /// 画提示按钮的第一个和第四个
        /// </summary>
        /// <param name="e"></param>
        private void DrawButtons(Graphics e)
        {
            //第一个框
            e.DrawLine(FormatStyle.WhitePen1, _points[0], _points[1]);
            e.DrawLine(FormatStyle.WhitePen1, _points[1], _points[2]);
            //第四个框
            e.DrawLine(FormatStyle.WhitePen1, _points[9], _points[10]);
            e.DrawLine(FormatStyle.WhitePen1, _points[10], _points[11]);
            e.FillRectangle(FormatStyle.GreyBrush, _rects[0]);
            e.FillRectangle(FormatStyle.GreyBrush, _rects[3]);
            e.DrawString(FormatStyle.str27[0], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[0], FormatStyle.MFormat);
            e.DrawString(FormatStyle.str27[3], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[3], FormatStyle.MFormat);

        }

        private void drawUpAndDown(Graphics dcGs)
        {
            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699, 88 + 48), new Point(699 + 63, 88 + 48));
            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699 + 63, 88 + 48), new Point(699 + 63, 88));
            dcGs.FillRectangle(FormatStyle.GreyBrush, new Rectangle(699, 88, 63, 48));
            dcGs.DrawString("返回", FormatStyle.Font12B, FormatStyle.WhiteBrush, new Rectangle(699, 88, 63, 48), FormatStyle.MFormat);

            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699, 88 + 62 + 48), new Point(699+63, 88 + 62 + 48));
            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699 + 63, 88 + 62 + 48), new Point(699 + 63, 88 + 62));
            dcGs.FillRectangle(FormatStyle.GreyBrush, new Rectangle(699,88+62,63,48));
            dcGs.DrawString("上移", FormatStyle.Font12B, FormatStyle.WhiteBrush, new Rectangle(699, 88 + 62, 63, 48), FormatStyle.MFormat);

            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699, 88 + 62 + 48 + 62), new Point(699 + 63, 88 + 62 + 48 + 62));
            dcGs.DrawLine(FormatStyle.WhitePen1, new Point(699 + 63, 88 + 62 + 48 + 62), new Point(699 + 63, 88 + 62 + 62));
            dcGs.FillRectangle(FormatStyle.GreyBrush, new Rectangle(699, 88 + 62 + 62, 63, 48));
            dcGs.DrawString("下移", FormatStyle.Font12B, FormatStyle.WhiteBrush, new Rectangle(699, 88 + 62 + 62, 63, 48), FormatStyle.MFormat);
        }

        /// <summary>
        /// 判断是哪个视图
        /// </summary>
        /// <param name="View">视图</param>
        /// <param name="e">画图对象</param>
        private void JudgeView(int View, Graphics e)
        {
            switch (View)
            {
                case 19:
                    DrawPoints(e, 1);
                    Fillrects(e, 0);
                    DrawChacter(e, 0);
                    break;
                case 20:
                    DrawPoints(e, 1);
                    Fillrects(e, 0);
                    DrawChacter(e, 0);
                    break;
                case 21:
                    DrawButtons(e);
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);
                    break;
                case 22:
                    DrawButtons(e);
                    break;
                case 23:
                    DrawPoints(e, 1);
                    Fillrects(e, 0);
                    DrawChacter(e, 0);
                    break;
                case 26:
                    if (RunCondtion.BtnisDown[0])
                    {

                        DrawDown(e);
                    }
                    else
                    {
                        DrawUp(e);
                    }
                    break;
                case 27:
                    if (RunCondtion.BtnisDown[1])
                    {
                        DrawDown(e);

                    }
                    else
                    {
                        DrawUp(e);
                    }
                    break;
                case 31:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);

                    break;
                case 32:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);

                    break;
                case 28:
                    if (RunCondtion.BtnisDown[2])
                    {
                        DrawDown(e);
                    }
                    else
                    {

                        DrawUp(e);
                    }
                    break;
                case 45:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);
                    break;
                case 13:
                    //
                    e.DrawLine(FormatStyle.WhitePen1, _points[15], _points[16]);
                    e.DrawLine(FormatStyle.WhitePen1, _points[16], _points[17]);
                    //
                    e.FillRectangle(FormatStyle.GreyBrush, _rects[5]);
                    //
                    e.DrawString("清除\n主断\n分段次数\n[C]", FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[5], FormatStyle.MFormat);
                    break;
                case 39:
                    DrawButtons(e);
                    break;
                case 48:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);
                    break;
                case 49:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);
                    break;
                case 50:
                    DrawPoints(e, 0);
                    Fillrects(e, 1);
                    DrawChacter(e, 1);
                    break;
                case 53:
                    drawUpAndDown(e);
                    break;
                case 46:
                    DrawButtons(e);
                    break;
                case 55:

                    //第一个框
                    e.DrawLine(FormatStyle.WhitePen1, _points[0], _points[1]);
                    e.DrawLine(FormatStyle.WhitePen1, _points[1], _points[2]);
                    //第四个框
                    e.DrawLine(FormatStyle.WhitePen1, _points[9], _points[10]);
                    e.DrawLine(FormatStyle.WhitePen1, _points[10], _points[11]);
                    e.FillRectangle(FormatStyle.GreyBrush, _rects[0]);
                    e.FillRectangle(FormatStyle.GreyBrush, _rects[3]);
                    e.DrawString("主\n界面", FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[0], FormatStyle.MFormat);
                    e.DrawString(FormatStyle.str27[3], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[3], FormatStyle.MFormat);

                    //e.DrawLine(FormatStyle.WhitePen1, _points[9], _points[10]);
                    //e.DrawLine(FormatStyle.WhitePen1, _points[10], _points[11]);
                    //e.FillRectangle(FormatStyle.GreyBrush, _rects[3]);
                    ////e.DrawString(FormatStyle.str27[0], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[0], FormatStyle.MFormat);
                    //e.DrawString(FormatStyle.str27[3], FormatStyle.Font14B, FormatStyle.WhiteBrush, _rects[3], FormatStyle.MFormat);
                    break;
            }
        }

        private void InitData()
        {
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _points = new List<Point>();
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
            //第一个框填充的位置
            _rects.Add(new Rectangle(699, 88, 63, 48));
            //第二个框填充的位置
            _rects.Add(new Rectangle(701, 279, 64, 47));
            //第三个框填充的位置
            _rects.Add(new Rectangle(701, 344, 64, 47));
            //第四个框填充的位置
            _rects.Add(new Rectangle(701, 406, 64, 100));
            //下一页填充的位置
            _rects.Add(new Rectangle(716, 230, 64, 50));
            //状态信息右侧的提示位置
            _rects.Add(new Rectangle(711, 101, 74, 137));
            //上一页按钮框的位置
            _rects.Add(new Rectangle(716, 160, 64, 50));

            //第一个框的位置
            _points.Add(new Point(699, 136));
            _points.Add(new Point(763, 136));
            _points.Add(new Point(763, 87));
            //第二个框的位置
            _points.Add(new Point(701, 328));
            _points.Add(new Point(766, 328));
            _points.Add(new Point(766, 279));
            //第三个框的位置
            _points.Add(new Point(701, 393));
            _points.Add(new Point(766, 393));
            _points.Add(new Point(766, 343));
            //第四个框的位置
            _points.Add(new Point(701, 507));
            _points.Add(new Point(766, 507));
            _points.Add(new Point(766, 406));
            //下一页
            _points.Add(new Point(716, 281));
            _points.Add(new Point(781, 281));
            _points.Add(new Point(781, 230));
            //状态信息右侧提示
            _points.Add(new Point(711, 238));
            _points.Add(new Point(785, 238));
            _points.Add(new Point(785, 101));
            //上一页
            _points.Add(new Point(716, 211));
            _points.Add(new Point(781, 211));
            _points.Add(new Point(781, 160));
        }

    }
}
