using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.View.ViewModel
{
    public class CommunicationDataFacadeServiceViewModel : NotificationObject
    {
        private CommunicationDataServiceViewModel m_SelectedViewModel;
        public CommunicationDataFacadeServiceBase DataFacadeService { private set; get; }

        public List<CommunicationDataServiceViewModel> CommunicationDataServiceModelCollection { private set; get; }

        public CommunicationDataServiceViewModel SelectedViewModel
        {
            set
            {
                if (Equals(value, m_SelectedViewModel))
                {
                    return;
                }
                m_SelectedViewModel = value;
                RaisePropertyChanged(() => SelectedViewModel);
            }
            get { return m_SelectedViewModel; }
        }

        public CommunicationDataFacadeServiceViewModel(IDataPackage dataPackage, CommunicationDataFacadeServiceBase dataFacadeService)
        {
            DataFacadeService = dataFacadeService;
            CommunicationDataServiceModelCollection = new List<CommunicationDataServiceViewModel>();
            var indexService = dataPackage.ServiceManager.GetService<IIndexDescriptionService>();

            foreach (var interpreter in indexService.AllIndexInterpreters)
            {
                var dataService = dataFacadeService.GetCommunicationDataService(interpreter.Key);
                if (dataService!= null)
                {
                    CommunicationDataServiceModelCollection.Add(new CommunicationDataServiceViewModel(interpreter.Key, dataService));
                }
            }

            SelectedViewModel = CommunicationDataServiceModelCollection.FirstOrDefault();
        }

        public void SetSelectedType(ProjectType projectType, string appName)
        {
            var key = new CommunicationDataKey(projectType, appName);
            SelectedViewModel = CommunicationDataServiceModelCollection.FirstOrDefault(f => f.CommunicationDataKey.Equals(key));
        }

        public void SetSelectedBoolIndex(int idx)
        {
            if (SelectedViewModel != null)
            {
                SelectedViewModel.InData.SetSelectedBool(idx);
            }
        }

        public void SetSelectedFloatIndex(int idx)
        {
            if (SelectedViewModel != null)
            {
                SelectedViewModel.InData.SetSelectedFloat(idx);
            }
        }
    }
}