using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using CommonUtil.Annotations;
using MMI.Facility.Interface.Data;

namespace Motor.ATP.Domain.Interface.UserAction
{
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
    internal abstract class IDriverSelectableContracts : IDriverSelectable
    {
        public IDriverInterface Parent { get; private set; }
        public event EventHandler<DriverSelectedEventArgs> Selected;
        public ReadOnlyCollection<IDriverSelectableItem> SelectableItems { get; private set; }

        public IDriverSelectableItem this[UserActionType actionType]
        {
            get { throw new NotImplementedException(); }
        }

        private IDriverSelectableContracts()
        {
            
        }

        public void RaiseSelectedChanged(DriverSelectedEventArgs args)
        {
            Contract.Requires(args != null);
            Contract.Requires(args.SelectedItem != null);
        }

        public void UpdateStates()
        {
            
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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