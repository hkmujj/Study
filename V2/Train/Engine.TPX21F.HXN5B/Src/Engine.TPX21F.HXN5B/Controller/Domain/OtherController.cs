using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Threading;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Other.Detail;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class OtherController : ControllerBase<OtherViewModel>, IResetSupport
    {
        private DispatcherTimer m_UpdateCurrentTimeTimer;

        [ImportingConstructor]
        public OtherController()
        {
        }

        public override void Initalize()
        {
            GlobalTimer.Instance.Tick500MS += OnUpdateCurrentTime;

            InitalizeAccumulative();
        }

        private void InitalizeAccumulative()
        {
            ViewModel.Model.AccumulativeDataModel.PowerCollection =
                new Lazy<ReadOnlyCollection<AccumulativeDataItem>>(
                    () =>
                        GlobalParam.Instance.AccumulativeDataConfig.Value.PowerCollection.Select(
                            s => new AccumulativeDataItem(s)).ToList().AsReadOnly());

            ViewModel.Model.AccumulativeDataModel.TimeCollection =
                new Lazy<ReadOnlyCollection<AccumulativeDataItem>>(
                    () =>
                        GlobalParam.Instance.AccumulativeDataConfig.Value.TimeCollection.Select(
                            s => new AccumulativeDataItem(s)).ToList().AsReadOnly());

            ViewModel.Model.AccumulativeDataModel.TowCollection =
                new Lazy<ReadOnlyCollection<AccumulativeDataItem>>(
                    () =>
                        GlobalParam.Instance.AccumulativeDataConfig.Value.TowCollection.Select(
                            s => new AccumulativeDataItem(s)).ToList().AsReadOnly());

            ViewModel.Model.AccumulativeDataModel.BrakeCollection =
                new Lazy<ReadOnlyCollection<AccumulativeDataItem>>(
                    () =>
                        GlobalParam.Instance.AccumulativeDataConfig.Value.BrakeCollection.Select(
                            s => new AccumulativeDataItem(s)).ToList().AsReadOnly());
        }

        private void OnUpdateCurrentTime(object sender, EventArgs eventArgs)
        {
            ViewModel.Model.SimTime = DateTime.Now;
        }

        public void Reset()
        {
            ViewModel.Model.AdjustSpan = TimeSpan.Zero;
        }
    }
}