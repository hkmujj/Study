using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    
    public class LightSettingBackBtnResponse :  BtnResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.维修菜单);
        }
    }
}
