using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;

namespace General.FlashLoader.ConfigEditer.Model
{
    [XmlType]
    public class DataInterface : NotificationObject
    {
        private ExcelConfig m_InBool;
        private ExcelConfig m_OutFloat;
        private ExcelConfig m_InFloat;
        private ExcelConfig m_OutBool;

        public ExcelConfig InBool
        {
            set
            {
                if (Equals(value, m_InBool))
                {
                    return;
                }
                m_InBool = value;
                RaisePropertyChanged(() => InBool);
            }
            get { return m_InBool; }
        }

        public ExcelConfig OutBool
        {
            set
            {
                if (Equals(value, m_OutBool))
                {
                    return;
                }
                m_OutBool = value;
                RaisePropertyChanged(() => OutBool);
            }
            get { return m_OutBool; }
        }

        public ExcelConfig InFloat
        {
            set
            {
                if (Equals(value, m_InFloat))
                {
                    return;
                }
                m_InFloat = value;
                RaisePropertyChanged(() => InFloat);
            }
            get { return m_InFloat; }
        }

        public ExcelConfig OutFloat
        {
            set
            {
                if (Equals(value, m_OutFloat))
                {
                    return;
                }
                m_OutFloat = value;
                RaisePropertyChanged(() => OutFloat);
            }
            get { return m_OutFloat; }
        }
    }
}