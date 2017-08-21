using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;

namespace CommonControls
{
    public partial class DefSetValue_HaveUnit_Two : PictureBox, ISetValue
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

                _defLabel_Title_1.DefText = value;
            }
        }
        private String _defTitle = "标题";

        [Description("输入框背景颜色"), Category("自定义属性")]
        [Browsable(true)]
        public Color InputFrameColor
        {
            get { return _inputFrameColor; }
            set
            {
                if (_inputFrameColor == value) return;
                _inputFrameColor = value;
            }
        }
        private Color _inputFrameColor = Color.Blue;

        [Description("当前输入框编号"), Category("自定义属性")]
        [Browsable(true)]
        public Int32 CurrentLabelID
        {
            get { return _currentLabelID; }
            set
            {
                if (_currentLabelID == value) return;

                if (_currentLabelID >= 0 && _defLabels_SetValue != null &&
                    _currentLabelID < _defLabels_SetValue.Count &&
                    _defLabels_SetValue.Count != 0)
                {
                    _defLabels_SetValue[_currentLabelID].BackColor = Color.Transparent;
                    //Invalidate();
                }

                _currentLabelID = value;
                if (_defLabels_SetValue == null || _defLabels_SetValue.Count == 0)
                {
                    _currentLabelID = -1;
                    return;
                }

                if (_currentLabelID == _defLabels_SetValue.Count || _currentLabelID < -1)
                {
                    if (NextISetValue != null)//移动到下一个设置框
                    {
                        NextISetValue.ToTheFirst();
                    }
                    _currentLabelID = -1;
                    return;
                }

                if (_currentLabelID <= -1)
                {
                    if (LastISetValue != null)//移动到上一个设置框
                    {
                        LastISetValue.ToTheLast();
                    }
                    _currentLabelID = -1;
                    return;
                }

                _defLabels_SetValue[_currentLabelID].BackColor = _inputFrameColor;
                //Invalidate();
            }
        }
        private Int32 _currentLabelID = -1;

        public ISetValue NextISetValue { get; set; }
        public ISetValue LastISetValue { get; set; }

        private List<DefLabel> _defLabels_SetValue = new List<DefLabel>();
        private List<DefLabel> _defLabels_CurValue = new List<DefLabel>();

        public DefSetValue_HaveUnit_Two()
        {
            InitializeComponent();

            _defLabels_SetValue = new List<DefLabel>()
            {
                _defLabel_Set_1,_defLabel_Set_2
            };
            _defLabels_CurValue = new List<DefLabel>()
            {
                _defLabel_Cur_1,_defLabel_Cur_2
            };
        }

        public void ToTheLast()
        {
            CurrentLabelID = _defLabels_SetValue.Count - 1;
        }

        public void ToTheFirst()
        {
            CurrentLabelID = 0;
        }

        public void Last()
        {
            CurrentLabelID--;
        }

        public void Next()
        {
            CurrentLabelID++;
        }

        public void SetValue(Int32 value)
        {
            _defLabels_SetValue[_currentLabelID].DefText = value.ToString();
            CurrentLabelID++;
        }

        public Int32 GetValue()
        {
            Int32 value = 0;
            String strValue = "";
            for (int i = 0; i < _defLabels_SetValue.Count; i++)
            {
                if (_defLabels_SetValue[i].DefText != "" && _defLabels_SetValue[i].DefText != null)
                {
                    if (i < _defLabels_CurValue.Count)
                    {
                        strValue += _defLabels_SetValue[i].DefText;
                        _defLabels_CurValue[i].DefText = _defLabels_SetValue[i].DefText;
                    }
                }
            }

            if (strValue != "") value = Convert.ToInt32(strValue);
            return value;
        }
    }
}
