using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// ��·���������ť����
    /// </summary>
    [Export]
    public class BypassHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// ��ť���²���
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.��·�������);
        }
    }
}