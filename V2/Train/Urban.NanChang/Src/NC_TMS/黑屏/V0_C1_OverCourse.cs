using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace NC_TMS.黑屏
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V0_C1_OverCourse_2D : baseClass
    {
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "结束课程";
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                switch (nParaB)
                {
                    case 100:
                        VC_C2_GetValue.IsHandleBroadMode = false;
                        VC_C2_GetValue.CurrentStationID = 0;
                        VC_C2_GetValue.EndStationID = 0;
                        VC_C2_GetValue.StartStationID = 0;
                        VC_C2_GetValue.NextStationID = 0;
                        VC_C0_Title.BoardMode = "----";
                        break;
                }
            }
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }
    }
}
