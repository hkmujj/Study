using System;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Philippine.Adapter.Interface;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Philippine.Adapter.Adapter
{
    [Export(typeof(IModelAdapter))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ModelAdapter : IModelAdapter
    {
        [ImportingConstructor]
        public ModelAdapter()
        {
            GlobalParam.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged +=
                ModelAdapter_CourseStateChanged;
        }
        [Import]
        public IFaultManager FaultManager { get; private set; }

        [Import]
        public IPhilippineViewModel Model { get; private set; }

        [Import]
        public IMainViewModelAdapter MainView { get; private set; }

        [Import]
        public ITitleAdapter TitleAdapter { get; private set; }

        public void DataChanged(object sender, CommunicationDataChangedArgs args)
        {
            //var stop = Stopwatch.StartNew();
            DataChanged(args.ChangedBools);
            DataChanged(args.ChangedFloats);
            MainView.DataChanged(sender, args);
            TitleAdapter.DataChanged(sender, args);
            Model.VVVF.VVVFViewUnits.Changed(args.ChangedBools.NewValue, GlobalParam.Train);
            Model.VVVF.VVVFViewUnits.Changed(args.ChangedFloats.NewValue, GlobalParam.Train);
            Model.Brake.Brake.Changed(args.ChangedBools.NewValue, GlobalParam.Train);
            Model.Brake.Brake.Changed(args.ChangedFloats.NewValue, GlobalParam.Train);
            Model.TMS.TMS.Changed(args.ChangedBools.NewValue, GlobalParam.Train);
            Model.APS.APS.Changed(args.ChangedBools.NewValue, GlobalParam.Train);
            Model.APS.APS.Changed(args.ChangedFloats.NewValue, GlobalParam.Train);
            Model.VAC.VAC.Changed(args.ChangedBools.NewValue, GlobalParam.Train);
            Model.VAC.VAC.Changed(args.ChangedFloats.NewValue, GlobalParam.Train);
            Model.IOState.IOState.Changed(args.ChangedBools.NewValue);
            Model.IOState.IOState.Changed(args.ChangedFloats.NewValue);


            //AppLog.Info($"花费时间:{stop.ElapsedMilliseconds}毫秒\r\n变化数据量:{args.ChangedBools.NewValue.Keys.Count + args.ChangedFloats.NewValue.Keys.Count}");
        }

        public void DataChanged(CommunicationDataChangedArgs<bool> args)
        {
            var idnex = IndexConfigure.InBoolIndex[InBoolKeys.BlackScreen];
            var value = GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(idnex);
            if (args.NewValue.ContainsKey(idnex))
            {
                if (args.NewValue[idnex] && GlobalParam.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CurrentCourseState == CourseState.Started)
                {
                    Model.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Name = ControlNames.LoginView,
                        Region = RegionNames.MainShellRegion
                    });
                    Model.EventAggregator.GetEvent<ButtonInitEvent>().Publish(new ButtonInitEvenArgs()
                    {
                        ViewName = ControlNames.VACShell
                    });
                }
                if (!args.NewValue[idnex] ||
                         GlobalParam.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CurrentCourseState !=
                         CourseState.Started)
                {
                    Model.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Name = ControlNames.BlackView,
                        Region = RegionNames.MainShellRegion
                    });
                }
                Model.EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
                {
                    Index = IndexConfigure.OutFloatIndex[OutFloatKeys.CoolModel],
                    SendData = args.NewValue[idnex] ? 1 : 0,
                    Type = CmdType.SetFloatValue
                });
                Model.EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
                {
                    Index = IndexConfigure.OutFloatIndex[OutFloatKeys.SetAirConditionTemperature],
                    SendData = args.NewValue[idnex] ? 25 : 0,
                    Type = CmdType.SetFloatValue
                });
            }

            foreach (var key in args.NewValue.Keys.Where(key => FaultManager.AllInfo.ContainsKey(key)))
            {
                if (args.NewValue[key])
                {
                    FaultManager.Add(key);
                }
                else
                {
                    FaultManager.Remove(key);
                }
            }
        }

        public void DataChanged(CommunicationDataChangedArgs<float> args)
        {
        }

        public void Clear()
        {
            Model.Clear();
        }

        public void Clear(object obj, Type type)
        {
        }

        private void ModelAdapter_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            Model.EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
            {
                Name = ControlNames.BlackView,
                Region = RegionNames.MainShellRegion
            });
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            DataChanged(dataChangedArgs);
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            DataChanged(dataChangedArgs);
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            DataChanged(sender, dataChangedArgs);
        }
    }
}