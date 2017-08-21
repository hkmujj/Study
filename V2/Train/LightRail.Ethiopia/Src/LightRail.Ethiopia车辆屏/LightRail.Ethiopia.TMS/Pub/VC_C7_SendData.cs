using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.HVAC;
using LightRail.Ethiopia.TMS.Main;

namespace LightRail.Ethiopia.TMS.Pub
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C7_SendData : baseClass
    {
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-****";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            V101_Main_ControlButton._btns.ForEach(a =>
            {
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, a.IsReplication ? 0 : 1, 0);
            });
            V101_Main_ControlButton._btns_Mutual.ForEach(a =>
            {
                if (a.ID == 3)
                {
                    append_postCmd(CmdType.SetBoolValue, 48, a.IsReplication ? 0 : 1, 0);
                }
                else
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, a.IsReplication ? 0 : 1, 0);

            });
            V101_Main_ControlButton._btns_Switch.ForEach(a =>
            {
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + a.ID, a.IsReplication ? 0 : 1, 0);
            });
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + V101_Main_ControlButton._btn_SkipStation.ID, V101_Main_ControlButton._btn_SkipStation.IsReplication ? 0 : 1, 0);


            V702_HVAC_Paremset._btns.ForEach(a =>
            {
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + a.ID, a.IsReplication ? 0 : 1, 0);
            });

            base.paint(dcGs);
        }
    }
}
