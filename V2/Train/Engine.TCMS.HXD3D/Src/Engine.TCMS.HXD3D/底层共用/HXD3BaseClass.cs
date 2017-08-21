using System.Linq;
using Engine.TCMS.HXD3D.Fault.Ensure;
using Engine.TCMS.HXD3D.Title;
using MMI.Facility.Interface;

namespace Engine.TCMS.HXD3D.底层共用
{
    public class HXD3BaseClass : baseClass
    {
        public TopTitleScreen TopTitleScreen { get; private set; }

        public FaultEnsure FaultEnsure { get; private set; }

        /// <summary>对象初始化</summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public sealed override bool init(ref int nErrorObjectIndex)
        {
            TopTitleScreen = ObjectService.GetObject<TopTitleScreen>(ProjectName).FirstOrDefault();
            FaultEnsure = ObjectService.GetObject<FaultEnsure>(ProjectName).FirstOrDefault();

            Initalize();

            return true;
        }

        protected virtual void Initalize()
        {
            
        }
    }
}