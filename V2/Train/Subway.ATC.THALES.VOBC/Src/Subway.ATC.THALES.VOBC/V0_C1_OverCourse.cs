using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0_C1_OverCourse_1D : baseClass
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
            if(nParaA==2)
            {
                if (nParaB == 100)
                {
                    VC_C0_GetValue.CurrentInfos = new System.Collections.Generic.List<MessageInfo>();
                    VC_C0_GetValue.CurrentFaluts = new System.Collections.Generic.List<MessageInfo>();
                    VC_C0_GetValue.AllFaluts = new System.Collections.Generic.List<MessageInfo>();
                    VC_C0_GetValue.AllInfos = new System.Collections.Generic.List<MessageInfo>();
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
