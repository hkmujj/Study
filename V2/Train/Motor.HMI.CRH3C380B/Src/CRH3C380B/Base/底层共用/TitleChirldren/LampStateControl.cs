using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.底层共用.TitleChirldren
{
    public class LampStateControl : TitleChirldBase
    {
        private Image m_LampStaetImage;

        private readonly LampStateViewAdapter m_LampStateViewAdapter;

        private LampState m_LampState;

        private readonly int[] m_StateIndexs;

        public LampState LampState
        {
            private set
            {
                if (value == m_LampState)
                {
                    return;
                }

                m_LampState = value;
                m_LampStaetImage = m_LampStateViewAdapter.GetImage(value);
            }
            get { return m_LampState; }
        }



        public LampStateControl(DMITitle title)
            : base(title)
        {
            m_LampStateViewAdapter =
                new LampStateViewAdapter();
            m_StateIndexs =
                (new[]
                {InBoolKeys.Inb灯光状态位0位, InBoolKeys.Inb灯光状态位1位, InBoolKeys.Inb灯光状态位2位, InBoolKeys.Inb信息状态条远光灯强}).Select(
                    title.GetInBoolIndex).ToArray();
            title.UIObj.InBoolList.AddRange(m_StateIndexs);
            RefreshAction = o => RefreshLampState();
        }

        public override void OnDraw(Graphics g)
        {
            if (m_LampStaetImage != null)
            {
                g.DrawImage(m_LampStaetImage, OutLineRectangle);
            }
        }

        private void RefreshLampState()
        {
            LampState = LampState.Closed;
            if (Title.BoolList[m_StateIndexs[0]])
            {
                LampState = LampState.LowLightFar;
            }
            if (Title.BoolList[m_StateIndexs[1]])
            {
                LampState = LampState.HightLightNear;
            }
            if (Title.BoolList[m_StateIndexs[2]])
            {
                LampState = LampState.LowLightNear;
            }
            if (Title.BoolList[m_StateIndexs[3]])
            {
                LampState = LampState.HightLightFar;
            }
        }
    }

    public enum LampState
    {
        Closed,

        [Description("车灯c.png")] LowLightFar,
        [Description("车灯b.png")] HightLightNear,
        [Description("车灯d.png")] HightLightFar,
        [Description("车灯a.png")] LowLightNear,
    }

    public class LampStateViewAdapter
    {
        private readonly Dictionary<LampState, Image> m_StateImagedDictionary;

        public LampStateViewAdapter()
        {
            m_StateImagedDictionary = new Dictionary<LampState, Image>();
        }

        public Image GetImage(LampState state)
        {
            if (state == LampState.Closed)
            {
                return null;
            }

            if (!m_StateImagedDictionary.ContainsKey(state))
            {
                InitalizeStateImages();
            }

            return m_StateImagedDictionary[state];
        }

        private void InitalizeStateImages()
        {
            m_StateImagedDictionary.Add(LampState.LowLightFar, CommonImages.车灯c);
            m_StateImagedDictionary.Add(LampState.HightLightNear, CommonImages.车灯b);
            m_StateImagedDictionary.Add(LampState.HightLightFar, CommonImages.车灯d);
            m_StateImagedDictionary.Add(LampState.LowLightNear, CommonImages.车灯a);
        }
    }
}