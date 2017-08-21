using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuTenScreen : MenuScreenBase
    {
        public MenuTenScreen()
        {
            TextFileIndex = "MenuTenScreen";
            NameArr = new[]{ "净水箱100%\n水位", "净水箱75%\n水位", "净水箱50%\n水位",
                                      "净水箱25%\n水位", "净水箱5%\n水位",
                                      "",
                                      "废水箱100%\n水位", "废水箱75%\n水位", "废水箱50%\n水位",
                                      "废水箱25%\n水位" };
        }
        //2
        public override string GetInfo()
        {
            return "菜单10视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "10", "", "", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 10, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}