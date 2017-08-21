using System.Linq;
using System.Windows;
using Engine._6A.Config;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common
{
    public class FaultAdapter : ModelAdapterBase
    {
        public FaultAdapter(IEngineAdapter adapter) : base(adapter)
        {
            Adapter.FaultManage.CurrentInfoChanged += FaultManage_CurrentInfoChanged;
            Adapter.Model.Fault.FaultManage = adapter.FaultManage;
            Adapter.FaultManage.DispalyInfoChnged += FaultManage_DispalyInfoChnged;

        }
        void FaultManage_DispalyInfoChnged()
        {
            Adapter.Model.Fault.PageInfo = Adapter.FaultManage.GetCurrent().Count == 0 ? string.Empty : string.Format("第{0}页 / 共{1}页", Adapter.FaultManage.CurrentPage, Adapter.FaultManage.MaxPage);
            Adapter.Model.Fault.ButtonVisibility = string.IsNullOrEmpty(Adapter.Model.Fault.PageInfo)
                ? Visibility.Hidden
                : Visibility.Visible;
            Adapter.Model.Fault.CurrentFalutInfos = Adapter.FaultManage.GetCurrent();
        }

        void FaultManage_CurrentInfoChanged()
        {

        }
        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(args);
            foreach (var key in args.NewValue.Keys.Where(key => Adapter.Model.Fault.FaultManage.AllInfo.ContainsKey(key)))
            {
                if (GlobalParam.InitParam.CommunicationDataService.ReadService.GetBoolAt(key))
                {
                    Adapter.Model.Fault.FaultManage.Add(key);
                }
                else
                {
                    Adapter.Model.Fault.FaultManage.Remove(key);
                }

            }
        }
    }
}