using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class MonitorEngineUpActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            var mm = ViewModel.Value.Domain.MonitorViewModel.Model;
            mm.CurrentEngineIndex = ( mm.CurrentEngineIndex + mm.EnginePageCollection.Value.Count - 1 ) %
                                    ( mm.EnginePageCollection.Value.Count );
        }
    }
}