using System;
using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TrainElectricity : baseClass
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
        private List<Rectangle> _rectangles;
        /// <summary>
        /// 点列表
        /// </summary>
        private List<Point> _points;
        /// <summary>
        /// 
        /// </summary>
        DateTime startTime;
        /// <summary>
        /// 火车启动的时间
        /// </summary>
        double times;

        public override string GetInfo()
        {
            return "列车供电";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            startTime = DateTime.Now;
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            DrawRects(dcGs);
            DrawImgs(dcGs);
            DrawPoints(dcGs);
            FillRects(dcGs);
            DrawChacter(dcGs);
            base.paint(dcGs);

        }
        /// <summary>
        /// 画各矩形框
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawRects(Graphics e)
        {   
            //上排8个矩形框
            for (int i = 0; i < 8; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rectangles[i]);
            }
            //下排13个矩形框
            for (int i = 0; i < 13; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen1, _rectangles[i+8]);
            }
       
       
        }
        /// <summary>
        /// 画图片
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawImgs(Graphics e)
        {   
            //列车供电1，列车供电2
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(_imgs[0],_rectangles[21+i]);
            }
            //上排8个，供电应许等图标
           
             for (int i = 0; i < 8; i++)
            {
                if (BoolList[_boolIds[i]])
                {
                    e.DrawImage(_imgs[4], _rectangles[0 + i]);
                    
                }
                else
                {
                    e.DrawImage(_imgs[1], _rectangles[0 + i]);
                }
              
            }
             //MQS
             for (int i = 0; i < 4; i++)
             {
                 if (BoolList[_boolIds[i+10]])
                 {
                     e.DrawImage(_imgs[5], _rectangles[67 + i]);
                 }
                 else
                 {
                     e.DrawImage(_imgs[3], _rectangles[67 + i]); 
                 }
                
             }
             //km13,Km14
            for (int i = 0; i <2; i++)
            {
                if (BoolList[_boolIds[i + 8]])
                {
                    e.DrawImage(_imgs[5], _rectangles[66+i*5]);
                }
                else
                {
                    e.DrawImage(_imgs[2], _rectangles[66+i*5]); 
                }
            }
            //二极管底图
            for (int i = 0; i < 2; i++)
            {
                e.DrawImage(_imgs[6], _rectangles[88+i]); 
            }
           
           
           
          
           
        }
        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawPoints(Graphics e)
        {
            //左边第一条直线和横第一条直线
            for (int i = 0; i < 2; i++)
            {
                e.DrawLine(FormatStyle.WhitePen1, _points[i], _points[i+1]);
            }
            //左边第二条直线和横着第二条直线
            for (int i = 0; i < 2; i++)
            {
                e.DrawLine(FormatStyle.WhitePen1, _points[i + 3], _points[i + 4]);
            }
                e.DrawLine(FormatStyle.WhitePen1, _points[6], _points[7]);
                e.DrawLine(FormatStyle.WhitePen1, _points[8], _points[9]);
                e.DrawLine(FormatStyle.WhitePen1, _points[10], _points[11]);
                e.DrawLine(FormatStyle.WhitePen1, _points[12], _points[13]);
                e.DrawLine(FormatStyle.WhitePen1, _points[14], _points[15]);
                e.DrawLine(FormatStyle.WhitePen1, _points[16], _points[17]);
                e.DrawLine(FormatStyle.WhitePen1, _points[11], _points[18]);
                e.DrawLine(FormatStyle.WhitePen1, _points[19], _points[20]);
                e.DrawLine(FormatStyle.WhitePen1, _points[21], _points[22]);
                e.DrawLine(FormatStyle.WhitePen1, _points[23], _points[24]);
            
         
                     
        }
        /// <summary>
        /// 填充矩形框
        /// </summary>
        /// <param name="e">画图对象</param>

        private void FillRects(Graphics e)
        {
            //快熔
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[_boolIds[i + 14]])
                {

                    e.FillRectangle(FormatStyle.GreenBrush, _rectangles[23 + i]);
                }
                else
                {

                    e.FillRectangle(FormatStyle.WhiteBrush, _rectangles[23 + i]);
                }

            }
        }
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawChacter(Graphics e)
        {
            //上排18个矩形框的文字
            for (int i = 0; i <18; i++)
            {
                e.DrawString(FormatStyle.str13[i],FormatStyle.Font12,FormatStyle.WhiteBrush,_rectangles[25+i],FormatStyle.MFormat);
            }
            //中间10个变动的值
            for (int i = 0; i < 10; i++)
            {
                e.DrawString(FloatList[_foolatIds[0+i]].ToString(), FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[43 + i], FormatStyle.MFormat);
                e.DrawString(FormatStyle.str14[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[53 + i], FormatStyle.MFormat);
            }
            
            e.DrawString("电度量", FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[63], FormatStyle.MFormat);
            e.DrawString((FloatList[_foolatIds[10]]*times).ToString(), FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[64], FormatStyle.MFormat);
            e.DrawString("kw/h", FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[65], FormatStyle.MFormat);
            e.DrawString("列车供电1", FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[21], FormatStyle.MFormat);
            e.DrawString("列车供电2", FormatStyle.Font12, FormatStyle.WhiteBrush, _rectangles[22], FormatStyle.MFormat);
            //下排16个
            for (int i = 0; i < 16; i++)
            {
                e.DrawString(FormatStyle.str15[i], FormatStyle.Font10, FormatStyle.WhiteBrush, _rectangles[i + 72],
                    FormatStyle.MFormat);
            }
            ChangeTime();
        
            
        }
        private void ChangeTime()
        {
            
            System.TimeSpan endTime;
           endTime = DateTime.Now-startTime;
           times = endTime.Days * 24 + endTime.Hours + endTime.Minutes / 60.0 + endTime.Seconds / 3600.0;
            DateTime time1;
          
        }


        /// <summary>
        /// 初始化和赋值
        /// </summary>
        private void InitData()
        {
            #region 列表的初始化
            _imgs=new List<Image>();
            _boolIds=new List<int>();
            _foolatIds=new List<int>();
            _rectangles=new List<Rectangle>();
            _points=new List<Point>();
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

            #region 矩形框和点坐标的赋值

            //上排8个矩形框
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _rectangles.Add(new Rectangle(242+i*221,48+j*31,91,29));
                }
                
            }
            //中间10个矩形框
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _rectangles.Add(new Rectangle(221 + i * 266, 194 + j * 33, 88, 29)); 
                }
            }
            //10个矩形框中间中间的矩形框
            _rectangles.Add(new Rectangle(317,194,166,62));
            //底部两个矩形框
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(188+i*346,368,69,124));
            }
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(5 + i * 668, 47, 122, 31));
            }
            _rectangles.Add(new Rectangle(114,443,34,27));
            _rectangles.Add(new Rectangle(634,443,34,27));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _rectangles.Add(new Rectangle(145+i*411,50+j*30,89,23));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _rectangles.Add(new Rectangle(113+ i * 460, 197 + j * 34, 110, 23));
                }  
            }
            //供电电压，供电电流等，的值
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _rectangles.Add(new Rectangle(221 + i * 266, 194 + j * 33, 56, 29)); 
                }
            }
            //供电电压，供电电流等，的d单位
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    _rectangles.Add(new Rectangle(277 + i * 266, 194 + j * 33, 33, 29));
                }
            }
               _rectangles.Add(new Rectangle(373,200,57,23));
               _rectangles.Add(new Rectangle(353, 228, 57, 23));
               _rectangles.Add(new Rectangle(431, 228, 47, 23));
              _rectangles.Add(new Rectangle(141,382,46,19));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _rectangles.Add(new Rectangle(291+i*175, 380 + j* 59, 69, 19));
                }
              
            }
            _rectangles.Add(new Rectangle(653, 381, 46, 19));
            //底部16个文字的矩形框位置
            _rectangles.Add(new Rectangle(129,367,43,16));
            _rectangles.Add(new Rectangle(112, 406, 73, 16));
            _rectangles.Add(new Rectangle(113, 427, 37, 16));
            _rectangles.Add(new Rectangle(276, 364, 43, 16));
            _rectangles.Add(new Rectangle(264, 400, 78, 16));
            _rectangles.Add(new Rectangle(276, 421, 43, 16));
            _rectangles.Add(new Rectangle(264, 460, 78, 16));
            _rectangles.Add(new Rectangle(450, 362, 43, 16));
            _rectangles.Add(new Rectangle(438, 399, 78, 16));
            _rectangles.Add(new Rectangle(450, 420, 43, 16));
            _rectangles.Add(new Rectangle(440, 461, 78, 16));
            _rectangles.Add(new Rectangle(637, 364, 43, 16));
            _rectangles.Add(new Rectangle(618, 400, 78, 16));
            _rectangles.Add(new Rectangle(628, 424, 46, 16));
            _rectangles.Add(new Rectangle(113, 474, 32, 16));
            _rectangles.Add(new Rectangle(635, 474, 46, 16));
            //二极管的位置
            for (int i = 0; i < 2; i++)
            {
                _rectangles.Add(new Rectangle(204+i*348,400,35,57));
            }
            //画直线点的坐标
            _points.Add(new Point(34,75));
            _points.Add(new Point(34,457));
            _points.Add(new Point(114,457));
            _points.Add(new Point(88, 75));
            _points.Add(new Point(88, 397));
            _points.Add(new Point(145, 397));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _points.Add(new Point(259 + j * 38, 397+i*60));
                }
              
            }
            _points.Add(new Point(750, 75));
            _points.Add(new Point(750, 457));
            _points.Add(new Point(698, 75));
            _points.Add(new Point(698, 397));
            _points.Add(new Point(602, 397));
            _points.Add(new Point(656, 397));
            _points.Add(new Point(602, 457));
            _points.Add(new Point(634, 457));
            _points.Add(new Point(668, 457));
            _points.Add(new Point(148, 457));
            _points.Add(new Point(188, 457));
            _points.Add(new Point(436, 397));
            _points.Add(new Point(471, 397));
            _points.Add(new Point(436, 457));
            _points.Add(new Point(471, 457));
            #endregion



        }
    }
}
