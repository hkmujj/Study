using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TPX21F.HXN5B.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateSelfTestItemProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateSelfTestItemProvider(IEventAggregator eventAggregator, HXN5BViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var tst = ViewModel.Domain.TestViewModel.TestSelfViewModel;
            var selected = tst.Model.SelectedItem;

            if (selected != null)
            {
                var itcfg = selected.ItemConfig;
                selected.ShowingContentCollection[0] = GetItemContentStringIfCan(itcfg.ItemTitle1, itcfg.ValueType1,
                    itcfg.ValueStringFormat1, itcfg.Keys1);
                selected.ShowingContentCollection[1] = GetItemContentStringIfCan(itcfg.ItemTitle2, itcfg.ValueType2,
                    itcfg.ValueStringFormat2, itcfg.Keys2);
                selected.ShowingContentCollection[2] = GetItemContentStringIfCan(itcfg.ItemTitle3, itcfg.ValueType3,
                    itcfg.ValueStringFormat3, itcfg.Keys3);
                selected.ShowingContentCollection[3] = GetItemContentStringIfCan(itcfg.ItemTitle4, itcfg.ValueType4,
                    itcfg.ValueStringFormat4, itcfg.Keys4);
                selected.ShowingContentCollection[4] = GetItemContentStringIfCan(itcfg.ItemTitle5, itcfg.ValueType5,
                    itcfg.ValueStringFormat5, itcfg.Keys5);
                selected.ShowingContentCollection[5] = GetItemContentStringIfCan(itcfg.ItemTitle6, itcfg.ValueType6,
                    itcfg.ValueStringFormat6, itcfg.Keys6);
                selected.ShowingContentCollection[6] = GetItemContentStringIfCan(itcfg.ItemTitle7, itcfg.ValueType7,
                    itcfg.ValueStringFormat7, itcfg.Keys7);
                selected.ShowingContentCollection[7] = GetItemContentStringIfCan(itcfg.ItemTitle8, itcfg.ValueType8,
                    itcfg.ValueStringFormat8, itcfg.Keys8);
                selected.ShowingContentCollection[8] = GetItemContentStringIfCan(itcfg.ItemTitle9, itcfg.ValueType9,
                    itcfg.ValueStringFormat9, itcfg.Keys9);
                selected.ShowingContentCollection[9] = GetItemContentStringIfCan(itcfg.ItemTitle10, itcfg.ValueType10,
                    itcfg.ValueStringFormat10, itcfg.Keys10);
                selected.ShowingContentCollection[10] = GetItemContentStringIfCan(itcfg.ItemTitle11, itcfg.ValueType11,
                    itcfg.ValueStringFormat11, itcfg.Keys11);
                selected.ShowingContentCollection[11] = GetItemContentStringIfCan(itcfg.ItemTitle12, itcfg.ValueType12,
                    itcfg.ValueStringFormat12, itcfg.Keys12);
                selected.ShowingContentCollection[12] = GetItemContentStringIfCan(itcfg.ItemTitle13, itcfg.ValueType13,
                    itcfg.ValueStringFormat13, itcfg.Keys13);
                selected.ShowingContentCollection[13] = GetItemContentStringIfCan(itcfg.ItemTitle14, itcfg.ValueType14,
                    itcfg.ValueStringFormat14, itcfg.Keys14);
                selected.ShowingContentCollection[14] = GetItemContentStringIfCan(itcfg.ItemTitle15, itcfg.ValueType15,
                    itcfg.ValueStringFormat15, itcfg.Keys15);
            }
        }

        private string GetItemContentStringIfCan(string title, string valueType, string valueStringFormat,
            List<string> keys)
        {
            if (!string.IsNullOrWhiteSpace(title) && keys != null &&
                keys.Any())
            {
                return GetItemContentString(valueType, valueStringFormat, keys);
            }

            return string.Empty;
        }


        private string GetItemContentString(string valueType, string valueStringFormat, List<string> keys)
        {
            if (valueType == typeof(SelfTestItemConnectState).Name)
            {
                if (DataService.GetInBoolOf(keys[0]))
                {
                    return "连通";
                }

                return string.Empty;
            }

            if (valueType == typeof(SelfTestItemOpenState).Name)
            {
                if (DataService.GetInBoolOf(keys[0]))
                {
                    return "打开";
                }

                return "关闭";
            }

            if (string.IsNullOrWhiteSpace(valueType) || valueType == typeof(float).Name ||
                valueType == typeof(float).FullName)
            {
                if (!string.IsNullOrWhiteSpace(keys[0]))
                {
                    return DataService.GetInFloatOf(keys[0]).ToString(valueStringFormat);
                }
            }

            return string.Empty;
        }
    }
}