using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CommonUtil.Util;
using DevExpress.Mvvm.POCO;
using LightRail.HMI.SZLHLF.Constant;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.View.Contents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    [Export(typeof(IModels))]
    public class OtherModel : ModelBase
    {
        private string m_strNextStation;
        private string m_strCurViewTitle;
        private string m_strEndStation;
        private float m_Electricity;
        private float m_Voltage;
        private float m_Speed;
        private float m_TrainNum;
        private DateTime m_CurrentTime;
        private bool m_HMIBlack;
        private string m_PassWord;

        public override void Initialize()
        {
            //m_HMIBlack = false;
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public string NextStation
        {
            get
            {
                if (m_strNextStation.IsNullOrWhiteSpace())
                {
                    m_strNextStation = "?";
                }

                return m_strNextStation;
            }
            set
            {
                if (value == m_strNextStation)
                {
                    return;
                }

                m_strNextStation = value;

                RaisePropertyChanged(() => NextStation);
            }
        }


        /// <summary>
        /// 当前视图标题
        /// </summary>
        public string CurViewTitle
        {
            get { return m_strCurViewTitle; }
            set
            {
                if (value == m_strCurViewTitle)
                {
                    return;
                }

                m_strCurViewTitle = value;

                RaisePropertyChanged(() => CurViewTitle);
            }
        }


        /// <summary>
        /// 终点站
        /// </summary>
        public string EndStation
        {
            get
            {
                if (m_strEndStation.IsNullOrWhiteSpace())
                {
                    m_strEndStation = "?";
                }

                return m_strEndStation;
            }
            set
            {
                if (value == m_strEndStation)
                {
                    return;
                }

                m_strEndStation = value;

                RaisePropertyChanged(() => EndStation);
            }
        }
        
        /// <summary>
        /// 电流
        /// </summary>
        public float Electricity
        {
            set
            {
                if (value.Equals(m_Electricity))
                {
                    return;
                }

                m_Electricity = value;
                RaisePropertyChanged(() => Electricity);
            }
            get { return m_Electricity; }
        }
        
        /// <summary>
        /// 电压
        /// </summary>
        public float Voltage
        {
            set
            {
                if (value.Equals(m_Voltage))
                {
                    return;
                }

                m_Voltage = value;
                RaisePropertyChanged(() => Voltage);
            }
            get { return m_Voltage; }
        }

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed
        {
            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }

                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
            get { return m_Speed; }
        }

        /// <summary>
        /// 列车号
        /// </summary>
        public float TrainNum
        {
            get { return m_TrainNum; }
            set
            {
                if (value == m_TrainNum)
                {
                    return;
                }

                m_TrainNum = value;
                RaisePropertyChanged(() => TrainNum);
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
                RaisePropertyChanged(() => CurrentTime);
            }
        }
        //[ImportMany]
        //private List<Lazy<IModels>> AllModels;
        /// <summary>
        /// 黑屏
        /// </summary>
        public bool HMIBlack
        {
            get { return m_HMIBlack; }
            set
            {
                if (value == m_HMIBlack)
                {
                    return;
                }
                m_HMIBlack = value;
                //if (m_HMIBlack)
                //{
                //    AllModels.ForEach(f => f.Value.Initialize());
                //    ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.ContentUpContent, typeof(RootContentView).FullName); ;
                //}
                //else
                //{
                //    AllModels.ForEach(f => f.Value.Clear());
                //}
                RaisePropertyChanged(()=>HMIBlack);
            }
        }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            get { return m_PassWord; }
            set
            {
                if (value == m_PassWord)
                {
                    return;
                }
                m_PassWord = value;
                RaisePropertyChanged(() => PassWord);
            }
        }
    }
}
