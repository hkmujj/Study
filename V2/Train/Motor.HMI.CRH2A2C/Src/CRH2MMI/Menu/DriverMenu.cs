using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Bottom;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Resource;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Menu
{
    internal class DriverMenu : MenuBase
    {
        private static readonly Size DefaultBtnSize = new Size(150, 50);

        private List<CRH2Button> m_Btns;

        private SuggestiveInformation m_SuggestiveInformation;

        private SuggestiveInformation SuggestiveInformation
        {
            get {
                return m_SuggestiveInformation ??
                       (m_SuggestiveInformation = m_CRH2Menu.GetSameProjectObjcect<SuggestiveInformation>());
            }
        }


        public DriverMenu(CRH2menu crh2Menu)
            : base(crh2Menu)
        {
            
        }

        public override void Init()
        {
            base.Init();

            m_Btns = MenuResource.Instance.DriverMenuContents.Select(s => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(175 + s.Xoffset * DefaultBtnSize.Width, 125 + s.Yoffset * DefaultBtnSize.Height, DefaultBtnSize.Width, DefaultBtnSize.Height),
                ButtonDownEvent = (sender, args) =>
                {
                    SuggestiveInformation.ClearInformation();
                    var view = (ViewConfig)s.Goto;
                    if (view == ViewConfig.FaultView)
                    {
                        SetAppraiseFaultViewValue(1);
                    }

                },
                ButtonUpEvent = (sender, args) =>
                {
                    var view = (ViewConfig) s.Goto;
                    if (view == ViewConfig.FaultView)
                    {
                        SetAppraiseFaultViewValue(0);
                    }
                    if (ChangePagaManager.Instance.CanChangeTo(view))
                    {
                        m_CRH2Menu.append_postCmd(CmdType.ChangePage, s.Goto, 0, 0);
                    }
                    else
                    {
                        SuggestiveInformation.UpdateInformation(new InformationModel(ChangePagaManager.Instance.GetLastError()) { InformationType = InformationType.Error, Location = InformationLocation.Up });
                    }
                },
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                BackImage = GlobalResource.Instance.BtnUpImage,
                Text = s.Name,
            }).ToList();
        }

        private void SetAppraiseFaultViewValue(int value)
        {
            var idx = m_CRH2Menu.ScreenId == ScreenId.CRH2 ? OubKeys.给评价的1屏故障一揽按键状态 : OubKeys.给评价的2屏故障一揽按键状态;
            m_CRH2Menu.append_postCmd(CmdType.SetBoolValue, m_CRH2Menu.IndexDescriptionConfig.OutBoolDescriptionDictionary[idx], value, 0);
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            m_Btns.ForEach(e => e.OnDraw(g));
        }

        public override void SwitchFromOthers()
        {
            SuggestiveInformation.UpdateInformation(new InformationModel("请选择项目。") {InformationType = InformationType.Info});
        }

        public override bool OnMouseDown(Point point)
        {
            return m_Btns.Any(a => a.OnMouseDown(point));
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            return m_Btns.Any(a => a.OnMouseUp(point));
        }
    }
}
