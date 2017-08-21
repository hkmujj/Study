using System.Collections.Generic;
using System.Drawing;
using System.IO;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.QingDao3Line.MMI.黑屏
{
    //课程结束视图
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ClassOverScreen : baseClass
    {
        //2
        public override string GetInfo()
        {
            return "课程结束视图";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            return Init();
        }

        public override void paint(Graphics g)
        {
            GetValue();
        }

        private void GetValue()
        {
            int index = 0;
            for (index = 0; index < m_BValue.Length; index++)
            {
                m_BValue[index] = BoolList[UIObj.InBoolList[index]];
            }
            for (index = 0; index < m_FValue.Length; index++)
            {
                m_FValue[index] = FloatList[UIObj.InFloatList[index]];
            }
        }

        private bool Init()
        {
            m_BValue = new bool[this.UIObj.InBoolList.Count];
            m_FValue = new float[this.UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            for (int index = 0; index < UIObj.ParaList.Count; index++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[index])));
            }
            return true;
        }

        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;

    }
}
