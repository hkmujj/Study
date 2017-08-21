using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using ATP200C.Common;
using ATP200C.Domain;
using ATP200C.PublicComponents;
using ATP200C.Resource.Images;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPMain : ATPBaseClass
    {
        public static ATPMain Instance { private set; get; }

        /// <summary>
        /// ��ǰ����ģʽ
        /// </summary>
        public ControModelEnum ControlModel { private set; get; }

        private TrainInfo m_TaTrainInfo = new TrainInfo();

        /// <summary>
        /// ��ǰ�źŵȼ�
        /// </summary>
        public CTCSLevel CTCSLevel { private set; get; }

        private GtRectText m_BText; //B �� �� �� ģ ʽ �� Ϣ
        private GtRectText m_CText; //C �� �� �� �� �� ״ ̬
        private readonly GtRectText[] m_EText = new GtRectText[2]; //E �� �� �� �� �� ģ ʽ
        private float[] m_Valuef;
        private bool[] m_Valueb;
        public float m_StartAngle = 135.0F;
        public float m_SweepAngle;

        /// <summary>
        /// �ƶ��ȼ�
        ///  0����ɶ��û��
        ///  1�������ƶ�
        ///  2��������ƶ�
        ///  3����������
        /// </summary>
        public BrakeLevel BrakeLevel { private set; get; }

        private float[] m_TrainInfo;
        private IFloatValueExpression m_ValueExpression;

        public override string GetInfo()
        {
            return "������2D";
        }

        public override bool Initalize()
        {

            Instance = this;

            m_Valuef = new float[UIObj.InFloatList.Count];
            m_Valueb = new bool[UIObj.InBoolList.Count];

            #region ;;;;;;;;;B �� �� �� ģ ʽ �� Ϣ;;;;;;;;;;

            m_BText = new GtRectText();
            m_BText.SetBkColor(1, 2, 3);
            m_BText.SetTextColor(255, 255, 255);
            m_BText.SetTextStyle(16, FormatStyle.Center, true, "����");
            m_BText.SetTextRect(ATP200CCommon.RectB[5].X + 3, ATP200CCommon.RectB[5].Y + 3,
                ATP200CCommon.RectB[5].Width - 8, 35);

            #endregion

            #region ;;;;;;;;;;;;C �� �� �� �� �� ״ ̬;;;;;;;;;;;;;;

            m_CText = new GtRectText();
            m_CText.SetBkColor(1, 2, 3);
            m_CText.SetTextColor(255, 255, 255);
            m_CText.SetTextStyle(14, FormatStyle.Center, true, "����");
            m_CText.SetTextRect(ATP200CCommon.RectC[7].X, ATP200CCommon.RectC[7].Y, ATP200CCommon.RectC[7].Width,
                ATP200CCommon.RectC[7].Height);
            //C_text.isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;E �� �� �� �� �� ģ ʽ;;;;;;;;;;;;;;;

            m_EText[0] = new GtRectText();
            m_EText[0].SetBkColor(1, 2, 3);
            m_EText[0].SetTextColor(255, 255, 255);
            m_EText[0].SetTextStyle(16, FormatStyle.Center, true, "����");
            m_EText[0].SetTextRect(ATP200CCommon.RectE[2].X + 2, ATP200CCommon.RectE[2].Y + 2,
                ATP200CCommon.RectE[2].Width - 4, ATP200CCommon.RectE[2].Height - 4);
            //E_text[0].isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;E �� ��˫�� ģ ʽ;;;;;;;;;;;;;;;

            m_EText[1] = new GtRectText();
            m_EText[1].SetBkColor(1, 2, 3);
            m_EText[1].SetTextColor(255, 255, 255);
            m_EText[1].SetTextStyle(16, FormatStyle.Center, true, "����");
            m_EText[1].SetTextRect(ATP200CCommon.RectE[3].X + 2, ATP200CCommon.RectE[3].Y + 2,
                ATP200CCommon.RectE[3].Width - 4, ATP200CCommon.RectE[3].Height - 4);
            //E_text[0].isdrawrectfrm = true;

            #endregion

            #region;;;;;;;�� �� ͼ Ƭ;;;;;;;

            #endregion

            m_ValueExpression = new TrainInfoExpression();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (!OutBoolList[UIObj.OutBoolList[0]] &&
                    (ControlModel == ControModelEnum.���� || ControlModel == ControModelEnum.Null))
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                }
            }
        }

        public override void paint(Graphics g)
        {
            append_postCmd(CmdType.SetBoolValue, 4801, 1, 1);
            GetValue();
            ReFreshData();
            DrawOn(g);
        }

        public void GetValue()
        {
            for (int i = 0; i < m_Valuef.Length; i++)
            {
                m_Valuef[i] = FloatList[UIObj.InFloatList[i]];
            }
            for (int i = 0; i < m_Valueb.Length; i++)
            {
                m_Valueb[i] = BoolList[UIObj.InBoolList[i]];
            }

            try
            {
                m_TrainInfo = UIObj.InFloatList.Skip(21).Take(3).Select(s => FloatList[s]).ToArray();
            }
            catch (Exception e)
            {
            }

        }

        public void ReFreshData()
        {
            if (m_Valueb[0])
            {
                TrainControlMode.CurrentTrainControl = ControlModeName.CSM;
            }

            if (m_Valueb[1])
            {
                TrainControlMode.CurrentTrainControl = ControlModeName.TSM;
            }

            #region :::::::: ��ȡ����ģʽ

            RefreshControlModel();

            m_BText.SetText(ControlModel != ControModelEnum.Null ? ControlModel.ToString() : String.Empty);

            #endregion

            #region :::::::: �� �� CTCS�ȼ�

            RefreshCTCSLevel();

            #endregion


            //�ƶ��ȼ�
            BrakeLevel = (BrakeLevel) Convert.ToInt32(m_Valuef[7]);

            if (m_Valueb[4])
            {
                m_EText[0].SetText("����");
            }
            else if (m_Valueb[5])
            {
                m_EText[0].SetText("�˿�");
            }
            else
            {
                m_EText[0].SetText("");
            }

            m_EText[1].SetText(OutBoolList[UIObj.OutBoolList[0]] ? "˫��" : "����");

        }

        private void RefreshCTCSLevel()
        {
            switch (Convert.ToInt32(m_Valuef[6])) //CTCS�ȼ�
            {
                case 0:
                    CTCSLevel = CTCSLevel.CTCS0;
                    m_CText.SetText("CTCS 0");
                    break;
                case 1:
                    CTCSLevel = CTCSLevel.CTCS1;
                    m_CText.SetText("CTCS 1");
                    break;
                case 2:
                    CTCSLevel = CTCSLevel.CTCS2;
                    m_CText.SetText("CTCS 2");
                    break;
                default:
                    CTCSLevel = CTCSLevel.None;
                    m_CText.SetText(" ");
                    break;
            }
        }

        private void RefreshControlModel()
        {
            switch (Convert.ToInt32(m_Valuef[5]))
            {
                case 1:
                    ControlModel = ControModelEnum.��ȫ;
                    break;
                case 2:
                    ControlModel = ControModelEnum.����;
                    break;
                case 3:
                    ControlModel = ControModelEnum.Ŀ��;
                    break;
                case 4:
                    ControlModel = ControModelEnum.����;
                    break;
                case 5:
                    ControlModel = ControModelEnum.����;
                    break;
                case 6:
                    ControlModel = ControModelEnum.LKJ;
                    break;
                case 7:
                    ControlModel = ControModelEnum.����;
                    break;
                case 8:
                    ControlModel = ControModelEnum.����;
                    break;
                case 10:
                    ControlModel = ControModelEnum.ð��;
                    break;
                case 11:
                    ControlModel = ControModelEnum.ð��;
                    break;
                default:
                    ControlModel = ControModelEnum.Null;
                    break;
                    ;
            }
        }

        public void DrawOn(Graphics g)
        {

            DrawUpDownArrow(g);

            //B��ģʽ��Ϣ��ʾ
            m_BText.OnDraw(g);

            //C �� �� �� �� �� ״ ̬
            m_CText.OnDraw(g);

            //E��
            for (int i = 0; i < 2; i++)
            {
                m_EText[i].OnDraw(g);
            }

            DrawBrakeLevel(g);

            //E �� ˾���� ���κ���Ϣ
            g.DrawString("˾����:", ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectAreaE[2]);
            g.DrawString("���κ�", ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectAreaE[3]);
            if (Math.Abs(m_TrainInfo[0]) > float.Epsilon)
            {
                m_TaTrainInfo = (TrainInfo) m_ValueExpression.Interprete(m_TrainInfo);
                GlobalParam.Instance.TrainInfo.TrainId = m_TaTrainInfo.TrainId;
            }
            else
            {
                m_TaTrainInfo.TrainId = GlobalParam.Instance.TrainInfo.TrainId;
                if (Math.Abs(m_TrainInfo[2]) < float.Epsilon)
                {
                    m_TaTrainInfo.DriverId = GlobalParam.Instance.TrainInfo.DriverId;
                }
                else
                {
                    m_TaTrainInfo.DriverId = m_TrainInfo[2].ToString(CultureInfo.InvariantCulture);
                    GlobalParam.Instance.TrainInfo.DriverId = m_TaTrainInfo.DriverId;
                }

            }
            g.DrawString(m_TaTrainInfo.DriverId, ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush,
                ATP200CCommon.RectAreaE[4]);
            g.DrawString(m_TaTrainInfo.TrainId, ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush,
                ATP200CCommon.RectAreaE[5]);

            //E �� �� ʾ ʱ �� �� �� ��
            g.DrawString(CurrentTime.ToString("T"), ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush,
                ATP200CCommon.RectAreaE[6]);

        }

        private void DrawUpDownArrow(Graphics g)
        {
            var up = TextInfoManager.Instance.CanGoBack
                ? ImageResource.Up_1
                : ImageResource.Up_0;
            g.DrawRectangle(ATP200CCommon.LinePen, ATP200CCommon.RectAreaE[0]);
            g.DrawImage(up, ATP200CCommon.RectAreaE[0]);

            var down = TextInfoManager.Instance.CanGoForward ? ImageResource.Down_1 : ImageResource.Down_0;
            g.DrawRectangle(ATP200CCommon.LinePen, ATP200CCommon.RectAreaE[1]);
            g.DrawImage(down, ATP200CCommon.RectAreaE[1]);

        }

        private void DrawBrakeLevel(Graphics g)
        {
            if (BrakeLevel == BrakeLevel.Brake1)
            {
                g.DrawImage(ImageResource.zhidong_Level1, ATP200CCommon.RectC[8]);
                g.DrawString("1", ATP200CCommon.Font12B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectC[8].X + 28,
                    ATP200CCommon.RectC[8].Y + 10);
            }
            else if (BrakeLevel == BrakeLevel.Brake4)
            {
                g.DrawImage(ImageResource.zhiDong_level2, ATP200CCommon.RectC[8]);
                g.DrawString("4", ATP200CCommon.Font12B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectC[8].X + 28,
                    ATP200CCommon.RectC[8].Y + 10);
            }
            else if (BrakeLevel == BrakeLevel.Brake7)
            {
                g.DrawImage(ImageResource.zhiDong_Level3, ATP200CCommon.RectC[8]);
                g.DrawString("7", ATP200CCommon.Font12B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectC[8].X + 28,
                    ATP200CCommon.RectC[8].Y + 10);
            }
            else if (BrakeLevel == BrakeLevel.EmergencyBrake)
            {
                g.DrawImage(ImageResource.zhiDong_Level4, ATP200CCommon.RectC[8]);
            }
            else if (BrakeLevel == BrakeLevel.AllowEase)
            {
                g.DrawImage(ImageResource.zhiDong_Level5, ATP200CCommon.RectC[8]);
            }
        }
    }
}
