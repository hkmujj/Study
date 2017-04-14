using System;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace MMI.Facility.WPFInfrastructure.View
{
    /// <summary>
    /// 闪烁的装饰器
    /// </summary>
    public class FlickeringDecorator : AdornerDecorator, IFlickeringable
    {
        //private readonly Storyboard m_FlickeringStoryboard;

        private readonly Timer m_Timer;

        /// <summary>
        /// 
        /// </summary>
        public FlickeringDecorator()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = "FlickeringDecorator";
            }

            m_Timer = new Timer(s => Dispatcher.Invoke(new Action(() => Visibility = Visibility != Visibility.Visible ? Visibility.Visible : Visibility.Hidden)));

        }

        /// <summary>
        /// 停止闪烁后的状态
        /// </summary>
        public Visibility VisibilityAfterFlickering
        {
            get { return (Visibility)GetValue(VisibilityAfterFlickeringProperty); }
            set { SetValue(VisibilityAfterFlickeringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VisibilityAfterFlickering.  This enables animation, styling, binding, etc...
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty VisibilityAfterFlickeringProperty =
            DependencyProperty.Register("VisibilityAfterFlickering", typeof(Visibility), typeof(FlickeringDecorator), new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// 闪烁时长
        /// </summary>
        public double DurationSecond
        {
            get { return (double)GetValue(DurationSecondProperty); }
            set { SetValue(DurationSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DurationTick.  This enables animation, styling, binding, etc...
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DurationSecondProperty =
            DependencyProperty.Register("DurationSecond", typeof(double), typeof(FlickeringDecorator), new PropertyMetadata(1d, new PropertyChangedCallback(DurationTickChanged)));

        private static void DurationTickChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ( (FlickeringDecorator) dependencyObject ).OnDurationTickPropertyChanged(dependencyPropertyChangedEventArgs);
        }

        private void OnDurationTickPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            m_Timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(DurationSecond));
        }

        /// <summary>
        /// 是否闪烁
        /// </summary>
        public static readonly DependencyProperty FlickeringProperty =
            DependencyProperty.Register("Flickering",
                typeof(bool),
                typeof(FlickeringDecorator),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    FlickeringChanged,
                    null));

        private static void FlickeringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlickeringDecorator)d).OnFlickeringChanged(e);
        }

        private void OnFlickeringChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Flickering)
            {
                //this.BeginStoryboard(m_FlickeringStoryboard, HandoffBehavior.SnapshotAndReplace, true);
                m_Timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(DurationSecond));
            }
            else
            {
                //m_FlickeringStoryboard.Stop(this);
                m_Timer.Change(-1, -1);
                Visibility = VisibilityAfterFlickering;
            }
        }

        /// <summary>
        /// 是否闪烁
        /// </summary>
        public bool Flickering
        {
            get
            {
                return (bool)GetValue(FlickeringProperty);
            }
            set
            {
                SetValue(FlickeringProperty, value);
            }
        }
    }
}
