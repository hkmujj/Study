using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Staus.Menu;

namespace Motor.HMI.CRH5G.Staus
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuOneScreen : MenuScreenBase
    {
        public MenuOneScreen()
        {
            TextFileIndex = "MenuOneScreen";
            NameArr = new[]{ "使能车辆", "受电弓", "DJ/DJ1",
                                      "牵引变压器", "牵引变流器", "辅助变流器",
                                      "高压接触器1", "高压接触器2", "高压接触器3",
                                      "主压缩机", "充电机" };
        }

        //2
        public override string GetInfo()
        {
            return "菜单1视图";
        }


        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "1", "2", "3", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0,10).ToList());
        }

        protected override void ResponseUser()
        {
            //
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 3, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://其他
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;  
                        
            }
        }
    }
}