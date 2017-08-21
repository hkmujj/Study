using MMI.Facility.Interface;
using Motor.ATP._200H.Common;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 会被显示类型影响的基类
    /// </summary>
    public class ShowTypeEffectBaseClass : baseClass
    {
        protected ShowType ShowType { get; private set; }

        private int m_ShowTypeInboolIndex;

        public  override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            m_ShowTypeInboolIndex = GlobalParam.Instance.FindInBoolIndex("屏切换到辅助显示");

            Initalize();

            return true;
        }

        protected virtual void Initalize()
        {
            
        }

        /// <summary>
        /// 刷新辅助模式
        /// </summary>
        protected void RefreshShowType()
        {
            ShowType = ShowType.Normal;
            if (BoolList[m_ShowTypeInboolIndex])
            {
                ShowType = ShowType.Assistant;
            }
        }
    }
}