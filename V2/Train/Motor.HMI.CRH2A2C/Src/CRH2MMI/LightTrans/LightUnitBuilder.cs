using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.LightTrans.UnitView;

namespace CRH2MMI.LightTrans
{
    class LightUnitBuilder
    {

        private const int MiddlHeight = 240;

        private const int EndPointHeight1 = 190;

        private const int EndPointHeight2 = 380;

        private readonly ReadOnlyCollection<Car> m_Cars;
        private readonly List<LightUnitConfig> m_Endp2Endp;
        private readonly List<LightUnitConfig> m_Mid2Mid;
        private readonly List<LightUnitConfig> m_Endp;
        private readonly List<LightUnitConfig> m_Mid2Endp;

        private readonly Trans m_Trans;

        public LightUnitBuilder(Trans trans, ReadOnlyCollection<Car> cars, List<LightUnitConfig> configs)
        {
            m_Trans = trans;
            m_Cars = cars;


            if (configs.Any(a => a.Lines.Any(a1 => a1.CarName < 1 || a1.CarName > m_Cars.Count)))
            {
                LogMgr.Warn(string.Format("There is any CarName out of range[1,{0}] , please check ther LightTransConfig node of xml.", m_Cars.Count));
            }

            m_Endp2Endp = configs.FindAll(f => f.Lines.All(a => a.PointType == EndPointType.EndPoint));

            m_Mid2Mid = configs.FindAll(f => f.Lines.All(a => a.PointType == EndPointType.Middle));

            m_Endp =
            configs.FindAll(
                    f =>
                        f.Lines.All(
                            a => a.PointType == EndPointType.EndPoint || a.PointType == EndPointType.CI || a.PointType == EndPointType.BCU)
                        && f.Lines.GroupBy(g => g.CarName).Count() == 1);

            // BCU 在CI前面
            m_Endp.ForEach(e => e.Lines.Sort((a, b) => a.PointType.CompareTo(b.PointType)));

            m_Mid2Endp = configs.Except(new[] { m_Endp, m_Endp2Endp, m_Mid2Mid }.SelectMany(s => s)).ToList();


        }

        public List<UnitViewBase> BuildUnitViews()
        {

            var endpView = BuildEndPointViews(m_Endp);

            var midView = BuildMiddleView(m_Mid2Mid);

            var views = new List<UnitViewBase>(endpView.Cast<UnitViewBase>());

            views.AddRange(midView.Cast<UnitViewBase>());

            return views;
        }

        public List<Line> BuildConnetionLines(List<UnitViewBase> unitViewBases)
        {
            var endps = unitViewBases.FindAll(f => f is EndPointUnitView).Cast<EndPointUnitView>().ToList();
            var mids = unitViewBases.FindAll(f => f is MiddleUnitView).Cast<MiddleUnitView>().ToList();

            var endp2EndpView = BuildEndP2EndpLines(m_Endp2Endp, endps);

            var mid2EndView = BuildMid2EndpLines(m_Mid2Endp, mids, endps);

            var lines = new List<Line>(endp2EndpView);
            lines.AddRange(mid2EndView);

            return lines;
        }

        private IEnumerable<Line> BuildMid2EndpLines(List<LightUnitConfig> mid2EndpConfigs, List<MiddleUnitView> mids, List<EndPointUnitView> endps)
        {
            var tmp = new List<Line>();
            foreach (var mid2EndpConfig in mid2EndpConfigs)
            {
                var midConfig = mid2EndpConfig.Lines.Find(f => f.PointType == EndPointType.Middle);
                var midView = mids.Find(f => f.CarName == midConfig.CarName);

                var endpConfig = mid2EndpConfig.Lines.Find(f => f.PointType == EndPointType.EndPoint);
                var endpView = endps.Find(f => f.CarName == endpConfig.CarName);

                tmp.AddRange(BuildM2PLines(midView, endpView, mid2EndpConfig));
            }

            return tmp;
        }

