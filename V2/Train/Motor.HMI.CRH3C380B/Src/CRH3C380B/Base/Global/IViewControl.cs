using System.Drawing;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.Global
{
    public interface IViewControl
    {
        void Initalize(CRH3C380BBase source);

        void Refresh(CRH3C380BBase source, Graphics graphics);

        void Reset();
    }
}