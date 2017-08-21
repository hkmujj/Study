using System.Linq;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.SignalInfo
{
    public class DI1View : SingalInfoChild
    {
        public DI1View(NetSiganlInfoView parent) : base(parent)
        {
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            SetItemConfigs(Parent.ContentConfigs.Where(w => w.GroupName == "DI1"));
        }
    }
}