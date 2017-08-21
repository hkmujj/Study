using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DMICloseCouplerHatches : CRH3C380BBase
    {
        private List<ICloseCouplerHatchesView> m_CloseCouplerHatchesViewCollection;
        private ICloseCouplerHatchesView m_CurrentCloseCouplerHatchesView;

        private string[] m_BottomBtnContentCollection;

        public override string GetInfo()
        {
            return "系统/关闭车钩罩";
        }

        public override bool Initalize()
        {
            m_BottomBtnContentCollection = new string[DMITitle.BtnContentList.Count];
            m_BottomBtnContentCollection[DMITitle.BtnContentList.Count - 1] = "主页面";
            m_CloseCouplerHatchesViewCollection = new List<ICloseCouplerHatchesView>
            {
                new CloseCouplerHatchesView1(this),
                new CloseCouplerHatchesView2(this)
            };
            m_CurrentCloseCouplerHatchesView = m_CloseCouplerHatchesViewCollection[0];

            DMITitle.MarshallModeChanged += m =>
            {
                m_CurrentCloseCouplerHatchesView = DMITitle.MarshallMode
                    ? m_CloseCouplerHatchesViewCollection[1]
                    : m_CloseCouplerHatchesViewCollection[0];
                m_CurrentCloseCouplerHatchesView.Reset(DMITitle.MarshallMode);

            };
            return true;
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                UpdateBottomBtn();

                DMITitle.TitleName = "系统; 关闭车钩罩";
            }
        }

        public override void Paint(Graphics g)
        {
            ResponseBtnEvent();

            m_CurrentCloseCouplerHatchesView.OnPaint(g);
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                }
            }
        }

        private void ResetBottomBtnContent()
        {
            for (int i = 0; i < m_BottomBtnContentCollection.Length - 1; i++)
            {
                m_BottomBtnContentCollection[i] = string.Empty;
            }

            if (m_CurrentCloseCouplerHatchesView.CurrentSelectedCloseCouplerHatchesUnit == null)
            {
                return;
            }

            switch (m_CurrentCloseCouplerHatchesView.CurrentSelectedCloseCouplerHatchesUnit.State)
            {
                case CouplerHatchesState.Close:
                    m_BottomBtnContentCollection[0] = "";
                    m_BottomBtnContentCollection[2] = "";
                    break;
                case CouplerHatchesState.Open:
                    m_BottomBtnContentCollection[0] = "关闭前车钩罩";
                    m_BottomBtnContentCollection[2] = "关闭后车钩罩";
                    break;
                case CouplerHatchesState.Motion:
                    m_BottomBtnContentCollection[0] = "";
                    m_BottomBtnContentCollection[2] = "";
                    break;
                case CouplerHatchesState.Coupled:
                    m_BottomBtnContentCollection[0] = "";
                    m_BottomBtnContentCollection[2] = "";
                    break;
                case CouplerHatchesState.PlanCoupl:
                    m_BottomBtnContentCollection[0] = "";
                    m_BottomBtnContentCollection[2] = "";
                    break;
                case CouplerHatchesState.Breakdown:
                    m_BottomBtnContentCollection[0] = "";
                    m_BottomBtnContentCollection[2] = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateBottomBtn()
        {
            ResetBottomBtnContent();
            DMITitle.UpdateBtnContent(m_BottomBtnContentCollection);
        }
    }
}