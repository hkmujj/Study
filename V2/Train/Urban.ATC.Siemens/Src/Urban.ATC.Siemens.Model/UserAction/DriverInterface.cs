using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Model.UserAction
{
    [DebuggerDisplay("Id = {Id}")]
    public class DriverInterface : NotificationObject, IDriverInterface
    {
        private IDriverSelectable m_DriverSelectable;

        public DriverInterface(DriverInterfaceKey id, IDriverSelectable driverSelectable, IATP atp, IDriverPopupView popupView = null)
        {
            Id = id;
            PopupView = popupView;
            DriverSelectable = driverSelectable;
            ATP = atp;
            ChirldrenCollection = new List<IDriverInterface>();
            Chirldren = new ReadOnlyCollection<IDriverInterface>(ChirldrenCollection);
            LastInterfaceStack = new Stack<IDriverInterface>();
        }

        public IATP ATP { get; private set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public IDriverInterface Parent { get; protected set; }

        protected List<IDriverInterface> ChirldrenCollection { private set; get; }

        /// <summary>
        /// 子结点
        /// </summary>
        public ReadOnlyCollection<IDriverInterface> Chirldren { get; private set; }

        /// <summary>
        /// ID
        /// </summary>
        public DriverInterfaceKey Id { get; private set; }

        /// <summary>
        /// 弹出框
        /// </summary>
        public IDriverPopupView PopupView { get; private set; }

        /// <summary>
        /// 司机的选择信息
        /// </summary>
        public IDriverSelectable DriverSelectable
        {
            get { return m_DriverSelectable; }
            private set
            {
                if (Equals(value, m_DriverSelectable))
                {
                    return;
                }
                if (m_DriverSelectable != null)
                {
                    m_DriverSelectable.PropertyChanged -= DriverSelectableOnPropertyChanged;
                }
                m_DriverSelectable = value;

                RaisePropertyChanged(() => DriverSelectable);

                if (m_DriverSelectable != null)
                {
                    m_DriverSelectable.PropertyChanged += DriverSelectableOnPropertyChanged;
                }
            }
        }

        private void DriverSelectableOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged(PropertySupport.ExtractPropertyName(() => DriverSelectable) + "." + propertyChangedEventArgs.PropertyName);
        }

        /// <summary>
        /// 上一个接口
        /// </summary>
        public Stack<IDriverInterface> LastInterfaceStack { get; private set; }

        public void SetParent(IDriverInterface driverInterface)
        {
            Contract.Requires(driverInterface != null);
            DriverInterface concreate;
            if (Parent != null)
            {
                concreate = Parent as DriverInterface;
                if (concreate != null)
                {
                    concreate.ChirldrenCollection.Remove(this);
                }
            }

            Parent = driverInterface;

            concreate = Parent as DriverInterface;
            if (concreate != null)
            {
                concreate.ChirldrenCollection.Add(this);
            }
        }

        /// <summary>
        /// 从 lastInterface 跳转进来
        /// </summary>
        /// <param name="lastInterface"></param>
        /// <returns></returns>
        public virtual bool NavigateToThis(IDriverInterface lastInterface)
        {
            // 为0是第一次进入， lastInterface.LastInterfaceStack.Peek() != this 避免循环返回。
            if (LastInterfaceStack.Count == 0 || lastInterface.LastInterfaceStack.Peek() != this)
            {
                LastInterfaceStack.Push(lastInterface);
            }

            if (PopupView != null)
            {
                PopupView.UpdateState(lastInterface, this);
                PopupView.ResponseAction(this);
            }

            return true;
        }

        /// <summary>
        /// 从这里跳转出去
        /// </summary>
        public virtual bool NavigateFromThis()
        {
            if (PopupView != null)
            {
                PopupView.FinishResponseAction(this);
            }
            return true;
        }
    }
}