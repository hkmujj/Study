using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.DialScreen
{
    /// <summary>
    /// 表盘
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class DialScreen : baseClass
    {
      
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "表盘";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Img = UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(RecPath, s))).ToArray();

            LoadDialPlateInfo();

            InitData();

            return true;
        }

        private void LoadDialPlateInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "表盘信息.txt");
            var all = File.ReadLines(file, Encoding.Default).ToArray();

            for (int nIndex = 0; nIndex < all.Length; nIndex++)
            {
                var cStr = all[nIndex];
                string[] split = cStr.Split('=');
                if (split.Length == 2)
                {
                    if (nIndex == 0)
                    {
                        if (int.TryParse(split[1].Trim(), out m_PointerNumb))
                        {
                            m_PointerRects = new RectangleF[m_PointerNumb];
                            m_CentrePoints = new PointF[m_PointerNumb];
                            m_TheMiniAngles = new float[m_PointerNumb];
                            m_TheMaxAngles = new float[m_PointerNumb];
                            m_TheMaxValues = new float[m_PointerNumb];
                            m_PointerImgID = new int[m_PointerNumb];
                            m_Pointers = new SpeedPointer[m_PointerNumb];
                        }
                    }
                    else if (nIndex == 1)
                    {
                        if (int.TryParse(split[1].Trim(), out m_DialNumb))
                        {
                            m_DialRects = new RectangleF[m_DialNumb];
                            m_DialLables = new string[m_DialNumb];
                        }
                    }
                    else if (nIndex > 0 && m_PointerNumb > 0 && m_ThePointerID < m_PointerNumb)
                    {
                        if (split[0] == "表盘" + (m_ThePointerID + 1).ToString() + "顶点坐标X")
                        {
                            m_DialRects[m_ThePointerID].X = float.Parse(split[1]);
                        }
                        else if (split[0] == "表盘" + (m_ThePointerID + 1).ToString() + "顶点坐标Y")
                        {
                            m_DialRects[m_ThePointerID].Y = float.Parse(split[1]);
                        }
                        else if (split[0] == "表盘" + (m_ThePointerID + 1).ToString() + "宽W")
                        {
                            m_DialRects[m_ThePointerID].Width = float.Parse(split[1]);
                        }
                        else if (split[0] == "表盘" + (m_ThePointerID + 1).ToString() + "高H")
                        {
                            m_DialRects[m_ThePointerID].Height = float.Parse(split[1]);
                        }
                        else if (split[0] == "表盘" + (m_ThePointerID + 1).ToString() + "标签")
                        {
                            m_DialLables[m_ThePointerID] = split[1] == "无" ? string.Empty : split[1];
                        }

                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "顶点坐标X")
                        {
                            m_PointerRects[m_ThePointerID].X = float.Parse(split[1]);
                        }
                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "顶点坐标Y")
                        {
                            m_PointerRects[m_ThePointerID].Y = float.Parse(split[1]);
                        }
                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "宽W")
                        {
                            m_PointerRects[m_ThePointerID].Width = float.Parse(split[1]);
                        }
                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "高H")
                        {
                            m_PointerRects[m_ThePointerID].Height = float.Parse(split[1]);
                        }

                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "中心点坐标X")
                        {
                            m_CentrePoints[m_ThePointerID].X = float.Parse(split[1]);
                        }
                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "中心点坐标Y")
                        {
                            m_CentrePoints[m_ThePointerID].Y = float.Parse(split[1]);
                        }

                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "指针初始角度")
                        {
                            m_TheMiniAngles[m_ThePointerID] = float.Parse(split[1]);
                        }
                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "指针最大角度")
                        {
                            m_TheMaxAngles[m_ThePointerID] = float.Parse(split[1]);
                        }

                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "图片序号")
                        {
                            m_PointerImgID[m_ThePointerID] = int.Parse(split[1]);
                        }

                        else if (split[0] == "指针" + (m_ThePointerID + 1).ToString() + "最大值")
                        {
                            m_TheMaxValues[m_ThePointerID] = float.Parse(split[1]);
                            m_ThePointerID++;
                        }
                    }
                }
                else if (split.Length == 1 && split[0].Trim() == "over")
                {
                    for (int i = 0; i < m_PointerNumb; i++)
                    {
                        m_Pointers[i] = new SpeedPointer(m_TheMaxAngles[i], m_TheMiniAngles[i], m_TheMaxValues[i],
                            m_PointerRects[i], m_CentrePoints[i], m_Img[m_PointerImgID[i]]);
                    }
                }
            }
        }

        int m_PointerNumb = 0;
        int[] m_PointerImgID;
        RectangleF[] m_PointerRects;
        PointF[] m_CentrePoints;
        float[] m_TheMiniAngles;
        float[] m_TheMaxAngles;
        float[] m_TheMaxValues;
        int m_ThePointerID = 0;

        int m_DialNumb = 0;
        RectangleF[] m_DialRects;
        string[] m_DialLables;
        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
          
            GetValue();
            DrawOn(g);
         
            base.paint(g);
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        /// <summary>
        /// 循环数据
        /// </summary>
        private void GetValue()
        {
            //接收float数据
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {

                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }

            //fValue[0] = 10;

            //test[0] = Test(test[0], 15f, 1000f);
            //test[1] = Test(test[1], 35f, 1000f);
            //test[2] = Test(test[2], 25f, 1600f);
            //test[3] = Test(test[3], 45f, 1600f);

            #endregion
        }

        float[] m_Test = { 0, 0, 0, 0 };
        private float Test(float test, float scale, float maxNumb)
        {
            test += scale;
            if (test >= maxNumb) test = 0;
            return test;
        }

        private void DrawOn(Graphics e)
        {
            //e.FillRectangle(FormatStyle.BlackSolidBrush, rects[0]);
            for (int i = 0; i < m_DialRects.Length-1; i++)
            {
                e.DrawImage(m_Img[i], m_DialRects[i]);
                if (m_DialLables[i] != string.Empty)
                {
                    e.FillRectangle(FormatStyle.m_WhiteSolidBrush,
                        new RectangleF(m_DialRects[i].X, m_DialRects[i].Y + m_DialRects[i].Height, m_DialRects[i].Width, 20));
                    e.DrawString(m_DialLables[i], FormatStyle.m_Font12, FormatStyle.m_BlackSolidBrush,
                        new RectangleF(m_DialRects[i].X, m_DialRects[i].Y + m_DialRects[i].Height, m_DialRects[i].Width, 20),
                        m_DrawFormat);
                }
            }
            e.DrawImage(m_Img[5], m_DialRects[2]);
            for (int i = 0; i < m_Pointers.Length; i++)
            {
                m_Pointers[i].PaintPointerNormal(e, ref m_FValue[i]);
            }

            //e.DrawString(test1.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 180));
            //e.DrawString(test2.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 500));
            //e.DrawString(test3.ToString(), FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
            //    new PointF(20, 700));
            //speedPointer.PaintPointer(e, Img[2], ref fValue[0]);
        }
    

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_DrawFormat.Alignment = StringAlignment.Center;

            m_RightFormat.LineAlignment = StringAlignment.Far;
            m_RightFormat.Alignment = StringAlignment.Center;

            m_LeftFormat.LineAlignment = StringAlignment.Near;
            m_LeftFormat.Alignment = StringAlignment.Center;

            m_PDrawPoint = new PointF[20];

            m_Rects = new RectangleF[120];

            #region :::::::::::::::::::::::: rects ::::::::::::::::::::::
            m_Rects[0] = new RectangleF(0, 0, 480, 854);
           #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        public String[] m_Str1 = new String[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public String[] m_Str2 = new String[2] { "确定", "取消" };


      
        public SpeedPointer m_SpeedPointer;
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] m_FValue;

        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] m_Img=new Image[8];

        SpeedPointer[] m_Pointers=new SpeedPointer[10];

        #endregion#

        #endregion

    }
}
