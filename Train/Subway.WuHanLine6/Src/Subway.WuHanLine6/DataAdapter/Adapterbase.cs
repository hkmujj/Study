using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Events;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.DataAdapter
{
    /// <summary>
    ///
    /// </summary>

    public class Adapterbase : IModelAdapter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Adapterbase()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            Model = ServiceLocator.Current.GetInstance<IModels>(ClassExportName.WuHanModel) as WuHanModel;
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public virtual void Initialize()
        {
            EventAggregator.GetEvent<BoolChangedEvent>().Subscribe(DataChanged);
            EventAggregator.GetEvent<FloatChangedEvent>().Subscribe(DataChanged);
        }

        /// <summary>
        /// 数据变化
        /// </summary>
        /// <param name="args"></param>
        protected virtual void DataChanged(IDictionary<int, float> args) { }

        /// <summary>
        /// 数据变换
        /// </summary>
        /// <param name="args"></param>
        protected virtual void DataChanged(IDictionary<int, bool> args) { }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        //[Import]
        protected IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Model
        /// </summary>
        protected WuHanModel Model { get; private set; }
    }
}