using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class OpenScreen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "开放画面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();
            return true;
        }

        #endregion#

        #region ::::::::::::::::::::::::event override:::::::::::::::::::::::::::::#
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>( Common.Str203);
            }
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            for (; index < 11; index++)
            {
                if (m_Rect[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    ButtonColorChange(0);
                    break;
                case 1:
                    ButtonColorChange(1);
                    break;
                case 2:
                    ButtonColorChange(2);
                    break;
                case 3:
                    ButtonColorChange(3);
                    break;
                case 4:
                    ButtonColorChange(4);
                    break;
                case 5:
                    ButtonColorChange(5);
                    break;
                case 6:
                    ButtonColorChange(6);
                    break;
                case 7:
                    ButtonColorChange(7);
                    break;
                case 8:
                    ButtonColorChange(8);
                    break;
                case 9:
                    ButtonColorChange(9);
                    break;
                case 10:
                    ButtonDownAndSendValue();
                    break;
            }
            return true;
        }

        #endregion#

        #region :::::::::::::::::::::::: ex funes ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
                m_OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                m_SetBValue[i] = BoolList[UIObj.OutBoolList[i]];
                m_SetBValueNumb[i] = UIObj.OutBoolList[i];
            }

        }

        /// <summary>
        /// 按开放之后发送数据
        /// </summary>
        private void ButtonDownAndSendValue()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_BValue[i] && m_BValue[i] != m_ButtonState[i])
                {
                    append_postCmd(CmdType.SetBoolValue, m_SetBValueNumb[i], 0, 0);
                }
                else if (!m_BValue[i] && m_BValue[i] != m_ButtonState[i])
                {
                    append_postCmd(CmdType.SetBoolValue, m_SetBValueNumb[i], 1, 0);
                }

                m_ChangeState[i] = true;
                m_ButtonOldState[i] = false;
            }
        }
        
        /// <summary>
        /// 按键颜色变化
        /// </summary>
        /// <param name="index"></param>
        private void ButtonColorChange(int index)
        {
            m_ChangeState[index] = false;
            if (!m_BValue[index] && !m_ButtonOldState[index])
            {
                m_ButtonState[index] = true;
                m_ButtonOldState[index] = m_ButtonState[index];
            }
            else if (!m_BValue[index] && m_ButtonOldState[index])
            {
                m_ButtonState[index] = false;
                m_ButtonOldState[index] = m_ButtonState[index];
            }
            else if (m_BValue[index] && !m_ButtonOldState[index])
            {
                m_ButtonState[index] = false;
                m_ButtonOldState[index] = !m_ButtonState[index];
            }
            else if (m_BValue[index] && m_ButtonOldState[index])
            {
                m_ButtonState[index] = true;
                m_ButtonOldState[index] = !m_ButtonState[index];
            }
        }

        /// <summary>
        /// 开放状态
        /// </summary>
        /// <param name="e"></param>
        private void DrawState(Graphics e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_BValue[i])
                {
                    e.FillRectangle(Common.RedBrush, m_Rects[i]);
                    e.DrawString(i > 5 ? "隔离" : "开放", Common.Txt14FontR,
                        Common.WhiteBrush, m_Rects[i], Common.DrawFormat);
                    //没按按键，根据状态画图
                    if (m_ChangeState[i])
                    {
                        e.FillRectangle(Common.BlueBrush, m_Rects[10 + i]);
                    }
                }
                else
                {
                    e.FillRectangle(Common.GreenBrush, m_Rects[i]);
                    e.DrawString("正常", Common.Txt14FontR, Common.BlackBrush,
                        m_Rects[i], Common.DrawFormat);
                }

                if (!m_ChangeState[i] && m_ButtonState[i])
                {
                    e.FillRectangle(Common.BlueBrush, m_Rects[10 + i]);
                }
            }
            //白线
            for (int i = 0; i < 10; i++)
            {
                e.DrawLine(Common.WhitePen1, m_PDrawPoint[i * 2], m_PDrawPoint[i * 2 + 1]);
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawState(e);

            for (int i = 0; i < 10; i++)
            {
                m_Buttons[i].DrawRectButtonNoFillAndNoState(e);
                e.DrawString(m_Str1[i], Common.Txt14FontR, Common.WhiteBrush, m_Rects[20 + i], Common.DrawFormat);
            }
            m_Buttons[10].DrawRectButoonFillAndNoState(e);
            e.DrawString("开放", Common.Txt12FontB, Common.WhiteBrush, m_Rects[31], Common.DrawFormat);
        }

        #endregion#

        #region:::::::::::::::所有坐标点的初始化、表盘和进度条的初始化:::::::::::::::#
        /// <summary>
        /// 初始化坐标点
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::坐标点:::::::::::::::::::::::::::::#
            for (int i = 0; i < 6; i++)
            {
                m_PDrawPoint[i * 2] = new Point(74 + 75 * i, 290);
                m_PDrawPoint[i * 2 + 1] = new Point(74 + 75 * i, 340);
            }
            for (int i = 0; i < 2; i++ )
            {
                m_PDrawPoint[12 + i * 2] = new Point(74 + 75 * i, 410);
                m_PDrawPoint[16 + i * 2] = new Point(249 + 100 * i, 410);
                m_PDrawPoint[12 + i * 2 + 1] = new Point(74 + 75 * i, 460);
                m_PDrawPoint[16 + i * 2 + 1] = new Point(249 + 100 * i, 460);
            }

            #endregion#
            //第一排6个
            for (int i = 0; i < 6; i++)
            {
                //上排状态，绿色、红色
                m_Rects[i] = new Rectangle(75 * i, 290, 74, 50);
                //按钮外框线
                m_Rects[10 + i] = new Rectangle(75 * i, 340, 74, 50);
                //按钮内框线
                m_Rects[20 + i] = new Rectangle(75 * i + 5, 345, 64, 40);
            }
            //第二排4个
            for (int i = 0; i < 2; i++)
            {
                //上排状态，绿色、红色
                m_Rects[6 + i] = new Rectangle(75 * i, 410, 74, 50);
                m_Rects[8 + i] = new Rectangle(150 + 100 * i, 410, 99, 50);
                //按钮外框线
                m_Rects[16 + i] = new Rectangle(75 * i, 460, 74, 50);
                m_Rects[18 + i] = new Rectangle(150 + 100 * i, 460, 99, 50);
                //按钮内框线
                m_Rects[26 + i] = new Rectangle(75 * i + 5, 465, 64, 40);
                m_Rects[28 + i] = new Rectangle(150 + 100 * i + 5, 465, 89, 40);
            }
            //开放按键
            m_Rects[30] = new Rectangle(390, 480, 100, 35);
            m_Rects[31] = new Rectangle(395, 485, 90, 25);

            for (int i = 0; i < 10; i++)
            {
                m_Buttons[i] = new HXD3Button(m_Rects[10 + i], m_Rects[20 + i]);
            }
            m_Buttons[10] = new HXD3Button(m_Rects[30], m_Rects[31]);

            m_Rect = new List<Region>();
            for (int i = 0; i < 10; i++)
            {
                m_Rect.Add(new Region(m_Rects[10 + i]));
            }
            m_Rect.Add(new Region(m_Rects[30]));
        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[120];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[120];

        /// <summary>
        /// 发送的数字量
        /// </summary>
        public bool[] m_SetBValue = new bool[10];

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        public int[] m_SetBValueNumb = new int[10];

        /// <summary>
        /// 接收模拟量
        /// </summary>
        public float[] m_FValue = new float[20];

        /// <summary>
        /// 坐标集
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// 矩形框
        /// </summary>
        public Rectangle[] m_Rects = new Rectangle[80];

        /// <summary>
        /// 按键列表
        /// </summary>
        public List<Region> m_Rect;

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#
        /// <summary>
        /// 10个开关的状态
        /// 为true时，表示没有按按键，绘制的是对应上排颜色的按键
        /// 为false时，表示按下了某个按键，停止绘对应上排颜色的按键，根据当前按钮的颜色取反
        /// </summary>
        public static bool[] m_ChangeState = new bool[10] { true, true, true, true, true, true, true, true, true, true };

        /// <summary>
        /// 10个按钮的状态
        /// </summary>
        public bool[] m_ButtonState = new bool[10];

        /// <summary>
        /// 10个按键上一次的状态
        /// </summary>
        public static bool[] m_ButtonOldState = new bool[10];

        /// <summary>
        /// 开放按键
        /// </summary>
        public bool m_Kaifang = false;

        /// <summary>
        /// 10个按键
        /// </summary>
        public HXD3Button[] m_Buttons = new HXD3Button[11];

        public string[] m_Str1 = new string[10] { "CI1", "CI2", "CI3", "CI4", "CI5", "CI6", "APU1", "APU2", "无人警惕", "轮缘润滑" };
        public string[] m_Str2 = new string[3] { "LG1 电压:", "LG2 电压:", "电流:" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}