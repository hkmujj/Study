using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.Train
{
    [Export]
    public class StationModel : NotificationObject
    {
        private string m_NextStation;
        private string m_EndStatiion;

        public string NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }

                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        public string EndStatiion
        {
            get { return m_EndStatiion; }
            set
            {
                if (value == m_EndStatiion)
                {
                    return;
                }

                m_EndStatiion = value;
                RaisePropertyChanged(() => EndStatiion);
            }
        }
    }
}