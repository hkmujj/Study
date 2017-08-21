using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ATPComControl;
using ATPComControl.SpeedArc;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._200H.Common;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// �������� MainAeroB�� 
    ///     �ź��������� B����ʾ��Ϣ ����Ԥ����Ϣ 
    /// �����ˣ�Ԭ ��    
    /// ����ʱ�䣺2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainAeroB : ShowTypeEffectBaseClass
    {
        #region ˽���ֶ�

        private RectText m_BText; //B �� �� �� ģ ʽ �� Ϣ
        private readonly int m_StartAngle = 135;

        // Create coordinates of rectangle to bound ellipse.
        public int m_X = Common2D.RectB[0].X + 25;
        public int m_Y = Common2D.RectB[0].Y + 20;
        public int m_Width = 280;
        public int m_Height = 280;

        public Graphics m_Picpointer;
        private int m_Izhizhen;
        public float m_Anglev = 0;
        private readonly string[] m_SpeedStr = {"400", "300", "200", "150", "100", "50", "0"};
        private Image[] m_Img;
        private SpeedPointer m_SpeedPointer;

        private readonly Font m_SpeedValueFont = new Font("����", 16, FontStyle.Bold);

        private readonly StringFormat m_SpeedValueStringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        #endregion

        #region �����ٶȵ�Ԫ��

        /// <summary>
        /// ���ٿ̶Ȼ�ˢ
        /// </summary>
        private Pen m_XianShuKeDuPen = Common2D.GrayPen20;

        /// <summary>
        /// ��������Բ���Ļ�ˢ
        /// </summary>
        private Pen m_XianShuArcPen = Common2D.GrayPen12;

        /// <summary>
        /// Ŀ���ٶȻ�ˢ
        /// </summary>
        private Pen m_TargetSpeedPen = Common2D.GrayPen12;


        #endregion

        #region �ӿ�����

        /// <summary>
        /// �Ƿ���TSMģʽ��true ��ʾ���ڸ�ģʽ��
        /// </summary>
        private bool m_IsTsm;

        /// <summary>
        /// �Ƿ���CSMģʽ
        /// </summary>
        private bool m_IsCSM;

        /// <summary>
        /// ��ǰ�ٶ�
        /// </summary>
        private int m_CurrentSpeed;

        /// <summary>
        /// Ŀ���ٶ�
        /// </summary>
        private int m_TargetSpeed;

        /// <summary>
        /// �����ٶ�
        /// </summary>
        private int m_AllowSpeed;

        /// <summary>
        /// EBI�����ƶ��ٶ�
        /// </summary>
        private int m_EbiSpeed;

        /// <summary>
        /// SBI������ƶ��ٶ�
        /// </summary>
        private int m_SbiSpeed;

        /// <summary>
        /// ��ǰ����ģʽ
        /// </summary>
        private ControModelEnum m_ControlMode;

        #endregion

        private ISpeedConverter m_SpeedConverter;

        private Image m_ImagePointer;
        

        #region  ���ط���

        /// <summary>
        /// ��ȡͼԪ��������
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "�ź���������B����Ϣ��ʾ����";
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        protected override void Initalize()
        {
            if (UIObj.ParaList.Count >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                }
            }

            m_Img = new Image[]
            {
                ImageResource.zhizhenhui, ImageResource.zhizhenhuang, ImageResource.zhizhencheng, ImageResource.zhizhenhong
            };

            m_SpeedConverter = new ATP200HConvert();
            var tmp = new RectangleF(100, 30, 270, 270);
            m_SpeedPointer = new SpeedPointer(225,
                -45f,
                -45f,
                400,
                0,
                tmp,
                new PointF(tmp.X + tmp.Width/2, tmp.Y + tmp.Height/2),
                m_ImagePointer,
                m_SpeedConverter);

            #region B �� �� �� ģ ʽ �� Ϣ

            m_BText = new RectText();
            m_BText.SetBkColor(1, 2, 3);
            m_BText.SetTextColor(255, 255, 255);
            m_BText.SetTextStyle(16, FormatStyle.Center, true, "����");
            m_BText.SetTextRect(Common2D.RectB[5].X + 3, Common2D.RectB[5].Y + 3, Common2D.RectB[5].Width - 8, 35);

            #endregion
        }

        /// <summary>
        /// ���Ʒ���(�÷����ᱻ��ʱ����һ����ʱ�����ظ�����) 
        /// </summary>
        /// <param name="dcGs">���� GDI+ ��ͼ����</param>
        public override void paint(Graphics dcGs)
        {
            dcGs.SmoothingMode = SmoothingMode.HighQuality;
            RefreshShowType();

            if (ShowType == ShowType.Normal)
            {
                GetValue();
                RefreshData();
                RefreshDrawElement();
                OnDraw(dcGs);
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ݻ�ȡ����
        /// </summary>
        private void GetValue()
        {
            var adc = GlobalParam.Instance.DetailConfig.AdapterConfig;
            m_CurrentSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf��ǰ�ٶ�)]*adc.CurrentSpeedCoefficient));
            m_TargetSpeed =
                Math.Abs(Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.InfĿ���ٶ�)]*adc.TargetSpeedCoefficient));
            m_AllowSpeed =
                Math.Abs(Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf�����ٶ�)]*adc.PermitSpeedCoefficient));
                //�����ٶ�(����)
            m_ControlMode = ControModelEnumConvert.ConvertFrom(FloatList[this.GetInFloatIndex(InFloatKeys.Inf����ģʽ1��7)]);
            m_SbiSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf������ƶ��ٶ�SBI)]*
                                    adc.NormalBrakeSpeedCoefficient));
            m_EbiSpeed =
                Math.Abs(
                    Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf�����ƶ��ٶ�EBI)]*
                                    adc.EmergBrakeSpeedCoefficient));

            m_IsTsm = BoolList[this.GetInboolIndex(InBoolKeys.InbTSM��־)]; //TSM״̬
            m_IsCSM = BoolList[this.GetInboolIndex(InBoolKeys.InbCSM��־)]; //CSMģʽ
        }

        /// <summary>
        /// ˢ�����ݷ���
        /// </summary>
        private void RefreshData()
        {
        }

        /// <summary>
        /// ���Ʒ���
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            OnDrawV(g);
            DrawSpeedTable(g);
        }

        /// <summary>
        /// ˢ�»��� �� �̶��Լ� ָ��Ԫ��
        /// </summary>
        private void RefreshDrawElement()
        {
            SetXianShuPen();
            SetTargetArcPen();
            SetPointColor();
        }

        /// <summary>
        /// �ٶ����
        /// </summary>
        /// <param name="g"></param>
        public void OnDrawV(Graphics g)
        {
            switch (IsDrawArc())
            {
                case DrawModelEnum.DrawDetail:
                    //����������Ϣ
                    g.DrawArc(m_XianShuArcPen, m_X, m_Y, m_Width, m_Height, m_StartAngle, SetSweepAngle(m_AllowSpeed));
                    g.DrawArc(m_XianShuKeDuPen, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_AllowSpeed) - 3, 3);

                    //����Ŀ���ٶ�
                    g.DrawArc(m_TargetSpeedPen, m_X, m_Y, m_Width, m_Height, m_StartAngle, SetSweepAngle(m_TargetSpeed));
                    break;
                case DrawModelEnum.DrawKeDu:
                    //�������ٿ̶�
                    g.DrawArc(m_XianShuKeDuPen, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_AllowSpeed) - 3, 3);
                    break;
            }

            //���Ƶ�ǰ�ٶȳ����������
            if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed < m_EbiSpeed)
            {
                if (IsDrawCaoShuBiaoZhi())
                {
                    g.DrawArc(Common2D.OrangePen20, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_CurrentSpeed) - 3, 3);
                }
            }
            else if (m_CurrentSpeed > m_EbiSpeed)
            {
                if (IsDrawCaoShuBiaoZhi())
                {
                    g.DrawArc(Common2D.RedPen20, m_X + 8, m_Y + 8, m_Width - 16, m_Height - 16,
                        m_StartAngle + SetSweepAngle(m_SbiSpeed) - 3, 3);
                }
            }

            m_SpeedPointer.PaintPointerColor(g, m_Img[m_Izhizhen], m_CurrentSpeed);
            //��ʾ��ֵ�ٶ�
            g.DrawString(Convert.ToInt32(m_CurrentSpeed).ToString(), m_SpeedValueFont,
                Common2D.BlackBrush, m_X + 140, m_Y + 145, m_SpeedValueStringFormat);
        }

        private void DrawSpeedTable(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            #region ;;;;;;;;;B �� �� �� �� ��;;;;;;;;;;;;;;;;;;;

            //  g.DrawEllipse(Common2D.white_Pen2, Common2D.RectB[0].X +30, Common2D.RectB[0].Y +25,270, 270);
            for (int i = 0; i <= 30; i++)
            {
                if (i%5 == 0)
                {
                    g.DrawLine(Common2D.WhitePen2,
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 135*Math.Cos((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].Y + 160 - 135*Math.Sin((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 110*Math.Cos((i*9 - 45)*3.1415/180))
                        , Convert.ToInt32(Common2D.RectB[0].Y + 160 - 110*Math.Sin((i*9 - 45)*3.1415/180)));
                    if (i > 20)
                    {
                        g.DrawString(m_SpeedStr[i/5], Common2D.Font12, Common2D.WhiteBrush,
                            Convert.ToInt32(Common2D.RectB[0].X + 165 + 100*Math.Cos((i*9 - 45)*3.1415/180) - 7),
                            Convert.ToInt32(Common2D.RectB[0].Y + 160 - 100*Math.Sin((i*9 - 45)*3.1415/180) - 7));
                    }
                    else if (i <= 20)
                    {
                        g.DrawString(m_SpeedStr[i/5], Common2D.Font12, Common2D.WhiteBrush,
                            Convert.ToInt32(Common2D.RectB[0].X + 165 + 100*Math.Cos((i*9 - 45)*3.1415/180) - 20),
                            Convert.ToInt32(Common2D.RectB[0].Y + 160 - 100*Math.Sin((i*9 - 45)*3.1415/180) - 7));
                    }

                }
                else
                {
                    g.DrawLine(Common2D.WhitePen2,
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 135*Math.Cos((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].Y + 160 - 135*Math.Sin((i*9 - 45)*3.1415/180)),
                        Convert.ToInt32(Common2D.RectB[0].X + 165 + 125*Math.Cos((i*9 - 45)*3.1415/180))
                        , Convert.ToInt32(Common2D.RectB[0].Y + 160 - 125*Math.Sin((i*9 - 45)*3.1415/180)));
                }
            }

            #endregion
        }

        /// <summary>
        /// �ж��Ƿ���Ҫ���ƻ���
        /// </summary>
        /// <returns></returns>
        private DrawModelEnum IsDrawArc()
        {
            switch (m_ControlMode)
            {
                case ControModelEnum.����:
                case ControModelEnum.Ŀ��:
                case ControModelEnum.����:
                case ControModelEnum.����:
                    return DrawModelEnum.DrawKeDu;

                case ControModelEnum.LKJ:
                case ControModelEnum.����:
                    return DrawModelEnum.DrawNull;

                case ControModelEnum.��ȫ:
                    return DrawModelEnum.DrawDetail;
            }
            return DrawModelEnum.DrawNull;
        }

        #endregion


        #region ���û���Ԫ�صķ���

        /// <summary>
        /// ���û�ֵ���ٿ̶�,�Լ����λ�ˢ��ɫ
        /// </summary>
        /// <returns></returns>
        private void SetXianShuPen()
        {
            if (m_IsCSM) //������CSM��
            {
                m_XianShuKeDuPen = Common2D.GrayPen20;
                m_XianShuArcPen = Common2D.GrayPen12;
            }
            else if (m_IsTsm) //������TSM��
            {
                m_XianShuKeDuPen = Common2D.YellowPen20;
                m_XianShuArcPen = Common2D.YellowPen12;
            }
        }

        /// <summary>
        /// ����Ŀ���ٶ�Բ��
        /// </summary>
        private void SetTargetArcPen()
        {
            if (m_IsCSM) //������CSM��
            {
                m_TargetSpeedPen = Common2D.DarkGrayPen12;
            }
            else if (m_IsTsm) //������TSM��
            {
                m_TargetSpeedPen = Common2D.GrayPen12;
            }
        }

        /// <summary>
        /// ���ýǶ�
        /// </summary>
        private int SetSweepAngle(int allowSpeed)
        {
            if (allowSpeed <= 200)
            {
                return Convert.ToInt32(allowSpeed*135/150f);
            }
            if (allowSpeed <= 400)
            {
                return Convert.ToInt32((allowSpeed - 200)*135/300f + 180);
            }

            return 0;
        }

        /// <summary>
        /// ����ָ����ɫ
        /// </summary>
        private void SetPointColor()
        {
            if (m_IsCSM)
            {
                if (m_CurrentSpeed <= m_AllowSpeed)
                {
                    m_Izhizhen = 0;
                }
                else if (m_CurrentSpeed > m_AllowSpeed && m_CurrentSpeed <= m_EbiSpeed)
                {
                    m_Izhizhen = 2;
                }
                else
                {
                    m_Izhizhen = 3;
                }
            }

            if (m_IsTsm)
            {
                if (m_CurrentSpeed < m_TargetSpeed)
                {
                    m_Izhizhen = 0;
                }
                else if (m_CurrentSpeed < m_AllowSpeed)
                {
                    m_Izhizhen = 1;
                }
                else if (m_CurrentSpeed < m_EbiSpeed)
                {
                    m_Izhizhen = 2;
                }
                else
                {
                    m_Izhizhen = 3;
                }
            }

            //LKJ ����ģʽָ��һֱ��ɫ
            if (IsDrawCaoShuBiaoZhi() == false)
            {
                m_Izhizhen = 0;
            }
        }

        /// <summary>
        /// �ж��Ƿ���Ҫ����Ԥ����Ϣ
        /// </summary>
        /// <returns></returns>
        private bool IsDrawCaoShuBiaoZhi()
        {
            if (m_ControlMode == ControModelEnum.LKJ || m_ControlMode == ControModelEnum.����)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}