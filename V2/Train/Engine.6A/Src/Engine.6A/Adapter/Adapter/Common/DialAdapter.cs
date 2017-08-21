using System;

namespace Engine._6A.Adapter.Adapter.Common
{
    public class DialAdapter : ModelAdapterBase
    {
        public override void Heart()
        {
            base.Heart();
            if (DateTime.Now.Hour > 12)
            {
                Adapter.Model.Dial.HourAngle = ((double)(DateTime.Now.Hour - 12) * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) / (12d * 3600) * 360;
            }
            else
            {
                Adapter.Model.Dial.HourAngle = ((double)(DateTime.Now.Hour) * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second) / (12d * 3600) * 360;
            }
            Adapter.Model.Dial.MiuAngle = ((double)(DateTime.Now.Minute)) / (60d) * 360;
            Adapter.Model.Dial.SecAngle = ((double)(DateTime.Now.Second)) / (60d) * 360;

        }

        public DialAdapter(IEngineAdapter adapter)
            : base(adapter)
        {

        }
    }
}