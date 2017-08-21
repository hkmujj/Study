using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD1.DeepDomestic
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackScreen : baseClass
    {

        private float oldPage = 1;
        private int currentPage = 103;

        public override string GetInfo()
        {
            return "黑屏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            currentPage = nParaB;
            if (nParaA == 2)
                if (nParaB == 103)
                {
                    oldPage = nParaC;
                    CurrentFaultA.SortedFaultList.Clear();
                    CurrentFaultB.SortedFaultList.Clear();
                    AllFalutA.SortedFaultList.Clear();
                    AllFalutB.SortedFaultList.Clear();
                    HisteryFault.CurrentFault.Clear();
                    foreach (var v in Alert.AlertModelList)
                    {
                        v.SelectIndex = 0;
                        v.IsSelected = false;
                    }
                    Alert.SelectIndex = 0;
                }
                else
                    oldPage = nParaB;
        }

        public override void paint(Graphics dcGs)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);

            if (BoolList[UIObj.InBoolList[0]])
            {
                if (currentPage == 103)
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }


            append_postCmd(CmdType.SetFloatValue, 250, 0, TrainParameter.trainwight);
            append_postCmd(CmdType.SetFloatValue, 251, 0, TrainParameter.trainType);
            append_postCmd(CmdType.SetFloatValue, 253, 0, TrainParameter.axisWeight);
            append_postCmd(CmdType.SetFloatValue, 254, 0, TrainParameter.speed);
            //append_postCmd(CmdType.SetBoolValue, 2407, 1, 0);
        }
    }
}
