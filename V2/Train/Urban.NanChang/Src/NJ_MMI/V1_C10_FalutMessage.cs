using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C10_FalutMessage : baseClass
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

        public override void paint(Graphics dcGs)
        {
            paint_Falut(dcGs);

            base.paint(dcGs);
        }

        private int _time = 0;
        private int _intercal = 5;

        private void paint_Falut(Graphics dcGs)
        {
            //if (VC_C0_GetValue.CurrentFalut == null)
            //    return;

            //dcGs.DrawString(
            //    VC_C0_GetValue.CurrentFalut.Description,
            //    new Font("Arial", 12, FontStyle.Bold),
            //    Brushes.Red,
            //    new RectangleF(107, 513, 485, 41),
            //    Global.SF_CC
            //    );

            _intercal = 5;

            //if (VC_C0_GetValue.CurrentFalut.LogicID == UIObj.InBoolList[1])
            //{
            if (BoolList[UIObj.InBoolList[0]])
            {
                if (_time == 0 || _time == 1 || _time == 2)
                    dcGs.FillEllipse(Brushes.Red,
                        new RectangleF(442, 77, 30, 30)
                        );

                _time = (_time + 1) % _intercal;
            }
            //}
            //else _time = 0;
        }
    }
}
