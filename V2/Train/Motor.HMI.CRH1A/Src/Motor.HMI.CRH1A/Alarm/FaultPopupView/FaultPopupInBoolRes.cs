namespace Motor.HMI.CRH1A.Alarm.FaultPopupView
{
    public class FaultPopupInBoolRes
    {
        public static int GetRestartIndex(GT_FaultPopupView popupView)
        {
            return popupView.UIObj.InBoolList[0];
        }
    }
}
