using System;
using System.Collections.Generic;
using System.Drawing;

namespace Motor.HMI.CRH1A.Running.DialStrategy
{
    class NormalDialStrategy : DialStrategyBase
    {
        private readonly Queue<int> m_CurrentIndexs;

        private bool m_AddState;

        private int m_LastSecond;

        public NormalDialStrategy()
        {
            m_CurrentIndexs = new Queue<int>();
            m_AddState = true;
        }

        //static NormalDialStrategy()
        //{
        //    Instance = new NormalDialStrategy();
        //}

        //public static NormalDialStrategy Instance { private set; get; }
        public override void Display(Graphics g, int second)
        {

            if (second == 0 && m_LastSecond != 0  )
            {
                m_AddState = !m_AddState;
            }
            
            if (m_AddState)
            {
                DrawAddLight(g, second);
            }
            else
            {
                DrawAddDark(g, second);
            }

            m_LastSecond = second;
        }

        private void DrawAddDark(Graphics g, int second)
        {
            for (int i = 0; i <= second; i++)
            {
                OnDrawDark(g, i);
            }
            for (int i = second + 1; i < 60; i++)
            {
                OnDrawLight(g, i);
            }
        }

        private void DrawAddLight(Graphics g, int second)
        {
            for (int i = 0; i <= second; i++)
            {
                OnDrawLight(g, i);
            }
            for (int i = second + 1; i < 60; i++)
            {
                OnDrawDark(g, i);
            }
        }
    }
}
