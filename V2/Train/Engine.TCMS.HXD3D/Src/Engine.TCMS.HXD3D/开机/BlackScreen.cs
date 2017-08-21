using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.开机
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BlackScreen : baseClass
    {
        // Fields
        private bool[] m_BValue;

        // Methods
        public void Draw(Graphics e)
        {
            if (m_BValue[0])
            {
                append_postCmd(CmdType.ChangePage, 2, 0, 0f);
            }
        }

        public override string GetInfo()
        {
            return "黑屏视图";
        }

        private void GetValue()
        {
            for (var i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 0f);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];

            SetValueWhenDebug();
        }

        private void SetValueWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 1);
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }
    }
}