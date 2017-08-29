using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class NetTopologyController : ControllerBase<Lazy<NetTopologyViewModel>>
    {
       
        [ImportingConstructor]
        public NetTopologyController(Lazy<NetTopologyViewModel> viewModel) : base(viewModel)
        {
            OutCarBroadcastCommand = new DelegateCommand<string>(OutCarBroadcast);
            PassengerBroadcastCommand = new DelegateCommand<string>(PassengerBroadcast);
            PassengerCallTalkCommand = new DelegateCommand<string>(PassengerCallTalk);
            PassengerCallTalkResetCommand = new DelegateCommand<string>(PassengerCallTalkReset);
            CabCallTalkCommand = new DelegateCommand<string>(CabCallTalk);
        }


        public override void Initalize()
        {
            ViewModel.Value.Model.PropertyChanged += OnPropertyChanged;

            UpdateSendData();
        }

        /// <summary>
        /// 车外广播命令
        /// </summary>
        public ICommand OutCarBroadcastCommand { get; private set; }

        /// <summary>
        /// 客室广播命令
        /// </summary>
        public ICommand PassengerBroadcastCommand { get; private set; }

        /// <summary>
        /// 乘客对讲命令
        /// </summary>
        public ICommand PassengerCallTalkCommand { get; private set; }



        /// <summary>
        /// 乘客对讲复位命令
        /// </summary>
        public ICommand PassengerCallTalkResetCommand { get; private set; }



        /// <summary>
        /// 司机室对讲命令
        /// </summary>
        public ICommand CabCallTalkCommand { get; private set; }


        /// <summary>
        /// 车外广播
        /// </summary>
        private void OutCarBroadcast(string strOpen)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.车外广播], strOpen == "1" ? true : false);
                }
            }
        }


        /// <summary>
        /// 客室广播
        /// </summary>
        private void PassengerBroadcast(string strOpen)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.客室广播], strOpen == "1" ? true : false);
                }
            }
        }

        /// <summary>
        /// 乘客对讲
        /// </summary>
        private void PassengerCallTalk(string strOpen)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.乘客对讲], strOpen == "1" ? true : false);
                }
            }
        }


        /// <summary>
        /// 乘客对讲复位
        /// </summary>
        private void PassengerCallTalkReset(string strOpen)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.乘客对讲复位], strOpen == "1" ? true : false);
                }
            }
        }



        /// <summary>
        /// 司机室对讲
        /// </summary>
        private void CabCallTalk(string strOpen)
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[OutBoolKeys.司机室对讲], strOpen == "1" ? true : false);
                }
            }
        }

        /// <summary>
        /// 更新发送数据
        /// </summary>
        private void UpdateSendData()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                  

                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateSendData();
        }

        /// <summary>
        /// 重置输出数据
        /// </summary>
        public void ResetData()
        {
            OutCarBroadcast("0");
            PassengerBroadcast("0");
            PassengerCallTalk("0");
            PassengerCallTalkReset("0");
            CabCallTalk("0");
        }
    }
}