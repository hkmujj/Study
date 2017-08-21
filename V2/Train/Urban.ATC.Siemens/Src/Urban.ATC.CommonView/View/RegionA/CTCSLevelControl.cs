using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionA
{
    public partial class CTCSLevelControl : Label
    {
        private CTCSType m_CTCSType;
        private IATP m_ATP;
        private static Dictionary<CTCSType, string> m_CTCSDescriptionDictionary;

        public CTCSType CTCSType
        {
            set
            {
                m_CTCSType = value;
                UpdateTxt();
                Invalidate();
            }
            get { return m_CTCSType; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IATP ATP
        {
            set
            {
                if (m_ATP != null)
                {
                    m_ATP.CTCS.PropertyChanged -= CTCSOnPropertyChanged;
                }
                m_ATP = value;
                CTCSType = m_ATP.CTCS.CurrentCTCSType;
                if (m_ATP != null)
                {
                    m_ATP.CTCS.PropertyChanged += CTCSOnPropertyChanged;
                }
            }
            get { return m_ATP; }
        }

        private void CTCSOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<ICTCS, CTCSType>(a => a.CurrentCTCSType))
            {
                CTCSType = m_ATP.CTCS.CurrentCTCSType;
            }
            else if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<ICTCS, bool>(a => a.Visible))
            {
                this.InvokeUpdateVisibleIfNeed(ATP.CTCS.Visible);
            }
        }

        private static Dictionary<CTCSType, string> CTCSDescriptionDictionary
        {
            get
            {
                if (m_CTCSDescriptionDictionary == null)
                {
                    m_CTCSDescriptionDictionary = Enum.GetValues(typeof(CTCSType))
                        .Cast<CTCSType>()
                        .ToDictionary(kvp => kvp, kvp => EnumUtil.GetDescription(kvp)[0]);
                }
                return m_CTCSDescriptionDictionary;
            }
        }

        public CTCSLevelControl()
        {
            InitializeComponent();
        }


        private void UpdateTxt()
        {
            this.InvokeUpdateTextIfNeed(CTCSDescriptionDictionary[CTCSType]);
        }
    }
}
