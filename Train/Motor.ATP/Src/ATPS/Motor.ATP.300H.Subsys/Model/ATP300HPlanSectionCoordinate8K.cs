using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection;
using Motor.ATP.Infrasturcture.Model.SpeedMonitoringSection;

//;

namespace Motor.ATP._300H.Subsys.Model
{
    public class ATP300HPlanSectionCoordinate8K : PlanSectionCoordinateBase
    {
        public ATP300HPlanSectionCoordinate8K()
        {
            CanZoomIn = false;
            CanZoomOut = true;

            var font = new Font("幼圆", 14);
            Pen pen = null;
            Brush brush = null;
            const double speedZeroHeightScale = 0;
            const double maxWidthScale = 1;
            const double rap0 = 0.33;
            const double ranp = (maxWidthScale - rap0) / 3;

            m_DistanceScaleConverters = new List<IValueScaleConverter>()
            {
                new LineValueLogarithmScalConverter(0, 1000, 10, 0, rap0),
                new LineValueLineScaleConverter(1000, 2000, rap0, ranp),
                new LineValueLineScaleConverter(2000, 4000, rap0 + ranp, ranp),
                new LineValueLineScaleConverter(4000, 8000, rap0 + ranp*2, ranp),
                //new LineValueLineScaleConverter(4000, 8000, rap0 + ranp*3, ranp),
            };
            m_SpeedScaleConverters = new List<IValueScaleConverter>()
            {
                new LineValueLineScaleConverter(0, 450, speedZeroHeightScale, 1 - speedZeroHeightScale)
            };

            for (var i = 0; i <= 400; i += 100)
            {
                CoordinateYAxises.Add(new CoordinateAxis(i, GetSpeedScal(i), new Padding(0, 0, 0, 0), i.ToString())
                {
                    TextBrush = brush,
                    TextFont = font,
                    AxisPen = pen,
                });
            }
            CoordinateYAxises.Add(new CoordinateAxis(450, GetSpeedScal(450), new Padding()));

            CoordinateXAxises.Add(new CoordinateAxis(0, GetDistanceScal(0), new Padding(0, 0, 0, 0), "0")
            {
                TextBrush = brush,
                TextFont = font,
                AxisPen = pen,
            });
            for (var i = 1000; i <= 8000; i = i * 2)
            {
                CoordinateXAxises.Add(new CoordinateAxis(i, GetDistanceScal(i), new Padding(0, 0, 0, 0),
                    (i / 1000).ToString("0K"))
                {
                    TextBrush = brush,
                    TextFont = font,
                    AxisPen = pen,
                });
            }
        }
    }
}