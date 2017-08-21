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
        /// 当前控制模式
        /// </summary>
        public ControModelEnum ControlModel { private set; get; }

        private TrainInfo m_TaTrainInfo = new TrainInfo();

        /// <summary>
        /// 当前信号等级
        /// </summary>
        public CTCSLevel CTCSLevel { private set; get; }

        private GtRectText m_BText; //B 区 控 制 模 式 信 息
        private GtRectText m_CText; //C 区 设 备 运 行 状 态
        private readonly GtRectText[] m_EText = new GtRectText[2]; //E 区 人 控 机 控 模 式
        private float[] m_Valuef;
        private bool[] m_Valueb;
        public float m_StartAngle = 135.0F;
        public float m_SweepAngle;

        /// <summary>
        /// 制动等级
        ///  0代表啥都没有
        ///  1代表常用制动
        ///  2代表紧急制动
        ///  3代表允许缓解
        /// </summary>
        public BrakeLevel BrakeLevel { private set; get; }

        private float[] m_TrainInfo;
        private IFloatValueExpression m_ValueExpression;

        public override string GetInfo()
        {
            return "主界面2D";
        }

        public override bool Initalize()
        {

            Instance = this;

            m_Valuef = new float[UIObj.InFloatList.Count];
            m_Valueb = new bool[UIObj.InBoolList.Count];

            #region ;;;;;;;;;B 区 控 制 模 式 信 息;;;;;;;;;;

            m_BText = new GtRectText();
            m_BText.SetBkColor(1, 2, 3);
            m_BText.SetTextColor(255, 255, 255);
            m_BText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_BText.SetTextRect(ATP200CCommon.RectB[5].X + 3, ATP200CCommon.RectB[5].Y + 3,
                ATP200CCommon.RectB[5].Width - 8, 35);

            #endregion

            #region ;;;;;;;;;;;;C 区 设 备 运 行 状 态;;;;;;;;;;;;;;

            m_CText = new GtRectText();
            m_CText.SetBkColor(1, 2, 3);
            m_CText.SetTextColor(255, 255, 255);
            m_CText.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_CText.SetTextRect(ATP200CCommon.RectC[7].X, ATP200CCommon.RectC[7].Y, ATP200CCommon.RectC[7].Width,
                ATP200CCommon.RectC[7].Height);
            //C_text.isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;E 区 人 控 机 控 模 式;;;;;;;;;;;;;;;

            m_EText[0] = new GtRectText();
            m_EText[0].SetBkColor(1, 2, 3);
            m_EText[0].SetTextColor(255, 255, 255);
            m_EText[0].SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_EText[0].SetTextRect(ATP200CCommon.RectE[2].X + 2, ATP200CCommon.RectE[2].Y + 2,
                ATP200CCommon.RectE[2].Width - 4, ATP200CCommon.RectE[2].Height - 4);
            //E_text[0].isdrawrectfrm = true;

            #endregion

            #region ;;;;;;;;E 区 单双组 模 式;;;;;;;;;;;;;;;

            m_EText[1] = new GtRectText();
            m_EText[1].SetBkColor(1, 2, 3);
            m_EText[1].SetTextColor(255, 255, 255);
            m_EText[1].SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_EText[1].SetTextRect(ATP200CCommon.RectE[3].X + 2, ATP200CCommon.RectE[3].Y + 2,
                ATP200CCommon.RectE[3].Width - 4, ATP200CCommon.RectE[3].Height - 4);
            //E_text[0].isdrawrectfrm = true;

            #endregion

            #region;;;;;;;加 载 图 片;;;;;;;

            #endregion

            m_ValueExpression = new TrainInfoExpression();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (!OutBoolList[UIObj.OutBoolList[0]] &&
                    (ControlModel == ControModelEnum.待机 || ControlModel == ControModelEnum.Null))
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

            #region :::::::: 获取控制模式

            RefreshControlModel();

            m_BText.SetText(ControlModel != ControModelEnum.Null ? ControlModel.ToString() : String.Empty);

            #endregion

            #region :::::::: 更 新 CTCS等级

            RefreshCTCSLevel();

            #endregion


            //制动等级
            BrakeLevel = (BrakeLevel) Convert.ToInt32(m_Valuef[7]);

            if (m_Valueb[4])
            {
                m_EText[0].SetText("机控");
            }
            else if (m_Valueb[5])
            {
                m_EText[0].SetText("人控");
            }
            else
            {
                m_EText[0].SetText("");
            }

            m_EText[1].SetText(OutBoolList[UIObj.OutBoolList[0]] ? "双组" : "单组");

        }

        private void RefreshCTCSLevel()
        {
            switch (Convert.ToInt32(m_Valuef[6])) //CTCS等级
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
                    ControlModel = ControModelEnum.完全;
                    break;
                case 2:
                    ControlModel = ControModelEnum.部分;
                    break;
                case 3:
                    ControlModel = ControModelEnum.目视;
                    break;
                case 4:
                    ControlModel = ControModelEnum.引导;
                    break;
                case 5:
                    ControlModel = ControModelEnum.调车;
                    break;
                case 6:
                    ControlModel = ControModelEnum.LKJ;
                    break;
                case 7:
                    ControlModel = ControModelEnum.待机;
                    break;
                case 8:
                    ControlModel = ControModelEnum.隔离;
                    break;
                case 10:
                    ControlModel = ControModelEnum.冒进;
                    break;
                case 11:
                    ControlModel = ControModelEnum.冒后;
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

            //B区模式信息显示
            m_BText.OnDraw(g);

            //C 区 设 备 运 行 状 态
            m_CText.OnDraw(g);

            //E区
            for (int i = 0; i < 2; i++)
            {
                m_EText[i].OnDraw(g);
            }

            DrawBrakeLevel(g);

            //E 区 司机号 车次号信息
            g.DrawString("司机号:", ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectAreaE[2]);
            g.DrawString("车次号", ATP200CCommon.Font10B, ATP200CCommon.WhiteBrush, ATP200CCommon.RectAreaE[3]);
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

            //E 区 显 示 时 间 和 日 期
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
