using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// �̻𱨾���ť��Ӧ
    /// </summary>
    [Export]
    public class SmokeBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// ��ť���²���
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.�̻𱨾�);
        }
    }
}