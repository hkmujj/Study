using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Resource.Keys;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class SmokeViewModel : ViewModelBase
    {
        private SmokeState m_Car6SmokeState;
        private SmokeState m_Car5SmokeState;
        private SmokeState m_Car4SmokeState;
        private SmokeState m_Car3SmokeState;
        private SmokeState m_Car2SmokeState;
        private SmokeState m_Car1SmokeState;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmokeViewModel()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<BoolDataChangedEvent>().Subscribe((NetWorkResponse), ThreadOption.UIThread);
        }

        private void NetWorkResponse(DataChangedEventArgs<bool> dataChangedEventArgs)
        {
            var tmp = new Dictionary<string, Action>();
            tmp.Add(InBoolKeys.车1温烟探测器正常, () => Car1SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车1超过一定数量的温烟探测器失效, () => Car1SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车1探测带烟雾或高温, () => Car1SmokeState = SmokeState.Smoke);
            tmp.Add(InBoolKeys.车2温烟探测器正常, () => Car2SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车2超过一定数量的温烟探测器失效, () => Car2SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车2探测带烟雾或高温, () => Car2SmokeState = SmokeState.Smoke);
            tmp.Add(InBoolKeys.车3温烟探测器正常, () => Car3SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车3超过一定数量的温烟探测器失效, () => Car3SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车3探测带烟雾或高温, () => Car3SmokeState = SmokeState.Smoke);
            tmp.Add(InBoolKeys.车4温烟探测器正常, () => Car4SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车4超过一定数量的温烟探测器失效, () => Car4SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车4探测带烟雾或高温, () => Car4SmokeState = SmokeState.Smoke);
            tmp.Add(InBoolKeys.车5温烟探测器正常, () => Car5SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车5超过一定数量的温烟探测器失效, () => Car5SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车5探测带烟雾或高温, () => Car5SmokeState = SmokeState.Smoke);
            tmp.Add(InBoolKeys.车6温烟探测器正常, () => Car6SmokeState = SmokeState.Normal);
            tmp.Add(InBoolKeys.车6超过一定数量的温烟探测器失效, () => Car6SmokeState = SmokeState.Invalid);
            tmp.Add(InBoolKeys.车6探测带烟雾或高温, () => Car6SmokeState = SmokeState.Smoke);
            dataChangedEventArgs.Data.UpdateIfContainWhenAllTrue(tmp, null);
            //tmp.Add();
        }

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            Car1SmokeState = SmokeState.Normal;
            Car2SmokeState = SmokeState.Normal;
            Car3SmokeState = SmokeState.Normal;
            Car4SmokeState = SmokeState.Normal;
            Car5SmokeState = SmokeState.Normal;
            Car6SmokeState = SmokeState.Normal;
        }

        /// <summary>
        /// 车1烟火状态
        /// </summary>
        public SmokeState Car1SmokeState
        {
            get { return m_Car1SmokeState; }
            set
            {
                if (value == m_Car1SmokeState)
                {
                    return;
                }
                m_Car1SmokeState = value;
                RaisePropertyChanged(() => Car1SmokeState);
            }
        }

        /// <summary>
        /// 车2烟火状态
        /// </summary>
        public SmokeState Car2SmokeState
        {
            get { return m_Car2SmokeState; }
            set
            {
                if (value == m_Car2SmokeState)
                {
                    return;
                }
                m_Car2SmokeState = value;
                RaisePropertyChanged(() => Car2SmokeState);
            }
        }

        /// <summary>
        /// 车3烟火状态
        /// </summary>
        public SmokeState Car3SmokeState
        {
            get { return m_Car3SmokeState; }
            set
            {
                if (value == m_Car3SmokeState)
                {
                    return;
                }
                m_Car3SmokeState = value;
                RaisePropertyChanged(() => Car3SmokeState);
            }
        }

        /// <summary>
        /// 车4烟火状态
        /// </summary>
        public SmokeState Car4SmokeState
        {
            get { return m_Car4SmokeState; }
            set
            {
                if (value == m_Car4SmokeState)
                {
                    return;
                }
                m_Car4SmokeState = value;
                RaisePropertyChanged(() => Car4SmokeState);
            }
        }

        /// <summary>
        /// 车5烟火状态
        /// </summary>
        public SmokeState Car5SmokeState
        {
            get { return m_Car5SmokeState; }
            set
            {
                if (value == m_Car5SmokeState)
                {
                    return;
                }
                m_Car5SmokeState = value;
                RaisePropertyChanged(() => Car5SmokeState);
            }
        }

        /// <summary>
        /// 车6烟火状态
        /// </summary>
        public SmokeState Car6SmokeState
        {
            get { return m_Car6SmokeState; }
            set
            {
                if (value == m_Car6SmokeState)
                {
                    return;
                }
                m_Car6SmokeState = value;
                RaisePropertyChanged(() => Car6SmokeState);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SmokeState
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("正常，未探测到火或烟")]
        Normal,
        /// <summary>
        /// 失效
        /// </summary>
        [Description("超过一定数量的烟温探测器失效")]
        Invalid,
        /// <summary>
        /// 烟火
        /// </summary>
        [Description("探测到火或烟")]
        Smoke
    }
}