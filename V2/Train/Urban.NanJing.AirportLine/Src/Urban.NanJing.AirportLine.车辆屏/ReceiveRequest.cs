using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ReceiveRequest : baseClass
    {
        public override string GetInfo()
        {
            return "接受硬IO数据";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics g)
        {

        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            switch (nParaB)
            {
                case 17:
                    {

                    }
                    break;
            }
        }
    }
}
