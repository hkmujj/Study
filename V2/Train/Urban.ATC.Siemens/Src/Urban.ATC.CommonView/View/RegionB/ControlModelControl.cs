using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionB
{
    public partial class ControlModelControl : Label
    {
        private ControlType m_ControlType;
        private Dictionary<ControlType, string> m_ControlTypeTextMapping;
        private Action<ControlModelControl> m_UpdateTextAction;

        public ControlType ControlType
        {
            set
            {
                m_ControlType = value;
                UpdateText();
                Invalidate();
            }
            get { return m_ControlType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Dictionary<ControlType,string> ControlTypeTextMapping
        {
            set
            {
                m_ControlTypeTextMapping = value;
                UpdateText();
                Invalidate();
            }
            get { return m_ControlTypeTextMapping; }
        }

        /// <summary>
        /// 更新方法
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Action<ControlModelControl> UpdateTextAction
        {
            set
            {
                m_UpdateTextAction = value;
                UpdateText();
                Invalidate();
            }
            get { return m_UpdateTextAction; }
        }

        public ControlModelControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw , true);
            SetStyle(ControlStyles.UserMouse, false);

            ControlTypeTextMapping = Enum.GetValues(typeof (ControlType))
                .Cast<ControlType>()
                .ToDictionary(kvp => kvp, kvp => EnumUtil.GetDescription(kvp)[0]);
        }

        private void UpdateText()
        {
            if (UpdateTextAction != null)
            {
                UpdateTextAction(this);
                return;
            }

            if (ControlTypeTextMapping != null && ControlTypeTextMapping.ContainsKey(ControlType))
            {
                this.InvokeUpdateTextIfNeed(ControlTypeTextMapping[ControlType]);
            }
        }
    }
}
