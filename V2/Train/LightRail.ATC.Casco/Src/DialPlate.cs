using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LightRail.ATC.Casco
{
    public class DialPlate
    {
        private List<DialPlateDegreeLineView> m_DegreeLines;
        private ATCControlType m_ControlType;
        private Color m_ContentColor;

        /// <summary>
        /// Ô²ÐÄ
        /// </summary>
        public PointF CenterPoint { private set; get; }

        /// <summary>
        /// °ë¾¶
        /// </summary>
        public float R { private set; get; }

        public LineDialPlateModel LineDialPlateModel { private set; get; }

        public Color ContentColor
        {
            set
            {
                m_ContentColor = value;
                LineDialPlateModel.Pen1.Color = ContentColor;
                LineDialPlateModel.Pen2.Color = ContentColor;
                LineDialPlateModel.TextSolidBrush.Color = ContentColor;
            }
            get { return m_ContentColor; }
        }

        public ATCControlType ControlType
        {
            set {
                if (m_ControlType != value)
                {
                    m_ControlType = value;
                    switch (value)
                    {
                        
                        case ATCControlType.RM:
                            LineDialPlateModel.MaxVisibleDegreeSpeed = 10;
                            break;
                        case ATCControlType.RMR:
                            LineDialPlateModel.MaxVisibleDegreeSpeed = 5;
                            break;
                        case ATCControlType.Unknown:
                        case ATCControlType.Other:
                            LineDialPlateModel.MaxVisibleDegreeSpeed = 80;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("value", value, null);
                    }
                }
            }
            get { return m_ControlType; }
        }

        public DialPlate(LineDialPlateModel lineDialPlateModel, PointF centerPoint, float r)
        {
            LineDialPlateModel = lineDialPlateModel;
            CenterPoint = centerPoint;
            R = r;
            LineDialPlateModel.VisibleDialPlateDegreeCollectionChanged += UpdateDegreeLines;
            UpdateDegreeLines(lineDialPlateModel);
        }

        private void UpdateDegreeLines(IDialPlateModel dialPlateModel)
        {
            m_DegreeLines = LineDialPlateModel.VisibleDialPlateDegreeCollection.Select(s => new DialPlateDegreeLineView(s, CenterPoint, R))
                .ToList();
        }


        public void OnDraw(Graphics g)
        {
            m_DegreeLines.ForEach(e => e.OnDraw(g));
        }
    }
}