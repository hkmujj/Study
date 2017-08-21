using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using MMI.Facility.Interface.Data;
using Urban.Philippine.Adapter.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Adapter
{
    [Export(typeof(IMainViewModelAdapter))]
    public class MainViewModelAdapter : ModelAdapterBase, IMainViewModelAdapter
    {
        public override void DataChanged(CommunicationDataChangedArgs<bool> args)
        {
            base.DataChanged(args);
            if (MainViewModel == null)
            {
                MainViewModel = Adapter.Model.MainViewModel;
            }
            MainViewModel.Changed(args.NewValue, GlobalParam.Train);
            var index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainRunLeft];
            var index2 = IndexConfigure.InBoolIndex[InBoolKeys.TrainRunRight];
            if (args.NewValue.Keys.Any(a => a == index1 || a == index2))
            {
                MainViewModel.Dirction = null;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    MainViewModel.Dirction = Dirction.Left;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    MainViewModel.Dirction = Dirction.Roght;
                }
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainNoVisibility];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.TarinNoVisibility = args.NewValue[index1].ConvertVisibility();
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.MaintainerVisibility];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.MaintainerVisibility = args.NewValue[index1].ConvertVisibility();
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.MasterNoVisibility];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.MasterVisibility = args.NewValue[index1].ConvertVisibility();
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.ATPModelVisibility];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.AtpModelVisibility = args.NewValue[index1].ConvertVisibility();
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.ATPMode];
            if (args.NewValue.ContainsKey(index1))
            {
                if (args.NewValue[index1].Equals(true))
                {
                    MainViewModel.AtpModel = AtpModel.AtpMode;
                }
                
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.EMERGENCYMode];
            if (args.NewValue.ContainsKey(index1))
            {
                if (args.NewValue[index1].Equals(true))
                {
                    MainViewModel.AtpModel = AtpModel.Emergency;
                }
               
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.MANUALForwardMode];
            if (args.NewValue.ContainsKey(index1))
            {
                if (args.NewValue[index1].Equals(true))
                {
                    MainViewModel.AtpModel = AtpModel.ManualForward;
                }

            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.MANUALBackwardMode];
            if (args.NewValue.ContainsKey(index1))
            {
                if (args.NewValue[index1].Equals(true))
                {
                    MainViewModel.AtpModel = AtpModel.ManualBackward;
                }

            }

            
            SetMainPageValue(args);
        }

        private void SetMainPageValue(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainTractionAllow];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.TractionAloow = args.NewValue[index1];
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainSandBoxLow];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.SandBoxLow = args.NewValue[index1];
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainSanding];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.Sanding = args.NewValue[index1];
            }
            index1 = IndexConfigure.InBoolIndex[InBoolKeys.TrainSliding];
            if (args.NewValue.ContainsKey(index1))
            {
                MainViewModel.Sliding = args.NewValue[index1];
            }

        }


        public override void DataChanged(CommunicationDataChangedArgs<float> args)
        {
            base.DataChanged(args);
            if (MainViewModel == null)
            {
                MainViewModel = Adapter.Model.MainViewModel;
            }
            MainViewModel.Changed(args.NewValue, GlobalParam.Train);
            var index = IndexConfigure.IntFloatIndex[InFloatKeys.TrainNumber];
            if (args.NewValue.ContainsKey(index))
            {
                //MainViewModel.TrainNumber = $"Vehicie No.{args.NewValue[index].ToString("F0")}";
                MainViewModel.TrainNumber =  string.Format("Vehicie No.{0}",args.NewValue[index].ToString("F0"));
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.LimitSpeed];
            if (args.NewValue.ContainsKey(index))
            {
                MainViewModel.LimitSpeed = args.NewValue[index];
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.Maintainer];
            if (args.NewValue.ContainsKey(index))
            {
                //MainViewModel.Maintainer = $"Maintainer {args.NewValue[index].ToString("F0").PadLeft(4, '0')}";
                MainViewModel.Maintainer = string.Format("Maintainer {0}",args.NewValue[index].ToString("F0").PadLeft(4, '0'));
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.MasterNumber];
            if (args.NewValue.ContainsKey(index))
            {
                //MainViewModel.MasterNumber = $"Master No.{args.NewValue[index].ToString("F0")}";
                MainViewModel.MasterNumber = string.Format("Master No.{0}",args.NewValue[index].ToString("F0"));
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.ATPModel];
            if (args.NewValue.ContainsKey(index))
            {
                MainViewModel.AtpModel = (AtpModel)((int)args.NewValue[index]);
            }
        }

        public IMainViewModel MainViewModel { get; private set; }
    }
}