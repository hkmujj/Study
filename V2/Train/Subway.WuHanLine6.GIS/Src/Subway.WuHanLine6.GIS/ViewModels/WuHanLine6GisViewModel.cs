using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.XtraPrinting.Native;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.GIS.Config;
using Subway.WuHanLine6.GIS.Controller;
using Subway.WuHanLine6.GIS.Event;
using Subway.WuHanLine6.GIS.Extention;
using Subway.WuHanLine6.GIS.Models;
using Subway.WuHanLine6.GIS.Resource.Keys;

namespace Subway.WuHanLine6.GIS.ViewModels
{
    [Export]
    public class WuHanLine6GisViewModel : ViewModelBase, IDataListener
    {
        private ObservableCollection<StationName> m_Right;
        private ObservableCollection<StationName> m_Left;
        private string m_EndStation;
        private bool m_IsRightOpenDoor;
        private bool m_IsLeftOpenDoor;
        private StationName m_LastStation;
        private StationName m_CurrentStation;
        private StationName m_NextStation;
        private bool m_IsArrow1;
        private bool m_IsArrow2;
        private bool m_IsArrow3;
        private bool m_IsArrow4;
        private bool m_UnArrive;
        private bool m_Arrive;
        private string m_NextStationString;
        private StationName m_ArriveNextStationName;
        private Visibility m_MMIBlack;

        [ImportingConstructor]
        public WuHanLine6GisViewModel(WuHanLine6GisController controller)
        {
            Controller = controller;
            Right = new ObservableCollection<StationName>(GlobalParams.Instance.StationNames.OrderBy(o => o.Index));
            Left = new ObservableCollection<StationName>(GlobalParams.Instance.StationNames.OrderByDescending(o => o.Index));
            ArrowChanged = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ArrowChangedEvent>();
            if (GlobalParams.Instance.InitParam != null)
            {
                var cource = GlobalParams.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>();
                cource.CourseStateChanged += Cource_CourseStateChanged;
            }
            MMIBlack = Visibility.Hidden;
        }

