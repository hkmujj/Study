using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Staus.Menu;

namespace Motor.HMI.CRH5G.Staus
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuFourScreen : MenuScreenBase
    {
        public MenuFourScreen()
        {
            TextFileIndex = "MenuFourScreen";

            NameArr = new[]{ "�ÿͱ���1", "�ÿͱ���2", "����ұ���",
                                      "1���ƶ�״̬", "1�����", "2���ƶ�״̬",
                                      "2�����", "3���ƶ�״̬", "3�����",
                                      "4���ƶ�״̬", "4�����" };
        }
        //2
        public override string GetInfo()
        {
            return "�˵�4��ͼ";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "4", "5", "6", "����", "", "", "", "", "", Button10 };
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
                case 18://����
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}