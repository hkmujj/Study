using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class MonitorEngineDownActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.MonitorViewModel.Model;
            mm.CurrentEngineIndex = ( mm.CurrentEngineIndex + 1 ) % ( mm.EnginePageCollection.Value.Count );
        }
    }
}