using System.Drawing;
using ATP200C.MainView.FunctionButton;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.MainView
{
    /// <summary>
    /// 功能描述 GT_MainAeroB类 
    ///     信号屏主界面 B区显示信息 包括预警信息 
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-13
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPBlackScreen : ATPBaseClass
    {
        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "黑 屏 状 态";
        }

        public override bool Initalize()
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="g">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        private void OnDraw(Graphics g)
        {
            if (BoolList[UIObj.InBoolList[0]])
            {
                append_postCmd(CmdType.ChangePage, 11, 0, 0);
            }
            else
            {
                FunctionButtonView.Instance.IsLoad = false;
                FunctionButtonView.Instance.IsSetLoadSound = false;
                FunctionButtonView.Instance.SoudValue = 0;
            }
        }
    }
}