        private IEnumerable<Line> BuildM2PLines(MiddleUnitView midView, EndPointUnitView endpView, LightUnitConfig config)
        {
            var midPoints = midView.GetConnectionPoints();
            var endpPoints = endpView.GetConnectionPoints();

            Point[] needmidPoints = endpPoints[0].Y > midPoints[0].Y ? new Point[] { midPoints[2], midPoints[3] } : new Point[] { midPoints[0], midPoints[1] };
            Point[] needendpPoints = midView.Type == MiddleUnitView.UnitType.UpIs1 ? new Point[] { endpPoints[0], endpPoints[1] } : new Point[] { endpPoints[3], endpPoints[2] };

            var line1 = new Line(needmidPoints[0], new Point(needmidPoints[0].X, needendpPoints[0].Y))
            {
                RefreshAction = o => m_Trans.Resource.GetLinePen(o as Line, config.InBoolColoumNames.ToList())
            };

            var tmp = new List<Line>() { line1 };

            for (int i = 0; i < 2; i++)
            {
                var midp = needmidPoints[i];
                var endp = needendpPoints[i];
                if (i != 0)
                {
                    tmp.Add(new Line(midp, new Point(midp.X, endp.Y)) { RefreshAction = o => ((Line)o).LinePen = line1.LinePen });
                }
                tmp.Add(new Line(endp, new Point(midp.X, endp.Y)) { RefreshAction = o => ((Line)o).LinePen = line1.LinePen });
            }
            return tmp;

        }

        private IEnumerable<Line> BuildEndP2EndpLines(IEnumerable<LightUnitConfig> endp2EndpConfig, List<EndPointUnitView> endps)
        {
            return endp2EndpConfig.SelectMany(sconfig => BuildP2PLines(sconfig.Lines.Select(s => endps.Find(f => f.CarName == s.CarName)), sconfig));
        }

        private IEnumerable<Line> BuildP2PLines(IEnumerable<EndPointUnitView> curEndps, LightUnitConfig config)
        {
            if (curEndps.Count() != 2)
            {
                LogMgr.Error("The count of p2p'point is not 2, can not connect them");
                return new Line[] { };
            }

            var orderbyx = curEndps.OrderBy(o => o.Location.X);

            var points1 = orderbyx.First().GetConnectionPoints();
            var points2 = orderbyx.Last().GetConnectionPoints();

            var line1 = new Line(points1[2], points2[0])
            {
                RefreshAction = o => m_Trans.Resource.GetLinePen(o as Line, config.InBoolColoumNames.ToList())
            };
            var line2 = new Line(points1[3], points2[1])
            {
                RefreshAction = o => ((Line)o).LinePen = line1.LinePen,
            };

            return new[]
            {
                line1, line2
            };

        }

        private IEnumerable<MiddleUnitView> BuildMiddleView(IEnumerable<LightUnitConfig> mid2MidConfig)
        {
            return from lightUnitConfig in mid2MidConfig
                   let carno = lightUnitConfig.Lines[0].CarName - 1
                   let car = m_Cars.First(f => f.CarNo == carno)
                   let type =
                       lightUnitConfig.Lines[0].CarName == m_Cars.Count ? MiddleUnitView.UnitType.UpIs2 : MiddleUnitView.UnitType.UpIs1
                   let x =
                       lightUnitConfig.Lines[0].CarName == m_Cars.Count
                           ? car.Location.X + m_Cars[m_Cars.Count - 2].Size.Width+10
                           : car.Location.X + 10
                   select new MiddleUnitView(type, m_Trans, lightUnitConfig.InBoolColoumNames.ToList())
                   {
                       CarNo = carno,
                       Location = new Point(x, MiddlHeight)
                   };
        }

        private IEnumerable<EndPointUnitView> BuildEndPointViews(IEnumerable<LightUnitConfig> endpConfig)
        {
            return from gpCarName in endpConfig.GroupBy(g => g.Lines[0].CarName)
                   let carno = gpCarName.Key - 1
                   let car = m_Cars.First(f => f.CarNo == carno)
                   let x = carno == 0 ? car.Location.X + m_Cars[0].Size.Width - m_Cars[1].Size.Width : car.Location.X +4
                   let height = gpCarName.Key % 2 == 0 ? EndPointHeight1 : EndPointHeight2
                   select new EndPointUnitView(m_Trans, gpCarName.Select(s => s.InBoolColoumNames.ToList()).ToList())
                   {
                       CarNo = carno,
                       Location = new Point(x, height)
                   };
        }
    }
}
