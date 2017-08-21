using System.ComponentModel.Composition;
using Engine._6A.Constance;
using Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(ContractName.TrainUp, typeof(IForTheColumnTwoViewModel))]
    [Export(ContractName.TrainDown, typeof(IForTheColumnTwoViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ForTheColumnTwoViewModel : ViewModelBase, IForTheColumnTwoViewModel
    {

    }
}