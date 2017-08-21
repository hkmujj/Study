using System.Drawing;
using CRH2MMI.Common.View.Train;
using CRH2MMI.PowerClassify.PowerStates;

namespace CRH2MMI.PowerClassify
{
    internal interface IPowerUnit
    {

        TrainUnit Unit { set; get; }

        bool IsPowerOn { set; get; }

        Color Color { set; get; }

        PowerFrom PowerFrom { set; get; }

        bool TextVisible { set; get; }

        void OnDraw(Graphics g);

        void Refresh();

        void RefreshByState(PowerFrom powerFrom);
    }
}
