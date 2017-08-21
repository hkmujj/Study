using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MMI.Facility.WPFInfrastructure.View
{
    /// <summary>
    /// 闪烁
    /// </summary>
    public class FlickerManager : DependencyObject
    {
        
        private static readonly Dictionary<double, FlickerCollection> m_FlickerInfoDictionary;

        static FlickerManager()
        {
            m_FlickerInfoDictionary = new Dictionary<double, FlickerCollection>();
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty VisibilityAfterFlickingProperty = DependencyProperty.RegisterAttached(
            "VisibilityAfterFlicking", typeof(Visibility), typeof(FlickerManager),
            new PropertyMetadata(default(Visibility), OnPropertyChangedCallback));

        private static void OnPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            UpdateFlckerItem(dependencyObject as UIElement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetVisibilityAfterFlicking(DependencyObject element, Visibility value)
        {
            element.SetValue(VisibilityAfterFlickingProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Visibility GetVisibilityAfterFlicking(DependencyObject element)
        {
            return (Visibility) element.GetValue(VisibilityAfterFlickingProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty FlickingProperty = DependencyProperty.RegisterAttached(
            "Flicking", typeof(bool), typeof(FlickerManager),
            new PropertyMetadata(default(bool), OnPropertyChangedCallback));

        private static void UpdateFlckerItem(UIElement ui)
        {
            if (ui == null)
            {
                return;
            }

            var dur = GetDurationMiliSecond(ui);
            if (GetFlicking(ui))
            {
                if (!m_FlickerInfoDictionary.ContainsKey(dur))
                {
                    m_FlickerInfoDictionary.Add(dur, new FlickerCollection(GetDurationMiliSecond(ui)));
                }
                
                if (!m_FlickerInfoDictionary[dur].Contains(ui))
                {
                        m_FlickerInfoDictionary[dur].Add(ui);
                }
            }
            else
            {
                if (m_FlickerInfoDictionary.ContainsKey(dur))
                {
                    if (m_FlickerInfoDictionary[dur].Contains(ui))
                    {
                        m_FlickerInfoDictionary[dur].Remove(ui);
                        if (!m_FlickerInfoDictionary[dur].Any())
                        {
                            m_FlickerInfoDictionary.Remove(dur);
                        }
                    }
                }
                ui.Visibility = GetVisibilityAfterFlicking(ui);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DurationMiliSecondProperty = DependencyProperty.RegisterAttached(
            "DurationMiliSecond", typeof(double), typeof(FlickerManager),
            new PropertyMetadata(default(double), OnPropertyChangedCallback));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDurationMiliSecond(DependencyObject element, double value)
        {
            element.SetValue(DurationMiliSecondProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static double GetDurationMiliSecond(DependencyObject element)
        {
            return (double) element.GetValue(DurationMiliSecondProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetFlicking(DependencyObject element, bool value)
        {
            element.SetValue(FlickingProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetFlicking(DependencyObject element)
        {
            return (bool) element.GetValue(FlickingProperty);
        }


        private class FlickerCollection
        {
            double DurationMiliSecond
            {
                set
                {
                    m_DurationMiliSecond = value;
                    m_DispatcherTimer.Stop();
                    m_DispatcherTimer.Interval = TimeSpan.FromMilliseconds(value);
                    if (Math.Abs(DurationMiliSecond) > double.Epsilon)
                    {
                        m_DispatcherTimer.Start();
                    }
                }
                get { return m_DurationMiliSecond; }
            }

            private readonly List<UIElement> m_UiElements;

            private DispatcherTimer m_DispatcherTimer;
            private double m_DurationMiliSecond;

            readonly object m_UpdateLocker = new object();

            public FlickerCollection(double durationMiliSecond)
            {
                m_UiElements = new List<UIElement>();
                m_DispatcherTimer = new DispatcherTimer();
                m_DispatcherTimer.Tick += DispatcherTimerOnTick;
                DurationMiliSecond = durationMiliSecond;
            }

            public bool Contains(UIElement ui)
            {
                lock (m_UpdateLocker)
                {
                    return m_UiElements.Contains(ui);
                }
            }

            public bool Any()
            {
                lock (m_UpdateLocker)
                {
                    return m_UiElements.Any();
                }
            }

            public void Add(UIElement ui)
            {
                lock (m_UpdateLocker)
                {
                    m_UiElements.Add(ui);
                }
                
            }

            public void Remove(UIElement ui)
            {
                lock (m_UpdateLocker)
                {
                    m_UiElements.Remove(ui);
                }
            }

            private void DispatcherTimerOnTick(object sender, EventArgs eventArgs)
            {
                lock (m_UpdateLocker)
                {
                    foreach (var e in m_UiElements)
                    {
                        e.Visibility = e.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                    }
                }
            }
        }

    }
}