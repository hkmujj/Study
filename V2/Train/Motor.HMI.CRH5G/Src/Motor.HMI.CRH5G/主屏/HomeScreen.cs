using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH5G.底层共用;

namespace Motor.HMI.CRH5G.主屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class HomeScreen : CRH5GBase
    {
        private ReadOnlyCollection<RectangleF> m_RectsList;

        private List<HomeScreenItem> m_HomeScreenItemCollection;
        private HomeScreenItem m_SelectedItem;

        public override string GetInfo()
        {
            return "主屏视图";
        }

        public override bool Initalize()
        {
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.HomeScreen).AsReadOnly();


            if (ScreenIdentification == ScreenIdentification.ScreenTd)
            {
                m_HomeScreenItemCollection = new List<HomeScreenItem>()
                                             {
                                                 new HomeScreenItem() { Text = "语言/时间设置", ConfirmSelected = o => append_postCmd(CmdType.ChangePage,25,0,0)},
                                                 new HomeScreenItem() { Text = "系统设置", ConfirmSelected = o => append_postCmd(CmdType.ChangePage,37,0,0 )},
                                                 new HomeScreenItem() { Text = "状态", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 1, 0, 0) },
                                                 new HomeScreenItem() { Text = "仪器", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 21, 0, 0) },
                                                 new HomeScreenItem() { Text = "电子仪器", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 30, 0, 0) },
                                                 new HomeScreenItem() { Text = "命令", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 40, 0, 0) },
                                                 new HomeScreenItem() { Text = "制动试验", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 34, 0, 0) },
                                                 new HomeScreenItem() { Text = "报警历史", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 102, 0, 0) },
                                             };
            }
            else
            {
                m_HomeScreenItemCollection = new List<HomeScreenItem>()
                                             {
                                                 new HomeScreenItem() { Text = "语言/时间设置", },
                                                 new HomeScreenItem() { Text = "系统设置" },
                                                 new HomeScreenItem() { Text = "状态", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 1, 0, 0) },
                                                 new HomeScreenItem() { Text = "仪器", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 21, 0, 0) },
                                                 new HomeScreenItem() { Text = "电子仪器", ConfirmSelected = o => append_postCmd(CmdType.ChangePage, 30, 0, 0) },
                                             };
            }

            for (int i = 0; i < m_HomeScreenItemCollection.Count; i++)
            {
                var item = m_HomeScreenItemCollection[i];
                item.OutLineRectangle = Rectangle.Ceiling(m_RectsList[i]);
                item.TextFormat = FontsItems.TheAlignment(FontRelated.居中);
            }

            Select(m_HomeScreenItemCollection.First());

            return true;
        }


        private void Select(HomeScreenItem item)
        {
            if (m_SelectedItem != null)
            {
                m_SelectedItem.Selected = false;
            }
            item.Selected = true;
            m_SelectedItem = item;
        }

        private void SelectPrevious()
        {
            var idx = m_HomeScreenItemCollection.IndexOf(m_SelectedItem);
            if (idx > 0)
            {
                Select(m_HomeScreenItemCollection[idx - 1]);
            }
        }

        private void SelectNext()
        {
            var idx = m_HomeScreenItemCollection.IndexOf(m_SelectedItem);
            if (idx < m_HomeScreenItemCollection.Count - 1)
            {
                Select(m_HomeScreenItemCollection[idx + 1]);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (ScreenIdentification == ScreenIdentification.ScreenTs && nParaA == ParaADefine.SwitchFromOhter)
            {
                Select(m_HomeScreenItemCollection.First());
            }
        }

        public override void Paint(Graphics g)
        {
            if (ButtonsScreen.BtnState != null && !ButtonsScreen.BtnState.IsPress)
            {
                switch (ButtonsScreen.BtnState.BtnId)
                {
                    case 6 : //维护/控制键
                        append_postCmd(CmdType.ChangePage, 41, 0, 0);
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 11 : //上翻键
                        SelectPrevious();
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 12 : //下翻键
                        SelectNext();
                        ButtonsScreen.BtnState.Press();
                        break;
                    case 13 : //回车键
                        m_SelectedItem.OnConfirmSelected();
                        ButtonsScreen.BtnState.Press();
                        break;
                }
            }

            DrawOn(g);
        }

        private void DrawOn(Graphics g)
        {
            m_HomeScreenItemCollection.ForEach(e => e.OnDraw(g));
        }
    }
}
