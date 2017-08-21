using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 黄色警惕
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class YelllowAlert : baseClass
    {
        public static int _value = 0;
        private DateTime _dateTime;
        public override string GetInfo()
        {
            return "黄色警惕";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        bool _is20Used = false;
        bool _is10used = false;

        public override void paint(Graphics dcGs)
        {
            if (BoolList[1219] && !_is20Used)
            {
                _value = 20;
                _dateTime = DateTime.Now;
                _is20Used = true;
            }

            if (BoolList[1220] && !_is10used)
            {
                _value = 10;
                _dateTime = DateTime.Now;
                _is10used = true;
            }
            if (BoolList[1219] || BoolList[1220])
            {
                if (_value - (DateTime.Now - _dateTime).Seconds > 0)
                {
                    dcGs.FillRectangle(Common.YellowBrush, new Rectangle(215, 200, 370, 240));
                    dcGs.DrawString("警惕", new Font("Arial", 25), Common.RedBrush, new Point(350, 230));
                    dcGs.DrawString((_value - (DateTime.Now - _dateTime).Seconds).ToString(), new Font("Arial", 40), Common.RedBrush, new Point(350, 300));
                }
                else
                {
                    dcGs.FillRectangle(Common.YellowBrush, new Rectangle(215, 200, 370, 240));
                    dcGs.DrawString("警惕", new Font("Arial", 25), Common.RedBrush, new Point(350, 230));
                    dcGs.DrawString("0", new Font("Arial", 40), Common.RedBrush, new Point(350, 300));

                }
            }


            if (!BoolList[1219] && _is20Used)
                _is20Used = false;
            if (!BoolList[1220] && _is10used)
                _is10used = false;

        }
    }
}