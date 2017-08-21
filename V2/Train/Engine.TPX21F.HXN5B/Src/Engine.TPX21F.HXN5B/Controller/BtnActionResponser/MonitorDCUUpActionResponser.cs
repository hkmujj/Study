using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class MonitorDCUUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.MonitorViewModel.Model;
            mm.CurrentDCUIndex = (mm.CurrentDCUIndex + mm.DCUPageCollection.Value.Count - 1)%
                                 (mm.DCUPageCollection.Value.Count);
        }
    }
}