using CommonUtil.Controls;

namespace CRH2MMI.TrainLineNo
{
    interface ITranLinePage : IInnerControl
    {
        void Reset();

        string TitleText { get; }
    }
}
