using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models;
using Subway.WuHanLine6.Models.Model;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    /// 武汉数据适配层
    /// </summary>
    [Export]
    public class WuHanDadaAdapter : IModelAdapter, IDataListener
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">导入 导出名称为<seealso cref="ClassExportName.WuHanModel"/> 的 实现接口IModels的类</param>
        /// <param name="eventAggregator">导入事件聚合器</param>
        [ImportingConstructor]
        public WuHanDadaAdapter(IEventAggregator eventAggregator, [Import(ClassExportName.WuHanModel)] IModels model)
        {
            EventAggregator = eventAggregator;
            Model = model as WuHanModel;
            eventAggregator.GetEvent<BoolChangedEvent>().Subscribe(ResponseBoolValue);
        }

        private void ResponseBoolValue(IDictionary<int, bool> obj)
        {
            var index = GlobalParams.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.InBo黑屏司机室占用];
            if (obj.ContainsKey(index))
            {
                Model.MMIBlack = obj[index] ? Visibility.Visible : Visibility.Hidden;
                if (obj[index])
                {
                    EventAggregator.GetEvent<NavigatorKeyEvent>().Publish(StateInterfaceKeys.运行);
                }
            }
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void Initialize()
        {
            GlobalParams.Instance.InitParam.RegistDataListener(this);
            GlobalParams.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += WuHanDadaAdapter_CourseStateChanged;
            GlobalParams.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().ChangeState(CourseState.Started);
        }

        private void WuHanDadaAdapter_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            switch (e.CourseService.CurrentCourseState == CourseState.Started)
            {
                case true:
                    EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.MainShell);
                    break;

                default:
                    EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.BalckView);
                    break;
            }
        }

        /// <summary>
        /// Model
        /// </summary>
        public WuHanModel Model { get; private set; }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            EventAggregator.GetEvent<BoolChangedEvent>().Publish(dataChangedArgs.NewValue);
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            EventAggregator.GetEvent<FloatChangedEvent>().Publish(dataChangedArgs.NewValue);
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}