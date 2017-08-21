using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export]
    public class TransferInfoController : ControllerBase<TransferInfoViewModel>
    {
        public TransferInfoController()
        {
            ChangedType = new DelegateCommand<string>(ChangedTypeAction);
            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<ViewChangedEvent>()
                .Subscribe((satte) =>
                {
                    if (satte.Id.ToString().Equals(StateKeys.Root_检修状态_状态_传送信息))
                    {
                        CurrentState = TransferState.CI1;
                        ChangedDisplay();
                    }
                });
        }
        public TransferState CurrentState { get; private set; }
        private void ChangedTypeAction(string obj)
        {
            TransferState state;
            if (Enum.TryParse(obj, true, out state))
            {
                CurrentState = state;
                ChangedDisplay();
            }
        }

        private void ChangedDisplay()
        {
            ViewModel.DisplayOne =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 1).OrderBy(o => o.Number);
            ViewModel.DisplayTwo =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 2).OrderBy(o => o.Number);
            ViewModel.DisplayThree =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 3).OrderBy(o => o.Number);
            ViewModel.DisplayFour =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 4).OrderBy(o => o.Number);
            ViewModel.DisplayFive =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 5).OrderBy(o => o.Number);
            ViewModel.DisplaySix =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 6).OrderBy(o => o.Number);
            ViewModel.DisplaySeven =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 7).OrderBy(o => o.Number);
            ViewModel.DisplayEight =
            ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == 8).OrderBy(o => o.Number);
        }

        //private void ChangedDisplayValue(IEnumerable<TransferInfoUnit> unit, int location)
        //{
        //    unit =
        //       ViewModel.AllCollection.Where(w => w.State == CurrentState && w.Location == location).OrderBy(o => o.Number);
        //}

        public ICommand ChangedType { get; private set; }
    }
}