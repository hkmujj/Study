using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.V5过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V5_Data_Version : baseClass
    {
        private List<Image> _images = new List<Image>();    //图片资源

        public override string GetInfo()
        {
            return "过程数据界面-版本信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_images[0], new Rectangle(0,119,800,417));

            base.paint(dcGs);
        }
    }
}
