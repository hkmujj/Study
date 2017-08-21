using System.Windows.Forms;
using System.Xml.Serialization;
using CommonUtil.Model;

namespace General.FlashLoader.FlashView.Model
{
    [XmlRoot]
    public class FlashStartArg
    {
        public string ClilentSenderName { set; get; }

        public string ServiceSenderName { set; get; }

        public string AppName { set; get; }

        public FormBorderStyle FormBorderStyle { set; get; }

        public XmlRectangle Bound { set; get; }

        public bool TopMost { set; get; }

        public string SwfConfigFile { set; get; }
    }
}