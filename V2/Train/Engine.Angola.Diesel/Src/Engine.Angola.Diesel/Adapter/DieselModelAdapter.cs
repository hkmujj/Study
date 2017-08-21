using System.ComponentModel.Composition;
using System.Diagnostics;
using Engine.Angola.Diesel.Extension;
using Engine.Angola.Diesel.Model;
using Engine.Angola.Diesel.Resource.Keys;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;


namespace Engine.Angola.Diesel.Adapter
{
    [Export]
    public class DieselModelAdapter : IDataListener
    {
        [ImportingConstructor]
        [DebuggerStepThrough]
        public DieselModelAdapter(AngolaDieselShellModel shellModel)
        {
            ShellModel = shellModel;
        }

        public AngolaDieselShellModel ShellModel { private set; get; }

        public void Initalize()
        {
            GlobalParam.Instance.InitParam.RegistDataListener(this);
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs args)
        {
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb触发柴油机故障指示, b => ShellModel.IndicatorModel.State00 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb系统电压低, b => ShellModel.IndicatorModel.State01 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb柴油机超速, b => ShellModel.IndicatorModel.State02 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb燃油滤清器压力差过大, b => ShellModel.IndicatorModel.State03 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb燃油压力低, b => ShellModel.IndicatorModel.State04 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb机油压力低, b => ShellModel.IndicatorModel.State10 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb水温高, b => ShellModel.IndicatorModel.State11 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb水温低, b => ShellModel.IndicatorModel.State12 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb冷却器温度高, b => ShellModel.IndicatorModel.State13 = b);
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb空气温度高, b => ShellModel.IndicatorModel.State14 = b);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf机油压力表, f => ShellModel.DialModel.Dial1 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf水温表, f => ShellModel.DialModel.Dial2 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf系统电压表, f => ShellModel.DialModel.Dial3 = f);
            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf机油压力表, f => ShellModel.DialModel.Dial4 = f);

            args.ChangedFloats.UpdateIfContains(InFloatKeys.Inf柴油机转速, f => ShellModel.IndicatorModel.LedData = f);
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }
    }
}