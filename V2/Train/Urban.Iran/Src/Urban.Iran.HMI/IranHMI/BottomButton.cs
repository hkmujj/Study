using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BottomButton : HMIBase
    {
        private List<GDIButton> m_MenuButtons;

        public static BottomButton Instance { private set; get; }

        // ReSharper disable once InconsistentNaming
        public Action NavigateRight;

        // ReSharper disable once InconsistentNaming
        public Action NavigateLeft;

        protected override bool Initalize()
        {
            Instance = this;

            m_MenuButtons = new List<GDIButton>
            {
                ButtonFactory.CreateShadowButton(new Rectangle(2, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String10"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.Doors);
                        ;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(102, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String11"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.BrakeAir);
                        ;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(202, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String12"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.PAB);;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(302, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String13"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.HVACPage1);;
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(402, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String14"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) =>
                        {
                            if (!ViewIndexHelper.IsInEventListView(GlobleParam.Instance.CurrentIranViewIndex))
                            {
                                ChangedPage(IranViewIndex.PIS);;
                            }
                            else
                            {
                                OnNavigateLeft();
                            }
                        };
                        btn.RefreshAction = o =>
                        {
                            if (ViewIndexHelper.IsInEventListView(GlobleParam.Instance.CurrentIranViewIndex))
                            {
                                btn.BackImage = GdiCommon.BtnLeftArrawImage;
                                btn.Text = null;
                            }
                            else
                            {
                                btn.BackImage = GdiCommon.BtnBkBitmap;
                                btn.Text = GlobleParam.ResManager.GetString("String14");
                            }
                        };
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(502, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String15"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) =>
                        {
                            if (!ViewIndexHelper.IsInEventListView(GlobleParam.Instance.CurrentIranViewIndex))
                            {
                                ChangedPage(IranViewIndex.PublicAnnoucement);;
                            }
                            else
                            {
                                OnNavigateRight();
                            }
                        };
                        btn.RefreshAction = o =>
                        {
                            if (ViewIndexHelper.IsInEventListView(GlobleParam.Instance.CurrentIranViewIndex))
                            {
                                btn.BackImage = GdiCommon.BtnRightArrawImage;
                                btn.Text = null;
                            }
                            else
                            {
                                btn.BackImage = GdiCommon.BtnBkBitmap;
                                btn.Text = GlobleParam.ResManager.GetString("String15");
                            }
                        };
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(602, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String16"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.Operation);
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(702, 536, 97, 62),
                    GlobleParam.ResManager.GetString("String1"), btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.MainMenu);
                    })
            };

            return true;
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                NavigateRight = null;
                NavigateLeft = null;
            }
        }

        public override string GetInfo()
        {
            return "底部菜单按钮";
        }

        public override void paint(Graphics g)
        {
            m_MenuButtons.ForEach(e => e.OnPaint(g));
        }

        public override bool mouseDown(Point point)
        {
            return m_MenuButtons.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_MenuButtons.Any(a => a.OnMouseUp(point));
        }

        protected virtual void OnNavigateRight()
        {
            var handler = NavigateRight;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnNavigateLeft()
        {
            var handler = NavigateLeft;
            if (handler != null)
            {
                handler();
            }
        }
    }
}