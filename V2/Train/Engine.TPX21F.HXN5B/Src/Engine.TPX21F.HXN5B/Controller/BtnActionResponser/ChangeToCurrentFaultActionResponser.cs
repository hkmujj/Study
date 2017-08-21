using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToCurrentFaultActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.Domain.FaultManagerViewModel.Controller.UpdateCurrentFaultPages();

            NavigateTo(StateKeys.Root_���ϲ�ѯ��ǰ�¼�);
        }
    }
}