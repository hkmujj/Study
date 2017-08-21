using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.DMI.Index;
using Urban.Iran.DMI.Index.IndexKeys;
using Urban.Iran.DMI.Resources.Facade;

namespace Urban.Iran.DMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ErrorPage : baseClass
    {
        private Image m_BmpError;

        public override string GetInfo()
        {
            return "ErrorPage";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_BmpError = ImageResourceFacade.atpError;

            return true;
        }

        public override void paint(Graphics g)
        {
            //////////////////////////////////////////////////////////////////////////
            //∫⁄∆¡¥¶¿Ì
            if (!BoolList[IndexParam.Instance.InBoolKeyIndexDictionary[InBoolKeys.MMI∆¡¡¡∆¡]])
            {
                append_postCmd(CmdType.ChangePage, 51, 0, 0);
            }
            //////////////////////////////////////////////////////////////////////////
            g.DrawImage(m_BmpError, GlobleRect.BKRect);
        }
    }
}
