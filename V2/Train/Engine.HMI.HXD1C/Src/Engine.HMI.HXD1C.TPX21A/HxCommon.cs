using System.Drawing;

namespace Engine.HMI.HXD1C.TPX21A
{
    public class HxCommon
    {
        public static readonly Rectangle Recposition = new Rectangle(0, 0, 800, 600);
        //������Ϣ��ʾ��
        public static readonly Rectangle RectPublic = new Rectangle(Recposition.X, Recposition.Y, 800, 27);
        //������Ϣ��ʾ��
        public static readonly Rectangle RectDefault = new Rectangle(Recposition.X, Recposition.Y + 500, 800, 40);

        #region ;;;;;;;��ʾ�����ı��� �ƶ���Ϣ�� ��Ϣ��ʾ�� ������Ϣ�� ���ϵȼ�;;;;;;;;;;;;;

        public static readonly HxRectText HTitle = new HxRectText();
        public static readonly HxRectText[] HDefault = new HxRectText[4]; //�������ĸ��ı��� ����Ϊ�ƶ���Ϣ�� ��Ϣ��ʾ�� ������Ϣ�� ���ϵȼ�

        #endregion

        //�ײ������ı���
        public static readonly HxRectText[] ButtonText = new HxRectText[10];
        public static readonly Rectangle ButtonInfo = new Rectangle(Recposition.X + 3, Recposition.Y + 547, 81, 50); //������ʾ��

        #region ;;;;;;���캯����ʼ������;;;;;;;;;;;

        public HxCommon()
        {
            HTitle.SetBkColor(0, 0, 0);
            HTitle.SetTextColor(255, 255, 255);
            HTitle.SetTextStyle(13, FormatStyle.Center, true, "����");
            HTitle.SetTextRect(Recposition.X + 130, Recposition.Y + 3, 135, 24);
            // H_Title.isdrawrectfrm = true;

            //�������ı����ʼ��
            for (int i = 0; i < 4; i++)
            {
                HDefault[i] = new HxRectText();
                HDefault[i].SetBkColor(0, 0, 0);
                HDefault[i].SetTextColor(0, 0, 0);
                HDefault[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                if (i < 3)
                {
                    HDefault[i].SetTextRect(Recposition.X + i*242 + 4, RectDefault.Y - 7, 239, 50);
                }
                else
                {
                    HDefault[i].SetTextRect(Recposition.X + 730, RectDefault.Y - 7, 60, 50);
                }
                HDefault[i].SetLinePen(84, 84, 84, 2);
                HDefault[i].m_Isdrawrectfrm = true;

            }



            //�ײ������ı����ʼ��
            for (int i = 0; i < 10; i++)
            {
                ButtonText[i] = new HxRectText();
                ButtonText[i].SetTextColor(255, 255, 255);
                ButtonText[i].SetTextStyle(14, FormatStyle.Center, true, "����");
                ButtonText[i].SetTextRect(Recposition.X + 90 + 71*i, Recposition.Y + 547, 63, 50);
                ButtonText[i].SetLinePen(84, 84, 84, 2);
                ButtonText[i].SetDrawFrm(true);
            }
        }


        #endregion

        #region ;;;;;;;�� �� �� �� �� ��;;;;;;;;;

        public static readonly Pen WhitePen1 = new Pen(Color.FromArgb(255, 255, 255), 1);
        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        public static readonly Pen LinePen1 = new Pen(Color.FromArgb(84, 84, 84), 1);
        public static readonly Pen LinePen2 = new Pen(Color.FromArgb(84, 84, 84), 2);
        public static readonly Pen LightWhitePen1 = new Pen(Color.FromArgb(211, 211, 211), 1);
        public static readonly Pen LightWhitePen2 = new Pen(Color.FromArgb(211, 211, 211), 2);
        public static readonly Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public static readonly Pen GreenPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        public static readonly Pen BluePen = new Pen(Color.FromArgb(0, 0, 255), 2);

        #endregion

        #region ;;;;;;;;;;�� �� �� �� �� ��;;;;;;;;;;;;

        public static readonly Font Font12 = new Font("����", 12);
        public static readonly Font Font10B = new Font("����", 10, FontStyle.Bold);
        public static readonly Font Font12B = new Font("����", 12, FontStyle.Bold);
        public static readonly Font Font14B = new Font("����", 14, FontStyle.Bold);
        public static readonly Font Font14 = new Font("����", 14);
        public static readonly Font Font16B = new Font("����", 16, FontStyle.Bold);
        public static readonly Font Font24B = new Font("����", 24, FontStyle.Bold);


        #endregion

        #region ;;;;;;;;;;�� �� �� �� �� ˢ;;;;;;;;;;;

        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255)); //��ɫ��ˢ
        public static readonly SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192)); //��ɫ��ˢ
        public static readonly SolidBrush DeadBrush = new SolidBrush(Color.FromArgb(84, 84, 84)); //��ɫ��ˢ
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255)); //��ɫ��ˢ
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0)); //��ɫ��ˢ
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0)); //��ɫ��ˢ
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0)); //��ɫ��ˢ
        public static readonly SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0)); //��ɫ��ˢ

        #endregion
    }
}
