using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuSixScreen : MenuScreenBase
    {
        public MenuSixScreen()
        {
            TextFileIndex = "MenuSixScreen";
            NameArr = new[]{ "网关1", "网关2", "MPU/REP/CLT\n牵引1",
                                      "MPU/REP/CLT\n牵引2", "MPU/REP舒\n适1", "MPU/REP舒\n适性2",
                                      "司机室I/01", "司机室I/02", "车辆I/01",
                                      "车辆I/02", "生命信号" };
        }

        //2
        public override string GetInfo()
        {
            return "菜单6视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "4", "5", "6", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 5, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 6, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://其他
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}