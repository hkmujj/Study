using System.Collections.Generic;
using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DevExpress.Mvvm;
using LightRail.HMI.SZLHLF.Resources;
using LightRail.HMI.SZLHLF.Enum;
using System.Windows.Input;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export(typeof(InfoSetController))]
    public class InfoSetController : ControllerBase<InfoSetModel>
    {
        [ImportingConstructor]
        public InfoSetController(IEventAggregator eventAggregator)
        {
            LanguageChangeCommand = new DelegateCommand(ChangeLangAction);
            ExitCommand = new DelegateCommand(ExitAction);
        }

        private void ChangeLangAction()
        {
            ViewModel.NewIsCh = !ViewModel.NewIsCh;
            if (ViewModel.NewIsCh)
            {
                SZLHLFResourceManager.ChangedLanguage(Language.En);
                return;
            }
            SZLHLFResourceManager.ChangedLanguage(Language.Ch);
        }
        private void ExitAction()
        {
            //ViewModel.ViewModel.Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
            ViewModel.CurIsCh = ViewModel.NewIsCh;
        }

        public ICommand LanguageChangeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public Dictionary<string, string> InitDicToDictionary;
    }
}
