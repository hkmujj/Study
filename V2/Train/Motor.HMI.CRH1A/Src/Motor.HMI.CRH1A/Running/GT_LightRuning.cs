using System;
using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Running.DialStrategy;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Running
{
    /// <summary>
    /// 夜间运行模式 只显示时间和牵引制动指令相关信息    
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_LightRuning : CRH1BaseClass
    {
        Rectangle m_Recposition = new Rectangle(3, 185, 790, 260);
        Rectangle m_Clockrect;
        PointF m_Circle;//圆心位置
        PointF[] m_Points = new PointF[60];
        float m_Radius = 125.0F;//时钟半径
        int m_Hour = 0;
        int m_Minu = 0;
        int m_Second = 0;
        PointF[] m_PointH = new PointF[4];//时针位置
        PointF[] m_PointM = new PointF[4];//分针位置

        SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(46, 139, 87));
        SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        Font m_NoFont = new Font("Arial", 10);
        GDIRectText m_GText;//分别显示动力和温度

        Pen m_GreenPen1 = new Pen(Color.FromArgb(46, 139, 87), 2);
        Pen m_GreenPen2 = new Pen(Color.FromArgb(46, 139, 87), 3);

        Rectangle m_ButtonRect;
        float m_Valuef;
        Image m_Image;

        /// <summary>
        /// 制动等级
        /// </summary>
        //private bool[] _isBrakeLevel = new bool[7];

        //制动等级解析
        private IFloatValueExpression m_ValueExpression;

        private IDialStrategy m_DialStrategy;

        public override string GetInfo()
        {
            return "夜间运行模式";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            //3
            m_ValueExpression = new TowBrakeValueExpression();
            m_DialStrategy = new NormalDialStrategy()
            {
                DrawDarkAction = (g, i) => g.FillRectangle(m_BlackBrush, m_Points[i].X, m_Points[i].Y, 6, 6),
                DrawLightAction = (g, i) => g.FillRectangle(m_GreenBrush, m_Points[i].X, m_Points[i].Y, 6, 6),
            };
            if (UIObj.ParaList.Count > 0)
                // for(int i=0;i<3;i++)
                // {
                m_Image = Image.FromFile(RecPath + "//" + UIObj.ParaList[0]);
            // }
            InitData();
            return true;

        }
        public override void paint(Graphics g)
        {
            GetValue();
            ReFreshData();
            DrawOn(g);
        }
        public void GetValue()
        {
            if (UIObj.InFloatList.Count >= 1)
            {

                m_Valuef = FloatList[UIObj.InFloatList[0]];
            }
            DateTime date = CurrenTime;
            m_Hour = Convert.ToInt32(date.Hour);
            m_Minu = Convert.ToInt32(date.Minute);
            m_Second = Convert.ToInt32(date.Second);
            //时针位置
            m_PointH[0] = new PointF(m_Circle.X, m_Circle.Y);
            m_PointH[1] = new PointF(m_Circle.X + (float)((m_Radius - 85) * Math.Sin(m_Hour * 0.5235 - 0.35 + m_Minu * 0.00873)), m_Circle.Y - (float)((m_Radius - 85) * Math.Cos(m_Hour * 0.5235 - 0.35 + m_Minu * 0.00873)));
            m_PointH[2] = new PointF(m_Circle.X + (float)((m_Radius - 40) * Math.Sin(m_Hour * 0.5235 + m_Minu * 0.00873)), m_Circle.Y - (float)((m_Radius - 40) * Math.Cos(m_Hour * 0.5235 + m_Minu * 0.00873)));
            m_PointH[3] = new PointF(m_Circle.X + (float)((m_Radius - 85) * Math.Sin(m_Hour * 0.5235 + 0.35 + m_Minu * 0.00873)), m_Circle.Y - (float)((m_Radius - 85) * Math.Cos(m_Hour * 0.5235 + 0.35 + m_Minu * 0.00873)));

            //分针位置
            m_PointM[0] = new PointF(m_Circle.X, m_Circle.Y);
            m_PointM[1] = new PointF(m_Circle.X + (float)((m_Radius - 90) * Math.Sin(6 * m_Minu * 0.0175 - 0.25 + m_Second * 0.001745)), m_Circle.Y - (float)((m_Radius - 90) * Math.Cos(6 * m_Minu * 0.0175 - 0.25 + m_Second * 0.001745)));
            m_PointM[2] = new PointF(m_Circle.X + (float)((m_Radius - 15) * Math.Sin(6 * m_Minu * 0.0175 + m_Second * 0.001745)), m_Circle.Y - (float)((m_Radius - 15) * Math.Cos(6 * m_Minu * 0.0175 + m_Second * 0.001745)));
            m_PointM[3] = new PointF(m_Circle.X + (float)((m_Radius - 90) * Math.Sin(6 * m_Minu * 0.0175 + 0.25 + m_Second * 0.001745)), m_Circle.Y - (float)((m_Radius - 90) * Math.Cos(6 * m_Minu * 0.0175 + 0.25 + m_Second * 0.001745)));

            //for (int index = 0; index < 7; index++)
            //{
            //    _isBrakeLevel[index] = BoolList[UIObj.InBoolList[index]];
            //}
        }
        public void InitData()
        {
            //设置钟的显示位置
            m_Clockrect = new Rectangle(m_Recposition.X + 250, m_Recposition.Y + 10, 280, 240);
            m_Circle = new PointF(m_Clockrect.X + 130, m_Clockrect.Y + 120);
            for (int i = 0; i < 60; i++)
            {

                m_Points[i] = new PointF(m_Circle.X - 2 + (float)(m_Radius * Math.Sin(6 * i * 0.0175)), m_Circle.Y - (float)(m_Radius * Math.Cos(6 * i * 0.0175)));
            }

            //两边信息显示部分
            m_GText = new GDIRectText();
            m_GText.SetBkColor(0, 0, 0);
            m_GText.SetTextColor(46, 139, 87);
            m_GText.SetTextStyle(14, FormatStyle.Center, true, "Arial");
            m_GText.SetTextRect(m_Recposition.X + 90, m_Recposition.Y + 125, 75, 35);
            m_GText.SetDrawFrm(true);
            m_GText.SetLinePen(46, 139, 87, 2);

            //底部按钮初始化
            for (int i = 0; i < 4; i++)
            {

                m_ButtonRect = new Rectangle(m_Recposition.X + 627, m_Recposition.Y + 226, 80, 50);
            }

        }

        public void ReFreshData()
        {
            m_GText.SetText(m_ValueExpression.Interprete(m_Valuef));
        }
        public void DrawOn(Graphics g)
        {
            g.DrawString("牵引/制动指令", new Font("Arial", 13), m_GreenBrush, m_Recposition.X + 80, m_Recposition.Y + 100);
            m_GText.OnDraw(g);



            //绘制时钟表盘
            m_DialStrategy.Display(g, m_Second);

            for (int i = 0; i < 12; i++)
            {
                if (i < 8 && i != 0 && i != 6)
                {
                    g.DrawLine(m_GreenPen1, m_Circle.X + (float)((m_Radius - 5) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 5) * Math.Cos(i * 0.5235)), m_Circle.X +
                                                                                                                                                             (float)((m_Radius - 15) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 15) * Math.Cos(i * 0.5235)));
                }
                else if (i >= 8)
                {
                    g.DrawLine(m_GreenPen1, m_Circle.X + (float)((m_Radius - 10) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 10) * Math.Cos(i * 0.5235)), m_Circle.X +
                                                                                                                                                               (float)((m_Radius - 20) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 20) * Math.Cos(i * 0.5235)));
                }
                else
                {
                    g.DrawLine(m_GreenPen2, m_Circle.X + (float)((m_Radius - 10) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 10) * Math.Cos(i * 0.5235)), m_Circle.X +
                                                                                                                                                               (float)((m_Radius - 20) * Math.Sin(i * 0.5235)), m_Circle.Y - (float)((m_Radius - 20) * Math.Cos(i * 0.5235)));
                }
            }
            g.DrawString("1 2", m_NoFont, m_GreenBrush, m_Circle.X - 10, m_Circle.Y - m_Radius + 20);
            g.DrawString("3", m_NoFont, m_GreenBrush, m_Circle.X + (float)((m_Radius - 28) * Math.Sin(3 * 0.5235)), m_Circle.Y - 7 - (float)((m_Radius - 10) * Math.Cos(3 * 0.5235)));
            g.DrawString("6", m_NoFont, m_GreenBrush, m_Circle.X - 6, m_Circle.Y + m_Radius - 35);
            g.DrawString("9", m_NoFont, m_GreenBrush, m_Circle.X - 3 + (float)((m_Radius - 28) * Math.Sin(9 * 0.5235)), m_Circle.Y - 7 - (float)((m_Radius - 10) * Math.Cos(9 * 0.5235)));

            //绘制时针
            g.FillPolygon(m_GreenBrush, m_PointH);
            g.DrawLines(m_GreenPen1, m_PointH);

            g.FillPolygon(m_GreenBrush, m_PointM);
            g.DrawLines(m_GreenPen1, m_PointM);

            //绘制返回按钮
            //g.DrawRectangle(green_Pen2, Button_Rect);
            //g.DrawString("返 回", new Font("Arial", 13), green_Brush, Button_Rect.X + 10, Button_Rect.Y + 10);
        }

        protected override bool OnMouseUp(Point point)
        {
            OnPost(CmdType.ChangePage, (int) ViewConfig.Running, 0, 0);
            //OnButtonUp(nPoint.X, nPoint.Y);
            return true;
        }
    }
}