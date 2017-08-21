using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.Fault.Common;
using MMI.Facility.Interface;

namespace Engine.TCMS.HXD3D.Fault.Ensure.Detail
{
    class EnsureAllView : FaultEnsureView
    {
        public Action EnsureAllAction;

        private FautItemsView m_FautItemsView;

        public EnsureAllView(FaultEnsure targetObject) : base(targetObject, EnsureType.EnsureAll)
        {
            Filter = d => d.MsgOverTime == DateTime.MinValue;
        }

        public FaultMsgHXD3D SelectedMsg { get; private set; }

        private string[] ControlBtnTexts
        {
            get { return new[] {"全确认", "排序", "前一页", "后一页",}; }
        }

        private int m_CurrentPageIndex;

        private const int ItemCountPerPage = 15;

        private Lazy<List<HXD3DBlueButton>> m_ControlBtns;

        protected Predicate<FaultMsgHXD3D> Filter { get; set; }

        public override void OnDraw(Graphics g)
        {
            m_FautItemsView.OnDraw(g);

            m_ControlBtns.Value.ForEach(e => e.OnPaint(g));
        }

        public override void Init()
        {
            m_FautItemsView = new FaultEnsureItemsView();
            m_FautItemsView.ItemClicked += (sender, args) => OnEnsureItem(args.Arg);
            m_FautItemsView.Init();

            InitalizeControlBtns();
        }

        public override void NavigateToThis(FaultMsgHXD3D[] msgs)
        {
            m_CurrentPageIndex = 0;
            SelectedMsg = null;
            UpdateFault();
        }

        private void UpdateFault()
        {
            m_FautItemsView.UpdateFaults(
                FaultReceive.MsgInf.CurrentMsgList.Where(w => Filter(w)).Skip(m_CurrentPageIndex*ItemCountPerPage).Take(ItemCountPerPage));
        }

        private void InitalizeControlBtns()
        {
            m_ControlBtns = new Lazy<List<HXD3DBlueButton>>(() =>
            {
                var x = 459;
                var y = 525;
                var w = 80;
                var h = 30;
                var inter = 6;

                var btns = ControlBtnTexts.Select((s, i) => new HXD3DBlueButton()
                {
                    OutLineRectangle = new Rectangle(x + i*(w + inter), y, w, h),
                    Text = s,
                }).ToList();

                btns[0].ButtonClickEvent += (sender, args) =>
                {
                    //EnsureAllAction?.Invoke();
                    if (EnsureAllAction !=null)
                    {
                        EnsureAllAction.Invoke();
                    }
                    OnReture();
                };

                btns[1].Visible = false;

                btns[2].RefreshAction = o =>
                {
                    var btn = (HXD3DBlueButton) o;
                    btn.Visible = m_CurrentPageIndex > 0;
                };
                btns[2].ButtonClickEvent += (sender, args) =>
                {
                    --m_CurrentPageIndex;
                    UpdateFault();
                };

                btns[3].RefreshAction = o =>
                {
                    var btn = (HXD3DBlueButton) o;
                    btn.Visible = m_CurrentPageIndex*ItemCountPerPage + ItemCountPerPage <
                                  FaultReceive.MsgInf.CurrentMsgList.Count(c => Filter(c));
                };
                btns[3].ButtonClickEvent += (sender, args) =>
                {
                    ++m_CurrentPageIndex;
                    UpdateFault();
                };

                return btns;
            });
        }

        public override bool OnMouseDown(Point point)
        {
            m_FautItemsView.OnMouseDown(point);

            m_ControlBtns.Value.ForEach(e => e.OnMouseDown(point));

            return true;
        }

        public override bool OnMouseUp(Point point)
        {
            m_FautItemsView.OnMouseUp(point);

            m_ControlBtns.Value.ForEach(e => e.OnMouseUp(point));

            return true;

        }
    }
}