using System.IO;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 车顶隔离开关
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainTopSwitch : baseClass
    {
        private Image[] _imageArray;

        public override string GetInfo()
        {
            return "车顶隔离开关";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _imageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[i]), FileMode.Open))
                {
                    _imageArray[i] = Image.FromStream(fs);
                }
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawString("车顶隔离开关", Common._16Font, Common.WhiteBrush, new Point(200, 240));

            if (BoolList[1033])
            {
                g.DrawImage(_imageArray[1], new Rectangle(370, 210, 70, 70));
            }

            if (BoolList[1034])
            {
                g.DrawImage(_imageArray[0], new Rectangle(370, 210, 70, 70));
            }

        }
    }
}
