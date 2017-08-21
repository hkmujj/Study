using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX.Detail;
using Engine.TAX2.SS7C.ViewModel.Domain.TAX2Info;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.TAX2
{
    [Export]
    public class CheckWalkContrller : ControllerBase<CheckWalkViewModel>
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.AixeTemperItemCollection =
                new Lazy<ReadOnlyCollection<AixeTemperItem>>(
                    () =>
                        new ReadOnlyCollection<AixeTemperItem>(
                            Enumerable.Range(0, 6).Select(s => new AixeTemperItem()).ToList()));
        }
    }
}