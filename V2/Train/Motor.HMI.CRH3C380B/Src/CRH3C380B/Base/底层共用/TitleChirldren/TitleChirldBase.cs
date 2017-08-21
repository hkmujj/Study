using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.底层共用.TitleChirldren
{
    public abstract class TitleChirldBase : CommonInnerControlBase
    {
        protected DMITitle Title { private set; get; }

        protected TitleChirldBase(DMITitle title)
        {
            Title = title;
        }
    }
}