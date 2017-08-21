using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Assist
{
    [DebuggerDisplay("Scal={Scal}, Width={Width}")]
    public class DegreeItem :NotificationObject
    {
        /// <summary>
        /// 比例值，最大100
        /// </summary>
        public double Scal { set; get; }

        /// <summary>
        /// 刻度宽
        /// </summary>
        public double Width{ set; get; }

        /// <summary>
        /// 刻度厚
        /// </summary>
        public double Stoke { set; get; }
    }
}