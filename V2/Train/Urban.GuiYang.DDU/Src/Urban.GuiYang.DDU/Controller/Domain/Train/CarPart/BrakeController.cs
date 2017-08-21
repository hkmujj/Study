using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Contents;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain.Train.CarPart
{
    [Export]
    public class BrakeController : ControllerBase<TrainViewModel>
    {
        private IRegionManager m_RegionManager;

        private string[] m_ViewNames;

        public DelegateCommand GotoPreCommand { get; private set; }

        public DelegateCommand GotoNextCommand { get; private set; }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            m_ViewNames = new[]
            {
                typeof (BrakePage1ContentView).FullName,
                typeof (BrakePage2ContentView).FullName,
                typeof (BrakePage3ContentView).FullName
            };
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            GotoPreCommand = new DelegateCommand(OnGotoPreView);
            GotoNextCommand = new DelegateCommand(OnGotoNextView);
        }

        public void GotoFirstView()
        {
            ViewModel.Model.BrakePageIndex = 0;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.BrakePageIndex]);
        }

        private void OnGotoNextView()
        {
            ViewModel.Model.BrakePageIndex = (ViewModel.Model.BrakePageIndex + 1)%3;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.BrakePageIndex]);
        }

        private void OnGotoPreView()
        {
            ViewModel.Model.BrakePageIndex = (ViewModel.Model.BrakePageIndex + 3 - 1)%3;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.BrakePageIndex]);
        }
    }
}