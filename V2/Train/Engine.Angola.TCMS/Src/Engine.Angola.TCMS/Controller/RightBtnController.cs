using System.ComponentModel.Composition;
using Engine.Angola.TCMS.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Microsoft.Practices.Prism.Commands;

namespace Engine.Angola.TCMS.Controller
{
    [Export]
    public class RightBtnController : ControllerBase<RightBtnViewModel>
    {
        public void Initalize()
        {
            ViewModel.Model.F1Command = new DelegateCommand(OnF1Excute);
            ViewModel.Model.F2Command = new DelegateCommand(OnF2Excute);
            ViewModel.Model.F3Command = new DelegateCommand(OnF3Excute);
            ViewModel.Model.F4Command = new DelegateCommand(OnF4Excute);
            ViewModel.Model.F5Command = new DelegateCommand(OnF5Excute);
        }

        protected AngolaTCMSShellViewModel ParentViewModel
        {
            get { return ViewModel.Parent.Value; }
        }

        private void OnF1Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF1.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF1.ActionResponser.ResponseClick();
            }
        }

        private void OnF2Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF2.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF2.ActionResponser.ResponseClick();
            }
        }

        private void OnF3Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF3.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF3.ActionResponser.ResponseClick();
            }
        }

        private void OnF4Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF4.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF4.ActionResponser.ResponseClick();
            }
        }

        private void OnF5Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnF5.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnF5.ActionResponser.ResponseClick();
            }
        }
    }
}
