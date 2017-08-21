using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V47_VersionInfo : baseClass
    {
        private List<Image> _imgs = new List<Image>();

        public override string GetInfo()
        {
            return "版本信息界面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_imgs[0], new Point(0,0));

            base.paint(dcGs);
        }
    }
}
