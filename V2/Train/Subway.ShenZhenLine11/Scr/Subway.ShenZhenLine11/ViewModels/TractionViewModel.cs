using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using CommonUtil.Annotations;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Enum;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(TractionViewModel))]
    public class TractionViewModel : SubViewModelBase
    {
        public TractionViewModel()
        {
            var tmp = ExcelParser.Parser<TractionUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            TractionCollection = new ObservableCollection<TractionUnit>(tmp);
        }
        public ObservableCollection<TractionUnit> TractionCollection { get; set; }
        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(sender, args);
            foreach (var unit in TractionCollection)
            {
                unit.Changed(sender, args);
            }
        }

        public override void Clear()
        {
            base.Clear();
            foreach (var unit in TractionCollection)
            {
                unit.Status = Traction.Normal;
            }
        }
    }
}