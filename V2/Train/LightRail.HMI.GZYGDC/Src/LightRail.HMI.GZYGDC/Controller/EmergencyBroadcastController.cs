using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class EmergencyBroadcastController : ControllerBase<Lazy<EmergencyBroadcastViewModel>>
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
        public EmergencyBroadcastController(Lazy<EmergencyBroadcastViewModel> viewModel) : base(viewModel)
        {
            SendBroadcastCommand = new DelegateCommand<EmergencyBroadcastItem>(SendBroadcast);
        }


        public override void Initalize()
        {
            //默认为第一页
            m_CurPageNum = 1;

            //计算最大页编号
            m_MaxPageNum = (ViewModel.Value.Model.EmergencyBroadcastItems.Count + MAX_PAGE_COUNT - 1) / MAX_PAGE_COUNT;

            UpdateDisplayItems();
        }

        private void UpdateDisplayItems()
        {
            ViewModel.Value.Model.EmergencyBroadcastDisplayItems =
                ViewModel.Value.Model.EmergencyBroadcastItems.Skip((m_CurPageNum - 1)*MAX_PAGE_COUNT)
                    .Take(MAX_PAGE_COUNT);
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

        /// <summary>
        /// 发送广播命令
        /// </summary>
        public ICommand SendBroadcastCommand { get; private set; }


        private void SendBroadcast(EmergencyBroadcastItem item)
        {
            if (item != null)
            {
                if (GlobalParam.Instance.InitParam != null)
                {
                    var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                    if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                    {
                        if (item.IsChecked)
                        {
                            DataService.WriteService.ChangeFloat(GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFloatKeys.紧急广播编号], item.Index);
                        }
                        else
                        {
                            DataService.WriteService.ChangeFloat(GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFloatKeys.紧急广播编号], 0);
                        }

                        UpdateCheckedStatus(item);

                        UpdateDisplayItems();
                    }
                }
            }
        }

        /// <summary>
        /// 更新选中状态，保证集合中只有１个处于选中状态
        /// </summary>
        /// <param name="item"></param>
        private void UpdateCheckedStatus(EmergencyBroadcastItem item)
        {
            if (ViewModel.Value.Model.EmergencyBroadcastItems != null)
            {
                if (item != null)
                {
                    for (int i = 0; i < ViewModel.Value.Model.EmergencyBroadcastItems.Count; ++i)
                    {
                        if (item.Index == ViewModel.Value.Model.EmergencyBroadcastItems[i].Index)
                        {
                            ViewModel.Value.Model.EmergencyBroadcastItems[i].IsChecked = item.IsChecked;
                        }
                        else
                        {
                            ViewModel.Value.Model.EmergencyBroadcastItems[i].IsChecked = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ViewModel.Value.Model.EmergencyBroadcastItems.Count; ++i)
                    {
                        ViewModel.Value.Model.EmergencyBroadcastItems[i].IsChecked = false;
                    }
                }
            }
        }


        /// <summary>
        /// 重置广播输出数据
        /// </summary>
        public void ResetBroadcast()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var DataService = GlobalParam.Instance.InitParam.CommunicationDataService;

                if (DataService != null && GlobalParam.Instance.IndexDescription != null)
                {
                    DataService.WriteService.ChangeFloat(GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[OutFloatKeys.紧急广播编号], 0);
                }
            }

            UpdateCheckedStatus(null);
        }
    }
}