using System.Collections.Generic;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.PopupView
{
    public partial class PopupViewBase : UserControl
    {
        public virtual IOther Other { get; set; }

        protected List<Control> m_NeedOpuaqueLayerControls;

        public virtual void AddOpuaqueLayer(IOther other)
        {
            if (other != Other)
            {
                if (Other != null)
                {
                    m_NeedOpuaqueLayerControls.ForEach(e => e.ClearOpuaqueLayer(Other));
                }
                m_NeedOpuaqueLayerControls.ForEach(e => e.AddOpuaqueLayer(other));
                Other = other;
            }
            m_NeedOpuaqueLayerControls.ForEach(e => e.Invalidate());
        }

        public virtual void ClearOpuaqueLayer()
        {
            if (Other != null)
            {
                m_NeedOpuaqueLayerControls.ForEach(e => e.ClearOpuaqueLayer(Other));
            }
            Other = null;
        }

        public PopupViewBase()
        {
            InitializeComponent();
            m_NeedOpuaqueLayerControls = new List<Control>();
        }
    }
}
