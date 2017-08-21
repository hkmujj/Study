using System.ComponentModel;
using System.Xml.Serialization;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    [XmlRoot]
    public class NetDataPackageConfig : INotifyPropertyChanged, INetDataPackageConfig
    {
        private int m_PackageCount;
        private int m_FloatStartByte;
        private int m_FloatCountOfByte;
        private int m_BoolStartByte;
        private int m_BoolCountOfByte;
        private int m_FloatMappedStartIndex;
        private int m_BoolMappedStartIndex;

        /// <summary>
        /// 数据包数
        /// </summary>
        public int PackageCount
        {
            get { return m_PackageCount; }
            set
            {
                if (value == m_PackageCount)
                {
                    return;
                }
                m_PackageCount = value;
                OnPropertyChanged("PackageCount");
            }
        }

        /// <summary>
        /// float值的起始byte位置
        /// </summary>
        public int FloatStartByte
        {
            get { return m_FloatStartByte; }
            set
            {
                if (value == m_FloatStartByte)
                {
                    return;
                }
                m_FloatStartByte = value;
                OnPropertyChanged("FloatStartByte");
            }
        }

        /// <summary>
        /// float值的byte个数
        /// </summary>
        public int FloatCountOfByte
        {
            get { return m_FloatCountOfByte; }
            set
            {
                if (value == m_FloatCountOfByte)
                {
                    return;
                }
                m_FloatCountOfByte = value;
                OnPropertyChanged("FloatCountOfByte");
            }
        }

        /// <summary>
        /// Bool值的起始byte位置
        /// </summary>
        public int BoolStartByte
        {
            get { return m_BoolStartByte; }
            set
            {
                if (value == m_BoolStartByte)
                {
                    return;
                }
                m_BoolStartByte = value;
                OnPropertyChanged("BoolStartByte");
            }
        }

        /// <summary>
        /// Bool值的byte个数
        /// </summary>
        public int BoolCountOfByte
        {
            get { return m_BoolCountOfByte; }
            set
            {
                if (value == m_BoolCountOfByte)
                {
                    return;
                }
                m_BoolCountOfByte = value;
                OnPropertyChanged("BoolCountOfByte");
            }
        }

        /// <summary>
        /// float值的映射起始地址
        /// </summary>
        public int FloatMappedStartIndex
        {
            get { return m_FloatMappedStartIndex; }
            set
            {
                if (value == m_FloatMappedStartIndex)
                {
                    return;
                }
                m_FloatMappedStartIndex = value;
                OnPropertyChanged("FloatMappedStartIndex");
            }
        }

        /// <summary>
        /// float值的映射起始地址
        /// </summary>
        public int BoolMappedStartIndex
        {
            get { return m_BoolMappedStartIndex; }
            set
            {
                if (value == m_BoolMappedStartIndex)
                {
                    return;
                }
                m_BoolMappedStartIndex = value;
                OnPropertyChanged("BoolMappedStartIndex");
            }
        }

        /// <summary>
        /// 获得float的总个数
        /// </summary>
        /// <returns></returns>
        public int GetTotalFloatCount()
        {
            return FloatCountOfByte * PackageCount / 4;
        }

        /// <summary>
        /// 获得 bool 的总个数
        /// </summary>
        /// <returns></returns>
        public int GetTotalBoolCount()
        {
            return BoolCountOfByte * PackageCount * 8;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}