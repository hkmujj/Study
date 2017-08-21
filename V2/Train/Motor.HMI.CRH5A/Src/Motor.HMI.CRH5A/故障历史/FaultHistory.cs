using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Model;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5A.Resource.Images;
using Motor.HMI.CRH5A.底层共用;
using Motor.HMI.CRH5A.底层共用.消息;

namespace Motor.HMI.CRH5A.故障历史
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class FaultHistory : CRH5ABase
    {
        private FalutRepertory m_FalutRepertory;

        private List<RectangleF> m_RectsList;

        private string m_SortName = string.Empty;

        private readonly string[] m_SortNames =
        {
            "按时间排序", "按车号排序", "按设备排序"
        };

        public override string GetInfo()
        {
            return "故障历史视图";
        }

        //6
        public override bool Initalize()
        {
            return Init();
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA != 2)
            {
                return;
            }
            m_SortName = m_SortNames[0];

            m_FalutRepertory.UpdateHistory(FaultReceive.MsgInf.AllMsgsList);

        }

        public override void Paint(Graphics g)
        {

            DrawOn(g);

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress)
            {
                return;
            }

            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 10: //返回菜单
                    append_postCmd(CmdType.ChangePage, 33, 0, 0);
                    break;
                case 11: //上
                    m_FalutRepertory.Goto(Direction.Up);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 12: //下
                    m_FalutRepertory.Goto(Direction.Down);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 15: //左移键
                    m_FalutRepertory.Goto(Direction.Left);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16: //右移键
                    m_FalutRepertory.Goto(Direction.Right);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18: //时间排序
                    m_FalutRepertory.Sort(FalutSortType.Time);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 19: //车号排序
                    m_FalutRepertory.Sort(FalutSortType.CarNo);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 20: //设备排序
                    m_FalutRepertory.Sort(FalutSortType.Dev);
                    ButtonsScreen.BtnState.Press();
                    break;
            }
        }

        private void DrawOn(Graphics g)
        {
            g.DrawImage(ImagesResouce.故障, m_RectsList[0]);
            //排序方式
            g.DrawString(m_SortName, FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[1],
                FontsItems.TheAlignment(FontRelated.居中));

            //故障列表
            if (m_FalutRepertory.Any())
            {
                //游标
                if (m_FalutRepertory.CurrentSelectedMsgIndex >= 0)
                {
                    g.DrawString("->", FontsItems.Font12, SolidBrushsItems.WhiteBrush,
                        m_RectsList[9 + m_FalutRepertory.CurrentSelectedMsgIndex*6],
                        FontsItems.TheAlignment(FontRelated.靠右));
                }

                var idx = 0;
                foreach (var msg in m_FalutRepertory.CurrentPage)
                {
                    //序号
                    g.DrawString((m_FalutRepertory.CurrentPageIndex*10 + idx + 1).ToString(), FontsItems.Font12,
                        SolidBrushsItems.WhiteBrush,
                        m_RectsList[10 + idx*6], FontsItems.TheAlignment(FontRelated.居中));

                    g.DrawString(
                        msg.MsgReceiveTime.ToString(
                            "yyyy-MM-dd hh:mm:ss"),
                        FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[11 + idx*6],
                        FontsItems.TheAlignment(FontRelated.靠左));
                    //车号
                    g.DrawString(msg.TrainID.ToString(),
                        FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[12 + idx*6],
                        FontsItems.TheAlignment(FontRelated.靠左));
                    //设备
                    g.DrawString(msg.SubSysName,
                        FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[13 + idx*6],
                        FontsItems.TheAlignment(FontRelated.靠左));
                    //故障描述
                    g.DrawString(msg.MsgContent,
                        FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[14 + idx*6],
                        FontsItems.TheAlignment(FontRelated.靠左));

                    ++idx;
                }

                //车辆
                var selectedMsg = m_FalutRepertory.CurrentSelectedMsg;

                g.DrawString(Content(selectedMsg.TrainID.ToString()),
                    FontsItems.Font12,
                    SolidBrushsItems.WhiteBrush, m_RectsList[4], FontsItems.TheAlignment(FontRelated.靠左));
                //设备名
                g.DrawString(Content(selectedMsg.SubSysName), FontsItems.Font12,
                    SolidBrushsItems.WhiteBrush, m_RectsList[5], FontsItems.TheAlignment(FontRelated.靠左));
                //故障时
                g.DrawString(
                    Content(
                        selectedMsg.MsgReceiveTime.ToString("yyyy-MM-dd hh:mm:ss")),
                    FontsItems.Font12, SolidBrushsItems.WhiteBrush, m_RectsList[6],
                    FontsItems.TheAlignment(FontRelated.靠左));
                //故障描
                g.DrawString(Content(selectedMsg.MsgContent), FontsItems.Font12,
                    SolidBrushsItems.WhiteBrush, m_RectsList[7], FontsItems.TheAlignment(FontRelated.靠左上));
                //操作者指南
                g.DrawString(Content(selectedMsg.FaultSolutionStr),
                    FontsItems.Font12,
                    SolidBrushsItems.WhiteBrush, m_RectsList[8], FontsItems.TheAlignment(FontRelated.靠左上));
            }

            //当前页、总页码
            g.DrawString((m_FalutRepertory.CurrentPageIndex + 1).ToString(), FontsItems.Font14,
                SolidBrushsItems.WhiteBrush, m_RectsList[2],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(m_FalutRepertory.TotalPage.ToString(), FontsItems.Font14, SolidBrushsItems.WhiteBrush,
                m_RectsList[3],
                FontsItems.TheAlignment(FontRelated.居中));
        }

        private string Content(string str)
        {
            return "< " + str + " >";
        }

        private bool Init()
        {
            m_FalutRepertory = new FalutRepertory(10);

             

            m_RectsList = Coordinate.RectangleFLists(ViewIDName.FaultHistory);

            return true;
        }
    }
}
