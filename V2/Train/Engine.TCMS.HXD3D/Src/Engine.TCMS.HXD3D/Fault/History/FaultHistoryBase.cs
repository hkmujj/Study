using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.Fault.Common;
using Engine.TCMS.HXD3D.Fault.Ensure;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.Fault.History
{
    public class FaultHistoryBase : HXD3BaseClass
    {
        public FaultMsgHXD3D SelectedMsg { get; private set; }

        private string[] ControlBtnTexts { get { return new[] { "排序", "前一页", "后一页", }; } }

        private Lazy<FaultHistoryItemsView> m_FaultHistoryItemsView;

        private int m_CurrentPageIndex;

        private const int ItemCountPerPage = 15;

        private Lazy<List<HXD3DBlueButton>> m_ControlBtns;

        protected Predicate<FaultMsgHXD3D> Filter { get; set; }

        public override void paint(Graphics g)
        {
            m_FaultHistoryItemsView.Value.OnPaint(g);

            m_ControlBtns.Value.ForEach(e => e.OnPaint(g));
        }

        public override string GetInfo()
        {
            return "过滤";
        }

        protected override void Initalize()
        {
            InitalizeGridView();

            InitalizeControlBtns();
        }

        /// <summary>设置动态数据</summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (ParaADefine.SwitchFromOhter == nParaA)
            {
                m_CurrentPageIndex = 0;
                SelectedMsg = null;
            }
        }

        private void InitalizeControlBtns()
        {
            m_ControlBtns = new Lazy<List<HXD3DBlueButton>>(() =>
            {
                var x = 545;
                var y = 525;
                var w = 80;
                var h = 30;
                var inter = 6;

                var btns = ControlBtnTexts.Select((s, i) => new HXD3DBlueButton()
                {
                    OutLineRectangle = new Rectangle(x + i * (w + inter), y, w, h),
                    Text = s,
                }).ToList();

                btns[1].RefreshAction = o =>
                {
                    var btn = (HXD3DBlueButton)o;
                    btn.Visible = m_CurrentPageIndex > 0;
                };
                btns[1].ButtonClickEvent += (sender, args) => { --m_CurrentPageIndex; };

                btns[2].RefreshAction = o =>
                {
                    var btn = (HXD3DBlueButton)o;
                    btn.Visible = m_CurrentPageIndex * ItemCountPerPage + ItemCountPerPage < FaultReceive.MsgInf.CurrentMsgList.Count(c => Filter(c));
                };
                btns[2].ButtonClickEvent += (sender, args) => { ++m_CurrentPageIndex; };

                return btns;
            });
        }

        private void InitalizeGridView()
        {
            m_FaultHistoryItemsView = new Lazy<FaultHistoryItemsView>(() =>
            {
                var faultHistoryItemsView = new FaultHistoryItemsView();

                faultHistoryItemsView.Init();

                faultHistoryItemsView.RefreshAction = o =>
                {
                    faultHistoryItemsView.UpdateFaults(
                        FaultReceive.MsgInf.CurrentMsgList.Where(w => Filter(w)).Skip(m_CurrentPageIndex * ItemCountPerPage)
                            .Take(ItemCountPerPage));
                };

                faultHistoryItemsView.ItemClicked += (sender, args) =>
                {
                    SelectedMsg = args.Arg;
                    
                    append_postCmd(CmdType.ChangePage, 101, 0, 0f);
                    FaultEnsure.UpdateToEnsure(SelectedMsg);
                    FaultEnsure.NavigateTo(EnsureType.EnsureItem);
                };

                return faultHistoryItemsView;
            });
        }

        public override bool mouseDown(Point point)
        {
            m_FaultHistoryItemsView.Value.OnMouseDown(point);

            m_ControlBtns.Value.ForEach(e => e.OnMouseDown(point));

            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_FaultHistoryItemsView.Value.OnMouseUp(point);

            m_ControlBtns.Value.ForEach(e => e.OnMouseUp(point));

            return true;

        }

    }
}