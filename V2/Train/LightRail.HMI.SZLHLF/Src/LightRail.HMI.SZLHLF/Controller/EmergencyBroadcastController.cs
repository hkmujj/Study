using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Model.ConfigModel;
using LightRail.HMI.SZLHLF.Model.EmergencyBroadcastModel;
using LightRail.HMI.SZLHLF.Resources.Keys;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class EmergencyBroadcastController : ControllerBase<Lazy<EmergencyBroadcastInfoModel>>
    {
        /// <summary>
        /// 当前页编号
        /// </summary>
        private int m_CurPageNum;

        /// <summary>
        /// 总页编号
        /// </summary>
        private int m_MaxPageNum;

        /// <summary>
        /// 每页最大条目数量
        /// </summary>
        private const int MAX_PAGE_COUNT = 8;

       
        [ImportingConstructor]
        public EmergencyBroadcastController(Lazy<EmergencyBroadcastInfoModel> viewModel)
            : base(viewModel)
        {
            SendBroadcastCommand = new DelegateCommand<EmergencyBroadcastItem>(SendBroadcast);
        }


        public override void Initalize()
        {
            //默认为第一页
            m_CurPageNum = 1;

            //计算最大页编号
            m_MaxPageNum = (ViewModel.Value.EmergencyBroadcastItems.Count + MAX_PAGE_COUNT - 1) / MAX_PAGE_COUNT;

            UpdateDisplayItems();
        }

        public void Clear()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null)
                {
                    DataService.WriteService.ChangeFloat(
                        GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFolatKey.紧急广播号], 0);
                }
            }
        }

        private void UpdateDisplayItems()
        {
            ViewModel.Value.EmergencyBroadcastDisplayItems =new ObservableCollection<EmergencyBroadcastItem>(    ViewModel.Value.EmergencyBroadcastItems.Skip((m_CurPageNum - 1)*MAX_PAGE_COUNT)
                    .Take(MAX_PAGE_COUNT));
            
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void LastPage()
        {
            if (m_CurPageNum == 1)
            {
                return;
            }

            --m_CurPageNum;

            UpdateDisplayItems();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            if (m_CurPageNum == m_MaxPageNum)
            {
                return;
            }

            ++m_CurPageNum;

            UpdateDisplayItems();
        }

        private void SendBroadcast(EmergencyBroadcastItem item)
        {
            if (item != null)
            {
                if (GlobalParam.Instance.InitParam!=null)
                {
                    var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                    if (DataService != null)
                    {
                        DataService.WriteService.ChangeFloat(
                            GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFolatKey.紧急广播号], item.LogicNum);
                    }
                }
            }
        }

        /// <summary>
        /// 发送广播命令
        /// </summary>
        public ICommand SendBroadcastCommand { get; private set; }

        /// <summary>
        /// 当前页编号
        /// </summary>
        public int CurPageNum
        {
            get { return m_CurPageNum; }
            set
            {
                if (value == m_CurPageNum)
                {
                    return;
                }

                m_CurPageNum = value;
                RaisePropertyChanged(() => CurPageNum);
            }
        }
    }
}