using System;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.Fault.Detail;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.ViewModel.Domain.Fault;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.Fault
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class FaultManagerController : ControllerBase<Lazy<FaultManagerViewModel>>, IResetSupport
    {
        [ImportingConstructor]
        public FaultManagerController(Lazy<FaultManagerViewModel> viewModel) : base(viewModel)
        {
        }

        public void AddItem(NotifyInfoConfig itemConfig)
        {
            var domain = ViewModel.Value.Parent.Value;
            ViewModel.Value.Model.AllItems.Add(new FaultItem(itemConfig, DateTime.Now,
                domain.Model.MainData.MainStates.WorkState, domain.Model.MainData.OilEnginRotationRate,
                domain.Model.MainData.CurrentSpeed, 0));

            UpdateItems();
        }

        public void ResetItem(NotifyInfoConfig itemConfig)
        {
            foreach (var it in ViewModel.Value.Model.AllItems.Where(w => w.InfoConfig == itemConfig))
            {
                it.FaultState = FaultState.Disappeared;
            }

            UpdateItems();
        }

        public void Reset()
        {
            var model = ViewModel.Value.Model;
            model.AllItems.Clear();
            UpdateItems();
        }

        public void UpdateCurrentFaultPages()
        {
            var model = ViewModel.Value.Model;
            model.CurrentFaultItems.Reset(model.AllItems);
        }

        private void UpdateItems()
        {
            UpdateRootViewPages();
            UpdateCurrentFaultPages();
            UpdateTodayFaultPages();
            UpdateAllFaultPages();
        }

        public void UpdateRootViewPages()
        {
            var model = ViewModel.Value.Model;
            model.RootViewItems.Reset(model.AllItems);
            model.NotifyItems.Reset(model.AllItems);
        }

        public void UpdateAllFaultPages()
        {
            var model = ViewModel.Value.Model;
            model.AllPagedItems.Reset(model.AllItems);
        }

        public void UpdateTodayFaultPages()
        {
            var model = ViewModel.Value.Model;
            model.TodayFaultItems.Reset(model.AllItems);
        }

        public void ChangeToTodayFaults()
        {
            UpdateTodayFaultPages();

            var model = ViewModel.Value.Model;
            model.CurrentFaultDetails = model.TodayFaultItems;
        }

        public void ChangeToAllFaults()
        {
            UpdateAllFaultPages();

            var model = ViewModel.Value.Model;
            model.CurrentFaultDetails = model.AllPagedItems;
        }
    }
}