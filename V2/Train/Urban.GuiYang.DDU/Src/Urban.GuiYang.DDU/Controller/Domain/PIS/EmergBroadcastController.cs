using System;
using System.ComponentModel.Composition;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.PIS.EmergBroadcast;
using Urban.GuiYang.DDU.Utils;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain.PIS
{
    [Export]
    public class EmergBroadcastController : ControllerBase<EmergBroadcastViewModel>
    {
        public EmergBroadcastModel Model
        {
            get { return ViewModel.Model; }
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            Model.ShowingBroadcastCollection = new Lazy<PageWrapper<EmergBroadcastConfig>>(() =>
            {
                var pw = new PageWrapper<EmergBroadcastConfig>(8, null, true);
                pw.Reset(GlobalParam.Instance.EmergBroadcastConfigCollection.Value);
                return pw;
            });

            Model.PageDownCommand = new DelegateCommand(OnPageDown);
            Model.PageUpCommand = new DelegateCommand(OnPageUp);

            Model.SigleBroadcastCommand = new DelegateCommand<CommandParameter>(OnSigleBroadcast);
            Model.CircleBroadcastCommand = new DelegateCommand<CommandParameter>(OnCircleBroadcast);


            Model.SelectedListBoxCommand = new DelegateCommand(OnSelectListBox);


        }

        private void OnSelectListBox()
        {

            ViewModel.Parent.Parent.SendInterface.SelectedListBox(Model);

        }

        private void OnCircleBroadcast(CommandParameter commandParameter)
        {
            ViewModel.Parent.Parent.SendInterface.SendCircleEmergBroadcast((bool)commandParameter.Parameter);
        }

        private void OnSigleBroadcast(CommandParameter commandParameter)
        {
            ViewModel.Parent.Parent.SendInterface.SendSigleEmergBroadcast(commandParameter.Parameter != null && (string)commandParameter.Parameter == "1");
        }

        private void OnPageUp()
        {
            Model.ShowingBroadcastCollection.Value.GotoPrePage();
        }

        private void OnPageDown()
        {
            Model.ShowingBroadcastCollection.Value.GotoNextPage();
        }
    }
}