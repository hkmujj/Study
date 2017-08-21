using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class HX_BlackScreen : baseClass
    {
        private bool m_Started;

        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);

            return true;
        }

        public override void paint(Graphics g)
        {
            if (BoolList[UIObj.InBoolList[0]] && 
                !m_Started)
            {
                m_Started = true;
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
            else if (m_Started && !BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
                m_Started = false;
            }
        }
    }
}