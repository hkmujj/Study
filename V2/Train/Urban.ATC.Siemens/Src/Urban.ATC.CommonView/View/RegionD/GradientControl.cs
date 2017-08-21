using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    /// <summary>
    /// 坡度
    /// </summary>
    public partial class GradientControl : ForecationInfoControlBase
    {
        private IGradientInfomation m_GradientInfomation;

        private const int MaxSlopeItemCount = 15;

        private readonly Queue<GradientItemControl> m_SlopeItemControlBuffer;

        private readonly Queue<GradientItemControl> m_UsingSlopeItemControls;
        private IOther m_Other;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IOther Other
        {
            set
            {
                if (m_Other != value)
                {
                    m_Other = value;
                    foreach (var control in m_SlopeItemControlBuffer)
                    {
                        control.AddOpuaqueLayer(value);
                    }
                }
            }
            get { return m_Other; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IGradientInfomation GradientInfomation
        {
            set
            {
                if (m_GradientInfomation != value)
                {
                    if (m_GradientInfomation != null)
                    {
                        m_GradientInfomation.GradientInfomationItemItemsChanged -=
                            DeclivityInfomationItemsOnCollectionChanged;
                    }

                    m_GradientInfomation = value;
                    if (m_GradientInfomation != null)
                    {
                        m_GradientInfomation.GradientInfomationItemItemsChanged +=
                            DeclivityInfomationItemsOnCollectionChanged;
                    }
                    Invalidate();
                }
            }
            get { return m_GradientInfomation; }
        }


        public GradientControl()
        {
            InitializeComponent();
            m_SlopeItemControlBuffer = new Queue<GradientItemControl>(MaxSlopeItemCount);
            m_UsingSlopeItemControls = new Queue<GradientItemControl>(MaxSlopeItemCount);

            for (int i = 0; i < MaxSlopeItemCount; i++)
            {
                var item = new GradientItemControl() { Visible = false, Size = Size };
                Controls.Add(item);
                m_SlopeItemControlBuffer.Enqueue(item);
            }
            SetStyle(ControlStyles.ResizeRedraw, true);
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            foreach (var itemControl in m_SlopeItemControlBuffer)
            {
                itemControl.ParentWidth = Width;
                itemControl.Height = Height;
            }
        }

        private void DeclivityInfomationItemsOnCollectionChanged(object sender, EventArgs notifyCollectionChangedEventArgs)
        {
            var usingCount = m_UsingSlopeItemControls.Count;
            var itemCount = m_GradientInfomation.GradientInfomationItemItems.Count();

            var itemIter = m_GradientInfomation.GradientInfomationItemItems.GetEnumerator();

            if (usingCount >= itemCount)
            {
                for (int i = 0; i < usingCount; i++)
                {
                    var itemControl = m_UsingSlopeItemControls.Dequeue();
                    itemIter.MoveNext();
                    itemControl.GradientInfomationItem = itemIter.Current;
                    m_UsingSlopeItemControls.Enqueue(itemControl);
                    itemControl.Invalidate();
                }
                for (int i = 0; i < usingCount - itemCount; i++)
                {
                    var itemControl = m_UsingSlopeItemControls.Dequeue();
                    itemControl.GradientInfomationItem = null;
                    itemControl.InvokeUpdateVisibleIfNeed(false);
                    itemControl.Invalidate();
                    m_SlopeItemControlBuffer.Enqueue(itemControl);
                }
            }
            else
            {
                for (int i = 0; i < usingCount; i++)
                {
                    var itemControl = m_UsingSlopeItemControls.Dequeue();
                    itemIter.MoveNext();
                    itemControl.GradientInfomationItem = itemIter.Current;
                    itemControl.Invalidate();
                    m_UsingSlopeItemControls.Enqueue(itemControl);
                }
                for (int i = 0; i < itemCount - usingCount; i++)
                {
                    var itemControl = m_SlopeItemControlBuffer.Dequeue();
                    itemIter.MoveNext();
                    itemControl.GradientInfomationItem = itemIter.Current;
                    itemControl.InvokeUpdateVisibleIfNeed(true);
                    itemControl.Invalidate();
                    m_UsingSlopeItemControls.Enqueue(itemControl);
                }
            }
        }
    }
}
