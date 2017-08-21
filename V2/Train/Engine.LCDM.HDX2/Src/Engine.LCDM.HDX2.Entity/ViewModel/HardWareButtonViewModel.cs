using System;
using System.ComponentModel.Composition;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using CommonUtil.Controls;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HardWareButtonViewModel : NotificationObject
    {
        private readonly DelegateCommand<object> m_BtnDownCommand;
        private readonly DelegateCommand<object> m_BtnUpCommand;

        private readonly IEventAggregator m_EventAggregator;

        public ICommand BtnDownCommand
        {
            get { return m_BtnDownCommand; }
        }

        public ICommand BtnUpCommand
        {
            get { return m_BtnUpCommand; }
        }

        [ImportingConstructor]
        public HardWareButtonViewModel(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            m_BtnDownCommand = new DelegateCommand<object>(btn => OnBtnEvent((HXD2HardwareBtn)btn, MouseState.MouseDown));
            m_BtnUpCommand = new DelegateCommand<object>(btn => OnBtnEvent((HXD2HardwareBtn)btn, MouseState.MouseUp));
        }


        private void OnBtnEvent(HXD2HardwareBtn btn, MouseState state)
        {
            m_EventAggregator.GetEvent<BtnEvent>().Publish(new BtnEventArg(state, btn));
        }
    }
}