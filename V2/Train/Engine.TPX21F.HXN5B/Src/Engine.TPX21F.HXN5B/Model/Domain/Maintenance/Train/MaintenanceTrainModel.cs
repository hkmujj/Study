using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train
{
    [Export]
    public class MaintenanceTrainModel : NotificationObject
    {
        [ImportingConstructor]
        public MaintenanceTrainModel(WhellRModel whellRModel, OperConsleSelectModel operConsleSelectModel)
        {
            WhellRModel = whellRModel;
            OperConsleSelectModel = operConsleSelectModel;
        }

        public WhellRModel WhellRModel { get; private set; }

        public OperConsleSelectModel OperConsleSelectModel { get; private set; }
    }
}