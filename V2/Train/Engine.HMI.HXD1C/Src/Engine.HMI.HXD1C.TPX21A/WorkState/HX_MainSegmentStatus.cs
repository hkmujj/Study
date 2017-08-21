using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A.WorkState
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class HX_MainSegmentStatus : WorkStateBase
    {

        public override string GetInfo()
        {
            return "����״̬";
        }

        protected override SortedList<int, Tuple<int, string>> InitalizeInfos()
        {
            BelongViewId = 9;
            return LoadTestInfo();
        }

        private SortedList<int, Tuple<int, string>> LoadTestInfo()
        {
            var info = new SortedList<int, Tuple<int, string>>();

            var file = Path.Combine(AppPaths.ConfigDirectory, "��������.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] str = cStr.Split(';');
                if (str.Length == 4 && str[0].Equals("4"))
                {
                    info.Add(info.Count, new Tuple<int, string>(Convert.ToInt32(str[2]), str[3]));
                }
            }

            return info;
        }

        protected override void SetCommonButtons()
        {
            //���ñ���
            HxCommon.HTitle.SetText("����״̬");

            HxCommon.ButtonText[0].SetText("");
            HxCommon.ButtonText[1].SetText("");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("ǣ��״̬");
            HxCommon.ButtonText[5].SetText("����״̬");
            HxCommon.ButtonText[6].SetText("�ܵ繭 ");
            HxCommon.ButtonText[7].SetText("");
            HxCommon.ButtonText[8].SetText("");
            HxCommon.ButtonText[9].SetText("������");

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

        }

        protected override void ChagePage(int i)
        {
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4://����4�л�������״̬��ͼ
                    append_postCmd(CmdType.ChangePage, 8, 0, 0);
                    break;
                case 5://����5�л�������״̬��ͼ
                    append_postCmd(CmdType.ChangePage, 9, 0, 0);
                    break;
                case 6://����7�л����ܵ繭״̬��ͼ
                    append_postCmd(CmdType.ChangePage, 10, 0, 0);
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9://����������
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
