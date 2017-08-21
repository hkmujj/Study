using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using Coordinate = Engine.TCMS.HXD3D.底层共用.Coordinate;

namespace Engine.TCMS.HXD3D.Fault.Ensure.Detail
{
    internal class EnsureItemView : FaultEnsureView
    {

        public EnsureItemView(FaultEnsure targetObject) : base(targetObject, EnsureType.EnsureItem)
        {
        }

        private List<Label> m_ConstInfos;

        private HXD3DBlueButton m_OkButton;

        private HXD3DBlueButton m_ReturnButton;

        private List<Label> m_FaultInfos;

        /// <summary>初始化</summary>
        public override void Init()
        {
            

            InitalizeConstInfos();

            InitalizeFaultInfos();

            InitalizeBtns();
        }

        public override void NavigateToThis(FaultMsgHXD3D[] msgs)
        {
            Tag = msgs;
            //UpdateFault(msgs?.FirstOrDefault());
            
            if (msgs !=null)
            {
                UpdateFault(msgs.FirstOrDefault());
            }
        }

        public void UpdateFault(FaultMsgHXD3D fault)
        {
            Tag = fault;

            if (fault !=null)
            {
                m_FaultInfos[0].Text = fault.TrainID.ToString("0000");
                m_FaultInfos[1].Text = fault.MsgID;
                m_FaultInfos[2].Text = fault.MsgContent;

                ChangeBtnsState(fault.MsgOverTime == DateTime.MinValue);
            }
            else
            {
                m_FaultInfos[0].Text = "";
                m_FaultInfos[1].Text = "";
                m_FaultInfos[2].Text = "";
                ChangeBtnsState(false);
            }
        }

        private void ChangeBtnsState(bool needOk)
        {
            m_OkButton.Visible = needOk;
            m_ReturnButton.Visible = !needOk;
        }

        private void InitalizeConstInfos()
        {
            var ts = new string[] {"车辆","故障编号"};

            var x = 20;
            var y = 160;
            var w = 100;
            var h = 30;
            var inter = 6;

            m_ConstInfos =
                ts.Select((s, i) => new Label()
                {
                    OutLineRectangle = new Rectangle(x, y + i*(inter + h), w, h),
                    Text = s,
                    Brush = SolidBrushsItems.YellowBrush1,
                    Font = new Font("宋体", 14),
                    Format = { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near}
                }).ToList();
        }

        private void InitalizeFaultInfos()
        {
            var ts = new[] { "车辆", "故障编号" };

            var x = 130;
            var y = 160;
            var w = 100;
            var h = 30;
            var inter = 6;

            m_FaultInfos =
                ts.Select((s, i) => new Label()
                {
                    OutLineRectangle = new Rectangle(x, y + i * (inter + h), w, h),
                    Text = s,
                    Brush = SolidBrushsItems.WhiteBrush,
                    Font = new Font("宋体", 14),
                    Format = { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near }
                }).ToList();

            var x2 = 20;
            var y2 = 240;
            var w2 = 600;
            var h2 = 250;
            m_FaultInfos.Add(new Label()
            {
                OutLineRectangle = new Rectangle(x2, y2, w2, h2),
                Text = "内容",
                Brush = SolidBrushsItems.YellowBrush1,
                Font = new Font("宋体", 20),
                Format = { LineAlignment = StringAlignment.Near, Alignment = StringAlignment.Near }
            });
        }

        private void InitalizeBtns()
        {
            
            var y = 280;
            var w = 100;
            var x = 800 - w - 10;
            var h = 30;

            m_OkButton = new HXD3DBlueButton() {OutLineRectangle = new Rectangle(x, y, w, h), Text = "确认", ButtonClickEvent =
                (sender, args) =>
                {
                    OnEnsureItem(Tag as FaultMsgHXD3D);
                } };

            var y2 = 500;
            m_ReturnButton = new HXD3DBlueButton()
            {
                OutLineRectangle = new Rectangle(x, y2, w, h),
                Text = "返回",
                ButtonClickEvent = (sender, args) =>
                {
                    OnReture();
                }
            };
        }



        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));
            m_FaultInfos .ForEach(e => e.OnDraw(g));
            m_FaultInfos .ForEach(e => e.OnDraw(g));
            m_OkButton.OnDraw(g);
            m_ReturnButton.OnDraw(g);
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            m_OkButton.OnMouseDown(point);

            m_ReturnButton.OnMouseDown(point);

            return true;
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            m_OkButton.OnMouseUp(point);

            m_ReturnButton.OnMouseUp(point);

            return true;
        }
    }
}
