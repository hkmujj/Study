using System.Drawing;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class BlackScreen : baseClass
    {
        /// <summary>
        /// 当前视图
        /// </summary>
        private int m_Currentview;

        //2
        public override string GetInfo()
        {
            return "黑屏视图";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            m_Currentview = nParaB;
            base.setRunValue(nParaA, nParaB, nParaC);
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        public void Draw(Graphics e)
        {
            if (m_Currentview == 0)
            {
                if (m_BValue[0])
                {
                    append_postCmd(CmdType.ChangePage, 57, 0, 0);
                }
            }

            if (!m_BValue[0])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }
        }

        private void GetValue()
        {
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[this.UIObj.InBoolList[i]];
            }
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 0);
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
        }

        private bool[] m_BValue;
    }
}