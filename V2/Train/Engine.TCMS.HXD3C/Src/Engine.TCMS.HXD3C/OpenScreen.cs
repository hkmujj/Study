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
            return "���Ż���";
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
        /// ��������
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
        /// ������֮��������
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
        /// ������ɫ�仯
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
        /// ����״̬
        /// </summary>
        /// <param name="e"></param>
        private void DrawState(Graphics e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_BValue[i])
                {
                    e.FillRectangle(Common.RedBrush, m_Rects[i]);
                    e.DrawString(i > 5 ? "����" : "����", Common.Txt14FontR,
                        Common.WhiteBrush, m_Rects[i], Common.DrawFormat);
                    //û������������״̬��ͼ
                    if (m_ChangeState[i])
                    {
                        e.FillRectangle(Common.BlueBrush, m_Rects[10 + i]);
                    }
                }
                else
                {
                    e.FillRectangle(Common.GreenBrush, m_Rects[i]);
                    e.DrawString("����", Common.Txt14FontR, Common.BlackBrush,
                        m_Rects[i], Common.DrawFormat);
                }

                if (!m_ChangeState[i] && m_ButtonState[i])
                {
                    e.FillRectangle(Common.BlueBrush, m_Rects[10 + i]);
                }
            }
            //����
            for (int i = 0; i < 10; i++)
            {
                e.DrawLine(Common.WhitePen1, m_PDrawPoint[i * 2], m_PDrawPoint[i * 2 + 1]);
            }
        }

        /// <summary>
        /// ��ͼ
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
            e.DrawString("����", Common.Txt12FontB, Common.WhiteBrush, m_Rects[31], Common.DrawFormat);
        }

        #endregion#

        #region:::::::::::::::���������ĳ�ʼ�������̺ͽ������ĳ�ʼ��:::::::::::::::#
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::�����:::::::::::::::::::::::::::::#
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
            //��һ��6��
            for (int i = 0; i < 6; i++)
            {
                //����״̬����ɫ����ɫ
                m_Rects[i] = new Rectangle(75 * i, 290, 74, 50);
                //��ť�����
                m_Rects[10 + i] = new Rectangle(75 * i, 340, 74, 50);
                //��ť�ڿ���
                m_Rects[20 + i] = new Rectangle(75 * i + 5, 345, 64, 40);
            }
            //�ڶ���4��
            for (int i = 0; i < 2; i++)
            {
                //����״̬����ɫ����ɫ
                m_Rects[6 + i] = new Rectangle(75 * i, 410, 74, 50);
                m_Rects[8 + i] = new Rectangle(150 + 100 * i, 410, 99, 50);
                //��ť�����
                m_Rects[16 + i] = new Rectangle(75 * i, 460, 74, 50);
                m_Rects[18 + i] = new Rectangle(150 + 100 * i, 460, 99, 50);
                //��ť�ڿ���
                m_Rects[26 + i] = new Rectangle(75 * i + 5, 465, 64, 40);
                m_Rects[28 + i] = new Rectangle(150 + 100 * i + 5, 465, 89, 40);
            }
            //���Ű���
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

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// ��ǰ���ڽ���������
        /// </summary>
        public bool[] m_BValue = new bool[120];

        /// <summary>
        /// ǰһ�����ڽ��յ�������
        /// </summary>
        public bool[] m_OldbValue = new bool[120];

        /// <summary>
        /// ���͵�������
        /// </summary>
        public bool[] m_SetBValue = new bool[10];

        /// <summary>
        /// ���͵���������boollist�е����
        /// </summary>
        public int[] m_SetBValueNumb = new int[10];

        /// <summary>
        /// ����ģ����
        /// </summary>
        public float[] m_FValue = new float[20];

        /// <summary>
        /// ���꼯
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// ���ο�
        /// </summary>
        public Rectangle[] m_Rects = new Rectangle[80];

        /// <summary>
        /// �����б�
        /// </summary>
        public List<Region> m_Rect;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#
        /// <summary>
        /// 10�����ص�״̬
        /// Ϊtrueʱ����ʾû�а����������Ƶ��Ƕ�Ӧ������ɫ�İ���
        /// Ϊfalseʱ����ʾ������ĳ��������ֹͣ���Ӧ������ɫ�İ��������ݵ�ǰ��ť����ɫȡ��
        /// </summary>
        public static bool[] m_ChangeState = new bool[10] { true, true, true, true, true, true, true, true, true, true };

        /// <summary>
        /// 10����ť��״̬
        /// </summary>
        public bool[] m_ButtonState = new bool[10];

        /// <summary>
        /// 10��������һ�ε�״̬
        /// </summary>
        public static bool[] m_ButtonOldState = new bool[10];

        /// <summary>
        /// ���Ű���
        /// </summary>
        public bool m_Kaifang = false;

        /// <summary>
        /// 10������
        /// </summary>
        public HXD3Button[] m_Buttons = new HXD3Button[11];

        public string[] m_Str1 = new string[10] { "CI1", "CI2", "CI3", "CI4", "CI5", "CI6", "APU1", "APU2", "���˾���", "��Ե��" };
        public string[] m_Str2 = new string[3] { "LG1 ��ѹ:", "LG2 ��ѹ:", "����:" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}