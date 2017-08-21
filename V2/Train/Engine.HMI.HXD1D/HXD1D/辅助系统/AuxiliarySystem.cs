using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D.辅助系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    class AuxiliarySystem:baseClass
    {  
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        /// <summary>
        /// 矩形框
        /// </summary>
        private List<Rectangle> _rects;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;

        /// <summary>
        /// 画直线点的坐标
        /// </summary>
        private List<Point> _points;

        public override string GetInfo()
        {
            return "辅助系统";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
           
        }

        public override void paint(Graphics dcGs)
        {
         
            base.paint(dcGs);
            DrawImg(dcGs);
            DrawCharacters(dcGs);
            DrawLine(dcGs);
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DrawImg(Graphics e)
        {
            //底图
            e.DrawImage(_imgs[0], _rects[0]);
            //闭合器
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[_boolIds[i]]&&i<3)
                {                
                    e.DrawImage(_imgs[1], _rects[i+1]);            
                }
                else if (BoolList[_boolIds[i]] && i >= 3)
                {
                    e.DrawImage(_imgs[2], _rects[i+1]);
                    
                }
                
            }
            //断路器

            for (int i = 0; i < 25; i++)
            {
                if (BoolList[_boolIds[6+i]])
              {
                e.DrawImage(_imgs[3], _rects[7 + i]);
              }
               
            }
            if (BoolList[_boolIds[31]])
            {
                e.DrawImage(_imgs[4], _rects[32]);

            }
           

        }
        /// <summary>
        /// 画视图上的文字
        /// </summary>
        /// <param name="e">画图对象</param>

        private void DrawCharacters(Graphics e)
        {
            e.DrawString("辅助逆变器1",FormatStyle.Font14,FormatStyle.GreyBrushsBrush,_rects[33],FormatStyle.MFormat);
            e.DrawString("辅助逆变器2", FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[34], FormatStyle.MFormat);
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[32]])
                {
                       e.DrawString("??",FormatStyle.Font14,FormatStyle.GreyBrushsBrush,_rects[35+i],FormatStyle.MFormat);        
                }
                 else
                {
                    e.DrawString(FloatList[_foolatIds[0+i]].ToString("0.0"), FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[35 + i], FormatStyle.MFormat);
                    e.DrawString(FormatStyle.str8[i], FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[35 + i], FormatStyle.RightFormat);
                }                 
                    
            }
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[_boolIds[33]])
                {
                    e.DrawString("??", FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[38 + i], FormatStyle.MFormat);
                }
                else
                {
                    e.DrawString(FloatList[_foolatIds[3 + i]].ToString("0.0"), FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[38 + i], FormatStyle.MFormat);
                    e.DrawString(FormatStyle.str8[i], FormatStyle.Font14, FormatStyle.GreyBrushsBrush, _rects[38 + i], FormatStyle.RightFormat);
                }       

            }
              
        }
        /// <summary>
        /// 画出现故障时出现的两条红线
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawLine(Graphics e)
        {
            if (BoolList[_boolIds[32]])
            {
                e.DrawLine(FormatStyle.RedPen3, _points[0], _points[1]); 
            }
            if (BoolList[_boolIds[33]])
            {
                e.DrawLine(FormatStyle.RedPen3, _points[2], _points[3]);
            }
           
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            #region 列表的初始化
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _points = new List<Point>();
            #endregion

            #region 从对象配置表里取的数据

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
            //底图
            _rects.Add(new Rectangle(0, 36, 799, 498));
            // 接触器31-K10闭合
            _rects.Add(new Rectangle(229, 114, 40, 19));
            //接触器31-K02闭合
            _rects.Add(new Rectangle(378, 114, 40, 19));
            //接触器31-K30闭合
            _rects.Add(new Rectangle(558, 114, 40, 19));
            //接触器31-K23闭合
            _rects.Add(new Rectangle(444, 274, 20, 52));
            //接触器31-K24闭合
            _rects.Add(new Rectangle(505, 274, 20, 52));
            //接触器31-K47闭合
            _rects.Add(new Rectangle(436, 379, 20, 62));
            //断路器31-Q48闭合
            _rects.Add(new Rectangle(162, 72, 31, 38));
            //断路器31-Q49闭合
            _rects.Add(new Rectangle(607, 72, 31, 38));
            // 断路器34-Q11,断路器34-Q12闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(12 + i * 49, 265, 31, 38));
            }
            // 断路器34-Q13
            _rects.Add(new Rectangle(113, 265, 31, 38));
            // 断路器34-Q14,34-Q15,34-Q16闭合
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(166 + i * 51, 265, 31, 38));
            }
            //断路器34-Q17,34-Q18闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(324 + i * 54, 204, 31, 38));
            }
            //断路器34-Q23,34-Q24闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(447 + i * 60, 204, 31, 38));
            }

            //断路器34-Q25,34-Q26闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(559 + i * 50, 264, 31, 38));
            }
            //断路器34-Q27,34-Q28闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(665 + i * 55, 264, 31, 38));
            }
            //断路器34-Q29,34-Q30闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(698 + i * 54, 435, 31, 38));
            }
            //断路器34-Q31
            _rects.Add(new Rectangle(640, 435, 31, 38));
            //断路器32-Q32,34-Q33闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(532 + i * 54, 435, 31, 38));
            }
            //断路器77-Q46,34-Q45闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(113 - i * 65, 385, 31, 38));
            }
            //断路器34-Q39,37-Q40闭合
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(192 + i * 133, 385, 31, 38));
            }
            //断路器34-F72闭合
            _rects.Add(new Rectangle(459, 459, 31, 38));
            //辅助逆变器1

            _rects.Add(new Rectangle(10, 43, 112, 22));
            //辅助逆变器2
            _rects.Add(new Rectangle(683, 43, 112, 22));
            //辅助逆变器1电压，频率，电流文字的位置
            for (int i = 0; i < 3; i++)
            {
                _rects.Add(new Rectangle(130 + i * 92, 40, 86, 25));
            }
            //辅助逆变器2，电压，频率，电流文字的位置
            for (int i = 2; i >= 0; i--)
            {
                _rects.Add(new Rectangle(130 + (i+3) * 92, 40, 86, 25));
            }
            //辅助逆变器1故障时的画红色线的两个点
            _points.Add(new Point(9, 63));
            _points.Add(new Point(122, 41));
            //辅助逆变器2故障时的画红色线的两个点
            _points.Add(new Point(683, 63));
            _points.Add(new Point(795, 41));

            #endregion
         
         
        }
    }
}
