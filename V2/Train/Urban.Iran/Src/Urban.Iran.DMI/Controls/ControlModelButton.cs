using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using CommonUtil.Controls.Button;
using CommonUtil.Model;
using MMI.Facility.Interface;
using Urban.Iran.DMI.Model;

namespace Urban.Iran.DMI.Controls
{
    public class ControlModelButton : GDIButton
    {
        public CommonUtil.Model.IReadOnlyDictionary<ControlModelState, Image> StateImageDictionary { set; get; }

        public ControlModelState State { set; get; }

        public CommonUtil.Model.IReadOnlyDictionary<int, ControlModelState> StateDictionary { set; get; }

        private static readonly Timer FlickTimer = new Timer(1000);

        private static bool m_Flicking;

        private readonly baseClass m_SrcObj;

        static ControlModelButton()
        {
            FlickTimer.Elapsed += FlickTimerOnElapsed;
            FlickTimer.Start();
        }

        public ControlModelButton(baseClass srcObj)
        {
            m_SrcObj = srcObj;
        }

        private static void FlickTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            m_Flicking = !m_Flicking;
        }

        public override void Refresh()
        {
            State = ControlModelState.Normal;

            foreach (var kvp in StateDictionary)
            {
                if (m_SrcObj.BoolList[kvp.Key])
                {
                    State = kvp.Value;
                }
            }

            base.Refresh();
        }

        public override void OnDraw(Graphics g)
        {

            switch (State)
            {
                case ControlModelState.Normal:
                    g.DrawImage(StateImageDictionary[ControlModelState.Normal], OutLineRectangle);
                    break;
                case ControlModelState.Request:
                    g.DrawImage(StateImageDictionary[ControlModelState.Request], OutLineRectangle);
                    break;
                case ControlModelState.Permmit:
                    if (m_Flicking)
                    {
                        g.DrawImage(StateImageDictionary[ControlModelState.Permmit], OutLineRectangle);
                    }
                    else
                    {
                        g.DrawImage(StateImageDictionary[ControlModelState.UnKnow], OutLineRectangle);
                    }

                    break;
                case ControlModelState.Actived:
                    g.DrawImage(StateImageDictionary[ControlModelState.Actived], OutLineRectangle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}