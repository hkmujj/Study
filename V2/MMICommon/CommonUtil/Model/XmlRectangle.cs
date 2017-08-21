using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Annotations;
using CommonUtil.Util;

namespace CommonUtil.Model
{
    /// <summary>
    /// 可xml序列化的 Rectangle
    /// </summary>
    [XmlType("Rectangle")]
    public class XmlRectangle : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("Rectangle")]
        public string RectangleXmlString
        {
            set
            {
                var datas = value.Split(new[] {','});

                if (datas.Length == 4)
                {
                    m_Rectangle = new Rectangle(datas[0].AsInt(), datas[1].AsInt(), datas[2].AsInt(), datas[3].AsInt());
                }

            }
            get
            {
                return string.Format("{0},{1},{2},{3}", m_Rectangle.X, m_Rectangle.Y, m_Rectangle.Width, m_Rectangle.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("Location")]
        public string LocationXmlString
        {
            set
            {
                var datas = value.Split(new[] { ',' });

                if (datas.Length == 2)
                {
                    m_Rectangle = new Rectangle(datas[0].AsInt(), datas[1].AsInt(), m_Rectangle.Width, m_Rectangle.Height);
                }

            }
            get
            {
                return string.Format("{0},{1}", m_Rectangle.X, m_Rectangle.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("Size")]
        public string SizeXmlString
        {
            set
            {
                var datas = value.Split(new[] { ',' });

                if (datas.Length == 2)
                {
                    m_Rectangle = new Rectangle(m_Rectangle.X, m_Rectangle.Y, datas[0].AsInt(), datas[1].AsInt());
                }

            }
            get
            {
                return string.Format("{0},{1}", m_Rectangle.Width, m_Rectangle.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int X
        {
            set
            {
                m_Rectangle.X = value;
                OnPropertyChanged("X");
            }
            get { return m_Rectangle.X; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Y
        {
            set
            {
                m_Rectangle.Y = value;
                OnPropertyChanged("Y");
            }
            get { return m_Rectangle.Y; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Width
        {
            set
            {
                m_Rectangle.Width = value;
                OnPropertyChanged("Width");
            }
            get { return m_Rectangle.Width; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public int Height
        {
            set
            {
                m_Rectangle.Height = value;
                OnPropertyChanged("Height");
            }
            get { return m_Rectangle.Height; }
        }

        [XmlIgnore] 
        private Rectangle m_Rectangle;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Rectangle Rectangle { set { m_Rectangle = value; }  get { return m_Rectangle; }}


        /// <summary>
        /// 
        /// </summary>
        public XmlRectangle()
        {
            m_Rectangle = new Rectangle();
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
