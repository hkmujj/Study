using System.Drawing;
using System.Windows.Forms;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOpaqueLayerService: IService
    {

        /// <summary>
        /// 
        /// </summary>
        Color BackColor { set; get; }

        /// <summary>
        /// 
        /// </summary>
        int Alpha { get; }

        /// <summary>
        /// 
        /// </summary>
        float LightPencent { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="paintEventArgs"></param>
        void PaintOpaqueLayer(object sender, PaintEventArgs paintEventArgs);
    }
}