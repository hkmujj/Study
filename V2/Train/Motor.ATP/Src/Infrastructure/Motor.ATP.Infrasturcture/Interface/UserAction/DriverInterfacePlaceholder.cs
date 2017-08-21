using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 空白的 IDriverInterface
    /// </summary>
    [DebuggerDisplay("Id = {Id}")]
    public sealed class DriverInterfacePlaceholder : NotificationObject, IDriverInterface
    {
        /// <summary>
        /// 所属ATP
        /// </summary>
        public IATP ATP { get; private set; }

        /// <summary>
        /// 父结点
        /// </summary>
        public IDriverInterface Parent { get; private set; }

        /// <summary>
        /// 子结点
        /// </summary>
        public ReadOnlyCollection<IDriverInterface> Chirldren { get; private set; }

        /// <summary>
        /// 与此接口对应的消息
        /// </summary>
        public IInfomationItem CurrentInfomationItem { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public DriverInterfaceKey Id { get; set; }

        /// <summary>
        /// 弹出框
        /// </summary>
        public IDriverPopupView PopupView { get; private set; }

        /// <summary>
        /// 司机的选择信息
        /// </summary>
        public IDriverSelectable DriverSelectable { get; private set; }

        /// <summary>
        /// 上一个接口
        /// </summary>
        public Stack<IDriverInterface> LastInterfaceStack { get; private set; }

        /// <summary>
        /// 设置父结点
        /// </summary>
        /// <param name="driverInterface"></param>
        public void SetParent(IDriverInterface driverInterface)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 从 lastInterface 跳转进来
        /// </summary>
        /// <param name="lastInterface"></param>
        /// <returns></returns>
        public bool NavigateToThis(IDriverInterface lastInterface)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 从这里跳转出去
        /// </summary>
        public bool NavigateFromThis()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            throw new NotImplementedException();
        }
    }
}