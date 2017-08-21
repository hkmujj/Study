namespace Motor.HMI.CRH1A.ButtomNotify
{
    public class ButtomNotifyModelWrapper
    {
        public ButtomNotifyModelWrapper(ButtomNotifyModel notifyModel)
        {
            NotifyModel = notifyModel;
        }

        public ButtomNotifyModel NotifyModel { private set; get; }

        public bool Responsed { set; get; }
    }
}