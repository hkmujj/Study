using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 快捷键操作模式
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class FastKeys : baseClass
    {
        #region  重载方法

        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "信号屏快捷键操作";
        }

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {

            nErrorObjectIndex = -1;

            InitData();

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="dcGs">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics dcGs)
        {
            base.paint(dcGs);
            OnDraw(dcGs);
            ButtonDownEvent();
        }

        #endregion

        #region 私有方法

        private void InitData()
        {

        }

        private void OnDraw(Graphics g)
        {
        }

        private void ButtonDownEvent()
        {
            for (int index = 0; index < 11; index++)
            {
                if (ButtonStatus.m_IsBottomButtonUp[index])
                {
                    switch (index)
                    {
                        case 0:
                            if (Main.CurrentModel == ControModelEnum.调车)
                            {
                                ConfirmModelView.m_TheModelSelect = ConfirmModel.退出调车;
                                append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            }
                            else
                            {
                                ConfirmModelView.m_TheModelSelect = ConfirmModel.调车;
                                append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            }
                            break;
                        case 1:
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.目视;
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 2:
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.启动;
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 3:
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.缓解;
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4:
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.上行载频;
                            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub上行按钮标志), 1, 0);
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 5:
                            ConfirmModelView.m_TheModelSelect = ConfirmModel.下行载频;
                            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub下行按钮标志), 1, 0);
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;

                        case 10:
                            append_postCmd(CmdType.SetBoolValue, this.GetOutBoolIndex(OutBoolKeys.Oub警惕), 1, 0);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
