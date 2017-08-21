using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonControls
{
    public partial class DefSetMode : PictureBox, ISetMode
    {
        [Description("标题"), Category("自定义属性")]
        [Browsable(true)]
        public String DefTitle
        {
            get { return _defTitle; }
            set
            {
                if (_defTitle == value) return;
                _defTitle = value;

                _defLabel_Title.DefText = value;
            }
        }
        private String _defTitle = "标题";

        [Description("模式描述"), Category("自定义属性")]
        [Browsable(true)]
        public String DefMode1
        {
            get { return _defMode1; }
            set
            {
                if (_defMode1 == value) return;
                _defMode1 = value;

                _defRadioBtn_1.Text = value;
            }
        }
        private String _defMode1 = "模式一";

        [Description("模式描述"), Category("自定义属性")]
        [Browsable(true)]
        public String DefMode2
        {
            get { return _defMode2; }
            set
            {
                if (_defMode2 == value) return;
                _defMode2 = value;

                _defRadioBtn_2.Text = value;
            }
        }
        private String _defMode2 = "模式二";

        [Browsable(false)]
        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;

                _panel.BorderColor = value ? Color.Blue : Color.White;
                _panel.Invalidate();
                Invalidate();
            }
        }
        private Boolean _isSelected = false;

        public Int32 ModeID { get; set; }
        private Int32 _currentMode = 0;

        private List<DefRadioButton> _defRadioBtns = new List<DefRadioButton>();

        public DefSetMode()
        {
            InitializeComponent();

            _defRadioBtns = new List<DefRadioButton>()
            {
                _defRadioBtn_1,_defRadioBtn_2
            };
            _defRadioBtn_1.Checked = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            

            base.OnPaint(e);
        }

        public void SetMode()
        {
            _currentMode = (_currentMode + 1) % 2;
            _defRadioBtns.ForEach(a => a.Checked = false);
            _defRadioBtns[_currentMode].Checked = true;
        }

        /// <summary>
        /// 送出模式
        /// </summary>
        /// <returns></returns>
        public Int32 GetMode()
        {
            return _currentMode;
        }
    }
}
