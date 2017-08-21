using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.CBTC.THALES.Model.TempModel
{
    [Export]
    public class RunningModel : NotificationObject
    {
        public const int DriverIdMaxLength = 6;

        private string m_InputtingDriverId;
        //private double m_InputtingVolumn;
        //private double m_InputtingLight;

        public string InputtingDriverId
        {
            get { return m_InputtingDriverId; }
            set
            {
                if (value == m_InputtingDriverId)
                    return;

                m_InputtingDriverId = value;
                RaisePropertyChanged(() => InputtingDriverId);
            }
        }

        //public double InputtingLight
        //{
        //    get { return m_InputtingLight; }
        //    set
        //    {
        //        if (value.Equals(m_InputtingLight))
        //            return;

        //        m_InputtingLight = value;
        //        RaisePropertyChanged(() => InputtingLight);
        //    }
        //}

        //public double InputtingVolumn
        //{
        //    get { return m_InputtingVolumn; }
        //    set
        //    {
        //        if (value.Equals(m_InputtingVolumn))
        //            return;

        //        m_InputtingVolumn = value;
        //        RaisePropertyChanged(() => InputtingVolumn);
        //    }
        //}
    }
}