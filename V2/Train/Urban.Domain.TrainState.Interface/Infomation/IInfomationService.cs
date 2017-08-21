using System;
using System.ComponentModel;

namespace Urban.Domain.TrainState.Interface.Infomation
{
    public interface IInfomationService 
    {
        /// <summary>
        /// 消息生发
        /// </summary>
        event Action<IInfomationItem> InfomationBegin;


        /// <summary>
        /// 消息结束
        /// </summary>
        event Action<IInfomationItem> InfomationEnd;


        void Initalize();

    }
}