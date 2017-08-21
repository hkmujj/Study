using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Engine.TAX2.SS7C.Model.Interface;
using Engine.TAX2.SS7C.ViewModel.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class OtherController : ControllerBase<OtherViewModel>, IResetSupport
    {
        private DispatcherTimer m_UpdateCurrentTimeTimer;
        private int m_CurrentLightIndex;

        private static readonly double[] LightConstants = {0, 0.2, 0.6};

        private int CurrentLightIndex
        {
            set
            {
                m_CurrentLightIndex = value;
                if (ViewModel != null)
                {
                    ViewModel.Model.OpacityPercent = LightConstants[CurrentLightIndex];
                }
            }
            get { return m_CurrentLightIndex; }
        }

        public ModifyTimeController ModifyTimeController { get; private set; }

        [ImportingConstructor]
        public OtherController()
        {
            
        }

        public override void Initalize()
        {
            ModifyTimeController = new ModifyTimeController(this) {ViewModel = ViewModel};

            m_UpdateCurrentTimeTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Normal,
                OnUpdateCurrentTime, Application.Current.Dispatcher);
            Reset();
        }

        public void ChangeLight()
        {
            CurrentLightIndex = (CurrentLightIndex + LightConstants.Length - 1)%LightConstants.Length;
        }



        private void OnUpdateCurrentTime(object sender, EventArgs eventArgs)
        {
            ViewModel.Model.SimTime = DateTime.Now;
        }

        public void Reset()
        {
            ViewModel.Model.AdjustSpan = TimeSpan.Zero;
            CurrentLightIndex = 1;

            ModifyTimeController.Reset();
        }
    }
}