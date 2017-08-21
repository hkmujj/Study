using CRH2MMI.Common.Global;
using MMI.Facility.Interface;

namespace CRH2MMI.CutState
{
    /// <summary>
    /// 一个车一个受电弓
    /// </summary>
    class RemovalStateContentView1Acc : RemovalStateContentViewBase
    {
        public override void Initalize(baseClass viewClass)
        {
            CreateGridAndTitle(viewClass);

            GlobalEvent.Instance.ReversalChanged += (sender, args) => StateGrid.Reverse();
        }

    }
}
