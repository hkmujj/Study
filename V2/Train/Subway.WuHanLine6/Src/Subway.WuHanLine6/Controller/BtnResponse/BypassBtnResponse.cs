using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// ��·��Ϣ��ť��Ӧ
    /// </summary>
    [Export]
    public class BypassBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// ��ť���²���
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.��·״̬);
        }
    }
}