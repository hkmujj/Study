using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using Urban.Iran.DMI.Model;

namespace Urban.Iran.DMI.Controls
{
    public class DWellControl : GDIRectText
    {
        public float TimeIndicator { set; get; }

        public DWellState State { set; get; }

        public ReadOnlyCollection<Tuple<DWellState, int>> StateIndexCollection { set; get; }

        public int TimeIndex { set; get; }

        private readonly baseClass m_SrcObj;

        public DWellControl(baseClass srcObj)
        {
            m_SrcObj = srcObj;
            BkColor = Color.FromArgb(84, 84, 254);
            TextColor = Color.White;
            TextFormat.Alignment = StringAlignment.Center;
            TextFormat.LineAlignment = StringAlignment.Center;
            TextFormat.SetTabStops(0, new float[] { 15, 15 });
        }

        public override void Refresh()
        {
            State = DWellState.None;
            var st = StateIndexCollection.FirstOrDefault(f => m_SrcObj.BoolList[f.Item2]);
            if (st != null)
            {
                State = st.Item1;
            }

            Visible = true;
            switch (State)
            {
                case DWellState.None:
                    Visible = false;
                    break;
                case DWellState.TimeIndicator:
                    TimeIndicator = m_SrcObj.FloatList[TimeIndex];
                    Text = string.Format("Dwell\t{0}", TimeIndicator.ToString("0").PadLeft(6));
                    break;
                case DWellState.TrainHold:
                    Text = "Dwell  HOLD";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            base.Refresh();
        }
    }
}