using System.ComponentModel.Composition;
using System.Linq;
using Excel.Interface;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.XiaMenLine1.Subsystem.Model;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.Controller
{
    [Export]
    public class TractionLockController : ControllerBase<TractionLockViewModel>
    {
        public override void Initalize()
        {
            ViewModel.Model.TractionLockUnitCollection = ExcelParser.Parser<TractionLockUnit>(SubsysParams.Instance.SubsystemInitParam.AppConfig.AppPaths.ConfigDirectory).ToList();
        }
    }
}