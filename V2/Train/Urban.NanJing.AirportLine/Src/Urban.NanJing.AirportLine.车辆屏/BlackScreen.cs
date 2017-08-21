using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackScreen : baseClass
    {
        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, 300, 1, 0);

            return true;
        }

        public override string GetInfo()
        {
            return "黑屏";
        }

        public override void paint(Graphics g)
        {
            if (BoolList[300])
            {
                BasicClass.m_StartTime = DateTime.Now;
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }
    }

}
