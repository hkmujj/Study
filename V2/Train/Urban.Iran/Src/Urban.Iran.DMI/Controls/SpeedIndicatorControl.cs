using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Model;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI.Controls
{
    public class SpeedIndicatorControl : CommonInnerControlBase, IDisposable
    {
        public ISpeedAngleCollection SpeedAngleCollection { set; get; }

        private Brush m_TextBrush;

        private Rectangle m_PlateImageRectangle;

        private Dictionary<SpeedState, BarCurveResourceModel> m_BarCurveShortDictionary;

        private Image m_SpeedPointImage;

        private readonly baseClass m_SrcObj;

        private SpeedState m_CurrentSpeedState;

        private readonly Matrix m_Matrix;

        private PointF m_CenterPointF;

        private Bitmap m_PlateBitmap;
        private Dictionary<SpeedState, BarCurveResourceModel> m_BarCurveLongDictionary;

        public BarCurveValue TargetSpeed { private set; get; }

        public BarCurveValue PermmittdSpeed { private set; get; }

        public BarCurveValue ExtendedPermiitedSpeed { private set; get; }

        public float CurrentSpeed { set; get; }

        public SpeedState CurrentSpeedState
        {
            set
            {
                m_CurrentSpeedState = value;
                m_TextBrush = GdiCommon.DarkBlueBrush;
                switch (value)
                {
                    case SpeedState.Green:
                    // TODO  add 
                    case SpeedState.Gray:
                        m_SpeedPointImage = ImageResourceFacade.grayPt;
                        break;
                    case SpeedState.Orange:
                        m_SpeedPointImage = ImageResourceFacade.orangePt;
                        break;
                    case SpeedState.Yellow:
                        m_SpeedPointImage = ImageResourceFacade.yellowPt;
                        break;
                    case SpeedState.Red:
                        m_SpeedPointImage = ImageResourceFacade.redPt;
                        m_TextBrush = GdiCommon.WhiteBrush;
                        break;
                    default:
                        m_SpeedPointImage = ImageResourceFacade.grayPt;
                        break;
                }
            }
            get { return m_CurrentSpeedState; }
        }

        public IEnumerable<int> InFloatIndexs
        {
            get
            {
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.目标点速度];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.到目标点的距离];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.列车当前速度];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.ATP推荐速度];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.允许速度];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.离站倒计时];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.速度指针状态];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.目标点速度状态];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.ATP推荐速度状态];
                yield return IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.允许速度状态];
            }
        }

        public SpeedIndicatorControl(baseClass srcObj)
        {
            m_SrcObj = srcObj;
            m_TextBrush = GdiCommon.BkgBrush;
            TargetSpeed = new BarCurveValue(0,100);
            PermmittdSpeed = new BarCurveValue(0,100);
            ExtendedPermiitedSpeed = new BarCurveValue(0, 100);
            InitalizeBarCurveDictionary();
            SpeedAngleCollection = new LineSpeedAngleCollection(45, 360 - 45, 0, 100);
            m_Matrix = new Matrix();
            OutLineChanged += OnOutLineChanged;
        }

        private void InitalizeBarCurveDictionary()
        {
            var l1 = 10;
            var l2 = 20;
            var outlin1 = OutLineRectangle;
            var outlin2 = Rectangle.Inflate(OutLineRectangle, -l1 / 2, -l1 / 2);
            m_BarCurveShortDictionary = new Dictionary<SpeedState, BarCurveResourceModel>()
            {
                {SpeedState.Gray, new BarCurveResourceModel(SpeedState.Gray,new Pen(Color.Gray, l1), outlin1)},
                {SpeedState.Green, new BarCurveResourceModel(SpeedState.Green,new Pen(Color.Green, l1), outlin1)},
                {SpeedState.Orange, new BarCurveResourceModel(SpeedState.Orange,new Pen(Color.Orange, l1), outlin1)},
                {SpeedState.Yellow, new BarCurveResourceModel(SpeedState.Yellow,new Pen(Color.Yellow, l1), outlin1)},
                {SpeedState.Red, new BarCurveResourceModel(SpeedState.Red, new Pen(Color.Red, l1), outlin1)},
            };
            m_BarCurveLongDictionary = new Dictionary<SpeedState, BarCurveResourceModel>()
            {
                {SpeedState.Gray, new BarCurveResourceModel(SpeedState.Gray,new Pen(Color.Gray, l2), outlin2)},
                {SpeedState.Green, new BarCurveResourceModel(SpeedState.Green,new Pen(Color.Green, l2), outlin2)},
                {SpeedState.Orange, new BarCurveResourceModel(SpeedState.Orange,new Pen(Color.Orange, l2), outlin2)},
                {SpeedState.Yellow, new BarCurveResourceModel(SpeedState.Yellow,new Pen(Color.Yellow, l2), outlin2)},
                {SpeedState.Red, new BarCurveResourceModel(SpeedState.Red,new Pen(Color.Red, l2), outlin2)},
            };
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var len = m_BarCurveShortDictionary.First().Value.Pen.Width;
            m_CenterPointF = OutLineRectangle.GetCenterPoint();
            m_PlateImageRectangle = Rectangle.Inflate(OutLineRectangle, (int)-len / 2, (int)-len / 2);
            m_PlateBitmap = new Bitmap(m_PlateImageRectangle.Width, m_PlateImageRectangle.Height);

            foreach (var value in m_BarCurveShortDictionary.Values)
            {
                value.OutLineRectangle = OutLineRectangle;
            }

            var outline2 = Rectangle.Inflate(OutLineRectangle, (int)(-len / 2), (int)(-len / 2));
            foreach (var value in m_BarCurveLongDictionary.Values)
            {
                value.OutLineRectangle = outline2;
            }
        }

        public override void Init()
        {
            var pen = new Pen(Color.White, 2);
            var g = Graphics.FromImage(m_PlateBitmap);
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighQuality;
            var len1 = 20f;
            var center = new PointF(m_PlateBitmap.Size.Width / 2F, m_PlateBitmap.Size.Height / 2f);
            for (int i = 0; i < 100; i += 2)
            {
                var p1 = new PointF((float)(center.X + Math.Cos(GetStandRadianAngle(i)) * center.X),
                    (float)(center.Y + Math.Sin(GetStandRadianAngle(i)) * center.Y));
                var p2 = new PointF((float)(center.X + Math.Cos(GetStandRadianAngle(i)) * (center.X - len1)),
                    (float)(center.Y + Math.Sin(GetStandRadianAngle(i)) * (center.Y - len1)));
                g.DrawLine(pen, p1, p2);
            }
            var len2 = 30f;
            var len3 = 50f;
            var sf = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            for (int i = 0; i <= 100; i += 10)
            {
                var p1 = new PointF((float)(center.X + Math.Cos(GetStandRadianAngle(i)) * center.X),
                    (float)(center.Y + Math.Sin(GetStandRadianAngle(i)) * center.Y));
                var p2 = new PointF((float)(center.X + Math.Cos(GetStandRadianAngle(i)) * (center.X - len2)),
                    (float)(center.Y + Math.Sin(GetStandRadianAngle(i)) * (center.Y - len2)));
                g.DrawLine(pen, p1, p2);

                var p3 = new PointF((float)(center.X + Math.Cos(GetStandRadianAngle(i)) * (center.X - len3)),
                    (float)(center.Y + Math.Sin(GetStandRadianAngle(i)) * (center.Y - len3)));
                g.DrawString(i.ToString(), GdiCommon.Txt15Font, Brushes.White, p3, sf);
            }

            g.DrawString("km/h", GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, new PointF(center.X, center.Y + 45),
                sf);

            g.Dispose();

            pen.Dispose();

            sf.Dispose();

        }

        private float GetStandRadianAngle(float speed)
        {
            return (float)((SpeedAngleCollection.ConvertToAngle(speed) - 270) * Math.PI / 180);
        }

        private float GetDrawArcAngle(float speed)
        {
            return SpeedAngleCollection.ConvertToAngle(speed) + 90;
        }

        public override void Refresh()
        {
            CurrentSpeed = m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.列车当前速度]];
            CurrentSpeedState =
                (SpeedState)
                    Convert.ToInt32(
                        m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.速度指针状态]]);

            TargetSpeed.Speed = m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.目标点速度]];
            TargetSpeed.SpeedState =
                (SpeedState)
                    Convert.ToInt32(
                        m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.目标点速度状态]]);

            PermmittdSpeed.Speed = m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.允许速度]];
            PermmittdSpeed.SpeedState =
                (SpeedState)
                    Convert.ToInt32(
                        m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.允许速度状态]]);

            ExtendedPermiitedSpeed.Speed =
                m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.ATP推荐速度]];
            ExtendedPermiitedSpeed.SpeedState =
                (SpeedState)
                    Convert.ToInt32(
                        m_SrcObj.FloatList[IndexParam.Instance.InFloatKeyIndexDictionary[InFloatKeys.ATP推荐速度状态]]);

            base.Refresh();
        }

        public override void OnDraw(Graphics g)
        {

            g.SmoothingMode = SmoothingMode.HighQuality;

            DrawPlate(g);

            DrawPointer(g);

            DrawSpeedText(g);

            DrawBarCurve(g);
        }

        private void DrawPointer(Graphics g)
        {
            var old = g.Transform;
            g.Transform = m_Matrix;
            m_Matrix.Reset();
            m_Matrix.Multiply(old);
            m_Matrix.RotateAt(SpeedAngleCollection.ConvertToAngle(CurrentSpeed), m_CenterPointF);

            g.DrawImage(m_SpeedPointImage, OutLineRectangle);

            g.Transform = old;
        }

        private void DrawBarCurve(Graphics g)
        {
            if (Math.Abs(TargetSpeed.Speed) < float.Epsilon && Math.Abs(PermmittdSpeed.Speed) < float.Epsilon &&
                Math.Abs(ExtendedPermiitedSpeed.Speed) < float.Epsilon)
            {
                return;
            }


            BarCurveResourceModel model;
            float maxAngle = 5;
            BarCurveResourceModel lastModel = GetResourceModelOrDefault(m_BarCurveShortDictionary, SpeedState.Gray);

            var startAngle = GetDrawArcAngle(0);
            var targetAngle = GetDrawArcAngle(TargetSpeed.Speed);
            var permmittedAngle = GetDrawArcAngle(PermmittdSpeed.Speed);
            var extenedAngle = GetDrawArcAngle(ExtendedPermiitedSpeed.Speed);

            DrawBeforeStartCurve(g);

            if (TargetSpeed.Speed > 0)
            {
                model = GetResourceModelOrDefault(m_BarCurveShortDictionary, TargetSpeed.SpeedState);
                g.DrawArc(model.Pen, model.OutLineRectangle, startAngle - 0.5f,
                    targetAngle - startAngle);
                lastModel = model;
                maxAngle = targetAngle;
            }

            if (PermmittdSpeed.Speed > TargetSpeed.Speed)
            {
                model = GetResourceModelOrDefault(m_BarCurveShortDictionary, PermmittdSpeed.SpeedState);
                g.DrawArc(model.Pen, model.OutLineRectangle, targetAngle - 0.5f,
                    permmittedAngle - targetAngle);
                lastModel = model;
                maxAngle = permmittedAngle;
            }
            lastModel = GetResourceModelOrDefault(m_BarCurveLongDictionary, lastModel.SpeedState);
            g.DrawArc(lastModel.Pen, lastModel.OutLineRectangle, maxAngle - 5, 5);


            if (ExtendedPermiitedSpeed.Speed > TargetSpeed.Speed || ExtendedPermiitedSpeed.Speed > PermmittdSpeed.Speed)
            {
                if (Math.Abs(TargetSpeed.Speed) < float.Epsilon && Math.Abs(PermmittdSpeed.Speed) < float.Epsilon)
                {
                    model = GetResourceModelOrDefault(m_BarCurveShortDictionary, ExtendedPermiitedSpeed.SpeedState);
                }
                else
                {
                    model = GetResourceModelOrDefault(m_BarCurveLongDictionary, ExtendedPermiitedSpeed.SpeedState);
                }
                var tmp = Math.Max(targetAngle, permmittedAngle);
                g.DrawArc(model.Pen, model.OutLineRectangle, tmp - 0.5f, extenedAngle - tmp);
            }

        }

        private void DrawBeforeStartCurve(Graphics g)
        {
            var shorModel = GetBeforeStartModel();
            g.DrawArc(shorModel.Pen, shorModel.OutLineRectangle, GetDrawArcAngle(0) - 5, 5);
        }

        private BarCurveResourceModel GetBeforeStartModel()
        {
            BarCurveResourceModel shorModel;
            if (Math.Abs(TargetSpeed.Speed) < float.Epsilon)
            {
                if (Math.Abs(PermmittdSpeed.Speed) < float.Epsilon)
                {
                    shorModel = GetResourceModelOrDefault(m_BarCurveShortDictionary, ExtendedPermiitedSpeed.SpeedState);
                }
                else
                {
                    shorModel = GetResourceModelOrDefault(m_BarCurveShortDictionary, PermmittdSpeed.SpeedState);
                }
            }
            else
            {
                shorModel = GetResourceModelOrDefault(m_BarCurveShortDictionary, TargetSpeed.SpeedState);
            }
            return shorModel;
        }

        BarCurveResourceModel GetResourceModelOrDefault(Dictionary<SpeedState, BarCurveResourceModel> dic,
            SpeedState speedState)
        {
            if (dic.ContainsKey(speedState))
            {
                return dic[speedState];
            }
            return dic.First().Value;
        }

        private void DrawSpeedText(Graphics g)
        {
            g.DrawString(CurrentSpeed.ToString("0"), GdiCommon.Txt14Font, m_TextBrush, m_CenterPointF,
                GdiCommon.CenterFormat);
        }

        private void DrawPlate(Graphics g)
        {
            g.DrawImage(m_PlateBitmap, m_PlateImageRectangle);
        }

        public void Dispose()
        {
            foreach (var value in m_BarCurveLongDictionary.Values)
            {
                value.Pen.Dispose();
            }

            foreach (var value in m_BarCurveShortDictionary.Values)
            {
                value.Pen.Dispose();
            }
        }
    }
}