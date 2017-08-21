using System.Drawing;

namespace Engine.TCMS.HXD3C.Utils
{
    public class Common
    {
        #region:::::::::::::::::::::::::::������ɫ::::::::::::::::::::::::::::::::#

        public static readonly Pen WhitePen1 = new Pen(Color.White, 1);
        public static readonly Pen WhitePen2 = new Pen(Color.White, 2);

        public static readonly Pen BlackPen1 = new Pen(Color.Black, 1);
        public static readonly Pen BlackPen2 = new Pen(Color.Black, 2);

        public static readonly Pen RedPen1 = new Pen(Color.Red, 1);

        public static readonly Pen YellowPen1 = new Pen(Color.Yellow, 1);
        public static readonly Pen YellowPen2 = new Pen(Color.Yellow, 2);
        public static readonly Pen YellowPen3 = new Pen(Color.Yellow, 3);

        public static readonly Pen GreenPen = new Pen(Color.Green, 2);
        public static readonly Pen GreenPen1 = new Pen(Color.Green, 1);

        #endregion#

        #region:::::::::::::::::::::::::::������ɫ::::::::::::::::::::::::::::::::#

        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        public static readonly SolidBrush PinkBrush = new SolidBrush(Color.FromArgb(255, 0, 255));
        public static readonly SolidBrush MarineBlueBrush = new SolidBrush(Color.FromArgb(0, 255, 255));

        #endregion#

        #region:::::::::::::::::::::::::::�����ʽ::::::::::::::::::::::::::::::::#

        public static readonly Font Txt1FontR = new Font("����", 1, FontStyle.Regular);
        public static readonly Font Txt10FontR = new Font("����", 10, FontStyle.Regular);
        public static readonly Font Txt12FontR = new Font("����", 12, FontStyle.Regular);
        public static readonly Font Txt13FontR = new Font("����", 13, FontStyle.Regular);
        public static readonly Font Txt14FontR = new Font("����", 14, FontStyle.Regular);
        public static readonly Font Txt16FontR = new Font("����", 16, FontStyle.Regular);
        public static readonly Font Txt20FontR = new Font("����", 20, FontStyle.Regular);
        public static readonly Font Txt24FontR = new Font("����", 24, FontStyle.Regular);
        public static readonly Font Txt30FontR = new Font("����", 30, FontStyle.Regular);

        public static readonly Font Txt1FontB = new Font("����", 1, FontStyle.Bold);
        public static readonly Font Txt10FontB = new Font("����", 10, FontStyle.Bold);
        public static readonly Font Txt12FontB = new Font("����", 12, FontStyle.Bold);
        public static readonly Font Txt13FontB = new Font("����", 13, FontStyle.Bold);
        public static readonly Font Txt14FontB = new Font("����", 14, FontStyle.Bold);
        public static readonly Font Txt16FontB = new Font("����", 16, FontStyle.Bold);
        public static readonly Font Txt20FontB = new Font("����", 20, FontStyle.Bold);
        public static readonly Font Txt24FontB = new Font("����", 24, FontStyle.Bold);
        public static readonly Font Txt30FontB = new Font("����", 30, FontStyle.Bold);

        public static readonly Font Txt1FontLr = new Font("LcdD", 1, FontStyle.Regular);
        public static readonly Font Txt10FontLr = new Font("LcdD", 10, FontStyle.Regular);
        public static readonly Font Txt12FontLr = new Font("LcdD", 12, FontStyle.Regular);
        public static readonly Font Txt13FontLr = new Font("LcdD", 13, FontStyle.Regular);
        public static readonly Font Txt14FontLr = new Font("LcdD", 14, FontStyle.Regular);
        public static readonly Font Txt16FontLr = new Font("LcdD", 16, FontStyle.Regular);
        public static readonly Font Txt20FontLr = new Font("LcdD", 20, FontStyle.Regular);
        public static readonly Font Txt24FontLr = new Font("LcdD", 24, FontStyle.Regular);
        public static readonly Font Txt30FontLr = new Font("LcdD", 30, FontStyle.Regular);
        public static readonly Font Txt34FontLr = new Font("LcdD", 34, FontStyle.Regular);

        public static readonly Font Txt1FontLb = new Font("LcdD", 1, FontStyle.Bold);
        public static readonly Font Txt10FontLb = new Font("LcdD", 10, FontStyle.Bold);
        public static readonly Font Txt12FontLb = new Font("LcdD", 12, FontStyle.Bold);
        public static readonly Font Txt13FontLb = new Font("LcdD", 13, FontStyle.Bold);
        public static readonly Font Txt14FontLb = new Font("LcdD", 14, FontStyle.Bold);
        public static readonly Font Txt16FontLb = new Font("LcdD", 16, FontStyle.Bold);
        public static readonly Font Txt20FontLb = new Font("LcdD", 20, FontStyle.Bold);
        public static readonly Font Txt24FontLb = new Font("LcdD", 24, FontStyle.Bold);
        public static readonly Font Txt30FontLb = new Font("LcdD", 30, FontStyle.Bold);
        public static readonly Font Txt34FontLb = new Font("LcdD", 34, FontStyle.Bold);

        public static readonly StringFormat CenterStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static readonly StringFormat DrawFormat = new StringFormat();
        public static readonly StringFormat RightFormat = new StringFormat();
        public static readonly StringFormat LeftFormat = new StringFormat();

        #endregion#


        #region �ײ��������� Str��ͼID

        public static readonly string[] Str201 = {"����״̬", "", "����", "������", "����״̬", "����״̬", "��������", "����  ��"};

        public static readonly string[] Str202 = {"��������", "����״̬", "���״̬", "������Դ", "����״̬", "����״̬", "��������", "����"};

        public static readonly string[] Str203 = {"����״̬", "", "����", "������", "����״̬", "����״̬", "��������", "����"};

        public static readonly string[] Str204 = {"����״̬", "", "����", "������", "", "", "��������", "����"};

        public static readonly string[] Str205 = { "", "", "", "", "", "", "", "����" };

        public static readonly string[] Str217 = {"��˾����", "��", "�㼶λ", "������Դ", "��ʾ��", "���˾���", "��Ե��", "����"};

        public static readonly string[] Str219 = {"��¼", "�趨", "����", "״̬", "", "", "", "����"};

        #endregion
    }
}
