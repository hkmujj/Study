using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.CommonControl;
using Engine.TCMS.HXD3D.Config;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using Excel.Interface;
using Coordinate = Engine.TCMS.HXD3D.底层共用.Coordinate;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child
{
    public class NetControlRootView : NetControlChildView
    {
        public NetControlRootView(ProcessNetControl parent) : base(parent, NetControlChildType.NetControlRoot)
        {
        }

        private List<HXD3DBlueButton> m_ChangePageBtns;

        private List<GDIRectText> m_GroupTitleInfo;

        private List<GDIRectText> m_ContentItems;

        private List<Rectangle> m_RectsList;


        /// <summary>初始化</summary>
        public override void Init()
        {
            var tmp = new List<RectangleF>();

            if (Coordinate.RectangleFLists(ViewIDName.ProcessNetControl, ref tmp))
            {
                m_RectsList = tmp.Select(Rectangle.Ceiling).ToList();


                InitalizeChangePageBtns();

                InitalizeContentItems();
            }

            InitGroupTitleInfo();
        }

        private void InitalizeContentItems()
        {
            var its = ExcelParser.Parser<NetControlItemConfig>(Path.Combine(Parent.AppPaths.ConfigDirectory)).ToList();

            m_ContentItems = m_RectsList.Skip(9).Take(18).Select((s, i) => new GDIRectText()
            {
                OutLineRectangle = Rectangle.Inflate(s, -1, 0),
                Text = its[i].Name,
                Tag = its[i],
                TextColor = Color.Black,
                NeedDarwOutline = true,
                TextFormat = FontsItems.TheAlignment(FontRelated.居中),
                RefreshAction = o =>
                {
                    var l = (GDIRectText) o;
                    var it = (NetControlItemConfig) l.Tag;
                    if (Parent.GetInBoolValue(it.RedIndex))
                    {
                        l.BKBrush = SolidBrushsItems.RedBrush1;
                        return;
                    }

                    if (Parent.GetInBoolValue(it.YellowIndex))
                    {
                        l.BKBrush = SolidBrushsItems.YellowBrush1;
                        return;
                    }

                    l.BKBrush = SolidBrushsItems.GreenBrush1;
                },
            }).ToList();
        }

        private void InitalizeChangePageBtns()
        {
            var ct = new[] {"软件版本", "信号信息", "传送信息",};
            var ctf = new[]
            {
                NetControlChildType.SoftVersion,
                NetControlChildType.SignalInfo,
                NetControlChildType.TrainsInfo,
            };
            m_ChangePageBtns =
                m_RectsList.Skip(61)
                    .Take(3)
                    .Select((s, i) => new HXD3DBlueButton()
                    {
                        OutLineRectangle = new Rectangle(s.X - 10, s.Y + 8, s.Width, s.Height),
                        Text = ct[i],
                        Tag = ctf[i],
                        ButtonClickEvent = (sender, args) =>
                        {
                            var btn = (HXD3DBlueButton) sender;
                            Parent.RequestNavigateTo((NetControlChildType) btn.Tag);
                        }
                    }).ToList();
        }

        private void InitGroupTitleInfo()
        {
            var titles = new[] {"LG", "PSU", "TCMS", "MPU1", "APU1", "MPU2", "APU2"};

            m_GroupTitleInfo =
                m_RectsList.Take(8)
                    .Where((r, i) => i != 2)
                    .Select(
                        (s, i) =>
                            new GDIRectText()
                            {
                                OutLineRectangle = Rectangle.Inflate(s, -6, -6),
                                NeedDarwOutline = false,
                                Text = titles[i],
                                TextFormat =
                                    new StringFormat()
                                    {
                                        // 前面三个在上，后面的在下
                                        LineAlignment = i <= 2 ? StringAlignment.Near : StringAlignment.Far,
                                        Alignment = StringAlignment.Center
                                    },
                            })
                    .ToList();
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            m_ChangePageBtns.ForEach(e => e.OnMouseDown(point));

            return true;
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            m_ChangePageBtns.ForEach(e => e.OnMouseUp(point));

            return true;
        }


        /// <summary>刷新并绘图, 会调用 Refresh</summary>
        /// <param name="g"></param>
        public override void OnPaint(Graphics g)
        {
            m_GroupTitleInfo.ForEach(e => e.OnDraw(g));

            m_ChangePageBtns.ForEach(e => e.OnDraw(g));

            DrawGreenLines(g);

            DrawWhiteLines(g);

            m_ContentItems.ForEach(e => e.OnPaint(g));
        }

        private void DrawWhiteLines(Graphics g)
        {
            for (var i = 0; i < 9; i++)
            {
                g.DrawRectangle(Pens.White, m_RectsList[i]);
            }
        }


        private void DrawGreenLines(Graphics g)
        {
            for (var i = 0; i < 34; i++)
            {
                g.DrawRectangle(Pens.Green, m_RectsList[27 + i]);
            }
        }
    }
}