using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS
{
    public class FormatStyle
    {
        private static float m_Scale = 1.0F;
        /// <summary>
        /// �����ߴ�
        /// </summary>
        public static float Scale
        {
            get { return m_Scale; }
        }

        /// <summary>
        /// �������ƶ�����
        /// </summary>
        public static int ScreenMoveX { get; private set; }

        /// <summary>
        /// �������ƶ�����
        /// </summary>
        public static int ScreenMoveY { get; private set; }

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ��1����
        /// </summary>
        public static readonly Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * Scale);

        /// <summary>
        /// ��1����
        /// </summary>
        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2 * Scale);

        /// <summary>
        /// ��1����
        /// </summary>
        public static readonly Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * Scale);

        /// <summary>
        /// ��3�л�
        /// </summary>
        public static readonly Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * Scale);

        /// <summary>
        /// ��1���
        /// </summary>
        public static readonly Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        #endregion

        #region :::::::::::::::::::::::::::::: ��ɫ :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ��ɫ
        /// </summary>
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// ����
        /// </summary>
        public static readonly SolidBrush LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// ���
        /// </summary>
        public static readonly SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// �л�
        /// </summary>
        public static readonly SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%��
        /// </summary>
        public static readonly SolidBrush HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// ��ɫ
        /// </summary>
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(101, 127, 162));
        #endregion

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        public const string StrFont = "����";
        public static readonly Font Font8 = new Font(StrFont, 8f * Scale);
        public static readonly Font Font10 = new Font(StrFont, 10f * Scale);
        public static readonly Font Font12 = new Font(StrFont, 12f * Scale);
        public static readonly Font Font14 = new Font(StrFont, 14f * Scale);
        public static readonly Font Font16 = new Font(StrFont, 16f * Scale);
        public static readonly Font Font18 = new Font(StrFont, 18f * Scale);
        public static readonly Font Font20 = new Font(StrFont, 20f * Scale);
        public static readonly Font Font22 = new Font(StrFont, 22f * Scale);
        public static readonly Font Font24 = new Font(StrFont, 24f * Scale);
        public static readonly Font Font26 = new Font(StrFont, 26f * Scale);
        public static readonly Font Font32 = new Font(StrFont, 32f * Scale);
        public static readonly Font Font34 = new Font(StrFont, 34f * Scale);
        public static readonly Font Font38 = new Font(StrFont, 38f * Scale);
        public static readonly Font Font64 = new Font(StrFont, 64f * Scale);

        public static readonly Font Font8B = new Font(StrFont, 8f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font10B = new Font(StrFont, 10f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font12B = new Font(StrFont, 12f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font14B = new Font(StrFont, 14f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font16B = new Font(StrFont, 16f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font18B = new Font(StrFont, 18f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font20B = new Font(StrFont, 20f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font22B = new Font(StrFont, 22f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font24B = new Font(StrFont, 24f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font26B = new Font(StrFont, 26f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font32B = new Font(StrFont, 32f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font34B = new Font(StrFont, 34f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font38B = new Font(StrFont, 38f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font64B = new Font(StrFont, 64f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        #endregion

        #region :::::::::::::::::::::::::::::: ���� :::::::::::::::::::::::::::::::::::::::
        public static readonly string[] Str1 = new string[7] { "����", "����״̬", "�յ�״̬", "�¼�", "ͨѶ״̬", "����", "����" };
        public static readonly string[] Str2 = new string[11] { "����ѹ����", "ǣ��ϵͳ", "BHB/BLB", "�˿ͱ���", "�̻𱨾�", "�����¶�", "1����", "2����", "�ƶ���ѹ��", "ͣ���ƶ�", "�����ƶ�" };
        public static readonly string[] Str3 = new string[6] { "0001", "0002", "0003", "0004", "0005", "0006" };
        public static readonly string[] Str4 = new string[24] { "1", "3", "2", "4", "1", "3", "2", "4", "1", "3", "2", "4", "4", "2", "3", "1", "4", "2", "3", "1", "4", "2", "3", "1" };
        public static readonly string[] Str5 = new string[24] { "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8" };
        public static readonly string[] Str6 = new string[24] { "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1" };
        public static readonly string[] ��ʾģʽ = new string[4] { "ATP", "ATO", "SM", "����ģʽ" };
        public static readonly string[] �ƶ�ģʽ = new string[4] { "ͣ���ƶ�", "�����ƶ�", "�����ƶ�", "�����ƶ�" };
        public static readonly string[] վ�㰴�� = new string[3] { "�����㲥", "վ������", "��·��Ϣ" };

        public static readonly string[] Str7 = new string[10] { "VVVF", "����״̬", "�м��ѹ(V)", "�������(A)", "SIV", "�����ѹ(V)", "110V���(V)", "������(A)", "�ܷ�ѹ��", "�ջ�ѹ��" };
        public static readonly string[] Str8 = new string[7] { "�����¶�", "�����¶�", "�յ�Ŀ���¶�", "�յ�ģʽ", "�¶�ģʽ", "ѹ����״̬", "�յ�����" };
        public static readonly string[] Str9 = new string[7] { "-1k", "-2k", "+1k", "+2k", "UICģʽ", "Ԥ��ر�", "����ģʽ" };
        public static readonly string[] Str10 = new string[24] { "1", "2", "3", "4", "1", "2", "3", "4", "1", "2", "3", "4", "4", "3", "2", "1", "4", "3", "2", "1", "4", "3", "2", "1" };

        public static readonly string[] Str11 = new string[62] {"VCM", "VCM", "REP", "REP", "REP", "REP", "REP", "REP", "DXM1", "DXM2", "ATC", "PIS", "HAVC1", "FDS", "HMI", "ERM", "DIM", "AXM", "HAVC2", "BCU"
        , "HMI", "ERM", "DIM", "AXM", "HAVC2", "BCU", "DXM1", "DXM2", "ATC", "PIS", "HAVC1", "FDS", "DXM1", "DXM2", "DCU", "BCU", "HAVC1", "HAVC2", "DXM1", "DXM2", "DCU", "BCU", "HAVC1", "HAVC2",
        "", "", "", "", "", "", "SIV", "SIV", "DXM1", "DXM2", "DCU", "HAVC1", "HAVC2", "DXM1", "DXM2", "DCU", "HAVC1", "HAVC2" };

        public static readonly string[] Str12 = new string[5] { "NO", "�ȼ�", "����", "��������", "ʱ��" };

        public static readonly string[] Str13 = new string[3] { "�ִ�\n����", "����\n����", "����\n��ʾ" };

        public static readonly string[] Str14 = new string[30] { "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", 
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "����", };

        public static readonly string[] Str15 = new string[12] { "ȫ�Զ�\n�㲥", "���Զ�\n�㲥", "1����", "2����", "3����", "6����", "Routeѡ��", "����", "ʼ��վ:", "�յ�վ:", "Line_ID", "Route_ID" };
        public static readonly string[] Str16 = new string[13] { "��·Ͷ��", "��·�г�", "������·", "ͣ���ƶ�������·", "�����ƶ�������·", "�ܷ�ѹ��������·", "ATC��·", "��������·", "�Źغ���·", "����������·", "BHB�г�", "ATC ZVRD���", "����ʹ��·" };
        public static readonly string[] Str17 = new string[4] { "BHB", "BLB", "BLB", "BHB"};
        public static readonly string[] Str18 = { "�˹�ģʽ", "ATPģʽ", "ATOģʽ", "SMģʽ", "����ģʽ","��Ԯģʽ","����ģʽ" };
        public static readonly string[] Str19 = {"ͣ���ƶ�", "�ƶ�", "�����ƶ�", "�����ƶ�", "ǣ��"};
        

        static FormatStyle()
        {
            ScreenMoveY = 0;
            ScreenMoveX = 0;
        }

        #endregion
    }
}
