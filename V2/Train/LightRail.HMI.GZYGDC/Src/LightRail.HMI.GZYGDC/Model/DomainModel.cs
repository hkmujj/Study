using System;
using System.ComponentModel.Composition;
using DevExpress.XtraPrinting.Native;
using LightRail.HMI.GZYGDC.Model.BtnStragy;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        //中部按键
        private IStateInterface m_CenterStateInterface;

        //底部按键
        private IStateInterface m_BottomStateInterface;

        /// <summary>
        /// 界面是否可见，与黑屏标志关联
        /// </summary>
        private bool m_bIsStart = false;


        private DateTime m_CurrentTime;

        /// <summary>
        /// 中部按键
        /// </summary>
        public IStateInterface CenterStateInterface
        {
            private set
            {
                if (Equals(value, m_CenterStateInterface))
                {
                    return;
                }

                m_CenterStateInterface = value;
                m_CenterStateInterface.UpdateState();
                RaisePropertyChanged(() => CenterStateInterface);
            }
            get { return m_CenterStateInterface; }
        }

        /// <summary>
        /// 更新中部按键
        /// </summary>
        /// <param name="current"></param>
        public void UpdateCenterState(IStateInterface current)
        {
            CenterStateInterface = current;
        }


        /// <summary>
        /// 底部按键
        /// </summary>
        public IStateInterface BottomStateInterface
        {
            private set
            {
                if (Equals(value, m_BottomStateInterface))
                {
                    return;
                }

                m_BottomStateInterface = value;
                m_BottomStateInterface.UpdateState();
                RaisePropertyChanged(() => BottomStateInterface);
            }
            get { return m_BottomStateInterface; }
        }

        /// <summary>
        /// 更新底部按键
        /// </summary>
        /// <param name="current"></param>
        public void UpdateBottomState(IStateInterface current)
        {
            BottomStateInterface = current;
        }


        /// <summary>
        /// 是否启动屏，屏是否可见
        /// </summary>
        public bool IsStart
        {
            get { return m_bIsStart; }
            set
            {
                if (value == m_bIsStart)
                {
                    return;
                }

                m_bIsStart = value;
                RaisePropertyChanged(()=>IsStart);
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                if (value.Equals(m_CurrentTime))
                {
                    return;
                }

                m_CurrentTime = value;
                RaisePropertyChanged(()=>CurrentTime);
            }
        }
    }
}