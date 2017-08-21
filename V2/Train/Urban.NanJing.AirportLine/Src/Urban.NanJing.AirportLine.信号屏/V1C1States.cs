using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C1States : baseClass
    {
        private Timer m_Timer;
        private String m_Dwell = String.Empty;
        private Int32 m_TimeCountDown = 0;
        private Int32 m_TimeCount = 999;
        private Boolean m_IsCountDownFlag = false;

        private String[] m_Modes;
        private String[] m_DockedStatus;
        private String[] m_DepartStatus;
        private List<Image> m_Images = new List<Image>();

        public override string GetInfo()
        {
            return "状态栏";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            m_Modes = new String[9] {"WM", "OFF", "ATO", "ATPM", "IATP", "RMF", "RMR", "DTB", "" };
            m_DockedStatus = new String[4] { "YES", "NO", "", "---" };
            m_DepartStatus = new String[4] { "NO", "YES", "", "---" };

            m_Timer = new Timer();
            m_Timer.Interval = 1000;
            m_Timer.Tick += _timer_Tick;

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + a, FileMode.Open))
                {
                    m_Images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (m_IsCountDownFlag)
            {
                if (m_TimeCountDown < 1000)
                {
                    m_TimeCountDown++;
                    m_Dwell = m_TimeCountDown.ToString();
                }
                else
                {
                    m_Timer.Stop();
                    m_BrushDwell = Brushes.Yellow;
                    m_Dwell = "<>";
                }
            }
            else
            {
                if (m_TimeCount == 0)
                {
                    m_Dwell = m_TimeCount.ToString();
                    m_Timer.Stop();
                }
                else m_TimeCount--;

                if (m_TimeCount > 5)
                {
                    m_BrushDwell = Brushes.Yellow;
                }
                else
                {
                    m_BrushDwell = Brushes.Red;
                }
                m_Dwell = m_TimeCount.ToString();
            }
        }

        public override void paint(Graphics g)
        {
            DrawMode(g);       //模式状态
            DrawDeckedStatu(g);//停靠状态
            DrawDepartState(g);//发车状态
            DrawDwell(g);      //停站状态

            base.paint(g);
        }

        private String DrawMode(Graphics g)
        {
            String mode = String.Empty;

            for (int i = 0; i < 9; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    g.DrawString(m_Modes[i], Global.m_FontArial16B, Global.m_SbBlue, new PointF(115, 15));
                    mode = m_Modes[i];
                    break;
                }
            }

            return mode;
        }

        private void DrawDeckedStatu(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Brush b = Brushes.Lime;
                    if (i == 3)
                        b = Brushes.Red;
                    g.DrawString(m_DockedStatus[i], Global.m_FontArial16B, b, new PointF(325, 15));
                    break;
                }
            }
        }
        private Brush m_BrushDwell = Brushes.Yellow;
        /// <summary>
        /// 绘制停站状态
        /// </summary>
        /// <param name="g"></param>
        private void DrawDwell(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    if (i == 0)
                    {
                        m_IsCountDownFlag = true;
                        m_BrushDwell = Brushes.Red;
                        m_Timer.Start();
                        m_TimeCount = 31;
                    }
                    else if (i == 1)
                    {
                        m_IsCountDownFlag = false;
                        m_Timer.Start();
                        m_TimeCountDown = 0;
                    }
                    else
                    {
                        m_IsCountDownFlag = false;
                        m_Timer.Stop();
                        m_TimeCountDown = 0;
                        m_TimeCount = 31;
                        m_Dwell = "";
                    }

                    break;
                }
            }

            //if (this._isCountDownFlag)
            //{
            //    g.DrawImage(this._images[0], new PointF(540 - 17f, 3));
            //}
            g.DrawString(
                m_Dwell,
                Global.m_FontArial16B,
                m_BrushDwell,
                new RectangleF(540 - 17, 3+3, 53, 50),
                Global.m_SfCc
                );
        }

        private void DrawDepartState(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    Brush b = Brushes.Red;
                    if (i == 1)
                        b = Brushes.Lime;
                    g.DrawString(m_DepartStatus[i], Global.m_FontArial16B, b, new PointF(735, 15));
                    break;
                }
            }
        }
    }
}
