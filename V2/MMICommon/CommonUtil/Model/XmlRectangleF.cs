using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace CommonUtil.Model
{
        /// <summary>
    /// 可xml序列化的 RectangleF
    /// </summary>
    [XmlType("RectangleF")]
    public class XmlRectangleF 
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
                    m_RectangleF = new RectangleF(datas[0].AsFloat(), datas[1].AsFloat(), datas[2].AsFloat(), datas[3].AsFloat());
                }

            }
            get
            {
                return string.Format("{0},{1},{2},{3}", m_RectangleF.X, m_RectangleF.Y, m_RectangleF.Width, m_RectangleF.Height);
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
                    m_RectangleF = new RectangleF(datas[0].AsFloat(), datas[1].AsFloat(), m_RectangleF.Width, m_RectangleF.Height);
                }

            }
            get
            {
                return string.Format("{0},{1}", m_RectangleF.X, m_RectangleF.Y);
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
                    m_RectangleF = new RectangleF(m_RectangleF.X, m_RectangleF.Y, datas[0].AsFloat(), datas[1].AsFloat());
                }

            }
            get
            {
                return string.Format("{0},{1}", m_RectangleF.Width, m_RectangleF.Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public float X
        {
            set { m_RectangleF.X = value; }
            get { return m_RectangleF.X; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public float Y
        {
            set { m_RectangleF.Y = value; }
            get { return m_RectangleF.Y; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public float Width
        {
            set { m_RectangleF.Width = value; }
            get { return m_RectangleF.Width; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public float Height
        {
            set { m_RectangleF.Height = value; }
            get { return m_RectangleF.Height; }
        }

        [XmlIgnore] 
        private RectangleF m_RectangleF;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public RectangleF RectangleF { set { m_RectangleF = value; }  get { return m_RectangleF; }}


        /// <summary>
        /// 
        /// </summary>
        public XmlRectangleF()
        {
            m_RectangleF = new Rectangle();
        }

    }
}
