using System;
using System.ComponentModel.Composition;
using CBTC.Infrasturcture.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Subway.CBTC.QuanLuTong.Constant;

namespace Subway.CBTC.QuanLuTong.Controller.Navigator.Running
{
    [Export]
    public class RunningNavigatorController
    {
        public DelegateCommand<Type> NavigatToCommand { get; private set; }

        private readonly IRegionManager m_RegionManager;

        [ImportingConstructor]
        public RunningNavigatorController(IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            NavigatToCommand = new DelegateCommand<Type>(OnNavigatorTo);
        }

        public void Initalize()
        {
        }

        public void ToDefaultView()
        {
            
        }

        private void OnNavigatorTo(Type obj)
        {
            m_RegionManager.RequestNavigate(RegionNames.RunningContent, obj.FullName);
        }
    }
}