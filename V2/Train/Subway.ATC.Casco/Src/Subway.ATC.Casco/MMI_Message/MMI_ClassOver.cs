using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.Casco.MMI_Message
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMI_ClassOver : ATCBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "MMI课程结束视图";
        }


        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {
                _msgClear = true;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            GetValue();
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private void GetValue()
        {
            if (_msgClear)
            {
                MMI_MsgReceive.msgInf.ClearAllList();
                MMI_ClassBegin.TheD_Value = 0;
            }

        }
        #endregion

        private bool _msgClear = false;

    }
}