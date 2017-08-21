using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    /// <summary>
    /// 减速状态
    /// </summary>
    public partial class SpeedChangesControl : ForecationInfoControlBase
    {
        private ObservableCollection<ISpeedChangeInfo> m_SpeedChangeInfos;

        private readonly List<SpeedChangeTypeControl> m_SpeedChangeTypeControls = new List<SpeedChangeTypeControl>();
        private IATP m_ATP;
        private readonly Action<NotifyCollectionChangedEventArgs> m_UpdateItemsAction;

        public const int MaxShowItemCount = 2;

        public const double ItemWidthProportion = 0.1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IATP ATP
        {
            set
            {
                m_ATP = value;
                m_SpeedChangeTypeControls.ForEach(e => e.AddOpuaqueLayer(ATP.Other));
            }
            get { return m_ATP; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ObservableCollection<ISpeedChangeInfo> SpeedChangeInfos
        {
            set
            {
                if (m_SpeedChangeInfos != value)
                {
                    if (m_SpeedChangeInfos != null)
                    {
                        m_SpeedChangeInfos.CollectionChanged -= SpeedChangeInfosOnCollectionChanged;
                    }
                    m_SpeedChangeInfos = value;
                    if (m_SpeedChangeInfos != null)
                    {
                        m_SpeedChangeInfos.CollectionChanged += SpeedChangeInfosOnCollectionChanged;
                    }
                    Invalidate();
                }
            }
            get { return m_SpeedChangeInfos; }
        }

        public SpeedChangesControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            m_UpdateItemsAction = UpdateItems;

            m_SpeedChangeTypeControls = new List<SpeedChangeTypeControl>()
            {
                speedChangeTypeControl1,
                speedChangeTypeControl2,
                speedChangeTypeControl3,
                speedChangeTypeControl4,
                speedChangeTypeControl5,
                speedChangeTypeControl6,
                speedChangeTypeControl7,
                speedChangeTypeControl8,
                speedChangeTypeControl9,
                speedChangeTypeControl10,
            };
            m_SpeedChangeTypeControls.ForEach(e => e.Visible = false);
            
        }

        protected override void OnSizeChanged(EventArgs args)
        {
            var region = this.GetActureRectangleF();

            m_SpeedChangeTypeControls.ForEach(e =>
            {
                e.Width = (int) (region.Width*ItemWidthProportion);
                e.Height = (int) region.Height;
                if (e.SpeedChangeInfo != null)
                {
                    UpdateItemLocation(e, e.SpeedChangeInfo);
                }
            });

            base.OnSizeChanged(args);
        }

        private void SpeedChangeInfosOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            this.InvokeIfNeed(m_UpdateItemsAction, args);
        }

        private void UpdateItems(NotifyCollectionChangedEventArgs args)
        {
            SpeedChangeTypeControl control;
            ISpeedChangeInfo info;
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    info = (ISpeedChangeInfo) args.NewItems[0];
                    control = m_SpeedChangeTypeControls.FirstOrDefault(f => f.SpeedChangeInfo == null);
                    if (control != null)
                    {
                        control.Visible = true;
                        UpdateItemLocation(control, info);
                        control.SpeedChangeInfo = info;
                        //info.PropertyChanged += SpeedChangeInfoOnPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    info = (ISpeedChangeInfo) args.NewItems[0];
                    control = m_SpeedChangeTypeControls.FirstOrDefault(f => f.SpeedChangeInfo == info);
                    if (control != null)
                    {
                        control.Visible = false;
                        control.SpeedChangeInfo = null;
                        //info.PropertyChanged -= SpeedChangeInfoOnPropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    info = (ISpeedChangeInfo) args.OldItems[0];
                    control = m_SpeedChangeTypeControls.FirstOrDefault(f => f.SpeedChangeInfo == info);
                    if (control != null)
                    {
                        control.Visible = false;
                        control.SpeedChangeInfo = null;
                    }

                    info = (ISpeedChangeInfo) args.NewItems[0];
                    control = m_SpeedChangeTypeControls.FirstOrDefault(f => f.SpeedChangeInfo == null);
                    if (control != null)
                    {
                        control.Visible = true;
                        UpdateItemLocation(control, info);
                        control.SpeedChangeInfo = info;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SpeedChangeInfoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertySupport.ExtractPropertyName<ISpeedChangeInfo, double>(s => s.Distance))
            {
                var item = (ISpeedChangeInfo) sender;
                var control = m_SpeedChangeTypeControls.FirstOrDefault(f => f.SpeedChangeInfo == item);
                if (control != null)
                {
                    UpdateItemLocation(control, item);
                }
            }
        }

        private void UpdateItemLocation(SpeedChangeTypeControl control, ISpeedChangeInfo info)
        {
            control.Left = (int) (PlanSectionCoordinate.GetDistanceScal(info.Distance)*
                                  this.GetActureRectangle().Width) - 4;
        }

        protected override void OnPlanSectionCoordinateChanged()
        {
            foreach (var source in m_SpeedChangeTypeControls.Where(w => w.SpeedChangeInfo != null))
            {
                UpdateItemLocation(source, source.SpeedChangeInfo);
            }
        }
    }
}
