using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine7.Envets;
using Subway.ShenZhenLine7.Extention;
using Subway.ShenZhenLine7.Resource.Keys;
using Subway.ShenZhenLine7.ViewModels;

namespace Subway.ShenZhenLine7.Controller.ViewModelController
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
        }

        private void BooChanged(DataChangedEventArgs<bool> args)
        {
            args.Data.UpdateIfContain(InBoolKeys.列车运行方向右, b => ViewModel.IsRunCar6 = b);
            args.Data.UpdateIfContain(InBoolKeys.列车运行方向左, b => ViewModel.IsRunCar1 = b);
            args.Data.UpdateIfContain(InBoolKeys.列车预开门侧右, b => ViewModel.IsRightOpenDoor = b);
            args.Data.UpdateIfContain(InBoolKeys.列车预开门侧左, b => ViewModel.IsLeftOpenDoor = b);
            args.Data.UpdateIfContain(InBoolKeys.司机室激活1, b => ViewModel.IsActiveCar1 = b);
            args.Data.UpdateIfContain(InBoolKeys.司机室激活6, b => ViewModel.IsActiveCar6 = b);


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
            ViewModel.PageInfo = string.Format("第{0}页/共{1}页", m_CurrentPage, m_MaxPage);
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