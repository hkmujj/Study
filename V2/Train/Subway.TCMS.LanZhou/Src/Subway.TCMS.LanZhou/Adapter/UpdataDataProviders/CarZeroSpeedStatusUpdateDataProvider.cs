﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.LineInformation;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarZeroSpeedStatusUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarZeroSpeedStatusUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Where(w => w.LineInformationDatas != null).Select(s => s.LineInformationDatas.Value.ZeroSpeedStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.ZeroSpeedNormal));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private BranchInformationStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<BranchInformationStatus, ZeroSpeedStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.ZeroSpeedLineInf))
            {
                return BranchInformationStatus.Branch;
            }
            return BranchInformationStatus.Normal;
        }
    }
}