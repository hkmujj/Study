using System.Windows.Forms;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    public partial class ForecationInfoControlBase : UserControl
    {
        private IPlanSectionCoordinate m_PlanSectionCoordinate;

        public IPlanSectionCoordinate PlanSectionCoordinate
        {
            set
            {
                m_PlanSectionCoordinate = value;
                OnPlanSectionCoordinateChanged();
                Invalidate();
            }
            get { return m_PlanSectionCoordinate; }
        }


        protected ForecationInfoControlBase()
        {
            InitializeComponent();
        }

        protected virtual void OnPlanSectionCoordinateChanged()
        {

        }

    }
}
