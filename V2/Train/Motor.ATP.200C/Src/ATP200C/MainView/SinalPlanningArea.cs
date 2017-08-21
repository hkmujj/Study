using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ATP200C.Common;
using ATP200C.PublicComponents;
using ATP200C.Resource.Images;
using MMI.Facility.Interface.Attribute;

namespace ATP200C.MainView
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class SinalPlanningArea : ATPBaseClass
    {
        private readonly string[] m_Distance = { "1000", "2000", "4000", "8000" };
        private Image[] m_Image;
        private int m_SinalLevel;//信号等级
        private int m_QiMo;//起模点横坐标


        #region 起 摸 区 域 元素

        private Rectangle m_Rect;

        /// <summary>
        /// 期摸点X坐标集合
        /// </summary>
        private readonly List<int> m_PointXs = new List<int>();

        /// <summary>
        /// 期模点Y坐标集合
        /// </summary>
        private readonly List<int> m_PointYs = new List<int>();


        private RectangleF m_SignalViewRegion;

        #endregion

        #region  重载方法

        public override string GetInfo()
        {
            return "机车信号及速度变化";
        }

        public override bool Initalize()
        {
            m_Image = new Image[]
            {
                ImageResource.L5,
                ImageResource.L4,
                ImageResource.L3,
                ImageResource.L2,
                ImageResource.L,
                ImageResource.LU,
                ImageResource.U,
                ImageResource.U2,
                ImageResource.U2S,
                ImageResource.HU,
                ImageResource.UU,
                ImageResource.UUS,
                ImageResource.HB,
                ImageResource.H,
                ImageResource.HZ,
                ImageResource.LU2,
                ImageResource.U3,
                ImageResource.HZ2,
            };

            m_Rect = new Rectangle(ATP200CCommon.RectD[0].X + 5, ATP200CCommon.RectD[0].Y + 150, 280, 155);

            m_SignalViewRegion = new RectangleF(ATP200CCommon.RectD[0].X + 100, ATP200CCommon.RectD[0].Y + 170, 80F,
                80F);

            // _configTextInfo.isdrawrectfrm = true;
            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();

            GetQiMoDianPoint();

            OnDraw(g);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetValue()
        {
            m_SinalLevel = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]) - 100;
            m_QiMo = Convert.ToInt32(TranslateXPoint(FloatList[UIObj.InFloatList[2]]));
        }


        #region 内部数据转换方法
        /// <summary>
        /// 将实际横坐标转换为屏上横坐标
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float TranslateXPoint(float x)
        {
            if (x <= 0)
            {
                return m_Rect.X;
            }
            if (x>0 && x < 1000)
            {
                return  ATP200CCommon.RectD[0].X + 5 + x * 135 / 1000;
            }
            if (x >= 1000 && x < 2000)
            {
                return ATP200CCommon.RectD[0].X + 90 + x * 45 / 1000;
            }
            if (x>= 2000 && x < 4000)
            {
                return  ATP200CCommon.RectD[0].X + 135 + x * 45 / 2000;
            }
            if (x >= 4000 && x < 8000)
            {
                return  ATP200CCommon.RectD[0].X + 180 + x * 45 / 4000;
            }
            return ATP200CCommon.RectD[0].X + 270;
        }

        /// <summary>
        /// 将实际高度转换为屏上的纵坐标
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private static float TranslateYPoint(float y)
        {
            if (y > 0 && y <= 250)
            {
               return  ATP200CCommon.RectD[0].Y + 305 - y * 150 / 250;
            }
            return ATP200CCommon.RectD[0].Y + 305;
        }

        #endregion

        /// <summary>
        /// 获取起模点集合
        /// </summary>
        private void GetQiMoDianPoint()
        {
            m_PointXs.Clear();//清空点集
            m_PointYs.Clear();

            for (int index = 0; index < 10; index++)
            {
                int pointX = Convert.ToInt32(FloatList[UIObj.InFloatList[2 * index+4]]);

                if (index==0)
                {
                    if (pointX<0)
                    {
                        pointX = 0;
                    }
                        m_PointXs.Add(pointX);
                        m_PointYs.Add(Convert.ToInt32(FloatList[UIObj.InFloatList[2 * index+5]]));
                }
                else
                {
                    if (pointX <= m_PointXs[index - 1])
                    {
                        break;
                    }
                    if (pointX >= 8000)
                    {
                        pointX = 8000;
                        m_PointXs.Add(pointX);
                        int pointY=Convert.ToInt32(FloatList[UIObj.InFloatList[2 * index + 5]]);
                        if (pointY> m_PointYs[index-1])
                        {
                            pointY=m_PointYs[index-1];
                        }
                        m_PointYs.Add(pointY);
                        break;
                    }
                    m_PointXs.Add(pointX);
                    m_PointYs.Add(Convert.ToInt32(FloatList[UIObj.InFloatList[2 * index + 5]]));
                }
            }
        }

        /// <summary>
        /// 根据限速点 得到起模点集合
        /// </summary>
        private List<Point> GetPolygonPoints()
        {
            var polygonPoints = new List<Point>();
            var starPoint = new Point(m_Rect.X, m_Rect.Bottom);

            if (m_PointXs.Count<2)
            {
                return polygonPoints;
            }

            polygonPoints.Add(starPoint);

            //获取所有起模点集合
            var qimoPoints = m_PointXs.Select((t, index) => new Point((int) TranslateXPoint(t), (int) TranslateYPoint(m_PointYs[index]))).ToList();

            //构造组成计划区域的点坐标集合
            for (int index = 0; index < qimoPoints.Count - 1; index++)
			{
                polygonPoints.Add(qimoPoints[index]);
                Point vitualPoint = GetThridPoint(qimoPoints[index], qimoPoints[index + 1]);
                polygonPoints.Add(vitualPoint);
			}

            //polygonPoints.Add(qimoPoints[qimoPoints.Count - 2]);
            polygonPoints.Add(qimoPoints[qimoPoints.Count - 1]);
            Point endPoint = new Point
            {
                X = qimoPoints[qimoPoints.Count - 1].X,
                Y = m_Rect.Bottom
            };
            polygonPoints.Add(endPoint);
            polygonPoints.Add(starPoint);

            return polygonPoints;
        }

        /// <summary>
        /// 根据两点构造第三点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private Point GetThridPoint(Point point1, Point point2)
        {
            Point returnPoint = new Point
            {
                X = point2.X,
                Y = point1.Y
            };

            return returnPoint;
        }

        /// <summary>
        /// 根据起模线进行切割
        /// </summary>
        /// <param name="polyPoints"></param>
        private void AddPointByQiMoXian(List<Point> polyPoints)
        {
            for (int index = 0; index < polyPoints.Count; index++)
            {
                if (polyPoints[index].X > m_QiMo)
                {
                    if(index>0)
                    {
                        if (polyPoints[index].Y > polyPoints[index - 1].Y)
                        {
                            Point inserPoint = new Point
                            {
                                X = m_QiMo,
                                Y = polyPoints[index - 1].Y
                            };
                            polyPoints.Insert(index, inserPoint);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取MSRP点
        /// </summary>
        /// <param name="polyPoints"></param>
        /// <returns></returns>
        private List<int> GetMsrpPoint(List<Point> polyPoints)
        {
            List<int> msrpPoints = new List<int>();
            for (int index = 0; index < polyPoints.Count - 2;index++ )
            {
                if (polyPoints[index].X == polyPoints[index + 1].X)
                {
                    msrpPoints.Add(polyPoints[index].X);
                }
            }

            return msrpPoints;
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            #region D 区 的 绘 制

            if (ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS2 && ATPMain.Instance.ControlModel == ControModelEnum.完全) //
            {
                g.FillRectangle(ATP200CCommon.DarkBlueBrush, ATP200CCommon.RectD[0].X + 5, ATP200CCommon.RectD[0].Y + 150, 280, 155);

                //if (GT_Main.Model == ControModelEnum.完全)//处于完全模式才显示计划区
                //{
                    List<Point> pointList = GetPolygonPoints();
                    AddPointByQiMoXian(pointList);
                    if (pointList.Count >= 4)
                    {
                        g.FillPolygon(ATP200CCommon.LightBlueBrush, pointList.ToArray());
                    }

                    List<int> msrpXs = GetMsrpPoint(pointList);
                    foreach (int pointX in msrpXs)
                    {
                        g.DrawLine(ATP200CCommon.WhitePen2, pointX, ATP200CCommon.RectD[0].Y + 295, pointX, ATP200CCommon.RectD[0].Y + 315);//绘制Mrsp变化点  
                    }
                //}
                 
                #region 绘制刻度
                for (int i = 0; i < 6; i++)
                {
                    g.DrawLine(ATP200CCommon.WhitePen2,
                        ATP200CCommon.RectD[0].X + 5,
                        ATP200CCommon.RectD[0].Y + 155 + i * 30,
                        ATP200CCommon.RectD[0].X + 285,
                        ATP200CCommon.RectD[0].Y + 155 + i * 30);
                    g.DrawString(( 50 * ( 5 - i ) ).ToString(),
                        ATP200CCommon.Font12B,
                        ATP200CCommon.WhiteBrush,
                        ATP200CCommon.RectD[0].X + 3,
                        ATP200CCommon.RectD[0].Y + 155 + i * 30);
                }
                g.DrawLine(ATP200CCommon.WhitePen2, ATP200CCommon.RectD[0].X + 5, ATP200CCommon.RectD[0].Y + 155, ATP200CCommon.RectD[0].X + 5, ATP200CCommon.RectD[0].Y + 335);
                g.DrawString("0", ATP200CCommon.Font14B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectD[0].X - 2, ATP200CCommon.RectD[0].Y + 345);
                for (int i = 0; i < 4; i++)
                {
                    g.DrawLine(ATP200CCommon.WhitePen2,
                        ATP200CCommon.RectD[0].X + 135 + i * 45,
                        ATP200CCommon.RectD[0].Y + 155,
                        ATP200CCommon.RectD[0].X + 135 + i * 45,
                        ATP200CCommon.RectD[0].Y + 335);
                    g.DrawString(m_Distance[i], ATP200CCommon.Font14B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectD[0].X + 115 + i * 45, ATP200CCommon.RectD[0].Y + 345);
                }
                #endregion

               
                g.DrawLine(ATP200CCommon.YellowPen3, m_QiMo, ATP200CCommon.RectD[0].Y + 145, m_QiMo, ATP200CCommon.RectD[0].Y + 335);//绘制起模点位置
            }

            if (ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS0 ||
                ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS1 ||
                ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS2) //CTCS处于0级或1级显示机车信号
            {
                if (m_SinalLevel == 8 || m_SinalLevel == 11 || m_SinalLevel == 12)
                {
                    if (CurrentTime.Second % 2 != 0)
                    {
                        g.DrawImage(m_Image[m_SinalLevel], m_SignalViewRegion);
                    }
                }
                else if (m_SinalLevel >= 0 && m_SinalLevel < 19)
                {
                    g.DrawImage(m_Image[m_SinalLevel], m_SignalViewRegion);
                }
            }

            

            #endregion
        }
        #endregion
    }
}
