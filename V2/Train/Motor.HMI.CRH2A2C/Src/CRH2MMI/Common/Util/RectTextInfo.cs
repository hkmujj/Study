using System;
using System.Drawing;

namespace CRH2MMI.Common.Util
{
    /// <summary>
    /// �ı����ο� λ�á�����ɫ����ʽ����
    /// </summary>
    public class RectTextInfo
    {
        public SolidBrush TextBrush;
        public Font tmpDrawFont;
        public String Str;
        public StringFormat drawFormat = new StringFormat();
        public SolidBrush BKBrush;
        public RectangleF RectPosition;

        public Pen Drawlinepen = new Pen(Color.FromArgb(255, 255, 255), 1);


        public void SetRectStr(string str)
        {
            Str = str;
        }

        public void SetBK(int bkc)
        {
            if (bkc == 1)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
            }
            else if (bkc == 2)
            {
                BKBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
            }
            else if (bkc == 3)
            {
                BKBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            }
            else if (bkc == 4)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
            }
            else if (bkc == 5)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            }
        }

        public void SetTC(int textc)
        {
            if (textc == 0)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
            else if (textc == 1)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            else if (textc == 2)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            else if (textc == 3)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        }

        /// <summary>
        /// �����ı�������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="flg"></param>
        /// <param name="str"></param>
        /// <param name="textc"></param>
        /// <param name="bkc"></param>
        /// <param name="format"></param>
        /// <param name="size"></param>
        public void SetRectTextInfo(float x, float y, float width, float height, int flg,
            string str, int textc,int bkc,int format, float size)
        {
            RectPosition.X = x;
            RectPosition.Y = y;
            RectPosition.Height = height;
            RectPosition.Width = width;

            Str = str;


            tmpDrawFont = new Font(CRH2Resource.StrFont, size);


            if (textc==0)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
            else if (textc == 1)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            else if (textc == 2)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            else if (textc == 3)//��ɫ
                TextBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
            
            if (format == 0)//����
            {
                drawFormat.Alignment = (StringAlignment)1;
                drawFormat.LineAlignment = (StringAlignment)1;
            }
            else if (format == 1)//����
            {
                drawFormat.Alignment = (StringAlignment)2;
                drawFormat.LineAlignment = (StringAlignment)1;
            }
            else if (format == 2)//����
            {
                drawFormat.Alignment = (StringAlignment)1;
                drawFormat.LineAlignment = (StringAlignment)0;
            }

            if (bkc == 1)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
            }
            else if (bkc == 2)
            {
                BKBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
            }
            else if (bkc == 3)
            {
                BKBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
            }
            else if (bkc == 4)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
            }
            else if (bkc == 5)
            {
                BKBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            }
            
        }

        public virtual void OnDraw(Graphics e)
        {

            if (BKBrush!=null)
                e.FillRectangle(BKBrush, RectPosition.X, RectPosition.Y,
                    RectPosition.Width, RectPosition.Height);
            e.DrawRectangle(Drawlinepen, RectPosition.X, RectPosition.Y,
            RectPosition.Width, RectPosition.Height);

            if (TextBrush != null)
            {
                e.DrawString(Str, tmpDrawFont, TextBrush, RectPosition, drawFormat);
            }
        }
    }
}
