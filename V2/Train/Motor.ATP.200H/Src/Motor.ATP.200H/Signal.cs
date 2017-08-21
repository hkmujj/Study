using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Signal : ShowTypeEffectBaseClass
    {
        private readonly string[] m_FText = new string[8] { "数据", "模式", "载频", "等级", "音量\n亮度", "启动", "缓解", "警惕" };
        //主视图F 区显示的文本

        private readonly string[] m_Distance = new string[4] { "1000", "2000", "4000", "8000" };
        private readonly RectText[] m_GText = new RectText[8];
        private Image[] m_Image;
        private int m_CTCSLevel; //CSCS等级
        private int m_SinalLevel; //信号等级
        private int m_QiMo; //起模点横坐标

        public static bool IsConfigFlag { get; set; }

        private ConfigInfo m_ConfigInfo;

        private RectText m_ConfigTextInfo;

        private int m_ClearCount;

        #region 起 摸 区 域 元素

        private Rectangle m_Rect;

        /// <summary>
        /// 期摸点X坐标集合
        /// </summary>
        private readonly List<float> m_PointXs = new List<float>();

        /// <summary>
        /// 期模点Y坐标集合
        /// </summary>
        private readonly List<float> m_PointYs = new List<float>();

        private Pen m_ConfirmPen;

        #endregion

        private int[] m_DistanceIndex;

        private int[] m_SpeedIndex;

        #region  重载方法

        public override string GetInfo()
        {
            return "机车信号及速度变化";
        }

        protected override void Initalize()
        {
            m_ConfirmPen = new Pen(Color.FromArgb(255, 255, 0), 1);

            m_DistanceIndex = GetInstanceIndexs().ToArray();
            m_SpeedIndex = GetSpeedIndexs().ToArray();

            for (var i = 0; i < 8; i++)
            {
                m_GText[i] = new RectText();
                m_GText[i].SetBkColor(0, 0, 0);
                m_GText[i].SetTextColor(255, 255, 255);
                m_GText[i].SetTextStyle(16, FormatStyle.Center, true, "宋体");
                m_GText[i].SetTextRect(Common2D.RectF[i].X + 2, Common2D.RectF[i].Y + 2, Common2D.RectF[i].Width - 4,
                    Common2D.RectF[i].Height - 4);
            }

            m_Image = new Image[]
            {
                ImageResource.L5, ImageResource. L4, ImageResource. L3, ImageResource. L2, ImageResource. L, ImageResource. LU, ImageResource. U, ImageResource. U2, ImageResource. U2S, ImageResource.  HU, ImageResource. UU, ImageResource. UUS, ImageResource.  HB, ImageResource.  H, ImageResource.  HZ, ImageResource. LU2, ImageResource. U3, ImageResource.  HZ2, 
            };

            m_Rect = new Rectangle(Common2D.RectD[0].X + 5, Common2D.RectD[0].Y + 150, 280, 155);

            m_ConfigTextInfo = new RectText();
            m_ConfigTextInfo.SetBkColor(0, 0, 0);
            m_ConfigTextInfo.SetTextColor(255, 255, 255);
            m_ConfigTextInfo.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_ConfigTextInfo.SetTextRect(Common2D.RectE[4].X + 2, Common2D.RectE[4].Y + 3, 220,
                Common2D.RectE[4].Height - 6);
            m_ConfigTextInfo.SetLinePen(255, 255, 0, 1);
        }

        private IEnumerable<int> GetInstanceIndexs()
        {
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息1);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息2);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息3);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息4);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息5);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息6);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息7);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息8);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息9);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息10);

        }

        private IEnumerable<int> GetSpeedIndexs()
        {
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息1);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息2);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息3);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息4);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息5);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息6);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息7);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息8);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息9);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息10);

        }

        public override void paint(Graphics g)
        {
            RefreshShowType();

            RefreshConigInfo();

            UpdateValue();

            GetQiMoDianPoint();
            //getPolygonPoints();
            ButtonEvent();

            OnDraw(g);

            ClearSendFlag();

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取数据
        /// </summary>
        private void UpdateValue()
        {
            m_CTCSLevel = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.InfCTCS等级)]);

            m_SinalLevel = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf机车信号)]) - 100;

            m_QiMo = Convert.ToInt32(TranslateXPoint(FloatList[this.GetInFloatIndex(InFloatKeys.Inf起模点横坐标)]));

            //菜单文本

            if (!IsConfigFlag)
            {
                for (var index = 0; index < 8; index++)
                {
                    m_GText[index].SetText(m_FText[index]);
                }
            }
            else
            {
                for (var index = 0; index < 8; index++)
                {
                    m_GText[index].SetText(index == 7 ? "确认" : string.Empty);
                }
            }


        }

        /// <summary>
        /// 刷新确认信息
        /// </summary>
        private void RefreshConigInfo()
        {
            foreach (var configInfo in TextInfo.m_ConfigInfos.Where(configInfo => BoolList[configInfo.ReceiveIndex]))
            {
                IsConfigFlag = true;
                m_ConfigInfo = configInfo;
                return;
            }

            m_ConfigInfo = null;
            IsConfigFlag = false;
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
            if (x > 0 && x < 1000)
            {
                return Common2D.RectD[0].X + 5 + x * 135 / 1000;
            }
            if (x >= 1000 && x < 2000)
            {
                return Common2D.RectD[0].X + 90 + x * 45 / 1000;
            }
            if (x >= 2000 && x < 4000)
            {
                return Common2D.RectD[0].X + 135 + x * 45 / 2000;
            }
            if (x >= 4000 && x < 8000)
            {
                return Common2D.RectD[0].X + 180 + x * 45 / 4000;
            }
            return Common2D.RectD[0].X + 270;
        }

        /// <summary>
        /// 将实际高度转换为屏上的纵坐标
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private float TranslateYPoint(float y)
        {
            if (y > 0 && y <= 250)
            {
                return Common2D.RectD[0].Y + 305 - y * 150 / 250;
            }
            return Common2D.RectD[0].Y + 305;
        }

        #endregion

        /// <summary>
        /// 获取起模点集合
        /// </summary>
        private void GetQiMoDianPoint()
        {
            m_PointXs.Clear(); //清空点集
            m_PointYs.Clear();

            for (var index = 0; index < 10; index++)
            {
                var pointX = FloatList[m_DistanceIndex[index]];

                if (index == 0)
                {
                    if (pointX < 0)
                    {
                        pointX = 0;
                    }
                    m_PointXs.Add(pointX);
                    m_PointYs.Add(FloatList[m_SpeedIndex[index]]);
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
                        var pointY = FloatList[m_SpeedIndex[index]];
                        if (pointY > m_PointYs[index - 1])
                        {
                            pointY = m_PointYs[index - 1];
                        }
                        m_PointYs.Add(pointY);
                        break;
                    }
                    m_PointXs.Add(pointX);
                    m_PointYs.Add(FloatList[m_SpeedIndex[index]]);
                }
            }
        }

        /// <summary>
        /// 根据限速点 得到起模点集合
        /// </summary>
        private List<PointF> GetPolygonPoints()
        {
            var polygonPoints = new List<PointF>();
            var starPoint = new Point(m_Rect.X, m_Rect.Bottom);

            if (m_PointXs.Count < 2)
            {
                return polygonPoints;
            }

            polygonPoints.Add(starPoint);

            //获取所有起模点集合
            var qimoPoints =
                m_PointXs.Select((t, index) => new PointF(TranslateXPoint(t), TranslateYPoint(m_PointYs[index])))
                    .ToList();

            //构造组成计划区域的点坐标集合
            for (var index = 0; index < qimoPoints.Count - 1; index++)
            {
                polygonPoints.Add(qimoPoints[index]);
                var vitualPoint = GetThridPoint(qimoPoints[index], qimoPoints[index + 1]);
                polygonPoints.Add(vitualPoint);
            }

            polygonPoints.Add(qimoPoints[qimoPoints.Count - 1]);

            var endPoint = new PointF
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
        private PointF GetThridPoint(PointF point1, PointF point2)
        {
            var returnPoint = new PointF
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
        private void AddPointByQiMoXian(List<PointF> polyPoints)
        {
            for (var index = 0; index < polyPoints.Count; index++)
            {
                if (polyPoints[index].X > m_QiMo && (index > 0 && polyPoints[index].Y > polyPoints[index - 1].Y))
                {
                    var inserPoint = new PointF { X = m_QiMo, Y = polyPoints[index - 1].Y };
                    polyPoints.Insert(index, inserPoint);
                    break;
                }
            }
        }

        /// <summary>
        /// 获取MSRP点
        /// </summary>
        /// <param name="polyPoints"></param>
        /// <returns></returns>
        private List<float> GetMsrpPoint(List<PointF> polyPoints)
        {
            var msrpPoints = new List<float>();
            for (var index = 0; index < polyPoints.Count - 2; index++)
            {
                if (Math.Abs(polyPoints[index].X - polyPoints[index + 1].X) < float.Epsilon)
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

            if (ShowType == ShowType.Normal)
            {
                DrawAreaD(g);
            }

            DrawAreaF(g);
        }

        private void DrawAreaF(Graphics g)
        {
            // F 区 按 钮 信 息
            for (var i = 0; i < 8; i++)
            {
                m_GText[i].OnDraw(g);
            }
        }

        private void DrawAreaD(Graphics g)
        {
            if (m_CTCSLevel == 2 && (Main.CurrentModel == ControModelEnum.完全 || Main.CurrentModel == ControModelEnum.部分))
            {
                g.FillRectangle(Common2D.DarkBlueBrush, Common2D.RectD[0].X + 5, Common2D.RectD[0].Y + 150, 280, 155);

                if (Main.CurrentModel == ControModelEnum.完全)
                {
                    DrawSpeedPlan(g);
                }

                DrawDegrees(g);

                g.DrawLine(Common2D.YellowPen3, m_QiMo, Common2D.RectD[0].Y + 145, m_QiMo, Common2D.RectD[0].Y + 335);
                //绘制起模点位置
            }

            if (m_CTCSLevel == 0 || m_CTCSLevel == 1 || m_CTCSLevel == 2) //CTCS处于0级或1级显示机车信号
            {
                DrawCabSignal(g);
            }

            if (IsConfigFlag)
            {
                DrawConfirmInfo(g);
            }
        }

        private void DrawConfirmInfo(Graphics g)
        {
            if (m_ConfigInfo != null)
            {
                m_ConfigTextInfo.SetText(m_ConfigInfo.ShowInfo);
            }

            m_ConfigTextInfo.OnDraw(g);

            if (this.CurrenTime().Second % 2 == 0)
            {
                g.DrawRectangle(m_ConfirmPen, m_ConfigTextInfo.m_RectPosition);
            }
        }

        private void DrawCabSignal(Graphics g)
        {
            var sinalSize = new Size(80, 80);
            if (m_SinalLevel == 8 || m_SinalLevel == 11 || m_SinalLevel == 12)
            {
                if (this.CurrenTime().Second % 2 != 0)
                {
                    g.DrawImage(m_Image[m_SinalLevel], Common2D.RectD[0].X + 100, Common2D.RectD[0].Y + 170,
                        sinalSize.Width, sinalSize.Height);
                }
            }
            else if (m_SinalLevel >= 0 && m_SinalLevel < 19)
            {
                g.DrawImage(m_Image[m_SinalLevel], Common2D.RectD[0].X + 100, Common2D.RectD[0].Y + 170,
                    sinalSize.Width, sinalSize.Height);
            }
        }

        private void DrawSpeedPlan(Graphics g)
        {
            var pointList = GetPolygonPoints();
            AddPointByQiMoXian(pointList);
            if (pointList.Count >= 4)
            {
                g.FillPolygon(Common2D.LightBlueBrush, pointList.ToArray());
            }

            var msrpXs = GetMsrpPoint(pointList);
            foreach (var pointX in msrpXs)
            {
                g.DrawLine(Common2D.WhitePen2, pointX, Common2D.RectD[0].Y + 295, pointX,
                    Common2D.RectD[0].Y + 315); //绘制Mrsp变化点  
            }
        }

        private void DrawDegrees(Graphics g)
        {
            for (var i = 0; i < 6; i++)
            {
                g.DrawLine(Common2D.WhitePen2,
                    Common2D.RectD[0].X + 5,
                    Common2D.RectD[0].Y + 155 + i * 30,
                    Common2D.RectD[0].X + 285,
                    Common2D.RectD[0].Y + 155 + i * 30);
                g.DrawString((50 * (5 - i)).ToString(),
                    Common2D.Font12B,
                    Common2D.WhiteBrush,
                    Common2D.RectD[0].X + 3,
                    Common2D.RectD[0].Y + 155 + i * 30);
            }
            g.DrawLine(Common2D.WhitePen2, Common2D.RectD[0].X + 5, Common2D.RectD[0].Y + 155,
                Common2D.RectD[0].X + 5, Common2D.RectD[0].Y + 335);
            g.DrawString("0", Common2D.Font14B, Common2D.WhiteBrush, Common2D.RectD[0].X - 2,
                Common2D.RectD[0].Y + 345);
            for (var i = 0; i < 4; i++)
            {
                g.DrawLine(Common2D.WhitePen2,
                    Common2D.RectD[0].X + 135 + i * 45,
                    Common2D.RectD[0].Y + 155,
                    Common2D.RectD[0].X + 135 + i * 45,
                    Common2D.RectD[0].Y + 335);
                g.DrawString(m_Distance[i], Common2D.Font14B, Common2D.WhiteBrush, Common2D.RectD[0].X + 115 + i * 45,
                    Common2D.RectD[0].Y + 345);
            }
        }

        //响应硬件按钮事件
        private void ButtonEvent()
        {
            for (var index = 0; index < 8; index++)
            {
                if (ButtonStatus.m_IsRightButtonUp[index])
                {
                    if (IsConfigFlag) //确认模式
                    {
                        if (m_ConfigInfo == null)
                        {
                            return;
                        }

                        if (index == 7)
                        {
                            m_ClearCount = 0;
                            append_postCmd(CmdType.SetBoolValue, m_ConfigInfo.SendIndex, 1, 0);
                        }
                    }

                    #region 普通模式

                    else
                    {
                        switch (index)
                        {
                            case 0:
                                append_postCmd(CmdType.ChangePage, 2, 0, 0);
                                break;
                            case 1:
                                append_postCmd(CmdType.ChangePage, 3, 0, 0);
                                break;
                            case 2:
                                append_postCmd(CmdType.ChangePage, 4, 0, 0);
                                break;
                            case 3:
                                append_postCmd(CmdType.ChangePage, 5, 0, 0);
                                break;
                            case 4:
                                append_postCmd(CmdType.ChangePage, 6, 0, 0);
                                break;
                            case 5:
                                if (Main.CurrentModel == ControModelEnum.待机)
                                {
                                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                                    ConfirmModelView.m_TheModelSelect = ConfirmModel.启动;
                                }
                                break;
                            case 6:
                                append_postCmd(CmdType.ChangePage, 7, 0, 0);
                                ConfirmModelView.m_TheModelSelect = ConfirmModel.缓解;
                                break;
                            case 7:
                                append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub警惕), 1, 0);
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion
                }
                else if (ButtonStatus.m_IsBottomButtonUp[index] && (index == 6 && IsConfigFlag))
                {
                    if (m_ConfigInfo != null)
                    {
                        m_ClearCount = 0;
                        append_postCmd(CmdType.SetBoolValue, m_ConfigInfo.SendIndex, 1, 0);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 清空输出信息
        /// </summary>
        private void ClearSendFlag()
        {
            if (m_ClearCount == 4)
            {
                if (TextInfo.m_ConfigInfos != null)
                {
                    foreach (var configInfo in TextInfo.m_ConfigInfos)
                    {
                        append_postCmd(CmdType.SetBoolValue, configInfo.SendIndex, 0, 0);
                    }
                    m_ClearCount = 0;
                }
            }

            m_ClearCount++;
        }

        #endregion
    }
}
