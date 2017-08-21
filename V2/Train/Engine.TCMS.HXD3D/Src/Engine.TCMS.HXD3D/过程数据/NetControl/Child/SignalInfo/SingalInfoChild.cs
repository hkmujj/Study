using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.Config;
using Engine.TCMS.HXD3D.HXD3D;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.SignalInfo
{
    public class SingalInfoChild : CommonInnerControlBase
    {
        protected List<GDIRectText> TextItems { get; private set; }

        public SingalInfoChild(NetSiganlInfoView parent)
        {
            Parent = parent;
        }

        protected NetSiganlInfoView Parent { get; private set; }

        protected virtual void SetItemConfigs(IEnumerable<SingalInfoContentConfig> configs)
        {
            TextItems =
                configs.Select(
                    s =>
                        new GDIRectText()
                        {
                            DrawFont = new Font("Arial",10),
                            Text = s.Content,
                            OutLineRectangle = new Rectangle(s.X, s.Y, s.Width, s.Height),
                            OutLinePen = Pens.White,
                            NeedDarwOutline = true,
                            TextFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                },
                            RefreshAction = o =>
                            {
                                var t = (GDIRectText) o;
                                var flag = Parent.Parent.GetInBoolValue(s.InBoolName);
                                t.BkColor = flag ? SolidBrushsItems.GreenBrush1.Color : Color.Transparent;
                                t.TextColor = flag ? Color.Black : Color.White;
                            },
                            Tag = s,
                        }).ToList();
        }

        /// <summary>刷新并绘图, 会调用 Refresh</summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            base.OnPaint(g);

            TextItems.ForEach(e => e.OnPaint(g));
        }
    }
}