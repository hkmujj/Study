using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// ���а�������4
    /// </summary>
    [Export]
    public class RunHelpBtnResponse4 : BtnResponseBase
    {
        /// <summary>
        /// ��ť���²���
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.���н������4);
        }
    }
}