using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using HXD1D.运行条件;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ButtonSeting : baseClass
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
        /// 矩形框编号
        /// </summary>
        private List<Rectangle> _rects;




        private List<Region> _btnRegions;
        #region 重载的方法
        public override string GetInfo()
        {
            return "按钮";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        void SendTestData(int index, bool Type)
        {
            append_postCmd(CmdType.SetInBoolValue, 800 + index, Type ? 1 : 0, 0);
        }
        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            for (; index < _btnRegions.Count; index++)
            {
                if (_btnRegions[index].IsVisible(nPoint))
                    break;

            }
            if (index >= 0 && index <= 15)
            {
                SendTestData(index, true);
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < _btnRegions.Count; index++)
            {
                if (_btnRegions[index].IsVisible(nPoint))
                    break;

            }
            if (index >= 0 && index <= 15)
            {
                SendTestData(index, false);
            }
            return base.mouseUp(nPoint);
        }
        public override void paint(Graphics dcGs)
        {
            DrawRects(dcGs);
            IsShow(dcGs);
            DrawChacters(dcGs);
            DrawImgs(dcGs);
            base.paint(dcGs);

        }
        #endregion

        /// <summary>
        /// 按键复位
        /// </summary>
        private void ButtonReset()
        {
            for (int i = 0; i < 16; i++)
            {
                Title.buttonIsDown[i] = false;
            }
        }

        private void IsShow(Graphics e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Title.buttonIsDown[i])
                {
                    e.DrawImage(_imgs[5], _rects[i]);
                }

            }
        }

        /// <summary>
        /// 画按钮的矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawRects(Graphics e)
        {
            //底部10个按钮
            for (int i = 0; i < 10; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[i]);
            }
            // 右边6个按钮
            for (int i = 0; i < 6; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[10 + i]);
            }

        }
        /// <summary>
        /// 画按钮上的文字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawChacters(Graphics e)
        {
            //底部0-9的文字
            for (int i = 0; i < 10; i++)
            {
                e.DrawString(((i + 1) % 10).ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
            }
            //取消键的按钮上的文字
            e.DrawString("C", FormatStyle.Font16B, FormatStyle.WhiteBrush, _rects[10], FormatStyle.LeftFormat);
        }
        /// <summary>
        /// 画按钮上的图像
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImgs(Graphics e)
        {
            for (int i = 0; i < 4; i++)
            {
                e.DrawImage(_imgs[i], _rects[16 + i]);
            }
            e.DrawImage(_imgs[4], _rects[20]);

        }

        private void InitData()
        {
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _btnRegions = new List<Region>();
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
            //底部的十个按钮
            for (int i = 0; i < 10; i++)
            {
                _rects.Add(new Rectangle(120 + i * 68, 613, 58, 58));//下标题各按钮位置
            }
            //右侧的六个按钮
            for (int i = 0; i < 6; i++)
            {
                _rects.Add(new Rectangle(816, 71 + i * 85, 52, 75));
            }
            //右侧上一个，下一个，上一页，下一页的按钮图片
            for (int i = 0; i < 4; i++)
            {
                _rects.Add(new Rectangle(836, 166 + i * 85, 20, 21));
            }
            //右侧最后一个按钮的图片
            _rects.Add(new Rectangle(824, 504, 38, 32));
            //
            for (int i = 0; i < 16; i++)
            {
                _btnRegions.Add(new Region(_rects[i]));
            }
        }

    }
}
