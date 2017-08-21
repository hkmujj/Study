using System.Diagnostics;

namespace CRH2MMI.Common.View.Train
{
    [DebuggerDisplay("CarNo={CarNo}  Width={Width}")]
    class CarWidthModel
    {
        public CarWidthModel()
        {

        }

        [DebuggerStepThrough]
        public CarWidthModel(int carno, int width)
        {
            CarNo = carno;
            Width = width;
        }

        public int CarNo { set; get; }
        public int Width { set; get; }
    }
}
