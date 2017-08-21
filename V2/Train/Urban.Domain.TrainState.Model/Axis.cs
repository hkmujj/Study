using System.Collections.ObjectModel;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    public class Axis : UpdatingProvider<Axis>, IAxis
    {
        public bool IsFault { get; set; }

        public string FaultInfo { get; set; }

        public object Identity { get; set; }

        /// <summary>
        /// 轴距
        /// </summary>
        public double Wheelbase { get; set; }

        public ReadOnlyCollection<IWheel> Wheels { get; set; }

        IBraking IAxis.Braking { get { return Braking; } }
        public Braking Braking { set; get; }

        public override void Update(IUpdateParam updateParam)
        {
            base.Update(updateParam);

            Braking.Update(updateParam);
        }
    }
}