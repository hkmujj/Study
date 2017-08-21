using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace LightRail.ATC.Casco
{
    public class LineDialPlateModel : IDialPlateModel
    {
        private float m_MaxVisibleDegreeSpeed;

        private const float SpeedPerDegree = 2;

        private const float AnglePerDegree = 280f/40;

        public static readonly Pen Pen1 = new Pen(Color.White, 2);
        public static readonly Pen Pen2 = new Pen(Color.White, 3);

        public static readonly SolidBrush TextSolidBrush = new SolidBrush(FormatStyle.WhiteSolidBrush.Color);

        /// <summary>
        /// 每一速度 的度数
        /// </summary>
        private const float AnglePerSpeed = AnglePerDegree / SpeedPerDegree;

        public const float ZeroAngleInDrawArc = -50f - 180;

        public event Action<IDialPlateModel> VisibleDialPlateDegreeCollectionChanged;

        public float MaxVisibleDegreeSpeed
        {
            get { return m_MaxVisibleDegreeSpeed; }
            set
            {
                m_MaxVisibleDegreeSpeed = value;
                UpdateVisibleDialPlateDegreeCollection();
            }
        }

        readonly List<DialPlateDegree> m_DialPlateDegreeCollection;
        private List<DialPlateDegree> m_VisibleDialPlateDegreeCollectionProvider;

        List<DialPlateDegree> VisibleDialPlateDegreeCollectionProvider
        {
            get { return m_VisibleDialPlateDegreeCollectionProvider; }
            set
            {
                m_VisibleDialPlateDegreeCollectionProvider = value;
                VisibleDialPlateDegreeCollection = value.AsReadOnly();
            }
        }

        public ReadOnlyCollection<DialPlateDegree> VisibleDialPlateDegreeCollection { private set; get; }

        public LineDialPlateModel(float maxVisibleDegreeSpeed)
        {
            var brush = TextSolidBrush;
            
            var degreeCount = 41;
            var length1 = 15;
            var length2 = 30;
            var length3 = length1 + 5;
            m_DialPlateDegreeCollection =
                Enumerable.Range(0, degreeCount)
                    .Select(s => new DialPlateDegree(s * SpeedPerDegree, AnglePerDegree * s, length1, Pen1, string.Empty, brush, FormatStyle.Font20))
                    .ToList();

            // 10,20,30...
            for (int i = 0; i < degreeCount; i += 5)
            {
                var it = m_DialPlateDegreeCollection[i];
                m_DialPlateDegreeCollection[i] = new DialPlateDegree(it.Speed, it.Angle, length3, it.DegreePen, it.Text, it.TextBrush, FormatStyle.Font20);
            }

            // 20,40, 60...
            for (int i = 0; i < degreeCount; i += 5)
            {
                var it = m_DialPlateDegreeCollection[i];
                m_DialPlateDegreeCollection[i] = new DialPlateDegree(it.Speed, it.Angle, length2, Pen2, it.Speed.ToString("0"), it.TextBrush, FormatStyle.Font20);
            }


            MaxVisibleDegreeSpeed = maxVisibleDegreeSpeed;
        }


        private void UpdateVisibleDialPlateDegreeCollection()
        {
            VisibleDialPlateDegreeCollectionProvider =
                m_DialPlateDegreeCollection.TakeWhile(f => f.Speed <= MaxVisibleDegreeSpeed).ToList();
            var last = VisibleDialPlateDegreeCollection.Last();
            if (Math.Abs(last.Speed - MaxVisibleDegreeSpeed) > float.Epsilon)
            {
                var first = VisibleDialPlateDegreeCollection.First();
                VisibleDialPlateDegreeCollectionProvider.Add(new DialPlateDegree(MaxVisibleDegreeSpeed,
                    ConvertSpeedToAngle(MaxVisibleDegreeSpeed), first.Legth, first.DegreePen,
                    MaxVisibleDegreeSpeed.ToString("0"), first.TextBrush, FormatStyle.Font20));
            }
            OnVisibleDialPlateDegreeCollectionChanged();
        }

        /// <summary>
        /// 转换到表盘角度
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public virtual float ConvertSpeedToAngle(float speed)
        {
            if (speed > m_DialPlateDegreeCollection.Last().Speed)
            {
                speed = m_DialPlateDegreeCollection.Last().Speed;
            }
            var tmp = m_DialPlateDegreeCollection.Last(f => f.Speed <= speed);
            return tmp.Angle + (speed - tmp.Speed) * AnglePerSpeed;

        }

        /// <summary>
        /// 转换到画圆孤角度
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public virtual float ConvertSpeedToDrawArcAngle(float speed)
        {
            return ConvertSpeedToAngle(speed) + ZeroAngleInDrawArc;
        }

        protected virtual void OnVisibleDialPlateDegreeCollectionChanged()
        {
            var handler = VisibleDialPlateDegreeCollectionChanged;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}