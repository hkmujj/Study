using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonControls
{
    public partial class DefFalut : PictureBox
    {
        private List<FalutInfo> _faluts = new List<FalutInfo>();
        private List<FalutInfo> _showingFaluts = new List<FalutInfo>();
        private Int32 _currentShowingFalutIndex = 0;//在显示集合中的索引
        private Int32 _currentFalutIndex = 0;//在故障列表中的索引

        [Description("是否显示结束时间"), Category("自定义属性")]
        [Browsable(true)]
        public Boolean IsShowEndTime 
        {
            get
            {
                return _isShowEndTime;
            }
            set 
            {
                _isShowEndTime = value;
                Invalidate();
            }
        }
        private bool _isShowEndTime = false;

        public DefFalut()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            paint_Frame(e.Graphics);
            paint_Current(e.Graphics);
            paint_Falut(e.Graphics);
        }

        private void paint_Frame(Graphics g)
        {
            //标题栏
            g.DrawString(
                "等级",
                StaticProperty.FontSont11,
                Brushes.Red,
                new RectangleF(0, 0, 36, 30),
                StaticProperty.SfCenter
                );
            g.DrawString(
                "车号",
                StaticProperty.FontSont11,
                Brushes.Black,
                new RectangleF(36, 0, 36, 30),
                StaticProperty.SfCenter
                );
            g.DrawString(
                "代码",
                StaticProperty.FontSont11,
                Brushes.White,
                new RectangleF(72, 0, 60, 30),
                StaticProperty.SfCenter
                );
            g.DrawString(
                "故障内容",
                StaticProperty.FontSont11,
                Brushes.Black,
                new RectangleF(132, 0, _isShowEndTime?360:422, 30),
                StaticProperty.SfCenter
                );
            g.DrawString(
                "故障发生时间",
                StaticProperty.FontSont11,
                Brushes.Black,
                new RectangleF(_isShowEndTime?492:554, 0, _isShowEndTime?140:280, 30),
                StaticProperty.SfCenter
                );

            if (_isShowEndTime)
            {
                g.DrawString(
                    "故障结束时间",
                    StaticProperty.FontSont11,
                    Brushes.Black,
                    new RectangleF(632, 0, 140, 30),
                    StaticProperty.SfCenter
                    );
            }

            g.FillRectangle(Brushes.Yellow, new Rectangle(0,30,36,this.Size.Height-30));
            g.FillRectangle(Brushes.Blue, new Rectangle(72, 30, 60, this.Size.Height - 30));
            g.FillRectangle(Brushes.Yellow, new Rectangle(132, 30, 642, this.Size.Height - 30));
        }

        private SolidBrush _fillBrush = new SolidBrush(Color.FromArgb(14, 0, 105));
        private void paint_Current(Graphics g)
        {
            if (_showingFaluts == null || _showingFaluts.Count == 0) return;

            g.FillRectangle(_fillBrush, new Rectangle(0, 30 + _currentShowingFalutIndex * 25, 774, 25));
        }

        private void paint_Falut(Graphics g)
        {
            if (_showingFaluts == null || _showingFaluts.Count == 0) return;

            for (int i = 0; i < _showingFaluts.Count; i++)
            {
                g.DrawString(
                    _showingFaluts[i].Grade,
                    StaticProperty.FontSont11,
                    _currentShowingFalutIndex==i?Brushes.White:Brushes.Black,
                    new RectangleF(0, 30+25*i, 36, 25),
                    StaticProperty.SfCenter
                    );
                g.DrawString(
                    _showingFaluts[i].TrainID.ToString(),
                    StaticProperty.FontSont11,
                    _currentShowingFalutIndex == i ? Brushes.White : Brushes.Black,
                    new RectangleF(36, 30+25*i, 36, 25),
                    StaticProperty.SfCenter
                    );
                g.DrawString(
                    _showingFaluts[i].Code.ToString(),
                    StaticProperty.FontSont11,
                    Brushes.White,
                    new RectangleF(72, 30+25*i, 60, 25),
                    StaticProperty.SfCenter
                    );
                g.DrawString(
                    _showingFaluts[i].Description,
                    StaticProperty.FontSont11,
                    _currentShowingFalutIndex == i ? Brushes.White : Brushes.Black,
                    new RectangleF(132, 30 + 25 * i, _isShowEndTime ? 360 : 422, 25),
                    StaticProperty.SfLeftCenter
                    );
                g.DrawString(
                    _showingFaluts[i].StartTime,
                    StaticProperty.FontSont11,
                    _currentShowingFalutIndex == i ? Brushes.White : Brushes.Black,
                    new RectangleF(_isShowEndTime ? 492 : 554, 30 + 25 * i, _isShowEndTime ? 140 : 280, 25),
                    StaticProperty.SfCenter
                    );
                if(_isShowEndTime)
                    g.DrawString(
                        _showingFaluts[i].EndTime,
                        StaticProperty.FontSont11,
                        _currentShowingFalutIndex == i ? Brushes.White : Brushes.Black,
                        new RectangleF(632, 30 + 25 * i, 140, 25),
                        StaticProperty.SfCenter
                        );
            }
        }

        public void SetEndTime(FalutInfo falut)
        {
            FalutInfo fi = _faluts.Find(a => a.LogicID == falut.LogicID);
            if (fi != null) fi.EndTime = falut.EndTime;
        }

        public void AddFalut(FalutInfo falut)
        {
            _faluts.Add(falut);

            if (_showingFaluts.Count < 18)
                _showingFaluts.Add(falut);
            Invalidate();
        }

        public void RemoveFalut(FalutInfo falut)
        {
            if (_faluts.Contains(falut))
            {
                _faluts.Remove(falut);
                _showingFaluts.Remove(falut);

                if (_currentShowingFalutIndex >= _showingFaluts.Count)
                {
                    _currentShowingFalutIndex = 0;
                }
            }
            else
            {
                FalutInfo fi = _faluts.Find(a => a.Code == falut.Code);
                if (fi != null)
                {
                    _faluts.Remove(fi);
                    _showingFaluts.Remove(fi);

                    if (_currentShowingFalutIndex >= _showingFaluts.Count)
                    {
                        _currentShowingFalutIndex = 0;
                    }
                }
            }
            Invalidate();
        }

        public void Reset()
        {
            _currentShowingFalutIndex = 0;
            _currentFalutIndex = 0;
            _showingFaluts.Clear();
            _faluts.Clear();
        }

        public void Up()
        {
            if (_currentShowingFalutIndex == 0)
            {
                if (_currentFalutIndex > 0)
                {
                    _showingFaluts.RemoveAt(_showingFaluts.Count - 1);
                    _showingFaluts.Insert(0, _faluts[_currentFalutIndex - 1]);
                    _currentFalutIndex--;
                    Invalidate();
                }
            }
            else
            {
                _currentFalutIndex--;
                _currentShowingFalutIndex--;
                Invalidate();
            }
        }

        public void Down()
        {
            if (_currentShowingFalutIndex == 17)
            {
                if (_currentFalutIndex < _faluts.Count - 1)
                {
                    _showingFaluts.RemoveAt(0);
                    _showingFaluts.Add(_faluts[_currentFalutIndex + 1]);
                    _currentFalutIndex++;
                    Invalidate();
                }
            }
            else
            {
                if (_currentShowingFalutIndex < _showingFaluts.Count - 1)
                {
                    _currentFalutIndex++;
                    _currentShowingFalutIndex++;
                    Invalidate();
                }
            }
        }

        public FalutInfo GetCurrentFalut()
        {
            if (_faluts == null || _faluts.Count == 0 || _faluts.Count <= _currentFalutIndex) return null;
            return _faluts[_currentFalutIndex];
        }
    }

    public class FalutInfo
    {
        public Int32 LogicID { get; set; }

        public String Grade { get; set; }

        public Int32 TrainID { get; set; }

        public String Code { get; set; }

        public String PointOut { get; set; }

        public String Description { get; set; }

        public String StartTime { get; set; }

        public String EndTime { get; set; }

        public String V0 { get; set; }

        public String V1 { get; set; }

        public String Info { get; set; }

        public Boolean IsOK { get; set; }
    }
}
