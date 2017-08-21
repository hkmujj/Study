using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.Wuxi.TMS
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Meter : TMSBaseClass
    {
         /// <summary>
         /// 矩形框列表
         /// </summary>
        private List<Rectangle> m_Rectangles;

        #region :::::::::::::::::::::::::: 初始化 重载 :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "仪表";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            Draw(dcGs);

        }

        private void Draw(Graphics e)
        {
            e.DrawEllipse(FormatStyle.m_WhitePen,m_Rectangles[0]);
         
        }

        private void InitData()
        {
            m_Rectangles=new List<Rectangle>();
            m_Rectangles.Add(new Rectangle(280,60,200,200));
        }
        
        #endregion
    }
}
