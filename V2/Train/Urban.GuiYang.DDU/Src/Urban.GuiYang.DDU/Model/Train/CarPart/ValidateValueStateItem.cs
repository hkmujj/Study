using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class ValidateValueStateItem : NotificationObject
    {
        private bool m_GeneralState;
        private float m_Value1;
        private float m_Value2;

        public float Value2
        {
            get { return m_Value2; }
            set
            {
                if (value.Equals(m_Value2))
                {
                    return;
                }
                m_Value2 = value;
                RaisePropertyChanged(() => Value2);
            }
        }
        public bool GeneralState
        {
            get { return m_GeneralState; }
            set
            {
                if (value.Equals(m_GeneralState))
                {
                    return;
                }
                m_GeneralState = value;
                RaisePropertyChanged(() => GeneralState);
            }
        }

        public float Value1
        {
            get { return m_Value1; }
            set
            {
                if (value.Equals(m_Value1))
                {
                    return;
                }
                m_Value1 = value;
                RaisePropertyChanged(() => Value1);
            }
        }
    }
}