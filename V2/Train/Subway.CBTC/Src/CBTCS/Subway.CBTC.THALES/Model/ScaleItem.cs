using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering.Helpers;

namespace Subway.CBTC.THALES.Model
{

    public class SpeedDialScale
    {
        public static SpeedDialScale Instance = new SpeedDialScale();
        public ReadOnlyCollection<ScaleItem> AllScaleItems { get; private set; }

        public SpeedDialScale()
        {
            var all = new List<ScaleItem>();
            AllScaleItems = new ReadOnlyCollection<ScaleItem>(all);
            var texts = GetText().GetEnumerator();
            foreach (var anle in GetAnles())
            {
                var len = 5;
                var text = string.Empty;
                if (anle.Item1 % 10 == 0)
                {
                    texts.MoveNext();
                    text = texts.Current;
                    len = 10;
                }
                all.Add(new ScaleItem(anle.Item1, anle.Item2, len, text));
            }

        }

        private IEnumerable<string> GetText()
        {
            for (int i = 0; i <= 90; i += 10)
            {
                yield return i.ToString();
            }
        }

        private IEnumerable<Tuple<float, float>> GetAnles()
        {
            var startAngle = -43;
            var conut = 18;
            var step = (224.5 - startAngle) / conut;
            for (int i = 0; i <= conut; ++i)
            {
                yield return new Tuple<float, float>(i * 5, startAngle + (float)step * i);
            }

        }
    }

    public class ScaleItem
    {
        public ScaleItem(float speed, float angle, float length, string text = null)
        {
            Speed = speed;
            Angle = angle;
            Length = length;
            Text = text;
        }
        /// <summary>
        /// 角度
        /// </summary>
        public float Angle { get; set; }
        /// <summary>
        /// 线条长度
        /// </summary>
        public float Length { get; set; }
        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { get; set; }


    }
}
