using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Adapter.UpdateDataProviders
{
    [Export(typeof (IUpdateDataProvider))]
    public class CarPECUUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarPECUUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {

            foreach (var car in ViewModel.TrainViewModel.Model.CarCollection)
            {
                for (int i = 0; i < car.PECU.PECUItems.Count; ++i)
                {
                    var it = car.PECU.PECUItems[i];

                    it.State = GetPECUState(args, it.ItemConfig, i);

                }
            }
        }

        private PECUState GetPECUState(CommunicationDataChangedArgs args, CarPECUConfig it, int index)
        {
            if (DataService.ReadService.GetInBoolOf(GetActiveIndexName(it, index)))
            {
                return PECUState.Active;
            }
            if (DataService.ReadService.GetInBoolOf(GetUsingIndexName(it, index)))
            {
                return PECUState.Using;
            }
            return PECUState.Unactive;
        }
        private string GetActiveIndexName(CarPECUConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.PECU1IndexActive;
                case 1:
                    return it.PECU2IndexActive;
                case 2:
                    return it.PECU3IndexActive;
                case 3:
                    return it.PECU4IndexActive;
                case 4:
                    return it.PECU5IndexActive;
                case 5:
                    return it.PECU6IndexActive;
                case 6:
                    return it.PECU7IndexActive;
                case 7:
                    return it.PECU8IndexActive;

            }

            return String.Empty;
        }
        private string GetUsingIndexName(CarPECUConfig it, int index)
        {
            switch (index)
            {
                case 0:
                    return it.PECU1IndexUsing;
                case 1:
                    return it.PECU2IndexUsing;
                case 2:
                    return it.PECU3IndexUsing;
                case 3:
                    return it.PECU4IndexUsing;
                case 4:
                    return it.PECU5IndexUsing;
                case 5:
                    return it.PECU6IndexUsing;
                case 6:
                    return it.PECU7IndexUsing;
                case 7:
                    return it.PECU8IndexUsing;

            }
            return String.Empty;
        }

        private PECUState GetPECUState(CommunicationDataChangedArgs args, CarPECUConfig it)
        {
            if (DataService.ReadService.GetInBoolOf(it.PECU1IndexActive))
            {
                return PECUState.Active;
            }
            if (DataService.ReadService.GetInBoolOf(it.PECU1IndexUsing))
            {
                return PECUState.Using;
            }
            return PECUState.Unactive;
        }
    }
}