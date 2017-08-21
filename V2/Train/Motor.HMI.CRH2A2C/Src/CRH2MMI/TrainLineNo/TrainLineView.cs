using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;


namespace CRH2MMI.TrainLineNo
{
    class TrainLineView : CommonInnerControlBase
    {
        private static readonly Size DefaultTxtSize = new Size(20, 20);
        private const int DefaultInterval = 10;
        private static readonly Size DefaultSize;
        private static readonly Size DefaultConstInfoSize = new Size(50, 20);
        private static readonly List<Size> DefaultRectSize;

        static TrainLineView()
        {
            DefaultRectSize = new List<Size>()
            {
                new Size(DefaultTxtSize.Width*6 + DefaultInterval*2, DefaultInterval *2 + DefaultTxtSize.Height),
                new Size(DefaultTxtSize.Width*11 + DefaultInterval*2, DefaultInterval *2 + DefaultTxtSize.Height),
            };
            DefaultSize = new Size(DefaultConstInfoSize.Width + DefaultInterval + DefaultRectSize.Sum(s => s.Width),
                Math.Max(DefaultConstInfoSize.Height, DefaultRectSize.Max(m => m.Height)));
        }

        public TrainLineView()
        {
            Size = DefaultSize;

            Init();

            OutLineChanged = OnOutLineChanged;
        }

        public void GotoNext()
        {
            ReselectTrainLineTxt(m_TranLineTxts[(m_TranLineTxts.IndexOf(m_SelectedTrainLineText) + 1) % m_TranLineTxts.Count]);
        }

        public void GotoPrevious()
        {
            ReselectTrainLineTxt(m_TranLineTxts[(m_TranLineTxts.IndexOf(m_SelectedTrainLineText) + m_TranLineTxts.Count - 1) % m_TranLineTxts.Count]);
        }

        public void DeleteCurrent()
        {
            m_SelectedTrainLineText.Text = string.Empty;
            GotoPrevious();
        }

        public void ReplaceCurrent(string content)
        {
            if (m_SelectedTrainLineText != null)
            {
                m_SelectedTrainLineText.Text = content;
            }
            GotoNext();
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var scalx = (float)Size.Width / DefaultSize.Width;
            var scaly = (float)Size.Height / DefaultSize.Height;
            var scalMat = new Matrix();
            scalMat.Scale(scalx, scaly);
            var mat = new Matrix();
            mat.Translate(Location.X, Location.Y);
            mat.Scale(scalx, scaly);

            m_ConstInfo.OutLineRectangle = new Rectangle(mat.TransformPoint(m_ConstInfo.Location),
                new Size(scalMat.TransformPoint(new Point(m_ConstInfo.Size.Width, m_ConstInfo.Size.Height))));

            for (int i = 0; i < m_BlackRectangles.Count; i++)
            {
                m_BlackRectangles[i] = new Rectangle(mat.TransformPoint(m_BlackRectangles[i].Location),
                new Size(scalMat.TransformPoint(new Point(m_BlackRectangles[i].Size.Width, m_BlackRectangles[i].Size.Height))));
            }

            m_TranLineTxts.ForEach(e => e.OutLineRectangle = new Rectangle(mat.TransformPoint(e.Location),
                new Size(scalMat.TransformPoint(new Point(e.Size.Width, e.Size.Height)))));

        }



        private List<GDIRectText> m_TranLineTxts;
        private GDIRectText m_SelectedTrainLineText;

        /// <summary>
        /// 黑色区域 
        /// </summary>
        private List<Rectangle> m_BlackRectangles; 

        private GDIRectText m_ConstInfo;
        private string m_TrainLine;

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainLine
        {
            set
            {
                m_TrainLine = value;
                var tmp = m_TrainLine.ToCharArray();
                var min = Math.Min(tmp.Length, m_TranLineTxts.Count);
                for (int i = 0; i < min; i++)
                {
                    m_TranLineTxts[i].Text = tmp[i].ToString();
                }
                for (int i = min; i < m_TranLineTxts.Count; i++)
                {
                    m_TranLineTxts[i].Text = string.Empty;
                }
            }
            get { return m_TrainLine; }
        }


        public override void Init()
        {
            InitBlackRect();

            InitConstInfo();

            InitTrainLineTxt();

        }

        public void RefreshTrainLine()
        {
            var tmp = m_TranLineTxts.FindAll(f => !f.Text.IsNullOrWhiteSpace()).Select(s => s.Text);
            var sb = new StringBuilder();
            foreach (var s in tmp)
            {
                sb.Append(s);
            }
            m_TrainLine = sb.ToString();
        }

        private void InitTrainLineTxt()
        {
            m_TranLineTxts = new List<GDIRectText>();
            var point = new Point(m_BlackRectangles[0].X + DefaultTxtSize.Width + DefaultInterval,
                m_BlackRectangles[0].Y + DefaultInterval);
            var font = new Font(CRH2Resource.Font12.FontFamily, 10);
            var strFormat = new StringFormat() {LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft};
            for (int i = 0; i < 4; i++)
            {
                m_TranLineTxts.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(point.X +i*DefaultTxtSize.Width, point.Y, DefaultTxtSize.Width, DefaultTxtSize.Height),
                    TextColor = Color.White,
                    DrawFont = font,
                    TextFormat = strFormat,
                    Text = i.ToString()
                });
            }
            point = new Point(m_BlackRectangles[1].X + DefaultTxtSize.Width + DefaultInterval,
                m_BlackRectangles[1].Y + DefaultInterval);
            for (int i = 0; i < 6; i++)
            {
                m_TranLineTxts.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(point.X + i * DefaultTxtSize.Width, point.Y, DefaultTxtSize.Width, DefaultTxtSize.Height),
                    TextColor = Color.White,
                    DrawFont = font,
                    TextFormat = strFormat,
                    Text = i.ToString()
                });
            }
            ReselectTrainLineTxt(m_TranLineTxts.First());
        }

        private void InitConstInfo()
        {
            m_ConstInfo = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, DefaultSize.Height / 2 - DefaultConstInfoSize.Height / 2, DefaultConstInfoSize.Width, DefaultConstInfoSize.Height),
                BkColor = CRH2Resource.WWBrush.Color,
                TextColor = Color.Black,
                Bold = true,
                Text = "车 次",
            };
        }

        private void InitBlackRect()
        {
            m_BlackRectangles = new List<Rectangle>()
            {
                new Rectangle(DefaultConstInfoSize.Width + DefaultInterval, 0, DefaultRectSize[0].Width, DefaultRectSize[0].Height ),
                new Rectangle(DefaultConstInfoSize.Width + DefaultInterval*2 + DefaultRectSize[0].Width, 0, DefaultRectSize[1].Width, DefaultRectSize[1].Height )
            };
            
        }

        private void ReselectTrainLineTxt(GDIRectText txt)
        {
            if (m_SelectedTrainLineText != null)
            {
                m_SelectedTrainLineText.TextColor = Color.White;
                m_SelectedTrainLineText.BkColor = Color.Black;
            }
            m_SelectedTrainLineText = txt;
            m_SelectedTrainLineText.TextColor = Color.Black;
            m_SelectedTrainLineText.BkColor = Color.Yellow;
        }

        public void Reset()
        {
            ReselectTrainLineTxt(m_TranLineTxts.First());
        }

        public override void OnDraw(Graphics g)
        {
            m_ConstInfo.OnDraw(g);

            m_BlackRectangles.ForEach(e => g.FillRectangle(Brushes.Black, e));

            m_TranLineTxts.ForEach(e => e.OnDraw(g));
        }
    }
}
