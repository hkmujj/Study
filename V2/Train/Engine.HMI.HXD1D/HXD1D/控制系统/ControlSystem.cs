using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D.控制系统
{    
    [GksDataType(DataType.isMMIObjectClass)]
    class ControlSystem:baseClass
    {
        #region 列表对象的定义
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
        /// 矩形框
        /// </summary>
        private List<Rectangle> _rects;
        #endregion

        #region 重载的方法
        public override string GetInfo()
        {
            return "控制系统";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {

            DrawImg(dcGs);
            FillRects(dcGs);
            DrawCharacters(dcGs);
            base.paint(dcGs);
        }


        #endregion

        #region 自己定义的方法 
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImg(Graphics e)
        {
            e.DrawImage(_imgs[0], _rects[25]);//底图
        }
        /// <summary>
        /// 画字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawCharacters(Graphics e)
        {
            for (int i = 0; i < 25; i++)
            {
                e.DrawString(FormatStyle.str9[i], FormatStyle.Font14, FormatStyle.BlackBrush, _rects[i], FormatStyle.MFormat);
            }
            for (int i = 0; i < 8; i++)
            {
                e.DrawString(FormatStyle.str10[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[26 + i], FormatStyle.MFormat);
            }

        }
        /// <summary>
        /// 根据状态不同填充矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void FillRects(Graphics e)
        {
            for (int i = 0; i < 25; i++)
            {
                e.FillRectangle(FormatStyle.GreyBrush, _rects[i]);

                if (BoolList[_boolIds[i]])
                {
                    e.FillRectangle(FormatStyle.GreenBrush, _rects[i]);
                }
                if (BoolList[_boolIds[i + 25]])
                {
                    e.FillRectangle(FormatStyle.RedBrush, _rects[i]);
                }

            }

        }

        #endregion
      
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            #region 列表对象的初始化
            _rects = new List<Rectangle>();
            _imgs = new List<Image>();
            _foolatIds = new List<int>();
            _boolIds = new List<int>();
            #endregion
         
            #region 从配置表取Img，Floatids，Boolids编号
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

            #region 列表对象的赋值
            //
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rects.Add(new Rectangle(107 + j * 518, 118 + i * 52, 68, 42));
                }

            }
            //
            for (int i = 0; i < 4; i++)
            {
                _rects.Add(new Rectangle(192 + i * 86, 169, 68, 42));
            }
            //
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(107 + i * 85, 327, 68, 42));
            }
            //
            for (int i = 0; i < 4; i++)
            {
                _rects.Add(new Rectangle(365 + i * 85, 327, 68, 42));
            }
            //
            for (int i = 0; i < 9; i++)
            {
                _rects.Add(new Rectangle(56 + i * 77, 444, 68, 42));
            }
            //底图
            _rects.Add(new Rectangle(0, 35, 800, 505));
            //文字CCU的位置
            _rects.Add(new Rectangle(431, 150, 32, 14));
            //文字line_A的位置
            _rects.Add(new Rectangle(87, 263, 58, 20));
            //文字line_B的位置
            _rects.Add(new Rectangle(60, 302, 58, 26));
            //文字cab1的位置
            _rects.Add(new Rectangle(233, 368, 58, 26));
            //下排文字line_A的位置
            _rects.Add(new Rectangle(129, 368, 58, 26));
            //文字cab2的位置
            _rects.Add(new Rectangle(656, 368, 58, 26));
            //下排文字line_B的位置
            _rects.Add(new Rectangle(102, 418, 58, 26));
            //位置MIO的位置
            _rects.Add(new Rectangle(745, 452, 58, 26));

            #endregion
          
        }
    

    }
}
