using System.Drawing;
using System.Windows.Forms;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Service
{
    public interface IOpaqueLayerService: IService
    {

        Color BackColor { set; get; }

        int Alpha { get; }

        float LightPencent { set; get; }

        void PaintOpaqueLayer(object sender, PaintEventArgs paintEventArgs);
    }
}