using System;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Resource;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Controller
{
    [Export]
    public class BorderCastController : ControllerBase<BorderCastViewModel>
    {

        [ImportingConstructor]
        public BorderCastController(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            EventAggregator = eventAggregator;
            RegionManager = regionManager;
            ModelSelect = new DelegateCommand<string>(ModelSelectAction);
            StationDown = new DelegateCommand<string>(StationDownAction);
            StationSelect = new DelegateCommand<string>(StationSelectAction);
            SenFloatEvent = eventAggregator.GetEvent<SendDataEvent<float>>();
            SendBoolEvent = eventAggregator.GetEvent<SendDataEvent<bool>>();
            SendBoolCommand = new DelegateCommand<string>(SendBoolCommandAction);
            Confirm = new DelegateCommand(ConfirmAction);
        }

        private void ConfirmAction()
        {
            var index = GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[OutFloatKeys.始发站];
            var value = ViewModel.AllStation.FirstOrDefault(f => f.Name.Equals(ViewModel.StartStation))?.Index;
            SendData(value, index);
            index = GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[OutFloatKeys.下一站];
            value = ViewModel.AllStation.FirstOrDefault(f => f.Name.Equals(ViewModel.NextStation))?.Index;
            SendData(value, index);
            index = GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[OutFloatKeys.终点站];
            value = ViewModel.AllStation.FirstOrDefault(f => f.Name.Equals(ViewModel.EndStation))?.Index;
            SendData(value, index);

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToEvent>().Publish(new NavigatorToEvent.NavigatorArgs()
            {
                Names = ViewNames.DoorView
            });


        }
        
        private void SendData(int? value, int index)
        {
            if (value != null)
            {
                SenFloatEvent.Publish(new SendDataEnvetArgs<float>()
                {
                    Index = index,
                    Value = (float)value,
                });
                
            }
        }
        private void SendBoolCommandAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            SendBoolEvent.Publish(new SendDataEnvetArgs<bool>()
            {
                Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[obj],
                Value = true,
                IsClear = true
            });
            
        }

        protected SendDataEvent<bool> SendBoolEvent { get; private set; }
        protected SendDataEvent<float> SenFloatEvent { get; private set; }

        private bool CanExecute()
        {
            return m_Model == BorderCastModel.Manual;
        }

        private void ModelSelectAction(string obj)
        {
            BorderCastModel model;
            if (System.Enum.TryParse(obj, true, out model))
            {
                m_Model = model;
                switch (m_Model)
                {
                    case BorderCastModel.Auto:
                        SendBoolEvent.Publish(new SendDataEnvetArgs<bool>()
                        {
                            Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.广播模式自动],
                            Value = true,
                        });
                        SendBoolEvent.Publish(new SendDataEnvetArgs<bool>()
                        {
                            Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.广播模式手动],
                            Value = false,
                        });
                        break;
                    case BorderCastModel.Manual:
                        SendBoolEvent.Publish(new SendDataEnvetArgs<bool>()
                        {
                            Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.广播模式自动],
                            Value = false,
                        });
                        SendBoolEvent.Publish(new SendDataEnvetArgs<bool>()
                        {
                            Index = GlobalParam.Instance.IndexConfig.OutBoolDescriptionDictionary[OutBoolKeys.广播模式手动],
                            Value = true,
                        });

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private BorderCastModel m_Model;
        private void StationSelectAction(string obj)
        {
            m_Index = obj.ToInt();
        }

        /// <summary>
        /// 当前设置的站点  -1 无  1 始发站  2 下一站  3 终点站
        /// </summary>
        private int m_Index;
        private void StationDownAction(string obj)
        {
            if (!CanExecute())
            {
                return;
            }
            var name = ViewModel.AllStation.FirstOrDefault(f => f.Index == obj.ToInt())?.Name;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            switch (m_Index)
            {
                case 1:
                    ViewModel.StartStation = name;
                    break;
                case 2:
                    ViewModel.NextStation = name;
                    break;
                case 3:
                    ViewModel.EndStation = name;
                    break;
                case -1:
                    break;
            }
        }
        /// <summary>
        /// 站点按下命令
        /// </summary>
        public ICommand StationDown { get; private set; }
        /// <summary>
        /// 始发站  下一站  终点站 按下命令
        /// </summary>
        public ICommand StationSelect { get; private set; }
        /// <summary>
        /// 模式选择命令
        /// </summary>
        public ICommand ModelSelect { get; private set; }
        /// <summary>
        /// 发送Bool量
        /// </summary>
        public ICommand SendBoolCommand { get; private set; }
        /// <summary>
        /// 确认按键
        /// </summary>
        public ICommand Confirm { get; private set; }

        protected IEventAggregator EventAggregator { get; private set; }
        protected IRegionManager RegionManager { get; private set; }

    }
}