using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.Turkmenistan.Controller
{
    [Export]
    public class HardwareBtnController : ControllerBase<HardwareBtnViewModel>
    {
        public override void Initalize()
        {
            ViewModel.Model.B1Command = new DelegateCommand(OnB1Excute);
            ViewModel.Model.B2Command = new DelegateCommand(OnB2Excute);
            ViewModel.Model.B3Command = new DelegateCommand(OnB3Excute);
            ViewModel.Model.B4Command = new DelegateCommand(OnB4Excute);
            ViewModel.Model.B5Command = new DelegateCommand(OnB5Excute);

        }

        protected DomainViewModel ParentViewModel
        {
            get { return ViewModel.Parent.Value; }
        }

        private void OnB1Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF1.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF1.ActionResponser.ResponseClick();
            }
        }

        private void OnB2Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF2.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF2.ActionResponser.ResponseClick();
            }
        }

        private void OnB3Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF3.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF3.ActionResponser.ResponseClick();
            }
        }

        private void OnB4Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF4.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF4.ActionResponser.ResponseClick();
            }
        }

        private void OnB5Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF5.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF5.ActionResponser.ResponseClick();
            }
        }
    }
}