using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using Engine.TCMS.HXD3D.HXD3D;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.TransInfo
{
    public class TransInfoContent : CommonInnerControlBase
    {
        private List<Label> m_SendTitle;

        private List<Label> m_RecvTitle;

        private List<Label> m_SendContents;

        private List<Label> m_RecvContents;

        private static readonly Rectangle Zero = new Rectangle(150, 225, 26, 26);

        private List<Label> m_ConstInfos;

        private List<Line> m_Lines;

        private const int CountPerLine = 20;
        private const int LineCount = 4;

        public void UpdateSendData(ReadOnlyCollection<string> send)
        {
            if (send != null)
            { 
                UpdateContent(send, m_SendTitle, m_SendContents);
            }
        }

        public void UpdateRecvData(ReadOnlyCollection<string> recv)
        {
            if (recv != null)
            {
                UpdateContent(recv, m_RecvTitle, m_RecvContents);
            }

        }

        private void UpdateContent(ReadOnlyCollection<string> value, List<Label> title, List<Label> contents)
        {
            for (int i = 0; i < title.Count; ++i)
            {
                title[i].Visible = i < value.Count/CountPerLine + (value.Count%CountPerLine == 0 ? 0 : 1);
            }

            for (int i = 0; i < value.Count; ++i)
            {
                contents[i].Text = value[i];
                contents[i].Visible = true;
            }

            foreach (var l in contents.Skip(value.Count))
            {
                l.Visible = false;
            }
        }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_Lines.ForEach(e => e.OnDraw(g));

            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_SendContents.ForEach(e => e.OnDraw(g));

            m_RecvContents.ForEach(e => e.OnDraw(g));

            m_SendTitle.ForEach(e => e.OnDraw(g));

            m_RecvTitle.ForEach(e => e.OnDraw(g));
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            InitalizeConstInfos();

            InitalizeLines();

            InitalizeContentAndTitle();
        }

        private void InitalizeContentAndTitle()
        {
            m_SendContents = new List<Label>();
            m_RecvContents = new List<Label>();
            m_SendTitle = new List<Label>();
            m_RecvTitle = new List<Label>();

            var x = Zero.X;
            var y = Zero.Y;
            var w = Zero.Width;
            var h = Zero.Height;

            for (int j = 0; j < 4; ++j)
            {
                for (int i = 0; i < 20; ++i)
                {
                    m_SendContents.Add(new Label()
                    {
                        OutLineRectangle = new Rectangle(x + w*i, y + (j + 1)*h, w, h),
                        Text = i.ToString("00"),
                        Brush = Brushes.White,
                        Font = CommonRes.Font12,
                        Format = {Alignment = StringAlignment.Far}
                    });
                }

                m_SendTitle.Add(new Label()
                {
                    OutLineRectangle = new Rectangle(x - w, y + (j + 1)*h, w, h),
                    Text = (j*20).ToString(),
                    Brush = Brushes.White,
                    Font = CommonRes.Font12,
                    Format = {Alignment = StringAlignment.Far}
                });
            }

            for (int j = 4; j < 8; ++j)
            {
                for (int i = 0; i < 20; ++i)
                {
                    m_RecvContents.Add(new Label()
                    {
                        OutLineRectangle = new Rectangle(x + w*i, y + (j + 1)*h, w, h),
                        Text = i.ToString("00"),
                        Brush = Brushes.White,
                        Font = CommonRes.Font12,
                        Format = {Alignment = StringAlignment.Far}
                    });
                }

                m_RecvTitle.Add(new Label()
                {
                    OutLineRectangle = new Rectangle(x - w, y + (j + 1)*h, w, h),
                    Text = ((j - 4)*20).ToString(),
                    Brush = Brushes.White,
                    Font = CommonRes.Font12,
                    Format = {Alignment = StringAlignment.Far}
                });
            }
        }

        private void InitalizeConstInfos()
        {
            m_ConstInfos = new List<Label>(22);

            var x = Zero.X;
            var y = Zero.Y;
            var w = Zero.Width;
            var h = Zero.Height;

            for (int i = 0; i < 20; ++i)
            {
                m_ConstInfos.Add(new Label()
                {
                    OutLineRectangle = new Rectangle(x + w*i, y, w, h),
                    Text = (i + 1).ToString(),
                    Brush = Brushes.White,
                    Font = CommonRes.Font12,
                    Format = {Alignment = StringAlignment.Far}
                });
            }

            var x2 = x - 100;
            var y2 = y + h;
            var w2 = w*2;
            var names = new[] {"发送", "接受"};
            for (int i = 0; i < 2; ++i)
            {
                m_ConstInfos.Add(new Label()
                {
                    OutLineRectangle = new Rectangle(x2, y2 + h*4*i, w2, h),
                    Text = names[i],
                    Brush = Brushes.White,
                    Font = CommonRes.Font12,
                    Format = {Alignment = StringAlignment.Far}
                });
            }
        }

        private void InitalizeLines()
        {
            m_Lines = new List<Line>(6);

            var x = Zero.X;
            var y = Zero.Y;
            var w = Zero.Width;
            var h = Zero.Height;

            // 竖向
            var step = w*5;
            var bottomY = y + h*9;
            for (int i = 0; i < 4; ++i)
            {
                var x1 = x + step*i;
                m_Lines.Add(new Line() {StartPoint = new Point(x1, y), EndPoint = new Point(x1, bottomY)});
            }

            //横向
            step = h*4;
            var x2 = x - 90;
            var x3 = x + w*20 + 5;
            for (int i = 0; i < 2; ++i)
            {
                var y1 = y + step*i + h;
                m_Lines.Add(new Line() {StartPoint = new Point(x2, y1), EndPoint = new Point(x3, y1)});
            }
        }
    }
}