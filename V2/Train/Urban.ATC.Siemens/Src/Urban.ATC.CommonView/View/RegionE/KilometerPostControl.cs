using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class KilometerPostControl : Label
    {
        private IKilometerPost m_KilometerPost;

        private readonly Action m_UpdateAction;

        public IKilometerPost KilometerPost
        {
            set
            {
                if (m_KilometerPost != value)
                {
                    if (m_KilometerPost != null)
                    {
                        m_KilometerPost.PropertyChanged -= KilometerPostOnPropertyChanged;
                    }
                    m_KilometerPost = value;
                    
                    if (m_KilometerPost != null)
                    {
                        m_KilometerPost.PropertyChanged += KilometerPostOnPropertyChanged;
                    }
                    UpdateStates();
                    Invalidate();
                }
            }
            get { return m_KilometerPost; }
        }


        public KilometerPostControl()
        {
            InitializeComponent();
            m_UpdateAction = () =>
            {
                UpdateText();

                UpdateVisible();
            };

            Text = "K";
        }

        private void KilometerPostOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Contract.Requires(KilometerPost != null);

            UpdateStates();
        }

        private void UpdateStates()
        {
            this.InvokeIfNeed(m_UpdateAction);
        }


        private void UpdateVisible()
        {
            this.InvokeUpdateVisibleIfNeed(KilometerPost.Visible);
        }

        private void UpdateText()
        {
            this.InvokeUpdateTextIfNeed(string.Format("K{0:0} + {1:0}", (int)KilometerPost.Kilometer, KilometerPost.Meter));
        }
    }
}
