using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C10FalutMessage : baseClass
    {
        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行界面-显示故障";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
        #endregion

        public override void paint(Graphics g)
        {
            paint_Falut(g);

            base.paint(g);
        }

        private int m_Time = 0;
        private int m_Intercal = 5;

        private void paint_Falut(Graphics g)
        {
            if (VCGetFaluts.CurrentFault == null)
                return;

            g.DrawString(
                VCGetFaluts.CurrentFault.Display,
                Global.m_FontArial12B,
                Brushes.Red,
                new RectangleF(107, 513, 485, 41),
                Global.m_SfCc
                );

            m_Intercal = BoolList[UIObj.InBoolList[0]] ? 2 : 5;

            if (VCGetFaluts.CurrentFault.Logic == UIObj.InBoolList[1])
            {
                if (m_Time == 0)
                    g.FillEllipse(Brushes.Red,
                        new RectangleF(442, 77, 30, 30)
                        );

                m_Time = (m_Time + 1) % m_Intercal;
            }
            else m_Time = 0;
        }
    }
}
