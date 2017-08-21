using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAeroB类 
    ///     信号屏主界面 B区显示信息 包括预警信息 
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class MMIBlackScreen : baseClass
    {
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "黑 屏 状 态";
        }

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            nErrorObjectIndex = -1;

            append_postCmd(CmdType.SetInBoolValue, this.GetInboolIndex(InBoolKeys.InbATP屏重启标志), 1, 0);

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            if (BoolList[this.GetInboolIndex(InBoolKeys.InbATP屏重启标志)])
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
            else
            {
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf司机号), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf车次号头), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf车次号尾), 0, 0);
                append_postCmd(CmdType.SetFloatValue, this.GetOutFloatIndex(OutFloatKeys.Ouf音量), 0, 0);
            }
        }
    }
}