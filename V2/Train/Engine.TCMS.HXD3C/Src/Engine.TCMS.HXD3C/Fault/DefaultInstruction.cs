using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C.Fault
{
    //����˵��
    [GksDataType(DataType.isMMIObjectClass)]
    public class DefaultInstruction : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "����˵��";
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
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str204);
            }
        }
        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
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
        /// ������
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            for (int i = 0; i < 14; i++)
            {
                e.DrawRectangle(Common.WhitePen1, 
                    new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + i * 30, 60, 30));
                e.DrawRectangle(Common.WhitePen1, 
                    new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + i * 30, 600, 30));
            }
            e.DrawString("����", Common.Txt12FontB, Common.WhiteBrush, 
                new Rectangle(m_PDrawPoint[0], new Size(60, 30)), Common.DrawFormat);
            e.DrawString("˵��", Common.Txt12FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[1], new Size(600, 30)), Common.DrawFormat);
            e.DrawString("", Common.Txt12FontB, Common.WhiteBrush, 
                new Rectangle(m_PDrawPoint[1], new Size(600, 30)), Common.DrawFormat);
            for (int i = 0; i < 13; i++)
            {
                e.DrawString((i + 1).ToString(), Common.Txt12FontB, Common.WhiteBrush, 
                    new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + (i + 1) * 30, 60, 30), Common.DrawFormat);
                e.DrawString(m_Str1[i], Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + (i + 1) * 30, 600, 30), Common.LeftFormat);
            }
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);
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
            m_PDrawPoint[0] = new Point(50, 110);
            m_PDrawPoint[1] = new Point(110, 110);
            m_PDrawPoint[2] = new Point(0, 0);
            m_PDrawPoint[3] = new Point(0, 0);
            m_PDrawPoint[4] = new Point(0, 0);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            m_PDrawPoint[10] = new Point(0, 0);
            m_PDrawPoint[11] = new Point(0, 0);
            m_PDrawPoint[12] = new Point(0, 0);
            m_PDrawPoint[13] = new Point(0, 0);
            m_PDrawPoint[14] = new Point(0, 0);
            m_PDrawPoint[15] = new Point(0, 0);
            m_PDrawPoint[16] = new Point(0, 0);
            m_PDrawPoint[17] = new Point(0, 0);
            m_PDrawPoint[18] = new Point(0, 0);
            m_PDrawPoint[19] = new Point(0, 0);
            #endregion#

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
        /// ͼƬ��
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#


        /// <summary>
        /// ������
        /// </summary>
        public NeedChangeLength[] m_GrogressBar = new NeedChangeLength[9];

        public string[] m_Str1 = new string[13] { "����λ����", "�ٿ���ͨ���", "ͣ�������½��¶ȣ��ٿ�����",
            "ͣ�������½���ѹ���ٿ�����", "�ٿ�������", "״̬��λ�󣬰���λ���ء�", "��λ���ذ��º���Ͷ������·����",
            "��˾�����ֱ��á�0��λ", "���ѹ�ָ�������λ���ء�", "��Ͷ�����ؿ���", "ͨ����Զ����رպϺ󣬰���λ���ء�",
            "������CI����", "��κ�����ɨ��APU����" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}
