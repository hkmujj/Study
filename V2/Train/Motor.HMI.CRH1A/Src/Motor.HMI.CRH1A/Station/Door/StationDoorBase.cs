using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Station.Door
{
    class StationDoorBase : CommonInnerControlBase, IStationDoor
    {
        //private int[] m_BoolIndexs;

        public virtual StationDoorState State { get; set; }

        public StationDoorStyle Style { get; set; }

        public StationDoorLocation DoorLocation { get; set; }

        public int CarNo { get; set; }


        public static Size DefaultSize = new Size(30, 20);

        //public int[] BoolIndexs
        //{
        //    set
        //    {
        //        m_BoolIndexs = value;
        //        if (BoolIndexs == null || BoolIndexs.Length != GlobalParam.CarCount * 2)
        //        {
        //            var msg = string.Format("In bools of station doors is null or the count is not {0}", GlobalParam.CarCount * 2);
        //            LogMgr.Fatal(msg);
        //            throw new ArgumentException(msg);
        //        }
        //    }
        //    protected get { return m_BoolIndexs; }
        //}
    }
}
