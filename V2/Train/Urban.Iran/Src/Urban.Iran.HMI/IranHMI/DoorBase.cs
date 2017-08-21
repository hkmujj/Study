using System;
using System.Collections.Generic;
using System.Drawing;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Model;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Config.ConfigModel;

namespace Urban.Iran.HMI
{
    public class DoorBase : Door
    {
        public Dictionary<int, int> IndexDictionary { get; set; }
        public string CarDoorName { get; private set; }
        public DoorBase(HMIBase baseClass, DoorListening door)
        {
            CarDoorName = door.CarName + door.DoorName;
       
            ImagesDictionary = new Dictionary<DoorState, Image>
                               {
                                   { DoorState.Close, new Bitmap(baseClass.RecPath + "\\frame/drClose.jpg") },
                                   { DoorState.Open, new Bitmap(baseClass.RecPath + "\\frame/drOpen.jpg") },
                                   { DoorState.CutOut, new Bitmap(baseClass.RecPath + "\\frame/drCutout.jpg") },
                                   { DoorState.Fault, new Bitmap(baseClass.RecPath + "\\frame/drFaulty.jpg") },
                                   { DoorState.EmergencyUnlock, new Bitmap(baseClass.RecPath + "\\frame/drEmergencyUnlock.jpg") },
                                   { DoorState.Lock, new Bitmap(baseClass.RecPath + "\\frame/drClose.jpg") },
                                   { DoorState.Unlock, new Bitmap(baseClass.RecPath + "\\frame/drClose.jpg") },
                                   { DoorState.Unkown, new Bitmap(baseClass.RecPath + "\\frame/drClose.jpg") }
                               };
            IndexDictionary = new Dictionary<int, int>();
        }

        public void OnDraw(Graphics g)
        {
            g.DrawImage(ImagesDictionary[DoorState], Trainside ? OutRectangleTure : OutRectangleFalse);
        }

        public void OnPaint(Graphics g)
        {
            OnRefreshAction(this);
            g.DrawImage(ImagesDictionary[DoorState], Trainside ? OutRectangleTure : OutRectangleFalse);
        }

        public event Action<object> RefreshAction;

        protected virtual void OnRefreshAction(object obj)
        {
            Action<object> handler = RefreshAction;
            if (handler != null)
            {
                handler(obj);
            }
        }

        public Rectangle OutRectangleFalse { get; set; }
        public Rectangle OutRectangleTure { get; set; }
    }
}
