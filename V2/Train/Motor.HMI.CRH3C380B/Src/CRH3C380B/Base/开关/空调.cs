using System;
using System.Drawing;
using CommonUtil.Model;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.开关.AirConditioning;
using Motor.HMI.CRH3C380B.Common;
using ProjectType = Motor.HMI.CRH3C380B.Common.ProjectType;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMIAirCondition : CRH3C380BBase
    {
        private IAirconditionView m_CurrentAirConditionView;
        private Action<Graphics> m_Action;
        private Rectangle m_DrawRectangle;
        private string[] m_BottomBtnContentCollection;

        public override string GetInfo()
        {
            return "开关/空调";
        }

        public override bool Initalize()
        {
            m_BottomBtnContentCollection = new string[DMITitle.BtnContentList.Count];
            m_BottomBtnContentCollection[DMITitle.BtnContentList.Count - 1] = "主页面";

            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                m_CurrentAirConditionView = new AirconditionViewBL(this);
            }
            else
            {
                m_CurrentAirConditionView = new AirconditionView8(this);
                DMITitle.MarshallModeChanged += title =>
                {
                    m_CurrentAirConditionView = DMITitle.MarshallMode
                        ? new AirconditionView16(this)
                        : new AirconditionView8(this);
                };
            }

            m_CurrentAirConditionView.CurrentSelectedAirconditionUnitChanged += OnSelectedAirconditionItemChanged;
            m_CurrentAirConditionView.AirconditionStateChanged += OnAirconditionStateChanged;
            m_Action = g => g.FillRectangle(Brushes.Blue, m_DrawRectangle);
            m_DrawRectangle = new Rectangle(new Point(40, 40), new Size(40, 40));
            return true;
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                UpdateBottomBtn();

                DMITitle.TitleName = "开关; 空调";
            }
        }

        public override void Paint(Graphics g)
        {
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex("所有单车空调紧急关闭"), 0, 0);
            RespondBtnEvent();
            m_Action(g);
            // m_CurrentAirConditionView.OnDraw(g);
            m_CurrentAirConditionView.OnPaint(g);
        }

        private void RespondBtnEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    append_postCmd(CmdType.ChangePage, 190, 0, 0);
                    break;
                case 1: //左
                    m_CurrentAirConditionView.Select(Direction.Left);
                    break;
                case 2: //右
                    m_CurrentAirConditionView.Select(Direction.Right);
                    break;
                case 3: //上
                    m_CurrentAirConditionView.Select(Direction.Up);
                    break;
                case 4: //下
                    m_CurrentAirConditionView.Select(Direction.Down);
                    break;
                case 13: //空调紧急关
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex("所有单车空调紧急关闭"), 1, 0);
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void OnSelectedAirconditionItemChanged(IAirconditionView airconditionView)
        {
            UpdateBottomBtn();
            if (airconditionView.CurrentSelectedAirconditionUnit == null)
            {
                m_Action = g => g.FillRectangle(Brushes.Blue, m_DrawRectangle);
            }
            else
            {
                m_Action = g => g.FillRectangle(Brushes.Black, m_DrawRectangle);
            }
        }


        private void OnAirconditionStateChanged(IAirconditionStateItem airconditionStateItem)
        {
            UpdateBottomBtn();
        }

        private void ResetBottomBtnContent()
        {
            for (int i = 0; i < m_BottomBtnContentCollection.Length - 1; i++)
            {
                m_BottomBtnContentCollection[i] = string.Empty;
            }

            m_BottomBtnContentCollection[7] = "空调\n紧急关";
            if (m_CurrentAirConditionView.CurrentSelectedAirconditionUnit != null)
            {
                switch (m_CurrentAirConditionView.CurrentSelectedAirconditionUnit.State)
                {
                    case AirConditionState.Default:
                        m_BottomBtnContentCollection[1] = "空调关(防冻)\n";
                        m_BottomBtnContentCollection[2] = "空调开\n";
                        break;
                    case AirConditionState.Open:
                        m_BottomBtnContentCollection[0] = "空调开\n";
                        m_BottomBtnContentCollection[2] = "温度设定值\n";
                        break;
                    case AirConditionState.SetTemperature:
                        m_BottomBtnContentCollection[0] = "温度设定值\n";
                        m_BottomBtnContentCollection[1] = "空调紧急关\n";
                        break;
                    case AirConditionState.EmergaceClose:
                        m_BottomBtnContentCollection[0] = "空调紧急关\n";
                        m_BottomBtnContentCollection[1] = "空调关(防冻)\n";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateBottomBtn()
        {
            ResetBottomBtnContent();
            DMITitle.UpdateBtnContent(m_BottomBtnContentCollection);
        }

    }
}