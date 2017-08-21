using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MenuTwoScreen : MenuScreenBase
    {
        public MenuTwoScreen()
        {
            TextFileIndex = "MenuTwoScreen";
            NameArr = new[]
                       {
                           "400V������", "��عضϿ���", "��ѹ�Ӵ���\nSAZ3",
                           "�����Ӵ���", "���¼��ϵͳ", "���м��ϵͳ",
                           "���ҿյ�", "˾���ҿյ�", "Ӧ��ͨ��",
                           "�г��˲�ɳ��", "�м���ɳ��"
                       };
        }

        //2
        public override string GetInfo()
        {
            return "�˵�2��ͼ";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "1", "2", "3", "����", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15: //1
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16: //2
                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17: //3
                    append_postCmd(CmdType.ChangePage, 3, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18: //����
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}