using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;

namespace General.FlashLoader.ConfigEditer.Model
{
    [XmlType]
    public class ExcelConfig : NotificationObject
    {
        private string m_File;
        private string m_Sheet;
        private string m_Name;
        private string m_Index;

        public string File
        {
            set
            {
                if (value == m_File)
                {
                    return;
                }
                m_File = value;
                RaisePropertyChanged(() => File);
            }
            get { return m_File; }
        }

        public string Sheet
        {
            set
            {
                if (value == m_Sheet)
                {
                    return;
                }
                m_Sheet = value;
                RaisePropertyChanged(() => Sheet);
            }
            get { return m_Sheet; }
        }

        public string Name
        {
            set
            {
                if (value == m_Name)
                {
                    return;
                }
                m_Name = value;
                RaisePropertyChanged(() => Name);
            }
            get { return m_Name; }
        }

        public string Index
        {
            set
            {
                if (value == m_Index)
                {
                    return;
                }
                m_Index = value;
                RaisePropertyChanged(() => Index);
            }
            get { return m_Index; }
        }
    }
}