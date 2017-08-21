using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Brake;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance
{
    [Export]
    public class MaintenanceModel : NotificationObject
    {
        [ImportingConstructor]
        public MaintenanceModel(MaintenanceBrakeModel brakeModel, MaintenanceTrainModel trainModel)
        {
            BrakeModel = brakeModel;
            TrainModel = trainModel;
        }

        public MaintenanceBrakeModel BrakeModel { get; private set; }

        public MaintenanceTrainModel TrainModel { get; private set; }

        private void t()
        {
            
        }

        private string m_InputingPassword;

        public string InputingPassword
        {
            set
            {
                if (value == m_InputingPassword)
                {
                    return;
                }

                m_InputingPassword = value;
                RaisePropertyChanged(() => InputingPassword);
            }
            get { return m_InputingPassword; }
        }
    }
}