        private void Cource_CourseStateChanged(object sender, CourseStateChangedArgs e)
        {
            switch (e.CourseService.CurrentCourseState)
            {
                case CourseState.Unknown:
                    break;
                case CourseState.Started:
                    MMIBlack = Visibility.Visible;
                    break;
                case CourseState.Stoped:
                    Left.ForEach(f =>
                    {
                        f.IsPast = false;
                        f.IsNext = false;
                        f.IsSkip = false;
                    });
                    NextStation = null;
                    LastStation = null;
                    CurrentStation = null;
                    ArriveNextStationName = null;
                    MMIBlack = Visibility.Hidden;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Visibility MMIBlack
        {
            get { return m_MMIBlack; }
            set
            {
                if (value == m_MMIBlack)
                {
                    return;
                }
                m_MMIBlack = value;
                RaisePropertyChanged(() => MMIBlack);
            }
        }

        public WuHanLine6GisController Controller { get; private set; }

        public string NextStationString
        {
            get { return m_NextStationString; }
            set
            {
                if (value == m_NextStationString)
                {
                    return;
                }
                m_NextStationString = value;
                RaisePropertyChanged(() => NextStationString);
            }
        }

        public ObservableCollection<StationName> Right
        {
            get { return m_Right; }
            set
            {
                if (Equals(value, m_Right))
                {
                    return;
                }
                m_Right = value;
                RaisePropertyChanged(() => Right);
            }
        }

        public ObservableCollection<StationName> Left
        {
            get { return m_Left; }
            set
            {
                if (Equals(value, m_Left))
                {
                    return;
                }
                m_Left = value;
                RaisePropertyChanged(() => Left);
            }
        }
        /// <summary>
        /// 终点站名称
        /// </summary>
        public string EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        public bool IsLeftOpenDoor
        {
            get { return m_IsLeftOpenDoor; }
            set
            {
                if (value == m_IsLeftOpenDoor)
                {
                    return;
                }
                m_IsLeftOpenDoor = value;
                RaisePropertyChanged(() => IsLeftOpenDoor);
            }
        }

        public bool IsRightOpenDoor
        {
            get { return m_IsRightOpenDoor; }
            set
            {
                if (value == m_IsRightOpenDoor)
                {
                    return;
                }
                m_IsRightOpenDoor = value;
                RaisePropertyChanged(() => IsRightOpenDoor);
            }
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public StationName NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (Equals(value, m_NextStation))
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        /// <summary>
        /// 当前
        /// </summary>
        public StationName CurrentStation
        {
            get { return m_CurrentStation; }
            set
            {
                if (Equals(value, m_CurrentStation))
                {
                    return;
                }
                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }
        /// <summary>
        /// 到站下一站
        /// </summary>
        public StationName ArriveNextStationName
        {
            get { return m_ArriveNextStationName; }
            set
            {
                if (Equals(value, m_ArriveNextStationName))
                {
                    return;
                }
                m_ArriveNextStationName = value;
                RaisePropertyChanged(() => ArriveNextStationName);
            }
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public StationName LastStation
        {
            get { return m_LastStation; }
            set
            {
                if (Equals(value, m_LastStation))
                {
                    return;
                }
                m_LastStation = value;
                RaisePropertyChanged(() => LastStation);
            }
        }

        public bool IsArrow1
        {
            get { return m_IsArrow1; }
            set
            {
                if (value == m_IsArrow1)
                {
                    return;
                }
                m_IsArrow1 = value;
                RaisePropertyChanged(() => IsArrow1);
            }
        }

        public bool IsArrow2
        {
            get { return m_IsArrow2; }
            set
            {
                if (value == m_IsArrow2)
                {
                    return;
                }
                m_IsArrow2 = value;
                RaisePropertyChanged(() => IsArrow2);
            }
        }

        public bool IsArrow3
        {
            get { return m_IsArrow3; }
            set
            {
                if (value == m_IsArrow3)
                {
                    return;
                }
                m_IsArrow3 = value;
                RaisePropertyChanged(() => IsArrow3);
            }
        }

        public bool IsArrow4
        {
            get { return m_IsArrow4; }
            set
            {
                if (value == m_IsArrow4)
                {
                    return;
                }
                m_IsArrow4 = value;
                RaisePropertyChanged(() => IsArrow4);
            }
        }
        public ArrowChangedEvent ArrowChanged { get; private set; }
        /// <summary>
        /// 要到达
        /// </summary>
        public bool UnArrive
        {
            get { return m_UnArrive; }
            set
            {
                if (value == m_UnArrive)
                {
                    return;
                }
                m_UnArrive = value;
                Task.Factory.StartNew(new Action(() =>
                {
                    ArrowChanged.Publish(new ArrowChangedEvent.Args(value));
                }));

                if (value)
                {
                    Controller.ChangToStation();
                }
                else
                {
                    Controller.ChangToMap();
                }
                RaisePropertyChanged(() => UnArrive);
            }
        }
        /// <summary>
        /// 已经到达
        /// </summary>
        public bool Arrive
        {
            get { return m_Arrive; }
            set
            {
                if (value == m_Arrive)
                {
                    return;
                }
                m_Arrive = value;
                RaisePropertyChanged(() => Arrive);
            }
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            dataChangedArgs.NewValue.UpdateIfContanin(InBoolKeys.列车准备到站, b => UnArrive = b);
            dataChangedArgs.NewValue.UpdateIfContanin(InBoolKeys.列车已经到站, b => Arrive = b);
            dataChangedArgs.NewValue.UpdateIfContanin(InBoolKeys.列车预开门侧右侧, b => IsRightOpenDoor = b);
            dataChangedArgs.NewValue.UpdateIfContanin(InBoolKeys.列车预开门侧左侧, b => IsLeftOpenDoor = b);
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            dataChangedArgs.NewValue.UpdateIfContanin(InFloatKeys.上一站, f =>
            {
                var tmp = Left.FirstOrDefault(d => d.Index == (int)f);
                if (tmp != null)
                {
                    LastStation = tmp;
                }
            });
            dataChangedArgs.NewValue.UpdateIfContanin(InFloatKeys.下一站, f =>
            {
                Controller.ChangedNext(false);
                var tmp = Left.FirstOrDefault(d => d.Index == (int)f);
                if (tmp != null)
                {
                    NextStation = tmp;
                }
                Controller.ChangedNext(true);
            });
            dataChangedArgs.NewValue.UpdateIfContanin(InFloatKeys.当前站, f =>
            {
                var tmp = Left.FirstOrDefault(d => d.Index == (int)f);
                if (tmp != null)
                {
                    CurrentStation = tmp;
                }
            });
            dataChangedArgs.NewValue.UpdateIfContanin(InFloatKeys.到站下一站, f =>
            {
                ArriveNextStationName = Left.FirstOrDefault(d => d.Index == (int)f);
            });
            dataChangedArgs.NewValue.UpdateIfContanin(InFloatKeys.终点站, f => EndStation = Left.FirstOrDefault(d => d.Index == (int)f)?.ChineseName);
        }
        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {

        }
    }
}