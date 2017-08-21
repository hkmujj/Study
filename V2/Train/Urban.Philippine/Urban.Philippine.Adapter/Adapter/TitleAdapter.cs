using System;
using System.ComponentModel.Composition;
using System.Threading;
using MMI.Facility.Interface.Data;
using Urban.Philippine.Adapter.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Adapter
{
    [Export(typeof(ITitleAdapter))]
    public class TitleAdapter : ModelAdapterBase, ITitleAdapter
    {
        public ITitleViewModel Title { get; private set; }
        private Timer m_Timer;

        public TitleAdapter()
        {
            m_Timer = new Timer(state =>
            {
                if (Title == null)
                {
                    Title = Adapter.Model.Title;
                }
                Title.CurrentDateTime = DateTime.Now;
            }, null, 10000, 1000);
        }

        public override void DataChanged(CommunicationDataChangedArgs<float> args)
        {
            base.DataChanged(args);
            if (Title == null)
            {
                Title = Adapter.Model.Title;
            }
            var index = IndexConfigure.IntFloatIndex[InFloatKeys.NetVoltage];
            if (args.NewValue.ContainsKey(index))
            {
                Title.NetVoltage = args.NewValue[index];
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.NetCurrent];
            if (args.NewValue.ContainsKey(index))
            {
                Title.NetCurrent = args.NewValue[index];
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.BatteryVoltage];
            if (args.NewValue.ContainsKey(index))
            {
                Title.Battery = args.NewValue[index];
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.Level];
            if (args.NewValue.ContainsKey(index))
            {
                Title.Level = args.NewValue[index].ConvertToLevel();
            }
            index = IndexConfigure.IntFloatIndex[InFloatKeys.Speed];
            if (args.NewValue.ContainsKey(index))
            {
                Title.Speed = args.NewValue[index];
            }
        }
    }
}