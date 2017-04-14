using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.YdConfigCreater.Controller.Result.Detail.Formats;
using MMITool.Addin.YdConfigCreater.Events;
using MMITool.Addin.YdConfigCreater.Extension;
using MMITool.Addin.YdConfigCreater.Model.Constant;
using MMITool.Addin.YdConfigCreater.Model.Result.Detail;
using MMITool.Addin.YdConfigCreater.ViewModel.Result;

namespace MMITool.Addin.YdConfigCreater.Controller.Result
{
    [Export]
    public class ResultController : ControllerBase<ResultViewModel>
    {
        private readonly IEventAggregator m_EventAggregator;

        private readonly IApplicationService m_ApplicationService;

        [ImportingConstructor]
        public ResultController(IEventAggregator eventAggregator, IApplicationService applicationService)
        {
            m_EventAggregator = eventAggregator;
            m_ApplicationService = applicationService;
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            m_EventAggregator.GetEvent<CreateResultEvent>()
                .Subscribe(OnCreateResult, ThreadOption.BackgroundThread, true,
                    args => args.State == CreateResultEvent.CreateState.ToStart);
            m_EventAggregator.GetEvent<CopyResultItemFileEvent>().Subscribe(OnCopyResultItemFile);
            ViewModel.Model.ResultItemCollection = new ObservableCollection<ResultItem>(new List<ResultItem>()
            {
                CreateResultItem(ModuleType.MainNode),
                CreateResultItem(ModuleType.Teach),
                CreateResultItem(ModuleType.OCC),
                CreateResultItem(ModuleType.IO),
                CreateResultItem(ModuleType.MMI),
            });

            ViewModel.Model.SelectedResultItem = ViewModel.Model.ResultItemCollection.First();
        }

        private void OnCopyResultItemFile(CopyResultItemFileEvent.Args obj)
        {
            try
            {
                var files = new StringCollection() {obj.ResultItem.FileFullName};
                Clipboard.SetFileDropList(files);
                m_EventAggregator.GetEvent<UpdateFooterTextEvent>().Publish(new UpdateFooterTextEvent.Args("已将文件复制到剪贴板。"));
            }
            catch (Exception e)
            {
                LogMgr.Error("Can not copy file, {0}", e);
                m_EventAggregator.GetEvent<UpdateFooterTextEvent>().Publish(new UpdateFooterTextEvent.Args("复制文件到剪贴板出错。"));
            }
        }

        private ResultItem CreateResultItem(ModuleType type)
        {
            return new ResultItem(type, System.IO.Path.Combine(m_ApplicationService.AppPath, "Outputs", type.ToString(), "YDComm.conf"));
        }

        private void OnCreateResult(CreateResultEvent.Args obj)
        {
            foreach (var it in ViewModel.Model.ResultItemCollection)
            {
                it.Content =
                    obj.ConditionModel.ToString(ConditionFormatFactory.Insntace.GetFormatProvider(it.ModuleType));

                try
                {
                    var dir = System.IO.Path.GetDirectoryName(it.FileFullName);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    File.WriteAllText(it.FileFullName, it.Content);
                }
                catch (Exception e)
                {
                    LogMgr.Error("Can not write file , {0}", e);
                }
            }

            m_EventAggregator.GetEvent<CreateResultEvent>()
                .Publish(new CreateResultEvent.Args(obj.ConditionModel, CreateResultEvent.CreateState.Completed));
        }
    }
}