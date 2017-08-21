using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using MMI.Facility.Interface;

namespace Urban.QingDao.VT
{
    public static class VTExtend
    {
        public static ControlType ToControlType(this object type)
        {
            return (ControlType)Convert.ToInt32(type.ToString());
        }

        public static PageType ToPageType(this object type)
        {
            return (PageType)Convert.ToInt32(type.ToString());
        }

        public static int ToInt(this object type)
        {
            if (string.IsNullOrEmpty(type.ToString()))
            {
                return 0;
            }
            return (int)double.Parse(type.ToString());
        }

        public static Rectangle ToRetangle(this object type)
        {
            var tep = type.ToString().Split(',');
            Rectangle rect;
            rect = tep.Length == 4 ? new Rectangle(tep[0].ToInt(), tep[1].ToInt(), tep[2].ToInt(), tep[3].ToInt()) : new Rectangle();
            return rect;
        }

        public static Image ToImage(this object type, baseClass baseClass)
        {
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                return Image.FromFile(Path.Combine(baseClass.RecPath, type.ToString()));
            }
            return null;
        }

        public static VTControl ToVTControl(this VTControlStruct control, baseClass baseClass)
        {
            var tmp = new VTControl(control.Type, control.ControlID, control.DefaultStatus, baseClass, control.PageType);
            tmp.Images = new Dictionary<int, Tuple<Image, int>>();
            tmp.Images.Add(control.DefaultStatus, new Tuple<Image, int>(control.DefaultImage, 0));
            if (tmp.Type != ControlType.Button && tmp.Type != ControlType.Back && tmp.Type != ControlType.Dial)
            {
                tmp.Images.Add(control.ControlStatus, new Tuple<Image, int>(control.CurrentIamge, control.OutKey));
            }
            tmp.InKey = control.InKey;
            tmp.Text = control.Text;
            tmp.OutRectangle = control.Rect;

            return tmp;
        }
    }
}