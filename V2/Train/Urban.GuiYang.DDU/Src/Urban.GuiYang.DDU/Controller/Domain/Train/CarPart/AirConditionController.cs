using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.View.Contents.Contents;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain.Train.CarPart
{
    [Export]
    public class AirConditionController : ControllerBase<TrainViewModel>
    {
        private IRegionManager m_RegionManager;

        private string[] m_ViewNames;

        public DelegateCommand GotoPreCommand { get; private set; }

        public DelegateCommand GotoNextCommand { get; private set; }

        public DelegateCommand<object> ChangeAirConditionModelCommand { get; private set; }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            m_ViewNames = new[]
            {
                typeof (AirConditionPage1ContentView).FullName,
                typeof (AirConditionPage2ContentView).FullName,
                
            };
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            GotoPreCommand = new DelegateCommand(OnGotoPreView);
            GotoNextCommand = new DelegateCommand(OnGotoNextView);
            ChangeAirConditionModelCommand = new DelegateCommand<object>(OnChangeAirConditionModelCommand);
        }

        private void OnChangeAirConditionModelCommand(object s)
        {
            var type = ( AirConditionItemType)s;
            ChangeAirConditionModel(type);
        }

        private void ChangeAirConditionModel(AirConditionItemType type)
        {
            ViewModel.Model.CarCollection.ForEach(
                e =>
                    e.AirCondition.CurrentAirConditionPage1 =
                        e.AirCondition.AirConditionPage1Collection.Value.FirstOrDefault(f => f.ItemType == type));
        }

        public void GotoFirstView()
        {
            ViewModel.Model.AirConditionPageIndex= 0;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.AirConditionPageIndex]);
            this.OnChangeAirConditionModelCommand(AirConditionItemType.Auto);
           
        }

        private void OnGotoNextView()
        {
            ViewModel.Model.AirConditionPageIndex = (ViewModel.Model.AirConditionPageIndex + 1)%2;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.AirConditionPageIndex]);
        }

        private void OnGotoPreView()
        {
            ViewModel.Model.AirConditionPageIndex = (ViewModel.Model.AirConditionPageIndex + 2 - 1)%2;
            m_RegionManager.RequestNavigate(RegionNames.ContentContentContent, m_ViewNames[ViewModel.Model.AirConditionPageIndex]);
        }
    }
}