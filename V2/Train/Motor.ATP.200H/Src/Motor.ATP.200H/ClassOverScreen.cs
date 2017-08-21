using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Motor.ATP._200H
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ClassOverScreen : baseClass
    {
        #region  重载方法
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "课程结束视图";
        }


        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            GetValue();
            OnDraw(dcGs);
        }
        #endregion

        #region  私有方法
        private void GetValue()
        {

        }

        private void OnDraw(Graphics g)
        {
            TextInfo.ClearCurrentInforRecords();
        }
        #endregion
    }
}
