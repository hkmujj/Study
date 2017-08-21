using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace YunDa.JC.MMI.Common
{
    [Serializable]
    public class DataConfigInfo
    {
        [Browsable(true)]
        public Int32 Grade { get; set; }

        [Browsable(true)]
        public Int32 BoolLogic
        {
            get { return _boolLogic; }
            set { _boolLogic = value; }
        }
        private Int32 _boolLogic = -1;

        [Browsable(true)]
        public Boolean BoolValue
        {
            get { return _boolValue; }
            set { _boolValue = value; }
        }
        private Boolean _boolValue = true;

        [Browsable(true)]
        public String Format { get; set; }

        [Browsable(true)]
        public Int32 FloatLogic
        {
            get { return _floatLogic; }
            set { _floatLogic = value; }
        }
        private Int32 _floatLogic = -1;

        [Browsable(true)]
        public String DefText
        {
            get { return _defText; }
            set { _defText = value; }
        }
        private String _defText = "";

        [Browsable(true)]
        public Image DefBackground
        {
            get { return _defBackground; }
            set { _defBackground = value; }
        }
        private Image _defBackground = null;

        [Browsable(true)]
        public FontInfo DefFontInfo
        {
            get { return _defFontInfo; }
            set { _defFontInfo = value; }
        }
        private FontInfo _defFontInfo = new FontInfo();

        [Browsable(true)]
        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }
        private Color _backColor = Color.Black;

        public DataConfigInfo()
        {
        }
    }

    [Serializable]
    public class StateCondition
    {
        [Browsable(true)]
        public float MaxValue { get; set; }

        [Browsable(true)]
        public float MinValue { get; set; }

        [Browsable(true)]
        public String DefText { get; set; }

        [Browsable(true)]
        public Font DefFont { get; set; }

        [Browsable(true)]
        public Color TextColor { get; set; }

        [Browsable(true)]
        public Image Background { get; set; }

        [Browsable(true)]
        public Int32 BoolLogic 
        {
            get { return _boolLogic; }
            set { _boolLogic = value; }
        }
        private Int32 _boolLogic = 0;

        [Browsable(false)]
        public Boolean IsValid { get; set; }

        public StateCondition()
        {
        }
    }

    
}
