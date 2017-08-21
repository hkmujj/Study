using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.View.ViewModel
{
    public class CommunicationDataServiceItemViewModel : NotificationObject
    {
        private IndexValueDescriptionModel<int, bool> m_SelectedBool;
        private IndexValueDescriptionModel<int, float> m_SelectedFloat;

        public CommunicationDataServiceItemViewModel(IWritableCommunicationDataReadService dataService)
        {
            DataService = dataService;
            

            SetSelectedBool(10);

        }

        public IWritableCommunicationDataReadService DataService { private set; get; }

        public IndexValueDescriptionModel<int, bool> SelectedBool
        {
            set
            {
                if (Equals(value, m_SelectedBool))
                {
                    return;
                }
                m_SelectedBool = value;
                RaisePropertyChanged(() => SelectedBool);
            }
            get { return m_SelectedBool; }
        }

        public IndexValueDescriptionModel<int, float> SelectedFloat
        {
            set
            {
                if (Equals(value, m_SelectedFloat))
                {
                    return;
                }
                m_SelectedFloat = value;
                RaisePropertyChanged(() => SelectedFloat);
            }
            get { return m_SelectedFloat; }
        }

        public void SetSelectedBool(int idx)
        {
            SelectedBool = DataService.BoolList.FirstOrDefault(f => f.Index == idx);
        }

        public void SetSelectedFloat(int idx)
        {
            SelectedFloat = DataService.FloatList.FirstOrDefault(f => f.Index == idx);
        }
    }
}