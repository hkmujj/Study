using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 到站广播按钮响应
    /// </summary>
    [Export]
    public class ArriveStationBorderCastResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            OutBoolKeys.OutBo到站广播.SendBoolData(true, true);
        }
    }
}