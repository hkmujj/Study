using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YunDa.JC.MMI.Common.Extensions;
using Excel.Interface;

namespace CommonControls
{
    public partial class DefTable : PictureBox
    {
        [Description("表格的行数"),Category("自定义属性")]
        [Browsable(true)]
        public Int32 RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                Invalidate();
            }
        }
        private Int32 _rowCount = 16;

        [Description("表格的行高"), Category("自定义属性")]
        [Browsable(true)]
        public Int32 RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                Invalidate();
            }
        }
        private Int32 _rowHeight = 26;

        [Description("序号的宽度"), Category("自定义属性")]
        [Browsable(true)]
        public Int32 SerialNumberWidth
        {
            get { return _serialNumberWidth; }
            set
            {
                _serialNumberWidth = value;
                Invalidate();
            }
        }
        private Int32 _serialNumberWidth = 30;

        [Description("状态集合"), Category("自定义属性")]
        [Browsable(true)]
        public List<String> States 
        {
            get { return _states; }
            set
            {
                _states = value;
            }
        }
        private List<String> _states = new List<string>();

        [Description("当前页面"), Category("自定义属性")]
        [Browsable(true)]
        public Int32 CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                this.InvokeInvalidate();
            }
        }
        private Int32 _currentPage = 0;

        private List<Int32> _currentStates = new List<int>();
        private List<LogicInfo> _logicInfos = new List<LogicInfo>();
        public DefTable()
        {
            InitializeComponent();
            IsTheFirstPage = true;

            //readData();
        }

        public void SetLogicInfo(List<LogicInfo> logicInfos)
        {
            this._logicInfos = logicInfos;
            this.InvokeInvalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            for (int i = 0; i < _rowCount; i++)
            {
                Brush sb = Brushes.White;
                if (_currentStates.Contains(_currentPage * _rowCount + i))
                {
                    e.Graphics.FillRectangle(
                                Brushes.White,
                                new Rectangle(
                                    _currentPage == 0 ? (0 + 1 + 2) : (this.Width - _serialNumberWidth - 2+2),
                                    _currentPage == 0 ? (0 + i * _rowHeight + 2) : (this.Height - _rowCount * _rowHeight + i * _rowHeight - 1+2),
                                    _serialNumberWidth-3,
                                    _rowHeight-3
                                    )
                                );
                     e.Graphics.FillRectangle(
                                Brushes.White,
                                new Rectangle(
                                    0 + 1 + _serialNumberWidth + 2,
                                    (this._currentPage == 0 ? 0 : (this.Height - _rowCount * _rowHeight - 1)) + i * _rowHeight + 2,
                                    this.Width - _serialNumberWidth * 2 - 3 - 3,
                                    _rowHeight - 3
                                    )
                                );
                     sb = Brushes.Black;
                }
                        

                e.Graphics.DrawRectangle(
                    Pens.White,
                    new Rectangle(
                        0+1,
                        0 + i * _rowHeight, 
                        _serialNumberWidth, 
                        _rowHeight
                        )
                    );
                e.Graphics.DrawRectangle(
                    Pens.White,
                    new Rectangle(
                        this.Width-_serialNumberWidth-2,
                        this.Height - _rowCount*_rowHeight + i * _rowHeight-1,
                        _serialNumberWidth,
                        _rowHeight
                        )
                    );

                e.Graphics.DrawRectangle(
                    Pens.White,
                    new Rectangle(
                        0 + 1+_serialNumberWidth,
                        (this._currentPage == 0 ? 0 : (this.Height - _rowCount * _rowHeight-1)) + i * _rowHeight, 
                        this.Width-_serialNumberWidth*2-3,
                        _rowHeight
                        )
                    );
                //序号
                e.Graphics.DrawString(
                      (_currentPage*16+i+1).ToString(),
                      Font,
                      _currentPage==0?sb:Brushes.White,
                      new Rectangle(
                        0 + 1,
                        0 + i * _rowHeight,
                        _serialNumberWidth,
                        _rowHeight
                        ),
                      StaticProperty.SfCenter
                      );
                e.Graphics.DrawString(
                      (_currentPage * 16+16 + i+1).ToString(),
                      Font,
                      _currentPage != 0 ? sb : Brushes.White,
                      new Rectangle(
                        this.Width - _serialNumberWidth - 2,
                        this.Height - _rowCount * _rowHeight + i * _rowHeight - 1,
                        _serialNumberWidth,
                        _rowHeight
                        ),
                      StaticProperty.SfCenter
                      );
            }

            paint_State(e.Graphics);
        }

        private Brush _brush = Brushes.White;
        private void paint_State(Graphics g)
        {
            if (_logicInfos == null || _logicInfos.Count == 0) return;

            Int32 count = (_currentPage == _logicInfos.Count / _rowCount ? (_logicInfos.Count - _logicInfos.Count / _rowCount * _rowCount) : _rowCount);
            for (int i = 0; i < count; i++)
            {
                _brush = Brushes.White;
                if (_currentStates.Contains(_currentPage * _rowCount + i))
                {
                    _brush = Brushes.Black;
                }
                g.DrawString(
                          _logicInfos[_currentPage * _rowCount + i].Description,
                          Font,
                          _brush,
                          new Rectangle(
                              0 + 1 + _serialNumberWidth,
                              (this._currentPage == 0 ? 0 : (this.Height - _rowCount * _rowHeight - 1)) + i * _rowHeight+4,
                              this.Width - _serialNumberWidth * 2 - 3,
                              _rowHeight
                              ),
                          StaticProperty.SfLeftCenter
                          );
            }
        }

        public void SetRowState(Int32 id,bool state)
        {
            Int32 index = -1;
            for (int i = 0; i < _logicInfos.Count; i++)
            {
                if (_logicInfos[i].ID == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return;

            if (_currentStates.Contains(index)) _currentStates.Remove(index);
            else if(state)
            {
                _currentStates.Add(index);
            }
            this.InvokeInvalidate();
            //Invalidate();
        }

        public bool IsTheLastPage { get; set; }
        public bool IsTheFirstPage { get; set; }

        public void NextPage()
        {
            if (_currentPage < _logicInfos.Count / _rowCount)
            {
                CurrentPage++;
                IsTheFirstPage = false;
            }
            if (_currentPage == _logicInfos.Count / _rowCount) 
            IsTheLastPage = true;
        }

        public void LastPage()
        {
            if (_currentPage > 0)
            {
                CurrentPage--;
                IsTheLastPage = false;
            }
            if(_currentPage==0) 
            IsTheFirstPage = true;
        }
    }
}
