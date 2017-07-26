using System;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Monitor;
using Motor.ATP.Infrasturcture.ViewModel;

namespace Motor.ATP.Infrasturcture.Controller.Monitor
{
    public class MonitorController : ControllerBase<MonitorViewModel>
    {
        private readonly ViewControlController m_ViewControlController;

        public ATPDomain ATP { get { return ViewModel.Domain; } }

        public MonitorController()
        {
            m_ViewControlController = new ViewControlController();
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.MonitorMessage.PropertyChanged += MonitorMessageOnPropertyChanged;
            ViewModel.Model.ForecastInformationItems =
                ATP.ForecastInformation.ForecastInformationItems.Cast<ForecastInformationItem>()
                    .ToList()
                    .AsReadOnly();

            m_ViewControlController.ViewModel = ViewModel;
            m_ViewControlController.Initalize();
        }

        private void MonitorMessageOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            GenerateMessage(ViewModel.Model.MonitorMessage.MessageId);
        }


        private void GenerateMessage(int messageId)
        {
            var inf = (InfomationCreater)ATP.InfomationService.GetInformationCreater();

            inf.ClearContents();

            inf.UpdateInfomation(Math.Abs(messageId), ViewModel.Model.MonitorMessage.UpdateType);

            inf.FlushInfomation();
        }
    }
}