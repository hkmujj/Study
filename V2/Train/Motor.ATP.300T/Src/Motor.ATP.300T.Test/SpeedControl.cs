using System;
using System.Drawing;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.ATP._300T.Test
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class SpeedControl : MMIBaseClass
    {
        private Random m_Random;

        public override bool init(ref int nErrorObjectIndex)
        {
            AppLog.Info("Initalize test.");
            m_Random = new Random();
            append_postCmd(CmdType.SetInBoolValue, 23, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 32, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 53, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 63, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 26, 1, 0);
            return true;
        }

        public override void paint(Graphics g)
        {
            append_postCmd(CmdType.SetInFloatValue, 1, m_Random.Next(0, 600), 1);
            append_postCmd(CmdType.SetInFloatValue, 2, m_Random.Next(0, 600), 2);
            append_postCmd(CmdType.SetInFloatValue, 3, m_Random.Next(0, 600), 3);
            append_postCmd(CmdType.SetInFloatValue, 4, m_Random.Next(0, 600), 4);
            append_postCmd(CmdType.SetInFloatValue, 5, m_Random.Next(0, 600), 5);
            append_postCmd(CmdType.SetInFloatValue, 6, m_Random.Next(0, 600), 6);
            
            append_postCmd(CmdType.SetInFloatValue, 9, 6, m_Random.Next(0, 11));

            for (var i = 20; i < 40; i++)
            {
                append_postCmd(CmdType.SetInFloatValue, i, 0, m_Random.Next(0, 3200));
            }

            for (var i = 41; i < 47; i+=2)
            {
                append_postCmd(CmdType.SetInFloatValue, i, 0, m_Random.Next(0, 3200));
            }

            for (var i = 42; i < 47; i += 2)
            {
                append_postCmd(CmdType.SetInFloatValue, i, 0, m_Random.Next(1, 3200));
            }


            for (var i = 50; i < 71; i += 2)
            {
                append_postCmd(CmdType.SetInFloatValue, i, 0, m_Random.Next(0, 3200));
            }

            for (var i = 51; i < 71; i += 2)
            {
                append_postCmd(CmdType.SetInFloatValue, i, 0, m_Random.Next(0, 3));
            }

        }
    }
}
