using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Annotations;

namespace CommonUtil.Model
{
    /// <summary>
    /// 可xml序列化的 Point 
    /// </summary>
    public class XmlPoint : INotifyPropertyChanged
    {
        private  Point m_Point;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int X
        {
            set
            {
                m_Point = new Point(value, m_Point.Y);
                RaisePropertyChanged("X");
            }
            get { return Point.X; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Y
        {
            set
            {
                m_Point = new Point(m_Point.X, value);
                RaisePropertyChanged("Y");
            }
            get { return Point.Y; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point Point
        {
            get { return m_Point; }
        }

        /// <summary>
        /// 
        /// </summary>
        public XmlPoint()
        {
            m_Point = new Point();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}