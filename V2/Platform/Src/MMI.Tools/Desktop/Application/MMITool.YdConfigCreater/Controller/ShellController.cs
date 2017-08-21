using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.YdConfigCreater.Events;
using MMITool.Addin.YdConfigCreater.ViewModel;

namespace MMITool.Addin.YdConfigCreater.Controller
{
    [Export]
    public class ShellController : ControllerBase<ShellViewModel>
    {
        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public ShellController(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            m_EventAggregator.GetEvent<CreateResultEvent>().Subscribe(OnCreateResult);
            m_EventAggregator.GetEvent<UpdateFooterTextEvent>().Subscribe(s => ViewModel.Model.FooterText = s.Text);
            ViewModel.Model.ClearFooterCommand = new DelegateCommand<object>(OnClearFooter);
        }

        private void OnClearFooter(object obj)
        {
            ViewModel.Model.FooterText = string.Empty;
        }

        private void OnCreateResult(CreateResultEvent.Args obj)
        {
            switch (obj.State)
            {
                case CreateResultEvent.CreateState.ToStart:
                    ViewModel.Model.FooterText = "开始创建……";
                    break;
                case CreateResultEvent.CreateState.Completed:
                    ViewModel.Model.FooterText = "创建成功完成。";
                    break;
                case CreateResultEvent.CreateState.Fail:
                    ViewModel.Model.FooterText = "创建失败……";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}