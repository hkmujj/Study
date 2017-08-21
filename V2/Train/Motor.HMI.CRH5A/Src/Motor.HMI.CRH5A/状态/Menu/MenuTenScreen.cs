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
            NameArr = new[]{ "��ˮ��100%\nˮλ", "��ˮ��75%\nˮλ", "��ˮ��50%\nˮλ",
                                      "��ˮ��25%\nˮλ", "��ˮ��5%\nˮλ",
                                      "",
                                      "��ˮ��100%\nˮλ", "��ˮ��75%\nˮλ", "��ˮ��50%\nˮλ",
                                      "��ˮ��25%\nˮλ" };
        }
        //2
        public override string GetInfo()
        {
            return "�˵�10��ͼ";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "10", "", "", "����", "", "", "", "", "", Button10 };
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