using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace CRH2MMI.Config.ConfigModel
{
    public class CRH2CommunicationPortRectangleModel : CRH2CommunicationPortModel
    {
        
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

        [XmlAttribute]
        public int X
        {
            set { m_Rectangle.X = value; }
            get { return m_Rectangle.X; }
        }

        [XmlAttribute]
        public int Y
        {
            set { m_Rectangle.Y = value; }
            get { return m_Rectangle.Y; }
        }

        [XmlAttribute]
        public int Width
        {
            set { m_Rectangle.Width = value; }
            get { return m_Rectangle.Width; }
        }

        [XmlAttribute]
        public int Height
        {
            set { m_Rectangle.Height = value; }
            get { return m_Rectangle.Height; }
        }

        [XmlIgnore] 
        private Rectangle m_Rectangle;

        [XmlIgnore]
        public Rectangle Rectangle { set { m_Rectangle = value; }  get { return m_Rectangle; }}


        public CRH2CommunicationPortRectangleModel()
        {
            m_Rectangle = new Rectangle();
        }

    }
}
