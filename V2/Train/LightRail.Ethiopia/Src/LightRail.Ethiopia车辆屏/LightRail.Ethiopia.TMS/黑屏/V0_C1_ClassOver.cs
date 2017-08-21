using System.Drawing;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.HVAC;
using LightRail.Ethiopia.TMS.Main;
using LightRail.Ethiopia.TMS.PIS;
using LightRail.Ethiopia.TMS.Pub;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0_C1_ClassOver : baseClass
    {
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "课程结束";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)//复位
            {
                V101_Main_ControlButton._btns.ForEach(a =>
                {
                    a.IsReplication = true;
                    ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, 0, 0);
                });
                V101_Main_ControlButton._btns_Mutual.ForEach(a =>
                {
                    a.IsReplication = true;
                    ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, 0, 0);
                });
                V101_Main_ControlButton._btns_Switch.ForEach(a =>
                {
                    a.IsReplication = true;
                    ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, 0, 0);
                });
                V101_Main_ControlButton._btn_SkipStation.IsReplication = true;
                ((ButtonStyle)V101_Main_ControlButton._btn_SkipStation.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + V101_Main_ControlButton._btn_SkipStation.ID, 0, 0);


                V702_HVAC_Paremset._btns.ForEach(a =>
                {
                    a.IsReplication = true;
                    ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

                    if (a.ID == 3)
                    {
                        a.IsReplication = false;
                        ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
                    }
                });
                V702_HVAC_Paremset._currentStateID = 67;
                V703_HVAC_Test._btns.ForEach(a =>
                {
                    a.IsReplication = true;
                    ((ButtonStyle)a.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                });

                V6_PIS_MianView._currentUpDown = V6_PIS_MianView.UpDown.UP;
                V6_PIS_MianView.CurrentTcmsPis = V6_PIS_MianView.TcmsPis.AUTO;

                VC_C0_Title.IsShowHelpView = false;
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }
    }
}
