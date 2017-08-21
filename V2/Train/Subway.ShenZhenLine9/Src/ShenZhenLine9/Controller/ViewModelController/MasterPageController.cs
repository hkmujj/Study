using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class MasterPageController : ControllerBase<MasterPageViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public MasterPageController()
        {
            LastPage = new DelegateCommand((LastPageResponse));
            NextPage = new DelegateCommand((NextPageResponse));
            Confirm = new DelegateCommand((ConfirmResponse));
            var events = ServiceLocator.Current.GetInstance<IEventAggregator>();
            events.GetEvent<BoolDataChangedEvent>().Subscribe((BooChanged));
            events.GetEvent<FloatDataChangedEvent>().Subscribe((FloatChanged));
        }

        private void FloatChanged(DataChangedEventArgs<float> args)
        {
            args.Data.UpdateIfContain(InFloatKeys.限速值, f => ViewModel.LimitSpeed = f.ToString("F0"));
            args.Data.UpdateIfContain(InFloatKeys.速度, f => ViewModel.Speed = f.ToString("F0"));
            args.Data.UpdateIfContain(InFloatKeys.牵引级位, f => ViewModel.Traction = f);
            args.Data.UpdateIfContain(InFloatKeys.制动级位, f => ViewModel.Brake = f);
            args.Data.UpdateIfContain(InFloatKeys.工况, f => ViewModel.WorkState = (WorkState)f);
            args.Data.UpdateIfContain(InFloatKeys.信号模式, f => ViewModel.SignalModel = (SignalModel)f);
        }

        private void BooChanged(DataChangedEventArgs<bool> args)
        {
            args.Data.UpdateIfContain(InBoolKeys.列车运行方向右, b => ViewModel.IsRunCar6 = b);
            args.Data.UpdateIfContain(InBoolKeys.列车运行方向左, b => ViewModel.IsRunCar1 = b);
            args.Data.UpdateIfContain(InBoolKeys.列车预开门侧右, b => ViewModel.IsRightOpenDoor = b);
            args.Data.UpdateIfContain(InBoolKeys.列车预开门侧左, b => ViewModel.IsLeftOpenDoor = b);
            args.Data.UpdateIfContain(InBoolKeys.司机室激活1, b => ViewModel.IsActiveCar1 = b);
            args.Data.UpdateIfContain(InBoolKeys.司机室激活6, b => ViewModel.IsActiveCar6 = b);

            args.Data.UpdateIfContain(InBoolKeys.空调功能区故障, b => ViewModel.MainContentBrnViewModel.AirConditionFault = b);
            args.Data.UpdateIfContain(InBoolKeys.辅助功能区故障, b => ViewModel.MainContentBrnViewModel.AssistFault = b);
            args.Data.UpdateIfContain(InBoolKeys.紧急广播功能区故障, b => ViewModel.MainContentBrnViewModel.EmergencyBorderCastFault = b);
            args.Data.UpdateIfContain(InBoolKeys.门功能区故障, b => ViewModel.MainContentBrnViewModel.DoorFault = b);
            args.Data.UpdateIfContain(InBoolKeys.紧急对讲功能区故障, b => ViewModel.MainContentBrnViewModel.EmergencyTalkFault = b);
            args.Data.UpdateIfContain(InBoolKeys.走行部功能区故障, b => ViewModel.MainContentBrnViewModel.LeeFeedFault = b);
            args.Data.UpdateIfContain(InBoolKeys.制动功能区故障, b => ViewModel.MainContentBrnViewModel.BrakeFault = b);
            args.Data.UpdateIfContain(InBoolKeys.牵引功能区故障, b => ViewModel.MainContentBrnViewModel.TractionFault = b);
            args.Data.UpdateIfContain(InBoolKeys.烟火功能区故障, b => ViewModel.MainContentBrnViewModel.SmokeFault = b);
            args.Data.UpdateIfContain(InBoolKeys.空压机功能区故障, b => ViewModel.MainContentBrnViewModel.AirPumpFault = b);
            args.Data.UpdateIfContain(InBoolKeys.受电弓功能区故障, b => ViewModel.MainContentBrnViewModel.HSCBFault = b);
            args.Data.UpdateIfContain(InBoolKeys.LCU功能区故障, b => ViewModel.MainContentBrnViewModel.LCUFault = b);

            args.Data.UpdateIfContain(InBoolKeys.自动控制, b =>
            {
                if (b)
                {
                    ViewModel.ControlModel = ControlModel.Auto;
                }
            });
            args.Data.UpdateIfContain(InBoolKeys.人工控制, b =>
            {
                if (b)
                {
                    ViewModel.ControlModel = ControlModel.Manual;
                }
            });
        }

        private int m_CurrentPage;
        private int m_MaxPage;
        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            m_CurrentPage = 1;
            m_MaxPage = (ViewModel.AllBorderCastItems.Count + PageNum - 1) / PageNum;
            SetPageInfo();
        }

        private void SetPageInfo()
        {
            ViewModel.PageInfo = $"第{m_CurrentPage}页/共{m_MaxPage}页";
            ViewModel.DisplayItems = ViewModel.AllBorderCastItems.Skip((m_CurrentPage - 1) * PageNum).Take(PageNum);
        }
        private void ConfirmResponse()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public const int PageNum = 10;
        private void NextPageResponse()
        {
            if (m_CurrentPage == m_MaxPage)
            {
                return;
            }
            m_CurrentPage++;
            SetPageInfo();
        }

        private void LastPageResponse()
        {
            if (m_CurrentPage == 1)
            {
                return;
            }
            m_CurrentPage--;
            SetPageInfo();
        }

        /// <summary>
        /// 紧急广播下一页
        /// </summary>
        public ICommand NextPage { get; private set; }

        /// <summary>
        /// 紧急广播上一页
        /// </summary>
        public ICommand LastPage { get; private set; }

        /// <summary>
        /// 紧急广播 确认
        /// </summary>
        public ICommand Confirm { get; private set; }
    }
}