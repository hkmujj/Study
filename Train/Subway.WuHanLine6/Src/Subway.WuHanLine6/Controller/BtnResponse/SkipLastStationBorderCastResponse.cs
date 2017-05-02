using System.ComponentModel.Composition;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 上一站跳站
    /// </summary>
    [Export]
    public class SkipLastStationBorderCastResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            OutBoolKeys.OutBo向后跳站.SendBoolData(true, true);
        }
    }
}