using System;
using System.Drawing;

namespace Engine.Turkmenistan.LKJ.Common
{
    /// <summary>
    /// �����������Զ��尴ť
    /// �����ˣ�����
    /// ����ʱ�䣺2014-07-15
    /// </summary>
    public class Button : IControl, IDisposable
    {
        private readonly Boolean m_SelfReplication;//��ť�Ը���ʶ
        #region �¼�/����
        /// <summary>
        /// ��ӻ��Ƴ���ť����¼���Ӧ��������
        /// </summary>
        public event EventHandleClickEvent ClickEvent
        {
            add { m_ClickEvent += value; }
            remove { m_ClickEvent -= value; }
        }
        private EventHandleClickEvent m_ClickEvent;

        public event EventHandleClickEvent DownEvent
        {
            add { m_DownEvent += value; }
            remove { m_DownEvent -= value; }
        }
        private EventHandleClickEvent m_DownEvent = null;
        #endregion

        #region ����/����
        /// <summary>
        /// ��ȡ�����ð�ť�ı�����
        /// </summary>
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        private String m_Text = String.Empty;

        /// <summary>
        /// ��ȡ��ť��ʶID����
        /// </summary>
        public Int32 ID
        {
            get { return m_ID; }
        }
        private readonly Int32 m_ID = -1;

        /// <summary>
        /// ��ȡ��ťStyle���ԣ�ʹ��ʱ��ת��ΪButtonStyle��
        /// </summary>
        public IStyle Style
        {
            get { return m_Style; }
        }
        private readonly IStyle m_Style;

        /// <summary>
        /// ��ȡ�����ð�ťʹ�ܿ�������
        /// </summary>
        public Boolean Enable
        {
            get { return m_Enable; }
            set
            {
                if (m_Enable == value)
                    return;

                m_Enable = value;
            }
        }
        private Boolean m_Enable = true;

        /// <summary>
        /// ��ȡ�����ð�ť���ھ�����������
        /// </summary>
        public RectangleF Rect
        {
            get { return m_Rect; }
            set
            {
                if (m_Rect == value)
                    return;

                m_Rect = value;
            }
        }
        private RectangleF m_Rect;

        /// <summary>
        /// ��ȡ�������Ƿ��ý������ԣ����ܺ�����ӣ�
        /// </summary>
        public Boolean IsFocus
        {
            get { return m_IsFocus; }
            set
            {
                if (m_IsFocus == value)
                    return;

                m_IsFocus = value;
            }
        }
        private Boolean m_IsFocus = false;

        /// <summary>
        /// ��ȡ�������Ƿ�λ���ԣ���ť�Ƿ����û�����ʹ�ø�����ʵ�ְ�ť�ĸ�λ��
        /// </summary>
        public Boolean IsReplication
        {
            get { return m_IsReplication; }
            set
            {
                if (m_IsReplication == value)
                    return;

                m_IsReplication = value;
            }
        }
        private Boolean m_IsReplication = true;
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯�������ݰ�ť�ı���Ϣ�����ھ������򡢰�ť��ʶ����ťStyle���Ը���ʶ����Ϣ���찴ť
        /// </summary>
        /// <param name="text">��ť�ı�</param>
        /// <param name="rect">��ť��������</param>
        /// <param name="id">��ťID</param>
        /// <param name="style">��ť��ʽ</param>
        /// <param name="selfReplication">�Ը���ʶ��Ĭ��Ϊ�Ը���ť</param>
        public Button(String text, RectangleF rect, Int32 id, ButtonStyle style, Boolean selfReplication = true)
        {
            m_Text = text;
            m_Rect = rect;
            m_ID = id;
            m_Style = style;
            m_SelfReplication = selfReplication;
        }
        #endregion

        #region ����⺯��
        /// <summary>
        /// ��갴�¹��ܺ������ú������ڽ�������갴�º����е��ã�
        /// </summary>
        /// <param name="p">�����������</param>
        public void MouseDown(Point p)
        {
            if (!m_Enable)//��ť�����ã���������
                return;

            if (m_Rect.Contains(p))//��������ڰ�ť���������ڣ�ʵ�ְ�ť���¹���
            {
                if (m_DownEvent != null)
                    m_DownEvent(this, new ClickEventArgs<int>(m_ID));

                m_IsFocus = true;//��ť��ý��㣨�������ܻ�ʹ�õ���
                m_IsReplication = !m_IsReplication;//��ť��λ����ȡ��

            }
        }

        /// <summary>
        /// ��굯���ܺ������ú������ڽ�������굯�����е��ã�
        /// </summary>
        /// <param name="p">�����������</param>
        public void MouseUp(Point p)
        {
            if (!m_Enable)//��ť�����ã���������
                return;

            if (m_Rect.Contains(p))//��������ڰ�ť���������ڣ�ʵ�ְ�ť������
            {

                if (m_ClickEvent != null)//������ť�����¼�
                    m_ClickEvent(this, new ClickEventArgs<int>(m_ID));
                if (m_SelfReplication)//Ϊ�Ը���ť������λ
                {
                    m_IsReplication = true;
                }
            }
        }
        #endregion

        #region ��ť���ƺ���
        /// <summary>
        /// ��ť���ƣ����ڽ�����ƺ�����ʵʱ���ã�
        /// </summary>
        /// <param name="dcGs"></param>
        public void Paint(Graphics dcGs)
        {
            if (!m_Enable)//��ť�����ã����ƻ�ɫ����
            {

                if (((ButtonStyle)m_Style).DisableImage == null)
                    ((ButtonStyle)m_Style).DisableImage = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/Resource/Btn_Disable.png");

                dcGs.DrawImage(((ButtonStyle)m_Style).DisableImage, m_Rect);
                dcGs.DrawString(
                    m_Text,
                    ((ButtonStyle)m_Style).FontStyle.Font,
                    new SolidBrush(Color.FromArgb(150, 150, 150)),
                    new RectangleF(m_Rect.X, m_Rect.Y + 3, m_Rect.Width, m_Rect.Height),
                    ((ButtonStyle)m_Style).FontStyle.StringFormat
                );
                return;
            }

            if (!m_IsReplication)//��ťδ��λ�����ư���ͼƬ����
            {
                if (((ButtonStyle)m_Style).DownImage != null)
                    dcGs.DrawImage(((ButtonStyle)m_Style).DownImage, m_Rect);
            }
            else//��ť��λ�����Ƴ���ͼƬ����
            {
                if (m_Style.Background != null)
                    dcGs.DrawImage(m_Style.Background, m_Rect);
            }
            dcGs.DrawString(
                m_Text,
                ((ButtonStyle)m_Style).FontStyle.Font,
                ((ButtonStyle)m_Style).FontStyle.TextBrush,
                new RectangleF(m_Rect.X, m_Rect.Y + 3, m_Rect.Width, m_Rect.Height),
                ((ButtonStyle)m_Style).FontStyle.StringFormat
            );//���ư�ť�ı�
        }
        #endregion

        #region IDisposable�ӿں���
        /// <summary>
        /// IDisposable�ӿں�����������Ҫ��ӹ���
        /// </summary>
        public void Dispose()
        {
            //��ť�ͷ���Դ
        }
        #endregion
    }
}