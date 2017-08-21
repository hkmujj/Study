using System;
using System.Drawing;

namespace Urban.Wuxi.TMS
{
    public class FormatStyle
    {
        private static readonly float m_Scale = 1.0f;
        /// <summary>
        /// �����ߴ�
        /// </summary>
        public static float Scale { get { return m_Scale; } }

        private static readonly int m_ScreenMoveX = 0;
        /// <summary>
        /// �������ƶ�����
        /// </summary>
        public static int ScreenMoveX { get { return m_ScreenMoveX; } }

        private static readonly int m_ScreenMoveY = 0;
        /// <summary>
        /// �������ƶ�����
        /// </summary>
        public static int ScreenMoveY { get { return m_ScreenMoveY; } }


        public static StringFormat m_Cneter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ��1����
        /// </summary>
        public static Pen m_WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * m_Scale);

        /// <summary>
        /// ��1����
        /// </summary>
        public static Pen m_BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * m_Scale);

        /// <summary>
        /// ��3�л�
        /// </summary>
        public static Pen m_MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * m_Scale);

        /// <summary>
        /// ��1���
        /// </summary>
        public static Pen m_DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        #endregion

        #region :::::::::::::::::::::::::::::: ��ɫ :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// ����
        /// </summary>
        public static SolidBrush m_LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// ���
        /// </summary>
        public static SolidBrush m_DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// �л�
        /// </summary>
        public static SolidBrush m_MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%��
        /// </summary>
        public static SolidBrush m_HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// �ۻ�ɫ
        /// </summary>
        public static SolidBrush m_OrangeBrush = new SolidBrush(Color.Orange);

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_BlueBrush = new SolidBrush(Color.FromArgb(101, 127, 162));
        /// <summary>
        /// ǳ��
        /// </summary>
        public static SolidBrush m_GreyBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        /// <summary>
        /// ��ɫ
        /// </summary>
        public static SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        /// <summary>
        /// �׻�
        /// </summary>
        public static SolidBrush m_BgrBursh = new SolidBrush(Color.FromArgb(170, 170, 170));
        /// <summary>
        ///����ɫ
        /// </summary>
        public static SolidBrush m_ThinBlue = new SolidBrush(Color.FromArgb(0, 109, 189));
        #endregion

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        public const String StrFont = "����";
        public static Font m_Font8 = new Font(StrFont, 8f * m_Scale);
        public static Font m_Font10 = new Font(StrFont, 10f * m_Scale);
        public static Font m_Font12 = new Font(StrFont, 12f * m_Scale);
        public static Font m_Font14 = new Font(StrFont, 14f * m_Scale);
        public static Font m_Font16 = new Font(StrFont, 16f * m_Scale);
        public static Font m_Font18 = new Font(StrFont, 18f * m_Scale);
        public static Font m_Font20 = new Font(StrFont, 20f * m_Scale);
        public static Font m_Font22 = new Font(StrFont, 22f * m_Scale);
        public static Font m_Font24 = new Font(StrFont, 24f * m_Scale);
        public static Font m_Font26 = new Font(StrFont, 26f * m_Scale);
        public static Font m_Font32 = new Font(StrFont, 32f * m_Scale);
        public static Font m_Font34 = new Font(StrFont, 34f * m_Scale);
        public static Font m_Font38 = new Font(StrFont, 38f * m_Scale);
        public static Font m_Font64 = new Font(StrFont, 64f * m_Scale);

        public static Font m_Font8B = new Font(StrFont, 8f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font10B = new Font(StrFont, 10f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font12B = new Font(StrFont, 12f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font14B = new Font(StrFont, 14f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font16B = new Font(StrFont, 16f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font18B = new Font(StrFont, 18f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font20B = new Font(StrFont, 20f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font22B = new Font(StrFont, 22f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font24B = new Font(StrFont, 24f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font26B = new Font(StrFont, 26f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font32B = new Font(StrFont, 32f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font34B = new Font(StrFont, 34f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font38B = new Font(StrFont, 38f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font64B = new Font(StrFont, 64f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        #endregion

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        public static String[] m_Str1 = new String[7] { "����", "����״̬", "�յ�״̬", "�¼�", "ͨѶ״̬", "����", "����" };
        public static String[] m_Str2 = new String[10] { "����ѹ����", "ǣ��ϵͳ", "�˿ͱ���", "�̻𱨾�", "BHB", "BLB", "ͣ���ƶ�", "�����ƶ�", "�����¶�", "���ƶ�������" };
        public static String[] m_Str3 = new String[6] { "TC1", "M11", "M21", "M22", "M12", "TC2" };
        public static String[] m_Str4 = new String[24] { "1", "3", "2", "4", "1", "3", "2", "4", "1", "3", "2", "4", "4", "2", "3", "1", "4", "2", "3", "1", "4", "2", "3", "1" };
        public static String[] m_Str5 = new String[24] { "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8" };
        public static String[] m_Str6 = new String[24] { "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1" };
        public static String[] m_��ʾģʽ = new String[4] { "ATP", "ATO", "SM", "����ģʽ" };
        public static String[] m_�ƶ�ģʽ = new String[4] { "ͣ���ƶ�", "�����ƶ�", "�����ƶ�", "�����ƶ�" };
        public static String[] m_վ�㰴�� = new String[9] { "", "", "", "", "", "�����㲥", "վ������", "", "" };

        public static String[] m_Str7 = new String[10] { "����״̬", "�м��ѹ(V)", "�������(A)", "SIV", "380V���(V)", "110V���(V)", "������(A)", "�ܷ�ѹ��", "�ջ�ѹ��", "�ƶ���ѹ��" };
        public static String[] m_Str8 = new String[9] { "�����¶�", "�����¶�", "�յ�Ŀ���¶�", "�յ�״̬(����1)", "�յ�״̬(����2)", "�յ�ģʽ(����1)", "�յ�ģʽ(����2)", "�յ��������", "ѹ����״̬" };
        public static String[] m_Str9 = new String[9] { "22", "24", "26", "28", "ͨ��", "UICģʽ", "�ر�Ԥ���Ԥ��", "", "����ģʽ" };
        public static String[] m_Str10 = new String[24] { "1", "2", "3", "4", "1", "2", "3", "4", "1", "2", "3", "4", "4", "3", "2", "1", "4", "3", "2", "1", "4", "3", "2", "1" };

        public static String[] m_Str11 = new String[64] { "REP", "REP", "REP", "REP", "REP", "REP",
                                                "VCM", "DIM", "SIV", "HMI", "AXM", "PIS", "ATC", "ERM",
                                                "DXM1", "DXM2", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "DCU", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "DCU", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "VCM(M)", "DIM", "SIV", "HMI", "AXM", "PIS", "ATC", "ERM",};

        public static String[] m_Str12 = new String[5] { "NO", "�ȼ�", "����", "��������", "ʱ��" };

        public static String[] m_Str13 = new String[3] { "�ִ�\n����", "����\n����", "����\n��ʾ" };

        public static String[] m_Str14 = new String[30] { "�г�����", "�г����", "�������", "Խվ", "�ȶ��˿�����", "Ԥ��", "Ԥ��", "Ԥ��",
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", 
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "����", };

        public static String[] m_Str15 = { "ȫ�Զ�\n�㲥", "���Զ�\n�㲥", "�ֶ�\nģʽ", "", "1����", "2����", "·��\n����", "����", "ʼ��վ", "�յ�վ" };
        public static String[] m_Str16 = new String[13] { "��·Ͷ��", "��·�г�", "������·", "ͣ���ƶ�������·", "�����ƶ�������·", "�ܷ�ѹ��������·", "ATC��·", "��������·", "�Źغ���·", "����������·", "BHB�г�", "ATC ZVRD���", "����ʹ��·" };
        public static String[] m_Str17 = new String[4] { "BHB", "BLB", "BLB", "BHB" };
        public static String[] m_Str18 = { "�˹�ģʽ", "ATPģʽ", "ATOģʽ", "ATBģʽ", "����ģʽ", "����ǣ��ģʽ", "��Ԯģʽ", "����ģʽ", "RMFģʽ", "RMPģʽ", "δ֪" };
        public static String[] m_Str19 = { "ͣ���ƶ�", "�����ƶ�", "�����ƶ�", "�����ƶ�", "ǣ��" };
        public static String[] m_Str20 = { "����\n�ƶ�", "ǣ��\n����", "����", "��ת��\n��ǣ��", "����\n��Ϣ", "", " ����" };
        public static String[] m_Str21 = { "�����ƶ�������ʾ", "ǣ������������ʾ", "����������ʾ", "������������ʱ����ת�����ǣ��ģʽ", "������Ϣ��ʾ" };
        public static String[] m_Str22 = { "�����ƶ�", "ǣ������", "��ת����ǣ��", "����", "����" };
        public static String[] m_Str23 = { "1B", "2B", "3B", "4B" };
        public static String[] m_Str24 = { "1A", "2A", "3A", "4A" };
        public static String[] m_Str25 = { "4B", "3B", "2B", "1B" };
        public static String[] m_Str26 = { "4A", "3A", "2A", "1A" };
        public static String[] m_Str27 = { "˾����ռ��" };
        public static String[] m_Str28 = { "1", "2", "1", "2", "1", "2", "1", "2", "1", "2", "1", "2" };
        public static String[] m_Str29 = { "TC1��������", "M11��������", "M21��������", "M12��������", "M22��������", "TC2��������", "������������", "����" };
        public static String[] m_Str30 = new String[6] { "TC1", "M11", "M21", "M22", "M12", "TC2" };

        #endregion
    }
}
