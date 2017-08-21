using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class CoolingSystem:baseClass
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

        #region 重载的方法
        public override string GetInfo()
        {
            return "冷却系统";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            DrawRects(dcGs);
            DrawChacter(dcGs);
            base.paint(dcGs);

        }

        #endregion

        #region 画图的各种方法
        /// <summary>
        /// 画矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 22; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[i]);
            }
        }
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="e">画图对象</param>

        private void DrawChacter(Graphics e)
        {
            for (int i = 0; i < 11; i++)
            {
                e.DrawString(FormatStyle.str16[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[22 + i], FormatStyle.RightFormat);
            }
            for (int i = 0; i < 11; i++)
            {
                e.DrawString(FormatStyle.str16[i + 11], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[33 + i], FormatStyle.LeftFormat);
            }
            //电机1-6温度
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(FloatList[_foolatIds[i]].ToString(CultureInfo.InvariantCulture) + "℃", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
                e.DrawString(FloatList[_foolatIds[i + 10]].ToString(CultureInfo.InvariantCulture) + "℃", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[11 + i], FormatStyle.MFormat);
            }
            //主变流器1-2冷却水温度，柜体温度
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[_foolatIds[i + 4]].ToString(CultureInfo.InvariantCulture) + "℃", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i + 5], FormatStyle.MFormat);
                e.DrawString(FloatList[_foolatIds[i + 14]].ToString(CultureInfo.InvariantCulture) + "℃", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i + 16], FormatStyle.MFormat);
            }
            //主变流器1-2出水压力，进水压力，水压力差
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(FloatList[_foolatIds[i + 6]].ToString("0.00") + "bar", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i + 7], FormatStyle.MFormat);
                e.DrawString(FloatList[_foolatIds[i + 16]].ToString("0.00") + "bar", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i + 18], FormatStyle.MFormat);
            }
            //主变流器1-2中间直流电压
            e.DrawString(FloatList[_foolatIds[9]].ToString("0") + "V", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[10], FormatStyle.MFormat);
            e.DrawString(FloatList[_foolatIds[19]].ToString("0") + "V", FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[21], FormatStyle.MFormat);
            //油流1-2状态
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[_boolIds[i]])
                {
                    e.DrawString(FormatStyle.str17[0], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[4 + i * 11], FormatStyle.MFormat);
                }
                else if (BoolList[_boolIds[i + 2]])
                {
                    e.DrawString(FormatStyle.str17[1], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[4 + i * 11], FormatStyle.MFormat);
                }
            }
        }

        #endregion
     
        /// <summary>
        /// 列表初始化和坐标的赋值
        /// </summary>
        private void InitData()
        {
            #region 列表的初始化
            _boolIds = new List<int>();
            _imgs = new List<Image>();
            _foolatIds = new List<int>();
            _rects = new List<Rectangle>();
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

            #region 坐标的赋值 

            //22个矩形框
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    _rects.Add(new Rectangle(275 + i * 119, 73 + j * 36, 118, 35));
                }
            }
            //左边11个矩形框外围文字
            for (int i = 0; i < 11; i++)
            {
                _rects.Add(new Rectangle(52, 73 + i * 36, 216, 36));

            }
            //右边11个矩形框外围文字
            for (int i = 0; i < 11; i++)
            {
                _rects.Add(new Rectangle(526, 85 + i * 36, 216, 36));

            }
            
            #endregion

          
        }
    }
}
