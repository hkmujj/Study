using System;
using System.Drawing;
using CommonUtil.Model;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.开关.Lighting;
using Motor.HMI.CRH3C380B.Common;
using ProjectType = Motor.HMI.CRH3C380B.Common.ProjectType;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMILighting : CRH3C380BBase
    {
        // private List<ILightingView> m_LightingViewCollection;
        private ILightingView m_CurrentLightingView;

        private string[] m_BottomBtnContentCollection;

        private Action<Graphics> m_Action;
        private Rectangle m_DrawRectangle;

        public override string GetInfo()
        {
            return "开关/照明";
        }

        public override bool Initalize()
        {
            m_BottomBtnContentCollection = new string[DMITitle.BtnContentList.Count];
            m_BottomBtnContentCollection[DMITitle.BtnContentList.Count - 1] = "主页面";

            //m_LightingViewCollection = new List<ILightingView>() { new LightingView8(this), new LightingView16(this) };
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                m_CurrentLightingView = new LightingViewBL(this);
            }
            else
            {
                m_CurrentLightingView = new LightingView8(this);

                DMITitle.MarshallModeChanged += title =>
                {
                    m_CurrentLightingView = title.MarshallMode ? new LightingView16(this) : new LightingView8(this);
                };
            }

            m_CurrentLightingView.CurrentSelectedLightingUnitChanged += OnSelectedLightingItemChanged;
            m_CurrentLightingView.LightingStateChanged += OnLightingStateChanged;
            m_Action = g => g.FillRectangle(Brushes.Blue, m_DrawRectangle);
            m_DrawRectangle = new Rectangle(new Point(40, 40), new Size(40, 40));
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                UpdateBottomBtn();

                DMITitle.TitleName = "开关; 照明";
            }
        }

        public override void Paint(Graphics g)
        {
            RespondBtnEvent();
            m_Action(g);
            m_CurrentLightingView.OnPaint(g);
        }

        private void RespondBtnEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 190, 0, 0);
                    break;
                case 1:
                    // 左
                    m_CurrentLightingView.Select(Direction.Left);
                    break;
                case 2:
                    // 右
                    m_CurrentLightingView.Select(Direction.Right);
                    break;
                case 3:
                    // 上
                    m_CurrentLightingView.Select(Direction.Up);
                    break;
                case 4:
                    // 下
                    m_CurrentLightingView.Select(Direction.Down);
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void OnSelectedLightingItemChanged(ILightingView lightingView)
        {
            UpdateBottomBtn();
            if (lightingView.CurrentSelectedLightingUnit == null)
            {
                m_Action = g => g.FillRectangle(Brushes.Blue, m_DrawRectangle);
            }
            else
            {
                m_Action = g => g.FillRectangle(Brushes.Black, m_DrawRectangle);
            }
        }


        private void OnLightingStateChanged(ILightStateItem lightStateItem)
        {
            UpdateBottomBtn();
        }

        private void ResetBottomBtnContent()
        {
            for (int i = 0; i < m_BottomBtnContentCollection.Length - 1; i++)
            {
                m_BottomBtnContentCollection[i] = string.Empty;
            }

            if (m_CurrentLightingView.CurrentSelectedLightingUnit != null)
            {
                switch (m_CurrentLightingView.CurrentSelectedLightingUnit.State)
                {
                    case LightState.Light0:
                        m_BottomBtnContentCollection[1] = "照明\n1";
                        m_BottomBtnContentCollection[2] = "照明\n1/3";
                        break;
                    case LightState.Light1:
                        m_BottomBtnContentCollection[0] = "照明\n0";
                        m_BottomBtnContentCollection[2] = "照明\n1/3";
                        break;
                    case LightState.Light1P3:
                        m_BottomBtnContentCollection[0] = "照明\n0";
                        m_BottomBtnContentCollection[1] = "照明\n1";
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