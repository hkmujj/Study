using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class MonitorDCUDownActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.MonitorViewModel.Model;
            mm.CurrentDCUIndex = (mm.CurrentDCUIndex + 1)%(mm.DCUPageCollection.Value.Count);
        }
    }
}