using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Fault
{
    public class RadarTarget : NotificationObject
    {
        private double m_verticalDistance;
        private double m_MinverticalDistance;
        private double m_MaxverticalDistance;

        private double m_horizontalDistance;
        private double m_MinhorizontalDistance;
        private double m_MaxhorizontalDistance;

        private CBTCColor m_backColor;

        private bool m_Visible = false;



        public RadarTarget()
        {
            //水平距离，基于最左边，范围：0 - 100
            MinHorizontalDistance = 0;
            MaxHorizontalDistance = 100;

            //垂直距离，基于底边，默认范围：0 - 200
            MinVerticalDistance = 0;
            MaxVerticalDistance = 200;
        }

        /// <summary>
        /// 垂直距离，单位：米(m)
        /// </summary>
        public double VerticalDistance
        {
            get { return m_verticalDistance; }
            set
            {
                if (value == m_verticalDistance)
                    return;
                m_verticalDistance = value;
                RaisePropertyChanged(() => VerticalDistance);
            }
        }


        /// <summary>
        /// 最小垂直距离，单位：米(m)
        /// </summary>
        public double MinVerticalDistance
        {
            get { return m_MinverticalDistance; }
            set
            {
                if (value == m_MinverticalDistance)
                    return;
                m_MinverticalDistance = value;
                RaisePropertyChanged(() => MinVerticalDistance);
            }
        }


        /// <summary>
        /// 最大垂直距离，单位：米(m)
        /// </summary>
        public double MaxVerticalDistance
        {
            get { return m_MaxverticalDistance; }
            set
            {
                if (value == m_MaxverticalDistance)
                    return;
                m_MaxverticalDistance = value;
                RaisePropertyChanged(() => MaxVerticalDistance);
            }
        }


        /// <summary>
        /// 水平距离，单位：米(m)
        /// 基于最左边，范围：0 - 100
        /// </summary>
        public double HorizontalDistance
        {
            get { return m_horizontalDistance; }
            set
            {
                if (value == m_horizontalDistance)
                    return;
                m_horizontalDistance = value;
                RaisePropertyChanged(() => HorizontalDistance);
            }
        }


        /// <summary>
        /// 最小水平距离，单位：米(m)
        /// </summary>
        public double MinHorizontalDistance
        {
            get { return m_MinhorizontalDistance; }
            set
            {
                if (value == m_MinhorizontalDistance)
                    return;
                m_MinhorizontalDistance = value;
                RaisePropertyChanged(() => MinHorizontalDistance);
            }
        }


        /// <summary>
        /// 最大水平距离，单位：米(m)
        /// </summary>
        public double MaxHorizontalDistance
        {
            get { return m_MaxhorizontalDistance; }
            set
            {
                if (value == m_MaxhorizontalDistance)
                    return;
                m_MaxhorizontalDistance = value;
                RaisePropertyChanged(() => MaxHorizontalDistance);
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public CBTCColor BackColor
        {
            get { return m_backColor; }
            set
            {
                if (value == m_backColor)
                    return;
                m_backColor = value;
                RaisePropertyChanged(() => BackColor);
            }
        }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                    return;

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }
    }
}
