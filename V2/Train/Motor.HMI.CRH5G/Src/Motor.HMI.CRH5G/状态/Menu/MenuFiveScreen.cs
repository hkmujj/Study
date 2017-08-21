using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuFiveScreen : MenuScreenBase
    {
        public MenuFiveScreen()
        {
            TextFileIndex = "MenuFiveScreen";
            NameArr = new[]{ "�����", "13�ż���/12\n�Ž�������", "1λ��",
                                      "2λ��", "24�ż���/34\n�Ž�������", "3λ��",
                                      "4λ��" };
        }
        //2
        public override string GetInfo()
        {
            return "�˵�5��ͼ";
        }


        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "4", "5", "6", "����", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0,10).ToList());
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
                case 18://����
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}