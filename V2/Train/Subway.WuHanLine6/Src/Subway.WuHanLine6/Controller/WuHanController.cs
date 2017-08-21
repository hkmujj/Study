using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Attributes;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.FaultInfos;
using Subway.WuHanLine6.Models.BtnModel;
using Subway.WuHanLine6.Resource.Keys;
using Subway.WuHanLine6.ViewModels;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 武汉ViewModel控制类
    /// </summary>
    [Export(ClassExportName.WuHanControler, typeof(WuHanController))]
    public class WuHanController : ControllerBase<WuHanViewModel>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regionManager">区域管理器</param>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="stateInterfaceFactory"></param>
        [ImportingConstructor]
        public WuHanController(IRegionManager regionManager, IEventAggregator eventAggregator, IStateInterfaceFactory stateInterfaceFactory)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            StateInterfaceFactory = stateInterfaceFactory;
            eventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorResponse, ThreadOption.UIThread);
            eventAggregator.GetEvent<NavigatorKeyEvent>().Subscribe(Navigator);
            Load = new DelegateCommand(OnLoad);
            NavigatorCommand = new DelegateCommand<string>(Navigator);
            NavigatroPassword = new DelegateCommand<NavigatorArgs>((NavigatorPassword));
        }

        private void NavigatorPassword(NavigatorArgs navigatorArgs)
        {
            if (navigatorArgs != null)
            {
                Navigator(navigatorArgs.Navigator);
                ViewModel.Model.PasswordModel.BackView = navigatorArgs.Current;
                ViewModel.Model.PasswordModel.ConfirmView = navigatorArgs.NextNavigator;
            }
        }

        private void OnLoad()
        {
            Navigator(StateInterfaceKeys.运行);
        }

        /// <summary>
        /// 导航到对应的Key
        /// </summary>
        /// <param name="key">Key</param>
        public void Navigator(string key)
        {
            var stateKey = new StateKeys(key);
            Navigator(stateKey);
        }

        private void NavigatorResponse(string fullName)
        {
            var type = Type.GetType(fullName);
            if (type != null)
            {
                var parent = type.GetCustomAttributes(typeof(ParentViewAttribute), false).FirstOrDefault() as ParentViewAttribute;
                NavigatorToParent(parent);

                var borther = type.GetCustomAttributes(typeof(BrotherViewAttribute), false).Cast<BrotherViewAttribute>();

                NavigatorToBrother(borther);

                NavigatorToMe(fullName);
            }
        }

        private void NavigatorToMe(string fullName)
        {
            var type = Type.GetType(fullName);
            if (type != null)
            {
                var att = type.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute;
                if (att != null)
                {
                    RegionManager.RequestNavigate(att.RegionName, fullName);
                }
            }
        }

        private void NavigatorToBrother(IEnumerable<BrotherViewAttribute> borth)
        {
            borth.ToList().ForEach(f => NavigatorToMe(f.BrotherView.FullName));
        }

        private void NavigatorToParent(ParentViewAttribute att)
        {
            if (att != null)
            {
                NavigatorResponse(att.ParentView.FullName);
            }
        }

        private void Navigator(StateKeys keys)
        {
            ViewModel.Model.UpdateCurrent(StateInterfaceFactory.GetOrCreat(keys));
            UpdateView(keys);
        }

        private void UpdateView(StateKeys keys)
        {
            if (keys.Key.Equals(StateInterfaceKeys.实时故障))
            {
                EventAggregator.GetEvent<FaultViewChangedEvent>().Publish(new FaultViewChangedArgs()
                {
                    Level = FaultLevel.Level12,
                    Current = FaultCurrent.Current,
                });
            }
            else if (keys.Key.Equals(StateInterfaceKeys.故障记录))
            {
                EventAggregator.GetEvent<FaultViewChangedEvent>().Publish(new FaultViewChangedArgs()
                {
                    Level = FaultLevel.Level12,
                    Current = FaultCurrent.Histort,
                });
            }
            else if (keys.Key.Equals(StateInterfaceKeys.今日故障))
            {
                EventAggregator.GetEvent<FaultViewChangedEvent>().Publish(new FaultViewChangedArgs()
                {
                    Level = FaultLevel.Level12,
                    Current = FaultCurrent.Day,
                });
            }
        }

        /// <summary>
        /// StateInterfaceFactory
        /// </summary>
        public IStateInterfaceFactory StateInterfaceFactory { get; private set; }

        /// <summary>
        /// 区域管理器
        /// </summary>
        protected IRegionManager RegionManager { get; private set; }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// MouseDown
        /// </summary>
        public ICommand Load { get; private set; }

        /// <summary>
        /// 导航命令
        /// </summary>
        public ICommand NavigatorCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand NavigatroPassword { get; private set; }
    }
}