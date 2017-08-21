using System;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ClassOver : baseClass
    {
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                //清除分相类型数据
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);

                //>>>>>>>>>>>>>>>>>>结束课程，状态复位20150803 唐林<<<<<<<<<<<<<<<
                EngineSetting.IsSetFenX = false;
                EngineSetting.PartMutually = EngineSetting.PartMutuallyType.Open;
                EngineSetting.PointOut = "";
                MaintainTesting.DingLunMode_Cancel = 1;
                MaintainTesting.DingLunMode_Active = 0;
                MaintainTesting.KuNeiFenXiangTest_Over = 1;
                MaintainTesting.KuNeiFenXiangTest_Start = 0;
                MaintainTesting.LinYuMode_Cancel = 1;
                MaintainTesting.LinYuMode_Active = 0;
                ControlSeting.RunModel_Normal = 1;
                ControlSeting.RunModel_Urgency = 0;
                ControlSeting._rowid = 0;
                ControlSeting.DisplayDictionary.Clear();
                ControlSeting.DisplayValue = 3;
                V48_PressModeSet.IsCutOff = true;
                V49_Power.IsMode1 = true;
                V50_Blackout.IsMode1 = true;
                V53_LongDistanceCutOff.CurrentRow = 0;
                V53_LongDistanceCutOff.CutOffList = new Int32[16];
                Title.Current = -1;
                Title.OpenCloseCount = 0;
                Title.Rowid = 0;
                Title.ContentDictionary = new System.Collections.Generic.Dictionary<int, int>();
                Title.PassWordDictionary = new System.Collections.Generic.Dictionary<int, string>();
                Password.PointOut = "";
                Password.Index = 0;

                //>>>>>>>>>>>>>>>>>>结束课程，状态复位20150803 唐林<<<<<<<<<<<<<<<
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }
    }
}
