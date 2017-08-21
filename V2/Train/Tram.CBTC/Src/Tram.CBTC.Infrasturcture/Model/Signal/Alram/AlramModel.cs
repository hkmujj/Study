using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Signal.Alram
{
    /// <summary>
    /// 报警模型
    /// </summary>
    public class AlramModel<T>:NotificationObject
    {
        private T m_Value;
        private float m_Distance;
        private string m_Text;
        private bool m_Visible;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public T Value
        {
            get { return m_Value; }
            set
            {
                if (Equals(value, m_Value))
                {
                    return;
                }
                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        /// <summary>
        /// 距离
        /// </summary>
        public float Distance
        {
            get { return m_Distance; }
            set
            {
                if (value.Equals(m_Distance))
                {
                    return;
                }
                m_Distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }

        /// <summary>
        /// 文字
        /// </summary>
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (value == m_Text)
                {
                    return;
                }
                m_Text = value;
                RaisePropertyChanged(() => Text);
            }
        }
    }
}
