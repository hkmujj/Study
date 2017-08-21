using System;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.Casco.MMI_Message
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_ClassBegin : ATCBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "MMI课程开始";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics g)
        {
            GetValue();
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            currentCycleValue = Convert.ToInt32(FloatList[199]);
            if (currentCycleValue != 0)
            {
                MMI_Main.MMI_Main.ClassBegin = true;
                if (currentCycleValue != lastCycleValue)
                {
                    TheD_Value = currentCycleValue - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
                }
            }
            else
            {
                TheD_Value = 0;
            }
            lastCycleValue = currentCycleValue;
            //append_postCmd(CmdType.SetFloatValue, 299, 0, TheD_Value);
        }
        #endregion

        int currentCycleValue;
        int lastCycleValue;
        public static int TheD_Value;
    }
}