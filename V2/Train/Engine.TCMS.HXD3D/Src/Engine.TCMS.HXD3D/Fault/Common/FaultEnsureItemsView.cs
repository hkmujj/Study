using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.Extension;
using Engine.TCMS.HXD3D.HXD3D;

namespace Engine.TCMS.HXD3D.Fault.Common
{
    internal class FaultEnsureItemsView : FautItemsView
    {
        /// <summary>初始化</summary>
        public override void Init()
        {
            InitalizeGridLine();

            InitalizeFautlLineView();
        }

        private void InitalizeFautlLineView()
        {
            var x1 = 0;
            var x2 = 800;
            var y = 118;
            var height = 26;
            var count = 15;

            var xs = GetGridlineXs().ToList();

            m_FaultLineViews =
                Enumerable.Range(0, count)
                    .Select((s, i) => new FaultLineView(CreateLineLabs(xs, y, height, i),
                        (contents, msg) =>
                        {
                            if (msg != null)
                            {
                                contents[0].Text = msg.TheMsgLevel.ToFaultName();
                                contents[1].Text = msg.TheSameMsgNumb.ToString();
                                contents[2].Text = msg.MsgReceiveTime.ToString("yy/MM/dd HH:mm:ss");
                                contents[3].Text = msg.SubSysName;
                                contents[4].Text = msg.MsgID;
                                contents[5].Text = msg.FaultSolutionStr;
                                contents[6].Text = msg.HasAck ? "Yes" : "No";
                            }
                            else
                            {
                                contents[0].Text = string.Empty;
                                contents[1].Text = string.Empty;
                                contents[2].Text = string.Empty;
                                contents[3].Text = string.Empty;
                                contents[4].Text = string.Empty;
                                contents[5].Text = string.Empty;
                                contents[6].Text = string.Empty;
                            }
                        })
                    {
                        OutLineRectangle = new Rectangle(x1, y + i * height, 800, height),
                        SelectedBrush = SolidBrushsItems.BlueBrush3,
                        LineClicked = (sender, args) =>
                        {
                            if (args.Arg != null)
                            {
                                OnItemClicked(args);
                            }
                        }
                    })
                    .ToList();

        }

        private List<Label> CreateLineLabs(List<int> xs, int y, int height, int lineIndex)
        {
            var titls = GetTitleNames();

            return xs.Select(
                (s, i) =>
                    new Label()
                    {
                        OutLineRectangle =
                            new Rectangle(s, y + lineIndex * height, i != xs.Count - 1 ? xs[i + 1] - s : 800 - s, height),
                        Text = titls[i],
                        Brush = Brushes.White,
                        Format =
                        {
                            Alignment = i == 1 ? StringAlignment.Far : StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        },
                    }).ToList();
        }

        private string[] GetTitleNames()
        {
            return new[] { "Pr", "∑", "发生时间", "装置", "号", "名称/内容", "Ack"};
        }

        private void InitalizeGridLine()
        {
            var x1 = 0;
            var x2 = 800;
            var y = 118;
            var height = 26;
            var count = 15;

            m_GridLines =
                Enumerable.Range(0, count + 1)
                    .Select((s, i) => new Line(new Point(x1, y + height * i), new Point(x2, y + height * i)))
                    .ToList();

            var yend = y + height * count;

            m_GridLines.AddRange(GetGridlineXs().Select(s => new Line(new Point(s, y), new Point(s, yend))));
        }

        private IEnumerable<int> GetGridlineXs()
        {
            yield return 0;
            yield return 32;
            yield return 65;
            yield return 176;
            yield return 288;
            yield return 325;
            yield return 720;
        }
    }
}