using CRH2MMI.Common.Config;

namespace CRH2MMI.Common
{
    internal interface INavigateAware
    {
        void NavigateInCurrent(ViewConfig current);

        void NavigateTo(ViewConfig toThis);

        void NavigateFrom(ViewConfig fromOhter);
    }
}