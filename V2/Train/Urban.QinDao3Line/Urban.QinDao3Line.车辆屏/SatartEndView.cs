using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI
{

    [GksDataType(DataType.isMMIObjectClass)]
    class SatartEndView : NewQBaseclass
    {
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            StartView();
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        private void StartView()
        {

            //BoolList[_boolIds[1]] = false;
            append_postCmd(CmdType.SetInBoolValue, m_BoolIds[1], 0, 0);

            if (BoolList[m_BoolIds[0]])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }

        }
    }
}
