using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.NRIET.Model.RegionRadar
{
    public class DistanceLineItem : NotificationObject
    {
        private double m_Value;
        private double m_Location;
        private double m_Lenght;
        private string m_Text;

        /// <summary>
        /// 值
        /// </summary>
        public double Value
        {
            set { m_Value = value; RaisePropertyChanged(() => Value); }
            get { return m_Value; }
        }

        /// <summary>
        /// 位置
        /// </summary>
        public double Location
        {
            set{ m_Location = value; RaisePropertyChanged(() => Location);}
            get{return m_Location;}
        }

        /// <summary>
        /// 刻度线的长度
        /// </summary>
        public double Lenght
        {
            set { m_Lenght = value; RaisePropertyChanged(() => Lenght); }
            get { return m_Lenght; }
        }

        public string Text
        {
            set { m_Text = value; RaisePropertyChanged(() => Text); }
            get { return m_Text; }
        }
    }
}