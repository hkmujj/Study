using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.MainMenu
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MainMenu : HMIBase
    {
        private List<GDIButton> m_BtnItems;

        private List<MainMenuItem> m_ShowInNomarlItems;
        private List<MainMenuItem> m_NotShowInNoActiveItems;

        public override string GetInfo()
        {
            return "主菜单";
        }

        protected override bool Initalize()
        {
            m_ShowInNomarlItems = new List<MainMenuItem>()
            {
                MainMenuItem.EventHistory,
                MainMenuItem.Interlocks,
                MainMenuItem.BypassOverview,
                MainMenuItem.Settings,
                MainMenuItem.EventOverview,
                MainMenuItem.LoadWeight,
                MainMenuItem.CommOverview,
                MainMenuItem.Fire
            };
            m_NotShowInNoActiveItems = new List<MainMenuItem>
            {
                MainMenuItem.WSPTest,
                MainMenuItem.MaintTest,
                MainMenuItem.PISTest
            };
            var btnRectangles = InitalizeBtnRegions();

            m_BtnItems = MainMenuItemExtension.AllMainMenuItems.Select(s =>
            {
                var location = s.GetDisplayLocation();
                var description = s.GetDescription();

                return ButtonFactory.CreateButton(btnRectangles[location.Horizontal, location.Vertical],
                    GlobleParam.ResManagerText.GetString(description),
                    btn =>
                    {
                        btn.Tag = new Tuple<MainMenuItem, IranViewIndex>(s, s.GetGotoView().ViewIndex);
                        btn.RefreshAction = o =>
                        {
                            var b = (GDIButton)o;
                            b.Visible = true;
                            if (GlobleParam.Instance.WorkModel == HMIWorkModel.Normal)
                            {
                                b.Visible = m_ShowInNomarlItems.Contains(((Tuple<MainMenuItem, IranViewIndex>)(b.Tag)).Item1);
                            }
                            if (GlobleParam.Instance.WorkModel == HMIWorkModel.NoActoveDrive)
                            {
                                b.Visible =
                                    !m_NotShowInNoActiveItems.Contains(
                                        ((Tuple<MainMenuItem, IranViewIndex>)(b.Tag)).Item1);
                            }
                        };
                        btn.ButtonUpEvent +=
                            (sender, args) =>
                                ChangedPage(((Tuple<MainMenuItem, IranViewIndex>)((GDIButton)sender).Tag).Item2);
                    });
            }).ToList();

            return true;
        }

        private Rectangle[,] InitalizeBtnRegions()
        {
            var width = 97;
            var height = 62;
            var y = 123;
            var x = 95;
            var btnRectangles = new Rectangle[4, 4];
            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    btnRectangles[j, i] = new Rectangle(x, y, width, height);
                    x += 172;
                }
                y += 88;
                x = 95;
            }

            return btnRectangles;
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex)1;

            m_BtnItems.ForEach(e => e.OnPaint(g));
        }

        public override bool mouseDown(Point point)
        {
            return m_BtnItems.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_BtnItems.Any(a => a.OnMouseUp(point));
        }
    }
}