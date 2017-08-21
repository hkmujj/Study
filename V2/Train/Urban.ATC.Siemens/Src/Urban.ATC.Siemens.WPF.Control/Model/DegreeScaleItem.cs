using Microsoft.Practices.Prism.ViewModel;

namespace RailwaySimulation.Motor.ATP.View.Model
{
    public class DegreeScaleItem : NotificationObject
    {
        private double m_Value;
        private double m_Angle;
        private double m_Lenght;
        private string m_Text;
        private double m_AngleIncrement;

        /// <summary>
        /// 值
        /// </summary>
        public double Value
        {
            set { m_Value = value; RaisePropertyChanged(() => Value); }
            get { return m_Value; }
        }

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle
        {
            set { m_Angle = value; RaisePropertyChanged(() => Angle); }
            get { return m_Angle; }
        }

        /// <summary>
        /// 相对于0值的角度增量
        /// </summary>
        public double AngleIncrement
        {
            set { m_AngleIncrement = value; RaisePropertyChanged(() => AngleIncrement); }
            get { return m_AngleIncrement; }
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