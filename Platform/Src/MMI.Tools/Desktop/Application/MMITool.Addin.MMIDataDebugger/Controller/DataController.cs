using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIDataDebugger.Config.Model;
using MMITool.Addin.MMIDataDebugger.Model;
using MMITool.Addin.MMIDataDebugger.Model.Base;
using MMITool.Addin.MMIDataDebugger.ViewModel;

namespace MMITool.Addin.MMIDataDebugger.Controller
{
    [Export]
    public class DataController : ControllerBase<DataViewModel>
    {
        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        private IApplicationService m_ApplicationService;

        [DebuggerStepThrough]
        [ImportingConstructor]
        public DataController(IApplicationService applicationService)
        {
            m_ApplicationService = applicationService;
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
        }

        private void OnLoaded(CommandParameter commandParameter)
        {
            ThreadPool.QueueUserWorkItem(state => ViewModel.Initalize());
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            GlobalParam.Instance.Initalize();

            var config = GlobalParam.Instance.DataDebuggerConfig.Value;
            var indata = config.DataBufferConfig.InData;
            var outdata = config.DataBufferConfig.OutData;

            ViewModel.Model.DataCollection = new DataCollection
            {
                InData =
                {
                    BoolItems = new Lazy<ObservableCollection<BoolItem>>(() =>
                    {
                        return
                            new ObservableCollection<BoolItem>(
                                Enumerable.Range(indata.BoolStart, indata.BoolEnd)
                                    .Select(s => new BoolItem(GetDescription(AppIndexType.InBool, s), s))
                                    .ToList());
                    }),
                    FloatItems = new Lazy<ObservableCollection<FloatItem>>(
                        () =>
                            new ObservableCollection<FloatItem>(
                                Enumerable.Range(indata.FloatStart, indata.FloatEnd)
                                    .Select(s => new FloatItem(GetDescription(AppIndexType.InFloat, s), s))
                                    .ToList()))
                },
                OutData =
                {
                    BoolItems = new Lazy<ObservableCollection<BoolItem>>(() =>
                    {
                        return
                            new ObservableCollection<BoolItem>(
                                Enumerable.Range(outdata.BoolStart, outdata.BoolEnd)
                                    .Select(s => new BoolItem(GetDescription(AppIndexType.OutBool, s), s))
                                    .ToList());
                    }),
                    FloatItems =
                        new Lazy<ObservableCollection<FloatItem>>(
                            () =>
                                new ObservableCollection<FloatItem>(
                                    Enumerable.Range(outdata.FloatStart, outdata.FloatEnd)
                                        .Select(s => new FloatItem(GetDescription(AppIndexType.OutFloat, s), s))
                                        .ToList())),
                }
            };
        }

        public string GetDescription(AppIndexType type, int index)
        {
            var dic = GlobalParam.Instance.IndexDescriptionConfigDic.Value;
            if (dic.ContainsKey(type))
            {
                var it = dic[type].FirstOrDefault(f => f.Index == index);
                if (it != null)
                {
                    return it.Name;
                }
            }
            return string.Empty;
        }
    }
}