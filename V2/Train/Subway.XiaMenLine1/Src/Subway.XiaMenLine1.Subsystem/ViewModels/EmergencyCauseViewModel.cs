using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Subsystem.Model;

namespace Subway.XiaMenLine1.Subsystem.ViewModels
{
    [Export]
    public class EmergencyCauseViewModel : NotificationObject, IDisposable
    {
        public ObservableCollection<EnmergencyCauseUnit> EnmergencyCause { get; set; }

        public EmergencyCauseViewModel()
        {
            var tmp =
                ExcelParser.Parser<EnmergencyCauseUnit>(
                    SubsysParams.Instance.SubsystemInitParam.AppConfig.AppPaths.ConfigDirectory);
            EnmergencyCause = new ObservableCollection<EnmergencyCauseUnit>(tmp);
        }

        public void Dispose()
        {
            EnmergencyCause.ToList().ForEach(f => f.IsEmergency = false);
        }
    }
}