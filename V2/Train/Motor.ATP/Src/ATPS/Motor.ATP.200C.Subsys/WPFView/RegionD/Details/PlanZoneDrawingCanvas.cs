using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Motor.ATP._200C.Subsys.WPFView.RegionD.Details
{
    [Obsolete]
    class PlanZoneDrawingCanvas : Canvas
    {
        private DrawingVisual m_DrawingVisual;
        private DispatcherTimer m_Timer;

        protected override Visual GetVisualChild(int index)
        {
            return m_DrawingVisual;
        }

        protected override int VisualChildrenCount { get { return 1; } }

        public PlanZoneDrawingCanvas()
        {
            m_DrawingVisual = new DrawingVisual();
            AddVisualChild(m_DrawingVisual);
            AddLogicalChild(m_DrawingVisual);
            m_Timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 200), DispatcherPriority.Render, OnTimer,
                Dispatcher.CurrentDispatcher);
            //m_Timer.Start();
            //this.Draw();
        }

        private void OnTimer(object sender, EventArgs eventArgs)
        {
            InvalidateVisual();
        }

        public static readonly DependencyProperty GeometryStringProperty = DependencyProperty.Register(
            "GeometryString", typeof (string), typeof (PlanZoneDrawingCanvas), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PlanZoneDrawingCanvas)dependencyObject).InvalidateVisual();
        }

        public string GeometryString
        {
            get { return (string) GetValue(GeometryStringProperty); }
            set { SetValue(GeometryStringProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);
            dc.DrawGeometry(Brushes.White, null, Geometry.Parse(GeometryString));
            //dc.Close();
        }
    }
}
