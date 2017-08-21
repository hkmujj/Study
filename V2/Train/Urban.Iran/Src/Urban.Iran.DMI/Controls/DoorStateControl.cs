using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using MMI.Facility.Interface;
using Urban.Iran.DMI.Model;

namespace Urban.Iran.DMI.Controls
{
    public class DoorStateControl : CommonInnerControlBase
    {
        public DoorState State { set; get; }

        public ReadOnlyCollection<Tuple<int, DoorState>> DoorStateIndexTuples { set; get; }

        public CommonUtil.Model.IReadOnlyDictionary<DoorState, Image> StateImageDictionary { set; get; }

        private readonly baseClass m_SrcObj;

        public DoorStateControl(baseClass srcObj)
        {
            m_SrcObj = srcObj;
        }

        public override void Refresh()
        {
            State = DoorState.Unkown;

            var t = DoorStateIndexTuples.FirstOrDefault(f => m_SrcObj.BoolList[f.Item1]);
            if (t != null)
            {
                State = t.Item2;
            }


            base.Refresh();
        }

        public override void OnDraw(Graphics g)
        {
            if (State != DoorState.Unkown)
            {
                g.DrawImage(StateImageDictionary[State], OutLineRectangle);
            }
        }
    }
}