using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using CommonUtil.Annotations;
using MMI.Facility.Interface.Data;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    [ContractClass(typeof(IDriverSelectableContracts))]
    public interface IDriverSelectable : INotifyPropertyChanged
    {
        /// <summary>
        /// 父结点
        /// </summary>
        IDriverInterface Parent { get; }

        /// <summary>
        /// 选择了一个功能键
        /// </summary>
        event EventHandler<DriverSelectedEventArgs> Selected;

        /// <summary>
        /// 可供选择的集合
        /// </summary>
        ReadOnlyCollection<IDriverSelectableItem> SelectableItems { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        IDriverSelectableItem this[UserActionType actionType] { get; }

        /// <summary>
        /// 由硬件产生的按键状态变化调用。
        /// </summary>
        /// <param name="args"></param>
        void RaiseSelectedChanged(DriverSelectedEventArgs args);

        /// <summary>
        /// 手动更新状态
        /// </summary>
        void UpdateStates();
    }

    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// IDriverSelectable 接口契约
    /// </summary>
    [ContractClassFor(typeof(IDriverSelectable))]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    internal abstract class IDriverSelectableContracts : IDriverSelectable
    {
        /// <summary>
        /// 父结点
        /// </summary>
        public IDriverInterface Parent { get; private set; }

        /// <summary>
        /// 选择了一个功能键
        /// </summary>
        public event EventHandler<DriverSelectedEventArgs> Selected;

        /// <summary>
        /// 可供选择的集合
        /// </summary>
        public ReadOnlyCollection<IDriverSelectableItem> SelectableItems { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        public IDriverSelectableItem this[UserActionType actionType]
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        private IDriverSelectableContracts()
        {
            
        }

        /// <summary>
        /// 由硬件产生的按键状态变化调用。
        /// </summary>
        /// <param name="args"></param>
        public void RaiseSelectedChanged(DriverSelectedEventArgs args)
        {
            Contract.Requires(args != null);
            Contract.Requires(args.SelectedItem != null);
        }

        /// <summary>
        /// 手动更新状态
        /// </summary>
        public void UpdateStates()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>在更改属性值时发生。</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelected(DriverSelectedEventArgs e)
        {
            var handler = Selected;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}