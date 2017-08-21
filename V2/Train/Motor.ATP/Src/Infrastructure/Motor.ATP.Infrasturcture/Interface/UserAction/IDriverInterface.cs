using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 司机接口
    /// </summary>
    public interface IDriverInterface : INotifyPropertyChanged
    {
        /// <summary>
        /// 所属ATP
        /// </summary>
        IATP ATP { get; }

        /// <summary>
        /// 父结点
        /// </summary>
        IDriverInterface Parent { get; }

        /// <summary>
        /// 子结点
        /// </summary>
        ReadOnlyCollection<IDriverInterface> Chirldren { get; }

        /// <summary>
        /// 与此接口对应的消息
        /// </summary>
        IInfomationItem CurrentInfomationItem { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        DriverInterfaceKey Id { get; }

        /// <summary>
        /// 弹出框
        /// </summary>
        IDriverPopupView PopupView { get; }

        /// <summary>
        /// 司机的选择信息
        /// </summary>
        IDriverSelectable DriverSelectable { get; }

        /// <summary>
        /// 上一个接口
        /// </summary>
        Stack<IDriverInterface> LastInterfaceStack { get; }

        /// <summary>
        /// 设置父结点
        /// </summary>
        /// <param name="driverInterface"></param>
        void SetParent(IDriverInterface driverInterface);

        /// <summary>
        /// 从 lastInterface 跳转进来
        /// </summary>
        /// <param name="lastInterface"></param>
        /// <returns></returns>
        bool NavigateToThis(IDriverInterface lastInterface);

        /// <summary>
        /// 从这里跳转出去
        /// </summary>
        bool NavigateFromThis();

    }
}