using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.Domain.Model.SpeedMonitoringSection
{
    public class DefaultPlanSectionCoordinate : PlanSectionCoordinateBase
    {
        public DefaultPlanSectionCoordinate()
        {
            var font = new Font("幼圆", 18);
            var pen = Pens.Red;
            var brush = Brushes.Red;
            const double speedZeroHeightScale = 0.3;
            //const double maxWidthScale = 0.96;
            const double maxWidthScale = 1;
            const double ranp = (maxWidthScale - 0.5) / 4;

            m_DistanceScaleConverters = new List<IValueScaleConverter>()
                                        {
                                            new LineValueLogarithmScalConverter(0, 500, 10, 0, 0.5),
                                            new LineValueLineScaleConverter(500, 1000, 0.5, ranp),
                                            new LineValueLineScaleConverter(1000, 2000, 0.5 + ranp , ranp),
                                            new LineValueLineScaleConverter(2000, 4000, 0.5 + ranp * 2, ranp),
                                            new LineValueLineScaleConverter(4000, 8000, 0.5 + ranp * 3, ranp),
                                        };
            m_SpeedScaleConverters = new List<IValueScaleConverter>()
                                     {
                                         new LineValueLineScaleConverter(0, 400, speedZeroHeightScale, 1-speedZeroHeightScale)
                                     };

            for (var i = 0; i < 400; i += 100)
            {
                CoordinateYAxises.Add(new CoordinateAxis(i, GetSpeedScal(i), new Padding(0, 0, 0, 0), i.ToString())
                                      {
                                          TextBrush = brush,
                                          TextFont = font,
                                          AxisPen = pen,
                                      });
            }
            CoordinateXAxises.Add(new CoordinateAxis(0, GetDistanceScal(0), new Padding(0, 0, 0, 0), "0")
            {
                TextBrush = brush,
                TextFont = font,
                AxisPen = pen,
            });
            CoordinateXAxises.Add(new CoordinateAxis(500, GetDistanceScal(500), new Padding(0, 0, 0, 0), "500")
            {
                TextBrush = brush,
                TextFont = font,
                AxisPen = pen,
            });
            for (var i = 1000; i <= 8000; i = i * 2)
            {
                CoordinateXAxises.Add(new CoordinateAxis(i, GetDistanceScal(i), new Padding(0, 0, 0, 0), (i/1000).ToString("0K"))
                {
                    TextBrush = brush,
                    TextFont = font,
                    AxisPen = pen,
                });
            }
        }
    }
}
