using System.Collections.Generic;
using System.Drawing;

namespace Subway.ATC.Casco.Common
{
    public enum ATCState
    {
        FullFault,

        PartFault,

        Normal,
    }

    public static class ATCStateExtension
    {
        private static Dictionary<ATCState, Brush> m_StateBrushDictionary;

        public static Brush GetStateBrush(this ATCState state)
        {
            if (m_StateBrushDictionary == null)
            {
                m_StateBrushDictionary = new Dictionary<ATCState, Brush>()
                {
                    {ATCState.FullFault, new SolidBrush(Color.Red)},
                    {ATCState.PartFault, new SolidBrush(Color.Yellow)},
                    {ATCState.Normal, new SolidBrush(Color.FromArgb(40, 144, 48))},
                };
            }
            if (!m_StateBrushDictionary.ContainsKey(state))
            {
                return m_StateBrushDictionary[ATCState.Normal];
            }
            return m_StateBrushDictionary[state];
        }
    }
}