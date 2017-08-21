using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.自动速度控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIASC : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnContentBuffer =
        {
            "ASC\n关闭", "2km/h", "5km/h", "10km/h", "25km/h", "最大\n速度", "",
            "", "", "主页面"
        };

        private AutoSpeedControl m_AutoSpeedControl;

        public static BottomBtnType CurrentSelectdBtnType;

        private readonly List<string> m_OutBNameIndexs = new List<string>
        {
            OutBoolKeys.Oub自动速度控制_速度设定关闭,
            OutBoolKeys.Oub自动速度控制_2kmh连挂速度,
            OutBoolKeys.Oub自动速度控制_5kmh,
            OutBoolKeys.Oub自动速度控制_10kmh,
            OutBoolKeys.Oub自动速度控制_25kmh,
            OutBoolKeys.Oub最大速度,
        };

        public override string GetInfo()
        {
            return "DMI自动速度控制";
        }


        public override bool Initalize()
        {
            m_AutoSpeedControl = new AutoSpeedControl();
            m_AutoSpeedControl.SeletChanged +=
                control =>
                {
                    UpdateBtnContent(control);

                    m_OutBNameIndexs.ForEach(e => append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(e), 0, 0));

                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(m_OutBNameIndexs[(int)control.CurrentSeletedSpeed.BtnType]), 1,
                        0);

                    DMITitle.IsASCModel = control.CurrentSeletedSpeed != control.AutoSpeedControlModelCollection[0];
                };
            CurrentSelectdBtnType = BottomBtnType.Btn01;

            m_AutoSpeedControl.Select(CurrentSelectdBtnType);

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIASC, ref m_RectsList))
            {

            }

            return true;
        }

        private void UpdateBtnContent(AutoSpeedControl control)
        {
            control.AutoSpeedControlModelCollection.Select(s => s.BtnContent).ToArray().CopyTo(m_BtnContentBuffer, 0);
            for (int i = 1; i < m_BtnContentBuffer.Length; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnContentBuffer[i];
            }
            DMITitle.BtnContentList[0].BtnStr = control.CurrentSeletedSpeed ==
                                                m_AutoSpeedControl.AutoSpeedControlModelCollection[0]
                ? String.Empty
                : m_BtnContentBuffer[0];
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (ParaADefine.SwitchFromOhter == nParaA)
            {
                UpdateState();

                UpdateBtnContent(m_AutoSpeedControl);
            }
        }

        public override void Paint(Graphics g)
        {
            UpdateState();

            DrawViewContent(g);
        }

        private void UpdateState()
        {
            m_AutoSpeedControl.Select(CurrentSelectdBtnType);

            ResponseBtnEvent();
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }
            switch (DMIButton.BtnUpList[0])
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
                case 6: //ASC关闭
                    if (DMITitle.BtnContentList[0].BtnStr == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        m_AutoSpeedControl.Select(BottomBtnType.Btn01);
                        DMITitle.BtnContentList[0].BtnStr = string.Empty;
                    }

                    break;
                case 7: //2km/h
                    m_AutoSpeedControl.Select(BottomBtnType.Btn02);
                    DMITitle.BtnContentList[0].BtnStr = m_BtnContentBuffer[0];
                    break;
                case 8: //5km/h
                    m_AutoSpeedControl.Select(BottomBtnType.Btn03);
                    DMITitle.BtnContentList[0].BtnStr = m_BtnContentBuffer[0];
                    break;
                case 9: //10km/h
                    m_AutoSpeedControl.Select(BottomBtnType.Btn04);
                    DMITitle.BtnContentList[0].BtnStr = m_BtnContentBuffer[0];
                    break;
                case 10: //25km/h
                    m_AutoSpeedControl.Select(BottomBtnType.Btn05);
                    DMITitle.BtnContentList[0].BtnStr = m_BtnContentBuffer[0];
                    break;
                case 11: //最大速度
                    m_AutoSpeedControl.Select(BottomBtnType.Btn06);
                    DMITitle.BtnContentList[0].BtnStr = m_BtnContentBuffer[0];
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }

            CurrentSelectdBtnType = m_AutoSpeedControl.CurrentSeletedSpeed.BtnType;
        }


        private void DrawViewContent(Graphics g)
        {
            g.DrawString("开关;自动速度控制(ASC)",
                FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1],
                FontsItems.TheAlignment(FontRelated.靠左));

            foreach (var autoSpeedControlModel in m_AutoSpeedControl.AutoSpeedControlModelCollection)
            {
                g.DrawString(autoSpeedControlModel.ViewContent,
                    FontsItems.FontC24B,
                    GetViewContentBrush(autoSpeedControlModel),
                    GetViewContentRegion(autoSpeedControlModel),
                    FontsItems.TheAlignment(FontRelated.靠左));
            }

            var currentSelected = m_AutoSpeedControl.CurrentSeletedSpeed;
            g.FillRectangle(GetViewContentRegionBrush(currentSelected), GetViewContentRegion(currentSelected));
            g.DrawString(currentSelected.ViewContent,
                FontsItems.FontC24B,
                GetViewContentBrush(currentSelected),
                GetViewContentRegion(currentSelected),
                FontsItems.TheAlignment(FontRelated.靠左));
        }

        private RectangleF GetViewContentRegion(AutoSpeedControlModel model)
        {
            return m_RectsList[(13 + (int) model.BtnType)];
        }


        private Brush GetViewContentBrush(AutoSpeedControlModel model)
        {
            if (DMITitle.NightMode)
            {
                return model.IsSelected ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
            }
            return model.IsSelected ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush;
        }

        private Brush GetViewContentRegionBrush(AutoSpeedControlModel model)
        {
            if (DMITitle.NightMode)
            {
                return model.IsSelected ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush;
            }
            return model.IsSelected ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
        }
    }
}
