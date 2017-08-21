using ATP200C.MainView;
using ATP200C.PopupViews;

namespace ATP200C
{
    public class GlobalParam
    {
        public TrainInfo TrainInfo = new TrainInfo();
        public static GlobalParam Instance { get; private set; }
        public InputDriverIDViewItem DriverIdViewItem { get; set; }
        public InputTrainLineViewItem TrainLineViewItem { get; set; }
        static GlobalParam()
        {
            Instance = new GlobalParam();
        }
    }
}