using System.Drawing;

namespace Urban.QingDao.VT
{
    public struct VTControlStruct
    {
        public ControlType Type;
        public int ControlID;
        public int DefaultStatus;
        public int ControlStatus;
        public string Text;
        public int InKey;
        public int OutKey;
        public Rectangle Rect;
        public Image CurrentIamge;
        public Image DefaultImage;
        public PageType PageType;
    }
}