using System;
using System.Drawing;

namespace CRH2MMI.Common.Util
{
    internal interface IUnit
    {
        /// <summary>
        /// 车厢号
        /// </summary>
        [Obsolete("CarNo is not reasonable,use UnitNo")]
        int CarNo { set; get; }

        /// <summary>
        /// 单元号
        /// </summary>
        int UnitNo { set; get; }

        /// <summary>
        /// 实际的轮廓
        /// </summary>
        Rectangle ActureOutLine {  get; }

    }

}
