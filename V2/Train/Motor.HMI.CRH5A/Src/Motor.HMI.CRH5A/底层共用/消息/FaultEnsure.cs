using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5A.Resource.Images;
using Motor.HMI.CRH5A.Staus;

namespace Motor.HMI.CRH5A.底层共用.消息
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultEnsure : CRH5ABase
    {



        private List<RectangleF> m_RectsList;

        private int m_LastViewId = -100;
        private int m_FaultId;

        //2
        public override string GetInfo()
        {
            return "故障确认";
        }

        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2)
            {
                return;
            }
            m_LastViewId = (int)nParaC;
            m_FaultId = 0;
        }

        public override void Paint(Graphics g)
        {
            if (FaultReceive.MsgInf.UnFlagCurrentMsgList.Count == 0)
            {
                append_postCmd(CmdType.ChangePage, m_LastViewId, 0, 0);
            }
            else
            {
                DrawBaseImage(g);
                DrawOn(g);
                GetButton();
            }
        }

        private void InitData()
        {

            m_RectsList = Coordinate.RectangleFLists(ViewIDName.FaultEnsure);
            m_RectsList.Add(new RectangleF(m_RectsList[6].X + m_RectsList[6].Width + 50, m_RectsList[6].Y, 40, 30));

        }

        private void GetButton()
        {
            TitleScreen.BtnStr12[0] = m_FaultId == 0 ? "" : "←";
            TitleScreen.BtnStr12[1] = FaultReceive.MsgInf.UnFlagCurrentMsgList.Count == 0 ? "" : "→";
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress)
            {
                return;
            }
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 16: //2
                    if (FaultReceive.MsgInf.UnFlagCurrentMsgList.Count >= m_FaultId)
                    {
                        FaultReceive.MsgInf.MsgBeRead(FaultReceive.MsgInf.UnFlagCurrentMsgList[m_FaultId].MsgLogicID);
                    }
                    ButtonsScreen.BtnState.Press();
                    break;
                case 10: //C键返回上一视图
                    append_postCmd(CmdType.ChangePage, m_LastViewId, 0, 0);
                    break;

            }
        }

        private void DrawOn(Graphics g)
        {
            if (FaultReceive.MsgInf.UnFlagCurrentMsgList.Count <= m_FaultId)
            {
                m_FaultId = FaultReceive.MsgInf.UnFlagCurrentMsgList.Count - 1;
            }
            //车辆号
            g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[m_FaultId].TrainID.ToString(),
                FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            //故障数
            g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList.Count.ToString(),
                FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));

            //当前故障编号
            g.DrawString((m_FaultId + 1).ToString(), FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));

            //设备
            g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[m_FaultId].SubSysName,
                FontsItems.Font18, SolidBrushsItems.WhiteBrush,
                m_RectsList[4], FontsItems.TheAlignment(FontRelated.靠左上));
            //描述
            g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[m_FaultId].MsgContent,
                FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                m_RectsList[5], FontsItems.TheAlignment(FontRelated.靠左上));
            //操作者指南
            g.DrawString(FaultReceive.MsgInf.UnFlagCurrentMsgList[m_FaultId].FaultSolutionStr,
                FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                m_RectsList[6], FontsItems.TheAlignment(FontRelated.靠左上));
            //指向箭头
            //e.DrawString("→", FontsItems.Font36, SolidBrushsItems.GreenBrush_1, _rectsList[_rectsList.Count-1], FontsItems.TheAlignment(Font_Related.靠右));
        }

        private void DrawBaseImage(Graphics g)
        {
            g.DrawImage(ImagesResouce.faultEnsure, m_RectsList[0]);
        }
    }
}
